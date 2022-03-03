using System.Collections.Generic;

namespace FlybuyExample
{
    public interface IFlybuyService
    {
        Customer CurrentCustomer();

        void CreateCustomer(Customer customer);

        void UpdateCustomer(Customer customer);

        void ClaimOrder(Order order, Customer customer);

        void CreateOrder(Order order, Customer customer);

        void UpdateOrder(Order order, string customerState);

        void FetchOrders();

        IList<Order> GetOrders();

        IList<Site> GetSites();
    }
}
