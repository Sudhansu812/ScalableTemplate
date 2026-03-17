using ScalableApplication.Application.Helpers;

namespace ScalableApplication.Application.Interfaces.Helpers
{
    public interface IObjectValidationHelper
    {
        public ObjectValidationResult ValidateObject<T>(T obj);
    }
}
