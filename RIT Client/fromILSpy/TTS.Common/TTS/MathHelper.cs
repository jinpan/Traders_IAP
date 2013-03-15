using Microsoft.JScript;
using Microsoft.JScript.Vsa;
using System;
using System.Linq;
namespace TTS
{
	public static class MathHelper
	{
		private static readonly VsaEngine Engine = VsaEngine.CreateEngine();
		public static decimal Evaluate(string expr)
		{
			return decimal.Parse(Eval.JScriptEvaluate(expr, MathHelper.Engine).ToString());
		}
		public static decimal CalculateVWAP(decimal vwap, decimal vwapvolume, decimal volumechange, decimal price)
		{
			if (vwapvolume + volumechange == 0m)
			{
				return 0m;
			}
			if (System.Math.Sign(vwapvolume + volumechange) != System.Math.Sign(vwapvolume))
			{
				return price;
			}
			if (System.Math.Sign(vwapvolume) != System.Math.Sign(volumechange))
			{
				return vwap;
			}
			return (vwap * vwapvolume + volumechange * price) / (vwapvolume + volumechange);
		}
		public static void IncrementPeriodTick(ref int period, ref int tick, int increment, int ticksperperiod)
		{
			int num = period * ticksperperiod + tick + increment;
			period = num / ticksperperiod;
			tick = num % ticksperperiod;
			if (tick == 0)
			{
				tick = ticksperperiod;
				period--;
			}
		}
		public static int Round(decimal n, int sigfigs)
		{
			double num = System.Convert.ToDouble(n);
			double num2 = System.Math.Pow(10.0, System.Math.Ceiling(System.Math.Log10(num)) - (double)sigfigs);
			return System.Convert.ToInt32(System.Math.Round(num / num2) * num2);
		}
		public static int GreatestCommonFactor(params decimal[] a)
		{
			return MathHelper.GreatestCommonFactor(a.Select(new Func<decimal, int>(System.Convert.ToInt32)).ToArray<int>());
		}
		public static int GreatestCommonFactor(params int[] a)
		{
			if (a.Length == 1)
			{
				return a[0];
			}
			int num = a[0];
			for (int i = 1; i < a.Length; i++)
			{
				num = MathHelper.GreatestCommonFactor(num, a[i]);
			}
			return num;
		}
		public static int GreatestCommonFactor(int a, int b)
		{
			while (b != 0)
			{
				int num = a % b;
				a = b;
				b = num;
			}
			return a;
		}
		public static string MillisecondsToFractionString(int ms)
		{
			if (ms <= 1000)
			{
				return string.Format("{0:0.##}x", 1000m / ms);
			}
			int num = MathHelper.GreatestCommonFactor(ms, 1000);
			return string.Format("{0}/{1}x", 1000 / num, ms / num);
		}
		public static string MillisecondsToPercentString(int ms)
		{
			return string.Format("{0:0%}", 1000.0 / (double)ms);
		}
	}
}
