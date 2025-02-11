

//using System.Data.Common;
using System.Numerics;
using Raylib_cs;
using SerenitySystem.coordinates;

namespace snake
{
  public class Cell
    {

        public Coordinates coordinates;

        public bool isWalkable = true;
                private Grid grid;
        public Cell(Coordinates coordinates, Grid grid)
        {
            this.coordinates = coordinates;
            this.grid = grid;
        }
        public void Draw()
        {
            var cellPosInWorld = grid.GridToWorld(coordinates);
            Raylib.DrawRectangle((int)cellPosInWorld.X, (int)cellPosInWorld.Y, grid.cellsize, grid.cellsize, Color.White);
        }
    }


    public class Grid
    {
        public Vector2 position = Vector2.Zero;
        public int columns { get; private set; }
        public int rows { get; private set; }
        public int cellsize { get; private set; }

        private Cell[,] grid;

        public Grid(int columns = 10, int rows = 10, int cellsize = 64)
        {
            this.columns = columns;
            this.rows = rows;
            this.cellsize = cellsize;
            grid = new Cell[columns, rows];
        }

        public void Draw()
        {
            for (int column = 0; column < columns; column++)
            {

                for (int row = 0; row < rows; row++)
                {
                    Cell cell = grid[column, row];
                    cell?.Draw();

                }

            }

            for (int column = 0; column <= columns; column++)
                Raylib.DrawLineV(GridToWorld(new(column, 0)), GridToWorld(new(column, rows)), Color.Gray);

            for (int row = 0; row <= rows; row++)
               Raylib.DrawLineV(GridToWorld(new(0, row)), GridToWorld(new(columns, row)), Color.Gray);



        }
        public void SetCell(Coordinates coordinates)
        {
            if (!isInsideGrid(coordinates))
            {
                return;
            }
            Cell cell = new Cell(coordinates, this);

            grid[coordinates.column, coordinates.row] = cell;
        } 

        public Cell? GetCell(Coordinates coordinates)
        {
            if (!isInsideGrid(coordinates))
            {
                return null; 
            }
            return grid[coordinates.column, coordinates.row];
        }



      
        public Coordinates WorldToGrid(Vector2 pos)
        {
           pos -= position;
            pos /= cellsize;
            return new Coordinates((int)pos.X, (int)pos.Y);
        }

        public Vector2 GridToWorld(Coordinates coordinates)
        {
            coordinates *= cellsize;
            return coordinates.ToVector2 + position;

        }
        private bool isInsideGrid(Coordinates coordinates)
        {
            return coordinates.column >= 0 && coordinates.column < columns && coordinates.row >= 0 && coordinates.row < rows;
        }


    }
}
