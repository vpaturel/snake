using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raylib_cs;
using SerenitySystem.Scenes;
using SerenitySystem.services;
using snake;



namespace SerenitySystem.Scenes
{
    public class GameOverScence : AbstractScene
    {
       int score;
        public GameOverScence()
        {
         

        }


        public override void Load(object[] args)
        {
            if (args == null) return;
            foreach (var arg in args)
            {

                Console.WriteLine(arg);



            }
            score = (int)args[0];
        }

        public override void Draw()
        {
            Raylib.DrawText("GameOver", 10, 10, 40, Color.White);
            Raylib.DrawText("Score : " + score, 10, 50, 20, Color.White);
            Raylib.DrawText("Press Enter to open Menu", 10, 80, 20, Color.White);

        }

        public override void Update()
        {
            if (Raylib.IsKeyPressed(KeyboardKey.Enter))
            {
                Services.GetService<IScenesManager>().Load<MenuScence>(null);
            }
        }

    }
}
