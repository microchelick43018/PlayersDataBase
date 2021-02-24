using System;
using System.Collections.Generic;
namespace PlayersDataBase
{
    class Program
    {
        static void ShowMenu()
        {
            Console.WriteLine("Выберите одну из функций:");
            Console.WriteLine("1 - Добавить игрока.");
            Console.WriteLine("2 - Забанить игрока.");
            Console.WriteLine("3 - Разбанить игрока.");
            Console.WriteLine("4 - Удалить игрока.");
            Console.WriteLine("5 - Выход.");
        }

        static void Main(string[] args)
        {
            DataEditor dataEditor = new DataEditor();
            int choice;
            bool exit = false;
            while (exit == false)
            {
                ShowMenu();
                choice = InputChecker.MakeChoice();
                switch (choice)
                {
                    case 1:
                        dataEditor.AddPlayer();
                        break;
                    case 2:
                        dataEditor.BlockPlayer();
                        break;
                    case 3:
                        dataEditor.UnblockPlayer();
                        break;
                    case 4:
                        dataEditor.DeletePlayer();
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
    }

    class InputChecker
    {
        public static int ReadInt()
        {
            bool isCorrected = int.TryParse(Console.ReadLine(), out int choice);
            while (isCorrected == false)
            {
                Console.WriteLine("Неверный ввод. Повторите попытку: ");
                isCorrected = int.TryParse(Console.ReadLine(), out choice);
            }
            return choice;
        }

        public static int MakeChoice(int maxNumber = 5)
        {
            int choice = ReadInt();
            while (choice > maxNumber || choice < 1)
            {
                Console.Write("Неверный ввод. Повторите попытку: ");
                choice = ReadInt();
            }
            return choice;
        }
    }

    class DataEditor
    {
        private List<Player> _players;

        public DataEditor()
        {
            _players = new List<Player>();
        }

        public void ShowPlayers()
        {
            for (int i = 0; i < _players.Count; i++)
            {
                _players[i].ShowInfo(i + 1);
            }
        }

        public void AddPlayer()
        {
            string nickName;
            int level;
            bool isBanned;
            Console.WriteLine("Введите Никнейм игрока: ");
            nickName = Console.ReadLine();
            Console.WriteLine("Введите уровень игрока: ");
            level = InputChecker.ReadInt();
            Console.WriteLine("Забанен ли игрок? 1 - да, 2 - нет.");
            int choice = InputChecker.ReadInt();
            while (choice != 1 && choice != 2)
            {
                Console.WriteLine("Неверный ввод. Повторите попытку: ");
                choice = InputChecker.ReadInt();
            }
            isBanned = choice == 1;
            Player player = new Player(nickName, level, isBanned);
            _players.Add(player);
            Console.WriteLine("Игрок добавлен!");
        }

        public void BlockPlayer()
        {
            if (CountChecker() == true)
            {
                ShowPlayers();
                Console.Write("Введите номер игрока, которого хотите забанить: ");
                int choice = InputChecker.MakeChoice(_players.Count);
                _players[choice - 1].Block();
            }
        }

        public void UnblockPlayer()
        {
            if (CountChecker() == true)
            {
                ShowPlayers();
                Console.Write("Введите номер игрока, которого хотите разбанить: ");
                int choice = InputChecker.MakeChoice(_players.Count);
                _players[choice - 1].UnBlock();
            }
        }

        public void DeletePlayer()
        {
            if (CountChecker() == true)
            {
                ShowPlayers();
                Console.Write("Введите номер игрока, которого хотите удалить: ");
                int choice = InputChecker.MakeChoice(_players.Count);
                _players.RemoveAt(choice - 1);
            }
        }

        public bool CountChecker()
        {
            if (_players.Count == 0)
            {
                Console.WriteLine("Листа не существует! Добавьте хотя бы одного игрока.");
                return false;
            }
            else
            {
                return true;
            }
        }
    }

    class Player
    {
        public string NickName { get; private set; }
        public int Level { get; private set; }
        public bool IsBanned { get; private set; }

        public Player(string nickName, int level, bool isBanned)
        {
            NickName = nickName;
            Level = level;
            IsBanned = isBanned;
        }

        public void Block()
        {
            IsBanned = true;
        }

        public void UnBlock()
        {
            IsBanned = false;
        }

        public void ShowInfo(int orderNumber)
        {
            Console.Write($"Номер: {orderNumber}, никнейм: {NickName}, уровень: {Level}, cтатус - ");
            if (IsBanned == true)
            {
                Console.WriteLine("забанен");
            }
            else
            {
                Console.WriteLine("не забанен");
            }
        }
    }
}
