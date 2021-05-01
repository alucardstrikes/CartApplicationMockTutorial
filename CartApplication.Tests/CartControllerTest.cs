using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CartApplicationMockTutorial;
using CartApplicationMockTutorial.Services;
using Moq;
using NUnit.Framework;
namespace CartApplication.Tests
{
    [TestFixture]

    public class CartControllerTest
    {

        private CartController controller;
        private Mock<IPaymentService> paymentServiceMock;
        private Mock<ICartService> cartServiceMock;

        private Mock<IShipmentService> shipmentServiceMock;
        private Mock<ICard> cardMock;
        private Mock<IAddressInfo> addressInfoMock;
        private List<CartItem> items;

        [SetUp]
        public void Setup()
        {

            cartServiceMock = new Mock<ICartService>();
            paymentServiceMock = new Mock<IPaymentService>();
            shipmentServiceMock = new Mock<IShipmentService>();

            // arrange
            cardMock = new Mock<ICard>();
            addressInfoMock = new Mock<IAddressInfo>();

            // 
            var cartItemMock = new Mock<CartItem>();
            cartItemMock.Setup(item => item.Price).Returns(10);

            items = new List<CartItem>()
          {
              cartItemMock.Object
          };

            cartServiceMock.Setup(c => c.Items()).Returns(items.AsEnumerable());

            controller = new CartController(cartServiceMock.Object, paymentServiceMock.Object, shipmentServiceMock.Object);
        }
        [Test]
        public void CartController_WhenAmountDeductionIsSeuccessfullFromCard_CardIsCharged()
        {

            //Arrange
            cardMock.SetupAllProperties();
            cardMock.Object.Name = "Ashes";
            cardMock.Object.CardNumber = "123";
            cardMock.Object.ValidTo = DateTime.Now;

            paymentServiceMock.Setup(p => p.Charge(It.IsAny<double>(), cardMock.Object)).Returns(true);
            
            //Act
            var result = controller.CheckOut(cardMock.Object, null);

            //Assert
            Assert.AreEqual("charged", result);
        }

        [Test]
        public void CartController_WhenAmountDeductionIsUnSeuccessfullFromCard_CardIsNotCharged()
        {
            var paymentService_Mock = new Mock<IPaymentService>();
            var shipMentService_Mock = new Mock<IShipmentService>();
            var cartService_Mock = new Mock<ICartService>();
            var card_Mock = new Mock<ICard>();
            card_Mock.SetupAllProperties();
            card_Mock.Object.Name = "Ashes";
            card_Mock.Object.CardNumber = "123";
            card_Mock.Object.ValidTo = DateTime.Now;

            paymentService_Mock.Setup(p => p.Charge(It.IsAny<double>(), card_Mock.Object)).Returns(false);
            var CartControllerMock = new CartController(cartService_Mock.Object, paymentService_Mock.Object, shipMentService_Mock.Object);
            var result = CartControllerMock.CheckOut(card_Mock.Object, null);
            Assert.AreEqual("not charged", result);
        }

        [Test]
        public void CartController_ItemIsShipped_WhenCardIsChargedSuccessfully()
        {

        }
    }
}
