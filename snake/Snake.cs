//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
using SerenitySystem.coordinates;
using Raylib_cs;

namespace snake
{
    public class Snake
    {
        Grid grid;
        Queue<Coordinates> body = new Queue<Coordinates>();
        Coordinates currentDirection = Coordinates.right;
        Coordinates nextDirection = Coordinates.right;

        public Snake(Coordinates coordinates, Grid grid, int startSize = 3)
        {
            this.grid = grid;
            for (int i = startSize -1; i >= 0 ; i--)
            {
                body.Enqueue(coordinates + currentDirection * i);
            }

        }

        public void Draw()
        {
            foreach (var segment in body)
            {
               var posInWorld = grid.GridToWorld(segment);
                Raylib.DrawRectangle((int)posInWorld.X, (int)posInWorld.Y, grid.cellsize, grid.cellsize, Color.White);
            }
        }

        public void Move()
        {
            currentDirection = nextDirection;
            var head = body.Last();
            var newHead = head + currentDirection;
            body.Enqueue(newHead);
            body.Dequeue();
            //Console.WriteLine(body.Count);
        }

        public void SetDirection(Coordinates newDirection)
        {
            if (newDirection == -currentDirection || newDirection == Coordinates.zero) return;
            nextDirection = newDirection;
        }



    }
}
