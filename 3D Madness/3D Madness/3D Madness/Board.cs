using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _3D_Madness
{
    /// <summary>
    ///  
    /// </summary>
    public class Board
    {
        const int sizeX = 20;
        const int sizeY = 20;

        public List<Element> element = new List<Element>();
 
        public VertexPositionTexture[][][] x { get; set; }
        public VertexPositionTexture[] x1 { get; set; }
        public VertexPositionTexture[] x2 { get; set; }
        public VertexPositionTexture[] x3 { get; set; }
        public VertexPositionTexture[] x4 { get; set; }

        private int size = 1;
        private Texture2D txt1;
        private Texture2D txt2;

        public Board(Game g, Texture2D _txt1, Texture2D _txt2)
        {
            txt1 = _txt1;
            txt2 = _txt2;

            x = new VertexPositionTexture[sizeX][][];
            for (int i = 0; i < sizeX; i++)
            {
                x[i] = new VertexPositionTexture[sizeY][];

                for (int j = 0; j < sizeY; j++)
                {
                    x[i][j] = new VertexPositionTexture[4];
                }
            }
            GenerateBoard();
        }

        public void GenerateBoard()
        {
            for (int i = 0; i < sizeX; i++)
            {
                for (int j = 0; j < sizeY; j++)
                {
                    x[i][j][0] = new VertexPositionTexture(
                  new Vector3(i, j + size, 0), new Vector2(0, 0));
                    x[i][j][1] = new VertexPositionTexture(
                        new Vector3(i + size, j + size, 0), new Vector2(1, 0));
                    x[i][j][2] = new VertexPositionTexture(
                        new Vector3(i, j, 0), new Vector2(0, 1));
                    x[i][j][3] = new VertexPositionTexture(
                        new Vector3(i + size, j, 0), new Vector2(1, 1));

                    if (j % 2 == 0 || i % 2 == 0)
                    {
                        element.Add(new Element(x[i][j], txt1));
                    }
                    else
                    {
                        element.Add(new Element(x[i][j], txt1));
                    }
                }
            }
        }
    }
}