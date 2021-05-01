using CartApplicationMockTutorial.Services;
using System.Collections.Generic;

namespace CartApplicationMockTutorial
{
    public interface ICartService
    {

       
        double Total();
        IEnumerable<CartItem> Items();

        void AddItem(CartItem cartItem);

    }
}