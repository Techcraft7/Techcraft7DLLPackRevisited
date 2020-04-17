using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Techcraft7_DLL_Pack.GraphicsEnginesAndUtils._3D
{
    public class Transform3D
    {
        public Point3D Location = new Point3D(0, 0, 0);
        public Rotation3D Rotation = new Rotation3D(0, 0, 0);

        public Transform3D(Point3D loc, Rotation3D rot)
        {
            Location = loc;
            Rotation = rot;
        }
    }
}
