using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
namespace TTS
{
	[ProtoContract, TypeConverter(typeof(ExpandableObjectConverter))]
	public class ChartItem<T> where T : ChartPoint
	{
		public event Action<int[], T[]> ChartUpdated = delegate
		{
		};
		public event System.Action<int> PeriodsAvailableUpdated = delegate
		{
		};
		[ProtoMember(1)]
		public int InitialTicks
		{
			get;
			set;
		}
		[ProtoMember(2)]
		public int TicksPerPeriod
		{
			get;
			set;
		}
		[ProtoMember(3)]
		public int StartPeriod
		{
			get;
			set;
		}
		[ProtoMember(4)]
		public int StopPeriod
		{
			get;
			set;
		}
		[ProtoMember(5)]
		public System.Collections.Generic.List<T> Data
		{
			get;
			set;
		}
		[ProtoMember(6)]
		public int PeriodsAvailable
		{
			get;
			set;
		}
		public ChartItem()
		{
			this.Data = new System.Collections.Generic.List<T>();
		}
		public ChartItem(int startperiod, int stopperiod, int ticksperperiod, T[] initialdata = null) : this()
		{
			this.InitialTicks = ((initialdata == null) ? 0 : initialdata.Length);
			this.StartPeriod = startperiod;
			this.StopPeriod = stopperiod;
			this.TicksPerPeriod = ticksperperiod;
			this.PeriodsAvailable = 0;
			if (initialdata != null)
			{
				for (int i = 0; i < initialdata.Length; i++)
				{
					this.AddPoint(i, initialdata[i]);
				}
			}
		}
		public void GetGroups(int groupsize, out int[] x, out T[] y)
		{
			if (this.Data.Count == 0)
			{
				x = new int[0];
				y = new T[0];
				return;
			}
			int num = (this.Data.Count - 1) / groupsize + 1;
			y = new T[num];
			x = new int[num];
			for (int i = 0; i < this.Data.Count; i++)
			{
				if (y[i / groupsize] == null && this.Data[i] != null)
				{
					T[] arg_90_0 = y;
					int arg_90_1 = i / groupsize;
					T t = this.Data[i];
					arg_90_0[arg_90_1] = (T)((object)t.Clone(ChartPoint.CloneType.COPY));
				}
				else
				{
					if (y[i / groupsize] != null)
					{
						y[i / groupsize].Merge(this.Data[i]);
					}
				}
				x[i / groupsize] = (i / groupsize + 1 - this.InitialTicks / groupsize) * groupsize;
			}
		}
		public void GetLastGroup(int groupsize, out int x, out T y)
		{
			if (this.Data.Count == 0)
			{
				x = 0;
				y = default(T);
				return;
			}
			int num = (this.Data.Count - 1) / groupsize + 1;
			x = (num - this.InitialTicks / groupsize) * groupsize;
			y = default(T);
			for (int i = (num - 1) * groupsize; i < this.Data.Count; i++)
			{
				if (y == null && this.Data[i] != null)
				{
					T t = this.Data[i];
					y = (T)((object)t.Clone(ChartPoint.CloneType.COPY));
				}
				else
				{
					if (y != null)
					{
						y.Merge(this.Data[i]);
					}
				}
			}
		}
		public T[] GetData(int initialticks, int startperiod, int stopperiod)
		{
			System.Collections.Generic.List<T> list = new System.Collections.Generic.List<T>(initialticks + (stopperiod - startperiod + 1) * this.TicksPerPeriod);
			int num = System.Math.Min(this.Data.Count - 1, this.TrueIndex(stopperiod, this.TicksPerPeriod)) - this.TrueIndex(startperiod, 1) + 1;
			if (initialticks > 0)
			{
				list.AddRange(this.Data.GetRange(this.InitialTicks - initialticks, initialticks));
			}
			if (num > 0)
			{
				list.AddRange(this.Data.GetRange(this.TrueIndex(startperiod, 1), num));
			}
			return list.ToArray();
		}
		public void AddPoint(int i, T point)
		{
			int count = this.Data.Count;
			if (this.Data.Count == 0)
			{
				this.Data.Add((T)((object)point.Clone(ChartPoint.CloneType.BACKWARD)));
			}
			while (this.Data.Count <= i)
			{
				System.Collections.Generic.List<T> arg_6C_0 = this.Data;
				T t = this.Data[this.Data.Count - 1];
				arg_6C_0.Add((T)((object)t.Clone(ChartPoint.CloneType.FORWARD)));
			}
			T t2 = this.Data[i];
			t2.Merge(point);
			T[] array = this.Data.GetRange(System.Math.Min(count, i), i - System.Math.Min(count, i) + 1).ToArray();
			int[] array2 = new int[array.Length];
			int num = this.TrueIndexToGameTick(System.Math.Min(count, i));
			for (int j = 0; j < array.Length; j++)
			{
				array2[j] = num + j;
			}
			this.ChartUpdated(array2, array);
		}
		public void AddPoint(int period, int tick, T point)
		{
			if (period < this.StartPeriod || period > this.StopPeriod)
			{
				return;
			}
			if (tick < 1)
			{
				tick = 1;
			}
			if (tick > this.TicksPerPeriod)
			{
				tick = this.TicksPerPeriod;
			}
			if (period > this.PeriodsAvailable)
			{
				this.PeriodsAvailable = period;
				this.PeriodsAvailableUpdated(this.PeriodsAvailable);
			}
			this.AddPoint(this.TrueIndex(period, tick), point);
		}
		public void Reset()
		{
			if (this.Data.Count > this.InitialTicks)
			{
				this.Data.RemoveRange(this.InitialTicks, this.Data.Count - this.InitialTicks + 1);
			}
		}
		private int TrueIndex(int period, int tick)
		{
			return (period - this.StartPeriod) * this.TicksPerPeriod + this.InitialTicks + tick - 1;
		}
		private int TrueIndexToGameTick(int index)
		{
			return index - this.InitialTicks + (this.StartPeriod - 1) * this.TicksPerPeriod + 1;
		}
		public byte[] GetProtoBytes()
		{
			byte[] result;
			using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream())
			{
				Serializer.Serialize<ChartItem<T>>(memoryStream, this);
				result = memoryStream.ToArray();
			}
			return result;
		}
	}
}
