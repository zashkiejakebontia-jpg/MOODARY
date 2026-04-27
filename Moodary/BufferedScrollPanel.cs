using System.Windows.Forms;

namespace Moodary
{
    public class BufferedScrollPanel : Panel
    {
        public BufferedScrollPanel()
        {
            DoubleBuffered = true;
            ResizeRedraw = true;
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            UpdateStyles();
        }

        protected override void OnScroll(ScrollEventArgs se)
        {
            base.OnScroll(se);
            Invalidate(true);
            Update();
        }
    }
}
