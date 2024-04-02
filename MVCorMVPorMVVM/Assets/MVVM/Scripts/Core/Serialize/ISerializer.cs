namespace MVVM.Serialize
{
    /// <summary>
    /// 序列化接口
    /// </summary>
    public interface ISerializer
    {
        //序列化
        string Serialize<T>(T obj, bool readableOutput = false) where T : class, new();
        //反序列化
        T Deserialize<T>(string json) where T : class, new();
    }
}