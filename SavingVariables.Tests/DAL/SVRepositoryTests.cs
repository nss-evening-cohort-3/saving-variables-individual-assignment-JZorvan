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
    }
}
