using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace UdpServer.Services.Services;
public static class JsonProcessor
{
    public static string Serialize<T>(T obj) 
        => JsonConvert.SerializeObject(obj);
    public static dynamic Deserialize(string jsonString) 
        => JsonConvert.DeserializeObject<dynamic>(jsonString);
    public static JObject ParseToJObj(string json) 
        => JObject.Parse(json);
}