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
        Game1 mainGameClass { set; get; }
        Rectangle rNewGame, rScores, rEnd, rMouse;
        MouseState ms;
        public Menu(Game game): base(game)
        {
            mainGameClass = (Game1)game;
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);

            tMenuBackground = mainGameClass.Content.Load<Texture2D>("tlo");
            tNewGame = mainGameClass.Content.Load<Texture2D>("nowagra");
            tScores = mainGameClass.Content.Load<Texture2D>("wyniki");
            tEnd = mainGameClass.Content.Load<Texture2D>("koniec");
        }

        public override void Initialize()
        {
            base.Initialize();

        }

        public override void Update(GameTime gameTime)
        {
            ms = Mouse.GetState();
            rNewGame = new Rectangle(mainGameClass.GraphicsDevice.Viewport.Width / 2 - 180, mainGameClass.GraphicsDevice.Viewport.Height / 2 - 130, tNewGame.Width, tNewGame.Height);
            rScores = new Rectangle(mainGameClass.GraphicsDevice.Viewport.Width / 2 - 180, mainGameClass.GraphicsDevice.Viewport.Height / 2 - 30, tScores.Width, tScores.Height);
            rEnd = new Rectangle(mainGameClass.GraphicsDevice.Viewport.Width / 2 - 180, mainGameClass.GraphicsDevice.Viewport.Height / 2 + 70, tEnd.Width, tEnd.Height);
            rMouse = new Rectangle(ms.X,ms.Y,1,1);
            

            if (ms.LeftButton == ButtonState.Pressed)
            {
                if (rNewGame.Intersects(rMouse))
                    mainGameClass.pressedNewGame = true;
                if (rEnd.Intersects(rMouse))
                    mainGameClass.pressedTheEnd = true;
                    
            }
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            
            //spriteBatch.Begin();
            spriteBatch.Begin();
            spriteBatch.Draw(tMenuBackground, mainGameClass.GraphicsDevice.Viewport.Bounds, Color.White);
            spriteBatch.Draw(tNewGame, rNewGame, Color.White);
            spriteBatch.Draw(tScores, rScores, Color.White);
            spriteBatch.Draw(tEnd, rEnd, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
