using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CASurvive.ConsoleRenderer
{


    public struct RenderAsset
    {
        public RenderAsset(char sprite, int r, int g, int b)
        {
            this.sprite = sprite;
            this.r = r;
            this.g = g;
            this.b = b;
        }

        [JsonInclude]
        public char sprite { get; private set; }
        [JsonInclude]
        public int r { get; private set; }
        [JsonInclude]
        public int g { get; private set; }
        [JsonInclude]
        public int b { get; private set; }
        

    }
    public struct TileMaterial
    {
        public char sprite { get; private set; }
        public string color { get; private set; }
        public string finalSprite {  get; private set; }
        public TileMaterial(RenderAsset asset)
        {
            sprite = asset.sprite;
            color = $"\x1b[38;2;{asset.r};{asset.g};{asset.b}m";
            finalSprite = color + sprite;
        }
    }
    public abstract class LayerRenderer
    {

        public abstract bool CheckOutOfRange(int x, int y);
        public abstract TileMaterial? GetCharacter(int x, int y);

    }

    public class LandLayerRenderer : LayerRenderer
    {
        private LandLayer layer;
        public LandLayerRenderer(LandLayer layer) 
        {
            this.layer = layer;
        }
        public override bool CheckOutOfRange(int x, int y)
        {
            var dimensions = layer.GetDimensions();

            return x > dimensions.x || x < 0 || y > dimensions.y || y < 0;
        }
        public override TileMaterial? GetCharacter(int x, int y)
        {
            if (CheckOutOfRange(x, y)) throw new ArgumentOutOfRangeException();

            var tile = layer.GetLandAt(x, y);


            return RenderConfig.landAssets[tile];
        }
    }

    public static class RenderConfig
    {
        public static readonly TileMaterial emptyTile = new TileMaterial(new RenderAsset(' ', 0,0,0));
        public static Dictionary<LandType, TileMaterial> landAssets;

        public static void Initialize()
        {
            RenderConfig.landAssets = new();
            string landGraphics = File.ReadAllText("./land_graphics.json");

            var landAssets = JsonSerializer.Deserialize<Dictionary<LandType, RenderAsset>>(landGraphics);
            
            foreach(var key in landAssets.Keys)
            {
                RenderConfig.landAssets.Add(key,
                    new TileMaterial(landAssets[key]));
            }
        }
    }
    internal class Renderer
    {
        public int resX { get; private set; }
        public int resY { get; private set; }

        public int camX { get; private set;  }
        public int camY {  get; private set; }
        StringBuilder sb = new StringBuilder();
        public World world;
        private LayerRenderer[] layers;
        public Renderer(RendererOptions options)
        {
            world = options.world;

            resX = options.x;
            resY = options.y;


            layers = new LayerRenderer[1];
            layers[0] = new LandLayerRenderer(world.landLayer);

        }
        public void SetCamPosition(int x, int y)
        {
            camX = x; camY = y;
        }
        public void Render()
        {
            sb.Clear();


            for (int y = camY - resY / 2; y < camY + resY / 2; y++)
            {
                for (int x = camX - resX / 2; x < camX + resX / 2; x++)
                {
                    var tile = GetTileFor(x, y);

                    sb.Append(tile.finalSprite);
                }
                sb.Append('\n');
            }


            Console.SetCursorPosition(0, 0);

            Console.Write(sb.ToString());
        }

        private TileMaterial GetTileFor(int x, int y)
        {
            return _GetTileRecursive(layers.Length-1, x, y);
        }
        private TileMaterial _GetTileRecursive(int layerNumber, int x, int y)
        {
            try
            {
                var tile = layers[layerNumber].GetCharacter(x, y);

                if (tile == null)
                {
                    if (layerNumber == 0)
                    {
                        tile = RenderConfig.emptyTile;
                    }
                    else
                    {
                        tile = _GetTileRecursive(layerNumber - 1, x, y);
                    }
                }
                return (TileMaterial)tile;

            }
            catch (ArgumentOutOfRangeException)
            {
                return RenderConfig.emptyTile;
            }
        }
    }
    struct RendererOptions
    {
        public int x { get; set; }
        public int y { get; set; }
        
        public World world;

        public bool follow { get; set; }
    }
}
