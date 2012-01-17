using System;
using System.Collections.Generic;
using System.Windows.Forms;
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
        public Element[][] _stones;

        public List<Element> element1 = new List<Element>();

        float test = 0f;

        public VertexPositionTexture[][][] x { get; set; }

        public VertexPositionTexture[] x1 { get; set; }

        public VertexPositionTexture[] x2 { get; set; }

        public VertexPositionTexture[] x3 { get; set; }

        public VertexPositionTexture[] x4 { get; set; }

        public BasicEffect Effect { get; set; }
        public BasicEffect Effect1 { get; set; }

        public Game1 mainGameClass { get; set; }

        public Texture2D NextBlock { get; set; }

        private int size = 1;

        public List<Model3D> model = new List<Model3D>();

        public Texture2D txt1 { get; set; }

        private Texture2D txt2;
        private int textureIndex = 0;
        private SpriteBatch spriteBatch;

        public int numberOfRotation { get; set; }

        private int tempRotation;

       

        int X = 0;
        int Y = 0;

        Random rand;

        public Board(Game g, Texture2D _txt1, Texture2D _txt2)
            : base(g)
        {
            mainGameClass = (Game1)g;
            Effect = new BasicEffect(g.GraphicsDevice);
            Effect1 = new BasicEffect(g.GraphicsDevice);

            Effect1 = Effect;
            rand_element = new XML_Parser();
            elements = rand_element.XDocParse();
            spriteBatch = new SpriteBatch(g.GraphicsDevice);
            numberOfRotation = 0;

            for (int i = 0; i < 72; i++)
            {
                elements[i].Texture = (mainGameClass.Content.Load<Texture2D>(@"Blocks\" + elements[i].FileName));
            }
            txt1 = _txt1;
            txt2 = _txt2;

            _board = new Element[20][];
            _stones = new Element[20][];

            x = new VertexPositionTexture[sizeX][][];
            for (int i = 0; i < sizeX; i++)
            {
                x[i] = new VertexPositionTexture[sizeY][];
                _board[i] = new Element[20];
                _stones[i] = new Element[20];

                for (int j = 0; j < sizeY; j++)
                {
                    x[i][j] = new VertexPositionTexture[4];
                }
            }
            GenerateBoard();
            SetUpVertices();

    
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

        public void SetUpVertices()
        {
            VertexPositionTexture[] vertices = new VertexPositionTexture[4];
            vertices[0] = new VertexPositionTexture(new Vector3(0 + (float)X + 0.3f, 0.3f + Y + 0.5f - 0.25f, 0), new Vector2(0, 0));
            vertices[1] = new VertexPositionTexture(new Vector3(0.3f + X + 0.3f, 0.3f + Y + 0.5f - 0.25f, 0), new Vector2(0.5f, 0));
            vertices[2] = new VertexPositionTexture(new Vector3(0 + X + 0.3f, 0 + Y + 0.5f - 0.25f, 0), new Vector2(0, 0.5f));
            vertices[3] = new VertexPositionTexture(new Vector3(0.3f + X + 0.3f, 0 + Y + 0.5f - 0.25f, 0), new Vector2(0.5f, 0.5f));
            element1.Add(new Element(vertices, txt2));
        }

        private bool CheckBounds(int x, int y, int textInd)
        {
            Element next = elements[textInd];
            if (x > 1 && x < 19 && y > 1 && y < 19)
            {
                if (_board[x - 1][y].Texture == txt1 && _board[x + 1][y].Texture == txt1 && _board[x][y - 1].Texture == txt1 && _board[x][y + 1].Texture == txt1)
                    return false;
                if ((next.leftEdge == _board[x - 1][y].rightEdge || _board[x - 1][y].Texture == txt1 || (next.leftEdge == _board[x - 1][y].rightEdge + 1) || (next.leftEdge == _board[x - 1][y].rightEdge - 1)) &&
                    (next.rightEdge == _board[x + 1][y].leftEdge || _board[x + 1][y].Texture == txt1 || next.rightEdge == _board[x + 1][y].leftEdge + 1 || next.rightEdge == _board[x + 1][y].leftEdge - 1) &&
                    (next.upEdge == _board[x][y + 1].bottomEdge || _board[x][y + 1].Texture == txt1 || next.upEdge == _board[x][y + 1].bottomEdge + 1 || next.upEdge == _board[x][y + 1].bottomEdge - 1) &&
                    (next.bottomEdge == _board[x][y - 1].upEdge || _board[x][y - 1].Texture == txt1 || next.bottomEdge == _board[x][y - 1].upEdge + 1 || next.bottomEdge == _board[x][y - 1].upEdge - 1))
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

            if (mainGameClass.CheckStone)
            {
              //  mainGameClass.CheckStone = false;
                if (xRay.Intersects(new BoundingBox(new Vector3((float)X, (float)Y + 0.25f, 0), new Vector3((float)X + 0.25f, (float)Y + 0.75f, 0))) > 0f)
                {
                    SetUpVertices();
                    mainGameClass.CanStone = false;
                    mainGameClass.CheckStone = false;

                    model.Add(new Model3D(mainGameClass, X - 0.25f, Y + 0.5f));

                    Game1.listOfPlayers[Round.NumberOfActivePlayer - 1].NumberOfLittlePowns--;
                    mainGameClass.Window.Title = "Lewa";
                    _board[X][Y].stoneLeftEdge = 1;
                    _board[X][Y].player = Round.NumberOfActivePlayer;

                    Round.NextTurn();
                }
                else if (xRay.Intersects(new BoundingBox(new Vector3((float)X + 0.75f, (float)Y + 0.25f, 0), new Vector3((float)X + 1, (float)Y + 0.75f, 0))) > 0f)
                {
                    mainGameClass.Window.Title = "Prawa";

                    model.Add(new Model3D(mainGameClass, X + 0.75f, Y + 0.5f));

                    Game1.listOfPlayers[Round.NumberOfActivePlayer - 1].NumberOfLittlePowns--;
                    _board[X][Y].stoneRightEdge = 1;
                    _board[X][Y].player = Round.NumberOfActivePlayer;
                    mainGameClass.CanStone = false;
                    mainGameClass.CheckStone = false;
                    Round.NextTurn();
                }

                else if (xRay.Intersects(new BoundingBox(new Vector3((float)X + 0.25f, (float)Y + 0.75f, 0), new Vector3((float)X + 0.75f, (float)Y + 1, 0))) > 0f)
                {
                    mainGameClass.Window.Title = "Gora";

                    model.Add(new Model3D(mainGameClass, X + 0.25f, Y + 1));

                    Game1.listOfPlayers[Round.NumberOfActivePlayer - 1].NumberOfLittlePowns--;
                    _board[X][Y].stoneUpEdge = 1;
                    _board[X][Y].player = Round.NumberOfActivePlayer;
                    mainGameClass.CanStone = false;
                    mainGameClass.CheckStone = false;
                    Round.NextTurn();
                }

                else if (xRay.Intersects(new BoundingBox(new Vector3((float)X + 0.25f, (float)Y, 0), new Vector3((float)X + 0.75f, (float)Y + 0.25f, 0))) > 0f)
                {
                    mainGameClass.Window.Title = "Dol";

                    model.Add(new Model3D(mainGameClass, X+ 0.25f, Y));

                    Game1.listOfPlayers[Round.NumberOfActivePlayer - 1].NumberOfLittlePowns--;
                    _board[X][Y].stoneBottomEdge = 1;
                    _board[X][Y].player = Round.NumberOfActivePlayer;
                    mainGameClass.CanStone = false;
                    mainGameClass.CheckStone = false;
                    Round.NextTurn();
                }

                else if (xRay.Intersects(new BoundingBox(new Vector3((float)X, (float)Y, 0), new Vector3((float)X + 1, (float)Y + 1, 0))) > 0f)
                {
                    MessageBox.Show("Nie klikaj w srodek ! Spróbuj położyc pionka bliżej którejś z krawędzi");
       
                }
                else
                {
                    MessageBox.Show("Chcesz postawic pionka i kliknales po za krawedzia ? ;p");
         
                }
            }
            else
            {
                if (!mainGameClass.infoBar.wholeBar.Contains(Mouse.GetState().X, Mouse.GetState().Y))
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
                                            if (mainGameClass.putElement == true) Round.NextTurn();
                                            mainGameClass.CanStone = false;
                                            this.X = i;
                                            this.Y = j;
                                            // SetUpVertices();

                                            if (this.X >= 10)
                                            {
                                                //float test1 = (1 / ((i - 9) * 0.09f));
                                                //float c = ((xRay.Direction.X - ((i - 9) * 0.09f)) * test1) + i;

                                                float test1 = (1 / ((i - 9) * (2f / Math.Abs(mainGameClass.camera.view.Translation.X))));
                                                float d = (xRay.Direction.X * test1 - ((i - 10) * (2f / Math.Abs(mainGameClass.camera.view.Translation.Z)))) + i;
                                                //  float d = (xRay.Direction.X * test1 - (2f / Math.Abs(mainGameClass.camera.view.Translation.Z))) + i;
                                                // float c = d * test1 + i;

                                                //float d = xRay.Direction.X + mainGameClass.worldTranslation.Translation.X;// +(i - 10) * 0.08f;

                                                //float roznica = (i - 10) * 0.085f;

                                                //if (d - roznica > (roznica + 0.085f) - d)
                                                //{
                                                //    _board[i][j].rEdge = 1;
                                                //}
                                                //else if (d - roznica < (roznica + 0.085f) - d)
                                                //{
                                                //    _board[i][j].lEdge = 1;
                                                //}

                                                ////if(_board[i][j].rEdge =
                                                //      mainGameClass.Window.Title = xRay.Direction.X.ToString();
                                            }

                                            else if (this.X < 10)
                                            {
                                                //float test1 = (1 / ((i-9) * 0.09f));
                                                //float c = ((xRay.Direction.X - ((i - 9) * 0.09f)) * test1) + i;

                                                //float test1 = (1 / (((i-9)) * (2f / Math.Abs(mainGameClass.camera.view.Translation.Z))));
                                                //float d = (xRay.Direction.X * test1 - (((i-10)) * (2f / Math.Abs(mainGameClass.camera.view.Translation.Z)))) - i;
                                                ////  float d = (xRay.Direction.X * test1 - (2f / Math.Abs(mainGameClass.camera.view.Translation.Z))) + i;
                                                //// float c = d * test1 + i;
                                                //  mainGameClass.Window.Title = xRay.Direction.X.ToString();

                                                //float d = Math.Abs(xRay.Direction.X);// +(i - 10) * 0.08f;

                                                //float roznica = (10 - i) * 0.085f;

                                                //float cc = d - roznica;
                                                //if (d + roznica > (roznica + 0.085f) - d)
                                                //{
                                                //    _board[i][j].rEdge = 1;
                                                //}
                                                //else if (d + roznica < (roznica + 0.085f) - d)
                                                //{
                                                //    _board[i][j].lEdge = 1;
                                                //}
                                                //   mainGameClass.Window.Title = xRay.Position.X.ToString();
                                            }
                                            else
                                            {
                                                mainGameClass.Window.Title = "puste";
                                            }

                                            // potem zm * xRay.Direction X - powinno przesunac do odpowiedniej wspolrzednej

                                            this._board[i][j].Texture = elements[textureIndex].Texture;
                                            _board[i][j].leftEdge = elements[textureIndex].leftEdge;
                                            _board[i][j].rightEdge = elements[textureIndex].rightEdge;
                                            _board[i][j].upEdge = elements[textureIndex].upEdge;
                                            _board[i][j].bottomEdge = elements[textureIndex].bottomEdge;
                                            _board[i][j].additional = elements[textureIndex].additional;

                                            // ROTACJA TEKSTURY KLOCKA RYSOWANEGO NA PLANSZY
                                            if (numberOfRotation % 4 == 1)
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

                                            mainGameClass.putElement = true;
                                            //mainGameClass.CheckStone = true;
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
                else
                {
                    for (int i = 0; i < mainGameClass.infoBar.stoneArea.Length; i++)
                    {
                        if (mainGameClass.infoBar.stoneArea[i].Contains(Mouse.GetState().X, Mouse.GetState().Y))
                        {
                            mainGameClass.CheckStone = false;
                            mainGameClass.infoBar.numberOfStone[i] -= 1;
                            break;
                        }
                    }
                }
            }
            // patrz dokladnie ale to akurat mankament nie o to chodzi ale odrazu poka¿e
            //Window.Title = xRay.Intersects(new BoundingSphere(new Vector3(0,0,0), 1f)).ToString();
        }

        public override void Update(GameTime gameTime)
        {
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

                foreach (var item in model)
                {
                    item.myModel.Draw(Matrix.CreateScale(2f) * Matrix.CreateRotationX(1.5f) * Matrix.CreateTranslation(item.modelPosition.X,item.modelPosition.Y,0) * Effect.World, Effect1.View, Effect1.Projection);
                }
            }

            mainGameClass.infoBar.Draw(gameTime);
            //spriteBatch.Begin();
            //if (elements.Count >= 1)
            //    spriteBatch.Draw(elements[textureIndex].Texture, new Rectangle(200, 200, 50, 50),null, Color.White,numberOfRotation * -90 * (MathHelper.Pi/180), Vector2.Zero,SpriteEffects.None,0);
            //spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}