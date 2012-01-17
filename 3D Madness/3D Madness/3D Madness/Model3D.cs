using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _3D_Madness
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class Model3D
    {
        public Model myModel;

        public Game1 mainGameClass { get; set; }

        public float X { get; set; }

        public float Y { get; set; }

        float z = .0f;
        public Vector3 modelPosition;


        //public BasicEffect effect { get; set; }

        Matrix worldMatrix = Matrix.Identity;

        // Set the position of the camera in world space, for our view matrix.
        Vector3 cameraPosition = new Vector3(0.0f, 50.0f, 5000.0f);

        public Model3D(Game game, float x, float y)
        {
            mainGameClass = (Game1)game;
            myModel = mainGameClass.Content.Load<Model>(@"Models\Pionek");
            modelPosition = new Vector3(x, y, z);
        }
    }
}