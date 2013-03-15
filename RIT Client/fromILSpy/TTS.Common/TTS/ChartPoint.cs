using System;
namespace TTS
{
	public abstract class ChartPoint
	{
		public enum CloneType
		{
			COPY,
			FORWARD,
			BACKWARD
		}
		public abstract void Merge(ChartPoint point);
		public abstract ChartPoint Clone(ChartPoint.CloneType type = ChartPoint.CloneType.COPY);
	}
}
