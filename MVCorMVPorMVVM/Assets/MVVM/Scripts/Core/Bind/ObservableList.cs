using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MVVM
{
    public class ObservableList<T> : IList<T>
    {
        private List<T> _value = new List<T>();

        public delegate void ValueChangedHandler(List<T> oldValye, List<T> newValue);

        public ValueChangedHandler OnValueChanged;
        

        public List<T> Value
        {
            get { return _value; }
            set
            {
                if (!_value.SequenceEqual(value))
                {
                    var old = _value;
                    _value = value;
                    ValueChanged(old, _value);
                }
            }
        }

        private void ValueChanged(List<T> oldValue, List<T> newValue)
        {
            OnValueChanged?.Invoke(oldValue, newValue);
        }
        
        public delegate void AddHnadler(T item);
        public event AddHnadler OnAdd;
        public delegate void InsertHnadler(T item);
        public event InsertHnadler OnInsert;
        public delegate void RemoveHnadler(T item);
        public event AddHnadler OnRemove;
        
        public IEnumerator<T> GetEnumerator()
        {
            return _value.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(T item)
        {
            _value.Add(item);
            OnAdd?.Invoke(item);
        }

        public void Clear()
        {
            _value.Clear();
        }

        public bool Contains(T item)
        {
            return _value.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            _value.CopyTo(array, arrayIndex);
        }

        public bool Remove(T item)
        {
            if (_value.Remove(item))
            {
                OnRemove?.Invoke(item);
                return true;
            }

            return false;
        }

        public int Count { get; }
        public bool IsReadOnly { get; }
        public int IndexOf(T item)
        {
            return _value.IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            _value.Insert(index, item);
            OnInsert?.Invoke(item);
        }

        public void RemoveAt(int index)
        {
            _value.RemoveAt(index);
        }

        public T this[int index]
        {
            get => _value[index];
            set => _value[index] = value;
        }
    }
}