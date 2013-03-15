using System;
using System.Drawing;
using System.Windows.Forms.DataVisualization.Charting;
namespace TTS
{
	public static class ColorPaletteExtensions
	{
		public static System.Drawing.Color[] GetColorPalette(this ChartColorPalette palette)
		{
			System.Drawing.Color[] result = null;
			switch (palette)
			{
			case ChartColorPalette.None:
				result = new System.Drawing.Color[]
				{
					System.Drawing.Color.FromArgb(255, 65, 140, 240),
					System.Drawing.Color.FromArgb(255, 252, 180, 65),
					System.Drawing.Color.FromArgb(255, 224, 64, 10),
					System.Drawing.Color.FromArgb(255, 5, 100, 146),
					System.Drawing.Color.FromArgb(255, 191, 191, 191),
					System.Drawing.Color.FromArgb(255, 26, 59, 105),
					System.Drawing.Color.FromArgb(255, 255, 227, 130),
					System.Drawing.Color.FromArgb(255, 18, 156, 221),
					System.Drawing.Color.FromArgb(255, 202, 107, 75),
					System.Drawing.Color.FromArgb(255, 0, 92, 219),
					System.Drawing.Color.FromArgb(255, 243, 210, 136),
					System.Drawing.Color.FromArgb(255, 80, 99, 129),
					System.Drawing.Color.FromArgb(255, 241, 185, 168),
					System.Drawing.Color.FromArgb(255, 224, 131, 10),
					System.Drawing.Color.FromArgb(255, 120, 147, 190)
				};
				break;
			case ChartColorPalette.Bright:
				result = new System.Drawing.Color[]
				{
					System.Drawing.Color.FromArgb(255, 0, 128, 0),
					System.Drawing.Color.FromArgb(255, 0, 0, 255),
					System.Drawing.Color.FromArgb(255, 128, 0, 128),
					System.Drawing.Color.FromArgb(255, 0, 255, 0),
					System.Drawing.Color.FromArgb(255, 255, 0, 255),
					System.Drawing.Color.FromArgb(255, 0, 128, 128),
					System.Drawing.Color.FromArgb(255, 255, 255, 0),
					System.Drawing.Color.FromArgb(255, 128, 128, 128),
					System.Drawing.Color.FromArgb(255, 0, 255, 255),
					System.Drawing.Color.FromArgb(255, 0, 0, 128),
					System.Drawing.Color.FromArgb(255, 128, 0, 0),
					System.Drawing.Color.FromArgb(255, 255, 0, 0),
					System.Drawing.Color.FromArgb(255, 128, 128, 0),
					System.Drawing.Color.FromArgb(255, 192, 192, 192),
					System.Drawing.Color.FromArgb(255, 255, 99, 71),
					System.Drawing.Color.FromArgb(255, 255, 228, 181)
				};
				break;
			case ChartColorPalette.Grayscale:
				result = new System.Drawing.Color[]
				{
					System.Drawing.Color.FromArgb(255, 200, 200, 200),
					System.Drawing.Color.FromArgb(255, 189, 189, 189),
					System.Drawing.Color.FromArgb(255, 178, 178, 178),
					System.Drawing.Color.FromArgb(255, 167, 167, 167),
					System.Drawing.Color.FromArgb(255, 156, 156, 156),
					System.Drawing.Color.FromArgb(255, 145, 145, 145),
					System.Drawing.Color.FromArgb(255, 134, 134, 134),
					System.Drawing.Color.FromArgb(255, 123, 123, 123),
					System.Drawing.Color.FromArgb(255, 112, 112, 112),
					System.Drawing.Color.FromArgb(255, 101, 101, 101),
					System.Drawing.Color.FromArgb(255, 90, 90, 90),
					System.Drawing.Color.FromArgb(255, 79, 79, 79),
					System.Drawing.Color.FromArgb(255, 68, 68, 68),
					System.Drawing.Color.FromArgb(255, 57, 57, 57),
					System.Drawing.Color.FromArgb(255, 46, 46, 46),
					System.Drawing.Color.FromArgb(255, 35, 35, 35)
				};
				break;
			case ChartColorPalette.Excel:
				result = new System.Drawing.Color[]
				{
					System.Drawing.Color.FromArgb(255, 153, 153, 255),
					System.Drawing.Color.FromArgb(255, 153, 51, 102),
					System.Drawing.Color.FromArgb(255, 255, 255, 204),
					System.Drawing.Color.FromArgb(255, 204, 255, 255),
					System.Drawing.Color.FromArgb(255, 102, 0, 102),
					System.Drawing.Color.FromArgb(255, 255, 128, 128),
					System.Drawing.Color.FromArgb(255, 0, 102, 204),
					System.Drawing.Color.FromArgb(255, 204, 204, 255),
					System.Drawing.Color.FromArgb(255, 0, 0, 128),
					System.Drawing.Color.FromArgb(255, 255, 0, 255),
					System.Drawing.Color.FromArgb(255, 255, 255, 0),
					System.Drawing.Color.FromArgb(255, 0, 255, 255),
					System.Drawing.Color.FromArgb(255, 128, 0, 128),
					System.Drawing.Color.FromArgb(255, 128, 0, 0),
					System.Drawing.Color.FromArgb(255, 0, 128, 128),
					System.Drawing.Color.FromArgb(255, 0, 0, 255)
				};
				break;
			case ChartColorPalette.Light:
				result = new System.Drawing.Color[]
				{
					System.Drawing.Color.FromArgb(255, 230, 230, 250),
					System.Drawing.Color.FromArgb(255, 255, 240, 245),
					System.Drawing.Color.FromArgb(255, 255, 218, 185),
					System.Drawing.Color.FromArgb(255, 255, 250, 205),
					System.Drawing.Color.FromArgb(255, 255, 228, 225),
					System.Drawing.Color.FromArgb(255, 240, 255, 240),
					System.Drawing.Color.FromArgb(255, 240, 248, 255),
					System.Drawing.Color.FromArgb(255, 245, 245, 245),
					System.Drawing.Color.FromArgb(255, 250, 235, 215),
					System.Drawing.Color.FromArgb(255, 224, 255, 255)
				};
				break;
			case ChartColorPalette.Pastel:
				result = new System.Drawing.Color[]
				{
					System.Drawing.Color.FromArgb(255, 135, 206, 235),
					System.Drawing.Color.FromArgb(255, 50, 205, 50),
					System.Drawing.Color.FromArgb(255, 186, 85, 211),
					System.Drawing.Color.FromArgb(255, 240, 128, 128),
					System.Drawing.Color.FromArgb(255, 70, 130, 180),
					System.Drawing.Color.FromArgb(255, 154, 205, 50),
					System.Drawing.Color.FromArgb(255, 64, 224, 208),
					System.Drawing.Color.FromArgb(255, 255, 105, 180),
					System.Drawing.Color.FromArgb(255, 240, 230, 140),
					System.Drawing.Color.FromArgb(255, 210, 180, 140),
					System.Drawing.Color.FromArgb(255, 143, 188, 139),
					System.Drawing.Color.FromArgb(255, 100, 149, 237),
					System.Drawing.Color.FromArgb(255, 221, 160, 221),
					System.Drawing.Color.FromArgb(255, 95, 158, 160),
					System.Drawing.Color.FromArgb(255, 255, 218, 185),
					System.Drawing.Color.FromArgb(255, 255, 160, 122)
				};
				break;
			case ChartColorPalette.EarthTones:
				result = new System.Drawing.Color[]
				{
					System.Drawing.Color.FromArgb(255, 255, 128, 0),
					System.Drawing.Color.FromArgb(255, 184, 134, 11),
					System.Drawing.Color.FromArgb(255, 192, 64, 0),
					System.Drawing.Color.FromArgb(255, 107, 142, 35),
					System.Drawing.Color.FromArgb(255, 205, 133, 63),
					System.Drawing.Color.FromArgb(255, 192, 192, 0),
					System.Drawing.Color.FromArgb(255, 34, 139, 34),
					System.Drawing.Color.FromArgb(255, 210, 105, 30),
					System.Drawing.Color.FromArgb(255, 128, 128, 0),
					System.Drawing.Color.FromArgb(255, 32, 178, 170),
					System.Drawing.Color.FromArgb(255, 244, 164, 96),
					System.Drawing.Color.FromArgb(255, 0, 192, 0),
					System.Drawing.Color.FromArgb(255, 143, 188, 139),
					System.Drawing.Color.FromArgb(255, 178, 34, 34),
					System.Drawing.Color.FromArgb(255, 139, 69, 19),
					System.Drawing.Color.FromArgb(255, 192, 0, 0)
				};
				break;
			case ChartColorPalette.SemiTransparent:
				result = new System.Drawing.Color[]
				{
					System.Drawing.Color.FromArgb(150, 255, 0, 0),
					System.Drawing.Color.FromArgb(150, 0, 255, 0),
					System.Drawing.Color.FromArgb(150, 0, 0, 255),
					System.Drawing.Color.FromArgb(150, 255, 255, 0),
					System.Drawing.Color.FromArgb(150, 0, 255, 255),
					System.Drawing.Color.FromArgb(150, 255, 0, 255),
					System.Drawing.Color.FromArgb(150, 170, 120, 20),
					System.Drawing.Color.FromArgb(80, 255, 0, 0),
					System.Drawing.Color.FromArgb(80, 0, 255, 0),
					System.Drawing.Color.FromArgb(80, 0, 0, 255),
					System.Drawing.Color.FromArgb(80, 255, 255, 0),
					System.Drawing.Color.FromArgb(80, 0, 255, 255),
					System.Drawing.Color.FromArgb(80, 255, 0, 255),
					System.Drawing.Color.FromArgb(80, 170, 120, 20),
					System.Drawing.Color.FromArgb(150, 100, 120, 50),
					System.Drawing.Color.FromArgb(150, 40, 90, 150)
				};
				break;
			case ChartColorPalette.Berry:
				result = new System.Drawing.Color[]
				{
					System.Drawing.Color.FromArgb(255, 138, 43, 226),
					System.Drawing.Color.FromArgb(255, 186, 85, 211),
					System.Drawing.Color.FromArgb(255, 65, 105, 225),
					System.Drawing.Color.FromArgb(255, 199, 21, 133),
					System.Drawing.Color.FromArgb(255, 0, 0, 255),
					System.Drawing.Color.FromArgb(255, 138, 43, 226),
					System.Drawing.Color.FromArgb(255, 218, 112, 214),
					System.Drawing.Color.FromArgb(255, 123, 104, 238),
					System.Drawing.Color.FromArgb(255, 192, 0, 192),
					System.Drawing.Color.FromArgb(255, 0, 0, 205),
					System.Drawing.Color.FromArgb(255, 128, 0, 128)
				};
				break;
			case ChartColorPalette.Chocolate:
				result = new System.Drawing.Color[]
				{
					System.Drawing.Color.FromArgb(255, 160, 82, 45),
					System.Drawing.Color.FromArgb(255, 210, 105, 30),
					System.Drawing.Color.FromArgb(255, 139, 0, 0),
					System.Drawing.Color.FromArgb(255, 205, 133, 63),
					System.Drawing.Color.FromArgb(255, 165, 42, 42),
					System.Drawing.Color.FromArgb(255, 244, 164, 96),
					System.Drawing.Color.FromArgb(255, 139, 69, 19),
					System.Drawing.Color.FromArgb(255, 192, 64, 0),
					System.Drawing.Color.FromArgb(255, 178, 34, 34),
					System.Drawing.Color.FromArgb(255, 182, 92, 58)
				};
				break;
			case ChartColorPalette.Fire:
				result = new System.Drawing.Color[]
				{
					System.Drawing.Color.FromArgb(255, 255, 215, 0),
					System.Drawing.Color.FromArgb(255, 255, 0, 0),
					System.Drawing.Color.FromArgb(255, 255, 20, 147),
					System.Drawing.Color.FromArgb(255, 220, 20, 60),
					System.Drawing.Color.FromArgb(255, 255, 140, 0),
					System.Drawing.Color.FromArgb(255, 255, 0, 255),
					System.Drawing.Color.FromArgb(255, 255, 255, 0),
					System.Drawing.Color.FromArgb(255, 255, 69, 0),
					System.Drawing.Color.FromArgb(255, 199, 21, 133),
					System.Drawing.Color.FromArgb(255, 221, 226, 33)
				};
				break;
			case ChartColorPalette.SeaGreen:
				result = new System.Drawing.Color[]
				{
					System.Drawing.Color.FromArgb(255, 46, 139, 87),
					System.Drawing.Color.FromArgb(255, 102, 205, 170),
					System.Drawing.Color.FromArgb(255, 70, 130, 180),
					System.Drawing.Color.FromArgb(255, 0, 139, 139),
					System.Drawing.Color.FromArgb(255, 95, 158, 160),
					System.Drawing.Color.FromArgb(255, 60, 179, 113),
					System.Drawing.Color.FromArgb(255, 72, 209, 204),
					System.Drawing.Color.FromArgb(255, 176, 196, 222),
					System.Drawing.Color.FromArgb(255, 143, 188, 139),
					System.Drawing.Color.FromArgb(255, 135, 206, 235)
				};
				break;
			case ChartColorPalette.BrightPastel:
				result = new System.Drawing.Color[]
				{
					System.Drawing.Color.FromArgb(255, 65, 140, 240),
					System.Drawing.Color.FromArgb(255, 252, 180, 65),
					System.Drawing.Color.FromArgb(255, 224, 64, 10),
					System.Drawing.Color.FromArgb(255, 5, 100, 146),
					System.Drawing.Color.FromArgb(255, 191, 191, 191),
					System.Drawing.Color.FromArgb(255, 26, 59, 105),
					System.Drawing.Color.FromArgb(255, 255, 227, 130),
					System.Drawing.Color.FromArgb(255, 18, 156, 221),
					System.Drawing.Color.FromArgb(255, 202, 107, 75),
					System.Drawing.Color.FromArgb(255, 0, 92, 219),
					System.Drawing.Color.FromArgb(255, 243, 210, 136),
					System.Drawing.Color.FromArgb(255, 80, 99, 129),
					System.Drawing.Color.FromArgb(255, 241, 185, 168),
					System.Drawing.Color.FromArgb(255, 224, 131, 10),
					System.Drawing.Color.FromArgb(255, 120, 147, 190)
				};
				break;
			}
			return result;
		}
		public static System.Drawing.Color GetColorPaletteColor(this ChartColorPalette palette, int i)
		{
			return palette.GetColorPalette()[i % palette.GetColorPalette().Length];
		}
	}
}
