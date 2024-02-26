namespace MVVM.Message
{
    public class MessageArgs<T>
    {
        public string _info;
        public MessageArgs(string info)
        {
            _info = info;
        }
    }
}