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

        public Order(Site site, string number, DateTime pickupStart, string pickupType)
        {
            Site = site;
            Number = number;
            PickupType = pickupType;
            PickupStart = pickupStart;
        }

        public int SiteId() => Site.Id;

        public int Id { get; set; }
        public Site Site { get; set; }
        public string Number { get; set; }
        public string PickupType { get; set; }
        public DateTime PickupStart { get; set; }
    }
}
