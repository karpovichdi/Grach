using System.Linq;
using Grach.Effects;
using Grach.iOS.Effects;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ResolutionGroupName(nameof(Grach))]
[assembly: ExportEffect(typeof(IosRoundedLayoutEffect), nameof(RoundedLayoutEffect))]
namespace Grach.iOS.Effects
{
    public class IosRoundedLayoutEffect : PlatformEffect
    {
        private RoundedLayoutEffect _effect;

        protected override void OnAttached()
        {
            _effect = Element.Effects.FirstOrDefault(x => x is RoundedLayoutEffect) as RoundedLayoutEffect;
            if (_effect == null) 
                return;

            UIView control = Control ?? Container;
            if (control != null)
            {
                SetViewLayer(control);
            }
        }

        private void SetViewLayer(UIView view)
        {
            view.Layer.CornerRadius = _effect.CornerRadius;
            if (_effect.HasShadow)
            {
                view.Layer.ShadowColor = _effect.ShadowColor.ToCGColor();
                view.Layer.ShadowOffset = new CoreGraphics.CGSize(0, _effect.ShadowRadius * .5);
                view.Layer.ShadowRadius = _effect.ShadowRadius;
                view.Layer.ShadowOpacity = 1.0f;
            }
            if (_effect.HasBorder)
            {
                SetBorder(view);
            }
            view.Layer.MasksToBounds = false;
        }

        private void SetBorder(UIView view)
        {
            view.Layer.BorderWidth = _effect.BorderWidth;
            view.Layer.BorderColor = _effect.BorderColor.ToCGColor();
        }

        protected override void OnDetached() { }
    }
}