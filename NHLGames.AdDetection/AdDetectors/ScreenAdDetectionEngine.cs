using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using NHLGames.AdDetection.Properties;
using XnaFan.ImageComparison;

namespace NHLGames.AdDetection.AdDetectors
{
    internal class ScreenAdDetectionEngine : AdDetectionEngineBase
    {
        private readonly Bitmap _inProgressImage;

        private Screen _screenPlayingAd;

        public ScreenAdDetectionEngine()
        {
            _inProgressImage = Resources.CommercialBreakInProgress;
        }

        protected override int PollPeriodMilliseconds => 500;

        private static Bitmap CaptureScreen(Screen screen)
        {
            var ssInfo = new ScreenShotInfo(screen);

            //Create a new bitmap.
            var bmpScreenshot = new Bitmap(ssInfo.Width,
                ssInfo.Height,
                PixelFormat.Format32bppArgb);

            // Create a graphics object from the bitmap.
            using (var gfxScreenshot = Graphics.FromImage(bmpScreenshot))
            {
                // Take the screenshot from the upper left corner to the right bottom corner.
                gfxScreenshot.CopyFromScreen(screen.Bounds.X + ssInfo.XOffSet,
                    screen.Bounds.Y + ssInfo.YOffSet,
                    0,
                    0,
                    new Size(ssInfo.Width, ssInfo.Height),
                    CopyPixelOperation.SourceCopy);
            }

            return bmpScreenshot;
        }

        protected override bool IsAdCurrentlyPlaying()
        {
            foreach (var screen in Screen.AllScreens)
            {
                if (IsAdPlaying() && !IsAdPlaying(screen))
                {
                    continue;
                }

                using (var currentScreen = CaptureScreen(screen))
                {
                    var difference = currentScreen.PercentageDifference(_inProgressImage);

                    if (difference < 0.70)
                    {
                        _screenPlayingAd = screen;
                        return true;
                    }
                    _screenPlayingAd = null;
                    return false;
                }
            }

            return false;
        }

        private bool IsAdPlaying()
        {
            return _screenPlayingAd != null;
        }

        private bool IsAdPlaying(Screen screen)
        {
            return IsAdPlaying() && _screenPlayingAd.Equals(screen);
        }
    }
}