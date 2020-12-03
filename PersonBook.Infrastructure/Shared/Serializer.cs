using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using PersonBook.Infrastructure.Shared.Settings;

namespace PersonBook.Infrastructure.Shared
{
    public class Serializer
    {
        public static T As<T>(string json)
        {
            if (string.IsNullOrEmpty(json))
            {
                return default;
            }

            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Objects,
                DateFormatString = SystemSettings.ShortDatePattern
            };
            settings.Converters.Add(new IsoDateTimeConverter() { DateTimeFormat = SystemSettings.LongDatePattern });
            return JsonConvert.DeserializeObject<T>(json, settings);
        }

        public static string Serialize(object value, bool typeNameHandling = true, bool camelCase = false)
        {
            var settings = new JsonSerializerSettings
            {
                DateFormatString = SystemSettings.ShortDatePattern,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            };

            if (typeNameHandling)
            {
                settings.TypeNameHandling = TypeNameHandling.Objects;
            }

            if (camelCase)
            {
                settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            }

            settings.Converters.Add(new IsoDateTimeConverter() { DateTimeFormat = SystemSettings.LongDatePattern });
            var json = JsonConvert.SerializeObject(value, settings);

            return json;
        }
    }
}
