using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Services.Extensions
{
    public static class ObjectExtensions
    {
        private static readonly JsonSerializerSettings Settings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };

        public static string ToJson(this object obj, bool prettyPrint = false)
        {
            Settings.Formatting = prettyPrint ? Formatting.Indented : Formatting.None;
            var json = JsonConvert.SerializeObject(obj, Settings);
            return json;
        }
    }
}
