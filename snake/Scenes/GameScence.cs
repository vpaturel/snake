using snake;
using Raylib_cs;
using System.Numerics;
using SerenitySystem.coordinates;
namespace SerenitySystem.Scenes
{
    public class GameScence : AbstractScene
    {
        Grid grid = new Grid(26,14);
        Snake snake;

        float timer = 0;
        float interval = 0.5f;
        public override void Update()
        {
          // if (Raylib.IsMouseButtonPressed(MouseButton.Left))
          //  {
          //     var mousePosition = Raylib.GetMousePosition();
          //     var gridPosition = grid.WorldToGrid(mousePosition);
          //     Console.WriteLine(gridPosition);
          //
          //        if (gridPosition.row < 0 || gridPosition.column >= grid.columns || gridPosition.column < 0 || gridPosition.column >= grid.rows)
          //        {
          //            return;
          //       }
          //
          //
          //        grid.SetCell(gridPosition);
          //  }
          //
          snake.SetDirection(GetInputsDirection());
            timer  += Raylib.GetFrameTime();
            if (timer >= interval)
            {
                snake.Move();
                timer = 0;
            }


        }
        public override void Draw()
        {
            grid.Draw();
            snake.Draw();
        }
        public override void Load()
        {
            grid.position = new Vector2(128, 92);
            snake = new Snake(new Coordinates(10, 5), grid);

        }
        public override void Unload()
        {
           
        }

        private Coordinates GetInputsDirection()
        {
        var direction = Coordinates.zero;
            if (Raylib.IsKeyDown(KeyboardKey.Z)) direction = Coordinates.up;
            if (Raylib.IsKeyDown(KeyboardKey.S)) direction = Coordinates.down;
            if (Raylib.IsKeyDown(KeyboardKey.Q)) direction = Coordinates.left;
            if (Raylib.IsKeyDown(KeyboardKey.D)) direction = Coordinates.right;

            return direction;
        } 
 


    }
}
