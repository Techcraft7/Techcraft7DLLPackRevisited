using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Advanced;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Techcraft7OpenTKWrappers
{
	public static class ImageUtil
	{
		public static byte[] GetImageBytes(string path)
		{
			Image<Rgba32> image = Image.Load(path) as Image<Rgba32>;
			image.Mutate(x => x.Flip(FlipMode.Vertical));
			Span<Rgba32> tempPixels = null;
			_ = image.TryGetSinglePixelSpan(out tempPixels);
			List<byte> pixels = new List<byte>();

			foreach (Rgba32 p in tempPixels.ToArray())
			{
				pixels.Add(p.R);
				pixels.Add(p.G);
				pixels.Add(p.B);
				pixels.Add(p.A);
			}

			return pixels.ToArray();
		}
	}
}
