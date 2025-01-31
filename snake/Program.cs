using Raylib_cs;
using SerenitySystem.Scenes;
using SerenitySystem.services;
using snake;
class Program
{

    static ScenesManager scenesManager = new ScenesManager();
    static void Main()
    {
        int screenWidth = 1920;
        int screenHeight = 1080;

        Raylib.InitWindow(screenWidth, screenHeight, "Snake");
        Raylib.SetTargetFPS(60);

        scenesManager.Load<TestScene>();    

        while (!Raylib.WindowShouldClose())
        {
            Raylib.BeginDrawing();
            //Raylib.ClearBackground(Color.White);
            scenesManager.Update();
            scenesManager.Draw();
            Raylib.EndDrawing();
        }
        Raylib.CloseWindow();
    }
}


