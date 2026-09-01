using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CASurvive.ConsoleRenderer
{


    /*TODO:
     *  Закінчити базову логіку рендеру:
     *      1. Завершити структуру налаштування самого рендеререру
     *      2. Прописати базову логіку рекурсивного рендеру. Алгоритм:
     *          а) Починаємо з найверхнього шару
     *          б) Отримуємо по черзі кожен символ
     *          в) Якщо він пустий, то спускаємось на один шар 
     *          г) Повтроюємо допоки не знайдеться символ для рендеру
     */
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


        public abstract TileMaterial? GetCharacter(int x, int y);

    }

    public class LandLayerRenderer : LayerRenderer
    {
        private LandLayer layer;
        public LandLayerRenderer(LandLayer layer) 
        {
            this.layer = layer;
        }
        public override TileMaterial? GetCharacter(int x, int y)
        {
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
            string landGraphics = File.ReadAllText("./land_graphics.json");

            landAssets = JsonSerializer.Deserialize<Dictionary<LandType, TileMaterial>>(landGraphics);
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


            layers = new LayerRenderer[2];
            layers[0] = new LandLayerRenderer(world.landLayer);

        }

        public void Render()
        {
            sb.Clear();


            for (int y = camY - resY / 2; y < camY + resY / 2; y++)
            {
                for (int x = camX - resX / 2; x < camX + resX / 2; x++)
                {
                    var tile = GetTileFor(x, y);

                    sb.Append(tile);
                }
                sb.Append('\n');
            }


            Console.SetCursorPosition(0, 0);

            Console.Write(sb.ToString());
        }

        private TileMaterial GetTileFor(int x, int y)
        {
            return _GetTileRecursive(layers.Length, x, y);
        }
        private TileMaterial _GetTileRecursive(int layerNumber, int x, int y)
        {
            var tile = layers[layerNumber].GetCharacter(x, y);

           

            if(tile == null) { 
                if(layerNumber == 0)
                {
                    tile = RenderConfig.emptyTile;
                }
                else
                {
                    tile = _GetTileRecursive(layerNumber-1, x, y);
                }
            }

            return (TileMaterial)tile;
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
