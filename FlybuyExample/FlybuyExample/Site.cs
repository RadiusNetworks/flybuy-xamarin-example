using System;

namespace FlybuyExample
{
    public class Site
    {
        public Site(int id, string number, string name, string desc)
        {
            Id = id;
            Number = number;
            Name = name;
            Desc = desc;
        }

        public override string ToString() => Name;

        public int Id { get; set; }
        public string Number { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
    }
}
