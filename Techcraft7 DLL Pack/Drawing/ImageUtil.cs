using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Techcraft7_DLL_Pack.Drawing
{
    public static class ImageUtil
    {
		/// <summary>
		/// Makes a color darker by dividing RGB values by <paramref name="divisor"/>
		/// </summary>
		/// <param name="c"></param>
		/// <param name="divisor"></param>
		/// <returns></returns>
		public static Color Darken(Color c, int divisor)
		{
			return Color.FromArgb(c.A, c.R / divisor, c.G / divisor, c.B / divisor);
		}

        /// <summary>
        /// Tints a Bitmap by Multiplying all its pixels by a color
        /// </summary>
        /// <param name="img">The image to tint</param>
        /// <param name="c">The color to tint it by</param>
        /// <returns></returns>
        internal static Bitmap Tint(Bitmap img, Color c)
        {
            for (int x = 0; x < img.Width; x++)
            {
                for (int y = 0; y < img.Height; y++)
                {
                    if (img.GetPixel(x, y).A != 0)
                    {
                        Color p = img.GetPixel(x, y);
                        const double _255 = 255.0;
                        double r = p.R / _255;
                        double g = p.G / _255;
                        double b = p.B / _255;
                        double r2 = c.R / _255;
                        double g2 = c.G / _255;
                        double b2 = c.B / _255;
                        img.SetPixel(
                            x, y,
                            Color.FromArgb(
                                255,
                                r * r2 > 1 ? 255 : Convert.ToInt32(r * r2 * 255),
                                g * g2 > 1 ? 255 : Convert.ToInt32(g * g2 * 255),
                                b * b2 > 1 ? 255 : Convert.ToInt32(b * b2 * 255)
                                )
                        );
                    }
                }
            }
            return img;
        }
    }
}
