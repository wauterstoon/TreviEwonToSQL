namespace Domain.Entities
{
    public class Ewon
    {
        public Ewon(string name, string description)
        {
            this.name = name;
            this.description = description;
        }

        public string name { get; set; }
        public string description { get; set; }
    }
}
