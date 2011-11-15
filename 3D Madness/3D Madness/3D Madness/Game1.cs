using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace _3D_Madness
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        const float speed = 0.1f;

        // Movement and rotation stuff
        Matrix worldTranslation = Matrix.Identity;
        Matrix worldRotationX = Matrix.Identity;
        
        
        // Texture info
        Texture2D txt1;
        Texture2D txt2;
        VertexBuffer vertexBuffer;

        // Effect
        BasicEffect effect;

        Board test;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        // Game camera
        public Camera camera { get; set; }

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Window.AllowUserResizing = true;
            //graphics.ToggleFullScreen();
        }

        protected override void Initialize()
        {
            // Initialize camera
            camera = new Camera(this, Vector3.Zero, Vector3.Zero, Vector3.Up);
            // ustawienie pozycji poczatkowej swiata
            worldTranslation = Matrix.Add(worldTranslation, Matrix.CreateTranslation(new Vector3(-20, -20, -50)));
            Components.Add(camera);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            txt1 = Content.Load<Texture2D>(@"Textures\empty");
            txt2 = Content.Load<Texture2D>(@"Textures\Trees");
            
            // Set vertex data in VertexBuffer
            //vertexBuffer = new VertexBuffer(GraphicsDevice, typeof(VertexPositionTexture), verts.Length, BufferUsage.None);
            //vertexBuffer.SetData(verts);

            // Initialize the BasicEffect
            effect = new BasicEffect(GraphicsDevice);

            test = new Board(this, txt1, txt2);
        }

        protected override void UnloadContent() { }

        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            // Translation
            //Sterowanie kamera
            KeyboardState keyboardState = Keyboard.GetState();
            MouseState mouseState = Mouse.GetState();

            if (keyboardState.IsKeyDown(Keys.D))
                worldTranslation *= Matrix.CreateTranslation(-1 * speed, 0, 0);
            if (keyboardState.IsKeyDown(Keys.A))
                worldTranslation *= Matrix.CreateTranslation(speed, 0, 0);
            if (keyboardState.IsKeyDown(Keys.S))
                worldTranslation *= Matrix.CreateTranslation(0, speed, 0);
            if (keyboardState.IsKeyDown(Keys.W))
                worldTranslation *= Matrix.CreateTranslation(0, -1 * speed, 0);
            if (keyboardState.IsKeyDown(Keys.Q))
                worldTranslation *= Matrix.CreateTranslation(0, 0, -1 * speed);
            if (keyboardState.IsKeyDown(Keys.E))
                worldTranslation *= Matrix.CreateTranslation(0, 0, speed);
            if (keyboardState.IsKeyDown(Keys.Z))
                worldRotationX *= Matrix.CreateRotationX(MathHelper.PiOver4 / 60);
            if (keyboardState.IsKeyDown(Keys.X))
                worldRotationX *= Matrix.CreateRotationX(MathHelper.PiOver4 / -60);

            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                Vector3 nearSource = new Vector3(new Vector2(Mouse.GetState().X, Mouse.GetState().Y), 0f);
                Vector3 farSource = new Vector3(new Vector2(Mouse.GetState().X, Mouse.GetState().Y), 1f);
                Vector3 nearPoint = GraphicsDevice.Viewport.Unproject(nearSource,
               effect.Projection, effect.View, Matrix.Identity);

                Vector3 farPoint = GraphicsDevice.Viewport.Unproject(farSource,
                     camera.projection, camera.view, Matrix.Identity);
                Vector3 direction = farPoint - nearPoint;
                direction.Normalize();

                Ray xRay = new Ray(nearPoint, direction);
                Window.Title = xRay.Intersects(new BoundingSphere(new Vector3(0.5f, 0.5f, 0.5f), 0.5f)).ToString();

                //Window.Title = xRay.Intersects(new BoundingSphere(new Vector3(0,0,0), 1f)).ToString();
            }


            // Wazne do obracania klocka 
            // Rotation
            //worldRotation *= Matrix.CreateFromYawPitchRoll(
            //    MathHelper.PiOver4 / 60,
            //    0,
            //    0);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // Set the vertex buffer on the GraphicsDevice
            GraphicsDevice.SetVertexBuffer(vertexBuffer);

            //Set object and camera info

            // po co ten pierwszy worldRotation?
            //camera.view = worldRotation * worldTranslation * worldRotation;
            camera.view = worldTranslation * worldRotationX;

            effect.View = camera.view;
            effect.Projection = camera.projection;

            effect.TextureEnabled = true;

            // Begin effect and draw for each pass
            foreach (EffectPass pass in effect.CurrentTechnique.Passes)
            {
                foreach (var item in test.element)
                {
                    pass.Apply();
                    effect.Texture = item.Texture;

                    GraphicsDevice.SamplerStates[0] = SamplerState.LinearClamp; 
                    GraphicsDevice.DrawUserPrimitives<VertexPositionTexture>
                   (PrimitiveType.TriangleStrip, item.verts, 0, 2);
                }
            }
            base.Draw(gameTime);
        }
    }
}