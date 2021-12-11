using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CustomDict
{
    public class CustomDictionary<TKey, TValue> : IDictionary<TKey, TValue>
    {
        private LinkedList<KeyValuePair<TKey, TValue>>[] hashTable;
        public ICollection<TKey> Keys { get; }
        public ICollection<TValue> Values { get; }

        private int count = 0;
        public int Count => count;
        public bool IsReadOnly { get; }
        public CustomDictionary()
        {
            Keys = new List<TKey>();
            Values = new List<TValue>();
            IsReadOnly = false;
            hashTable = new LinkedList<KeyValuePair<TKey, TValue>>[16];
            for (int i = 0; i < hashTable.Length; i++)
                hashTable[i] = new LinkedList<KeyValuePair<TKey, TValue>>();
        }
        public TValue this[TKey key]
        {
            get
            {
                int hash = Math.Abs(key.GetHashCode()) % hashTable.Length;

                if (Keys.Contains(key))
                {

                    foreach (var item in hashTable[hash])
                    {
                        if (key.Equals(item.Key))
                        {
                            return item.Value;
                        }
                    }
                    return default;
                }
                else
                {
                    throw new KeyNotFoundException($"Key {key} not found");
                }
            }
            set
            {
                int hash = Math.Abs(key.GetHashCode()) % hashTable.Length;
                if (Keys.Contains(key))
                {
                    KeyValuePair<TKey, TValue> newPair = new KeyValuePair<TKey, TValue>(key, value);
                    foreach (var item in hashTable[hash])
                    {
                        if (key.Equals(item.Key))
                        {
                            hashTable[hash].Remove(item);
                            hashTable[hash].AddLast(newPair);
                            break;
                        }
                    }
                }
                else Add(key, value);

            }
        }


        public void Add(TKey key, TValue value)
        {
            int hash = Math.Abs(key.GetHashCode()) % hashTable.Length;
            if (Keys.Contains(key))
                throw new ArgumentException($"An element with key = {key} already exists.");
            else
            {
                KeyValuePair<TKey, TValue> pair = new KeyValuePair<TKey, TValue>(key, value);
                hashTable[hash].AddLast(pair);
                Keys.Add(key);
                Values.Add(value);
                count++;
                if (count >= hashTable.Length)
                {
                    Expand();
                }
            }
        }
        public void Add(KeyValuePair<TKey, TValue> keyValuePair)
        {
            Add(keyValuePair.Key, keyValuePair.Value);
        }
        private void Expand()
        {
            LinkedList<KeyValuePair<TKey, TValue>>[] oldHashTable = hashTable;
            hashTable = new LinkedList<KeyValuePair<TKey, TValue>>[oldHashTable.Length * 2];

            foreach (var linkedList in oldHashTable)
            {
                if (linkedList.Count != 0)
                    foreach (var item in linkedList)
                        Add(item);
            }
        }
        public bool ContainsKey(TKey key)
        {
            return Keys.Contains(key);
        }
        public bool Contains(KeyValuePair<TKey, TValue> keyValuePair)
        {
            int hash = Math.Abs(keyValuePair.Key.GetHashCode()) % hashTable.Length;
            return hashTable[hash].Contains(keyValuePair);
        }
        public bool Remove(TKey key)
        {
            int hash = Math.Abs(key.GetHashCode()) % hashTable.Length;
            if (ContainsKey(key))
            {
                foreach (var item in hashTable[hash])
                {
                    if (key.Equals(item.Key))
                    {
                        count--;
                        hashTable[hash].Remove(item);
                        Keys.Remove(key);
                        Values.Remove(item.Value);
                        return true;
                    }
                }
            }
            return false;
        }
        public bool Remove(KeyValuePair<TKey, TValue> keyValuePair)
        {
            return Remove(keyValuePair.Key);
        }
        public bool TryGetValue(TKey key, out TValue value)
        {
            int hash = Math.Abs(key.GetHashCode()) % hashTable.Length;
            if (ContainsKey(key))
            {
                foreach (var item in hashTable[hash])
                {
                    value = item.Value;
                    return true;
                }
            }
            value = default;
            return false;
        }
        public void Clear()
        {
            for (int i = 0; i < hashTable.Length; i++)
                hashTable[i].Clear();
            Keys.Clear();
            Values.Clear();
            count = 0;
        }
        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            if (arrayIndex < 0)
                throw new ArgumentOutOfRangeException("arrayIndex is less than 0.");
            if (array == null)
                throw new ArgumentNullException("array is null.");
            if (count > array.Length - 1 - arrayIndex)
                throw new ArgumentException("The number of elements in the source Collection " +
                    "is greater than the available space from arrayIndex to the end of the destination array.");
            for (int i = 0; i < hashTable.Length; ++i)
            {
                foreach (var item in hashTable[i])
                {
                    if (arrayIndex >= array.Length) break;
                    array[arrayIndex] = item;
                    arrayIndex++;
                }
            }
        }
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            for (int i = 0; i < hashTable.Length; ++i)
            {
                foreach (var item in hashTable[i])
                {
                    yield return item;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}

