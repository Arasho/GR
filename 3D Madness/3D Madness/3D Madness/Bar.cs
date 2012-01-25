using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace _3D_Madness
{
    public class Bar : Microsoft.Xna.Framework.DrawableGameComponent
    {
        private const int temporaryPlayerQuantity = 5;

        private SpriteBatch spritebatch;
        private SpriteFont font;
        private Texture2D[] playerName = new Texture2D[temporaryPlayerQuantity];

        private Game1 mainGameClass { get; set; }

        public Rectangle wholeBar { get; set; }

        private Vector2 origin = new Vector2();

        public MouseState ms { get; set; }

        public Rectangle[] stoneArea { get; set; }

        public int[] numberOfStone { get; set; }

        public Bar(Game game)
            : base(game)
        {
            mainGameClass = (Game1)game;
            spritebatch = new SpriteBatch(game.GraphicsDevice);
            font = game.Content.Load<SpriteFont>("font");

            for (int i = 0; i < temporaryPlayerQuantity; i++)
                playerName[i] = game.Content.Load<Texture2D>("belkagracza");

            stoneArea = new Rectangle[temporaryPlayerQuantity];
            numberOfStone = new int[temporaryPlayerQuantity];
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            for (int i = 0; i < stoneArea.Length; i++)
            {
                stoneArea[i] = new Rectangle(10, 100 * (i + 1) + 150, 20, 20);
            }

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

            for (int i = 0; i < Game1.listOfPlayers.Count; i++)
            {
                // userbar
                if (Game1.listOfPlayers[Round.NumberOfActivePlayer - 1].PlayerName == Game1.listOfPlayers[i].PlayerName)
                {
                    spritebatch.Draw(playerName[i], new Rectangle(10, 100 * (i + 1) + 100, wholeBar.Width - 20, playerName[i].Height), Color.Brown);
                    spritebatch.DrawString(font, Game1.listOfPlayers[i].PlayerName, new Vector2(15, 100 * (i + 1) + 105), Color.White);
                }
                else
                {
                    spritebatch.Draw(playerName[i], new Rectangle(10, 100 * (i + 1) + 100, wholeBar.Width - 20, playerName[i].Height), Color.White);
                    spritebatch.DrawString(font, Game1.listOfPlayers[i].PlayerName, new Vector2(15, 100 * (i + 1) + 105), Color.Black);
                }

                // pionki
                spritebatch.DrawString(font, Game1.listOfPlayers[i].NumberOfLittlePowns.ToString() + " x ", new Vector2(15, 100 * (i + 1) + 145), Color.Black);
                //spritebatch.Draw(
            }

            spritebatch.End();
            base.Draw(gameTime);
        }
    }
}