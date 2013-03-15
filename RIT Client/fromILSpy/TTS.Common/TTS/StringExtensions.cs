using System;
namespace TTS
{
	public static class StringExtensions
	{
		public static string DataTableStringEscape(this string str)
		{
			return str.Replace("'", "''");
		}
	}
}
