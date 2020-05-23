using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Advanced;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Techcraft7OpenTKWrappers.RenderingEngines;

namespace T7OTKTests_1
{
	internal class Game : GameWindow
	{
		SimpleRenderer renderer;

		int texture = -1;

		public Game() : base(800, 600, GraphicsMode.Default, "Techcraft7OpenTKWrappers Tests #1")
		{
			renderer = new SimpleRenderer();
		}

		protected override void OnUpdateFrame(FrameEventArgs e)
		{
			base.OnUpdateFrame(e);
		}

		protected override void OnLoad(EventArgs e)
		{
			GL.ClearColor(0.2f, 0.3f, 0.3f, 0.3f);

			base.OnLoad(e);
		}

		protected override void OnResize(EventArgs e)
		{
			GL.Viewport(0, 0, Width, Height);
			Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView((float)Math.PI / 4, Width / (float)Height, 1.0f, 64.0f);
			GL.MatrixMode(MatrixMode.Projection);
			GL.LoadMatrix(ref projection);
			base.OnResize(e);
		}

		protected override void OnRenderFrame(FrameEventArgs e)
		{
			GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
			Matrix4 modelview = Matrix4.LookAt(Vector3.Zero, new Vector3(0, 0, 1), Vector3.UnitY);
			GL.MatrixMode(MatrixMode.Modelview);
			GL.LoadMatrix(ref modelview);

			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

			renderer.DrawPrimitive(PrimitiveType.Triangles, new Tuple<Vector3, Vector2>[]
			{
				new Tuple<Vector3, Vector2>(new Vector3(-0.5f, -0.5f, 0), new Vector2(1, 0)),
				new Tuple<Vector3, Vector2>(new Vector3(0.5f, -0.5f, 0), new Vector2(0, 0)),
				new Tuple<Vector3, Vector2>(new Vector3(0, 0.5f, 0), new Vector2(0.5f, 1))
			}, texture);


			Context.SwapBuffers();
			base.OnRenderFrame(e);
		}
	}
}
