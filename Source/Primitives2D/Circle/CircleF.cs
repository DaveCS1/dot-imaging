#region Licence and Terms
// DotImaging Framework
// https://github.com/dajuric/dot-imaging
//
// Copyright © Darko Jurić, 2014-2018
// darko.juric2@gmail.com
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
#endregion

namespace DotImaging.Primitives2D
{
    // Accord Math Library
    // The Accord.NET Framework
    // http://accord-framework.net
    //
    // Copyright © César Souza, 2009-2014
    // cesarsouza at gmail.com
    //
    // Copyright © Darko Jurić, 2014-2019
    // darko.juric2 at gmail.com
    //
    //
    //    This library is free software; you can redistribute it and/or
    //    modify it under the terms of the GNU Lesser General Public
    //    License as published by the Free Software Foundation; either
    //    version 2.1 of the License, or (at your option) any later version.
    //
    //    This library is distributed in the hope that it will be useful,
    //    but WITHOUT ANY WARRANTY; without even the implied warranty of
    //    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
    //    Lesser General Public License for more details.
    //
    //    You should have received a copy of the GNU Lesser General Public
    //    License along with this library; if not, write to the Free Software
    //    Foundation, Inc., 51 Franklin St, Fifth Floor, Boston, MA  02110-1301  USA
    //

    using System.Drawing;

    /// <summary>
    ///   2D circle class.
    /// </summary>
    public struct CircleF
    {
        /// <summary>
        /// Horizontal center coordinate.
        /// </summary>
        public float X;
        /// <summary>
        /// Vertical center coordinate.
        /// </summary>
        public float Y;
        /// <summary>
        /// Circle radius.
        /// </summary>
        public float Radius;

        /// <summary>
        /// Gets or sets circle's location.
        /// </summary>
        public PointF Center
        {
            get { return new PointF(X, Y); }
            set 
            {
                this.X = value.X;
                this.Y = value.Y;
            }
        }

        /// <summary>
        /// Creates a new instance of an <see cref="CircleF"/> structure.
        /// </summary>
        /// <param name="position">Center position.</param>
        /// <param name="radius">Circle radius.</param>
        public CircleF(PointF position, float radius)
            : this(position.X, position.Y, radius)
        { }

        /// <summary>
        /// Creates a new instance of an <see cref="CircleF"/> structure.
        /// </summary>
        /// <param name="x">Horizontal center position.</param>
        /// <param name="y">Vertical center position.</param>
        /// <param name="radius">Circle radius.</param>
        public CircleF(float x, float y, float radius)
        {
            this.X = x;
            this.Y = y;
            this.Radius = radius;
        }

        /// <summary>
        ///   Gets the area of the circle (πR²).
        /// </summary>
        /// 
        public double Area
        {
            get { return Radius * Radius * System.Math.PI; }
        }

        /// <summary>
        ///   Gets the circumference of the circle (2πR).
        /// </summary>
        /// 
        public double Circumference
        {
            get { return 2 * Radius * System.Math.PI; }
        }

        /// <summary>
        ///   Gets the diameter of the circle (2R).
        /// </summary>
        /// 
        public float Diameter
        {
            get { return 2 * Radius; }
        }

        /// <summary>
        ///   Creates a new <see cref="Circle"/> from three non-linear points.
        /// </summary>
        /// 
        /// <param name="p1">The first point.</param>
        /// <param name="p2">The second point.</param>
        /// <param name="p3">The third point.</param>
        /// 
        public CircleF(PointF p1, PointF p2, PointF p3)
        {
            // ya = ma * (x - x1) + y1
            // yb = mb * (x - x2) + y2
            //
            // ma = (y2 - y1) / (x2 - x1)
            // mb = (y3 - y2) / (x3 - x2)
            double ma = (p2.Y - p1.Y) / (p2.X - p1.X);
            double mb = (p3.Y - p2.Y) / (p3.X - p2.X);

            //       (ma * mb * (y1 - y3) + mb * (x1 + x2) - ma * (x2 + x3)
            // x = ----------------------------------------------------------
            //                          2 * (mb - ma)
            double x = (ma * mb * (p1.Y - p3.Y) + mb * (p1.X + p2.Y) - ma * (p2.X + p3.X)) / (2 * (mb - ma));
            double y = ma * (x - p1.X) + p1.Y;

            this.X = (float)x;
            this.Y = (float)y;
            this.Radius = (float)System.Math.Sqrt((this.X - p1.X) * (this.X - p1.X) + (this.Y - p1.Y) * (this.Y - p1.Y)); //Euclidean distance
        }

        /// <summary>
        ///   Computes the distance from circle to point.
        /// </summary>
        /// 
        /// <param name="point">The point to have its distance from the circle computed.</param>
        /// 
        /// <returns>The distance from <paramref name="point"/> to this circle.</returns>
        /// 
        public double DistanceToPoint(PointF point)
        {
            var centerDiff = this.Center.DistanceTo(point);
            return System.Math.Abs(centerDiff - Radius);
        }
    }
}
