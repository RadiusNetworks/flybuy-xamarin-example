using System;

namespace FlybuyExample
{
    public interface IFlybuyService
    {
        Customer CurrentCustomer();

        void CreateCustomer(Customer customer);

        void UpdateCustomer(Customer customer);

        Order[] GetOrders();

        Order ClaimOrder(string redemptionCode, string pickupType);

        Order CreateOrder(string siteNumber, string orderNumber, string pickupType, DateTime pickupStart, DateTime pickupEnd);

        void UpdateOrder(string orderNumber, string customerState);
    }
}
