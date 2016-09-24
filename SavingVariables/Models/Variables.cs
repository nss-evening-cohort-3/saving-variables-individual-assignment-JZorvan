using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavingVariables.Models
{
    public class Variable
    {
        [Key]
        public int VarId { get; set; }

        [Required]
        [MaxLength(length: 1, ErrorMessage = "Variables can only be a single letter.")]
        public string VarLetter { get; set; }

       [Required]
        public int VarValue { get; set; }
    }
}
