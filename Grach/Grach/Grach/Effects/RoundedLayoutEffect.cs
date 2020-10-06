using Grach.Utills.Constants;
using Xamarin.Forms;

namespace Grach.Effects
{
    public class RoundedLayoutEffect : RoutingEffect
    {
        public bool HasShadow { get; set; } = true;

        public bool HasBorder { get; set; } = false;

        public float CornerRadius { get; set; } = 10;

        public float ShadowRadius { get; set; } = 3;

        public float BorderWidth { get; set; } = 10;

        public Color ShadowColor { get; set; } = Color.FromRgba(0, 0, 0, 0.7f);

        public Color BorderColor { get; set; } = Color.FromRgba(0, 0, 0, 0.7f);

        public RoundedLayoutEffect() 
            : base(ConstantsForms.EffectIds.RoundedLayoutEffectId) { }
    }
}