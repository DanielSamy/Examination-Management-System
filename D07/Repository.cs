namespace D07
{
    public class Repository<T> where T : ICloneable, IComparable<T>
    {
        private T[] _items;
        private int _count;

        public Repository(int capacity = 10)
        {
            if (capacity <= 0) throw new ArgumentException("Capacity must be > 0");
            _items = new T[capacity];
            _count = 0;
        }

        public int Count => _count;

        public void Add(T item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            if (_count == _items.Length)
                Array.Resize(ref _items, _items.Length * 2);
            _items[_count++] = (T)item.Clone();
        }

        public bool Remove(T item)
        {
            if (item == null) return false;
            int index = Array.IndexOf(_items, item, 0, _count);
            if (index < 0) return false;

            for (int i = index; i < _count - 1; i++)
                _items[i] = _items[i + 1];

            _items[_count - 1] = default!;
            _count--;
            return true;
        }

        public void Sort()
        {
            Array.Sort(_items, 0, _count);
        }

        public T[] GetAll()
        {
            T[] result = new T[_count];
            Array.Copy(_items, result, _count);
            return result;
        }
    }
}
