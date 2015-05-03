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
            game.StartScreen();
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

        List<int> highScores = new List<int>() { 14001, 17834, 13038, 9874, 7453, 37972, 8757, 12037, 9748, 9272};

        private Random randomNumGen = new Random();

        public HyperSpace()
        {
            this.Score = 0;
            this.Speed = 0;
            this.ObstacleList = new List<Obstacle>();
            this.SpaceShip = new Obstacle((Console.WindowWidth / 2) - 1, (Console.WindowHeight - 1));
            this.SpaceShip.Symbol = "^";
            this.SpaceShip.Color = ConsoleColor.Red;
            Console.BufferHeight = 30;
            Console.WindowHeight = 30;
            Console.BufferWidth = 100;
            Console.WindowWidth = 100;

        }
        public void PlayGame()
        {

            int x = randomNumGen.Next(0, Console.WindowWidth - 2);
            int y = 5;
            while (Smashed == false)
            {
                bool isSpaceRift = false;
                if (randomNumGen.Next(0, 101) < 20)
                {
                    isSpaceRift = true;

                    ObstacleList.Add(new Obstacle(randomNumGen.Next(0, Console.WindowWidth - 2), 5, ConsoleColor.Green, "%", isSpaceRift));
                }
                else
                {
                    ObstacleList.Add(new Obstacle(randomNumGen.Next(5, Console.WindowWidth - 2), y));

                }
                MoveShip();
                MoveObstacles();
                DrawGame();
                int accelorator = 0;
                if (Speed < 75 && accelorator < 5)
                {

                    Speed += 1;
                }
                else
                {
                    accelorator++;
                }

                Thread.Sleep(100 - Speed);
            }

            HighScore(Score);
        }

        public void MoveShip()
        {

            ConsoleKeyInfo keyPressed = new ConsoleKeyInfo();

            while (Console.KeyAvailable)
            {
                keyPressed = Console.ReadKey(true);
            }
            if (keyPressed.Key == ConsoleKey.LeftArrow && SpaceShip.X - 1 > 0)
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
                    if (Speed - 50 < 0)
                    {
                        Speed = 0;
                    }
                    else
                    {
                        Speed = Speed - 50;
                    }
                    ObstacleList.Remove(ObstacleList[i]);
                    Score += 50;


                }
                else if (ObstacleList[i].Y == SpaceShip.Y && ObstacleList[i].X == SpaceShip.X)
                {
                    Smashed = true;
                }
                if (ObstacleList[i].Y < Console.WindowHeight)
                {

                    Score += 1;
                    newObstacleList.Add(this.ObstacleList[i]);
                }

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

        public void StartScreen()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            string Logo = @"
███████╗██████╗  █████╗  ██████╗███████╗    ██████╗  █████╗  ██████╗███████╗    ██╗  ██╗
██╔════╝██╔══██╗██╔══██╗██╔════╝██╔════╝    ██╔══██╗██╔══██╗██╔════╝██╔════╝    ██║  ██║
███████╗██████╔╝███████║██║     █████╗      ██████╔╝███████║██║     █████╗      ███████║
╚════██║██╔═══╝ ██╔══██║██║     ██╔══╝      ██╔══██╗██╔══██║██║     ██╔══╝      ╚════██║
███████║██║     ██║  ██║╚██████╗███████╗    ██║  ██║██║  ██║╚██████╗███████╗         ██║
╚══════╝╚═╝     ╚═╝  ╚═╝ ╚═════╝╚══════╝    ╚═╝  ╚═╝╚═╝  ╚═╝ ╚═════╝╚══════╝         ╚═╝";
            for (int i = 0; i < 17; i++)
            {
                Thread.Sleep(200);
                Console.Clear();
                Logo = "\n" + Logo;
                Console.WriteLine(Logo);
            }
            string space = @"
                 .  . '    .                                             
      '   .            . '            .                +           
              `                          '    . '
        .                         ,'`.                         .
   .                  ..     _.-;'    `.              .    
              _.- `.##% _.--  ,'        `.             #     ___,,od000
           ,' -_ _.-.-- \   ,'            `-_       '%#%',,/////00000HH
         ,'     |_.'     )`/-     __..--  `-_`-._    J L/////00000HHHHM
 . +   ,'   _.-         / /   _-             `-._`-_/___\///0000HHHHMMM
     .'_.-        '    :_/_.-'                 _,`-/__V__\0000HHHHHMMMM
 . _-                           .        '   _,////\  |  /000HHHHHMMMMM
