namespace Hospital.Application.Exceptions;
internal class HandlerNotFoundException(string handlerType) : Exception($"Handler with type {handlerType} not found.") { }
