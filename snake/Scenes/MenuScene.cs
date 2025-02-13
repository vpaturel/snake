using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raylib_cs;
using SerenitySystem.Scenes;
using SerenitySystem.services;



namespace SerenitySystem.Scenes
{
    public class MenuScence : AbstractScene
    {

        public MenuScence()
        {


        }

        public override void Load(object[] args)
        {
            Console.WriteLine("Menu Scene Loaded");
        }

        public override void Draw()
        {
            Raylib.DrawText("Keyboard Destructor Snake", 10, 10, 40, Color.White);
            Raylib.DrawText("Press Enter to start", 10, 50, 20, Color.White);

        }

        public override void Update()
        {
            if (Raylib.IsKeyPressed(KeyboardKey.Enter))
            {
                Services.GetService<IScenesManager>().Load<GameScence>(null);
            }
        }

    }
}
