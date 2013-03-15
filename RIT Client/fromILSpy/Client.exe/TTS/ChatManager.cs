using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
namespace TTS
{
	public static class ChatManager
	{
		public delegate void ChatUpdatedDelegate(string chatkey, string header, string text, System.Drawing.Color color);
		private static System.Collections.Generic.Dictionary<string, System.Text.StringBuilder> Chats;
		private static readonly System.Text.StringBuilder TextHeader;
		public static event ChatManager.ChatUpdatedDelegate ChatUpdated;
		static ChatManager()
		{
			ChatManager.Chats = new System.Collections.Generic.Dictionary<string, System.Text.StringBuilder>();
			ChatManager.ChatUpdated = delegate
			{
			};
			ChatManager.TextHeader = new System.Text.StringBuilder("{\\rtf1\\ansi{\\colortbl;");
			System.Drawing.Color[] textColors = ConnectedTradersManager.TextColors;
			for (int i = 0; i < textColors.Length; i++)
			{
				System.Drawing.Color color = textColors[i];
				ChatManager.TextHeader.Append(string.Format("\\red{0}\\green{1}\\blue{2};", color.R, color.G, color.B));
			}
			ChatManager.TextHeader.Append("}}");
		}
		public static void Initialize()
		{
			ChatManager.Chats.Clear();
		}
		public static string GetChatHistory(string chatkey)
		{
			if (ChatManager.Chats.ContainsKey(chatkey))
			{
				return ChatManager.Chats[chatkey].ToString();
			}
			ChatManager.AddChat(chatkey);
			return ChatManager.Chats[chatkey].ToString();
		}
		public static void AddChat(string chatkey)
		{
			if (!ChatManager.Chats.ContainsKey(chatkey))
			{
				ChatManager.Chats.Add(chatkey, new System.Text.StringBuilder(ChatManager.TextHeader.ToString()));
				ChatManager.Chats[chatkey].Insert(ChatManager.Chats[chatkey].Length - 1, string.Format("\\i Chatting with {0}. Chat logs may be stored and saved. \\i0", string.IsNullOrWhiteSpace(chatkey) ? "Everyone" : chatkey));
			}
		}
		public static void AddChatText(string from, string to, string text)
		{
			string text2 = string.IsNullOrWhiteSpace(to) ? "" : ((from == Game.State.Trader.TraderID) ? to : from);
			string text3 = string.Format("[{0:HH:mm:ss}] {1}: ", System.DateTime.Now.ToString("t"), from);
			ChatManager.AddChat(text2);
			ChatManager.Chats[text2].Insert(ChatManager.Chats[text2].Length - 1, string.Format("\\par\\cf{2}\\b{0}\\b0\\cf0 {1}", text3, text.Replace("\\", "\\\\").Replace("{", "\\{").Replace("}", "\\}"), ConnectedTradersManager.TraderColor[from].Item2 + 1));
			if (string.IsNullOrWhiteSpace(text2) && (TTSFormManager.Instance.CurrentForm == null || TTSFormManager.Instance.CurrentForm.GetType() != typeof(Chat)))
			{
				Game.State.UnreadChatCount++;
			}
			ChatManager.ChatUpdated(text2, text3, text, ConnectedTradersManager.TraderColor[from].Item1);
			if (!string.IsNullOrWhiteSpace(text2))
			{
				TTSFormManager.Instance.FindAddChatWindow(text2);
			}
		}
	}
}
