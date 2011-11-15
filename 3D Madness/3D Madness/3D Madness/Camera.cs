using Microsoft.Xna.Framework;

namespace _3D_Madness
{
    public class Camera : Microsoft.Xna.Framework.GameComponent
    {
        public Matrix view { get; set; }
        public Matrix projection { get; set; }

        public Camera(Game game, Vector3 pos, Vector3 target, Vector3 up)
            : base(game)
        {
            // Initialize view matrix
            view = Matrix.CreateLookAt(pos, target, up);

            // Initialize projection matrix
            projection = Matrix.CreatePerspectiveFieldOfView(
                MathHelper.PiOver4,
                (float)Game.Window.ClientBounds.Width / (float)Game.Window.ClientBounds.Height,
                1, 
                100);
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}