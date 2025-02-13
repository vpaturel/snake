using snake;
using Raylib_cs;
using System.Numerics;
using SerenitySystem.services;
using SerenitySystem.coordinates;
using Timer = SerenitySystem.Helpers.Timer;
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
        Grid grid = new Grid(25, 14, 40);
        Snake snake;
        Apple apple;
        Timer gameOverTimer;
        Timer gamePlayTimer;

        GameState gameState = GameState.Playing;


        public GameScence()
        {
            grid.position = new Vector2(128, 92);
            snake = new Snake(new Coordinates(5, 5), grid, Color.Green);
            apple = new Apple(grid);


            gameOverTimer = AddTimer(GameOver, 1, false);
            gameOverTimer.Stop();
            gamePlayTimer = AddTimer(OnTimerTriggered, 0.2f);
            // appleTimer = AddTimer(apple.Respawn, 5, false);


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
            apple.Update();
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

            //if (Raylib.IsKeyDown(KeyboardKey.W)) direction = Coordinates.up;
            //if (Raylib.IsKeyDown(KeyboardKey.S)) direction = Coordinates.down;
            //if (Raylib.IsKeyDown(KeyboardKey.A)) direction = Coordinates.left;
            //if (Raylib.IsKeyDown(KeyboardKey.D)) direction = Coordinates.right;
            if (Raylib.IsKeyPressed(KeyboardKey.W))
            {
                direction = Coordinates.up;
            }else if (Raylib.IsKeyPressed(KeyboardKey.S))
            {
                direction = Coordinates.down;
            }
            else if (Raylib.IsKeyPressed(KeyboardKey.A))
            {
                direction = Coordinates.left;
            }
            else if (Raylib.IsKeyPressed(KeyboardKey.D))
            {
                direction = Coordinates.right;
            }   
            else
            {
                direction = Coordinates.zero;
            }
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
            Services.GetService<IScenesManager>().Load<GameOverScence>(new object[] { apple.totalScore });
        }

    }
}
