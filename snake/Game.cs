using System.Numerics;
using Raylib_cs;
using SerenitySystem.Scenes;
//using SerenitySystem.services;
//using snake;
public static class Game
{

    static ScenesManager scenesManager = new ScenesManager();

    public static readonly int width = 1280;
    public static readonly int height = 720;
    public static float scale => Math.Min(Raylib.GetScreenWidth() / (float)width, Raylib.GetScreenHeight() / (float)height);
    static float offsetX = 1f;
    static float offsetY = 1f;
    static void Main()
    {
        Raylib.SetConfigFlags(ConfigFlags.ResizableWindow | ConfigFlags.VSyncHint);
        int screenWidth = 1920;
        int screenHeight = 1080;

        Raylib.InitWindow(screenWidth, screenHeight, "Snake");
        Raylib.SetTargetFPS(60);
        Raylib.InitAudioDevice();

        scenesManager.Load<GameScence>(null);

        RenderTexture2D canvas = Raylib.LoadRenderTexture(width, height);
        Raylib.SetTextureFilter(canvas.Texture, TextureFilter.Point);

        while (!Raylib.WindowShouldClose())
        {
            float scaleW = width * scale;
            float scaleH = height * scale;
            offsetX = (Raylib.GetScreenWidth() - scaleW) * 0.5f;
            offsetY = (Raylib.GetScreenHeight() - scaleH) * 0.5f;

            Raylib.BeginTextureMode(canvas);
            Raylib.ClearBackground(Color.Black);
            scenesManager.Update();
            scenesManager.Draw();
            Raylib.EndTextureMode();

            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.Green);
            Raylib.DrawTexturePro(canvas.Texture, new Rectangle(0, 0, width, -height), new Rectangle(offsetX, offsetY, scaleW, scaleH), Vector2.Zero, 0, Color.White);
            Raylib.EndDrawing();
        }
        Raylib.CloseAudioDevice();
        Raylib.CloseWindow();
    }
}


