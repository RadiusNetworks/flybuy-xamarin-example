using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FlybuyExample
{
    public interface IFlybuyService
    {
        Customer CurrentCustomer();

        void CreateCustomer(Customer customer);

        void UpdateCustomer(Customer customer);

        void FetchOrder(string code);

        void ClaimOrder(Order order, Customer customer);

        void CreateOrder(Order order, Customer customer);

        void UpdateOrder(Order order, string customerState);

        void FetchOrders();

        ObservableCollection<Order> GetOrders();

        ObservableCollection<Site> GetSites();

        void OnMessageReceived(IDictionary<string, object> data);
    }
}
