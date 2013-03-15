using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
namespace TTS
{
	public static class ConnectedTradersManager
	{
		public static BindingList<string> ConnectedTraders = new BindingList<string>();
		public static System.Collections.Generic.Dictionary<string, Tuple<System.Drawing.Color, int>> TraderColor = new System.Collections.Generic.Dictionary<string, Tuple<System.Drawing.Color, int>>();
		public static System.Drawing.Color[] TextColors = new System.Drawing.Color[]
		{
			System.Drawing.Color.Black,
			System.Drawing.Color.Blue,
			System.Drawing.Color.Green,
			System.Drawing.Color.BlueViolet,
			System.Drawing.Color.Brown,
			System.Drawing.Color.Violet,
			System.Drawing.Color.DarkBlue,
			System.Drawing.Color.DarkCyan,
			System.Drawing.Color.DarkGoldenrod,
			System.Drawing.Color.DarkGray,
			System.Drawing.Color.DarkGreen,
			System.Drawing.Color.DarkKhaki,
			System.Drawing.Color.DarkMagenta,
			System.Drawing.Color.DarkOliveGreen,
			System.Drawing.Color.DarkOrange,
			System.Drawing.Color.DarkOrchid,
			System.Drawing.Color.DarkRed,
			System.Drawing.Color.DarkSalmon,
			System.Drawing.Color.DarkSeaGreen,
			System.Drawing.Color.DarkSlateBlue,
			System.Drawing.Color.DarkSlateGray,
			System.Drawing.Color.DarkTurquoise,
			System.Drawing.Color.DarkViolet,
			System.Drawing.Color.IndianRed,
			System.Drawing.Color.Gray,
			System.Drawing.Color.CadetBlue,
			System.Drawing.Color.Chocolate
		};
		public static void Initialize()
		{
			ConnectedTradersManager.ConnectedTraders.Clear();
			ConnectedTradersManager.TraderColor.Clear();
			ConnectedTradersManager.TraderColor.Add(Game.State.Trader.TraderID, Tuple.Create<System.Drawing.Color, int>(ConnectedTradersManager.TextColors[0], 0));
		}
		public static void AddClient(string traderid)
		{
			if (!ConnectedTradersManager.TraderColor.ContainsKey(traderid))
			{
				ConnectedTradersManager.TraderColor.Add(traderid, Tuple.Create<System.Drawing.Color, int>(ConnectedTradersManager.TextColors[ConnectedTradersManager.TraderColor.Count % ConnectedTradersManager.TextColors.Length], ConnectedTradersManager.TraderColor.Count % ConnectedTradersManager.TextColors.Length));
			}
			if (!ConnectedTradersManager.ConnectedTraders.Contains(traderid))
			{
				ConnectedTradersManager.ConnectedTraders.Add(traderid);
			}
		}
		public static void RemoveClient(string traderid)
		{
			if (ConnectedTradersManager.ConnectedTraders.Contains(traderid))
			{
				ConnectedTradersManager.ConnectedTraders.Remove(traderid);
			}
		}
	}
}
