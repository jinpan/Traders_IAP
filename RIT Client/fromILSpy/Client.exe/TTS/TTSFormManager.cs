using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Windows.Forms;
using System.Xml;
namespace TTS
{
	internal class TTSFormManager
	{
		private Queue<System.Type> LastFocusedType = new Queue<System.Type>();
		private System.Collections.Generic.Dictionary<string, WorkspaceItem> Workspaces = new System.Collections.Generic.Dictionary<string, WorkspaceItem>();
		private System.Windows.Forms.Form MDIParent;
		private System.Windows.Forms.ToolStripDropDownButton WorkspaceDropDown;
		private System.Windows.Forms.ToolStripDropDownButton WindowDropDown;
		public string CurrentWorkspace = "Default";
		public static TTSFormManager Instance
		{
			get;
			set;
		}
		public System.Windows.Forms.Form CurrentForm
		{
			get
			{
				return this.MDIParent.ActiveMdiChild;
			}
		}
		public TTSFormManager(System.Windows.Forms.Form mdiparent, System.Windows.Forms.ToolStripDropDownButton workspacedropdown, System.Windows.Forms.ToolStripDropDownButton windowdropdown)
		{
			this.MDIParent = mdiparent;
			this.WorkspaceDropDown = workspacedropdown;
			this.WindowDropDown = windowdropdown;
			this.ClearWindows();
			this.ClearWorkspace();
			this.AddWorkspace(this.CurrentWorkspace, true);
			this.WindowDropDown.Text = "Windows (0)";
		}
		private bool IsWindowAllowed(System.Type ttsformtype)
		{
			System.Reflection.FieldInfo field = ttsformtype.GetField("TTSWindowType", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);
			return !(field == null) && Game.State.General.AllowedWindows.HasFlag((WindowType)field.GetValue(null));
		}
		public void EnforceAllowedWindows()
		{
			string[] array = this.Workspaces.Keys.ToArray<string>();
			string[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				string key = array2[i];
				WorkspaceItem.WindowItem[] array3 = this.Workspaces[key].WindowItems.ToArray();
				WorkspaceItem.WindowItem[] array4 = array3;
				for (int j = 0; j < array4.Length; j++)
				{
					WorkspaceItem.WindowItem windowItem = array4[j];
					if (!this.IsWindowAllowed(windowItem.Window.GetType()))
					{
						windowItem.Window.Close();
					}
				}
			}
		}
		public void FindAddWindow(System.Type ttsformtype, TTSFormState state = null)
		{
			if (!ttsformtype.IsSubclassOf(typeof(TTSForm)))
			{
				throw new System.Exception("Invalid window type.");
			}
			System.Windows.Forms.Form[] mdiChildren = this.MDIParent.MdiChildren;
			for (int i = 0; i < mdiChildren.Length; i++)
			{
				System.Windows.Forms.Form form = mdiChildren[i];
				if (form.GetType() == ttsformtype && form.Visible)
				{
					form.Focus();
					form.WindowState = ((form.WindowState == System.Windows.Forms.FormWindowState.Minimized) ? System.Windows.Forms.FormWindowState.Normal : form.WindowState);
					return;
				}
			}
			this.AddWindow(ttsformtype, null, state);
		}
		public void FindAddChatWindow(string chatkey)
		{
			if (string.IsNullOrWhiteSpace(chatkey))
			{
				this.FindAddWindow(typeof(Chat), null);
				return;
			}
			System.Type typeFromHandle = typeof(PrivateChat);
			System.Windows.Forms.Form[] mdiChildren = this.MDIParent.MdiChildren;
			for (int i = 0; i < mdiChildren.Length; i++)
			{
				System.Windows.Forms.Form form = mdiChildren[i];
				if (form.GetType() == typeFromHandle && form.Visible && ((PrivateChat)form).ChatKey == chatkey)
				{
					form.Focus();
					form.WindowState = ((form.WindowState == System.Windows.Forms.FormWindowState.Minimized) ? System.Windows.Forms.FormWindowState.Normal : form.WindowState);
					return;
				}
			}
			TTSFormState state = new TTSFormState(chatkey);
			this.AddWindow(typeFromHandle, null, state);
		}
		public void AddWindow(System.Type ttsformtype, string workspace = null, TTSFormState state = null)
		{
			if (!ttsformtype.IsSubclassOf(typeof(TTSForm)))
			{
				throw new System.Exception("Invalid window type.");
			}
			if (!this.IsWindowAllowed(ttsformtype))
			{
				return;
			}
			workspace = (workspace ?? this.CurrentWorkspace);
			if (state == null)
			{
				WorkspaceItem.WindowItem windowItem = this.Workspaces[workspace].WindowItems.FindLast((WorkspaceItem.WindowItem x) => x.Window.GetType() == ttsformtype);
				if (windowItem != null && System.Windows.Forms.Control.ModifierKeys != System.Windows.Forms.Keys.Shift)
				{
					this.LastFocusedType.Enqueue(ttsformtype);
					if (this.LastFocusedType.Count > 2)
					{
						System.Type lastformtype = this.LastFocusedType.Dequeue();
						if (this.LastFocusedType.All((System.Type x) => x == lastformtype))
						{
							((Client)ThreadHelper.MainThread).ShowInfo("Additional Windows", "Hold [Shift] when clicking a menu item to open additional copies of the same window.");
						}
					}
					windowItem.Window.Activate();
					return;
				}
			}
			TTSForm form = (TTSForm)System.Activator.CreateInstance(ttsformtype, new object[]
			{
				state
			});
			form.MdiParent = this.MDIParent;
			TTSToolStripMenuItem menuitem = new TTSToolStripMenuItem(form.Text, null, delegate(object sender, System.EventArgs e)
			{
				form.Focus();
			});
			form.FormClosed += delegate(object sender, System.Windows.Forms.FormClosedEventArgs e)
			{
				this.WindowDropDown.DropDownItems.Remove(menuitem);
				this.Workspaces[workspace].WindowItems.RemoveAll((WorkspaceItem.WindowItem x) => x.Window == form);
				this.WindowDropDown.Text = string.Format("Windows ({0})", this.Workspaces[workspace].WindowItems.Count);
			};
			form.Activated += delegate(object sender, System.EventArgs e)
			{
				menuitem.Checked = true;
			};
			form.Deactivate += delegate(object sender, System.EventArgs e)
			{
				menuitem.Checked = false;
			};
			this.Workspaces[workspace].WindowItems.Add(new WorkspaceItem.WindowItem
			{
				Window = form,
				WindowMenuItem = menuitem
			});
			this.WindowDropDown.Text = string.Format("Windows ({0})", this.Workspaces[workspace].WindowItems.Count);
			if (workspace == this.CurrentWorkspace)
			{
				this.WindowDropDown.DropDownItems.Add(menuitem);
				form.Show();
			}
		}
		public void AddWorkspace(string name, bool select = false)
		{
			name = name.Trim();
			if (string.IsNullOrWhiteSpace(name) || this.Workspaces.ContainsKey(name))
			{
				throw new System.Exception("Invalid workspace name.");
			}
			TTSToolStripMenuItem tTSToolStripMenuItem = new TTSToolStripMenuItem(name, null, delegate(object sender, System.EventArgs e)
			{
				System.Windows.Forms.Form[] mdiChildren = this.MDIParent.MdiChildren;
				System.Windows.Forms.Form form;
				for (int i = 0; i < mdiChildren.Length; i++)
				{
					form = mdiChildren[i];
					if (form is TTSForm)
					{
						if (this.Workspaces[name].WindowItems.Exists((WorkspaceItem.WindowItem x) => x.Window == form))
						{
							form.Show();
						}
						else
						{
							form.Hide();
						}
					}
				}
				foreach (System.Windows.Forms.ToolStripItem toolStripItem in this.WorkspaceDropDown.DropDownItems)
				{
					if (toolStripItem is TTSToolStripMenuItem)
					{
						((TTSToolStripMenuItem)toolStripItem).Checked = false;
					}
				}
				foreach (WorkspaceItem.WindowItem current in this.Workspaces[this.CurrentWorkspace].WindowItems)
				{
					this.WindowDropDown.DropDownItems.Remove(current.WindowMenuItem);
				}
				foreach (WorkspaceItem.WindowItem current2 in this.Workspaces[name].WindowItems)
				{
					this.WindowDropDown.DropDownItems.Add(current2.WindowMenuItem);
				}
				((TTSToolStripMenuItem)sender).Checked = true;
				this.CurrentWorkspace = name;
				this.WindowDropDown.Text = string.Format("Windows ({0})", this.Workspaces[name].WindowItems.Count);
				this.WorkspaceDropDown.Text = name;
			});
			this.WorkspaceDropDown.DropDownItems.Add(tTSToolStripMenuItem);
			this.Workspaces.Add(name, new WorkspaceItem
			{
				WorkspaceMenuItem = tTSToolStripMenuItem
			});
			if (select)
			{
				tTSToolStripMenuItem.PerformClick();
			}
		}
		public string SaveLayout()
		{
			string[] array = this.Workspaces.Keys.ToArray<string>();
			System.Collections.Generic.List<TTSFormStateItem> list = new System.Collections.Generic.List<TTSFormStateItem>();
			string[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				string text = array2[i];
				foreach (WorkspaceItem.WindowItem current in this.Workspaces[text].WindowItems)
				{
					if (!(current.Window.GetType() == typeof(PrivateChat)))
					{
						list.Add(new TTSFormStateItem
						{
							State = current.Window.GetFormState(),
							Workspace = text
						});
					}
				}
			}
			System.IO.StringWriter stringWriter = new System.IO.StringWriter();
			XmlTextWriter writer = new XmlTextWriter(stringWriter);
			new NetDataContractSerializer().WriteObject(writer, list.ToArray());
			return stringWriter.ToString();
		}
		public bool LoadLayout(string layout)
		{
			bool result;
			try
			{
				XmlTextReader xmlTextReader = new XmlTextReader(new System.IO.StringReader(layout));
				TTSFormStateItem[] array = (TTSFormStateItem[])new NetDataContractSerializer().ReadObject(xmlTextReader);
				xmlTextReader.Close();
				this.Reset();
				TTSFormStateItem[] array2 = array;
				for (int i = 0; i < array2.Length; i++)
				{
					TTSFormStateItem tTSFormStateItem = array2[i];
					if (!this.Workspaces.ContainsKey(tTSFormStateItem.Workspace))
					{
						this.AddWorkspace(tTSFormStateItem.Workspace, false);
					}
					System.Type type = System.Type.GetType(tTSFormStateItem.State.WindowType);
					if (!(type == typeof(PrivateChat)))
					{
						this.AddWindow(type, tTSFormStateItem.Workspace, tTSFormStateItem.State);
					}
				}
				this.Workspaces["Default"].WorkspaceMenuItem.PerformClick();
				result = true;
			}
			catch
			{
				result = false;
			}
			return result;
		}
		public void RemoveWorkspace()
		{
			if (this.CurrentWorkspace == "Default")
			{
				throw new System.Exception("Cannot remove Default workspace.");
			}
			this.ClearWorkspaceWindows(null);
			this.WorkspaceDropDown.DropDownItems.Remove(this.Workspaces[this.CurrentWorkspace].WorkspaceMenuItem);
			this.Workspaces.Remove(this.CurrentWorkspace);
			this.CurrentWorkspace = "Default";
			this.Workspaces["Default"].WorkspaceMenuItem.PerformClick();
		}
		public void Reset()
		{
			this.ClearWindows();
			this.ClearWorkspace();
			this.CurrentWorkspace = "Default";
			this.AddWorkspace("Default", true);
			this.WindowDropDown.Text = "Windows (0)";
		}
		public void ClearWorkspaceWindows(System.Type windowtype = null)
		{
			TTSForm[] array = (
				from x in this.Workspaces[this.CurrentWorkspace].WindowItems
				where windowtype == null || x.Window.GetType() == windowtype
				select x.Window).ToArray<TTSForm>();
			TTSForm[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				TTSForm tTSForm = array2[i];
				tTSForm.Close();
				tTSForm.Dispose();
			}
		}
		public void ClearWindows()
		{
			System.Windows.Forms.Form[] array = this.MDIParent.MdiChildren.ToArray<System.Windows.Forms.Form>();
			System.Windows.Forms.Form[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				System.Windows.Forms.Form form = array2[i];
				form.Show();
				form.Close();
				form.Dispose();
			}
		}
		public void ClearWorkspace()
		{
			System.Collections.Generic.List<System.Windows.Forms.ToolStripItem> list = new System.Collections.Generic.List<System.Windows.Forms.ToolStripItem>();
			foreach (System.Windows.Forms.ToolStripItem toolStripItem in this.WorkspaceDropDown.DropDownItems)
			{
				if (toolStripItem is TTSToolStripMenuItem)
				{
					list.Add(toolStripItem);
				}
			}
			foreach (System.Windows.Forms.ToolStripItem current in list)
			{
				this.WorkspaceDropDown.DropDownItems.Remove(current);
			}
			this.Workspaces.Clear();
		}
	}
}
