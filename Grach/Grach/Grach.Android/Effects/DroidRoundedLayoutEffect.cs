using System.Linq;

using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Views;
using Grach.Droid.Effects;
using Grach.Effects;

using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

using AColor = Android.Graphics.Color;
using AView = Android.Views.View;
using Droid = Android;

[assembly: ResolutionGroupName(nameof(Grach))]
[assembly: ExportEffect(typeof(DroidRoundedLayoutEffect), nameof(RoundedLayoutEffect))]
namespace Grach.Droid.Effects
{
    public class DroidRoundedLayoutEffect : PlatformEffect
    {
        private RoundedLayoutEffect _effect;
        private Context _context;

        protected override void OnAttached()
        {
            _context = MainActivity.Context;
            _effect = (RoundedLayoutEffect)Element.Effects.FirstOrDefault(x => x is RoundedLayoutEffect);

            if (_effect == null) 
                return;

            if (Control != null)
            {
                Control.OutlineProvider = new RoundedOutlineProvider(_context.ToPixels(_effect.CornerRadius));
                Control.ClipToOutline = true;

                if (_effect.HasShadow)
                {
                    Control.Elevation = _context.ToPixels(_effect.ShadowRadius);
                }
                if (_effect.HasBorder)
                {
                    SetBorder(Control);
                }
            }
            else if (Container != null)
            {
                Container.OutlineProvider = new RoundedOutlineProvider(_context.ToPixels(_effect.CornerRadius));
                Container.ClipToOutline = true;
                if (_effect.HasShadow)
                {
                    Container.Elevation = _context.ToPixels(_effect.ShadowRadius);
                }
                if (_effect.HasBorder)
                {
                    SetBorder(Container);
                }
            }
        }

        private void SetBorder(AView view)
        {
            var gradientDrawable = new GradientDrawable();
            gradientDrawable.SetColor(((VisualElement)Element).BackgroundColor.ToAndroid());
            gradientDrawable.SetShape(ShapeType.Rectangle);
            gradientDrawable.SetCornerRadius(_context.ToPixels(_effect.CornerRadius));
            gradientDrawable.SetStroke((int)_context.ToPixels(_effect.BorderWidth), AColor.ParseColor(_effect.BorderColor.ToHex()));
            view.SetBackground(gradientDrawable);
        }

        protected override void OnDetached() { }

        public class RoundedOutlineProvider : ViewOutlineProvider
        {
            private readonly float _radius;
            public RoundedOutlineProvider(float radius)
            {
                _radius = radius;
            }
            public override void GetOutline(AView view, Outline outline)
            {
                outline?.SetRoundRect(0, 0, view.Width, view.Height, _radius);
            }
        }
    }
}