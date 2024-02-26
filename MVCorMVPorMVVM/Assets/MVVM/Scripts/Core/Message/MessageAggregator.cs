using System.Collections.Generic;
using UnityEngine;

namespace MVVM.Message
{
    public delegate void MessageHnadler<T>(object sender, MessageArgs<T> args);
    public class MessageAggregator<T>
    {
        private readonly Dictionary<string, MessageHnadler<T>> _messages = new Dictionary<string, MessageHnadler<T>>();

        public static readonly MessageAggregator<T> Instance = new MessageAggregator<T>();

        private MessageAggregator()
        {
            
        }

        public void Sublisher(string name, MessageHnadler<T> handler)
        {
            if (!_messages.ContainsKey(name))
            {
                _messages.Add(name, handler);
            }
            else
            {
                _messages[name] += handler;
            }
        }

        public void Publisher(string name, object sender, MessageArgs<T> args)
        {
            if (_messages.ContainsKey(name) && _messages[name] != null)
            {
                _messages[name](sender, args);
            }
        }
    }
}