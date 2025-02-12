using SerenitySystem.coordinates;
using Raylib_cs;
using System.Drawing;

namespace snake
{
    internal class Apple
    {
        public Coordinates coordinates { get; private set; }
        Grid grid;

       // Color color = Color.White;

        public Apple(Grid grid)
        {
            this.grid = grid;
            coordinates = Coordinates.Random(grid.columns, grid.rows);
        }

        public void Respawn()
        {
            coordinates = Coordinates.Random(grid.columns, grid.rows);
        }

        public void Draw()
        {

            var posInWorld = grid.GridToWorld(coordinates);
            Raylib.DrawTexture(Raylib.LoadTexture("Assets/Images/apple.png"), (int)posInWorld.X, (int)posInWorld.Y, Raylib_cs.Color.Red);
           // Raylib.DrawCircle((int)posInWorld.X + grid.cellsize / 2, (int)posInWorld.Y + grid.cellsize / 2, grid.cellsize / 2, Color.Red);
        }




    }
}
