using System.Text.Json;
using System.Text.Json.Serialization;
using BancoDoBrasil.Serialization;

namespace BancoDoBrasil.Tests.Common;

public static class JsonOptionsFactory
{
    public static JsonSerializerOptions Create()
        => new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            Converters = { new BbDateJsonConverter() }
        };
}
