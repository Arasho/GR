using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace _3D_Madness
{
    /// <summary>
    ///
    /// </summary>
    public class Board : Microsoft.Xna.Framework.DrawableGameComponent
    {

        public List<Element> elements { get; set; }       //Board

        XML_Parser rand_element;

        const int sizeX = 20;
        const int sizeY = 20;

        public List<Element> element = new List<Element>();

        public Element[][] janek;

        public VertexPositionTexture[][][] x { get; set; }

        public VertexPositionTexture[] x1 { get; set; }

        public VertexPositionTexture[] x2 { get; set; }

        public VertexPositionTexture[] x3 { get; set; }

        public VertexPositionTexture[] x4 { get; set; }

        public BasicEffect Effect { get; set; }

        public Game mainGameClass { get; set; }

        public Texture2D NextBlock { get; set; }

        private int size = 1;
        private Texture2D txt1;
        private Texture2D txt2;
        private int textureIndex = 0;
        private SpriteBatch spriteBatch;
        Random rand;

        public Board(Game g, Texture2D _txt1, Texture2D _txt2)
            : base(g)
        {
            mainGameClass = g;
            Effect = new BasicEffect(g.GraphicsDevice);
            rand_element = new XML_Parser();
            elements = rand_element.XDocParse();
            spriteBatch = new SpriteBatch(g.GraphicsDevice);

            for (int i = 0; i < 72; i++)
            {
                elements[i].Texture = (mainGameClass.Content.Load<Texture2D>(@"Blocks\" + elements[i].FileName));
            }
            txt1 = _txt1;
            txt2 = _txt2;

            janek = new Element[20][];

            x = new VertexPositionTexture[sizeX][][];
            for (int i = 0; i < sizeX; i++)
            {
                x[i] = new VertexPositionTexture[sizeY][];
                janek[i] = new Element[20];

                for (int j = 0; j < sizeY; j++)
                {
                    x[i][j] = new VertexPositionTexture[4];
                }
            }
            GenerateBoard();
            this.janek[10][10].Texture = elements[0].Texture;
            janek[10][10].leftEdge = elements[textureIndex].leftEdge;
            janek[10][10].rightEdge = elements[textureIndex].rightEdge;
            janek[10][10].upEdge = elements[textureIndex].upEdge;
            janek[10][10].bottomEdge = elements[textureIndex].bottomEdge;
            janek[10][10].additional = elements[textureIndex].additional;
            elements.RemoveAt(textureIndex);
            rand = new Random();
            textureIndex = rand.Next(0, elements.Count);
            NextBlock = elements[textureIndex].Texture;
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

                    janek[i][j] = new Element(x[i][j], txt1);
                }
            }
        }

        private bool CheckBounds(int x, int y, int textInd)
        {
            Element next = elements[textInd];
            if (x > 1 && x < 19 && y > 1 && y < 19)
            {
                if (janek[x - 1][y].Texture == txt1 && janek[x + 1][y].Texture == txt1 && janek[x][y - 1].Texture == txt1 && janek[x][y + 1].Texture == txt1)
                    return false;
                if ((next.leftEdge == janek[x - 1][y].rightEdge || janek[x - 1][y].Texture == txt1 || (next.leftEdge == janek[x - 1][y].rightEdge + 1) || (next.leftEdge == janek[x - 1][y].rightEdge - 1)) &&
                    (next.rightEdge == janek[x + 1][y].leftEdge || janek[x + 1][y].Texture == txt1 || next.rightEdge == janek[x + 1][y].leftEdge +1 || next.rightEdge == janek[x + 1][y].leftEdge -1) &&
                    (next.upEdge == janek[x][y + 1].bottomEdge || janek[x][y + 1].Texture == txt1 || next.upEdge == janek[x][y + 1].bottomEdge+1 || next.upEdge == janek[x][y + 1].bottomEdge-1) &&
                    (next.bottomEdge == janek[x][y - 1].upEdge || janek[x][y - 1].Texture == txt1 || next.bottomEdge == janek[x][y - 1].upEdge+1 || next.bottomEdge == janek[x][y - 1].upEdge-1))
                    return true;
                else
                    return false;

            }
            else 

            return true;    
        }

        public void MapMouseAndRandNewBlock(GraphicsDevice g, BasicEffect effect, Camera camera)
        {
            Vector3 nearSource = new Vector3(Mouse.GetState().X, Mouse.GetState().Y, 0f);
            Vector3 farSource = new Vector3(Mouse.GetState().X, Mouse.GetState().Y, 1f);
            Vector3 nearPoint = g.Viewport.Unproject(nearSource, camera.projection, camera.view, Matrix.Identity);
            Vector3 farPoint = g.Viewport.Unproject(farSource, camera.projection, camera.view, Matrix.Identity);
            Vector3 direction = farPoint - nearPoint;
            direction.Normalize();


            Ray xRay = new Ray(nearPoint, direction);

            for (int i = 0; i < sizeX; i++)
            {
                for (int j = 0; j < sizeY; j++)
                {
                    if (this.janek[i][j].Texture == txt1)
                    {
                        if (xRay.Intersects(new BoundingBox(new Vector3((float)i, (float)j, 0), new Vector3((float)i + 1, (float)j + 1, 0))) > 0f)
                        {
                            //Console.WriteLine("x: " + i + "     y " + j);
                            //Console.WriteLine("WIERZCHOLKI");
                            //for (int w = 0; w < 4; w++)
                            //{
                            //    Console.WriteLine(string.Format("X{0} : {1}, Y{0} : {2}",w, janek[i][j].verts[w].Position.X, janek[i][j].verts[w].Position.Y));
                            //}
                            if (elements.Count >= 1 && CheckBounds(i, j, textureIndex))
                            {
                                this.janek[i][j].Texture = elements[textureIndex].Texture;
                                janek[i][j].leftEdge = elements[textureIndex].leftEdge;
                                janek[i][j].rightEdge = elements[textureIndex].rightEdge;
                                janek[i][j].upEdge = elements[textureIndex].upEdge;
                                janek[i][j].bottomEdge = elements[textureIndex].bottomEdge;
                                janek[i][j].additional = elements[textureIndex].additional;
                                elements.RemoveAt(textureIndex);
                                textureIndex = rand.Next(0, elements.Count);
                                NextBlock = elements[textureIndex].Texture;

                            }

                        }
                    }
                }
            }

            // patrz dokladnie ale to akurat mankament nie o to chodzi ale odrazu poka¿e
            //Window.Title = xRay.Intersects(new BoundingSphere(new Vector3(0,0,0), 1f)).ToString();
        }

        public override void Draw(GameTime gameTime)
        {

            foreach (EffectPass pass in Effect.CurrentTechnique.Passes)
            {
                for (int i = 0; i < 20; i++)
                {
                    for (int j = 0; j < 20; j++)
                    {
                        Effect.Texture = janek[i][j].Texture;
                        pass.Apply();

                        GraphicsDevice.SamplerStates[0] = SamplerState.LinearClamp;
                        GraphicsDevice.DrawUserPrimitives<VertexPositionTexture>
                       (PrimitiveType.TriangleStrip, janek[i][j].verts, 0, 2);
                    }
                }
            }
            spriteBatch.Begin();
            if (elements.Count >= 1)
                spriteBatch.Draw(elements[textureIndex].Texture, new Rectangle(0, 0, 150, 150), Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}