using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SavingVariables
{
    public class Evaluate
    {
        // Regex Pattern
        string SetterPattern = @"^(?<Variable>[a-z])\s?[=]\s?(?<VarValue>-?\d+)$"; // Regex pattern for setting a constant

        public string VarChar { get; set; }
        public int VarValue { get; set; }
        public string Output { get; set; }

        VariableHandler VarHandler = new VariableHandler();

        public void CheckUserInput(string UserInput)
        {
            Regex SetterRegex = new Regex(SetterPattern);
            Match SetterMatch = SetterRegex.Match(UserInput);

            if (true == SetterRegex.IsMatch(UserInput))
            {
                VarChar = SetterMatch.Groups["Variable"].Value;  // Parses input into two variables
                VarValue = Convert.ToInt32(SetterMatch.Groups["VarValue"].Value);
                Output = VarHandler.CheckAndAdd(VarChar, VarValue);
            }
            else
            {
                Output = "Please enter an accepted command.";
            }
        }
    }
}
