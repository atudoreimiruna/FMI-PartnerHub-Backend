﻿using System;

namespace Licenta.Services.Exceptions;

[Serializable]
public class CustomBadRequestException : Exception
{
    public CustomBadRequestException() { }
    public CustomBadRequestException(string message) : base(message) { }
}

[Serializable]
public class CustomUnauthorizedException : Exception
{
    public CustomUnauthorizedException() { }
    public CustomUnauthorizedException(string message) : base(message) { }
}

[Serializable]
public class CustomForbiddenException : Exception
{
    public CustomForbiddenException() { }
    public CustomForbiddenException(string message) : base(message) { }
}

[Serializable]
public class CustomNotFoundException : Exception
{
    public CustomNotFoundException() { }
    public CustomNotFoundException(string message) : base(message) { }
}


