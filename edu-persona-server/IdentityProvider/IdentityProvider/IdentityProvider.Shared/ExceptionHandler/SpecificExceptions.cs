using System.Net;

namespace IdentityProvider.Shared.ExceptionHandler
{
    public class SpecificExceptions
    {
        public class BadRequestException : AppException
        {
            public BadRequestException(string message)
                : base(message, HttpStatusCode.BadRequest) { }
        }

        public class NotFoundException : AppException
        {
            public NotFoundException(string message)
                : base(message, HttpStatusCode.NotFound) { }
        }

        public class UnauthorizedException : AppException
        {
            public UnauthorizedException(string message)
                : base(message, HttpStatusCode.Unauthorized) { }
        }

        public class ForbiddenException : AppException
        {
            public ForbiddenException(string message)
                : base(message, HttpStatusCode.Forbidden) { }
        }
    }
}