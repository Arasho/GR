using Microsoft.Xna.Framework.Graphics;

namespace _3D_Madness
{
    public class Element
    {
        public VertexPositionTexture[] verts { get; set; }
        public Texture2D Texture { get; set; }

        public Element(VertexPositionTexture[] x, Texture2D texture)
        {
            verts = x;
            Texture = texture;
        }  
    }
}