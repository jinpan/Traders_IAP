using ProtoBuf;
using System;
using System.ComponentModel;
namespace TTS
{
	[ProtoContract, TypeConverter(typeof(ExpandableObjectConverter))]
	public class GameParameters : INotifyPropertyChanged
	{
		private int _Period;
		private int _Tick;
		private int _MillisecondsPerTick;
		private bool _IsActive;
		private bool _IsPaused;
		private GameStatus _Status;
		public event PropertyChangedEventHandler PropertyChanged;
		[ProtoMember(1), DefaultValue(1)]
		public int Period
		{
			get
			{
				return this._Period;
			}
			set
			{
				if (value != this._Period)
				{
					this._Period = value;
					this.RaisePropertyChangedEvent("Period");
				}
			}
		}
		[ProtoMember(2), DefaultValue(0)]
		public int Tick
		{
			get
			{
				return this._Tick;
			}
			set
			{
				if (value != this._Tick)
				{
					this._Tick = value;
					this.RaisePropertyChangedEvent("Tick");
				}
			}
		}
		[ProtoMember(3), DefaultValue(1)]
		public int MillisecondsPerTick
		{
			get
			{
				return this._MillisecondsPerTick;
			}
			set
			{
				if (value != this._MillisecondsPerTick)
				{
					this._MillisecondsPerTick = value;
					this.RaisePropertyChangedEvent("MillisecondsPerTick");
				}
			}
		}
		[ProtoMember(4), DefaultValue(false)]
		public bool IsActive
		{
			get
			{
				return this._IsActive;
			}
			set
			{
				if (value != this._IsActive)
				{
					this._IsActive = value;
					this.Status = (this.IsActive ? (this.IsPaused ? GameStatus.PAUSED : GameStatus.ACTIVE) : GameStatus.STOPPED);
					this.RaisePropertyChangedEvent("IsActive");
				}
			}
		}
		[ProtoMember(5), DefaultValue(false)]
		public bool IsPaused
		{
			get
			{
				return this._IsPaused;
			}
			set
			{
				if (value != this._IsPaused)
				{
					this._IsPaused = value;
					this.Status = (this.IsActive ? (this.IsPaused ? GameStatus.PAUSED : GameStatus.ACTIVE) : GameStatus.STOPPED);
					this.RaisePropertyChangedEvent("IsPaused");
				}
			}
		}
		public GameStatus Status
		{
			get
			{
				return this._Status;
			}
			private set
			{
				if (value != this._Status)
				{
					this._Status = value;
					this.RaisePropertyChangedEvent("Status");
				}
			}
		}
		public GameParameters()
		{
			this.SetDefaultValues();
		}
		private void RaisePropertyChangedEvent(string info)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}
	}
}
