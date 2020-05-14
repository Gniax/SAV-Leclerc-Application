using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Leclerc
{
    public static class Color
    {
        public static Task<bool> ColorTo(this VisualElement self, Xamarin.Forms.Color fromColor, Xamarin.Forms.Color toColor, Action<Xamarin.Forms.Color> callback, uint length = 250, Easing easing = null)
        {
            Func<double, Xamarin.Forms.Color> transform = (t) =>
              Xamarin.Forms.Color.FromRgba(fromColor.R + t * (toColor.R - fromColor.R),
                             fromColor.G + t * (toColor.G - fromColor.G),
                             fromColor.B + t * (toColor.B - fromColor.B),
                             fromColor.A + t * (toColor.A - fromColor.A));

            return ColorAnimation(self, "ColorTo", transform, callback, length, easing);
        }

        public static void CancelAnimation(this VisualElement self)
        {
            self.AbortAnimation("ColorTo");
        }

        static Task<bool> ColorAnimation(VisualElement element, string name, Func<double, Xamarin.Forms.Color> transform, Action<Xamarin.Forms.Color> callback, uint length, Easing easing)
        {
            easing = easing ?? Easing.Linear;
            var taskCompletionSource = new TaskCompletionSource<bool>();

            element.Animate(name, transform, callback, 16, length, easing, (v, c) => taskCompletionSource.SetResult(c));

            return taskCompletionSource.Task;
        }
    }
}
