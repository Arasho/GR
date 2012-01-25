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

        public Model3D(Game game, float x, float y, int color)
        {
            mainGameClass = (Game1)game;
            switch (color)
            {
                case 1:
                    {
                        myModel = mainGameClass.Content.Load<Model>(@"Models\yellow");
                        break;
                    }
                case 2:
                    {
                        myModel = mainGameClass.Content.Load<Model>(@"Models\red");
                        break;
                    }
                case 3:
                    {
                        myModel = mainGameClass.Content.Load<Model>(@"Models\blue");
                        break;
                    }
                case 4:
                    {
                        myModel = mainGameClass.Content.Load<Model>(@"Models\green");
                        break;
                    }
                case 5:
                    {
                        myModel = mainGameClass.Content.Load<Model>(@"Models\black");
                        break;
                    }
            }
            modelPosition = new Vector3(x, y, z);
        }
    }
}