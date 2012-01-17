using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _3D_Madness
{
    public class Model3D
    {
        public Model myModel;
        public Game1 mainGameClass { get; set; }

        float z = .0f;
        public Vector3 modelPosition;

        public Model3D(Game game, float x, float y)
        {
            mainGameClass = (Game1)game;
            myModel = mainGameClass.Content.Load<Model>(@"Models\Pionek");
            modelPosition = new Vector3(x, y, z);
        }
    }
}