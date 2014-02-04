using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.CoreGraphics;

namespace CustomPageControl
{
	public class CustomPageControl : UIControl
	{
		private float _thumbDiameter, _thumbOffset;
		private int _numberOfPages, _currentPage;
		private bool _hidesForSinglePage;

		public bool DefersCurrentPageDisplay { get; set; }

		public UIColor SelectedColor { get; set; }

		public UIColor SleepingColor { get; set; }

		public float ThumbDiameter
		{
			get
			{
				if (_thumbDiameter <= 0)
					_thumbDiameter = 5;
				return _thumbDiameter;
			}
			set { _thumbDiameter = value; }
		}

		public float ThumbOffset
		{
			get
			{
				if (_thumbOffset <= 0)
					_thumbOffset = 8;
				return _thumbOffset;
			}
			set { _thumbOffset = value; }
		}

		public int Pages
		{ 
			get { return _numberOfPages; }
			set
			{
				_numberOfPages = Math.Max(0, value);
				_currentPage = Math.Min(Math.Max(0, _currentPage), _numberOfPages - 1);
				this.SetNeedsDisplay();
				if (HidesForSinglePage && (_numberOfPages < 2))
					this.Hidden = true;
				else
					this.Hidden = false;
			}
		}

		public int CurrentPage
		{ 
			get { return _currentPage; }
			set
			{
				if (_currentPage != value)
				{				
					_currentPage = Math.Min(Math.Max(0, value), _numberOfPages - 1);
					this.SetNeedsDisplay();
				}
			}
		}

		public bool HidesForSinglePage
		{ 
			get { return _hidesForSinglePage; }

			set
			{
				_hidesForSinglePage = value;
				if (_hidesForSinglePage && (_numberOfPages < 2))
					this.Hidden = true;
			}
		}

		public CustomPageControl(RectangleF frame) : base(frame)
		{
			DefersCurrentPageDisplay = false;
			this.BackgroundColor = UIColor.Clear;			
		}

		public override void SizeToFit()
		{
			RectangleF tempFrame = this.Frame;
			tempFrame.Size = SizeForNumberOfPages(Pages);
			this.Frame = tempFrame;
		}

		public override void Draw(RectangleF area)
		{
			base.Draw(area);
			CGContext context = UIGraphics.GetCurrentContext();
			context.SaveState();
			context.SetAllowsAntialiasing(true);
			// get the caller's colors it they have been set or use the defaults
			CGColor selectedColor = SelectedColor != null ? SelectedColor.CGColor : UIColor.FromWhiteAlpha(1.0f, 1.0f).CGColor;
			CGColor sleepingColor = SleepingColor != null ? SleepingColor.CGColor : UIColor.FromWhiteAlpha(0.7f, 0.5f).CGColor;

			for (int i = 0; i < Pages; i++)
			{
				RectangleF dotRect = new RectangleF((ThumbDiameter + ThumbOffset) * i, 0, ThumbDiameter, ThumbDiameter);
				if (i == CurrentPage)
				{
					context.SetFillColorWithColor(selectedColor);
					context.FillEllipseInRect(dotRect);
				}
				else
				{
					context.SetFillColorWithColor(sleepingColor);
					context.FillEllipseInRect(dotRect);
				}
			}
			context.RestoreState();
		}

		/// <summary>
		/// Tracks end of touch and change CurrentPage based on left or right touch
		/// </summary>
		/// <param name="touches">Touches.</param>
		/// <param name="evt">Evt.</param>
		public override void TouchesEnded(NSSet touches, UIEvent evt)
		{
			base.TouchesEnded(touches, evt);
			UITouch theTouch = touches.AnyObject as UITouch;
			PointF touchLocation = theTouch.LocationInView(this);

			if (touchLocation.X < (this.Bounds.Size.Width / 2))
				this.CurrentPage = Math.Max(this.CurrentPage - 1, 0);
			else
				this.CurrentPage = Math.Min(this.CurrentPage + 1, Pages - 1);

			this.SendActionForControlEvents(UIControlEvent.ValueChanged);
		}

		public SizeF SizeForNumberOfPages(int pageCount)
		{
			return new SizeF(pageCount * ThumbDiameter + (pageCount - 1) * ThumbOffset, ThumbDiameter);
		}
	}
}