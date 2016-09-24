using SavingVariables.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavingVariables.DAL
{
    public class SVRepository
    {
        public SVContext Context { get; set; }

        public SVRepository()
        {
            Context = new SVContext();
        }
        
        public SVRepository(SVContext _context)
        {
            Context = _context;
        }
        
        public List<Variable> GetVariables()
        {
            int i = 1;
            return Context.Variables.ToList();
        } 

        public void AddVariable(Variable variable)
        {
            Context.Variables.Add(variable);
            Context.SaveChanges();
        }

        public void AddVariable(string varLetter, int varValue)
        {
            Variable variable = new Variable { VarLetter = varLetter, VarValue = varValue };
            Context.Variables.Add(variable);
            Context.SaveChanges();
        }

        public Variable FindVariable(string varLetter)
        {
            Variable targetVar = Context.Variables.FirstOrDefault(a => a.VarLetter.ToLower() == varLetter.ToLower());
            return targetVar;
        }

        public Variable DeleteVariable(string varLetter)
        {
            Variable targetVar = FindVariable(varLetter);
            if (targetVar != null)
            {
                Context.Variables.Remove(targetVar);
                Context.SaveChanges();
            }
            return targetVar;
        }

        public void DeleteAllVariables()
        {
            List<Variable> whole_var_db = GetVariables();
            foreach (var variable in whole_var_db)
            {
                Context.Variables.Remove(variable);
            }
            Context.SaveChanges();
        }
    }
}
