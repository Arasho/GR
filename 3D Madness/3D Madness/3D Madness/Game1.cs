using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
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

        // Effect
        BasicEffect effect;

        //Board
        Board board;

        // Generate element
        XML_Parser rand_element;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        // Game camera
        public Camera camera { get; set; }


        // Menu
        public bool menu;
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
    
            // Initialize the BasicEffect
            effect = new BasicEffect(GraphicsDevice);

            // Za³adowanie pustej planszy
            board = new Board(this, txt1, txt2);

            // Za³adowanie parsera 
            rand_element = new XML_Parser();
            rand_element.XDocParse();

        }

        protected override void UnloadContent() { }

        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            // katy rotacji swiata z macierzy rotacji, w tej chwili posiadamy rotacje wzgledem X, ale na przyszlosc gdybysmy potrzebowali
            // to sa tez wzgledem Y i Z
            // wg. strony -> http://www.codeguru.com/forum/archive/index.php/t-329530.html

            // camera.rotationAngleY = (-1) * Math.Asin(worldRotationY.M31);
            // camera.rotationAngleZ = Math.Atan2(worldRotationZ.M21, worldRotationZ.M11);
            

            camera.rotationAngleX = Math.Atan2(worldRotationX.M32, worldRotationX.M33);

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
            
            if (camera.rotationAngleX >= 0.0f && camera.rotationAngleX <= 0.6f)
            {
                if (keyboardState.IsKeyDown(Keys.Z))
                {
                    worldRotationX *= Matrix.CreateRotationX(MathHelper.PiOver4 / 60);
                }
                if (keyboardState.IsKeyDown(Keys.X))
                {
                    worldRotationX *= Matrix.CreateRotationX(MathHelper.PiOver4 / -60);
                }
            }
            else if (camera.rotationAngleX < 0.0f)
                worldRotationX = Matrix.CreateRotationX(0.0f);
            else if (camera.rotationAngleX > 0.6f)
                worldRotationX = Matrix.CreateRotationX(-0.6f);
            


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

                float help = .0f;
                for (float i = 0; i < 20; i = i + 1.0f)
                {
                    for (float j = 0; j < 20; j = j + 1.0f)
                    {
                        if (xRay.Intersects(new BoundingBox(new Vector3(i, j, 0), new Vector3(i+1, j+1, 0))) > 0f)
                        {
                            Window.Title = "x: " + i + "     y " + j;

                            
                            
                             board.janek[(int)i][(int)j].Texture = txt2;
                        }
                    }
                 
                }
                 
                // patrz dokladnie ale to akurat mankament nie o to chodzi ale odrazu poka¿e
                //Window.Title = xRay.Intersects(new BoundingSphere(new Vector3(0,0,0), 1f)).ToString();
            }


            // Wazne do obracania klocka 
            // Rotation
            //worldRotation *= Matrix.CreateFromYawPitchRoll(0
            //    MathHelper.PiOver4 / 60,
            //    0,
            //    0);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // po co ten pierwszy worldRotation?
            //camera.view = worldRotation * worldTranslation * worldRotation;
            camera.view = worldTranslation * worldRotationX;

            effect.View = camera.view;
            effect.Projection = camera.projection;

            effect.TextureEnabled = true;

            // Begin effect and draw for each pass
            foreach (EffectPass pass in effect.CurrentTechnique.Passes)
            {
                //foreach (var item in test.element)
                //{
                //    pass.Apply();
                //    effect.Texture = item.Texture;

                //    //   GraphicsDevice.SamplerStates[0] = SamplerState.LinearClamp; 
                //    GraphicsDevice.DrawUserPrimitives<VertexPositionTexture>
                //   (PrimitiveType.TriangleStrip, item.verts, 0, 2);
                //}


                for (int i = 0; i < 20; i++)
                {
                    for (int j = 0; j < 20; j++)
                    {
                        pass.Apply();
                        effect.Texture = board.janek[i][j].Texture;

                        //   GraphicsDevice.SamplerStates[0] = SamplerState.LinearClamp; 
                        GraphicsDevice.DrawUserPrimitives<VertexPositionTexture>
                       (PrimitiveType.TriangleStrip, board.janek[i][j].verts, 0, 2);
                    }
                }
            }  
            
            base.Draw(gameTime);
        }
    }
}