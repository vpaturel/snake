using Raylib_cs;
class Program
{
    static void Main()
    {
        int screenWidth = 800;
        int screenHeight = 600;

        Raylib.InitWindow(screenWidth, screenHeight, "Snake Game");
        Raylib.SetTargetFPS(60);


        while (!Raylib.WindowShouldClose())
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.White);
            Raylib.DrawText("Snake Game", 10, 10, 20, Color.Black);
            Raylib.EndDrawing();
        }
        Raylib.CloseWindow();
    }
}
