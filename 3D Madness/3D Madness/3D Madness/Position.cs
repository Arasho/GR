﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _3D_Madness {

    public class Pawn {


        /// <summary>
        /// Creates new Pawn
        /// </summary>
        /// <param name="x">position x of a pawn</param>
        /// <param name="y">position y of a pawn</param>
        public Pawn(int x, int y, int krawedz) {
            this.x = x;
            this.y = y;
            this.krawedz = krawedz;
        }

        /// <summary>
        /// position x setter & getter
        /// </summary>
        public int x { get; set; }

        /// <summary>
        /// position y setter & getter
        /// </summary>
        public int y { get; set; }
        public int krawedz {get; set;}


    }
}
