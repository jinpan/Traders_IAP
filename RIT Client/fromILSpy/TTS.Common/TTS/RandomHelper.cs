using System;
namespace TTS
{
	public static class RandomHelper
	{
		private static System.Random R = new System.Random();
		private static bool StoredUniformDeviateIsGood = false;
		private static double StoredUniformDeviate = 0.0;
		public static int Next()
		{
			return RandomHelper.R.Next();
		}
		public static int Next(int min, int max)
		{
			return RandomHelper.R.Next(min, max);
		}
		public static decimal NextDecimal()
		{
			return System.Convert.ToDecimal(RandomHelper.R.NextDouble());
		}
		public static double NextDouble()
		{
			return RandomHelper.R.NextDouble();
		}
		public static decimal NextNormal()
		{
			if (RandomHelper.StoredUniformDeviateIsGood)
			{
				RandomHelper.StoredUniformDeviateIsGood = false;
				return System.Convert.ToDecimal(RandomHelper.StoredUniformDeviate);
			}
			double num = 0.0;
			double num2 = 0.0;
			double num3 = 0.0;
			while (num >= 1.0 || num == 0.0)
			{
				num2 = 2.0 * RandomHelper.R.NextDouble() - 1.0;
				num3 = 2.0 * RandomHelper.R.NextDouble() - 1.0;
				num = num2 * num2 + num3 * num3;
			}
			double num4 = System.Math.Sqrt(-2.0 * System.Math.Log(num, 2.7182818284590451) / num);
			RandomHelper.StoredUniformDeviate = num2 * num4;
			RandomHelper.StoredUniformDeviateIsGood = true;
			return System.Convert.ToDecimal(num3 * num4);
		}
		public static decimal NextNormal(decimal mean, decimal stddev)
		{
			return RandomHelper.NextNormal() * stddev + mean;
		}
		public static double NextNormInv(double p, double mu, double sigma)
		{
			if (p < 0.0 || p > 1.0)
			{
				throw new System.ArgumentOutOfRangeException("The probality p must be bigger than 0 and smaller than 1");
			}
			if (sigma < 0.0)
			{
				throw new System.ArgumentOutOfRangeException("The standard deviation sigma must be positive");
			}
			if (p == 0.0)
			{
				return double.NegativeInfinity;
			}
			if (p == 1.0)
			{
				return double.PositiveInfinity;
			}
			if (sigma == 0.0)
			{
				return mu;
			}
			double num = p - 0.5;
			double num3;
			if (System.Math.Abs(num) <= 0.425)
			{
				double num2 = 0.180625 - num * num;
				num3 = num * (((((((num2 * 2509.0809287301227 + 33430.575583588128) * num2 + 67265.7709270087) * num2 + 45921.95393154987) * num2 + 13731.693765509461) * num2 + 1971.5909503065513) * num2 + 133.14166789178438) * num2 + 3.3871328727963665) / (((((((num2 * 5226.4952788528544 + 28729.085735721943) * num2 + 39307.895800092709) * num2 + 21213.794301586597) * num2 + 5394.1960214247511) * num2 + 687.18700749205789) * num2 + 42.313330701600911) * num2 + 1.0);
			}
			else
			{
				double num2;
				if (num > 0.0)
				{
					num2 = 1.0 - p;
				}
				else
				{
					num2 = p;
				}
				num2 = System.Math.Sqrt(-System.Math.Log(num2));
				if (num2 <= 5.0)
				{
					num2 += -1.6;
					num3 = (((((((num2 * 0.00077454501427834139 + 0.022723844989269184) * num2 + 0.24178072517745061) * num2 + 1.2704582524523684) * num2 + 3.6478483247632045) * num2 + 5.769497221460691) * num2 + 4.6303378461565456) * num2 + 1.4234371107496835) / (((((((num2 * 1.0507500716444169E-09 + 0.00054759380849953455) * num2 + 0.015198666563616457) * num2 + 0.14810397642748008) * num2 + 0.6897673349851) * num2 + 1.6763848301838038) * num2 + 2.053191626637759) * num2 + 1.0);
				}
				else
				{
					num2 += -5.0;
					num3 = (((((((num2 * 2.0103343992922881E-07 + 2.7115555687434876E-05) * num2 + 0.0012426609473880784) * num2 + 0.026532189526576124) * num2 + 0.29656057182850487) * num2 + 1.7848265399172913) * num2 + 5.4637849111641144) * num2 + 6.6579046435011033) / (((((((num2 * 2.0442631033899397E-15 + 1.4215117583164459E-07) * num2 + 1.8463183175100548E-05) * num2 + 0.00078686913114561329) * num2 + 0.014875361290850615) * num2 + 0.13692988092273581) * num2 + 0.599832206555888) * num2 + 1.0);
				}
				if (num < 0.0)
				{
					num3 = -num3;
				}
			}
			return mu + sigma * num3;
		}
	}
}
