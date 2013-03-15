using System;
using System.ComponentModel;
namespace TTS
{
	internal class AccountItem : INotifyPropertyChanged
	{
		private decimal _Balance;
		public event PropertyChangedEventHandler PropertyChanged;
		public decimal Balance
		{
			get
			{
				return this._Balance;
			}
			set
			{
				if (value != this._Balance)
				{
					this._Balance = value;
					this.RaisePropertyChangedEvent("Balance");
				}
			}
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
