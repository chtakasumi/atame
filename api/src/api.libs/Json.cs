using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace api.libs
{
    public static class Json
    {

        public static string ToJson<T>(T objeto)
        {
            var settings = new JsonSerializerSettings();
            settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            settings.Formatting = Formatting.Indented;
            return JsonConvert.SerializeObject(objeto, settings);
        }

        public static T ToObject<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
