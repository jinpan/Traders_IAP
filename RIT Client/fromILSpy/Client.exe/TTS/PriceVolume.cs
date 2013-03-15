using System;
namespace TTS
{
	public class PriceVolume
	{
		public decimal Price;
		public decimal Volume;
		public PriceVolume(decimal price, decimal volume)
		{
			this.Price = price;
			this.Volume = volume;
		}
	}
}
