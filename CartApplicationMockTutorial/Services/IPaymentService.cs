﻿using CartApplicationMockTutorial.Services;

namespace CartApplicationMockTutorial
{
    public interface IPaymentService
    {         
            bool Charge(double total, ICard card);
        
    }
}