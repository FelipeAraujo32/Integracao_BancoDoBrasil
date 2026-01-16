using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using BancoDoBrasil.Formatting;

namespace BancoDoBrasil.Serialization;

public sealed class BbDateJsonConverter : JsonConverter<DateTime>
{
    public override DateTime Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options)
    {
        throw new NotSupportedException(
            "Leitura de datas no formato BB não é suportada.");
    }

    public override void Write(
        Utf8JsonWriter writer,
        DateTime value,
        JsonSerializerOptions options)
    {
        // DateTime -> string no formato do BB
        writer.WriteStringValue(
            BbDateFormatter.Format(value));
    }
}