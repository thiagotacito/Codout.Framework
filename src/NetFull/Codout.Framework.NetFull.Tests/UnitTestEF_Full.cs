﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Codout.Framework.NetFull.Tests
{
    [TestClass]
    public class UnitTestEF_Full
    {
        [TestMethod]
        public void TestaInclusaoELeituraDBSQLEF_Full()
        {
            //**** CHECAR A STRING DE CONEXÃO NO ARQUIVO UnitOfWorkTest.cs
            UnitOfWorkTest unitOfWorkTest = new UnitOfWorkTest();

            Guid? guid = Guid.Parse("97A7513F-7D57-4171-96B5-AE02E2A9C6CE");

            Customer cliente = unitOfWorkTest.Customers.Get(guid);
            if (cliente == null)
            {
                cliente = new Customer { Nome = "José da Silva" };
                cliente.SetId((guid));
                unitOfWorkTest.Customers.Save(cliente);
            }
            else
            {
                cliente.Nome = $"José da Silva + {DateTime.Now.ToShortDateString()} + {DateTime.Now.ToLongTimeString()}";
                unitOfWorkTest.Customers.Update(cliente);
            }
            unitOfWorkTest.SaveChanges();

            var obj = unitOfWorkTest.Customers.Get(guid);

            Assert.AreEqual(guid, obj.Id);
        }
    }
}
