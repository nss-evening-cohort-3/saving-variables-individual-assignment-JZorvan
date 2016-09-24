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

        public void ConnectMocktoDatastore()
        {
            var queryable_list = variables_list.AsQueryable();

            mock_variable_table.As<IQueryable<Variable>>().Setup(m => m.Provider).Returns(queryable_list.Provider);
            mock_variable_table.As<IQueryable<Variable>>().Setup(m => m.Expression).Returns(queryable_list.Expression);
            mock_variable_table.As<IQueryable<Variable>>().Setup(m => m.ElementType).Returns(queryable_list.ElementType);
            mock_variable_table.As<IQueryable<Variable>>().Setup(m => m.GetEnumerator()).Returns(() => queryable_list.GetEnumerator);

            mock_variable_table.Setup(c => c.Authors).Returns(mock_variable_table.Object);


        }

        [TestMethod]
        public void TestMethod1()
        {
        }

        private class Mock<T>
        {
        }
    }
}
