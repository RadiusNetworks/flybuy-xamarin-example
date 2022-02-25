using System;

namespace FlybuyExample
{
    public interface IFlybuyService
    {
        Customer CurrentCustomer();

        Customer CreateCustomer(string name, string carType, string carColor, string carLicense, string phone);

        Order[] GetOrders();

        Order ClaimOrder(string redemptionCode, string pickupType);

        Order CreateOrder(string siteNumber, string orderNumber, string pickupType, DateTime pickupStart, DateTime pickupEnd);

        void UpdateOrder(string orderNumber, string customerState);
    }
}
