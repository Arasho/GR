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
        List<Texture2D> blocks = new List<Texture2D>();

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

        private int size = 1;
        private Texture2D txt1;
        private Texture2D txt2;
        private int textureIndex = 0;
        private SpriteBatch spriteBatch;

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
                blocks.Add(mainGameClass.Content.Load<Texture2D>(@"Blocks\" + elements[i].FileName));
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
        }

        public void GenerateBoard()
        {
            for (int i = 0; i < sizeX; i++)
            {
                for (int j = 0; j < sizeY; j++)
                {
                    x[i][j][0] = new VertexPositionTexture(
                  new Vector3(i, j + size, 0), new Vector2(0, 1));
                    x[i][j][1] = new VertexPositionTexture(
                        new Vector3(i + size, j + size, 0), new Vector2(1, 1));
                    x[i][j][2] = new VertexPositionTexture(
                        new Vector3(i, j, 0), new Vector2(0, 0));
                    x[i][j][3] = new VertexPositionTexture(
                        new Vector3(i + size, j, 0), new Vector2(1, 0));

                    janek[i][j] = new Element(x[i][j], txt1);
                }
            }
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
            Random rand = new Random();

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
                            this.janek[i][j].Texture = blocks[textureIndex];
                            blocks.RemoveAt(textureIndex);
                            textureIndex = rand.Next(0, blocks.Count);

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
            spriteBatch.Draw(blocks[textureIndex], new Rectangle(0, 0, 200, 200), Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}