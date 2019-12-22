using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace BlueTapeCrew.Extensions
{
    public static class ObjectExtensions
    {
        private static readonly JsonSerializerSettings Settings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };
        public static string ToJson(this object obj) => JsonConvert.SerializeObject(obj, Settings);
    }
}
