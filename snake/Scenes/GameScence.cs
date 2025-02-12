using snake;
using Raylib_cs;
using System.Numerics;
using SerenitySystem.services;
using SerenitySystem.coordinates;
using SerenitySystem.Helpers.Timer;
namespace SerenitySystem.Scenes
{
    public class GameScence : AbstractScene
    {

        enum GameState
        {
            Playing,
            Paused,
            GameOver
        }
        Grid grid = new Grid(10, 10, 40);
        Snake snake;
        Apple apple;
        Helpers.Timer.Timer gameOverTimer;
        Helpers.Timer.Timer gamePlayTimer;
        //Timer timer;
        GameState gameState = GameState.Playing;


        public GameScence()
        {
            grid.position = new Vector2(128, 92);
            snake = new Snake(new Coordinates(5, 5), grid, Color.Green);
            apple = new Apple(grid);


            gameOverTimer = AddTimer(GameOver, 2, false);
            gameOverTimer.Stop();
            gamePlayTimer= AddTimer(OnTimerTriggered, 0.5f);   


        }


        public override void Load(object[] args)
        {
            if (args == null) return;
            foreach (var arg in args)
            {

                Console.WriteLine(arg);

            }
        }
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
            base.Update();
            switch (gameState)
            {
                case GameState.Playing:
                    UpdatePlaying();
                    break;
                case GameState.Paused:
                    UpdatePaused();
                    break;

            }
        }


        private void UpdatePlaying()
        {
            snake.SetDirection(GetInputsDirection());
            if (Raylib.IsKeyPressed(KeyboardKey.P))
            {
                gameState = GameState.Paused;
                gamePlayTimer.Stop();   
            }
           
        }

        public void OnTimerTriggered()
        {
            snake.Move();
            if (snake.IsOutOfBounds() || snake.IsCollidingWithSelf())
            {
                gameState = GameState.GameOver;
                gameOverTimer.Start();
                gamePlayTimer.Stop();

            }
            if ((snake.IsCollidingWith(apple.coordinates))) EatApple();
        }


        public void UpdatePaused()
        {
            if (Raylib.IsKeyPressed(KeyboardKey.P))
            {
                gameState = GameState.Playing;
                gamePlayTimer.Start();  
            }
 
        }





        private void EatApple()
        {
            apple.Respawn();
            snake.Grow();
        }

        public override void Draw()
        {
            switch (gameState)
            {
                case GameState.Paused:
                    Raylib.DrawText("Paused", 10, 10, 20, Color.White);
                    break;
                case GameState.GameOver:
                    Raylib.DrawText("Game Over", 10, 10, 20, Color.White);
                    break;
            }

            grid.Draw();
            snake.Draw();
            apple.Draw();

        }

        public override void Unload()
        {

        }

        private Coordinates GetInputsDirection()
        {
            var direction = Coordinates.zero;

            if (Raylib.IsKeyDown(KeyboardKey.W)) direction = Coordinates.up;
            if (Raylib.IsKeyDown(KeyboardKey.S)) direction = Coordinates.down;
            if (Raylib.IsKeyDown(KeyboardKey.A)) direction = Coordinates.left;
            if (Raylib.IsKeyDown(KeyboardKey.D)) direction = Coordinates.right;
            //utiliser les scancode
            return direction;
        }

        // private Coordinates GetInputsDirection()
        // {
        //     var direction = Coordinates.zero;
        //     Console.WriteLine(Raylib.GetKeyPressed());
        //     Raylib.IsKeyDown()
        //
        //     if (Raylib.IsKeyDown((KeyboardKey)Raylib.GetKeyPressed()))
        //     {
        //         int key = Raylib.GetKeyPressed();
        //         if (key == (int)KeyboardKey.W) direction = Coordinates.up;
        //         if (key == (int)KeyboardKey.S) direction = Coordinates.down;
        //         if (key == (int)KeyboardKey.Q) direction = Coordinates.left;
        //         if (key == (int)KeyboardKey.D) direction = Coordinates.right;
        //     }
        //
        //     return direction;
        // }
        //



        public void GameOver()
        {
            Services.GetService<IScenesManager>().Load<GameScence>(null);
        }

    }
}