_-    .       '  +  .              .        ,//////0\ | /00HHHHHHHMMMMM
       `                                   ,//////000\|/00HHHHHHHMMMMMM
.             '       .  ' .   .       '  ,//////00000|00HHHHHHHHMMMMMM
     .             .    .    '           ,//////000000|00HHHHHHHMMMMMMM
                  .  '      .       .   ,///////000000|0HHHHHHHHMMMMMMM
";

            Logo = @"
███████╗██████╗  █████╗  ██████╗███████╗    ██████╗  █████╗  ██████╗███████╗    ██╗  ██╗
██╔════╝██╔══██╗██╔══██╗██╔════╝██╔════╝    ██╔══██╗██╔══██╗██╔════╝██╔════╝    ██║  ██║
███████╗██████╔╝███████║██║     █████╗      ██████╔╝███████║██║     █████╗      ███████║
╚════██║██╔═══╝ ██╔══██║██║     ██╔══╝      ██╔══██╗██╔══██║██║     ██╔══╝      ╚════██║
███████║██║     ██║  ██║╚██████╗███████╗    ██║  ██║██║  ██║╚██████╗███████╗         ██║
╚══════╝╚═╝     ╚═╝  ╚═╝ ╚═════╝╚══════╝    ╚═╝  ╚═╝╚═╝  ╚═╝ ╚═════╝╚══════╝         ╚═╝";
            int flash = 0;
            while (flash < 5)
            {
                Console.Clear();

                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine(space);

                Console.ForegroundColor = ConsoleColor.Red;
                Thread.Sleep(200);
                Console.WriteLine(Logo);
                Thread.Sleep(300);
                flash++;
            }
            string coinage = "                                       50 Cents To Play:";
            foreach (var letter in coinage)
            {
                Console.Write(letter);
                Thread.Sleep(20);
            }
            while (flash > 0)
            {
                Console.Clear();

                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine(space);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(Logo);
                Console.WriteLine(coinage);
                Thread.Sleep(200);

                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine(space);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(Logo);
                Thread.Sleep(200);
                flash--;
            }
            Console.WriteLine(coinage);
            Console.ReadKey();
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine(space);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(Logo);

            Console.WriteLine(@"
   __
   \ \_____
###[==_____>
   /_/  ");

            Thread.Sleep(200);
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine(space);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(Logo);

            Console.WriteLine(@"
           __
           \ \_____
        ###[==_____>
           /_/");

            Thread.Sleep(200);
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine(space);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(Logo);

            Console.WriteLine(@"
                    __
                    \ \_____
                 ###[==_____>
                    /_/");

            Thread.Sleep(200);
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine(space);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(Logo);

            Console.WriteLine(@"
                              __
                              \ \_____
                           ###[==_____>
                              /_/");


            Thread.Sleep(200);
            while (flash < 5)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine(space);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(Logo);

                Console.WriteLine(coinage);
                Console.WriteLine(@"
   __________  ______                   __
  / ____/ __ \/ / / /                   \ \_____
 / / __/ / / / / / /                 ###[==_____>
/ /_/ / /_/ /_/_/_/                     /_/
\____/\____(_|_|_)    ");

                Thread.Sleep(200);
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine(space);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(Logo);

                Console.WriteLine(coinage);
                Thread.Sleep(70);
                flash++;

            }

            Console.ResetColor();
        }

        public void HighScore(int playerScore)
        {
            string newHighScore = @"
                          __    _       __                                 ______
   ____  ___ _      __   / /_  (_)___ _/ /_     ______________  ________  / / / /
  / __ \/ _ \ | /| / /  / __ \/ / __ `/ __ \   / ___/ ___/ __ \/ ___/ _ \/ / / / 
 / / / /  __/ |/ |/ /  / / / / / /_/ / / / /  (__  ) /__/ /_/ / /  /  __/_/_/_/  
