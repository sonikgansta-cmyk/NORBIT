using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3_Collections
{
    public class SmartStack<T> : IEnumerable<T>
    {
        private T[] _items;
        private int _count;
        private const int DefaultCapacity = 4;

        // 1. Конструктор без параметров (создаётся массив ёмкостью 4 элемента)
        public SmartStack()
        {
            _items = new T[DefaultCapacity];
            _count = 0;
        }

        // 2. Конструктор с одним целочисленным параметром (создаётся массив указанной ёмкости)
        public SmartStack(int capacity)
        {
            if (capacity <= 0)
                throw new ArgumentException("Ёмкость должна быть больше нуля", nameof(capacity));

            _items = new T[capacity];
            _count = 0;
        }

        // 3. Конструктор, который в качестве параметра принимает коллекцию, реализующую интерфейс IEnumerable<T>,
        // создаёт массив нужного размера и копирует в него все элементы из коллекции
        // (в порядке стека - последний элемент коллекции будет вершиной стека)
        public SmartStack(IEnumerable<T> collection)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));

            int itemCount = 0;
            var enumerator = collection.GetEnumerator();
            while (enumerator.MoveNext())
            {
                itemCount++;
            }

            _count = itemCount;

            int capacity = Math.Max(DefaultCapacity, _count);
            _items = new T[capacity];

            if (_count > 0)
            {
                enumerator = collection.GetEnumerator();
                int index = _count - 1;
                while (enumerator.MoveNext())
                {
                    _items[index--] = enumerator.Current;
                }
            }
        }

        // 4. Метод Push, добавляющий элемент на вершину стека.
        // При нехватке места для добавления элемента, ёмкость массива должна удваиваться.
        public void Push(T item)
        {
            if (_count == _items.Length)
            {
                Resize(_items.Length * 2);
            }

            _items[_count++] = item;
        }

        // 5. Метод PushRange, добавляющий на вершину стека содержимое коллекции,
        // реализующей интерфейс IEnumerable<T>. Элементы должны добавляться в порядке,
        // обратном их следованию в коллекции (последний элемент коллекции станет вершиной стека).
        // Метод должен корректно учитывать число элементов
        public void PushRange(IEnumerable<T> collection)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));

            int itemCount = 0;
            var enumerator = collection.GetEnumerator();
            while (enumerator.MoveNext())
            {
                itemCount++;
            }

            if (itemCount == 0)
                return;

            int neededCapacity = _count + itemCount;
            if (neededCapacity > _items.Length)
            {
                int newCapacity = _items.Length;
                while (newCapacity < neededCapacity)
                {
                    newCapacity *= 2;
                }
                Resize(newCapacity);
            }

            enumerator = collection.GetEnumerator();

            T[] tempItems = new T[itemCount];
            int tempIndex = 0;
            while (enumerator.MoveNext())
            {
                tempItems[tempIndex++] = enumerator.Current;
            }

            for (int i = itemCount - 1; i >= 0; i--)
            {
                _items[_count++] = tempItems[i];
            }
        }

        // 6. Метод Pop, удаляющий и возвращающий элемент с вершины стека.
        // Метод должен генерировать исключение InvalidOperationException, если стек пуст.
        // Реальная ёмкость массива не должна уменьшаться при удалении.
        public T Pop()
        {
            if (_count == 0)
                throw new InvalidOperationException("Стек пуст");

            T item = _items[--_count];
            _items[_count] = default(T); 
            return item;
        }

        // 7. Метод Peek, возвращающий элемент с вершины стека без его удаления.
        // Должен генерировать исключение InvalidOperationException, если стек пуст.
        public T Peek()
        {
            if (_count == 0)
                throw new InvalidOperationException("Стек пуст");

            return _items[_count - 1];
        }


        // 8. Метод Contains, проверяющий наличие элемента в стеке.
        // Метод должен возвращать true, если элемент найден и false в противном случае.
        public bool Contains(T item)
        {
            EqualityComparer<T> comparer = EqualityComparer<T>.Default;

            for (int i = 0; i < _count; i++)
            {
                if (comparer.Equals(_items[i], item))
                    return true;
            }

            return false;
        }

        // 9. Свойство Count — получение количества элементов в стеке.
        public int Count => _count;

        // 10. Свойство Capacity — получение ёмкости: длины внутреннего массива.
        public int Capacity => _items.Length;

        // Увеличивает ёмкость внутреннего массива
        private void Resize(int newCapacity)
        {
            T[] newArray = new T[newCapacity];
            Array.Copy(_items, 0, newArray, 0, _count);
            _items = newArray;
        }

        // 11. Методы, реализующие интерфейсы IEnumerable и IEnumerable<T> (обход должен быть от вершины стека к основанию).
        public IEnumerator<T> GetEnumerator()
        {
            for (int i = _count - 1; i >= 0; i--)
            {
                yield return _items[i];
            }
        }

        // 11. Методы, реализующие интерфейсы IEnumerable и IEnumerable<T> (обход должен быть от вершины стека к основанию).
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        } 
    }

    internal class Program
    {
        static void Main(string[] args)
        {
        }
    }
}
