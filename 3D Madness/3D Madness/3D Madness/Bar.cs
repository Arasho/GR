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
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class Bar : Microsoft.Xna.Framework.DrawableGameComponent
    {
        private const int temporaryPlayerQuantity = 5;

        private SpriteBatch spritebatch;
        private SpriteFont font;
        private Texture2D [] playerName = new Texture2D[temporaryPlayerQuantity];
        private Game1 mainGameClass { get; set; }
        private Rectangle wholeBar;

        public Bar(Game game)
            : base(game)
        {
            mainGameClass = (Game1)game;
            spritebatch = new SpriteBatch(game.GraphicsDevice);
            font = game.Content.Load<SpriteFont>("font");

            for (int i = 0; i < temporaryPlayerQuantity; i++)
                playerName[i] = game.Content.Load<Texture2D>("belkagracza");
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            if (mainGameClass.graphics.IsFullScreen)
                wholeBar = new Rectangle(0, 0, (int)(mainGameClass.GraphicsDevice.Viewport.Width * 0.14f), mainGameClass.GraphicsDevice.Viewport.Height);
            else
                wholeBar = new Rectangle(0, 0, (int)(mainGameClass.GraphicsDevice.Viewport.Width * 0.22f), mainGameClass.GraphicsDevice.Viewport.Height);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spritebatch.Begin();
                spritebatch.Draw(playerName[0], wholeBar, Color.Blue);

                if (mainGameClass.board.NextBlock != mainGameClass.board.txt1)
                    spritebatch.Draw(((Game1)mainGameClass).board.NextBlock, new Rectangle(10, 10, 160, 160), Color.White);

                

                for (int i = 0; i < temporaryPlayerQuantity; i++)
                {
                    spritebatch.Draw(playerName[i], new Vector2(10, 100 * (i+1) + 90), Color.White);
                    spritebatch.DrawString(font, "Gracz " + (i + 1), new Vector2(15, 100 * (i+1) + 95), Color.Black);
                }

            spritebatch.End();
            base.Draw(gameTime);
        }
    }
}
