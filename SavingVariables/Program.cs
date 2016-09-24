using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavingVariables
{
    class Program
    {
        static void Main(string[] args)
        {
            // Program Variables
            string Prompt = "[{0}]>";
            int Counter = 0;
            bool GoForth = true;
            string bye = "See you next time!";

            // Instantiate Classes
            Evaluate evaluate = new Evaluate();
            Exit exit = new Exit();

            // Print an introduction to the program
            Console.WriteLine("***  Welcome to Saving Variables!  ***\r\n_______________________________________\r\n");

            // Runs program loop
            while (GoForth == true)
            {
                Console.Write(Prompt, Counter);  // Prints prompt
                String UserInput = Console.ReadLine().ToLower();  // Collects input in lower case

                switch (UserInput)
                {
                    case "exit":

                        Console.WriteLine(bye);
                        exit.ExitProgram();
                        break;

                    case "quit":

                        Console.WriteLine(bye);
                        exit.ExitProgram();
                        break;

                    default:
                        break;
                }

                Counter++;  // Increases prompt counter
            }

            Console.Read();
        }
    }
}
