﻿using Microsoft.Xna.Framework.Graphics;

namespace _3D_Madness
{
    public class Element
    {
        public VertexPositionTexture[] verts { get; set; }
        public Texture2D Texture { get; set; }

        private string fileName;
        public string FileName
        {
            get { return fileName; }
            set { fileName = value; }
        }


        public enum Edges
        {
            Road = 0,
            EndRoad = 1,
            Town = 3,
            EndTown = 4,
            Field = 6,
        };

        public int leftEdge { get; set; }
        public int rightEdge { get; set; }
        public int bottomEdge { get; set; }
        public int upEdge { get; set; }
        public int additional { get; set; }

        
        public Element(VertexPositionTexture[] x, Texture2D texture)
        {
            verts = x;
            Texture = texture;
        }


        public Element(string _fileName, string _leftEdge, string _rightEdge, string _bottomEdge,  string _upEdge, string  _additional)
        {
            fileName = _fileName;

            switch (_leftEdge)
            {
                case "Road":
                    leftEdge = 0;
                    break;
                case "EndRoad":
                    leftEdge = 1;
                    break;
                case "Town":
                    leftEdge = 3;
                    break;
                case "EndTown":
                    leftEdge = 4;
                    break;
                case "Field":
                    leftEdge = 6;
                    break;
                default:
                    break;
            }

            switch (_rightEdge)
            {
                case "Road":
                    rightEdge = 0;
                    break;
                case "EndRoad":
                    rightEdge = 1;
                    break;
                case "Town":
                    rightEdge = 3;
                    break;
                case "EndTown":
                    rightEdge = 4;
                    break;
                case "Field":
                    rightEdge = 6;
                    break;
                default:
                    break;
            }

            switch (_bottomEdge)
            {
                case "Road":
                    bottomEdge = 0;
                    break;
                case "EndRoad":
                    bottomEdge = 1;
                    break;
                case "Town":
                    bottomEdge = 3;
                    break;
                case "EndTown":
                    bottomEdge = 4;
                    break;
                case "Field":
                    bottomEdge = 6;
                    break;
                default:
                    break;
            }

            switch (_upEdge)
            {
                case "Road":
                    upEdge = 0;
                    break;
                case "EndRoad":
                    upEdge = 1;
                    break;
                case "Town":
                    upEdge = 3;
                    break;
                case "EndTown":
                    upEdge = 4;
                    break;
                case "Field":
                    upEdge = 6;
                    break;
                default:
                    break;
            }

            switch (_additional)
            {
                case "Cathedral":
                    additional = 1;
                    break;
                case "Shield":
                    additional = 2;
                    break;
                default:
                    additional = 0;
                    break;
            }
        }
    }
}