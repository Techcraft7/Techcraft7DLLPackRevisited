using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace Techcraft7_DLL_Pack.GraphicsEnginesAndUtils._3D
{
    public class Easy3DEngine
    {
        public Graphics g;
        public Panel Display;
        public Transform3D CameraTransfrom;
        public Action OnFrame;
        public List<Shape3D> Models;

        public Easy3DEngine(Panel panel)
        {
            Display = panel;
            g = Display.CreateGraphics();
        }

        public void Draw()
        {
            g.Clear(Color.Black);

            OnFrame.Invoke();
        }
    }
}
