using System;
using System.Threading;
namespace TTS
{
	internal class UniqueDateTime : System.IComparable<UniqueDateTime>
	{
		private static int UniqueID;
		public System.DateTime Time
		{
			get;
			set;
		}
		private int ID
		{
			get;
			set;
		}
		public UniqueDateTime(System.DateTime time)
		{
			this.Time = time;
			this.ID = System.Threading.Interlocked.Increment(ref UniqueDateTime.UniqueID);
		}
		public int CompareTo(UniqueDateTime other)
		{
			if (!(this.Time == other.Time))
			{
				return this.Time.CompareTo(other.Time);
			}
			return this.ID.CompareTo(other.ID);
		}
	}
}
