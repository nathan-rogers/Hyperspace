using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Hyperspace
{
    class Program
    {
        static void Main(string[] args)
        {

            HyperSpace game = new HyperSpace();
            game.PlayGame();

        }
    }
    #region
    public class HyperSpace
    {
        public int Score { get; set; }
        public int Speed { get; set; }
        public List<Obstacle> ObstacleList { get; set; }
        public Obstacle SpaceShip { get; set; }
        bool Smashed { get; set; }

        private Random randomNumGen = new Random();

        public HyperSpace()
        {
            this.Score = 0;
            this.Speed = 0;
            this.ObstacleList = new List<Obstacle>( );
            this.SpaceShip = new Obstacle((Console.WindowWidth / 2) - 1,(Console.WindowHeight - 1));
            this.SpaceShip.Symbol = "^";
            this.SpaceShip.Color = ConsoleColor.DarkRed;
            Console.BufferHeight = 30;
            Console.WindowHeight = 30;
            Console.BufferWidth = 100;
            Console.WindowWidth = 100;

        }
        //TODO finish this section
        public void PlayGame()
        {

            int x = randomNumGen.Next(0, Console.WindowWidth - 2);
            int y = 5;
            while (Smashed == false)
            {
                bool isSpaceRift = false;
                if (randomNumGen.Next(0, 101) < 10)
                {
                    isSpaceRift = true;

                    ObstacleList.Add(new Obstacle(randomNumGen.Next(0, Console.WindowWidth - 2), 5, ConsoleColor.Green, "%", isSpaceRift));
                }
                else
                {
                    ObstacleList.Add(new Obstacle(randomNumGen.Next(0, Console.WindowWidth - 2), y));
                }
                MoveShip();
                MoveObstacles();
                DrawGame();
                if (Speed < 170)
                {
                    Speed += 1;
                }

                Thread.Sleep(240 - Speed);
            }
            
        }

        public void MoveShip()
        {

            ConsoleKeyInfo keyPressed = new ConsoleKeyInfo();

            while (Console.KeyAvailable)
            {
               keyPressed = Console.ReadKey(true);
            }
            if (keyPressed.Key == ConsoleKey.LeftArrow && SpaceShip.X - 1> 0)
            {
                SpaceShip.X = SpaceShip.X - 1;
            }
            if (keyPressed.Key == ConsoleKey.RightArrow && SpaceShip.Y + 1 < Console.WindowWidth - 2)
            {
                SpaceShip.X += 1;
            }
        }

        public void MoveObstacles()
        {
            List<Obstacle> newObstacleList = new List<Obstacle>();
            for (int i = 0; i < ObstacleList.Count(); i++)
            {
                ObstacleList[i].Y += 1;
                if (ObstacleList[i].Y == SpaceShip.Y && ObstacleList[i].X == SpaceShip.X && ObstacleList[i].IsSpaceRift == true)
                {
                    Speed = Speed - 50;

                }
                if (ObstacleList[i].Y == SpaceShip.Y && ObstacleList[i].X == SpaceShip.X)
                {
                    Smashed = true;
                }
                if (ObstacleList[i].Y < Console.WindowHeight)
                {
                    newObstacleList.Add(this.ObstacleList[i]);
                }
                Score += 1;
            }
            ObstacleList = newObstacleList;
        }
        public void DrawGame()
        {
            Console.Clear();
            SpaceShip.Draw();
            foreach (var debris in ObstacleList)
            {
                debris.Draw();
            }
            PrintAtPosition(20, 2, "Score: " + this.Score, ConsoleColor.Green);
            PrintAtPosition(20, 3, "Speed: " + this.Speed, ConsoleColor.Green);
           
        }

        public void PrintAtPosition(int x, int y, string text, ConsoleColor color)
        {
            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = color;
            Console.Write(text);
        }

    }

    #endregion
    #   region
    public class Obstacle
    {
        public int X { get; set; }
        public int Y { get; set; }
        public ConsoleColor Color { get; set; }
        public string Symbol { get; set; }
        public bool IsSpaceRift { get; set; }

        public static List<string> obstacleList = new List<string>() { "!", "*", ":", ";", "'",};
        public static Random randomNumberGenerator = new Random();

        public Obstacle(int x, int y)
        {
            this.X = x;
            this.Y = y;
            this.Color = ConsoleColor.Cyan;
            this.Symbol = string.Format(obstacleList[randomNumberGenerator.Next(0, obstacleList.Count())], ConsoleColor.Cyan);
        }
        public Obstacle(int x, int y, ConsoleColor color, string symbol, bool isSpaceRift)
        {
            this.X = x;
            this.Y = y;
            this.Symbol = symbol;
            this.IsSpaceRift = IsSpaceRift;

        }

        public void Draw()
        {
            Console.SetCursorPosition(X, Y);
            Console.ForegroundColor = Color;
            Console.Write(Symbol);
        }



    }
    #endregion

}
