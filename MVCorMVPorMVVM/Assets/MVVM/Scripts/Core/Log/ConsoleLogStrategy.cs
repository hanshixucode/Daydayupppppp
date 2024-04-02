namespace MVVM.Log
{
    public class ConsoleLogStrategy : LogStrategy
    {
        public ConsoleLogStrategy()
        {
            SetContentWriter();
        }
        protected override void RecordMessage(string message)
        {
            Writer.Write(message);
        }

        protected override void SetContentWriter()
        {
            Writer = new ConsoleWriter();
        }
    }
}