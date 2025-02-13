using SerenitySystem.coordinates;

using Raylib_cs;


namespace snake
{
    public class Snake
    {
        Grid grid;
        Queue<Coordinates> body = new Queue<Coordinates>();
        Coordinates currentDirection = Coordinates.right;
        Coordinates nextDirection = Coordinates.right;
        Color color = Color.White;

        




        private Dictionary<int, Texture2D> texttures = new Dictionary<int, Texture2D>()
        {
         {1, Raylib.LoadTexture("Assets/Images/Snake/head_left.png")},
         {2, Raylib.LoadTexture("Assets/Images/Snake/head_up.png")},
         {3, Raylib.LoadTexture("Assets/Images/Snake/body_topleft.png")},
         {4, Raylib.LoadTexture("Assets/Images/Snake/head_right.png")},
         {5, Raylib.LoadTexture("Assets/Images/Snake/body_horizontal.png")},
         {6, Raylib.LoadTexture("Assets/Images/Snake/body_topright.png")},
         {8, Raylib.LoadTexture("Assets/Images/Snake/head_down.png")},
         {9, Raylib.LoadTexture("Assets/Images/Snake/body_bottomleft.png")},
         {10, Raylib.LoadTexture("Assets/Images/Snake/body_vertical.png")},
         {12, Raylib.LoadTexture("Assets/Images/Snake/body_bottomright.png")},
         {20, Raylib.LoadTexture("Assets/Images/Snake/tail_left.png")},
         {21, Raylib.LoadTexture("Assets/Images/Snake/tail_up.png")},
         {22, Raylib.LoadTexture("Assets/Images/Snake/tail_right.png")},
         {23, Raylib.LoadTexture("Assets/Images/Snake/tail_down.png")},
        };





        private bool isGrowing = false;

        public Coordinates head => body.Last();
        public Coordinates tail => body.First();

        public Snake(Coordinates coordinates, Grid grid,Color color, int startSize = 3)
        {
            this.color = color;
            this.grid = grid;
            for (int i = startSize - 1; i >= 0; i--)
            {
                body.Enqueue(coordinates - currentDirection * i);
                Console.WriteLine(body.Count);
            }

           
        }


        public void Move()
        {

            currentDirection = nextDirection;
            body.Enqueue(head + currentDirection);
            if (!isGrowing) body.Dequeue();
            else isGrowing = false;

           // Console.WriteLine(body.Count);
        }

        public void SetDirection(Coordinates newDirection)
        {
            if (newDirection == -currentDirection || newDirection == Coordinates.zero) return;
            nextDirection = newDirection;
        }


        public void Grow()
        {
            isGrowing = true;
            Console.WriteLine(body.Count);
        }

        public bool IsOutOfBounds()
        {
            return !grid.isInsideGrid(head);
        }

        public bool IsCollidingWith(Coordinates coordinates)
        {
            return body.Contains(coordinates);
        }


        public bool IsCollidingWithSelf()
        {
            HashSet<Coordinates> hashSet = new HashSet<Coordinates>();
            foreach (var segment in body)
            {
                if (!hashSet.Add(segment)) return true;
            }
            return false;

            //return body.Count != body.Distinct().Count();
        }


        public void Draw()
        {
            foreach (var segment in body)
            {

                
                var bodyArray = body.ToArray(); 
                for (int i = 1; i <  bodyArray.Length -1; i++)
                {
                    int id = GetBodyTextureID(bodyArray[i - 1], bodyArray[i], bodyArray[i + 1]);
                    Raylib.DrawTexture(texttures[id], (int)grid.GridToWorld(bodyArray[i]).X, (int)grid.GridToWorld(bodyArray[i]).Y, color); 

                }

                Raylib.DrawTexture(texttures[GetTailTextureID()], (int)grid.GridToWorld(tail).X, (int)grid.GridToWorld(tail).Y, color);
                Raylib.DrawTexture(texttures[GetHeadTextureID()], (int)grid.GridToWorld(head).X, (int)grid.GridToWorld(head).Y, color);


                // var posInWorld = grid.GridToWorld(segment);
                // Raylib.DrawRectangle((int)posInWorld.X, (int)posInWorld.Y, grid.cellsize, grid.cellsize, Color.White);
            }
        }



        private int GetHeadTextureID()
        {
            if (currentDirection == Coordinates.left) return 1;
            if (currentDirection == Coordinates.up) return 2;
            if (currentDirection == Coordinates.right) return 4;
            if (currentDirection == Coordinates.down) return 8;
            return 0;
        }

        private int GetBodyTextureID(Coordinates previous, Coordinates current, Coordinates next)
        {
            int id = 0;

            if (current + Coordinates.left == next || current + Coordinates.left == previous) id += 1;
            if (current + Coordinates.up == next || current + Coordinates.up == previous) id += 2;
            if (current + Coordinates.right == next || current + Coordinates.right == previous) id += 4;
            if (current + Coordinates.down == next || current + Coordinates.down == previous) id += 8;

            return id;
        }

        private int GetTailTextureID()
        {
            int id = 0;
            if (tail + Coordinates.left == body.ElementAt(1)) id = 20;
            if (tail + Coordinates.up == body.ElementAt(1)) id = 21;
            if (tail + Coordinates.right == body.ElementAt(1)) id = 22;
            if (tail + Coordinates.down == body.ElementAt(1)) id = 23;
            return id;
        }


    }
}
