using System;
using System.Text;

namespace MVVM.Log
{
    public abstract class LogStrategy
    {
        public readonly StringBuilder messageBuilder = new StringBuilder();
        
        protected IContenWriter Writer { get; set; }
        
        /// <summary>
        /// 记录日志 模板方法
        /// </summary>
        /// <param name="message"></param>
        protected abstract void RecordMessage(string message);
        /// <summary>
        /// 设置写入对象
        /// </summary>
        protected abstract void SetContentWriter();

        /// <summary>
        /// 公共入口
        /// </summary>
        /// <param name="message"></param>
        /// <param name="detail"></param>
        public void Log(string message, bool detail = false)
        {
            if (detail)
            {
                RecordDateTime();
            }
            RecordMessage(messageBuilder.AppendLine(string.Format("Messgae {0}", message)).ToString());
        }

        private void RecordDateTime()
        {
            messageBuilder.AppendLine(string.Format("Messgae {0}", DateTime.Now.ToString("u")));
        }
    }
}