using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace CustomPageControl
{
	public class CustomPageControlViewController : UIViewController
	{
		public static int RetinaOffset = UIScreen.MainScreen.Bounds.Height * UIScreen.MainScreen.Scale >= 1136 ? 88 : 0;
		private CustomPageControl _pageControl;

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			View.BackgroundColor = UIColor.Red;
			_pageControl = new CustomPageControl(new RectangleF(0, 450 + RetinaOffset, 320, 30));
			_pageControl.Pages = 3;
			_pageControl.CurrentPage = 0;
			_pageControl.ThumbDiameter = 30;
			_pageControl.SelectedColor = UIColor.Green;
			_pageControl.SizeToFit();
			_pageControl.Center = new PointF(320 / 2, 465 + RetinaOffset);
			Add(_pageControl);

			UIScrollView scrollView = new UIScrollView(new RectangleF(0, 0, 320, 450 + RetinaOffset))
			{
				ShowsVerticalScrollIndicator = false,
				ShowsHorizontalScrollIndicator = false,
				PagingEnabled = true,
			};
			scrollView.ContentSize = new SizeF(320 * 3, 450);
			Add(scrollView);
			scrollView.Scrolled += MainScreenScrolled;
			scrollView.AddSubview(CreateLabel(new RectangleF(0, 0, 320, 450 + RetinaOffset), "FirstScreen"));
			scrollView.AddSubview(CreateLabel(new RectangleF(320, 0, 320, 450 + RetinaOffset), "SecondScreen"));
			scrollView.AddSubview(CreateLabel(new RectangleF(640, 0, 320, 450 + RetinaOffset), "ThirdScreen"));

		}

		private UILabel CreateLabel(RectangleF rect, string title)
		{
			UILabel label = new UILabel(rect)
			{
				Text = title,
				TextAlignment = UITextAlignment.Center
			};
			return label;
		}

		private void MainScreenScrolled(object sender, EventArgs e)
		{
			UIScrollView scrollView = sender as UIScrollView;
			if (scrollView != null)
			{
				var page = (int)(scrollView.ContentOffset.X / scrollView.Frame.Width);
				_pageControl.CurrentPage = page;
			}
			                         
		}
	}
}

