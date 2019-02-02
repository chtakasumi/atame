using Newtonsoft.Json;

namespace api.libs
{
    public static class Json
    {

        public static string ToJson<T>(T objeto)
        {
            return JsonConvert.SerializeObject(objeto);
        }

        public static T ToObject<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
