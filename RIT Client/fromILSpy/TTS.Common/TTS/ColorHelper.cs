using Janus.Windows.GridEX;
using System;
using System.Drawing;
namespace TTS
{
	public static class ColorHelper
	{
		public static System.Drawing.Color TTSGreen = System.Drawing.Color.FromArgb(146, 231, 146);
		public static System.Drawing.Color TTSRed = System.Drawing.Color.FromArgb(246, 156, 156);
		public static System.Drawing.Color TTSBlue = System.Drawing.Color.FromArgb(146, 177, 220);
		public static System.Drawing.Color TTSYellow = System.Drawing.Color.FromArgb(246, 213, 156);
		public static System.Drawing.Color TTSLightGreen = System.Drawing.Color.FromArgb(244, 252, 244);
		public static System.Drawing.Color TTSLightRed = System.Drawing.Color.FromArgb(255, 247, 247);
		public static System.Drawing.Color TTSLightBlue = System.Drawing.Color.FromArgb(243, 246, 250);
		public static System.Drawing.Color TTSLightYellow = System.Drawing.Color.FromArgb(255, 252, 247);
		public static System.Drawing.Color TTSLightGray = System.Drawing.Color.FromArgb(204, 204, 204);
		public static GridEXFormatStyle RowStyleGreen = new GridEXFormatStyle
		{
			BackColor = ColorHelper.TTSGreen
		};
		public static GridEXFormatStyle RowStyleRed = new GridEXFormatStyle
		{
			BackColor = ColorHelper.TTSRed
		};
		public static GridEXFormatStyle RowStyleBlue = new GridEXFormatStyle
		{
			BackColor = ColorHelper.TTSBlue
		};
		public static GridEXFormatStyle RowStyleYellow = new GridEXFormatStyle
		{
			BackColor = ColorHelper.TTSYellow
		};
		public static GridEXFormatStyle RowStyleGray = new GridEXFormatStyle
		{
			BackColor = ColorHelper.TTSLightGray
		};
		public static GridEXFormatStyle CellStyleGreen = new GridEXFormatStyle
		{
			ForeColor = System.Drawing.Color.DarkGreen
		};
		public static GridEXFormatStyle CellStyleRed = new GridEXFormatStyle
		{
			ForeColor = System.Drawing.Color.DarkRed
		};
		public static GridEXFormatStyle CellStyleBlack = new GridEXFormatStyle
		{
			ForeColor = System.Drawing.Color.Black
		};
	}
}
