namespace SampleiOS
{
    [Register("AppDelegate")]
    public class AppDelegate : UIApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {
            // The window is created per scene in SceneDelegate.WillConnect (UIKit scene lifecycle, required from the iOS 27 SDK).
            return true;
        }
    }
}
