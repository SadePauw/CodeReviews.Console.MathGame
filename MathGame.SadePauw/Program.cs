using System;
using System.Diagnostics;

namespace MathGame.SadePauw
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            Run();
        }
        private static void Run()
        {
            Console.WriteLine("Please enter your name.");
            var name = Console.ReadLine();
            var date = DateTime.Now;

            string input;
            Console.Clear();
            Console.WriteLine($"Hello {name}, welcome to my Math Game!\n" +
                              "\nYou can enter 'Q' at any time to leave.");
            do
            {
                Console.WriteLine("What type of Math Game would you like?" +
                                  "\nA = Add " + " S = Subtract " +
                                  "\nM = Multiply " + " D = Divide " +
                                  "\nR = Random " + 
                                  "\n\nH = History " + " Q = Quit ");
                input = Console.ReadLine()?.ToUpper();

                if (!IsValidInput(input))
                {
                    Console.WriteLine("\nInvalid input. Please enter one of the specified characters.\n");
                    continue;
                }
                Console.Clear();
                GameSelection(input, name, date);
            } while (input != "Q");
        }
        private static void GameSelection(string input, string name, DateTime date)
        {
            int currentStreak;
            int difficulty;
            int questions;
            DateTime startTime;
            switch (input)
            {
                case "A":
                    difficulty = SelectDifficulty();
                    questions = SelectAmountOfQuestions();
                    currentStreak = 0;
                    startTime = DateTime.Now;
                    if (questions > 0)
                    {
                        for (int i = 0; i < questions; i++)
                        {
                            if (!Questions.AddQuestion(difficulty)) break;
                            currentStreak++;
                        }
                    }
                    else
                    {
                        while (Questions.AddQuestion(difficulty))
                        {
                            currentStreak++;
                        }
                    }
                    EndGame(name, date, startTime, currentStreak, difficulty, "Add");
                    break;
                case "S":
                    difficulty = SelectDifficulty();
                    questions = SelectAmountOfQuestions();
                    currentStreak = 0;
                    startTime = DateTime.Now;
                    if (questions > 0)
                    {
                        for (int i = 0; i < questions; i++)
                        {
                            if (!Questions.SubtractQuestion(difficulty)) break;
                            currentStreak++;
                        }
                    }
                    else
                    {
                        while (Questions.SubtractQuestion(difficulty))
                        {
                            currentStreak++;
                        }
                    }
                    EndGame(name, date, startTime, currentStreak, difficulty, "Subtract");
                    break;
                case "M":
                    difficulty = SelectDifficulty();
                    questions = SelectAmountOfQuestions();
                    currentStreak = 0;
                    startTime = DateTime.Now;
                    if (questions > 0)
                    {
                        for (int i = 0; i < questions; i++)
                        {
                            if (!Questions.MultiplyQuestion(difficulty)) break;
                            currentStreak++;
                        }
                    }
                    else
                    {
                        while (Questions.MultiplyQuestion(difficulty))
                        {
                            currentStreak++;
                        }
                    }
                    EndGame(name, date, startTime, currentStreak, difficulty, "Multiply");
                    break;
                case "D":
                    difficulty = SelectDifficulty();
                    questions = SelectAmountOfQuestions();
                    currentStreak = 0;
                    startTime = DateTime.Now;
                    if (questions > 0)
                    {
                        for (int i = 0; i < questions; i++)
                        {
                            if (!Questions.DivideQuestion(difficulty)) break;
                            currentStreak++;
                        }
                    }
                    else
                    {
                        while (Questions.DivideQuestion(difficulty))
                        {
                            currentStreak++;
                        }
                    }
                    EndGame(name, date, startTime, currentStreak, difficulty, "Divide");
                    break;
                case "R":
                    difficulty = SelectDifficulty();
                    questions = SelectAmountOfQuestions();
                    currentStreak = 0;
                    startTime = DateTime.Now;
                    if (questions > 0)
                    {
                        for (int i = 0; i < questions; i++)
                        {
                            if (!Questions.RandomQuestion(difficulty)) break;
                            currentStreak++;
                        }
                    }
                    else
                    {
                        while (Questions.RandomQuestion(difficulty))
                        {
                            currentStreak++;
                        }
                    }
                    EndGame(name, date, startTime, currentStreak, difficulty, "Random");
                    break;
                case "H":
                    if (History.GetHistory().Count < 1)
                    {
                        Console.WriteLine("\nNo history found!\n");
                        break;
                    }
                    foreach (var record in History.GetHistory())
                    {
                        Console.WriteLine($"{record}\n");
                    }
                    break;
                case "Q":
                    Environment.Exit(0);
                    break;
            }
        }
        private static string TimeElapsed(DateTime startTime)
        {
            DateTime endTime = DateTime.Now;
            TimeSpan timeElapsed = endTime - startTime;
            return timeElapsed.ToString("hh\\:mm\\:ss");
        }
        private static int SelectAmountOfQuestions()
        {
            while (true)
            {
                Console.WriteLine("Please select number of questions." +
                                  "\nLeave empty for infinite");
                var numberOfQuestions = Console.ReadLine();
                if (int.TryParse(numberOfQuestions, out int number))
                {
                    if (number > 0)
                    {
                        Console.Clear();
                        Console.WriteLine($"You entered {number}.");
                        return number;
                    }
                    Console.Clear();
                    Console.WriteLine($"You selected an infinite amount of questions.");
                    return 0;
                }
                else if (numberOfQuestions == string.Empty)
                {
                    Console.Clear();
                    Console.WriteLine($"You selected an infinite amount of questions.");
                    return 0;
                }
                else
                {
                    Console.WriteLine("Please enter a number.");
                }
            }
        }
        private static int SelectDifficulty()
        {
            while (true)
            {
                Console.WriteLine("Please select a difficulty:" +
                                  "\n1 = Easy" +
                                  "\n2 = Medium" +
                                  "\n3 = Hard");
                var difficulty = Console.ReadLine();
                switch (difficulty)
                {
                    case "1":
                        Console.Clear();
                        Console.WriteLine("You selected Easy.\n");
                        return 1;
                    case "2":
                        Console.Clear();
                        Console.WriteLine("You selected Medium.\n");
                        return 2;
                    case "3":
                        Console.Clear();
                        Console.WriteLine("You selected Hard.\n");
                        return 3;
                    default:
                        Console.Clear();
                        Console.WriteLine("Invalid answer.\n");
                        continue;
                }
            }
        }
        private static void EndGame(string name, DateTime date, DateTime startTime, int currentStreak, int difficulty, string mode)
        {
            var elapsed = TimeElapsed(startTime);
            string dif;
            switch (difficulty)
            {
                case 1:
                    dif = "Easy";
                    break;
                case 2:
                    dif = "Medium";
                    break;
                case 3:
                    dif = "Hard";
                    break;
                default:
                    dif = "Invalid";
                    break;
            }
            Console.WriteLine($"You made it to {currentStreak}. Time: {elapsed}!\n");
            History.SetHistory($"{date}, {name}, " +
                               $"Mode: {mode}, {currentStreak}, " +
                               $"Difficulty {dif}, " +
                               $"Time: {elapsed}");
        }
        static bool IsValidInput(string input)
        {
            string validCharacters = "ASMDRHQ";
            return validCharacters.Contains(input);
        }
    }
}