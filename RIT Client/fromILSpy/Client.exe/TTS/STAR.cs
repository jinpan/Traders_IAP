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
	internal class STAR
	{
		private static System.Resources.ResourceManager resourceMan;
		private static System.Globalization.CultureInfo resourceCulture;
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static System.Resources.ResourceManager ResourceManager
		{
			get
			{
				if (object.ReferenceEquals(STAR.resourceMan, null))
				{
					System.Resources.ResourceManager resourceManager = new System.Resources.ResourceManager("TTS.STAR", typeof(STAR).Assembly);
					STAR.resourceMan = resourceManager;
				}
				return STAR.resourceMan;
			}
		}
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static System.Globalization.CultureInfo Culture
		{
			get
			{
				return STAR.resourceCulture;
			}
			set
			{
				STAR.resourceCulture = value;
			}
		}
		internal static System.Drawing.Bitmap bg_image
		{
			get
			{
				object @object = STAR.ResourceManager.GetObject("bg_image", STAR.resourceCulture);
				return (System.Drawing.Bitmap)@object;
			}
		}
		internal static string greeting_string
		{
			get
			{
				return STAR.ResourceManager.GetString("greeting_string", STAR.resourceCulture);
			}
		}
		internal static System.Drawing.Bitmap login_image
		{
			get
			{
				object @object = STAR.ResourceManager.GetObject("login_image", STAR.resourceCulture);
				return (System.Drawing.Bitmap)@object;
			}
		}
		internal static System.Drawing.Icon program_icon
		{
			get
			{
				object @object = STAR.ResourceManager.GetObject("program_icon", STAR.resourceCulture);
				return (System.Drawing.Icon)@object;
			}
		}
		internal static string program_name
		{
			get
			{
				return STAR.ResourceManager.GetString("program_name", STAR.resourceCulture);
			}
		}
		internal static string rtd_string
		{
			get
			{
				return STAR.ResourceManager.GetString("rtd_string", STAR.resourceCulture);
			}
		}
		internal STAR()
		{
		}
	}
}
