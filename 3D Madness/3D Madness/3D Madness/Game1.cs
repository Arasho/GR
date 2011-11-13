using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace _3D_Madness
{
   /// <summary>
   /// This is the main type for your game
   /// </summary>
   public class Game1 : Microsoft.Xna.Framework.Game
   {
       GraphicsDeviceManager graphics;
       SpriteBatch spriteBatch;

       // Game camera
       public Camera camera { get; set; }

       VertexBuffer vertexBuffer;

       // Effect
       BasicEffect effect;

       Board test = new Board();

       // Movement and rotation stuff
       Matrix worldTranslation = Matrix.Identity;
       Matrix worldRotation = Matrix.Identity;

       // Texture info
       Texture2D texture;

       public Game1()
       { 
           graphics = new GraphicsDeviceManager(this);
           Content.RootDirectory = "Content";
           IsMouseVisible = true;
       }

       protected override void Initialize()
       {
           // Initialize camera
           camera = new Camera(this, new Vector3(0, 0, 5),
               Vector3.Zero, Vector3.Up);
           Components.Add(camera);


           base.Initialize();
       }
       

       protected override void LoadContent()
       {
           // Create a new SpriteBatch, which can be used to draw textures.
           spriteBatch = new SpriteBatch(GraphicsDevice);
           
           // Set vertex data in VertexBuffer
           //vertexBuffer = new VertexBuffer(GraphicsDevice, typeof(VertexPositionTexture), verts.Length, BufferUsage.None);
           //vertexBuffer.SetData(verts);

           // Initialize the BasicEffect
           effect = new BasicEffect(GraphicsDevice);

           // Load texture
           texture = Content.Load<Texture2D>(@"Textures\empty");
          
       }

       /// <summary>
       /// UnloadContent will be called once per game and is the place to unload
       /// all content.
       /// </summary>
       protected override void UnloadContent()
       {
           // TODO: Unload any non ContentManager content here
       }

       /// <summary>
       /// Allows the game to run logic such as updating the world,
       /// checking for collisions, gathering input, and playing audio.
       /// </summary>
       /// <param name="gameTime">Provides a snapshot of timing values.</param>
       protected override void Update(GameTime gameTime)
       {
           // Allows the game to exit
           if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
               this.Exit();

           // Translation
            //Sterowanie kamer¹
           KeyboardState keyboardState = Keyboard.GetState();
           MouseState mouseState = Mouse.GetState();
           if (keyboardState.IsKeyDown(Keys.A))
               worldTranslation *= Matrix.CreateTranslation(-.05f, 0, 0);
           if (keyboardState.IsKeyDown(Keys.D))
               worldTranslation *= Matrix.CreateTranslation(.05f, 0, 0);
           if (keyboardState.IsKeyDown(Keys.W))
               worldTranslation *= Matrix.CreateTranslation(0, .05f, 0);
           if (keyboardState.IsKeyDown(Keys.S))
               worldTranslation *= Matrix.CreateTranslation(0, -.05f, 0);
           if (keyboardState.IsKeyDown(Keys.Q))
               worldTranslation *= Matrix.CreateTranslation(0, 0, -0.5f); 
           if (keyboardState.IsKeyDown(Keys.E))
               worldTranslation *= Matrix.CreateTranslation(0, 0, 0.5f);
           if (keyboardState.IsKeyDown(Keys.Z))
               worldRotation *= Matrix.CreateRotationX(MathHelper.PiOver4 / 60);
           if (keyboardState.IsKeyDown(Keys.X))
               worldRotation *= Matrix.CreateRotationX(MathHelper.PiOver4 / -60);

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
               Window.Title = xRay.Intersects(new BoundingSphere(new Vector3(0, 0, 0), 1f)).ToString();

               Window.Title = xRay.Intersects(new BoundingSphere(new Vector3(0,0,0), 1f)).ToString();

               
           
           }

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
           camera.view = worldRotation * worldTranslation * worldRotation;
           effect.View = camera.view;
           effect.Projection = camera.projection;
           effect.Texture = texture;
           effect.TextureEnabled = true;

           // Begin effect and draw for each pass
           foreach (EffectPass pass in effect.CurrentTechnique.Passes)
           {
               pass.Apply();

               foreach (var item in test.element)
               {
                   GraphicsDevice.DrawUserPrimitives<VertexPositionTexture>
                  (PrimitiveType.TriangleStrip, item.verts, 0, 2);
               }       
           }
           base.Draw(gameTime);
       }
   }
}
