// Автор: Москвитина Александра
// Группа: ИСП(9)-23-1

// Автор: Москвитина Александра
// Группа: ИСП(9)-23-1
// Версия 2.0: Добавлен счетчик побед и кастомизация символов

using System;

class TicTacToe
{
    static char[] board = { '1', '2', '3', '4', '5', '6', '7', '8', '9' };
    static int player = 1;
    static int choice;
    static int flag = 0;

    // Счетчики побед
    static int player1Wins = 0;
    static int player2Wins = 0;
    static int draws = 0;

    // Кастомизируемые символы
    static char player1Symbol = 'X';
    static char player2Symbol = 'O';

    static void Main()
    {
        Console.WriteLine("Добро пожаловать в крестики-нолики!");
        Console.Write("Желаете кастомизировать символы? (да/нет): ");
        string customize = Console.ReadLine().ToLower();

        if (customize == "да" || customize == "yes" || customize == "y" || customize == "lf")
        {
            CustomizeSymbols();
        }

        do
        {
            PlayGame();

            Console.WriteLine("\nСтатистика:");
            Console.WriteLine($"Игрок 1 ({player1Symbol}): {player1Wins} побед");
            Console.WriteLine($"Игрок 2 ({player2Symbol}): {player2Wins} побед");
            Console.WriteLine($"Ничьих: {draws}");

            Console.Write("\nХотите сыграть еще? (да/нет): ");
            string playAgain = Console.ReadLine().ToLower();

            if (playAgain != "да" && playAgain != "yes" && playAgain != "y" && playAgain != "lf")
                break;

            ResetBoard();

        } while (true);

        Console.WriteLine("\nСпасибо за игру! Финальный счет:");
        Console.WriteLine($"Игрок 1 ({player1Symbol}): {player1Wins} : {player2Wins} Игрок 2 ({player2Symbol})");
        Console.WriteLine($"Ничьих: {draws}");
        Console.WriteLine("Нажмите любую клавишу для выхода");
        Console.ReadKey();
    }

    static void CustomizeSymbols()
    {
        Console.Write($"Введите символ для Игрока 1 (сейчас {player1Symbol}): ");
        string input = Console.ReadLine();
        if (!string.IsNullOrEmpty(input))
            player1Symbol = input[0];

        Console.Write($"Введите символ для Игрока 2 (сейчас {player2Symbol}): ");
        input = Console.ReadLine();
        if (!string.IsNullOrEmpty(input))
            player2Symbol = input[0];

        if (player1Symbol == player2Symbol)
        {
            Console.WriteLine("Символы не должны совпадать! Используем стандартные X и O");
            player1Symbol = 'X';
            player2Symbol = 'O';
        }
    }

    static void PlayGame()
    {
        do
        {
            Console.Clear();
            Console.WriteLine($"Игрок 1 - {player1Symbol}, Игрок 2 - {player2Symbol}");
            Console.WriteLine($"Счет: {player1Wins} : {player2Wins} (Ничьих: {draws})");
            Console.WriteLine($"\nХод игрока {player} ({GetCurrentSymbol()})");

            DrawBoard();

            bool isValid = int.TryParse(Console.ReadLine(), out choice);

            if (isValid && choice >= 1 && choice <= 9 &&
                board[choice - 1] != player1Symbol && board[choice - 1] != player2Symbol)
            {
                board[choice - 1] = GetCurrentSymbol();

                flag = CheckWin();

                if (flag == 0)
                {
                    if (player == 1)
                        player = 2;
                    else
                        player = 1;
                }
            }
            else
            {
                Console.WriteLine("Ой ой! Нажмите любую клавишу для продолжения");
                Console.ReadKey();
            }
        }
        while (flag == 0);

        Console.Clear();
        DrawBoard();

        if (flag == 1)
        {
            int winner = (player == 1) ? 2 : 1;
            Console.WriteLine($"\nИгрок {winner} ({GetSymbolByPlayer(winner)}) победил! Поздравляем!");

            if (winner == 1)
                player1Wins++;
            else
                player2Wins++;
        }
        else
        {
            Console.WriteLine("\nНичья! Победила дружба <3");
            draws++;
        }

        flag = 0;
    }

    static char GetCurrentSymbol()
    {
        return player == 1 ? player1Symbol : player2Symbol;
    }

    static char GetSymbolByPlayer(int playerNumber)
    {
        return playerNumber == 1 ? player1Symbol : player2Symbol;
    }

    static void ResetBoard()
    {
        for (int i = 0; i < 9; i++)
        {
            board[i] = char.Parse((i + 1).ToString());
        }
        player = 1;
        flag = 0;
    }

    static void DrawBoard()
    {
        Console.WriteLine($"\n {board[0]} | {board[1]} | {board[2]} ");
        Console.WriteLine("---+---+---");
        Console.WriteLine($" {board[3]} | {board[4]} | {board[5]} ");
        Console.WriteLine("---+---+---");
        Console.WriteLine($" {board[6]} | {board[7]} | {board[8]} \n");
    }

    static int CheckWin()
    {
        int[,] winConditions = new int[,]
        {
            {0,1,2}, {3,4,5}, {6,7,8},
            {0,3,6}, {1,4,7}, {2,5,8},
            {0,4,8}, {2,4,6}
        };

        for (int i = 0; i < winConditions.GetLength(0); i++)
        {
            if (board[winConditions[i, 0]] == board[winConditions[i, 1]] &&
                board[winConditions[i, 1]] == board[winConditions[i, 2]])
            {
                return 1;
            }
        }

        foreach (char cell in board)
        {
            if (cell != player1Symbol && cell != player2Symbol)
                return 0;
        }

        return -1;
    }
}