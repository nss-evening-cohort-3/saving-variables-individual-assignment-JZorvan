using SavingVariables.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavingVariables
{
    public class VariableHandler
    {
        SVRepository varRepo = new SVRepository();

        public string Output;

        public string CheckAndAdd(string VarChar, int VarValue)
        {
            if (varRepo.FindVariable(VarChar) == null)
            {
                varRepo.AddVariable(VarChar, VarValue);
                Output = String.Format("You have set '{0}' to equal {1}.", VarChar, VarValue.ToString());
                return Output;
            }
            else
            {
                Output = String.Format("'{0}' already has a value attached to it.", VarChar);
                return Output;
            }
        }

        public void ClearAllVariables()
        {
            varRepo.DeleteAllVariables();
        }
    }
}
