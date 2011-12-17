using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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
        private Texture2D[] playerName = new Texture2D[temporaryPlayerQuantity];

        private Game1 mainGameClass { get; set; }

        public Rectangle wholeBar { get; set; }

        private Vector2 origin = new Vector2();

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
                wholeBar = new Rectangle(0, 0, 160, mainGameClass.GraphicsDevice.Viewport.Height);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spritebatch.Begin();
            spritebatch.Draw(playerName[0], wholeBar, Color.White);

            if (mainGameClass.board.NextBlock != mainGameClass.board.txt1)
            {
                origin.X = mainGameClass.board.NextBlock.Width / 2;
                origin.Y = mainGameClass.board.NextBlock.Height / 2;

                // bloczek
                spritebatch.Draw(mainGameClass.board.NextBlock, new Rectangle(80, 80, wholeBar.Width, wholeBar.Width),
                                    null, Color.White, mainGameClass.board.numberOfRotation % 4 * -90 * (MathHelper.Pi / 180),
                                    origin, SpriteEffects.None, 0
                                );
            }

            for (int i = 0; i < mainGameClass.listOfPlayers.Count; i++)
            {
                // userbar
                spritebatch.Draw(playerName[i], new Rectangle(10, 100 * (i + 1) + 100, wholeBar.Width - 20, playerName[i].Height), Color.White);
                // nick
                spritebatch.DrawString(font, mainGameClass.listOfPlayers[i].PlayerName, new Vector2(15, 100 * (i + 1) + 105), Color.Black);
            }

            spritebatch.End();
            base.Draw(gameTime);
        }
    }
}