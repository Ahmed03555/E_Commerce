using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Application.Common
{
    public record Error (string Code,string Description ,ErrorType ErrorType = ErrorType.Failure )
    {
        public static Error Failure(string code ="General Failure",string description = "General Failure Has Occurred")
        {
            return new Error(code,description, ErrorType.Failure);
        }

        public static Error Validation(string code = "General Validation", string description = "General Validation Has Occurred")
        {
            return new Error(code, description, ErrorType.Validation);
        }
        public static Error NotFound(string code = "General NotFound", string description = "General NotFound Has Occurred")
        {
            return new Error(code, description, ErrorType.NotFound);
        }
        public static Error Conflict(string code = "General Conflict", string description = "General Conflict Has Occurred")
        {
            return new Error(code, description, ErrorType.Conflict);
        }
        public static Error Unauthorized(string code = "General Unauthorized", string description = "General Unauthorized Has Occurred")
        {
            return new Error(code, description, ErrorType.Failure);
        }
        public static Error Forbidden(string code = "General Failure", string description = "Forbidden Failure Has Occurred")
        {
            return new Error(code, description, ErrorType.Forbidden);
        }
        public static Error InvalidCredential(string code = "General InvalidCredential", string description = "General InvalidCredential Has Occurred")
        {
            return new Error(code, description, ErrorType.InvalidCredential);
        }
    }
    public enum ErrorType
    {
        Failure = 0,
        Validation =1,
        NotFound=2,
        Conflict=3,
        Unauthorized = 4,
        Forbidden=5,
        InvalidCredential = 6,
    }
}
