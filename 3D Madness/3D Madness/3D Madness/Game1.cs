using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace _3D_Madness
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        FormPlayers NewGameForm = new FormPlayers();
        const float speed = 0.1f;
        double time;
        private Rectangle camByExit;
        public Matrix worldTranslation = Matrix.Identity;
        public Matrix worldRotationX = Matrix.Identity;

        Texture2D txt1;
        Texture2D txt2;
        public Texture2D czarnymodel, zoltymodel, czerwonymodel, zielonymodel, niebieskimodel;
        public bool putElement = false;

        public float whereIam = 0.0f;

        public GraphicsDeviceManager graphics { get; set; }

        public static List<Player> listOfPlayers = new List<Player>();
        public static List<String> wyniki = new List<string>(listOfPlayers.Count);

        SpriteBatch spriteBatch;
        KeyboardState previousKeyboard;
        MouseState current, previous;
        public KeyboardState kbprevious;

        public bool startNewGame { get; set; }

        public bool playersSetings { get; set; }

        public bool pressedNewGame { get; set; }

        public bool pressedTheEnd { get; set; }

        // Game camera
        public Camera camera { get; set; }

        public Board board { get; set; }

        private Menu menu;

        public Bar infoBar { get; set; }

        public Model3D model3D { get; set; }

        public bool CheckStone = false;

        public bool CanStone = false;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Window.AllowUserResizing = true;
            pressedNewGame = false;
            pressedTheEnd = false;
            startNewGame = false;
            playersSetings = false;
            time = 0;
            System.Windows.Forms.Form form = (System.Windows.Forms.Form)System.Windows.Forms.Form.FromHandle(Window.Handle);
            form.Location = new System.Drawing.Point(0, 0);
            form.Size = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Size;
            graphics.PreferredBackBufferHeight = form.Size.Height;
            graphics.PreferredBackBufferWidth = form.Size.Width;
            //graphics.IsFullScreen = true;
        }

        protected override void Initialize()
        {
            camera = new Camera(this, Vector3.Zero, Vector3.Zero, Vector3.Up);
            // ustawienie pozycji poczatkowej swiata
            worldTranslation = Matrix.Add(worldTranslation, Matrix.CreateTranslation(new Vector3(-20, -20, -22)));

            Components.Add(camera);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            czerwonymodel = Content.Load<Texture2D>(@"Models\czerwonaminiatura");
            czarnymodel = Content.Load<Texture2D>(@"Models\czarnaminiatura");
            zoltymodel = Content.Load<Texture2D>(@"Models\zoltaminiatura");
            zielonymodel = Content.Load<Texture2D>(@"Models\zielonaminiatura");
            niebieskimodel = Content.Load<Texture2D>(@"Models\niebieskaminiatura");

            txt1 = Content.Load<Texture2D>(@"Textures\empty");
            txt2 = Content.Load<Texture2D>(@"Textures\Trees");

            menu = new Menu(this);

            // Zaladowanie pustej planszy
            board = new Board(this, txt1, txt2);
            infoBar = new Bar(this);
            //model3D = new Model3D(this);

            Components.Add(menu);
        }

        protected override void UnloadContent() { }

        protected override void Update(GameTime gameTime)
        {
            if (this.IsActive)
            {
                time += gameTime.ElapsedGameTime.TotalMilliseconds;
                camByExit = new Rectangle(GraphicsDevice.Viewport.Width - 110, -22, GraphicsDevice.Viewport.Width, 30);
                current = Mouse.GetState();
                // Allows the game to exit

                #region START GRY

                // START GRY //
                if (pressedNewGame)
                {
                    if (playersSetings == false)
                    {
                        NewGameForm.Show();
                        playersSetings = true;
                    }
                    if (NewGameForm.formClose == true)
                    {
                        for (int i = 0; i < NewGameForm.numberOfPlayers; i++)
                        {
                            listOfPlayers.Add(new Player(NewGameForm.namesOfPlayers[i], NewGameForm.colorsOfPlayers[i]));
                        }
                        NewGameForm.Close();
                        Components.Remove(menu);
                        Components.Add(board);
                        Components.Add(infoBar);
                        //Components.Add(model3D);
                        pressedNewGame = false;

                        Round.NumberOfPlayers = listOfPlayers.Count;
                        Round.NumberOfActivePlayer = 1;
                    }
                }
                ///////////////

                #endregion START GRY

                if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                    this.Exit();
                if (pressedTheEnd)
                    this.Exit();

                // po co ten pierwszy worldRotation?
                //camera.view = worldRotation * worldTranslation * worldRotation;
                camera.view = worldTranslation * worldRotationX;

                board.Effect.View = camera.view;
                board.Effect.Projection = camera.projection;
                board.Effect.TextureEnabled = true;

                // katy rotacji swiata z macierzy rotacji, w tej chwili posiadamy rotacje wzgledem X, ale na przyszlosc gdybysmy potrzebowali
                // to sa tez wzgledem Y i Z
                // wg. strony -> http://www.codeguru.com/forum/archive/index.php/t-329530.html

                // camera.rotationAngleY = (-1) * Math.Asin(worldRotationY.M31);
                // camera.rotationAngleZ = Math.Atan2(worldRotationZ.M21, worldRotationZ.M11);

                camera.rotationAngleX = Math.Atan2(worldRotationX.M32, worldRotationX.M33);

                #region Sterowanie kamera

                // Translation
                //Sterowanie kamera
                KeyboardState keyboardState = Keyboard.GetState();

                if (keyboardState.IsKeyDown(Keys.D))
                {
                    worldTranslation *= Matrix.CreateTranslation(-1 * speed, 0, 0);
                    whereIam += -1 * speed;
                }
                if (keyboardState.IsKeyDown(Keys.A))
                {
                    worldTranslation *= Matrix.CreateTranslation(speed, 0, 0);
                    whereIam += speed;
                }
                if (keyboardState.IsKeyDown(Keys.S))
                    worldTranslation *= Matrix.CreateTranslation(0, speed, 0);
                if (keyboardState.IsKeyDown(Keys.W))
                    worldTranslation *= Matrix.CreateTranslation(0, -1 * speed, 0);
                // ograniczenie zoomu, zeby nie przejsc przez plansze
                //Debug.WriteLine("Swiat: " + worldTranslation.Translation.ToString());
                //Debug.WriteLine("Kamera: " + camera.view.Translation.ToString());
                if (keyboardState.IsKeyDown(Keys.Q))
                {
                    if ((worldTranslation.Translation.Z <= -5.0f) && (worldTranslation.Translation.Z >= (-35.0f)))
                        worldTranslation *= Matrix.CreateTranslation(0, 0, -1 * speed);
                    else
                    {
                        if (worldTranslation.Translation.Z > -5.0f)
                            worldTranslation.Translation = new Vector3(worldTranslation.Translation.X, worldTranslation.Translation.Y, -5);
                        else if (worldTranslation.Translation.Z < -35.0f)
                            worldTranslation.Translation = new Vector3(worldTranslation.Translation.X, worldTranslation.Translation.Y, -35);
                    }
                }
                if (keyboardState.IsKeyDown(Keys.E))
                {
                    if ((worldTranslation.Translation.Z <= -5.0f) && (worldTranslation.Translation.Z >= (-35.0f)))
                        worldTranslation *= Matrix.CreateTranslation(0, 0, speed);
                    else
                    {
                        if (worldTranslation.Translation.Z > -5.0f)
                            worldTranslation.Translation = new Vector3(worldTranslation.Translation.X, worldTranslation.Translation.Y, -5);
                        else if (worldTranslation.Translation.Z < -35.0f)
                            worldTranslation.Translation = new Vector3(worldTranslation.Translation.X, worldTranslation.Translation.Y, -35);
                    }
                }
                if (keyboardState.IsKeyDown(Keys.Z))
                    worldRotationX *= Matrix.CreateRotationX(MathHelper.PiOver4 / 60);
                if (keyboardState.IsKeyDown(Keys.X))
                    worldRotationX *= Matrix.CreateRotationX(MathHelper.PiOver4 / -60);

                //ograniczenie zmiany nachylenia kamery (swiata?)
                if (camera.rotationAngleX < 0.0f)
                    worldRotationX = Matrix.CreateRotationX(0.0f);
                else if (camera.rotationAngleX > 0.6f)
                    worldRotationX = Matrix.CreateRotationX(-0.6f);

                #endregion Sterowanie kamera

                //reakcja na klikniÍcie myszy
                if (current.LeftButton == ButtonState.Pressed && previous.LeftButton == ButtonState.Released)
                {
                    if (Round.PutElement == false)
                    {
                        if (Components.Contains(board))
                        {
                            board.MapMouseAndRandNewBlock(GraphicsDevice, board.Effect, camera);
                            //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                            // tutaj warto by bylo jeszcze wyswietlic zamias nastepnego wylosowanego klocka rewers, a dopiero po rozpoczeciu tury przez nastepnego gracza wyswietlic nastepny klocek
                            //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                        }
                    }
                    else
                    {
                        //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!//
                        // tutaj trzeba dolozyc obsluge umieszczania pionka na planszy//
                        //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!//
                        if (CanStone)
                        {
                            CheckStone = true;
                            board.MapMouseAndRandNewBlock(GraphicsDevice, board.Effect, camera);
                            
                            //this.CanStone = false;
                            //CheckStone = false;
                        }
                    }
                }

                if (current.RightButton == ButtonState.Pressed && previous.RightButton == ButtonState.Released)
                {
                    if (Round.PutElement == false)
                        board.RotationBlock();
                }

                #region Przesuwanie kamery wzgledem myszy

                // przesuwanie kamery wzgledem myszy
                if (worldTranslation.Translation.X <= -1 && worldTranslation.Translation.X >= -37 && worldTranslation.Translation.Y <= 10 && worldTranslation.Translation.Y >= -38)
                {
                    if (camByExit.Intersects(new Rectangle(current.X, current.Y, 1, 1))) // wylacza przesuwanie, wokol przyciskow okna, X, maksym, minim
                    {
                        // nic nie rob
                    }
                    else if (current.X + 5 >= GraphicsDevice.Viewport.Width) //prawa
                    {
                        if (time > 300)
                        {
                            worldTranslation *= Matrix.CreateTranslation(-1 * speed, 0, 0);
                            whereIam += -1 * speed;
                        }
                    }
                    else if (current.X <= 0) //lewa
                    {
                        if (time > 300)
                        {
                            worldTranslation *= Matrix.CreateTranslation(speed, 0, 0);
                            whereIam += speed;
                        }
                    }
                    else if (current.Y + 5 >= GraphicsDevice.Viewport.Height) //dolna
                    {
                        if (time > 300)
                            worldTranslation *= Matrix.CreateTranslation(0, speed, 0);
                    }
                    else if (current.Y <= 0) //gorna
                    {
                        if (time > 300)
                            worldTranslation *= Matrix.CreateTranslation(0, -1 * speed, 0);
                    }
                    else time = 0;
                }
                else
                {
                    if (worldTranslation.Translation.X > -1)
                        worldTranslation.Translation = new Vector3(-1, worldTranslation.Translation.Y, worldTranslation.Translation.Z);
                    else if (worldTranslation.Translation.X < -37)
                        worldTranslation.Translation = new Vector3(-37, worldTranslation.Translation.Y, worldTranslation.Translation.Z);
                    else if (worldTranslation.Translation.Y > 10)
                        worldTranslation.Translation = new Vector3(worldTranslation.Translation.X, 10, worldTranslation.Translation.Z);
                    else if (worldTranslation.Translation.Y < -38)
                        worldTranslation.Translation = new Vector3(worldTranslation.Translation.X, -38, worldTranslation.Translation.Z);
                }

                previous = current;
                //Window.Title = "x: " + worldTranslation.Translation.X + " y: " + worldTranslation.Translation.Y + " z: " + worldTranslation.Translation.Z;

                #endregion Przesuwanie kamery wzgledem myszy

                // OBSLUGA TUR //
                if (keyboardState.IsKeyDown(Keys.Space) == true && previousKeyboard.IsKeyDown(Keys.Space) != true  && NewGameForm.IsDisposed)
                {
                    //putElement = false;
                    if (Round.EndRound(this) == true)
                    {
                        Round.NextTurn();
                    }
                    else
                    {
                        //if (Round.NumberOfActivePlayer < Round.NumberOfPlayers)
                        //    listOfPlayers[board._board[board.X][board.Y].player].PlayerColor += 1;
                        //else
                        //    listOfPlayers[board._board[board.X][board.Y].player].PlayerColor = 1;

                        //this.CanStone = true;
                    }
                }

                previousKeyboard = keyboardState;
                /////////////////
                
                //putElement = true;
                //this.CanStone = true;
                //if (keyboardState.IsKeyDown(Keys.Enter) == true)
                //{
                //if (Round.EndRound() == true)
                //{
                //    Round.NextTurn();
                //}
                //else
                //{
                //    //if (Round.NumberOfActivePlayer < Round.NumberOfPlayers)
                //    //    listOfPlayers[board._board[board.X][board.Y].player].PlayerColor += 1;
                //    //else
                //    //    listOfPlayers[board._board[board.X][board.Y].player].PlayerColor = 1;

                //    this.CanStone = true;
                //}
                //}
                /////////////////



                base.Update(gameTime);
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}