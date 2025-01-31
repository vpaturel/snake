

using System.Numerics;
using Raylib_cs;

namespace snake
{
  public class Cell
    {
        public int column { get; private set; }
        public int row { get; private set; }
        private Grid grid;
        public Cell(int column, int row, Grid grid)
        {
            this.column = column;
            this.row = row;
            this.grid = grid;
        }
        public void draw()
        {
            Raylib.DrawRectangle((int)grid.position.X + column * grid.cellsize, (int)grid.position.Y + row * grid.cellsize, grid.cellsize, grid.cellsize, Color.White);
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

        public void draw()
        {
            for (int column = 0; column < columns; column++) {

                for (int row = 0; row < rows; row++)
                {

                    
                    Cell cell = grid[column, row];
                    cell?.draw();
                    Raylib.DrawRectangleLines(column * cellsize + (int)position.X, row * cellsize + (int)position.Y, cellsize, cellsize, Color.White); // a supprimer

                }


            }
        }
        public void SetCell(int column, int row)
        {
            if (column < 0 || column >= columns || row < 0 || row >= rows)
            {
                return;
            }
            Cell cell = new Cell(column, row, this);
            grid[column, row] = cell;
        } 

        public Vector2 WorldToGrid(Vector2 worldPosition)
        {
            return new Vector2((int)(worldPosition.X - position.X) / cellsize, (int)(worldPosition.Y - position.Y) / cellsize);
        }
        
        public Vector2 GridToWorld(Vector2 gridPosition)
        {
            return new Vector2(gridPosition.X * cellsize + position.X, gridPosition.Y * cellsize + position.Y);
        }
    }
}
