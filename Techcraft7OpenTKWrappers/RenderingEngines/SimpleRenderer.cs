using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace Techcraft7OpenTKWrappers.RenderingEngines
{
	public class SimpleRenderer : AbstractRenderer
	{
		public override void DrawPrimitive(PrimitiveType type, Tuple<Vector3, Color4>[] verticies, float z = 5)
		{
			GL.Begin(type);
			foreach (var t in verticies)
			{
				GL.Color3(t.Item2.R, t.Item2.G, t.Item2.B);
				GL.Vertex3(t.Item1 + new Vector3(0, 0, z));
			}
			GL.End();
		}

		public override void DrawPrimitive(PrimitiveType type, Tuple<Vector3, Vector2>[] textureCoords, int texture, float z = 5)
		{
			GL.BindTexture(TextureTarget.Texture2D, texture);
		}
	}
}
