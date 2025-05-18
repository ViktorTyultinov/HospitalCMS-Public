namespace Hospital.Application.Exceptions;
internal class InvalidHandlerResultException(string requestType) : Exception($"Handler for {requestType} returned null or invalid type.") { }