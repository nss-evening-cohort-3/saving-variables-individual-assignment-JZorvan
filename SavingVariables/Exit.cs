using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SavingVariables
{
    public class Exits
    {
        public void ExitProgram()
        {
            Thread.Sleep(1000);  // One second delay
            Environment.Exit(0);  // Exits program
        }
    }
}
