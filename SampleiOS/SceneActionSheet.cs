using System;
using System.Collections.Generic;
using UIKit;

namespace DSComponentsSample
{
    /// <summary>
    /// Event data for <see cref="SceneActionSheet.Clicked"/>.
    /// </summary>
    public class SheetButtonEventArgs : EventArgs
    {
        public int ButtonIndex { get; }

        public SheetButtonEventArgs(int buttonIndex)
        {
            ButtonIndex = buttonIndex;
        }
    }

    /// <summary>
    /// Replacement for UIActionSheet, which is unavailable in UIScene-based apps. Has the same shape
    /// (AddButton, CancelButtonIndex, Clicked, ShowFrom) and shows a UIAlertController action sheet.
    /// </summary>
    public class SceneActionSheet
    {
        private readonly UIViewController mPresenter;
        private readonly string mTitle;
        private readonly List<string> mButtons = new List<string>();

        public int CancelButtonIndex { get; set; } = -1;

        public event EventHandler<SheetButtonEventArgs> Clicked;

        public SceneActionSheet(UIViewController presenter, string title)
        {
            mPresenter = presenter;
            mTitle = title;
        }

        public void AddButton(string title)
        {
            mButtons.Add(title);
        }

        public void ShowFrom(UIBarButtonItem item, bool animated)
        {
            var controller = UIAlertController.Create(mTitle, null, UIAlertControllerStyle.ActionSheet);

            for (var i = 0; i < mButtons.Count; i++)
            {
                var index = i;
                var style = index == CancelButtonIndex ? UIAlertActionStyle.Cancel : UIAlertActionStyle.Default;
                controller.AddAction(UIAlertAction.Create(mButtons[index], style, _ => Clicked?.Invoke(this, new SheetButtonEventArgs(index))));
            }

            // iPad shows action sheets as popovers, which need an anchor
            if (controller.PopoverPresentationController != null)
                controller.PopoverPresentationController.BarButtonItem = item;

            mPresenter.PresentViewController(controller, animated, null);
        }
    }
}
