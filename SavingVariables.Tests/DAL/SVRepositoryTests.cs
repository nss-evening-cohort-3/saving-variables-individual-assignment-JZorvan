using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SavingVariables.DAL;
using System.Collections.Generic;
using SavingVariables.Models;
using Moq;
using System.Data.Entity;
using System.Linq;

namespace SavingVariables.Tests.DAL
{
    [TestClass]
    public class SVRepositoryTests
    {
        Mock<SVContext> mock_context { get; set; }
        Mock<DbSet<Variable>> mock_variable_table { get; set; }
        List<Variable> variables_list { get; set; }
        SVRepository repo { get; set; }

        public void ConnectMockstoDatastore()
        {
            var queryable_list = variables_list.AsQueryable();

            mock_variable_table.As<IQueryable<Variable>>().Setup(m => m.Provider).Returns(queryable_list.Provider);
            mock_variable_table.As<IQueryable<Variable>>().Setup(m => m.Expression).Returns(queryable_list.Expression);
            mock_variable_table.As<IQueryable<Variable>>().Setup(m => m.ElementType).Returns(queryable_list.ElementType);
            mock_variable_table.As<IQueryable<Variable>>().Setup(m => m.GetEnumerator()).Returns(() => queryable_list.GetEnumerator());

            mock_context.Setup(c => c.Variables).Returns(mock_variable_table.Object);

            mock_variable_table.Setup(t => t.Add(It.IsAny<Variable>())).Callback((Variable a) => variables_list.Add(a));
            mock_variable_table.Setup(t => t.Remove(It.IsAny<Variable>())).Callback((Variable a) => variables_list.Remove(a));
        }

        [TestInitialize]
        public void Initialize()
        {
            mock_context = new Mock<SVContext>();
            mock_variable_table = new Mock<DbSet<Variable>>();
            variables_list = new List<Variable>();
            repo = new SVRepository(mock_context.Object);

            ConnectMockstoDatastore();
        }

        [TestCleanup]
        public void TearDown()
        {
            repo = null;
        }

        [TestMethod]
        public void CanCreateAnInstanceOfRepo()
        {
            SVRepository repo = new SVRepository();
            Assert.IsNotNull(repo);
        }

        [TestMethod]
        public void MakeSureRepoHasContext()
        {
            SVRepository repo = new SVRepository();

            SVContext actual_context = repo.Context;

            Assert.IsInstanceOfType(actual_context, typeof(SVContext));
        }

        [TestMethod]
        public void EnsureRepoStartsEmpty()
        {
            ConnectMockstoDatastore();
            SVRepository repo = new SVRepository(mock_context.Object);

            List<Variable> current_variables = repo.GetVariables();
            int expected_variable_count = 0;
            int actual_variable_count = current_variables.Count;

            Assert.AreEqual(expected_variable_count, actual_variable_count);
        }

        [TestMethod]
        public void CanAddVariableToDatabase()
        {
            SVRepository repo = new SVRepository(mock_context.Object);
            ConnectMockstoDatastore();
            Variable test_var = new Variable { VarLetter = "x", VarValue = 5 };

            repo.AddVariable(test_var);

            int expected_count = 1;
            int actual_count = repo.GetVariables().Count;

            Assert.AreEqual(expected_count, actual_count);
        }

        [TestMethod]
        public void CanAddVariablesToDatabaseByPassingArgs()
        {
            SVRepository repo = new SVRepository(mock_context.Object);
            ConnectMockstoDatastore();

            repo.AddVariable( "x" , 5 );

            int expected_count = 1;
            int actual_count = repo.GetVariables().Count;

            Assert.AreEqual(expected_count, actual_count);
        }

        [TestMethod]
        public void RepoEnsureFindAuthorByPenName()
        {
            variables_list.Add(new Variable { VarId = 0, VarLetter = "a", VarValue = 1 });
            variables_list.Add(new Variable { VarId = 1, VarLetter = "b", VarValue = 2 });
            variables_list.Add(new Variable { VarId = 2, VarLetter = "c", VarValue = 3 });

            SVRepository repo = new SVRepository(mock_context.Object);
            ConnectMockstoDatastore();

            string var_to_find = "b";
            Variable actual_variable = repo.FindVariable(var_to_find);

            int expected_id = 1;
            int actual_id = actual_variable.VarId;
            Assert.AreEqual(expected_id, actual_id);
        }

        [TestMethod]
        public void CanDeleteAVariable()
        {
            variables_list.Add(new Variable { VarId = 0, VarLetter = "a", VarValue = 1 });
            variables_list.Add(new Variable { VarId = 1, VarLetter = "b", VarValue = 2 });
            variables_list.Add(new Variable { VarId = 2, VarLetter = "c", VarValue = 3 });

            SVRepository repo = new SVRepository(mock_context.Object);
            ConnectMockstoDatastore();

            string var_to_delete = "b";
            Variable actual_variable = repo.DeleteVariable(var_to_delete);

            int expected_id_to_delete = 1;
            int actual_id_to_delete = actual_variable.VarId;

            int expected_var_count = 2;
            int actual_var_count = repo.GetVariables().Count;

            Assert.AreEqual(expected_id_to_delete, actual_id_to_delete);
            Assert.AreEqual(expected_var_count, actual_var_count);
        }

        [TestMethod]
        public void CannotRemoveSomethingThatIsNotThere()
        {
            variables_list.Add(new Variable { VarId = 0, VarLetter = "a", VarValue = 1 });
            variables_list.Add(new Variable { VarId = 1, VarLetter = "b", VarValue = 2 });
            variables_list.Add(new Variable { VarId = 2, VarLetter = "c", VarValue = 3 });

            SVRepository repo = new SVRepository(mock_context.Object);
            ConnectMockstoDatastore();

            string var_to_delete = "e";
            Variable actual_variable = repo.DeleteVariable(var_to_delete);

            Assert.IsNull(actual_variable);
        }

        [TestMethod]
        public void CanDeleteAllVariablesFromDatabase()
        {
            variables_list.Add(new Variable { VarId = 0, VarLetter = "a", VarValue = 1 });
            variables_list.Add(new Variable { VarId = 1, VarLetter = "b", VarValue = 2 });
            variables_list.Add(new Variable { VarId = 2, VarLetter = "c", VarValue = 3 });

            SVRepository repo = new SVRepository(mock_context.Object);
            ConnectMockstoDatastore();

            repo.DeleteAllVariables();

            int expected_var_count = 0;
            int actual_var_count = repo.GetVariables().Count;

            Assert.AreEqual(expected_var_count, actual_var_count);
        }
    }
}
