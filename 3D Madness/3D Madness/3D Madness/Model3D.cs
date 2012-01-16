using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _3D_Madness
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class Model3D : Microsoft.Xna.Framework.DrawableGameComponent
    {
        Model myModel;
        const float speed = 0.1f;
        // The aspect ratio determines how to scale 3d to 2d projection.
        float aspectRatio;
        private SpriteBatch spritebatch;

        private Game1 mainGameClass { get; set; }

        public float X { get; set; }

        public float Y { get; set; }

        public float moveMouseX { get; set; }

        public float moveMouseY { get; set; }

        float x = 20.0f;
        float y = 20.0f;
        float z = .0f;
        public Vector3 modelPosition;
        float modelRotation = 10.0f;

        public BasicEffect effect { get; set; }

        Matrix worldMatrix = Matrix.Identity;

        // Set the position of the camera in world space, for our view matrix.
        Vector3 cameraPosition = new Vector3(0.0f, 50.0f, 5000.0f);

        public Model3D(Game game)
            : base(game)
        {
            mainGameClass = (Game1)game;
            spritebatch = new SpriteBatch(game.GraphicsDevice);
            myModel = mainGameClass.Content.Load<Model>(@"Models\Wieza");
            aspectRatio = mainGameClass.GraphicsDevice.Viewport.AspectRatio;
            modelPosition = new Vector3(x, y, z);

            effect = new BasicEffect(mainGameClass.GraphicsDevice);
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            mainGameClass.camera.view = worldMatrix * mainGameClass.worldRotationX;
            effect.View = mainGameClass.camera.view;
            effect.Projection = mainGameClass.camera.projection;

            //modelPosition.X = X;
            //modelPosition.Y = Y;

            //     Matrix.CreateScale(0.2f);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            //modelPosition.X = X;
            //modelPosition.Y = Y;
            // Copy any parent transforms.
            Matrix[] transforms = new Matrix[myModel.Bones.Count];
            myModel.CopyAbsoluteBoneTransformsTo(transforms);

            // Draw the model. A model can have multiple meshes, so loop.
            foreach (ModelMesh mesh in myModel.Meshes)
            {
                // This is where the mesh orientation is set, as well
                // as our camera and projection.
                foreach (BasicEffect effect in mesh.Effects)
                {
                    //GraphicsDevice.SamplerStates[0] = SamplerState.LinearClamp;
                    effect.EnableDefaultLighting();
                    effect.World = transforms[mesh.ParentBone.Index] * Matrix.CreateRotationX(1.5f) * mainGameClass.worldTranslation * Matrix.CreateTranslation(modelPosition);
                    effect.View = mainGameClass.board.Effect.View;
                    effect.Projection = mainGameClass.camera.projection;

                    //  effect.Projection = mainGameClass.board.Effect.Projection;
                }
                // Draw the mesh, using the effects set above.
                mesh.Draw();
            }
            base.Draw(gameTime);
        }
    }
}