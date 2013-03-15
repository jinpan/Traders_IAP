using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;
namespace TTS
{
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0"), System.Diagnostics.DebuggerNonUserCode, System.Runtime.CompilerServices.CompilerGenerated]
	internal class RIT
	{
		private static System.Resources.ResourceManager resourceMan;
		private static System.Globalization.CultureInfo resourceCulture;
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static System.Resources.ResourceManager ResourceManager
		{
			get
			{
				if (object.ReferenceEquals(RIT.resourceMan, null))
				{
					System.Resources.ResourceManager resourceManager = new System.Resources.ResourceManager("TTS.RIT", typeof(RIT).Assembly);
					RIT.resourceMan = resourceManager;
				}
				return RIT.resourceMan;
			}
		}
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static System.Globalization.CultureInfo Culture
		{
			get
			{
				return RIT.resourceCulture;
			}
			set
			{
				RIT.resourceCulture = value;
			}
		}
		internal static System.Drawing.Bitmap bg_image
		{
			get
			{
				object @object = RIT.ResourceManager.GetObject("bg_image", RIT.resourceCulture);
				return (System.Drawing.Bitmap)@object;
			}
		}
		internal static string greeting_string
		{
			get
			{
				return RIT.ResourceManager.GetString("greeting_string", RIT.resourceCulture);
			}
		}
		internal static System.Drawing.Bitmap login_image
		{
			get
			{
				object @object = RIT.ResourceManager.GetObject("login_image", RIT.resourceCulture);
				return (System.Drawing.Bitmap)@object;
			}
		}
		internal static System.Drawing.Icon program_icon
		{
			get
			{
				object @object = RIT.ResourceManager.GetObject("program_icon", RIT.resourceCulture);
				return (System.Drawing.Icon)@object;
			}
		}
		internal static string program_name
		{
			get
			{
				return RIT.ResourceManager.GetString("program_name", RIT.resourceCulture);
			}
		}
		internal static string rtd_string
		{
			get
			{
				return RIT.ResourceManager.GetString("rtd_string", RIT.resourceCulture);
			}
		}
		internal RIT()
		{
		}
	}
}
