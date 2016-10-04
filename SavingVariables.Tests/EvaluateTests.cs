using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.RegularExpressions;

namespace SavingVariables.Tests
{
    [TestClass]
    public class EvaluateTests
    {
        Evaluate TestEval = new Evaluate();
        VariableHandler TestVariableHandler = new VariableHandler();
        static string TestPattern = @"^(?<Variable>[a-z])\s?[=]\s?(?<VarValue>-?\d+)$";
        Regex TestRegex = new Regex(TestPattern);

        [TestMethod]
        public void CanInstantiateAnInstanceOfEvaluate()
        {
            Assert.IsNotNull(TestEval);
        }

        [TestMethod]
        public void CanCheckUserInputAgainstRegex()
        {
            Assert.IsTrue(TestRegex.IsMatch("a = -1"));
            Assert.IsTrue(TestRegex.IsMatch("b =2"));
            Assert.IsTrue(TestRegex.IsMatch("c= 394756"));
            Assert.IsTrue(TestRegex.IsMatch("d=-46"));
            Assert.IsFalse(TestRegex.IsMatch("This should fail."));
        }
    }
}
