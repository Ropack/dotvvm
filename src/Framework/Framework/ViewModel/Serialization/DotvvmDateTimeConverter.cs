using System;
using System.Globalization;
using Newtonsoft.Json;

namespace DotVVM.Framework.ViewModel.Serialization
{
    public class DotvvmDateTimeConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value == null)
            {
                writer.WriteNull();
            }
            else
            {
                var date = (DateTime) value;
                var dateWithoutTimezone = new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second);
                writer.WriteValue(dateWithoutTimezone.ToString("O", CultureInfo.InvariantCulture));
            }
        }

        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
            {
                if (objectType == typeof(DateTime))
                {
                    return DateTime.MinValue;
                }
                else
                {
                    return null;
                }
            }
            else if (reader.TokenType == JsonToken.Date)
            {
                return (DateTime) reader.Value;
            }
            else if (reader.TokenType == JsonToken.String
                     && DateTime.TryParseExact((string)reader.Value, "O", CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
            { 
                return date;
            }

            throw new JsonSerializationException("The value specified in the JSON could not be converted to DateTime!");
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof (DateTime) || objectType == typeof (DateTime?);
        }
    }
}
