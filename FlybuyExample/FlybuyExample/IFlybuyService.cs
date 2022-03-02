using System;
using System.Collections.Generic;

namespace FlybuyExample
{
    public interface IFlybuyService
    {
        Customer CurrentCustomer();

        void CreateCustomer(Customer customer);

        void UpdateCustomer(Customer customer);

        Order[] GetOrders();

        Order ClaimOrder(string redemptionCode, string pickupType);

        void CreateOrder(Order order, Customer customer);

        void UpdateOrder(Order order, string customerState);

        IList<Site> GetSites();
    }
}
