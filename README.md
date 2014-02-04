# Purpose

Custom implementation of Apple's UIPageControl, for MonoTouch platform. This code replicates all the functionality of the standard control, but allow u to change the dot color, size and offset.

# Installation

Just add CustomPageControl into ur project, and init it like this
``` cs
CustomPageControl _pageControl = new CustomPageControl(new RectangleF(0, 450 + RetinaOffset, 320, 30));
_pageControl.Pages = 3;
_pageControl.CurrentPage = 0;
_pageControl.ThumbDiameter = 30;
_pageControl.SelectedColor = UIColor.Green;
_pageControl.SleepingColor = UIColor.Red;
_pageControl.SizeToFit();
_pageControl.Center = new PointF(320 / 2, 465 + RetinaOffset);
Add(_pageControl);
```


