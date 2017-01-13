using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ToolkitXam
{
    public class DelayedScaleAction : ScaleAction
    {
        public DelayedScaleAction() : base()
        {
            // Set defaults.
            Delay = 0;
        }

        public int Delay { set; get; }

        async protected override void Invoke(VisualElement visual)
        {
            visual.AnchorX = Anchor.X;
            visual.AnchorY = Anchor.Y;
            await Task.Delay(Delay);
            await visual.ScaleTo(Scale, (uint)Length, Easing);
        }
    }
}
