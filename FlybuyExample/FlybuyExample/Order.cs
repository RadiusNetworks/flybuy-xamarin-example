using System;

namespace FlybuyExample
{
    public class Order
    {
        public Order()
        {
            Number = "";
            PickupType = "curbside";
            PickupStart = new DateTime();
        }

        public Order(int id, Site site, string number, string pickupType)
        {
            Id = id;
            Site = site;
            Number = number;
            PickupType = pickupType;
        }

        public Order(int id, Site site, string number, string pickupType, DateTime pickupStart)
        {
            Id = id;
            Site = site;
            Number = number;
            PickupType = pickupType;
            PickupStart = pickupStart;
        }

        public int SiteId() => Site.Id;

        public int Id { get; set; }
        public Site Site { get; set; }
        public string Code { get; set; }
        public string Number { get; set; }
        public string PickupType { get; set; }
        public DateTime PickupStart { get; set; }
    }
}
