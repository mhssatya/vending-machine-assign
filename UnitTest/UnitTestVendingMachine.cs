using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Schema;
using VendingMachine_Application;
using VendingMachine_Application.Interfaces;
using VendingMachine_Domain.Entities;
using Xunit.Sdk;

namespace UnitTest
{
    [TestClass]
    public class UnitTestVendingMachine
    {
        private const decimal V = 1.25m;

        [TestMethod]
        [Description("Test that check ammout throws valid exception in case of mismatched values")]
        public void TestOrderItem()
        {
            VendingItem item = new VendingItem(1, "Coke", V, 5);
            var helperMock = new Mock<IHelper>();
            helperMock.Setup(x => x.PopulateItems()).Returns(new Dictionary<int, VendingItem>() { {1 , item } } );

            BlOrders order = new BlOrders();

            try { 
                order.CheckAmount(7, helperMock.Object.PopulateItems()[1], 4); Assert.Fail(); 
            }
            catch (Exception e) {
                Assert.AreEqual("Error ocured in ordering Amount not OK!", e.Message + " "+e.InnerException.Message); 
            }
             
        }

        [TestMethod]
        [Description("Test that check ordered values are changed after sucssesful order")]
        public void OrderTest()
        {
            VendingItem item = new VendingItem(1, "Coke", V, 5);
            var helperMock = new Mock<IHelper>();
            helperMock.Setup(x => x.PopulateItems()).Returns(new Dictionary<int, VendingItem>() { { 1, item } });

            BlOrders order = new BlOrders();
            Dictionary<int, VendingItem> dict = new Dictionary<int, VendingItem>(helperMock.Object.PopulateItems()); //{ { 1, helperMock.Object.PopulateItems()[1] } };
            order.Order(1, 2, ref dict);
            Assert.AreNotEqual(dict, helperMock.Object.PopulateItems());


        }

        [TestMethod]
        [Description("Test that check order command is formated OK")]
        public void CheckCommandTest()
        {
            var orderMoq = new Mock<IBlOrders>();
            orderMoq.Setup(m => m.CheckCommand("order 1 1 1", 1)).Returns(true);
            orderMoq.Verify();
            BlOrders order = new BlOrders();
            bool status = order.CheckCommand("order 1 1 1", 1);
            Assert.IsTrue(status);

        }
        [TestMethod]
        [Description("Test that check order command is not formated OK")]
        public void CheckCommandTest1()
        {
            BlOrders order = new BlOrders();
            bool status = order.CheckCommand("order 2 2 1", 1);
            Assert.IsFalse(status);
        }
    }
}
