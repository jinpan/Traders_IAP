using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
namespace SnapFormExtender
{
	public class SnapFormExtender : Component, IExtenderProvider, ISupportInitialize
	{
		private enum Orientation
		{
			Vertical,
			Horizontal
		}
		private class Edge
		{
			public int position;
			public int top;
			public int bottom;
			public Edge(int position, int top, int bottom)
			{
				this.position = position;
				this.top = top;
				this.bottom = bottom;
			}
			public int DistanceTo(SnapFormExtender.Edge edge)
			{
				return System.Math.Abs(edge.position - this.position);
			}
		}
		private bool enableSnap = true;
		private int snapDistance = 10;
		private System.Windows.Forms.Form parentForm;
		[Category("Misc"), DefaultValue(true), Description("Enable to align MDI child forms on moving and resizing.")]
		public bool Enabled
		{
			get
			{
				return this.enableSnap;
			}
			set
			{
				this.enableSnap = value;
			}
		}
		[Category("Misc"), DefaultValue(null), Description("Parent MDI form which childs should be aligned.")]
		public System.Windows.Forms.Form Form
		{
			get
			{
				return this.parentForm;
			}
			set
			{
				this.parentForm = value;
			}
		}
		[Category("Misc"), DefaultValue(4), Description("Distance at which the adhesion will occur.")]
		public int Distance
		{
			get
			{
				return this.snapDistance;
			}
			set
			{
				if (value < 0 || value > 20)
				{
					throw new System.ArgumentOutOfRangeException("Distance", value, "Distance must be a positive number less or equal to 20.");
				}
				this.snapDistance = value;
			}
		}
		public SnapFormExtender(IContainer container) : this()
		{
			container.Add(this);
		}
		public SnapFormExtender()
		{
		}
		public bool CanExtend(object component)
		{
			if (component is System.Windows.Forms.Form)
			{
				System.Windows.Forms.Form form = (System.Windows.Forms.Form)component;
				return form.IsMdiContainer;
			}
			return false;
		}
		void ISupportInitialize.BeginInit()
		{
		}
		void ISupportInitialize.EndInit()
		{
			if (!base.DesignMode && this.parentForm != null)
			{
				this.parentForm.MdiChildActivate += new System.EventHandler(this.parentForm_MdiChildActivate);
			}
		}
		private void parentForm_MdiChildActivate(object sender, System.EventArgs e)
		{
			if (this.parentForm.ActiveMdiChild != null)
			{
				System.Windows.Forms.Form activeMdiChild = this.parentForm.ActiveMdiChild;
				activeMdiChild.Move -= new System.EventHandler(this.child_Move);
				activeMdiChild.Resize -= new System.EventHandler(this.child_Resize);
				if (this.enableSnap)
				{
					activeMdiChild.Move += new System.EventHandler(this.child_Move);
					activeMdiChild.Resize += new System.EventHandler(this.child_Resize);
				}
			}
		}
		private void child_Move(object sender, System.EventArgs e)
		{
			if (sender == null)
			{
				return;
			}
			System.Windows.Forms.Form form = (System.Windows.Forms.Form)sender;
			if (form.WindowState != System.Windows.Forms.FormWindowState.Normal)
			{
				return;
			}
			System.Drawing.Rectangle mdiClientArea = this.GetMdiClientArea();
			this.SnapOnMove(form, mdiClientArea, SnapFormExtender.Orientation.Vertical);
			this.SnapOnMove(form, mdiClientArea, SnapFormExtender.Orientation.Horizontal);
		}
		private void child_Resize(object sender, System.EventArgs e)
		{
			if (sender == null)
			{
				return;
			}
			System.Windows.Forms.Form form = (System.Windows.Forms.Form)sender;
			if (form.WindowState != System.Windows.Forms.FormWindowState.Normal || form.FormBorderStyle == System.Windows.Forms.FormBorderStyle.FixedSingle)
			{
				return;
			}
			System.Drawing.Rectangle mdiClientArea = this.GetMdiClientArea();
			this.SnapOnResize(form, mdiClientArea);
		}
		private void SnapOnMove(System.Windows.Forms.Form form, System.Drawing.Rectangle mdiClientArea, SnapFormExtender.Orientation orientation)
		{
			int x = form.Location.X;
			int y = form.Location.Y;
			int width = form.Size.Width;
			int height = form.Size.Height;
			SnapFormExtender.Edge edge;
			SnapFormExtender.Edge edge2;
			SnapFormExtender.Edge edge3;
			SnapFormExtender.Edge edge4;
			if (orientation == SnapFormExtender.Orientation.Vertical)
			{
				edge = new SnapFormExtender.Edge(x, y, y + height - 1);
				edge2 = new SnapFormExtender.Edge(x + width - 1, y, y + height - 1);
				edge3 = new SnapFormExtender.Edge(0, 0, mdiClientArea.Height - 1);
				edge4 = new SnapFormExtender.Edge(mdiClientArea.Width - 1, 0, mdiClientArea.Height - 1);
			}
			else
			{
				edge = new SnapFormExtender.Edge(y, x, x + width - 1);
				edge2 = new SnapFormExtender.Edge(y + height - 1, x, x + width - 1);
				edge3 = new SnapFormExtender.Edge(0, 0, mdiClientArea.Width - 1);
				edge4 = new SnapFormExtender.Edge(mdiClientArea.Height - 1, 0, mdiClientArea.Width - 1);
			}
			int num = this.parentForm.MdiChildren.Length;
			int num2 = 0;
			int num3 = this.snapDistance + 1;
			int num4 = 0;
			int num5;
			while (num4 < num && num3 > 0)
			{
				System.Windows.Forms.Form form2 = this.parentForm.MdiChildren[num4];
				if (form2 != form && form2.Visible)
				{
					int x2 = form2.Location.X;
					int y2 = form2.Location.Y;
					int width2 = form2.Size.Width;
					int height2 = form2.Size.Height;
					SnapFormExtender.Edge edge5;
					SnapFormExtender.Edge edge6;
					if (orientation == SnapFormExtender.Orientation.Vertical)
					{
						edge5 = new SnapFormExtender.Edge(x2, y2, y2 + height2 - 1);
						edge6 = new SnapFormExtender.Edge(x2 + width2, y2, y2 + height2 - 1);
					}
					else
					{
						edge5 = new SnapFormExtender.Edge(y2, x2, x2 + width2 - 1);
						edge6 = new SnapFormExtender.Edge(y2 + height2, x2, x2 + width2 - 1);
					}
					if ((num5 = this.MinDistance(edge5, edge)) < num3)
					{
						num3 = num5;
						num2 = edge5.position;
					}
					if ((num5 = this.MinDistance(edge6, edge)) < num3)
					{
						num3 = num5;
						num2 = edge6.position;
					}
					edge5.position--;
					edge6.position--;
					if ((num5 = this.MinDistance(edge5, edge2)) < num3)
					{
						num3 = num5;
						num2 = edge5.position - (edge2.position - edge.position);
					}
					if ((num5 = this.MinDistance(edge6, edge2)) < num3)
					{
						num3 = num5;
						num2 = edge6.position - (edge2.position - edge.position);
					}
				}
				num4++;
			}
			if ((num5 = this.MinDistance(edge3, edge)) < num3)
			{
				num3 = num5;
				num2 = edge3.position;
			}
			if ((num5 = this.MinDistance(edge4, edge2)) < num3)
			{
				num3 = num5;
				num2 = edge4.position - (edge2.position - edge.position);
			}
			if (num3 <= this.snapDistance && num3 > 0)
			{
				if (orientation == SnapFormExtender.Orientation.Vertical)
				{
					form.Location = new System.Drawing.Point(num2, y);
					return;
				}
				form.Location = new System.Drawing.Point(x, num2);
			}
		}
		private void SnapOnResize(System.Windows.Forms.Form form, System.Drawing.Rectangle mdiClientArea)
		{
			int x = form.Location.X;
			int y = form.Location.Y;
			int width = form.Size.Width;
			int height = form.Size.Height;
			SnapFormExtender.Edge edge = new SnapFormExtender.Edge(x + width - 1, y, y + height - 1);
			SnapFormExtender.Edge edge2 = new SnapFormExtender.Edge(y + height - 1, x, x + width - 1);
			SnapFormExtender.Edge edge3 = new SnapFormExtender.Edge(mdiClientArea.Width - 1, 0, mdiClientArea.Height - 1);
			SnapFormExtender.Edge edge4 = new SnapFormExtender.Edge(mdiClientArea.Height - 1, 0, mdiClientArea.Width - 1);
			int num = this.parentForm.MdiChildren.Length;
			int width2;
			int height2 = width2 = 0;
			int num3;
			int num2 = num3 = this.snapDistance + 1;
			int num4;
			int num5;
			for (int i = 0; i < num; i++)
			{
				System.Windows.Forms.Form form2 = this.parentForm.MdiChildren[i];
				if (form2 != form && form2.Visible)
				{
					int x2 = form2.Location.X;
					int y2 = form2.Location.Y;
					int width3 = form2.Size.Width;
					int height3 = form2.Size.Height;
					SnapFormExtender.Edge edge5 = new SnapFormExtender.Edge(x2 - 1, y2, y2 + height3 - 1);
					SnapFormExtender.Edge edge6 = new SnapFormExtender.Edge(x2 + width3 - 1, y2, y2 + height3 - 1);
					SnapFormExtender.Edge edge7 = new SnapFormExtender.Edge(y2 - 1, x2, x2 + width3 - 1);
					SnapFormExtender.Edge edge8 = new SnapFormExtender.Edge(y2 + height3 - 1, x2, x2 + width3 - 1);
					if ((num4 = this.MinDistance(edge5, edge)) < num3)
					{
						num3 = num4;
						width2 = edge5.position - x + 1;
					}
					if ((num4 = this.MinDistance(edge6, edge)) < num3)
					{
						num3 = num4;
						width2 = edge6.position - x + 1;
					}
					if ((num5 = this.MinDistance(edge7, edge2)) < num2)
					{
						num2 = num5;
						height2 = edge7.position - y + 1;
					}
					if ((num5 = this.MinDistance(edge8, edge2)) < num2)
					{
						num2 = num5;
						height2 = edge8.position - y + 1;
					}
				}
			}
			if ((num4 = this.MinDistance(edge3, edge)) < num3)
			{
				num3 = num4;
				width2 = edge3.position - x + 1;
			}
			if ((num5 = this.MinDistance(edge4, edge2)) < num2)
			{
				num2 = num5;
				height2 = edge4.position - y + 1;
			}
			if (num3 <= this.snapDistance && num3 > 0)
			{
				form.Size = new System.Drawing.Size(width2, height);
			}
			if (num2 <= this.snapDistance && num2 > 0)
			{
				form.Size = new System.Drawing.Size(width, height2);
			}
		}
		private int MinDistance(SnapFormExtender.Edge edge1, SnapFormExtender.Edge edge2)
		{
			int num = edge1.DistanceTo(edge2);
			if (num > this.snapDistance || edge1.top > edge2.bottom + 1 || edge1.bottom < edge2.top - 1)
			{
				return 2147483647;
			}
			return num;
		}
		private System.Drawing.Rectangle GetMdiClientArea()
		{
			System.Drawing.Rectangle result = System.Drawing.Rectangle.Empty;
			if (this.parentForm == null)
			{
				return result;
			}
			foreach (System.Windows.Forms.Control control in this.parentForm.Controls)
			{
				if (control is System.Windows.Forms.MdiClient)
				{
					result = control.ClientRectangle;
				}
			}
			return result;
		}
	}
}