/_/ /_/\___/|__/|__/  /_/ /_/_/\__, /_/ /_/  /____/\___/\____/_/   \___(_|_|_)   
                              /____/                                            ";
             string highScore = @"
    ▓██   ██▓ ▒█████   █    ██     ██▓     ▒█████    ██████ ▓█████ 
    ▒██  ██▒▒██▒  ██▒ ██  ▓██▒   ▓██▒    ▒██▒  ██▒▒██    ▒ ▓█   ▀ 
      ▒██ ██░▒██░  ██▒▓██  ▒██░   ▒██░    ▒██░  ██▒░ ▓██▄   ▒███   
      ░ ▐██▓░▒██   ██░▓▓█  ░██░   ▒██░    ▒██   ██░  ▒   ██▒▒▓█  ▄ 
      ░ ██▒▓░░ ████▓▒░▒▒█████▓    ░██████▒░ ████▓▒░▒██████▒▒░▒████▒
       ██▒▒▒ ░ ▒░▒░▒░ ░▒▓▒ ▒ ▒    ░ ▒░▓  ░░ ▒░▒░▒░ ▒ ▒▓▒ ▒ ░░░ ▒░ ░
     ▓██ ░▒░   ░ ▒ ▒░ ░░▒░ ░ ░    ░ ░ ▒  ░  ░ ▒ ▒░ ░ ░▒  ░ ░ ░ ░  ░
     ▒ ▒ ░░  ░ ░ ░ ▒   ░░░ ░ ░      ░ ░   ░ ░ ░ ▒  ░  ░  ░     ░   
     ░ ░         ░ ░     ░            ░  ░    ░ ░        ░     ░  
 _______ _______  _____   ______ _______ ______   _____  _______  ______ ______ 
 |______ |       |     | |_____/ |______ |_____] |     | |_____| |_____/ |     \
 ______| |_____  |_____| |    \_ |______ |_____] |_____| |     | |    \_ |_____/
                                                                                ";
            
            bool newScoreboard = false;

            highScores.Sort();
            highScores.Reverse();
            highScores.Take(10);
            foreach (int score in highScores.Take(10))
            {
                if (playerScore >= score)
                {
                    newScoreboard = true;
                }
            }
            highScores.Add(playerScore);
            int place = 1;
            highScores.Sort();
            highScores.Reverse();
            

            if (newScoreboard == true)
            {
                Console.WriteLine(newHighScore);
                foreach (int number in highScores.Take(10))
                {
                    Thread.Sleep(500);
                    if (number == playerScore)
                    {

                        Console.WriteLine("YOUR SCORE: " + place + ". " + number);
                    }
                    else
                    {
                        Console.WriteLine(place + ". " + number);
                    }
                    place++;
                    if (place > 10)
                    {
                        break;
                    }
                }
            }
            else
            {
                Console.WriteLine(highScore);
                foreach (int number in highScores)
                {
                    Thread.Sleep(500);
                    Console.WriteLine(place + ". " + number);
                    place++;
                    if(place > 10)
                    {
                        break;
                    }
                }
            }
            Thread.Sleep(2000);
            PlayAgain();
        }
        public void PlayAgain()
        {
            Console.Clear();
            string continueText = @" ██████╗ ██████╗ ███╗   ██╗████████╗██╗███╗   ██╗██╗   ██╗███████╗██████╗ 
██╔════╝██╔═══██╗████╗  ██║╚══██╔══╝██║████╗  ██║██║   ██║██╔════╝╚════██╗
██║     ██║   ██║██╔██╗ ██║   ██║   ██║██╔██╗ ██║██║   ██║█████╗    ▄███╔╝
██║     ██║   ██║██║╚██╗██║   ██║   ██║██║╚██╗██║██║   ██║██╔══╝    ▀▀══╝ 
╚██████╗╚██████╔╝██║ ╚████║   ██║   ██║██║ ╚████║╚██████╔╝███████╗  ██╗   
 ╚═════╝ ╚═════╝ ╚═╝  ╚═══╝   ╚═╝   ╚═╝╚═╝  ╚═══╝ ╚═════╝ ╚══════╝  ╚═╝   
                                                                        `";
            int count = 10;
            while (count > 0)
            {

                ConsoleKeyInfo keyPressed = new ConsoleKeyInfo();

                while (Console.KeyAvailable)
                {
                    keyPressed = Console.ReadKey(true);
                    Smashed = false;
                    Score = 0;
                    Speed = 0;
                    ObstacleList.Clear();
                    PlayGame();
                }
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.Clear();
                Console.WriteLine(continueText);
                Console.WriteLine("   {0}", count);
                Thread.Sleep(1000);
                count--;
            }
            while (count < 15)
            {
                Console.Clear();
                Thread.Sleep(200);

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(@"
  ▄████  ▄▄▄       ███▄ ▄███▓▓█████     ▒█████   ██▒   █▓▓█████  ██▀███  
 ██▒ ▀█▒▒████▄    ▓██▒▀█▀ ██▒▓█   ▀    ▒██▒  ██▒▓██░   █▒▓█   ▀ ▓██ ▒ ██▒
▒██░▄▄▄░▒██  ▀█▄  ▓██    ▓██░▒███      ▒██░  ██▒ ▓██  █▒░▒███   ▓██ ░▄█ ▒
░▓█  ██▓░██▄▄▄▄██ ▒██    ▒██ ▒▓█  ▄    ▒██   ██░  ▒██ █░░▒▓█  ▄ ▒██▀▀█▄  
░▒▓███▀▒ ▓█   ▓██▒▒██▒   ░██▒░▒████▒   ░ ████▓▒░   ▒▀█░  ░▒████▒░██▓ ▒██▒
 ░▒   ▒  ▒▒   ▓▒█░░ ▒░   ░  ░░░ ▒░ ░   ░ ▒░▒░▒░    ░ ▐░  ░░ ▒░ ░░ ▒▓ ░▒▓░
  ░   ░   ▒   ▒▒ ░░  ░      ░ ░ ░  ░     ░ ▒ ▒░    ░ ░░   ░ ░  ░  ░▒ ░ ▒░
░ ░   ░   ░   ▒   ░      ░      ░      ░ ░ ░ ▒       ░░     ░     ░░   ░ 
      ░       ░  ░       ░      ░  ░       ░ ░        ░     ░  ░   ░     
                                                     ░        
");
                Thread.Sleep(400);
            }
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

        public static List<string> obstacleList = new List<string>() { "+", "_ ", "&", "#", "@", ":", ";", "'", };
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
            this.Color = color;
            this.Symbol = symbol;
            this.IsSpaceRift = isSpaceRift;

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
