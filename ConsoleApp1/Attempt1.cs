using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

/**
 * Auto-generated code below aims at helping you parse
 * the standard input according to the problem statement.
 **/
class Player
{
    readonly record struct Quadrant(int StartX, int Width, int StartY, int Height)
    {
        public (int x, int y) Center
            => ((StartX + Width) / 2, (StartY + Height) / 2);

        public Quadrant[] SplitIntoQuadrants()
        {
            var halfWidth = Width / 2;
            var halfHeight = Height / 2;

            var quadrants = new Quadrant[]
            {
                new(StartX, halfWidth + (Width % 2), StartY, halfHeight + (Height % 2)), // top-left
                new(StartX + halfWidth + (Width % 2), halfWidth, StartY, halfHeight + (Height % 2)), // top-right
                new(StartX, halfWidth + (Width % 2), StartY + halfHeight + (Height % 2), halfHeight), // bottom-right
                new(StartX + halfWidth + (Width % 2), halfWidth, StartY + halfHeight + (Height % 2), halfHeight), // bottom-left
            };

            return quadrants;
        }
    };

    static void Main(string[] args)
    {
        string[] inputs;
        inputs = Console.ReadLine().Split(' ');
        int w = int.Parse(inputs[0]); // width of the building.
        int h = int.Parse(inputs[1]); // height of the building.
        int n = int.Parse(Console.ReadLine()); // maximum number of turns before game over.
        inputs = Console.ReadLine().Split(' ');
        int x0 = int.Parse(inputs[0]);
        int y0 = int.Parse(inputs[1]);

        var quadrants = new Quadrant(0, w, 0, h).SplitIntoQuadrants();

        //Handle "Unknown" case
        Console.ReadLine();

        // game loop
        while (true)
        {
            for (int i = 0; i < 4; i++)
            {
                Console.Error.WriteLine(quadrants[i]);
            }
            var quadrantResults = new string[4];
            //Test four quadrants
            for (int i = 0; i < 4; i++)
            {
                var quadrant = quadrants[i];
                var testPos = quadrant.Center;

                Console.WriteLine($"{testPos.x} {testPos.y}");
                string bombDir = Console.ReadLine()!; // Current distance to the bomb compared to previous distance (COLDER, WARMER, SAME or UNKNOWN)
                quadrantResults[i] = bombDir;
            }

            //Go through quadrantResults in reverse
            for (int i = 3; i >= 0; i--)
            {
                var result = quadrantResults[i];
                if (result == "WARMER")
                {
                    Console.Error.WriteLine(quadrants[i]);
                    var bestQuadrant = quadrants[i];
                    quadrants = quadrants[i].SplitIntoQuadrants();
                    
                    break;
                }
            }

            // Write an action using Console.WriteLine()
            // To debug: Console.Error.WriteLine("Debug messages...");
        }
    }
}