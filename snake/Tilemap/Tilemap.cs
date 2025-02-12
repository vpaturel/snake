using System.Numerics;
using Raylib_cs;
using SerenitySystem.coordinates;

namespace snake.Tilemap
{
    public class Tilemap
    {
        public Vector2 position = Vector2.Zero;
        public int tileSize { get; private set; }
        public int columns { get; private set; }
        public int rows { get; private set; }
        private Dictionary<string,TilemapLayer> layers = new Dictionary<string, TilemapLayer>();
        private List<string> layersOrder = new List<string>();

        public Tilemap(int columns = 10, int rows = 10, int tileSize = 64)
        {
            this.columns = columns;
            this.rows = rows;
            this.tileSize = tileSize;
        }


        public void Draw()
        {
            foreach (var layeNamer in layersOrder)
            {
                var layer = layers[layeNamer];
                for (int column = 0; column < columns; column++)
                {
                    for (int row = 0; row < rows; row++)
                    {
                        Tile tile = layer.tiles[column, row];
                        if (tile.textureId >= 0)
                        {
                              Raylib.DrawTexture(layer.textures[tile.textureId],(int)position.X * tileSize,(int)position.Y * tileSize, layer.tint);
                        }
                    }
                }
            }
        }

        public void Addlayer(Texture2D[] textures, string name, Color tint)
        {
            TilemapLayer layer = new TilemapLayer();
            layer.textures = textures;
            layer.tint = tint;
            layer.tiles = new Tile[columns, rows];
            layers.Add(name, layer);
            layersOrder.Add(name);
        }

        public void SetTile(Coordinates coordinates, int textureId, string layerName)
        {
            if (!IsInBounds(coordinates)) return;
            layers[layerName].tiles[coordinates.column, coordinates.row] = new Tile
            {
                textureId = textureId,
                isSolid = false
            };
        }

        public bool IsInBounds(Coordinates coordinates)
        {
            return coordinates.column >= 0 && coordinates.column < columns && coordinates.row >= 0 && coordinates.row < rows;
        }

        private struct Tile
        {
            public int textureId;
            public bool isSolid;

        }
        private struct TilemapLayer
        {
            public Texture2D[] textures;
            public Tile[,] tiles;
            public Color tint;
        }

    }
}
