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
    public class Menu : Microsoft.Xna.Framework.DrawableGameComponent
    {
        SpriteBatch spriteBatch;
        Texture2D tMenuBackground, tNewGame, tScores, tEnd;
        Game mainWindow { set; get; }
        Rectangle rNewGame, rScores, rEnd, rMouse;
        MouseState ms;

        public Menu(Game game): base(game)
        {
            mainWindow = game;
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);

            tMenuBackground = mainWindow.Content.Load<Texture2D>("tlo");
            tNewGame = mainWindow.Content.Load<Texture2D>("nowagra");
        }

        public override void Initialize()
        {
            base.Initialize();

        }

        public override void Update(GameTime gameTime)
        {
            ms = Mouse.GetState();
            rNewGame = new Rectangle(mainWindow.GraphicsDevice.Viewport.Width / 2 - 180, mainWindow.GraphicsDevice.Viewport.Height/2-130, tNewGame.Width, tNewGame.Height);
            rMouse = new Rectangle(ms.X,ms.Y,1,1);
            

            if (ms.LeftButton == ButtonState.Pressed)
            {
                if (rNewGame.Intersects(rMouse))
                    ((Game1)Game).pressedNewGame = true;
            }
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            
            //spriteBatch.Begin();
            spriteBatch.Begin();
            spriteBatch.Draw(tMenuBackground, mainWindow.GraphicsDevice.Viewport.Bounds, Color.White);
            spriteBatch.Draw(tNewGame, rNewGame, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
