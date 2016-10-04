using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavingVariables
{
    public class Program
    {
        static void Main(string[] args)
        {
            // Program Variables
            string Prompt = "[{0}]> ";
            int Counter = 0;
            bool GoForth = true;
            string bye = "See you next time!";
            string deletedVars = "You have deleted all of your saved variables.";

            // Instantiate Classes
            Evaluate evaluate = new Evaluate();
            Exits exit = new Exits();
            Lasts last = new Lasts();
            VariableHandler varHandler = new VariableHandler();

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

                    case "clear all":

                        varHandler.ClearAllVariables();
                        Console.WriteLine(deletedVars);
                        break;

                    case "delete all":

                        varHandler.ClearAllVariables();
                        Console.WriteLine(deletedVars);
                        break;

                    case "remove all":

                        varHandler.ClearAllVariables();
                        Console.WriteLine(deletedVars);
                        break;

                    case "lastq":

                        if (last.LastCommand != null)
                        {
                            Console.WriteLine(last.LastCommand);
                        }
                        else
                        {
                            Console.WriteLine(last.NoPreviousCommand);
                        }
                        last.LastCommand = UserInput;
                        break;

                    default:

                        evaluate.CheckUserInput(UserInput);
                        last.LastCommand = UserInput;
                        Console.WriteLine(evaluate.Output);
                        break;
                }

                Counter++;  // Increases prompt counter
            }

            Console.Read();
        }
    }
}
