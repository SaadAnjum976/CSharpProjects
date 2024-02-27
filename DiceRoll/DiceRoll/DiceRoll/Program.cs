using System;

class Program
{
    static void Main(string[] args)
    {
        Random rand = new Random();

        while (true)
        {
            Console.WriteLine("Press enter to roll the dice, or type 'quit' to exit.");
            string input = Console.ReadLine();

            if (input == "quit")
            {
                break;
            }

            int roll1 = rand.Next(1, 7);
            int roll2 = rand.Next(1, 7);

            Console.WriteLine("You rolled a {0} and a {1}.", roll1, roll2);

            if (roll1 + roll2 == 7)
            {
                Console.WriteLine("You win!");
            }
            else
            {
                Console.WriteLine("You lose.");
            }
        }
    }
}