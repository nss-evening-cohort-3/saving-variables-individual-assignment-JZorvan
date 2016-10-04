using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavingVariables
{
    public class Lasts
    {
        public string LastCommand { get; set; }
        public string NoPreviousCommand = "You have not yet entered a command.";
        public string LastResult { get; set; }
    }
}
