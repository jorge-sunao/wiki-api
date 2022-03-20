namespace WikiAPI.Application.Contracts.Infrastructure;

public interface IJsonImporter
{
    Task<IEnumerable<T>?> ConvertJsonToEntityAsync<T>(string filePath);
}
