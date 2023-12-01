using System.Dynamic;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace SampleMicroserviceApp.Identity.Infrastructure.Persistence.EFCore.Extensions;

public static class JsonDbFunctions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="expression">Db column value</param>
    /// <param name="path">Example: $.hn -> HostName JsonProperty field  </param>
    /// <returns></returns>
    public static string? Value(string expression, string path)
    {
        // for UseInMemoryDatabase provider support

        var dynamicObject = JsonSerializer.Deserialize<ExpandoObject>(expression);

        var jsonFieldName = path.Replace("$.", "");

        return dynamicObject?.FirstOrDefault(p => p.Key == jsonFieldName).Value?.ToString();

        //throw new InvalidOperationException($"{nameof(Value)}cannot be called client side");
    }
}