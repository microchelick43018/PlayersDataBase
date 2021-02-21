using System;
using System.Collections.Generic;
namespace PlayersDataBase
{
    class Program
    {
        static int ReadInt()
        {
            bool isCorrected = int.TryParse(Console.ReadLine(), out int choice);
            while (isCorrected == false)
            {
                Console.WriteLine("Неверный ввод. Повторите попытку: ");
                isCorrected = int.TryParse(Console.ReadLine(), out choice);
            }
            return choice;
        }

        static int MakeMenuChoice()
        {
            int choice = ReadInt();
            while (choice > 5 || choice < 0)
            {
                Console.Write("Неверный ввод. Повторите попытку: ");
                choice = ReadInt();
            }
            return choice;
        }

        static int MakeListChoice(int listCount)
        {
            int choice = ReadInt();
            while (choice > listCount || choice < 1)
            {
                Console.Write("Неверный ввод. Повторите попытку: ");
                choice = ReadInt();
            }
            return choice;
        }

        static void ShowPlayers(List<Player> players)
        {
            foreach (var player in players)
            {
                player.ShowInfo();
            }
        }

        static void ShowMenu()
        {
            Console.WriteLine("Выберите одну из функций:");
            Console.WriteLine("1 - Добавить игрока.");
            Console.WriteLine("2 - Забанить игрока.");
            Console.WriteLine("3 - Разбанить игрока.");
            Console.WriteLine("4 - Удалить игрока.");
            Console.WriteLine("5 - Выход.");
        }

        static void AddPlayer(List<Player> players)
        {
            string nickName;
            int level;
            bool isBanned;
            int orderNumber;

            Console.WriteLine("Введите Никнейм игрока: ");
            nickName = Console.ReadLine();
            Console.WriteLine("Введите уровень игрока: ");
            level = ReadInt();
            Console.WriteLine("Забанен ли игрок? 1 - да, 2 - нет.");
            int choice = ReadInt();
            while (choice != 1 && choice != 2)
            {
                Console.WriteLine("Неверный ввод. Повторите попытку: ");
                choice = ReadInt();
            }
            if (choice == 1) { isBanned = true; }
            else { isBanned = false; }
            Player player = new Player(nickName, level, isBanned);
            players.Add(player);
            Console.WriteLine("Игрок добавлен!");
        }

        static void ChangePlayerBanStatus(List<Player> players, bool banStatus)
        {
            ShowPlayers(players);
            Console.Write("Введите номер игрока, которого хотите ");
            if (banStatus == true) { Console.Write("забанить:"); }
            else { Console.Write("разбанить: "); }
            int choice = MakeListChoice(players.Count);
            players[choice - 1].ChangeStatus(banStatus);
            if (banStatus == true) { Console.WriteLine("Игрок забанен!"); }
            else { Console.WriteLine("Игрок разбанен!"); }
        }

        static void DeletePlayer(List<Player> players)
        {
            ShowPlayers(players);
            Console.Write("Введите номер игрока, которого хотите удалить: ");
            int choice = MakeListChoice(players.Count);
            for (int i = choice; i < players.Count; i++)
            {
                players[i].DecOrderNumber();
            }
            Player.DecCount();
            players.RemoveAt(choice);
        }

        static void Main(string[] args)
        {
            var players = new List<Player>();
            int choice;
            bool exit = false;
            while (exit == false)
            {
                ShowMenu();
                choice = MakeMenuChoice();
                switch (choice)
                {
                    case 1:
                        AddPlayer(players);
                        break;
                    case 2:
                        ChangePlayerBanStatus(players, true);
                        break;
                    case 3:
                        ChangePlayerBanStatus(players, false);
                        break;
                    case 4:
                        DeletePlayer(players);
                        break;
                    case 5:
                        exit = true;
                        break;
                    default:
                        break;
                }
                Console.WriteLine("Нажмите на любую клавишу чтобы продолжить.");
                Console.ReadKey();
                Console.Clear();
            }

        }

        class Player
        {
            private bool _isBanned;
            private int _orderNumber;
            static private int _playersCount;

            public string NickName { get; private set; }
            public int Level { get; private set; }
            public bool IsBanned { get => _isBanned; private set => _isBanned = value; }
            public int OrderNumber { get => _orderNumber; private set => _orderNumber = value; }
            public static int PlayersCount { get => _playersCount; private set => _playersCount = value; }

            static Player()
            {
                _playersCount = 0;
            }

            public Player(string nickName, int level, bool isBanned)
            {
                NickName = nickName;
                Level = level;
                IsBanned = isBanned;
                _playersCount++;
                OrderNumber = _playersCount;
            }

            public static void DecCount()
            {
                _playersCount--;
            }

            public void DecOrderNumber()
            {
                _orderNumber--;
            }

            public void ChangeStatus(bool banStatus)
            {
                _isBanned = banStatus;
            }

            public void ShowInfo()
            {
                Console.Write($"Номер: {_orderNumber}, никнейм: {NickName}, уровень: {Level}, cтатус - ");
                if (IsBanned == true) { Console.WriteLine("забанен"); }
                else { Console.WriteLine("не забанен"); }
            }

        }
    }
}
