using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace _3D_Madness
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        const float speed = 0.1f;
        double time;
        private Rectangle camByExit;
        Matrix worldTranslation = Matrix.Identity;
        Matrix worldRotationX = Matrix.Identity;

        Texture2D txt1;
        Texture2D txt2;

        public GraphicsDeviceManager graphics { get; set; }

        SpriteBatch spriteBatch;
        MouseState current, previous;

        public bool pressedNewGame { get; set; }

        public bool pressedTheEnd { get; set; }

        // Game camera
        public Camera camera { get; set; }

        public Board board { get; set; }

        private Menu menu;

        public Bar infoBar { get; set; }

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Window.AllowUserResizing = true;
            pressedNewGame = false;
            pressedTheEnd = false;
            time = 0;
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

            txt1 = Content.Load<Texture2D>(@"Textures\empty");
            txt2 = Content.Load<Texture2D>(@"Textures\Trees");

            menu = new Menu(this);

            // Zaladowanie pustej planszy
            board = new Board(this, txt1, txt2);
            infoBar = new Bar(this);

            Components.Add(menu);
        }

        protected override void UnloadContent() { }

        protected override void Update(GameTime gameTime)
        {
            time += gameTime.ElapsedGameTime.TotalMilliseconds;
            camByExit = new Rectangle(GraphicsDevice.Viewport.Width - 110, -22, GraphicsDevice.Viewport.Width, 30);
            current = Mouse.GetState();
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

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

            // Translation
            //Sterowanie kamera
            KeyboardState keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.D))
                worldTranslation *= Matrix.CreateTranslation(-1 * speed, 0, 0);
            if (keyboardState.IsKeyDown(Keys.A))
                worldTranslation *= Matrix.CreateTranslation(speed, 0, 0);
            if (keyboardState.IsKeyDown(Keys.S))
                worldTranslation *= Matrix.CreateTranslation(0, speed, 0);
            if (keyboardState.IsKeyDown(Keys.W))
                worldTranslation *= Matrix.CreateTranslation(0, -1 * speed, 0);
            // ograniczenie zoomu, zeby nie przejsc przez plansze
            if (keyboardState.IsKeyDown(Keys.Q))
            {
                if ((camera.view.Translation.Z > (-35.0f)))
                    worldTranslation *= Matrix.CreateTranslation(0, 0, -1 * speed);
            }
            if (keyboardState.IsKeyDown(Keys.E))
            {
                if ((camera.view.Translation.Z < (-5.0f)))
                    worldTranslation *= Matrix.CreateTranslation(0, 0, speed);
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

            //reakcja na klikniÍcie myszy
            if (current.LeftButton == ButtonState.Pressed && previous.LeftButton == ButtonState.Released)
            {
                if (Components.Contains(board))
                    board.MapMouseAndRandNewBlock(GraphicsDevice, board.Effect, camera);
            }

            if (current.RightButton == ButtonState.Pressed && previous.RightButton == ButtonState.Released)
            {
                board.RotationBlock();
            }

            // przesuwanie kamery wzgledem myszy

            if (camByExit.Intersects(new Rectangle(current.X, current.Y, 1, 1))) // wylacza przesuwanie, wokol przyciskow okna, X, maksym, minim
            {
                // nic nie rob
            }
            else if (current.X + 5 >= GraphicsDevice.Viewport.Width) //prawa
            {
                if (time > 300)
                    worldTranslation *= Matrix.CreateTranslation(-1 * speed, 0, 0);
            }
            else if (current.X <= 0) //lewa
            {
                if (time > 300)
                    worldTranslation *= Matrix.CreateTranslation(speed, 0, 0);
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

            previous = current;
            Window.Title = "X: " + current.X + " Y: " + current.Y;
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            if (pressedNewGame)
            {
                Components.Remove(menu);
                Components.Add(board);
                Components.Add(infoBar);
                pressedNewGame = false;
                //spriteBatch.Begin();
                //spriteBatch.Draw(infoBar.playerName[0], camByExit, Color.Orange);
                //spriteBatch.End();
            }

            base.Draw(gameTime);
        }
    }
}