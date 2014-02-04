using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace CustomPageControl
{
	[Register("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate
	{
		UIWindow window;
		CustomPageControlViewController viewController;

		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{
			window = new UIWindow(UIScreen.MainScreen.Bounds);
			
			viewController = new CustomPageControlViewController();
			window.RootViewController = viewController;
			window.MakeKeyAndVisible();
			
			return true;
		}

		static void Main(string[] args)
		{
			UIApplication.Main(args, null, "AppDelegate");
		}
	}
}

