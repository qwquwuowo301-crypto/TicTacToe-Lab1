// Автор: Москвитина Александра
// Группа: ИСП(9)-23-1

using System;

class TicTacToe
{
    static char[] board = { '1', '2', '3', '4', '5', '6', '7', '8', '9' };
    static int player = 1;
    static int choice;
    static int flag = 0;

    static void Main()
    {
        do
        {
            Console.Clear();
            Console.WriteLine("Игрок 1 - X, Игрок 2 - O");
            Console.WriteLine($"\nХод игрока {player}");

            DrawBoard();

            bool isValid = int.TryParse(Console.ReadLine(), out choice);

            if (isValid && choice >= 1 && choice <= 9 && board[choice - 1] != 'X' && board[choice - 1] != 'O')
            {
                if (player == 1)
                    board[choice - 1] = 'X';
                else
                    board[choice - 1] = 'O';

                flag = CheckWin();

                if (player == 1)
                    player = 2;
                else
                    player = 1;
            }
            else
            {
                Console.WriteLine("Не сюда! Нажмите любую клавишу для продолжения");
                Console.ReadKey();
            }
        }
        while (flag == 0);

        Console.Clear();
        DrawBoard();

        if (flag == 1)
            Console.WriteLine($"\nИгрок {(player == 1 ? 2 : 1)} победил! ура!");
        else
            Console.WriteLine("\nПобедила дружба <3");

        Console.WriteLine("Нажмите любую клавишу для выхода");
        Console.ReadKey();
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
            if (cell != 'X' && cell != 'O')
                return 0;
        }

        return -1;
    }
}