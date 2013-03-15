using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
namespace MRG.Controls.UI
{
	public class LoadingCircle : Control
	{
		public enum StylePresets
		{
			MacOSX,
			Firefox,
			IE7,
			Custom
		}
		private const double NumberOfDegreesInCircle = 360.0;
		private const double NumberOfDegreesInHalfCircle = 180.0;
		private const int DefaultInnerCircleRadius = 8;
		private const int DefaultOuterCircleRadius = 10;
		private const int DefaultNumberOfSpoke = 10;
		private const int DefaultSpokeThickness = 4;
		private const int MacOSXInnerCircleRadius = 5;
		private const int MacOSXOuterCircleRadius = 11;
		private const int MacOSXNumberOfSpoke = 12;
		private const int MacOSXSpokeThickness = 2;
		private const int FireFoxInnerCircleRadius = 6;
		private const int FireFoxOuterCircleRadius = 7;
		private const int FireFoxNumberOfSpoke = 9;
		private const int FireFoxSpokeThickness = 4;
		private const int IE7InnerCircleRadius = 8;
		private const int IE7OuterCircleRadius = 9;
		private const int IE7NumberOfSpoke = 24;
		private const int IE7SpokeThickness = 4;
		private IContainer components;
		private readonly System.Drawing.Color DefaultColor = System.Drawing.Color.DarkGray;
		private Timer m_Timer;
		private bool m_IsTimerActive;
		private int m_NumberOfSpoke;
		private int m_SpokeThickness;
		private int m_ProgressValue;
		private int m_OuterCircleRadius;
		private int m_InnerCircleRadius;
		private System.Drawing.PointF m_CenterPoint;
		private System.Drawing.Color m_Color;
		private System.Drawing.Color[] m_Colors;
		private double[] m_Angles;
		private LoadingCircle.StylePresets m_StylePreset;
		[Category("LoadingCircle"), Description("Sets the color of spoke."), TypeConverter("System.Drawing.ColorConverter")]
		public System.Drawing.Color Color
		{
			get
			{
				return this.m_Color;
			}
			set
			{
				this.m_Color = value;
				this.GenerateColorsPallet();
				base.Invalidate();
			}
		}
		[Category("LoadingCircle"), Description("Gets or sets the radius of outer circle.")]
		public int OuterCircleRadius
		{
			get
			{
				if (this.m_OuterCircleRadius == 0)
				{
					this.m_OuterCircleRadius = 10;
				}
				return this.m_OuterCircleRadius;
			}
			set
			{
				this.m_OuterCircleRadius = value;
				base.Invalidate();
			}
		}
		[Category("LoadingCircle"), Description("Gets or sets the radius of inner circle.")]
		public int InnerCircleRadius
		{
			get
			{
				if (this.m_InnerCircleRadius == 0)
				{
					this.m_InnerCircleRadius = 8;
				}
				return this.m_InnerCircleRadius;
			}
			set
			{
				this.m_InnerCircleRadius = value;
				base.Invalidate();
			}
		}
		[Category("LoadingCircle"), Description("Gets or sets the number of spoke.")]
		public int NumberSpoke
		{
			get
			{
				if (this.m_NumberOfSpoke == 0)
				{
					this.m_NumberOfSpoke = 10;
				}
				return this.m_NumberOfSpoke;
			}
			set
			{
				if (this.m_NumberOfSpoke != value && this.m_NumberOfSpoke > 0)
				{
					this.m_NumberOfSpoke = value;
					this.GenerateColorsPallet();
					this.GetSpokesAngles();
					base.Invalidate();
				}
			}
		}
		[Category("LoadingCircle"), Description("Gets or sets the number of spoke.")]
		public bool Active
		{
			get
			{
				return this.m_IsTimerActive;
			}
			set
			{
				this.m_IsTimerActive = value;
				this.ActiveTimer();
			}
		}
		[Category("LoadingCircle"), Description("Gets or sets the thickness of a spoke.")]
		public int SpokeThickness
		{
			get
			{
				if (this.m_SpokeThickness <= 0)
				{
					this.m_SpokeThickness = 4;
				}
				return this.m_SpokeThickness;
			}
			set
			{
				this.m_SpokeThickness = value;
				base.Invalidate();
			}
		}
		[Category("LoadingCircle"), Description("Gets or sets the rotation speed. Higher the slower.")]
		public int RotationSpeed
		{
			get
			{
				return this.m_Timer.Interval;
			}
			set
			{
				if (value > 0)
				{
					this.m_Timer.Interval = value;
				}
			}
		}
		[Category("LoadingCircle"), DefaultValue(typeof(LoadingCircle.StylePresets), "Custom"), Description("Quickly sets the style to one of these presets, or a custom style if desired")]
		public LoadingCircle.StylePresets StylePreset
		{
			get
			{
				return this.m_StylePreset;
			}
			set
			{
				this.m_StylePreset = value;
				switch (this.m_StylePreset)
				{
				case LoadingCircle.StylePresets.MacOSX:
					this.SetCircleAppearance(12, 2, 5, 11);
					return;
				case LoadingCircle.StylePresets.Firefox:
					this.SetCircleAppearance(9, 4, 6, 7);
					return;
				case LoadingCircle.StylePresets.IE7:
					this.SetCircleAppearance(24, 4, 8, 9);
					return;
				case LoadingCircle.StylePresets.Custom:
					this.SetCircleAppearance(10, 4, 8, 10);
					return;
				default:
					return;
				}
			}
		}
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}
		public LoadingCircle()
		{
			base.SetStyle(ControlStyles.UserPaint, true);
			base.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
			base.SetStyle(ControlStyles.ResizeRedraw, true);
			base.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
			this.m_Color = this.DefaultColor;
			this.GenerateColorsPallet();
			this.GetSpokesAngles();
			this.GetControlCenterPoint();
			this.m_Timer = new Timer();
			this.m_Timer.Tick += new System.EventHandler(this.aTimer_Tick);
			this.ActiveTimer();
			base.Resize += new System.EventHandler(this.LoadingCircle_Resize);
		}
		private void LoadingCircle_Resize(object sender, System.EventArgs e)
		{
			this.GetControlCenterPoint();
		}
		private void aTimer_Tick(object sender, System.EventArgs e)
		{
			this.m_ProgressValue = ++this.m_ProgressValue % this.m_NumberOfSpoke;
			base.Invalidate();
		}
		protected override void OnPaint(PaintEventArgs e)
		{
			if (this.m_NumberOfSpoke > 0)
			{
				e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
				int num = this.m_ProgressValue;
				for (int i = 0; i < this.m_NumberOfSpoke; i++)
				{
					num %= this.m_NumberOfSpoke;
					this.DrawLine(e.Graphics, this.GetCoordinate(this.m_CenterPoint, this.m_InnerCircleRadius, this.m_Angles[num]), this.GetCoordinate(this.m_CenterPoint, this.m_OuterCircleRadius, this.m_Angles[num]), this.m_Colors[i], this.m_SpokeThickness);
					num++;
				}
			}
			base.OnPaint(e);
		}
		public override System.Drawing.Size GetPreferredSize(System.Drawing.Size proposedSize)
		{
			proposedSize.Width = (this.m_OuterCircleRadius + this.m_SpokeThickness) * 2;
			return proposedSize;
		}
		private System.Drawing.Color Darken(System.Drawing.Color _objColor, int _intPercent)
		{
			int r = (int)_objColor.R;
			int g = (int)_objColor.G;
			int b = (int)_objColor.B;
			return System.Drawing.Color.FromArgb(_intPercent, System.Math.Min(r, 255), System.Math.Min(g, 255), System.Math.Min(b, 255));
		}
		private void GenerateColorsPallet()
		{
			this.m_Colors = this.GenerateColorsPallet(this.m_Color, this.Active, this.m_NumberOfSpoke);
		}
		private System.Drawing.Color[] GenerateColorsPallet(System.Drawing.Color _objColor, bool _blnShadeColor, int _intNbSpoke)
		{
			System.Drawing.Color[] array = new System.Drawing.Color[this.NumberSpoke];
			byte b = (byte)(255 / this.NumberSpoke);
			byte b2 = 0;
			for (int i = 0; i < this.NumberSpoke; i++)
			{
				if (_blnShadeColor)
				{
					if (i == 0 || i < this.NumberSpoke - _intNbSpoke)
					{
						array[i] = _objColor;
					}
					else
					{
						b2 += b;
						if (b2 > 255)
						{
							b2 = 255;
						}
						array[i] = this.Darken(_objColor, (int)b2);
					}
				}
				else
				{
					array[i] = _objColor;
				}
			}
			return array;
		}
		private void GetControlCenterPoint()
		{
			this.m_CenterPoint = this.GetControlCenterPoint(this);
		}
		private System.Drawing.PointF GetControlCenterPoint(Control _objControl)
		{
			return new System.Drawing.PointF((float)(_objControl.Width / 2), (float)(_objControl.Height / 2 - 1));
		}
		private void DrawLine(System.Drawing.Graphics _objGraphics, System.Drawing.PointF _objPointOne, System.Drawing.PointF _objPointTwo, System.Drawing.Color _objColor, int _intLineThickness)
		{
			using (System.Drawing.Pen pen = new System.Drawing.Pen(new System.Drawing.SolidBrush(_objColor), (float)_intLineThickness))
			{
				pen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
				pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
				_objGraphics.DrawLine(pen, _objPointOne, _objPointTwo);
			}
		}
		private System.Drawing.PointF GetCoordinate(System.Drawing.PointF _objCircleCenter, int _intRadius, double _dblAngle)
		{
			double num = 3.1415926535897931 * _dblAngle / 180.0;
			return new System.Drawing.PointF(_objCircleCenter.X + (float)_intRadius * (float)System.Math.Cos(num), _objCircleCenter.Y + (float)_intRadius * (float)System.Math.Sin(num));
		}
		private void GetSpokesAngles()
		{
			this.m_Angles = this.GetSpokesAngles(this.NumberSpoke);
		}
		private double[] GetSpokesAngles(int _intNumberSpoke)
		{
			double[] array = new double[_intNumberSpoke];
			double num = 360.0 / (double)_intNumberSpoke;
			for (int i = 0; i < _intNumberSpoke; i++)
			{
				array[i] = ((i == 0) ? num : (array[i - 1] + num));
			}
			return array;
		}
		private void ActiveTimer()
		{
			if (this.m_IsTimerActive)
			{
				this.m_Timer.Start();
			}
			else
			{
				this.m_Timer.Stop();
				this.m_ProgressValue = 0;
			}
			this.GenerateColorsPallet();
			base.Invalidate();
		}
		public void SetCircleAppearance(int numberSpoke, int spokeThickness, int innerCircleRadius, int outerCircleRadius)
		{
			this.NumberSpoke = numberSpoke;
			this.SpokeThickness = spokeThickness;
			this.InnerCircleRadius = innerCircleRadius;
			this.OuterCircleRadius = outerCircleRadius;
			base.Invalidate();
		}
	}
}
