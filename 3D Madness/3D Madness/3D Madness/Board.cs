﻿using System;
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
        private int size = 1;
        public int numberOfRotation { get; set; }
        private int tempRotation;
        public int X = 0;
        public int Y = 0;
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
        public List<Model3D> model = new List<Model3D>();
        public Texture2D txt1 { get; set; }
        private Texture2D txt2;
        private int textureIndex = 0;
        private SpriteBatch spriteBatch;
        private const int GRASS = 6;
        Random rand;

        public Board(Game g, Texture2D txt1, Texture2D txt2)
            : base(g)
        {
            this.txt1 = txt1;
            this.txt2 = txt2;
            mainGameClass = (Game1)g;
            Effect = new BasicEffect(g.GraphicsDevice);

            rand_element = new XML_Parser();            
            spriteBatch = new SpriteBatch(g.GraphicsDevice);
            numberOfRotation = 0;

            loadTexturesAt(@"Blocks\");

            initBoard();
            GenerateBoard();

            _board[10][10].Texture = elements[0].Texture;
            _board[10][10].leftEdge = elements[textureIndex].leftEdge;
            _board[10][10].rightEdge = elements[textureIndex].rightEdge;
            _board[10][10].upEdge = elements[textureIndex].upEdge;
            _board[10][10].bottomEdge = elements[textureIndex].bottomEdge;
            _board[10][10].additional = elements[textureIndex].additional;
            elements.RemoveAt(textureIndex);

            rand = new Random();
            textureIndex = rand.Next(0, elements.Count-1);
            NextBlock = elements[textureIndex].Texture;
        }

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime) {
            foreach (EffectPass pass in Effect.CurrentTechnique.Passes) {
                for (int i = 0; i < 20; i++) {
                    for (int j = 0; j < 20; j++) {
                        Effect.Texture = _board[i][j].Texture;
                        pass.Apply();

                        GraphicsDevice.SamplerStates[0] = SamplerState.LinearClamp;
                        GraphicsDevice.DrawUserPrimitives<VertexPositionTexture>
                       (PrimitiveType.TriangleStrip, _board[i][j].verts, 0, 2);
                    }
                }

                foreach (var item in model) {
                    item.myModel.Draw(Matrix.CreateScale(2f) * Matrix.CreateRotationX(1.5f) * Matrix.CreateTranslation(item.modelPosition.X, item.modelPosition.Y, 0) * Effect.World, Effect.View, Effect.Projection);
                }
            }
            base.Draw(gameTime);
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
                Point cursorPos = new Point(this.X, this.Y);
                Player activePlayer = Game1.listOfPlayers[Round.NumberOfActivePlayer - 1];
                int playerColor = activePlayer.PlayerColor;
                Element thisBoard = _board[this.X][this.Y];

                try
                {
                    // LEFT EDGE
                    if (intersects(xRay, X, Y + 0.25f, 0, X + 0.25f, Y + 0.75f, 0))
                    {

                        if (!CanIPutStone(this.X, this.Y, 0))
                            throw new Edge2PawnCollisionException();

                        if (CheckIfModel(cursorPos, thisBoard.leftEdge))
                            throw new Edge2PawnCollisionException();

                        if (thisBoard.leftEdge == GRASS) // pawn cannot be placed on grass
                            throw new PawnCannotBePlacedHereException();

                        model.Add(new Model3D(mainGameClass, X, Y + 0.5f, playerColor));
                        activePlayer.NumberOfLittlePowns--; // decrease number of pawns
                        activePlayer.Pawns.Add(new Pawn(X, Y, Element.Direction.Left,thisBoard.leftEdge)); // save Pawn position
                        thisBoard.stoneLeftEdge = 1;
                        Round.PuttingPowl();
                        thisBoard.player = Round.NumberOfActivePlayer;

                        mainGameClass.CanStone = false;
                        mainGameClass.CheckStone = false;
                    }

                    // RIGHT EDGE
                    else if (intersects(xRay, X + 0.75f, Y + 0.25f, 0, X + 1, Y + 0.75f, 0))
                    {

                        if (!CanIPutStone(this.X, this.Y, 2))
                            throw new Pawn2PawnCollisionException();

                        if (CheckIfModel(cursorPos, thisBoard.rightEdge))
                            throw new Pawn2PawnCollisionException();

                        if (thisBoard.rightEdge == GRASS)
                            throw new PawnCannotBePlacedHereException();

                        model.Add(new Model3D(mainGameClass, X + 0.55f, Y + 0.5f, playerColor));
                        activePlayer.Pawns.Add(new Pawn(X, Y, Element.Direction.Right, thisBoard.rightEdge)); // save Pawn position
                        activePlayer.NumberOfLittlePowns--;
                        thisBoard.stoneRightEdge = 1;
                        Round.PuttingPowl();
                        thisBoard.player = Round.NumberOfActivePlayer;

                        mainGameClass.CanStone = false;
                        mainGameClass.CheckStone = false;
                    }

                    // TOP (UP) EDGE
                    else if (intersects(xRay, X + 0.25f, Y + 0.75f, 0, X + 0.75f, Y + 1, 0))
                    {

                        if (!CanIPutStone(this.X, this.Y, 1))
                            throw new Pawn2PawnCollisionException();

                        if (CheckIfModel(cursorPos, thisBoard.upEdge))
                            throw new Pawn2PawnCollisionException();

                        if (thisBoard.upEdge == GRASS)
                            throw new PawnCannotBePlacedHereException();

                        model.Add(new Model3D(mainGameClass, X + 0.30f, Y + 0.9f, playerColor));
                        activePlayer.Pawns.Add(new Pawn(X, Y, Element.Direction.Up, thisBoard.upEdge)); // save Pawn position
                        activePlayer.NumberOfLittlePowns--;
                        thisBoard.stoneUpEdge = 1;
                        Round.PuttingPowl();
                        thisBoard.player = Round.NumberOfActivePlayer;

                        mainGameClass.CanStone = false;
                        mainGameClass.CheckStone = false;
                    }

                    // BOTTOM (DOWN) EDGE
                    else if (intersects(xRay, X + 0.25f, Y, 0, X + 0.75f, Y + 0.25f, 0))
                    {
                        if (!CanIPutStone(this.X, this.Y, 3))
                            throw new Pawn2PawnCollisionException();

                        if (CheckIfModel(cursorPos, thisBoard.bottomEdge))
                            throw new Pawn2PawnCollisionException();

                        if (thisBoard.bottomEdge == GRASS)
                            throw new PawnCannotBePlacedHereException();

                        model.Add(new Model3D(mainGameClass, X + 0.30f, Y + 0.2f, playerColor));
                        activePlayer.Pawns.Add(new Pawn(X, Y, Element.Direction.Down, thisBoard.bottomEdge)); // save Pawn position
                        activePlayer.NumberOfLittlePowns--;
                        thisBoard.stoneBottomEdge = 1;
                        Round.PuttingPowl();
                        thisBoard.player = Round.NumberOfActivePlayer;

                        mainGameClass.CanStone = false;
                        mainGameClass.CheckStone = false;
                    }

                    //SRODEK
                    else if (intersects(xRay, X + 0.25f, Y + 0.25f, 0, X + 0.75f, Y + 0.75f, 0))
                    {
                        if (!CanIPutStone(this.X, this.Y, 4))
                            throw new Pawn2PawnCollisionException();

                        if (thisBoard.additional == 1)
                        {
                            model.Add(new Model3D(mainGameClass, X + 0.30f, Y + 0.5f, playerColor));

                            activePlayer.NumberOfLittlePowns--;
                            activePlayer.Pawns.Add(new Pawn(X, Y, Element.Direction.Down, thisBoard.bottomEdge)); // save Pawn position
                            thisBoard.stoneCenter = 1;
                            thisBoard.player = Round.NumberOfActivePlayer;
                            Round.PuttingPowl();
                            mainGameClass.CanStone = false;
                            mainGameClass.CheckStone = false;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Pionek nie może zostać postawiony we wskazane miejsce.");
                    }
                }
                catch (Pawn2PawnCollisionException e)
                {
                    MessageBox.Show("Kolizja z innym pionkiem.");
                }
                catch (Edge2PawnCollisionException e)
                {
                    MessageBox.Show("Kolizja pionka z krawędzią.");
                }
                catch (PawnCannotBePlacedHereException e)
                {
                    return; // do nothing. Pawn cannot be placed on grass
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
                            Element thisBoard = _board[i][j];

                            if (thisBoard.Texture == txt1)
                            {
                                if(intersects(xRay, i, j, 0, i+1, j+1, 0))
                                {
                                    if (elements.Count >= 1)
                                    {
                                        if (CheckBounds(i, j, textureIndex))
                                        {
                                            mainGameClass.CanStone = false;
                                            this.X = i;
                                            this.Y = j;

                                            thisBoard.Texture = elements[textureIndex].Texture;
                                            thisBoard.leftEdge = elements[textureIndex].leftEdge;
                                            thisBoard.rightEdge = elements[textureIndex].rightEdge;
                                            thisBoard.upEdge = elements[textureIndex].upEdge;
                                            thisBoard.bottomEdge = elements[textureIndex].bottomEdge;
                                            thisBoard.additional = elements[textureIndex].additional;

                                            // ROTACJA TEKSTURY KLOCKA RYSOWANEGO NA PLANSZY
                                            if (numberOfRotation % 4 == 1)
                                            {
                                                rotateTexture( i, j,
                                                    new Point(1,0), new Point(1,1), new Point(0,0), new Point(0,1)
                                                );
                                            }

                                            if (numberOfRotation % 4 == 2)
                                            {
                                                rotateTexture(i, j,
                                                    new Point(1, 1), new Point(0, 1), new Point(1, 0), new Point(0, 0)
                                                );
                                            }
                                            if (numberOfRotation % 4 == 3)
                                            {
                                                rotateTexture(i, j,
                                                    new Point(0, 1), new Point(0, 0), new Point(1, 1), new Point(1, 0)
                                                );
                                            }

                                            elements.RemoveAt(textureIndex);
                                            textureIndex = rand.Next(0, elements.Count-1);
                                            NextBlock = elements[textureIndex].Texture;
                                            numberOfRotation = 0;

                                            mainGameClass.putElement = true;
                                            mainGameClass.CanStone = true;
                                            Round.PuttingElement();

                                            if (elements.Count < 1)
                                            {
                                                string wyniki = "Game Over\n";
                                                string wyniki1 ="";
                                                for (int z = 0; z < Round.NumberOfPlayers; z++)
                                                {
                                                    foreach (Pawn pionek in Game1.listOfPlayers[z].Pawns)
                                                    {
                                                        Game1.listOfPlayers[z].PlayerPoints += FloodFill(new Point(pionek.x, pionek.y), pionek.krawedz, pionek.wartosc);
                                                    }
                                                    wyniki += Game1.listOfPlayers[z].PlayerName + ":" + Game1.listOfPlayers[z].PlayerPoints + " " + "pkt\n";
                                                    wyniki1 += Game1.listOfPlayers[z].PlayerName + ":" + Game1.listOfPlayers[z].PlayerPoints + " ";                                                 
                                                }

                                                if (DialogResult.OK == MessageBox.Show(wyniki))
                                                {
                                                    //Zapis wyników 
                                                    System.IO.StreamWriter file = new System.IO.StreamWriter("wyniki.txt", true);
                                                    file.WriteLine(DateTime.Now.ToString() + " " + wyniki1);
                                                    file.Close();

                                                    for (int z = 0; z < Round.NumberOfPlayers; z++)
                                                    {
                                                            Game1.listOfPlayers[z].PlayerPoints = 0;   
                                                    }

                                                    if (MessageBox.Show("Czy chcesz zacząć nową grę?", "Carcassonne", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                                    {


                                                        //mainGameClass.board.Dispose();
                                                        //mainGameClass.Components.Remove(mainGameClass.board);
                                                        //mainGameClass.Components.Add(mainGameClass.menu);

                                              
                                                        //  Game1
                                                     //   Application.Restart();
                                                     

                                                    }
                                                    else
                                                    {
                                                        Application.Exit();
                                                    }
                                                }
                                            }
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

        public void RotationBlock()
        {
            numberOfRotation++;
            tempRotation = elements[textureIndex].upEdge;
            elements[textureIndex].upEdge = elements[textureIndex].rightEdge;
            elements[textureIndex].rightEdge = elements[textureIndex].bottomEdge;
            elements[textureIndex].bottomEdge = elements[textureIndex].leftEdge;
            elements[textureIndex].leftEdge = tempRotation;
        }
        
        /// <summary>
        /// Funkcja sprawdza czy można postawić pionek na danej krawędzi w przypadku,
        /// gdy na elemenecie obok już stoi taki pionek na tej krawędzi
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="edge"></param>
        /// <returns></returns>
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

        private void initBoard() {
            _board = new Element[20][];
            x = new VertexPositionTexture[sizeX][][];
            for (int i = 0; i < sizeX; i++) {
                x[i] = new VertexPositionTexture[sizeY][];
                _board[i] = new Element[20];

                for (int j = 0; j < sizeY; j++) {
                    x[i][j] = new VertexPositionTexture[4];
                }
            }
        }

        private void loadTexturesAt(String folder) {
            elements = rand_element.XDocParse();
            for (int i = 0; i < 57; i++) {
                elements[i].Texture = (mainGameClass.Content.Load<Texture2D>(folder + elements[i].FileName));
            }
        }

        private void GenerateBoard() {
            for (int i = 0; i < sizeX; i++) {
                for (int j = 0; j < sizeY; j++) {
                    x[i][j][0] = new VertexPositionTexture(new Vector3(i, j + size, 0), new Vector2(0, 0));
                    x[i][j][1] = new VertexPositionTexture(new Vector3(i + size, j + size, 0), new Vector2(1, 0));
                    x[i][j][2] = new VertexPositionTexture(new Vector3(i, j, 0), new Vector2(0, 1));
                    x[i][j][3] = new VertexPositionTexture(new Vector3(i + size, j, 0), new Vector2(1, 1));

                    _board[i][j] = new Element(x[i][j], txt1);
                }
            }
        }

        private bool CheckBounds(int x, int y, int textInd) {
            Element next = elements[textInd];
            if (x > 1 && x < 19 && y > 1 && y < 19) {
                if (_board[x - 1][y].Texture == txt1 && _board[x + 1][y].Texture == txt1 && _board[x][y - 1].Texture == txt1 && _board[x][y + 1].Texture == txt1)
                    return false;
                if ((next.leftEdge == _board[x - 1][y].rightEdge || _board[x - 1][y].Texture == txt1 || (next.leftEdge == _board[x - 1][y].rightEdge + 1) || (next.leftEdge == _board[x - 1][y].rightEdge - 1)) &&
                    (next.rightEdge == _board[x + 1][y].leftEdge || _board[x + 1][y].Texture == txt1 || next.rightEdge == _board[x + 1][y].leftEdge + 1 || next.rightEdge == _board[x + 1][y].leftEdge - 1) &&
                    (next.upEdge == _board[x][y + 1].bottomEdge || _board[x][y + 1].Texture == txt1 || next.upEdge == _board[x][y + 1].bottomEdge + 1 || next.upEdge == _board[x][y + 1].bottomEdge - 1) &&
                    (next.bottomEdge == _board[x][y - 1].upEdge || _board[x][y - 1].Texture == txt1 || next.bottomEdge == _board[x][y - 1].upEdge + 1 || next.bottomEdge == _board[x][y - 1].upEdge - 1))
                    return true;
                else
                    return false;
            } else
                return true;
        }

        private Boolean intersects(Ray xRay, float vectorFirstX, float vectorFirstY, float vectorFirstZ,
                                    float vectorSecondX, float vectorSecondY, float vectorSecondZ) {
            return
                xRay.Intersects(
                    new BoundingBox(
                        new Vector3(vectorFirstX, vectorFirstY, vectorFirstZ),
                        new Vector3(vectorSecondX, vectorSecondY, vectorSecondZ)
                    )
                ) > 0f;
        }

        private int FloodFill(Point node, Element.Direction kierunek, int szukanaKrawedz)
        {
            int numberOfShield = 0;
            Element target = _board[node.X][node.Y];
            if (target.additional == 1) // jesli jest katedrą
                return 5;

            bool first = true;
            Queue<Point> Q = new Queue<Point>();
            List<Point> policzone = new List<Point>();
            
            if (szukanaKrawedz == (int)Element.Edges.EndRoad) szukanaKrawedz = (int)Element.Edges.Road;
            if (szukanaKrawedz == (int)Element.Edges.EndTown) szukanaKrawedz = (int)Element.Edges.Town;
            
            Q.Enqueue(node);
            while (Q.Count != 0)
            {
                Point n = Q.Dequeue();
                if (_board[n.X][n.Y].additional == 2 && !policzone.Contains(new Point(n.X, n.Y)))
                    numberOfShield += 1; 
                if (first)
                {
                    
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
            if (numberOfShield != 0)
            {
                int sum = 0;
                sum = policzone.Count * (numberOfShield *2);
                return sum;
            }
            else
                return policzone.Count;
        }

        private void checkForGameOver() {
            if (elements.Count < 50) {
                string wyniki = "Game Over\n";
                for (int z = 0; z < Round.NumberOfPlayers; z++) {
                    foreach (Pawn pionek in Game1.listOfPlayers[z].Pawns) {
                        Game1.listOfPlayers[z].PlayerPoints += FloodFill(new Point(pionek.x, pionek.y), pionek.krawedz, pionek.wartosc);
                    }
                    wyniki += Game1.listOfPlayers[z].PlayerName + ": " + Game1.listOfPlayers[z].PlayerPoints + "pkt\n";
                }

                if (DialogResult.OK == MessageBox.Show(wyniki)) {

                }
            }
        }

        private void rotateTexture(int i, int j, Point a, Point b, Point c, Point d) {
            _board[i][j].verts[0] = new VertexPositionTexture(new Vector3(i, j + size, 0), new Vector2(a.X, a.Y));
            _board[i][j].verts[1] = new VertexPositionTexture(new Vector3(i + size, j + size, 0), new Vector2(b.X, b.Y));
            _board[i][j].verts[2] = new VertexPositionTexture(new Vector3(i, j, 0), new Vector2(c.X, c.Y));
            _board[i][j].verts[3] = new VertexPositionTexture(new Vector3(i + size, j, 0), new Vector2(d.X, d.Y));
        }

        private bool CheckIfModel(Point node, int szukanaKrawedz)
        {
            Element target;
            bool first = true;
            Queue<Point> Q = new Queue<Point>();
            List<Point> policzone = new List<Point>();
            Element tmp = _board[node.X][node.Y];
            if (szukanaKrawedz == (int)Element.Edges.EndRoad) szukanaKrawedz = (int)Element.Edges.Road;
            if (szukanaKrawedz == (int)Element.Edges.EndTown) szukanaKrawedz = (int)Element.Edges.Town;

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

                            //if (target.stoneLeftEdge == 1) return true;
                              if (((target.stoneLeftEdge == 1) && (target.leftEdge == szukanaKrawedz)) || 
                                ((target.stoneRightEdge == 1) && (target.rightEdge == szukanaKrawedz)) ||
                                ((target.stoneUpEdge == 1) && (target.upEdge == szukanaKrawedz)) ||
                                ((target.stoneBottomEdge == 1) && (target.bottomEdge == szukanaKrawedz))) return true;
                            Q.Enqueue(new Point(n.X - 1, n.Y));
                        }

                        if (!policzone.Contains(new Point(n.X + 1, n.Y)) && (target.rightEdge == szukanaKrawedz || target.rightEdge == szukanaKrawedz + 1))
                        {
                            //if ((target.stoneLeftEdge == 1) || (target.stoneRightEdge == 1) || (target.stoneUpEdge == 1) || (target.stoneBottomEdge == 1)) return true;
                            //if (target.stoneRightEdge == 1) return true;
                            if (((target.stoneLeftEdge == 1) && (target.leftEdge == szukanaKrawedz)) ||
                                ((target.stoneRightEdge == 1) && (target.rightEdge == szukanaKrawedz)) ||
                                ((target.stoneUpEdge == 1) && (target.upEdge == szukanaKrawedz)) ||
                                ((target.stoneBottomEdge == 1) && (target.bottomEdge == szukanaKrawedz))) return true;
                            Q.Enqueue(new Point(n.X + 1, n.Y));
                        }

                        if (!policzone.Contains(new Point(n.X, n.Y + 1)) && (target.upEdge == szukanaKrawedz || target.upEdge == szukanaKrawedz + 1))
                        {
                            //if ((target.stoneLeftEdge == 1) || (target.stoneRightEdge == 1) || (target.stoneUpEdge == 1) || (target.stoneBottomEdge == 1)) return true;
                          //  if (target.stoneUpEdge == 1) return true;
                            if (((target.stoneLeftEdge == 1) && (target.leftEdge == szukanaKrawedz)) ||
                                ((target.stoneRightEdge == 1) && (target.rightEdge == szukanaKrawedz)) ||
                                ((target.stoneUpEdge == 1) && (target.upEdge == szukanaKrawedz)) ||
                                ((target.stoneBottomEdge == 1) && (target.bottomEdge == szukanaKrawedz))) return true;
                            Q.Enqueue(new Point(n.X, n.Y + 1));
                        }

                        if (!policzone.Contains(new Point(n.X, n.Y - 1)) && (target.bottomEdge == szukanaKrawedz || target.bottomEdge == szukanaKrawedz + 1))
                        {
                            //if ((target.stoneLeftEdge == 1) || (target.stoneRightEdge == 1) || (target.stoneUpEdge == 1) || (target.stoneBottomEdge == 1)) return true;
                           // if (target.stoneBottomEdge == 1) return true;
                            if (((target.stoneLeftEdge == 1) && (target.leftEdge == szukanaKrawedz)) ||
                                ((target.stoneRightEdge == 1) && (target.rightEdge == szukanaKrawedz)) ||
                                ((target.stoneUpEdge == 1) && (target.upEdge == szukanaKrawedz)) ||
                                ((target.stoneBottomEdge == 1) && (target.bottomEdge == szukanaKrawedz))) return true;
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
                            //if ((target.stoneLeftEdge == 1) || (target.stoneRightEdge == 1) || (target.stoneUpEdge == 1) || (target.stoneBottomEdge == 1)) return true;
                            //if (target.stoneLeftEdge == 1) return true;
                            if (((target.stoneLeftEdge == 1) && (target.leftEdge == szukanaKrawedz)) ||
                                ((target.stoneRightEdge == 1) && (target.rightEdge == szukanaKrawedz)) ||
                                ((target.stoneUpEdge == 1) && (target.upEdge == szukanaKrawedz)) ||
                                ((target.stoneBottomEdge == 1) && (target.bottomEdge == szukanaKrawedz))) return true;
                            Q.Enqueue(new Point(n.X - 1, n.Y));
                        }

                        if (!policzone.Contains(new Point(n.X + 1, n.Y)) && (target.rightEdge == szukanaKrawedz))
                        {
                            //if ((target.stoneLeftEdge == 1) || (target.stoneRightEdge == 1) || (target.stoneUpEdge == 1) || (target.stoneBottomEdge == 1)) return true;
                           // if (target.stoneRightEdge == 1) return true;
                            if (((target.stoneLeftEdge == 1) && (target.leftEdge == szukanaKrawedz)) ||
                                ((target.stoneRightEdge == 1) && (target.rightEdge == szukanaKrawedz)) ||
                                ((target.stoneUpEdge == 1) && (target.upEdge == szukanaKrawedz)) ||
                                ((target.stoneBottomEdge == 1) && (target.bottomEdge == szukanaKrawedz))) return true;
                            Q.Enqueue(new Point(n.X + 1, n.Y));
                        }

                        if (!policzone.Contains(new Point(n.X, n.Y + 1)) && (target.upEdge == szukanaKrawedz))
                        {
                            //if ((target.stoneLeftEdge == 1) || (target.stoneRightEdge == 1) || (target.stoneUpEdge == 1) || (target.stoneBottomEdge == 1)) return true;
                            //if (target.stoneUpEdge == 1) return true;
                            if (((target.stoneLeftEdge == 1) && (target.leftEdge == szukanaKrawedz)) ||
                                ((target.stoneRightEdge == 1) && (target.rightEdge == szukanaKrawedz)) ||
                                ((target.stoneUpEdge == 1) && (target.upEdge == szukanaKrawedz)) ||
                                ((target.stoneBottomEdge == 1) && (target.bottomEdge == szukanaKrawedz))) return true;
                            Q.Enqueue(new Point(n.X, n.Y + 1));
                        }

                        if (!policzone.Contains(new Point(n.X, n.Y - 1)) && (target.bottomEdge == szukanaKrawedz))
                        {
                           // if ((target.stoneLeftEdge == 1) || (target.stoneRightEdge == 1) || (target.stoneUpEdge == 1) || (target.stoneBottomEdge == 1)) return true;
                           // if (target.stoneBottomEdge == 1) return true;
                            if (((target.stoneLeftEdge == 1) && (target.leftEdge == szukanaKrawedz)) ||
                                ((target.stoneRightEdge == 1) && (target.rightEdge == szukanaKrawedz)) ||
                                ((target.stoneUpEdge == 1) && (target.upEdge == szukanaKrawedz)) ||
                                ((target.stoneBottomEdge == 1) && (target.bottomEdge == szukanaKrawedz))) return true;
                            Q.Enqueue(new Point(n.X, n.Y - 1));
                        }
                    }
                }
            }
            return false;
        }
    }
}