using Xamarin.Forms;

namespace Grach.Controls
{
    public class SquareView : ContentView
    {
        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
        }

        protected override SizeRequest OnMeasure(double widthConstraint, double heightConstraint)
        {
            HeightRequest = widthConstraint;
            return base.OnMeasure(widthConstraint, heightConstraint);
        }
    }
}