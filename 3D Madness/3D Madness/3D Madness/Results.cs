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
    public class Results : Microsoft.Xna.Framework.DrawableGameComponent
    {
        SpriteBatch spriteBatch;
        Texture2D tMenuBackground;
        Texture2D background_results;
        Texture2D back;
        private SpriteFont font;
        Game1 mainGameClass { set; get; }
        public string text { get; set; }

        Rectangle rect { get; set; }
        Rectangle rect_back { get; set; }
        Rectangle rMouse { get; set; }
        MouseState ms;
        public Results(Game game) : base(game)
        {
            mainGameClass = (Game1)game;
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);
            font = game.Content.Load<SpriteFont>("font");
            tMenuBackground = mainGameClass.Content.Load<Texture2D>("tlo");
            back = mainGameClass.Content.Load<Texture2D>("back");
            background_results = mainGameClass.Content.Load<Texture2D>(@"Textures\background_results");
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            ms = Mouse.GetState();
            rect = new Rectangle(100, 200, mainGameClass.GraphicsDevice.Viewport.Width - 200, mainGameClass.GraphicsDevice.Viewport.Height - 300);
            rect_back = new Rectangle(100, mainGameClass.GraphicsDevice.Viewport.Height - 160, back.Width/2, back.Height/2);

            rMouse = new Rectangle(ms.X, ms.Y, 1, 1);
            if (ms.LeftButton == ButtonState.Pressed)
            {
                if (rect_back.Intersects(rMouse))
                    mainGameClass.pressedBackFromResults = true;
            }
            // Wczytaj wyniki
            text = System.IO.File.ReadAllText(@"wyniki.txt");

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
           // spritebatch.Draw(playerName[0], wholeBar, Color.White);
            
            spriteBatch.Draw(tMenuBackground, mainGameClass.GraphicsDevice.Viewport.Bounds, Color.White);
            spriteBatch.Draw(background_results, rect, Color.White);
            spriteBatch.DrawString(font, text, new Vector2(110, 210), Color.Black);
            spriteBatch.Draw(back, rect_back, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
