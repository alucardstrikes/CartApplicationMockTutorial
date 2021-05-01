using CartApplicationMockTutorial.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartApplicationMockTutorial
{
    public class CartController
    {
        private readonly ICartService _cartService;
        private readonly IPaymentService _paymentService;
        private readonly IShipmentService _shipmentService;

        public CartController(
      ICartService cartService,
      IPaymentService paymentService,
      IShipmentService shipmentService
    )
        {
            _cartService = cartService;
            _paymentService = paymentService;
            _shipmentService = shipmentService;
        }

        public string CheckOut(ICard card, IAddressInfo addressInfo)
        {
            var result = _paymentService.Charge(_cartService.Total(), card);
            if (result)
            {
                _shipmentService.Ship(addressInfo, _cartService.Items());
                return "charged";
            }
            else
            {
                return "not charged";
            }
        }

        public void AddITemsToCart(List<CartItem> cartItems)
        {
            foreach (var item in cartItems)
            {
                _cartService.AddItem(item);
            }
        }
    }
}
