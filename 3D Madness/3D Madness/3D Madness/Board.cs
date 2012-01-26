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

        public VertexPositionTexture[][][] x { get; set; }

        public VertexPositionTexture[] x1 { get; set; }

        public VertexPositionTexture[] x2 { get; set; }

        public VertexPositionTexture[] x3 { get; set; }

        public VertexPositionTexture[] x4 { get; set; }

        public BasicEffect Effect { get; set; }

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

        public int X = 0;
        public int Y = 0;

        Random rand;

        public Board(Game g, Texture2D _txt1, Texture2D _txt2)
            : base(g)
        {
            mainGameClass = (Game1)g;
            Effect = new BasicEffect(g.GraphicsDevice);

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
                    if (CanIPutStone(this.X, this.Y, 0))
                    {
                        int punkty = FloodFill(new Point(this.X, this.Y), _board[this.X][this.Y].leftEdge);

                        //System.Diagnostics.Debug.WriteLine("To nasze punkty: " + punkty.ToString());

                        if (CheckIfModel(new Point(this.X, this.Y), _board[this.X][this.Y].leftEdge) == false)
                        {
                            model.Add(new Model3D(mainGameClass, X , Y + 0.5f, Game1.listOfPlayers[Round.NumberOfActivePlayer - 1].PlayerColor));
                            Game1.listOfPlayers[Round.NumberOfActivePlayer - 1].NumberOfLittlePowns--;
                            _board[X][Y].stoneLeftEdge = 1;
                            Round.PuttingPowl();
                            _board[X][Y].player = Round.NumberOfActivePlayer;
                        }
                        else
                        {
                            MessageBox.Show("Na tej krawędzi już stoi pionek gracza " + Game1.listOfPlayers[Round.NumberOfActivePlayer - 1].PlayerName);
                        }

                        mainGameClass.CanStone = false;
                        mainGameClass.CheckStone = false;
                        // Round.NextTurn();
                    }
                    else
                    {
                        MessageBox.Show("Na tej krawędzi już stoi pionek gracza " + Game1.listOfPlayers[Round.NumberOfActivePlayer - 1].PlayerName);
                    }
                }
                else if (xRay.Intersects(new BoundingBox(new Vector3((float)X + 0.75f, (float)Y + 0.25f, 0), new Vector3((float)X + 1, (float)Y + 0.75f, 0))) > 0f)
                {
                    if (CanIPutStone(this.X, this.Y, 2))
                    {
                        int punkty = FloodFill(new Point(this.X, this.Y), _board[this.X][this.Y].rightEdge);
                        //System.Diagnostics.Debug.WriteLine("To nasze punkty: " + punkty.ToString());
                        if (CheckIfModel(new Point(this.X, this.Y), _board[this.X][this.Y].rightEdge) == false)
                        {
                            model.Add(new Model3D(mainGameClass, X + 0.55f, Y + 0.5f, Game1.listOfPlayers[Round.NumberOfActivePlayer - 1].PlayerColor));
                            Game1.listOfPlayers[Round.NumberOfActivePlayer - 1].NumberOfLittlePowns--;
                            _board[X][Y].stoneRightEdge = 1;
                            Round.PuttingPowl();
                            _board[X][Y].player = Round.NumberOfActivePlayer;
                        }
                        else
                        {
                            MessageBox.Show("Na tej krawędzi już stoi pionek gracza " + Game1.listOfPlayers[Round.NumberOfActivePlayer - 1].PlayerName);
                        }

                        mainGameClass.CanStone = false;
                        mainGameClass.CheckStone = false;
                        //Round.NextTurn();
                    }
                    else
                    {
                        MessageBox.Show("Na tej krawędzi już stoi pionek gracza " + Game1.listOfPlayers[Round.NumberOfActivePlayer - 1].PlayerName);
                    }
                }

                else if (xRay.Intersects(new BoundingBox(new Vector3((float)X + 0.25f, (float)Y + 0.75f, 0), new Vector3((float)X + 0.75f, (float)Y + 1, 0))) > 0f)
                {
                    if (CanIPutStone(this.X, this.Y, 1))
                    {
                        int punkty = FloodFill(new Point(this.X, this.Y), _board[this.X][this.Y].upEdge);
                        System.Diagnostics.Debug.WriteLine("To nasze punkty: " + punkty.ToString());
                        if (CheckIfModel(new Point(this.X, this.Y), _board[this.X][this.Y].upEdge) == false)
                        {
                            model.Add(new Model3D(mainGameClass, X + 0.30f, Y + 0.9f, Game1.listOfPlayers[Round.NumberOfActivePlayer - 1].PlayerColor));
                            Game1.listOfPlayers[Round.NumberOfActivePlayer - 1].NumberOfLittlePowns--;

                            _board[X][Y].stoneUpEdge = 1;
                            Round.PuttingPowl();
                            _board[X][Y].player = Round.NumberOfActivePlayer;
                        }
                        else
                        {
                            MessageBox.Show("Na tej krawędzi już stoi pionek gracza " + Game1.listOfPlayers[Round.NumberOfActivePlayer - 1].PlayerName);
                        }

                        mainGameClass.CanStone = false;
                        mainGameClass.CheckStone = false;
                        // Round.NextTurn();
                    }
                    else
                    {
                        MessageBox.Show("Na tej krawędzi już stoi pionek gracza " + Game1.listOfPlayers[Round.NumberOfActivePlayer - 1].PlayerName);
                    }
                }

                else if (xRay.Intersects(new BoundingBox(new Vector3((float)X + 0.25f, (float)Y, 0), new Vector3((float)X + 0.75f, (float)Y + 0.25f, 0))) > 0f)
                {
                    if (CanIPutStone(this.X, this.Y, 3))
                    {
                        int punkty = FloodFill(new Point(this.X, this.Y), _board[this.X][this.Y].bottomEdge);
                        System.Diagnostics.Debug.WriteLine("To nasze punkty: " + punkty.ToString());
                        if (CheckIfModel(new Point(this.X, this.Y), _board[this.X][this.Y].bottomEdge) == false)
                        {
                            model.Add(new Model3D(mainGameClass, X + 0.30f, Y+0.2f, Game1.listOfPlayers[Round.NumberOfActivePlayer - 1].PlayerColor));
                            Game1.listOfPlayers[Round.NumberOfActivePlayer - 1].NumberOfLittlePowns--;

                            _board[X][Y].stoneBottomEdge = 1;
                            Round.PuttingPowl();
                            _board[X][Y].player = Round.NumberOfActivePlayer;
                        }
                        else
                        {
                            MessageBox.Show("Na tej krawędzi już stoi pionek gracza " + Game1.listOfPlayers[Round.NumberOfActivePlayer - 1].PlayerName);
                        }

                        mainGameClass.CanStone = false;
                        mainGameClass.CheckStone = false;
                        // Round.NextTurn();
                    }
                    else
                    {
                        MessageBox.Show("Na tej krawędzi już stoi pionek gracza " + Game1.listOfPlayers[Round.NumberOfActivePlayer - 1].PlayerName);
                    }
                }

                //else if (xRay.Intersects(new BoundingBox(new Vector3((float)X, (float)Y, 0), new Vector3((float)X + 1, (float)Y + 1, 0))) > 0f)
                //{
                //    MessageBox.Show("Nie klikaj w srodek ! Spróbuj położyc pionka bliżej którejś z krawędzi");
                //}

                //NOWA ZMIANA - SRODEK
                else if (xRay.Intersects(new BoundingBox(new Vector3((float)X + 0.25f, (float)Y + 0.25f, 0), new Vector3((float)X + 0.75f, (float)Y + 0.75f, 0))) > 0f)
                {
                    if (CanIPutStone(this.X, this.Y, 4))
                    {
                        if (_board[X][Y].additional == 1)
                        {
                            model.Add(new Model3D(mainGameClass, X + 0.30f, Y + 0.5f, Game1.listOfPlayers[Round.NumberOfActivePlayer - 1].PlayerColor));

                            Game1.listOfPlayers[Round.NumberOfActivePlayer - 1].NumberOfLittlePowns--;

                            _board[X][Y].stoneCenter = 1;
                            _board[X][Y].player = Round.NumberOfActivePlayer;
                            Round.PuttingPowl();
                            mainGameClass.CanStone = false;
                            mainGameClass.CheckStone = false;
                            //  Round.NextTurn();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Na srodku już stoi pionek gracza " + Game1.listOfPlayers[Round.NumberOfActivePlayer - 1].PlayerName);
                    }
                }

                //NOWA ZMIANA
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
                                    if (elements.Count >= 1)
                                    {
                                        if (CheckBounds(i, j, textureIndex))
                                        {
                                            //if (mainGameClass.putElement == true) //Round.NextTurn();
                                            mainGameClass.CanStone = false;
                                            this.X = i;
                                            this.Y = j;

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
                                            mainGameClass.CanStone = true;
                                            Round.PuttingElement();
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

        // Funkcja sprawdza czy można postawić pionek na danej krawędzi w przypadku,
        // gdy na elemenecie obok już stoi taki pionek na tej krawędzi
        public bool CanIPutStone(int x, int y, int edge)
        {
            switch (edge)
            {
                /* Lewa krawędź */
                case 0: if (x > 0 && x < 19) if (_board[x - 1][y].stoneRightEdge == 0) return true; else return false; break;
                /* Górna krawędź */
                case 1: if (y > 0 && y < 19) if (_board[x][y + 1].stoneBottomEdge == 0) return true; else return false; break;
                /* Prawa krawędź */
                case 2: if (x > 0 && x < 19) if (_board[x + 1][y].stoneLeftEdge == 0) return true; else return false; break;
                /* Dolna krawędź */
                case 3: if (x > 0 && x < 19) if (_board[x][y - 1].stoneUpEdge == 0) return true; else return false; break;
                /* Srodek */
                case 4: if (x > 0 && x < 19) if (_board[x][y].stoneCenter == 0) return true; else return false; break;
                default: return false;
            }
            return true;
        }

        private int FloodFill(Point node, int szukanaKrawedz)
        {
            Element target;
            bool first = true;
            Queue<Point> Q = new Queue<Point>();
            List<Point> policzone = new List<Point>();
            Element tmp = _board[node.X][node.Y];
            if (szukanaKrawedz == 1) szukanaKrawedz = 0;
            if (szukanaKrawedz == 4) szukanaKrawedz = 3;

            Q.Enqueue(node);
            while (Q.Count != 0)
            {
                if (first)
                {
                    Point n = Q.Dequeue();
                    if (n.X < _board.Length - 1 && n.Y < _board[0].Length - 1 && n.X >= 1 && n.Y >= 1)
                    {
                        policzone.Add(n);
                        target = _board[n.X][n.Y];
                        if (!policzone.Contains(new Point(n.X - 1, n.Y)) && (target.leftEdge == szukanaKrawedz || target.leftEdge == szukanaKrawedz + 1) && _board[n.X - 1][n.Y].rightEdge != -10)
                        {
                            Q.Enqueue(new Point(n.X - 1, n.Y));
                        }

                        if (!policzone.Contains(new Point(n.X + 1, n.Y)) && (target.rightEdge == szukanaKrawedz || target.rightEdge == szukanaKrawedz + 1) && _board[n.X + 1][n.Y].leftEdge != -10)
                        {
                            Q.Enqueue(new Point(n.X + 1, n.Y));
                        }

                        if (!policzone.Contains(new Point(n.X, n.Y + 1)) && (target.upEdge == szukanaKrawedz || target.upEdge == szukanaKrawedz + 1) && _board[n.X][n.Y + 1].bottomEdge != -10)
                        {
                            Q.Enqueue(new Point(n.X, n.Y + 1));
                        }

                        if (!policzone.Contains(new Point(n.X, n.Y - 1)) && (target.bottomEdge == szukanaKrawedz || target.bottomEdge == szukanaKrawedz + 1) && _board[n.X][n.Y - 1].upEdge != -10)
                        {
                            Q.Enqueue(new Point(n.X, n.Y - 1));
                        }
                        first = false;
                    }
                }
                else
                {
                    Point n = Q.Dequeue();
                    if (n.X < _board.Length - 1 && n.Y < _board[0].Length - 1 && n.X >= 1 && n.Y >= 1)
                    {
                        policzone.Add(n);
                        target = _board[n.X][n.Y];
                        if (!policzone.Contains(new Point(n.X - 1, n.Y)) && (target.leftEdge == szukanaKrawedz) && _board[n.X - 1][n.Y].rightEdge != -10)
                        {
                            Q.Enqueue(new Point(n.X - 1, n.Y));
                        }

                        if (!policzone.Contains(new Point(n.X + 1, n.Y)) && (target.rightEdge == szukanaKrawedz) && _board[n.X + 1][n.Y].leftEdge != -10)
                        {
                            Q.Enqueue(new Point(n.X + 1, n.Y));
                        }

                        if (!policzone.Contains(new Point(n.X, n.Y + 1)) && (target.upEdge == szukanaKrawedz) && _board[n.X][n.Y + 1].bottomEdge != -10)
                        {
                            Q.Enqueue(new Point(n.X, n.Y + 1));
                        }

                        if (!policzone.Contains(new Point(n.X, n.Y - 1)) && (target.bottomEdge == szukanaKrawedz) && _board[n.X][n.Y - 1].upEdge != -10)
                        {
                            Q.Enqueue(new Point(n.X, n.Y - 1));
                        }
                    }
                }
            }

            return policzone.Count;
        }

        private bool CheckIfModel(Point node, int szukanaKrawedz)
        {
            Element target;
            bool first = true;
            Queue<Point> Q = new Queue<Point>();
            List<Point> policzone = new List<Point>();
            Element tmp = _board[node.X][node.Y];
            if (szukanaKrawedz == 1) szukanaKrawedz = 0;
            if (szukanaKrawedz == 4) szukanaKrawedz = 3;

            Q.Enqueue(node);
            while (Q.Count != 0)
            {
                if (first)
                {
                    Point n = Q.Dequeue();
                    if (n.X < _board.Length - 1 && n.Y < _board[0].Length - 1 && n.X >= 1 && n.Y >= 1)
                    {
                        policzone.Add(n);
                        target = _board[n.X][n.Y];

                        if (!policzone.Contains(new Point(n.X - 1, n.Y)) && (target.leftEdge == szukanaKrawedz || target.leftEdge == szukanaKrawedz + 1))
                        {
                            if ((target.stoneLeftEdge == 1) || (target.stoneRightEdge == 1) || (target.stoneUpEdge == 1) || (target.stoneBottomEdge == 1)) return true;
                            Q.Enqueue(new Point(n.X - 1, n.Y));
                        }

                        if (!policzone.Contains(new Point(n.X + 1, n.Y)) && (target.rightEdge == szukanaKrawedz || target.rightEdge == szukanaKrawedz + 1))
                        {
                            if ((target.stoneLeftEdge == 1) || (target.stoneRightEdge == 1) || (target.stoneUpEdge == 1) || (target.stoneBottomEdge == 1)) return true;
                            Q.Enqueue(new Point(n.X + 1, n.Y));
                        }

                        if (!policzone.Contains(new Point(n.X, n.Y + 1)) && (target.upEdge == szukanaKrawedz || target.upEdge == szukanaKrawedz + 1))
                        {
                            if ((target.stoneLeftEdge == 1) || (target.stoneRightEdge == 1) || (target.stoneUpEdge == 1) || (target.stoneBottomEdge == 1)) return true;
                            Q.Enqueue(new Point(n.X, n.Y + 1));
                        }

                        if (!policzone.Contains(new Point(n.X, n.Y - 1)) && (target.bottomEdge == szukanaKrawedz || target.bottomEdge == szukanaKrawedz + 1))
                        {
                            if ((target.stoneLeftEdge == 1) || (target.stoneRightEdge == 1) || (target.stoneUpEdge == 1) || (target.stoneBottomEdge == 1)) return true;
                            Q.Enqueue(new Point(n.X, n.Y - 1));
                        }
                        first = false;
                    }
                }
                else
                {
                    Point n = Q.Dequeue();
                    if (n.X < _board.Length - 1 && n.Y < _board[0].Length - 1 && n.X >= 1 && n.Y >= 1)
                    {
                        policzone.Add(n);
                        target = _board[n.X][n.Y];
                        if (!policzone.Contains(new Point(n.X - 1, n.Y)) && (target.leftEdge == szukanaKrawedz))
                        {
                            if ((target.stoneLeftEdge == 1) || (target.stoneRightEdge == 1) || (target.stoneUpEdge == 1) || (target.stoneBottomEdge == 1)) return true;
                            Q.Enqueue(new Point(n.X - 1, n.Y));
                        }

                        if (!policzone.Contains(new Point(n.X + 1, n.Y)) && (target.rightEdge == szukanaKrawedz))
                        {
                            if ((target.stoneLeftEdge == 1) || (target.stoneRightEdge == 1) || (target.stoneUpEdge == 1) || (target.stoneBottomEdge == 1)) return true;
                            Q.Enqueue(new Point(n.X + 1, n.Y));
                        }

                        if (!policzone.Contains(new Point(n.X, n.Y + 1)) && (target.upEdge == szukanaKrawedz))
                        {
                            if ((target.stoneLeftEdge == 1) || (target.stoneRightEdge == 1) || (target.stoneUpEdge == 1) || (target.stoneBottomEdge == 1)) return true;
                            Q.Enqueue(new Point(n.X, n.Y + 1));
                        }

                        if (!policzone.Contains(new Point(n.X, n.Y - 1)) && (target.bottomEdge == szukanaKrawedz))
                        {
                            if ((target.stoneLeftEdge == 1) || (target.stoneRightEdge == 1) || (target.stoneUpEdge == 1) || (target.stoneBottomEdge == 1)) return true;
                            Q.Enqueue(new Point(n.X, n.Y - 1));
                        }
                    }
                }
            }
            return false;
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
                    item.myModel.Draw(Matrix.CreateScale(2f) * Matrix.CreateRotationX(1.5f) * Matrix.CreateTranslation(item.modelPosition.X, item.modelPosition.Y, 0) * Effect.World, Effect.View, Effect.Projection);
                }
            }
            base.Draw(gameTime);
        }
    }
}