﻿using System;
using System.Collections.Generic;

namespace PivotStack.Repositories
{
    /// <seealso href="http://stackoverflow.com/questions/35002/does-c-have-a-way-of-giving-me-an-immutable-dictionary">
    /// Does C# have a way of giving me an immutable Dictionary?
    /// </seealso>
    public class ReadOnlyDictionary<TKey, TValue> : IDictionary<TKey, TValue>
    {
        private readonly IDictionary<TKey, TValue> _dictionary;

        public ReadOnlyDictionary (IDictionary<TKey, TValue> dictionary)
        {
            _dictionary = dictionary;
        }

        public void Add (TKey key, TValue value)
        {
            throw new InvalidOperationException ();
        }

        public bool ContainsKey (TKey key)
        {
            return _dictionary.ContainsKey (key);
        }

        public ICollection<TKey> Keys
        {
            get { return _dictionary.Keys; }
        }

        public bool Remove (TKey key)
        {
            throw new InvalidOperationException ();
        }

        public bool TryGetValue (TKey key, out TValue value)
        {
            return _dictionary.TryGetValue (key, out value);
        }

        public ICollection<TValue> Values
        {
            get { return _dictionary.Values; }
        }

        public TValue this[TKey key]
        {
            get { return _dictionary[key]; }
            set { throw new InvalidOperationException (); }
        }

        public void Add (KeyValuePair<TKey, TValue> item)
        {
            throw new InvalidOperationException ();
        }

        public void Clear ()
        {
            throw new InvalidOperationException ();
        }

        public bool Contains (KeyValuePair<TKey, TValue> item)
        {
            return _dictionary.Contains (item);
        }

        public void CopyTo (KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            _dictionary.CopyTo (array, arrayIndex);
        }

        public int Count
        {
            get { return _dictionary.Count; }
        }

        public bool IsReadOnly
        {
            get { return true; }
        }

        public bool Remove (KeyValuePair<TKey, TValue> item)
        {
            throw new InvalidOperationException ();
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator ()
        {
            return _dictionary.GetEnumerator ();
        }

        System.Collections.IEnumerator
               System.Collections.IEnumerable.GetEnumerator ()
        {
            return ( (System.Collections.IEnumerable) _dictionary ).GetEnumerator ();
        }
    }
}
