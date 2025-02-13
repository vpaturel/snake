using SerenitySystem.coordinates;
using Raylib_cs;
using System.Drawing;
using System;
using Timer = SerenitySystem.Helpers.Timer;
using static System.Formats.Asn1.AsnWriter;
namespace snake
{
   
    internal class Apple
    {
        public Coordinates coordinates { get; private set; }
        Grid grid;
        private int appleScore;
        private int appleScoreValue = 500;
        public int totalScore{ get; private set; }
        Timer appleScoreTimer;
    


        // Color color = Color.White;

        public Apple(Grid grid)
        {
            this.grid = grid;
            coordinates = Coordinates.Random(grid.columns, grid.rows);
            totalScore = 0;
            appleScore = appleScoreValue;
            appleScoreTimer = new Timer(appleValueReduce, appleScore, false);
            appleScoreTimer.Start();
        }



        public void appleValueReduce()
        {
           if (appleScore > 0)
            {
                appleScore -= 1;
            }
            else
            {
                appleScoreTimer.Stop();
                
            }
        }
        public void Respawn()
        {
            totalScore = totalScore + appleScore;
            appleScore = appleScoreValue;
            appleScoreTimer.Start();
            coordinates = Coordinates.Random(grid.columns, grid.rows);
        }

        public void Draw()
        {
            
            var posInWorld = grid.GridToWorld(coordinates);
            Raylib.DrawTexture(Raylib.LoadTexture("Assets/Images/apple.png"), (int)posInWorld.X, (int)posInWorld.Y, Raylib_cs.Color.Red);
            Raylib.DrawText(appleScore.ToString(), (int)posInWorld.X, (int)posInWorld.Y , 20, Raylib_cs.Color.White);
            Raylib.DrawText("Score : " + totalScore, 500, 10, 20, Raylib_cs.Color.White);
        }

        public void Update()
        {
            Console.WriteLine(appleScore);
            appleValueReduce();

        }




    }
}
