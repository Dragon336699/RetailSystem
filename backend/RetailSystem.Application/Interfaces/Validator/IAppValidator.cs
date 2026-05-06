namespace RetailSystem.Application.Interfaces.Validator
{
    public interface IAppValidator
    {
        Task ValidateAsync<T>(T model);
    }
}
