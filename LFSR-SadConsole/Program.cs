using Microsoft.Xna.Framework;
using System;
using Console = SadConsole.Console;

namespace NetLFSR
{
    class Program
    {
        public const int Width = 60;
        public const int Height = 20;

        private static LFSR _lfsr;
        private static Console _gameConsole;
        private static Random _random = new Random();

        static void Main(string[] args)
        {
            // Setup the engine and create the main window.
            SadConsole.Game.Create(Width, Height);

            // Hook the start event so we can add consoles to the system.
            SadConsole.Game.OnInitialize = Init;

            SadConsole.Game.OnUpdate = Update;

            // Start the game.
            SadConsole.Game.Instance.Run();
            SadConsole.Game.Instance.Dispose();
        }
        private static void Update(GameTime gameTime) 
        {
            if (!_lfsr.Finished)
            {
                Coords coordinate = _lfsr.Coordinate;

                int x = (int)coordinate.X;
                int y = (int)coordinate.Y;

                byte r = (byte)_random.Next(25, 256);
                byte g = (byte)_random.Next(25, 256);
                byte b = (byte)_random.Next(25, 256);

                if (x >= 0 && x < Width)
                {
                    if (y >= 0 && y < Height)
                    {
                        _gameConsole.Print(x, y, "X", new Color(r, g, b), Color.Black);
                    }
                }

                _lfsr.Next();
            }
            else 
            {
                _gameConsole.Print(0, 0, "Finished!", Color.White, Color.Blue);
            }
        }
        private static void Init() 
        {
            _lfsr = new LFSR(Width, Height);

            _gameConsole = SadConsole.Global.CurrentScreen;
        }
    }
}