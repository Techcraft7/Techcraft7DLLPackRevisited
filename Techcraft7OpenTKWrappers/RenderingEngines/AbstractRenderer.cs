using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Techcraft7OpenTKWrappers.RenderingEngines
{
	public abstract class AbstractRenderer
	{
		public abstract void DrawPrimitive(PrimitiveType type, Tuple<Vector3, Color4>[] verticies, float z = 5);
		public abstract void DrawPrimitive(PrimitiveType type, Tuple<Vector3, Vector2>[] textureCoords, int texture, float z = 5);

		public void DrawRectangle(RectangleF rect, Color4 color, float z = 5)
		{
			DrawPrimitive(PrimitiveType.Quads, new Tuple<Vector3, Color4>[]
			{
				new Tuple<Vector3, Color4>(new Vector3(rect.X, rect.Y, 0), color),
				new Tuple<Vector3, Color4>(new Vector3(rect.X + rect.Width, rect.Y, 0), color),
				new Tuple<Vector3, Color4>(new Vector3(rect.X + rect.Width, rect.Y + rect.Height, 0), color),
				new Tuple<Vector3, Color4>(new Vector3(rect.X, rect.Y + rect.Height, 0), color)
			}, z);
		}
	}
}
