using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WikiAPI.Application.Contracts.Infrastructure;

namespace WikiAPI.Infrastructure.FileImport;

public class JsonImporter : IJsonImporter
{
    public async Task<IEnumerable<T>?> ConvertJsonToEntityAsync<T>(string filePath)
    {
        string jsonContent = await File.ReadAllTextAsync(filePath);
        var entityList = JsonConvert.DeserializeObject<List<T>>(jsonContent);

        return entityList;
    }
}
