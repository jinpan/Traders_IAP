using System;
using System.Security.Cryptography;
using System.Text;
namespace TTS
{
	public static class CrytoHelper
	{
		public static string GenerateMD5(string input)
		{
			byte[] array = new byte[input.Length * 2];
			System.Text.Encoding.Unicode.GetBytes(input.ToCharArray(), 0, input.Length, array, 0);
			string result;
			using (new System.Security.Cryptography.MD5CryptoServiceProvider())
			{
				byte[] array2 = new System.Security.Cryptography.MD5CryptoServiceProvider().ComputeHash(array);
				System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
				for (int i = 0; i < array2.Length; i++)
				{
					stringBuilder.Append(array2[i].ToString("X2"));
				}
				result = stringBuilder.ToString();
			}
			return result;
		}
	}
}
