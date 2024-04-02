using UnityEngine;

namespace MVVM.Serialize
{
    public class SerializeJson : ISerializer
    {
        public string Serialize<T>(T obj, bool readableOutput = false) where T : class, new()
        {
            //JsonUtility.ToJson(obj) something....
            throw new System.NotImplementedException();
        }

        public T Deserialize<T>(string json) where T : class, new()
        {
            return JsonUtility.FromJson<T>(json);
        }
    }
}