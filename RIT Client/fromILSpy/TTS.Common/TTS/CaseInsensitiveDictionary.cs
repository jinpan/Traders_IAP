using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
namespace TTS
{
	public class CaseInsensitiveDictionary<TKey, TValue> : System.Collections.Generic.Dictionary<TKey, TValue>
	{
		public CaseInsensitiveDictionary() : base((typeof(TKey) == typeof(string)) ? (System.StringComparer.OrdinalIgnoreCase as System.Collections.Generic.IEqualityComparer<TKey>) : null)
		{
		}
		public CaseInsensitiveDictionary(int capacity) : base(capacity, (typeof(TKey) == typeof(string)) ? (System.StringComparer.OrdinalIgnoreCase as System.Collections.Generic.IEqualityComparer<TKey>) : null)
		{
		}
		public CaseInsensitiveDictionary(System.Collections.Generic.IEqualityComparer<TKey> comparer) : base(comparer)
		{
		}
		public CaseInsensitiveDictionary(int capacity, System.Collections.Generic.IEqualityComparer<TKey> comparer) : base(capacity, comparer)
		{
		}
		public CaseInsensitiveDictionary(System.Collections.Generic.IDictionary<TKey, TValue> dictionary) : base(dictionary, (typeof(TKey) == typeof(string)) ? (System.StringComparer.OrdinalIgnoreCase as System.Collections.Generic.IEqualityComparer<TKey>) : null)
		{
		}
		public CaseInsensitiveDictionary(System.Collections.Generic.IDictionary<TKey, TValue> dictionary, System.Collections.Generic.IEqualityComparer<TKey> comparer) : base(dictionary, comparer)
		{
		}
		protected CaseInsensitiveDictionary(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context)
		{
		}
	}
}
