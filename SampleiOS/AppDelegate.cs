using DSComponentsSample.Controllers;

namespace SampleiOS
{
    [Register("AppDelegate")]
    public class AppDelegate : UIApplicationDelegate
    {
        public override UIWindow? Window
        {
            get;
            set;
        }

        UIViewController viewController;

        public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {
            // create a new window instance based on the screen size
            Window = new UIWindow(UIScreen.MainScreen.Bounds);

            // create a UIViewController with a single UILabel
            viewController = new UINavigationController(new DSComponentsMenuControllerController());
            Window.RootViewController = viewController;
            Window.MakeKeyAndVisible();

            return true;
        }
    }
}
