using System;
using System.CodeDom.Compiler;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.CompilerServices;
namespace TTS.Properties
{
	[GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "10.0.0.0"), System.Runtime.CompilerServices.CompilerGenerated]
	internal sealed class Settings : ApplicationSettingsBase
	{
		private static Settings defaultInstance = (Settings)SettingsBase.Synchronized(new Settings());
		public static Settings Default
		{
			get
			{
				return Settings.defaultInstance;
			}
		}
		[DefaultSettingValue("10000"), UserScopedSetting, System.Diagnostics.DebuggerNonUserCode]
		public decimal ServerPort
		{
			get
			{
				return (decimal)this["ServerPort"];
			}
			set
			{
				this["ServerPort"] = value;
			}
		}
		[DefaultSettingValue(""), UserScopedSetting, System.Diagnostics.DebuggerNonUserCode]
		public string ServerAddress
		{
			get
			{
				return (string)this["ServerAddress"];
			}
			set
			{
				this["ServerAddress"] = value;
			}
		}
		[DefaultSettingValue(""), UserScopedSetting, System.Diagnostics.DebuggerNonUserCode]
		public string Workspace
		{
			get
			{
				return (string)this["Workspace"];
			}
			set
			{
				this["Workspace"] = value;
			}
		}
		[DefaultSettingValue(""), UserScopedSetting, System.Diagnostics.DebuggerNonUserCode]
		public string ApplicationVersion
		{
			get
			{
				return (string)this["ApplicationVersion"];
			}
			set
			{
				this["ApplicationVersion"] = value;
			}
		}
		[DefaultSettingValue("True"), UserScopedSetting, System.Diagnostics.DebuggerNonUserCode]
		public bool IsSilentAlerts
		{
			get
			{
				return (bool)this["IsSilentAlerts"];
			}
			set
			{
				this["IsSilentAlerts"] = value;
			}
		}
		[DefaultSettingValue("False"), UserScopedSetting, System.Diagnostics.DebuggerNonUserCode]
		public bool IsOTCAutoAccept
		{
			get
			{
				return (bool)this["IsOTCAutoAccept"];
			}
			set
			{
				this["IsOTCAutoAccept"] = value;
			}
		}
	}
}
