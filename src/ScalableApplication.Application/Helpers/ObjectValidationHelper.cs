using ScalableApplication.Application.Interfaces.Helpers;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ScalableApplication.Application.Helpers
{
    public class ObjectValidationResult
    {
        public bool IsValid { get; set; }
        public string? Error { get; set; }
    }

    public class ObjectValidationHelper : IObjectValidationHelper
    {
        public ObjectValidationResult ValidateObject<T>(T obj)
        {
            List<ValidationResult> validationResults = new();
            StringBuilder errors = new();

            if (!Validator.TryValidateObject(obj!, new ValidationContext(obj!, null, null), validationResults, true))
            {
                foreach (var err in validationResults)
                {
                    errors.AppendLine($"{err.ErrorMessage}");
                }
                return new ObjectValidationResult
                {
                    IsValid = false,
                    Error = errors.ToString()
                };
            }

            return new ObjectValidationResult
            {
                IsValid = true,
                Error = string.Empty
            };
        }
    }
}
