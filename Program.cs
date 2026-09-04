using CASurvive.ConsoleRenderer;
using System.Security.Cryptography.X509Certificates;

namespace CASurvive
{
    internal class Program
    {
        public static Action? Loaded;
        static void Main(string[] args)
        {
            SimConfig.Initialize();
            RenderConfig.Initialize();

            World world = new World(new LandLayer(64,32));
            Renderer renderer = new Renderer(
                new RendererOptions()
                {
                    follow = false,
                    world = world,
                    x = 62,
                    y = 36
                }
                );
            renderer.SetCamPosition(0, 0);
            renderer.Render();

            while (true)
            {
                var key = Console.ReadKey();


                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        renderer.SetCamPosition(renderer.camX, renderer.camY + 1);
                        break;
                    case ConsoleKey.DownArrow:
                        renderer.SetCamPosition(renderer.camX, renderer.camY - 1);
                        break;
                    case ConsoleKey.LeftArrow:
                        renderer.SetCamPosition(renderer.camX - 1, renderer.camY);
                        break;
                    case ConsoleKey.RightArrow:
                        renderer.SetCamPosition(renderer.camX + 1, renderer.camY);
                        break;
                }
                renderer.Render();
            }
        }
    }
}
