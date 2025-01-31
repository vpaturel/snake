using snake;
using Raylib_cs;
using System.Numerics;

namespace SerenitySystem.Scenes
{
    public class TestScene : AbstractScene
    {
        Grid grid = new Grid(15,20,32);
        public override void Update()
        {
           if (Raylib.IsMouseButtonPressed(MouseButton.Left))
            {
               var mousePosition = Raylib.GetMousePosition();
               var gridPosition = grid.WorldToGrid(mousePosition);
               // if (gridPosition.X < 0 || gridPosition.X >= grid.columns || gridPosition.Y < 0 || gridPosition.Y >= grid.rows)
               // {
               //     return;
               // }
               Console.WriteLine(gridPosition);

                grid.SetCell((int)gridPosition.X, (int)gridPosition.Y);
            }

        }
        public override void Draw()
        {
            grid.draw();
        }
        public override void Load()
        {
            grid.position = new Vector2(300, 300);
            //grid.SetCell(5, 5);
            //grid.SetCell(6,5);
        }
        public override void Unload()
        {
           
        }
    }
}
