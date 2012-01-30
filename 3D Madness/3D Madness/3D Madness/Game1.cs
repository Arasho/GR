using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace _3D_Madness
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        private FormPlayers NewGameForm = new FormPlayers();
        private const float speed = 0.1f;
        private double time;
        private Rectangle camByExit;
        private Matrix worldTranslation = Matrix.Identity;
        private Matrix worldRotationX = Matrix.Identity;
        private Texture2D txt1;
        private Texture2D txt2;
        public Texture2D czarnymodel, zoltymodel, czerwonymodel, zielonymodel, niebieskimodel;
        public bool putElement = false;
        public float whereIam = 0.0f;
        public GraphicsDeviceManager graphics { get; set; }
        public static List<Player> listOfPlayers = new List<Player>();
        public static List<String> wyniki = new List<string>(listOfPlayers.Count);
        private SpriteBatch spriteBatch;
        private KeyboardState previousKeyboard;
        private  MouseState current, previous;
        public KeyboardState kbprevious;
        public bool startNewGame { get; set; }
        public bool playersSetings { get; set; }
        public bool pressedNewGame { get; set; }
        public bool pressedResults { get; set; }
        public bool pressedTheEnd { get; set; }
        public bool pressedBackFromResults { get; set; }

        // Game camera
        public Camera camera { get; set; }
        public Board board { get; set; }
        public Menu menu { get; set; }
        public Bar infoBar { get; set; }
        public Model3D model3D { get; set; }
        public Results results { get; set; }
        public bool CheckStone = false;
        public bool CanStone = false;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Window.AllowUserResizing = true;
            pressedNewGame = false;
            pressedResults = false;
            pressedTheEnd = false;
            pressedBackFromResults = false;
            startNewGame = false;
            playersSetings = false;
            time = 0;
            System.Windows.Forms.Form form = (System.Windows.Forms.Form)System.Windows.Forms.Form.FromHandle(Window.Handle);
            form.Location = new System.Drawing.Point(0, 0);
            form.Size = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Size;
            graphics.PreferredBackBufferHeight = form.Size.Height;
            graphics.PreferredBackBufferWidth = form.Size.Width;
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
            results = new Results(this);

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
            
                if (pressedNewGame)
                    initializeGame();

                if (pressedResults)
                    initializeResults();

                if (pressedBackFromResults)
                    backFromResults();

                checkForExit();                

                #region Sterowanie kamera  
                steerCameraViaKeyboard(Keyboard.GetState());
                implementMouseFunctionalities();
                steerCameraViaMouse();
                limitCameraAngle();                
                #endregion Sterowanie kamera

                affectCamera();
                
                base.Update(gameTime);
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }


        /// <summary>
        /// Update camera position.
        /// </summary>
        private void affectCamera() {
            camera.view = worldTranslation * worldRotationX;
            board.Effect.View = camera.view;
            board.Effect.Projection = camera.projection;
            board.Effect.TextureEnabled = true;
            camera.rotationAngleX = Math.Atan2(worldRotationX.M32, worldRotationX.M33);
        }

        /// <summary>
        /// Implementation of camera steering via mouse movement, especially on screen edges.
        /// </summary>
        private void steerCameraViaMouse() {
            // przesuwanie kamery wzgledem myszy
            if (worldTranslation.Translation.X <= -1 && worldTranslation.Translation.X >= -37 && worldTranslation.Translation.Y <= 10 && worldTranslation.Translation.Y >= -38) {
                if (camByExit.Intersects(new Rectangle(current.X, current.Y, 1, 1))) // wylacza przesuwanie, wokol przyciskow okna, X, maksym, minim
                    {
                    // nic nie rob
                } else if (current.X + 5 >= GraphicsDevice.Viewport.Width) //prawa
                    {
                    if (time > 300) {
                        worldTranslation *= Matrix.CreateTranslation(-1 * speed, 0, 0);
                        whereIam += -1 * speed;
                    }
                } else if (current.X <= 0) //lewa
                    {
                    if (time > 300) {
                        worldTranslation *= Matrix.CreateTranslation(speed, 0, 0);
                        whereIam += speed;
                    }
                } else if (current.Y + 5 >= GraphicsDevice.Viewport.Height) //dolna
                    {
                    if (time > 300)
                        worldTranslation *= Matrix.CreateTranslation(0, speed, 0);
                } else if (current.Y <= 0) //gorna
                    {
                    if (time > 300)
                        worldTranslation *= Matrix.CreateTranslation(0, -1 * speed, 0);
                } else time = 0;
            } else {
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
        }

        /// <summary>
        /// Implementation of all mouse buttons and behaviors.
        /// </summary>
        private void implementMouseFunctionalities() {
            if (isLeftButtonClicked()) {
                if (Round.PutElement == false) {
                    if (Components.Contains(board)) {
                        board.MapMouseAndRandNewBlock(GraphicsDevice, board.Effect, camera);
                    }
                } else {
                    if (CanStone) {
                        CheckStone = true;
                        board.MapMouseAndRandNewBlock(GraphicsDevice, board.Effect, camera);
                    }
                }
            }
            if (isRightButtonClicked()) {
                if (Round.PutElement == false)
                    board.RotationBlock();
            }
        }

        /// <summary>
        /// Implementation of steering camera via keyboard.
        /// </summary>
        /// <param name="keyboardState">The current state of keyboard</param>

        private void steerCameraViaKeyboard(KeyboardState keyboardState) {
            if (keyboardState.IsKeyDown(Keys.D)) {
                worldTranslation *= Matrix.CreateTranslation(-1 * speed, 0, 0);
                whereIam += -1 * speed;
            }
            if (keyboardState.IsKeyDown(Keys.A)) {
                worldTranslation *= Matrix.CreateTranslation(speed, 0, 0);
                whereIam += speed;
            }
            if (keyboardState.IsKeyDown(Keys.S)) {
                worldTranslation *= Matrix.CreateTranslation(0, speed, 0);
            }
            if (keyboardState.IsKeyDown(Keys.W)) {
                worldTranslation *= Matrix.CreateTranslation(0, -1 * speed, 0);
            }
            if (keyboardState.IsKeyDown(Keys.Q)) {
                zoomBoard(speed * -1); // zoom out
            }
            if (keyboardState.IsKeyDown(Keys.E)) {
                zoomBoard(speed); // zoom in
            }
            if (keyboardState.IsKeyDown(Keys.Z)) {
                worldRotationX *= Matrix.CreateRotationX(MathHelper.PiOver4 / 60);
            }
            if (keyboardState.IsKeyDown(Keys.X)) {
                worldRotationX *= Matrix.CreateRotationX(MathHelper.PiOver4 / -60);
            }
            if (keyboardState.IsKeyDown(Keys.Space) == true && previousKeyboard.IsKeyDown(Keys.Space) != true && NewGameForm.IsDisposed) {
                if (Round.EndRound(this) == true) {
                    Round.NextTurn();
                    // nie sprawdzaj pionka gracza pierwszego przy turze gracza drugiego
                    CheckStone = false;
                }
            }
            previousKeyboard = keyboardState;
        }

        /// <summary>
        /// Implement ability to exit via Escape key.
        /// </summary>
        private void checkForExit() {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();
            if (pressedTheEnd)
                this.Exit();
        }

        /// <summary>
        /// Initialization of the game. This method should be called on start.
        /// </summary>
        private void initializeGame() {
            if (playersSetings == false) {
                NewGameForm.Show();
                playersSetings = true;
            }
            if (NewGameForm.formClose == true) {
                initPlayers();
                NewGameForm.Close();
                Components.Remove(menu);
                Components.Add(board);
                Components.Add(infoBar);
                pressedNewGame = false;
            }
        }

        private void initializeResults()
        {
            Components.Remove(menu);
            Components.Add(results);
            pressedResults = false;
        }

        private void backFromResults()
        {
            Components.Remove(results);
            Components.Add(menu);
            pressedBackFromResults = false;
        }

        /// <summary>
        /// Checks whether right button of the mouse is clicked.
        /// </summary>
        /// <returns>true if clicked, false if not</returns>
        private bool isRightButtonClicked() {
            return current.RightButton == ButtonState.Pressed && previous.RightButton == ButtonState.Released;
        }

        /// <summary>
        /// Checks whether left button of the mouse is clicked.
        /// </summary>
        /// <returns>true if clicked, false if not</returns>
        private bool isLeftButtonClicked() {
            return current.LeftButton == ButtonState.Pressed && previous.LeftButton == ButtonState.Released;
        }

        /// <summary>
        /// Initialization of all players.
        /// </summary>
        private void initPlayers() {
            for (int i = 0; i < NewGameForm.numberOfPlayers; i++) {
                listOfPlayers.Add(new Player(NewGameForm.namesOfPlayers[i], NewGameForm.colorsOfPlayers[i]));
            }
            Round.NumberOfPlayers = listOfPlayers.Count;
            Round.NumberOfActivePlayer = 1;
        }

        /// <summary>
        /// Creates angle limits for the camera.
        /// </summary>
        private void limitCameraAngle() {
            //ograniczenie zmiany nachylenia kamery (swiata?)
            if (camera.rotationAngleX < 0.0f)
                worldRotationX = Matrix.CreateRotationX(0.0f);
            else if (camera.rotationAngleX > 0.6f)
                worldRotationX = Matrix.CreateRotationX(-0.6f);
        }

        /// <summary>
        /// Zooms board
        /// </summary>
        /// <param name="speed">how fast should board be zoomed</param>
        private void zoomBoard(float speed) {
            // ograniczenie zoomu, zeby nie przejsc przez plansze
            if ((worldTranslation.Translation.Z <= -5.0f) && (worldTranslation.Translation.Z >= (-35.0f)))
                worldTranslation *= Matrix.CreateTranslation(0, 0, speed);
            else {
                if (worldTranslation.Translation.Z > -5.0f)
                    worldTranslation.Translation = new Vector3(worldTranslation.Translation.X, worldTranslation.Translation.Y, -5);
                else if (worldTranslation.Translation.Z < -35.0f)
                    worldTranslation.Translation = new Vector3(worldTranslation.Translation.X, worldTranslation.Translation.Y, -35);
            }
        }
    }
}