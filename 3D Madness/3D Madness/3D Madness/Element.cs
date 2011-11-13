using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace _3D_Madness
{
    public class Element
    {
        public VertexPositionTexture[] verts { get; set; }

        public Element(VertexPositionTexture [] x)
        {
            verts = x; 
        }
    }
}
