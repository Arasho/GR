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
        Texture2D menutlo;
        Game oknoGlowne { set; get; }
        public Menu(Game game): base(game)
        {
            oknoGlowne = game;
            spriteBatch = new SpriteBatch(this.Game.GraphicsDevice);
            menutlo = oknoGlowne.Content.Load<Texture2D>("menutlo");

<<<<<<< HEAD
=======
            tMenuBackground = mainWindow.Content.Load<Texture2D>("tlo");
            tNewGame = mainWindow.Content.Load<Texture2D>("nowagra");
>>>>>>> origin/HEAD
        }

        public override void Initialize()
        {
            base.Initialize();

        }

        public override void Update(GameTime gameTime)
        {
<<<<<<< HEAD
=======
            ms = Mouse.GetState();
            rNewGame = new Rectangle(mainWindow.GraphicsDevice.Viewport.Width / 2 - 180, mainWindow.GraphicsDevice.Viewport.Height/2-130, tNewGame.Width, tNewGame.Height);
            rMouse = new Rectangle(ms.X,ms.Y,1,1);
            

            if (ms.LeftButton == ButtonState.Pressed)
            {
                if (rNewGame.Intersects(rMouse))
                    ((Game1)Game).pressedNewGame = true;
            }
>>>>>>> origin/HEAD
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            spriteBatch.Begin();
            spriteBatch.Draw(menutlo, oknoGlowne.GraphicsDevice.Viewport.Bounds,Color.White);
            spriteBatch.End();
            
        }
    }
}
