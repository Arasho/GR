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

        public Element[][] _board;

        public VertexPositionTexture[][][] x { get; set; }

        public VertexPositionTexture[] x1 { get; set; }

        public VertexPositionTexture[] x2 { get; set; }

        public VertexPositionTexture[] x3 { get; set; }

        public VertexPositionTexture[] x4 { get; set; }

        public BasicEffect Effect { get; set; }

        public Game1 mainGameClass { get; set; }

        public Texture2D NextBlock { get; set; }

        private int size = 1;
        public Texture2D txt1 { get; set; }
        private Texture2D txt2;
        private int textureIndex = 0;
        private SpriteBatch spriteBatch;
        private Rectangle wholeBar;

        private int numberOfRotation = 0;
        private int tempRotation;


        Random rand;

        public Board(Game g, Texture2D _txt1, Texture2D _txt2)
            : base(g)
        {
            mainGameClass = (Game1)g;
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

            _board = new Element[20][];

            x = new VertexPositionTexture[sizeX][][];
            for (int i = 0; i < sizeX; i++)
            {
                x[i] = new VertexPositionTexture[sizeY][];
                _board[i] = new Element[20];

                for (int j = 0; j < sizeY; j++)
                {
                    x[i][j] = new VertexPositionTexture[4];
                }
            }
            GenerateBoard();
            this._board[10][10].Texture = elements[0].Texture;
            _board[10][10].leftEdge = elements[textureIndex].leftEdge;
            _board[10][10].rightEdge = elements[textureIndex].rightEdge;
            _board[10][10].upEdge = elements[textureIndex].upEdge;
            _board[10][10].bottomEdge = elements[textureIndex].bottomEdge;
            _board[10][10].additional = elements[textureIndex].additional;
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
                    x[i][j][0] = new VertexPositionTexture(new Vector3(i, j + size, 0), new Vector2(0, 0));
                    x[i][j][1] = new VertexPositionTexture(new Vector3(i + size, j + size, 0), new Vector2(1, 0));
                    x[i][j][2] = new VertexPositionTexture(new Vector3(i, j, 0), new Vector2(0, 1));
                    x[i][j][3] = new VertexPositionTexture(new Vector3(i + size, j, 0), new Vector2(1, 1));


                    _board[i][j] = new Element(x[i][j], txt1);
                }
            }
        }

        private bool CheckBounds(int x, int y, int textInd)
        {
            Element next = elements[textInd];
            if (x > 1 && x < 19 && y > 1 && y < 19)
            {
                if (_board[x - 1][y].Texture == txt1 && _board[x + 1][y].Texture == txt1 && _board[x][y - 1].Texture == txt1 && _board[x][y + 1].Texture == txt1)
                    return false;
                if ((next.leftEdge == _board[x - 1][y].rightEdge || _board[x - 1][y].Texture == txt1 || (next.leftEdge == _board[x - 1][y].rightEdge + 1) || (next.leftEdge == _board[x - 1][y].rightEdge - 1)) &&
                    (next.rightEdge == _board[x + 1][y].leftEdge || _board[x + 1][y].Texture == txt1 || next.rightEdge == _board[x + 1][y].leftEdge +1 || next.rightEdge == _board[x + 1][y].leftEdge -1) &&
                    (next.upEdge == _board[x][y + 1].bottomEdge || _board[x][y + 1].Texture == txt1 || next.upEdge == _board[x][y + 1].bottomEdge+1 || next.upEdge == _board[x][y + 1].bottomEdge-1) &&
                    (next.bottomEdge == _board[x][y - 1].upEdge || _board[x][y - 1].Texture == txt1 || next.bottomEdge == _board[x][y - 1].upEdge+1 || next.bottomEdge == _board[x][y - 1].upEdge-1))
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
            if (!wholeBar.Contains(Mouse.GetState().X,Mouse.GetState().Y))
            {
                for (int i = 0; i < sizeX; i++)
                {
                    for (int j = 0; j < sizeY; j++)
                    {
                    if (this._board[i][j].Texture == txt1)
                        {
                            if (xRay.Intersects(new BoundingBox(new Vector3((float)i, (float)j, 0), new Vector3((float)i + 1, (float)j + 1, 0))) > 0f)
                            {
                                //Console.WriteLine("x: " + i + "     y " + j);
                                //Console.WriteLine("WIERZCHOLKI");
                                //for (int w = 0; w < 4; w++)
                                //{
                                //    Console.WriteLine(string.Format("X{0} : {1}, Y{0} : {2}",w, janek[i][j].verts[w].Position.X, janek[i][j].verts[w].Position.Y));
                                //}
                                if (elements.Count >= 1)
                                {
                                if (CheckBounds(i, j, textureIndex))
                                {
                                    this._board[i][j].Texture = elements[textureIndex].Texture;
                                    _board[i][j].leftEdge = elements[textureIndex].leftEdge;
                                    _board[i][j].rightEdge = elements[textureIndex].rightEdge;
                                    _board[i][j].upEdge = elements[textureIndex].upEdge;
                                    _board[i][j].bottomEdge = elements[textureIndex].bottomEdge;
                                    _board[i][j].additional = elements[textureIndex].additional;

                                    // ROTACJA TEKSTURY KLOCKA RYSOWANEGO NA PLANSZY 
                                    if(numberOfRotation % 4 == 1)
                                    {
                                        _board[i][j].verts[0] = new VertexPositionTexture(new Vector3(i, j + size, 0), new Vector2(1, 0));
                                        _board[i][j].verts[1] = new VertexPositionTexture(new Vector3(i + size, j + size, 0), new Vector2(1, 1));
                                        _board[i][j].verts[2] = new VertexPositionTexture(new Vector3(i, j, 0), new Vector2(0, 0));
                                        _board[i][j].verts[3] = new VertexPositionTexture(new Vector3(i + size, j, 0), new Vector2(0, 1));
                                    }
                                    
                                    if (numberOfRotation % 4 == 2)
                                    {
                                        _board[i][j].verts[0] = new VertexPositionTexture(new Vector3(i, j + size, 0), new Vector2(1, 1));
                                        _board[i][j].verts[1] = new VertexPositionTexture(new Vector3(i + size, j + size, 0), new Vector2(0, 1));
                                        _board[i][j].verts[2] = new VertexPositionTexture(new Vector3(i, j, 0), new Vector2(1, 0));
                                        _board[i][j].verts[3] = new VertexPositionTexture(new Vector3(i + size, j, 0), new Vector2(0, 0));
                                    }
                                    if (numberOfRotation % 4 == 3)
                                    {
                                        _board[i][j].verts[0] = new VertexPositionTexture(new Vector3(i, j + size, 0), new Vector2(0, 1));
                                        _board[i][j].verts[1] = new VertexPositionTexture(new Vector3(i + size, j + size, 0), new Vector2(0, 0));
                                        _board[i][j].verts[2] = new VertexPositionTexture(new Vector3(i, j, 0), new Vector2(1, 1));
                                        _board[i][j].verts[3] = new VertexPositionTexture(new Vector3(i + size, j, 0), new Vector2(1, 0));
                                    }

                                    elements.RemoveAt(textureIndex);
                                    textureIndex = rand.Next(0, elements.Count);
                                    NextBlock = elements[textureIndex].Texture;
                                    numberOfRotation = 0;
                                }
                            }
                            else
                            {
                                NextBlock = txt1;
                                }

                            }
                        }
                    }
                }
            }
            // patrz dokladnie ale to akurat mankament nie o to chodzi ale odrazu poka¿e
            //Window.Title = xRay.Intersects(new BoundingSphere(new Vector3(0,0,0), 1f)).ToString();
        }

        public override void Update(GameTime gameTime)
        {
            if(mainGameClass.graphics.IsFullScreen)
                wholeBar = new Rectangle(0, 0, (int)(mainGameClass.GraphicsDevice.Viewport.Width * 0.14f), mainGameClass.GraphicsDevice.Viewport.Height);
            else
                wholeBar = new Rectangle(0, 0, (int)(mainGameClass.GraphicsDevice.Viewport.Width * 0.22f), mainGameClass.GraphicsDevice.Viewport.Height);
            base.Update(gameTime);
        }
        
        public void RotationBlock()
        {
            numberOfRotation++;
            tempRotation = elements[textureIndex].upEdge;
            elements[textureIndex].upEdge = elements[textureIndex].rightEdge;
            elements[textureIndex].rightEdge = elements[textureIndex].bottomEdge;
            elements[textureIndex].bottomEdge = elements[textureIndex].leftEdge;
            elements[textureIndex].leftEdge = tempRotation; 
         }



        public override void Draw(GameTime gameTime)
        {
            
            foreach (EffectPass pass in Effect.CurrentTechnique.Passes)
            {
                
                for (int i = 0; i < 20; i++)
                {
                    for (int j = 0; j < 20; j++)
                    {
                        Effect.Texture = _board[i][j].Texture;
                        pass.Apply();

                        GraphicsDevice.SamplerStates[0] = SamplerState.LinearClamp;
                        GraphicsDevice.DrawUserPrimitives<VertexPositionTexture>
                       (PrimitiveType.TriangleStrip, _board[i][j].verts, 0, 2);
                    }
                }
            }

            spriteBatch.Begin();
            if (elements.Count >= 1)
                spriteBatch.Draw(elements[textureIndex].Texture, new Rectangle(100, 100, 50, 50),null, Color.White,numberOfRotation * -90 * (MathHelper.Pi/180), Vector2.Zero,SpriteEffects.None,0);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}