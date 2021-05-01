using CartApplicationMockTutorial.Services;
using System.Collections.Generic;

namespace CartApplicationMockTutorial
{
    public interface IShipmentService
    {
        void Ship(IAddressInfo info, IEnumerable<CartItem> items);
    }
}