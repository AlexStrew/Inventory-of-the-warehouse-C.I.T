using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace UnitTestProject1 {
    [TestClass]
    public class UnitTest1 {
        //Unit Test
        [TestMethod]
        public void GetInventory_ReturnsInventory_WhenInvNumExists() {
            //Arrange
            var mockContext = new Mock<InventoryContext>();
            var inventory = new Inventory { InvNum = "12345" };
            mockContext.Setup(x => x.Inventories.SingleOrDefaultAsync(It.IsAny<Expression<Func<Inventory, bool>>>())).ReturnsAsync(inventory);
            var controller = new InventoryController(mockContext.Object);

            //Act
            var result = controller.GetInventory("12345").Result;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(inventory, result);
        }

        [TestMethod]
        public void GetInventory_ReturnsNotFound_WhenInvNumDoesNotExist() {
            //Arrange
            var mockContext = new Mock<InventoryContext>();
            mockContext.Setup(x => x.Inventories.SingleOrDefaultAsync(It.IsAny<Expression<Func<Inventory, bool>>>())).ReturnsAsync(null);
            var controller = new InventoryController(mockContext.Object);

            //Act
            var result = controller.GetInventory("12345").Result;

            //Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }


    }
}
