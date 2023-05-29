using AutoMapper;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs;
using Licenta.Core.Interfaces;
using Licenta.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using Azure;
using Microsoft.AspNetCore.Http;
using System.IO;
using Licenta.Services.DTOs.Blob;
using Licenta.Core.Enums;
using Licenta.Core.Entities;
using Licenta.Services.Exceptions;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Licenta.Services.Managers;

public class FileManager : IFileManager
{
    private readonly string _storageConnectionString;
    private readonly string _storageContainerName;
    private readonly IRepository<Core.Entities.File> _fileRepository;
    private readonly IRepository<Event> _eventRepository;
    private readonly IRepository<Partner> _partnerRepository;
    private readonly IRepository<Student> _studentRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<FileManager> _logger;

    public FileManager(
        IConfiguration configuration, 
        ILogger<FileManager> logger,
        IRepository<Core.Entities.File> fileRepository,
        IMapper mapper,
        IRepository<Event> eventRepository,
        IRepository<Partner> partnerRepository,
        IRepository<Student> studentRepository)
    {
        _storageConnectionString = configuration.GetValue<string>("BlobConnectionString");
        _storageContainerName = configuration.GetValue<string>("BlobContainerName");
        _fileRepository = fileRepository;
        _mapper = mapper;
        _logger = logger;
        _eventRepository = eventRepository;
        _partnerRepository = partnerRepository;
        _studentRepository = studentRepository;
    }

    // TODO: get by id method

    public async Task<List<BlobDTO>> ListAsync()
    {
        BlobContainerClient container = new BlobContainerClient(_storageConnectionString, _storageContainerName);

        List<BlobDTO> files = new List<BlobDTO>();

        await foreach (BlobItem file in container.GetBlobsAsync())
        {
            string uri = container.Uri.ToString();
            var name = file.Name;
            var fullUri = $"{uri}/{name}";

            files.Add(new BlobDTO
            {
                Uri = fullUri,
                Name = name,
                ContentType = file.Properties.ContentType
            });
        }

        return files;
    }

    public async Task<BlobResponseDTO> UploadAsync(FileEntityEnum entity, long id, IFormFile blob)
    {
        BlobResponseDTO response = new();

        BlobContainerClient container = new BlobContainerClient(_storageConnectionString, _storageContainerName);

        try
        {
            var uploaded_file = new Core.Entities.File();

            BlobClient client = container.GetBlobClient(blob.FileName);

            await using (Stream? data = blob.OpenReadStream())
            {
                await client.UploadAsync(data);
            }

            response.Status = $"File {blob.FileName} Uploaded Successfully";
            response.Error = false;
            response.Blob.Uri = client.Uri.AbsoluteUri;
            response.Blob.Name = client.Name;

            if (entity.Equals(FileEntityEnum.Event))
            {
                var post = await _eventRepository.FindByIdAsync(id);
                if (post == null)
                {
                    throw new CustomNotFoundException("Post Not Found");
                }
                uploaded_file = new Core.Entities.File
                {
                    Name = blob.FileName,
                    Uri = response.Blob.Uri,
                    Entity = FileEntityEnum.Event,
                    EventId = post.Id
                };
                await _eventRepository.UpdateAsync(post);
            }
            else if(entity.Equals(FileEntityEnum.Partner))
            {
                var partner = await _partnerRepository.FindByIdAsync(id);
                if (partner == null)
                {
                    throw new CustomNotFoundException("Partner Not Found");
                }
                uploaded_file = new Core.Entities.File
                {
                    Name = blob.FileName,
                    Uri = response.Blob.Uri,
                    Entity = FileEntityEnum.Partner,
                    PartnerId = partner.Id
                };
                await _partnerRepository.UpdateAsync(partner);
            }
            else if (entity.Equals(FileEntityEnum.Student))
            {
                var student = await _studentRepository.FindByIdAsync(id);
                if (student == null)
                {
                    throw new CustomNotFoundException("Student Not Found");
                }
                uploaded_file = new Core.Entities.File
                {
                    Name = blob.FileName,
                    Uri = response.Blob.Uri,
                    Entity = FileEntityEnum.Student,
                    StudentId = student.Id
                };
                await _studentRepository.UpdateAsync(student);
            }
            await _fileRepository.AddAsync(uploaded_file);
        }

        catch (RequestFailedException ex)
            when (ex.ErrorCode == BlobErrorCode.BlobAlreadyExists)
            {
                _logger.LogError($"File with name {blob.FileName} already exists in container. Set another name to store the file in the container: '{_storageContainerName}.'");
                response.Status = $"File with name {blob.FileName} already exists. Please use another name to store your file.";
                response.Error = true;
                return response;
            }
        catch (RequestFailedException ex)
        {
            _logger.LogError($"Unhandled Exception. ID: {ex.StackTrace} - Message: {ex.Message}");
            response.Status = $"Unexpected error: {ex.StackTrace}. Check log with StackTrace ID.";
            response.Error = true;
            return response;
        }

        return response;
    }

    public async Task<BlobDTO> DownloadAsync(string blobFilename)
    {
        BlobContainerClient client = new BlobContainerClient(_storageConnectionString, _storageContainerName);

        try
        {
            BlobClient file = client.GetBlobClient(blobFilename);

            if (await file.ExistsAsync())
            {
                var data = await file.OpenReadAsync();
                Stream blobContent = data;

                var content = await file.DownloadContentAsync();

                string name = blobFilename;
                string contentType = content.Value.Details.ContentType;

                return new BlobDTO { Content = blobContent, Name = name, ContentType = contentType };
            }
        }
        catch (RequestFailedException ex)
            when (ex.ErrorCode == BlobErrorCode.BlobNotFound)
        {
            _logger.LogError($"File {blobFilename} was not found.");
        }

        return null;
    }

    public async Task<BlobResponseDTO> DeleteAsync(string blobFilename)
    {
        BlobContainerClient client = new BlobContainerClient(_storageConnectionString, _storageContainerName);

        BlobClient file = client.GetBlobClient(blobFilename);

        try
        {
            await file.DeleteAsync();
        }
        catch (RequestFailedException ex)
            when (ex.ErrorCode == BlobErrorCode.BlobNotFound)
        {
            _logger.LogError($"File {blobFilename} was not found.");
            return new BlobResponseDTO { Error = true, Status = $"File with name {blobFilename} not found." };
        }

        var fileToDelete = await _fileRepository
            .AsQueryable()
            .Where(x => x.Name == blobFilename)
            .FirstOrDefaultAsync();

        if (fileToDelete == null)
        {
            throw new CustomNotFoundException("File Not Found");
        }
        await _fileRepository.RemoveAsync(fileToDelete);

        return new BlobResponseDTO { Error = false, Status = $"File: {blobFilename} has been successfully deleted." };

    }
}
