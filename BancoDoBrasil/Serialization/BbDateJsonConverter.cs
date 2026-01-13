using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using BancoDoBrasil.Formatting;

namespace BancoDoBrasil.Serialization
{
    public sealed class BbDateJsonConverter : JsonConverter<DateTime>
    {
        public override DateTime Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options)
        {
            return DateTime.Parse(reader.GetString()!);
        }

        public override void Write(
            Utf8JsonWriter writer,
            DateTime value,
            JsonSerializerOptions options)
        {
            var formatted = BbDateFormatter.FormatDate(value);
            writer.WriteStringValue(formatted);
        }
    }
}
