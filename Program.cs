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

        static int MakeChoice(int maxNumber = 5)
        {
            int choice = ReadInt();
            while (choice > maxNumber || choice < 1)
            {
                Console.Write("Неверный ввод. Повторите попытку: ");
                choice = ReadInt();
            }
            return choice;
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

        static void Main(string[] args)
        {
            DataEditor dataEditor = new DataEditor();
            int choice;
            bool exit = false;
            while (exit == false)
            {
                ShowMenu();
                choice = MakeChoice();
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

        class DataEditor
        {
            private List<Player> _players;

            public DataEditor()
            {
                _players = new List<Player>();
            }

            public void ShowPlayers()
            {
                foreach (var player in _players)
                {
                    player.ShowInfo();
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
                level = ReadInt();
                Console.WriteLine("Забанен ли игрок? 1 - да, 2 - нет.");
                int choice = ReadInt();
                while (choice != 1 && choice != 2)
                {
                    Console.WriteLine("Неверный ввод. Повторите попытку: ");
                    choice = ReadInt();
                }
                isBanned = choice == 1;
                Player player = new Player(nickName, level, isBanned, _players.Count + 1);
                _players.Add(player);
                Console.WriteLine("Игрок добавлен!");                
            }

            public void BlockPlayer()
            {
                ShowPlayers();
                Console.Write("Введите номер игрока, которого хотите забанить: ");
                int choice = MakeChoice(_players.Count);
                _players[choice - 1].Block();
            }

            public void UnblockPlayer()
            {
                ShowPlayers();
                Console.Write("Введите номер игрока, которого хотите разбанить: ");
                int choice = MakeChoice(_players.Count);
                _players[choice - 1].UnBlock();
            }

            public void DeletePlayer()
            {
                ShowPlayers();
                Console.Write("Введите номер игрока, которого хотите удалить: ");
                int choice = MakeChoice(_players.Count);
                for (int i = choice; i < _players.Count; i++)
                {
                    _players[i].DecOrderNumber();
                }
                _players.RemoveAt(choice - 1);
            }
        }

        class Player
        {
            public string NickName { get; private set; }
            public int Level { get; private set; }
            public bool IsBanned { get; private set; }
            public int OrderNumber { get; private set; }

            public Player(string nickName, int level, bool isBanned, int orderNumber)
            {
                NickName = nickName;
                Level = level;
                IsBanned = isBanned;
                OrderNumber = orderNumber;
            }

            public void DecOrderNumber()
            {
                OrderNumber--;
            }           

            public void Block()
            {
                IsBanned = true;
            }

            public void UnBlock()
            {
                IsBanned = false;
            }

            public void ShowInfo()
            {
                Console.Write($"Номер: {OrderNumber}, никнейм: {NickName}, уровень: {Level}, cтатус - ");
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
}
