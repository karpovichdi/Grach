using System;
using Grach.Interfaces;
using Grach.iOS.Renderers;
using Grach.Interfaces;
using Grach.iOS.Renderers;

using UIKit;

using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ContentPage), typeof(ContentPageRenderer))]
namespace Grach.iOS.Renderers
{
    public class ContentPageRenderer : PageRenderer
    {
        private UIBarButtonItem backButton;

        protected UIBarButtonItem BackButton
        {
            get
            {
                if (backButton == null)
                {
                    backButton = new UIBarButtonItem
                    {
                        Title = string.Empty,
                        Image = UIImage.FromFile("back_icon.png"),
                        ImageInsets = new UIEdgeInsets(0, -8, 0, 0)
                    };
                }

                return backButton;
            }
        }

        protected UIBarButtonItem LeftBarButtonItem
        {
            get => NavigationController?.TopViewController?.NavigationItem?.LeftBarButtonItem;
            set
            {
                if (NavigationController?.TopViewController?.NavigationItem != null)
                    NavigationController.TopViewController.NavigationItem.LeftBarButtonItem = value;
            }
        }

        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            if (NavigationItem != null && Element is Page page &&
                NavigationPage.GetHasNavigationBar(page) &&
                NavigationPage.GetHasBackButton(page))
            {
                SetCustomBackButton();
            }
        }

        public override void ViewWillDisappear(bool animated)
        {
            BackButton.Clicked -= BackButtonClicked;
            base.ViewWillDisappear(animated);
        }

        private void SetCustomBackButton()
        {
            BackButton.Clicked += BackButtonClicked;
            LeftBarButtonItem = BackButton;
        }

        private void BackButtonClicked(object sender, EventArgs e)
        {
            if (Element is Page page && page.BindingContext is IBackNavigationHandler backNavigationHandler)
            {
                backNavigationHandler.NavigateBack();
            }
        }
    }
}