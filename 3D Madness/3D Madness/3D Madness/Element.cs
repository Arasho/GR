using Microsoft.Xna.Framework.Graphics;

namespace _3D_Madness
{
    /// <summary>
    ///  
    /// </summary>
    public class Element
    {
        #region Properties

        public VertexPositionTexture[] verts { get; set; }

        public Texture2D Texture { get; set; }

        #endregion Properties

        #region Constructors

        public Element(VertexPositionTexture[] x, Texture2D texture)
        {
            verts = x;
            Texture = texture;
        }

        #endregion Constructors
    }
}