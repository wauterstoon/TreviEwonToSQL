namespace Domain.Entities
{
    public class Tag
    {


        public Tag(string name)
        {
            this.name = name;
          
        }

        public Tag(string name, DateTime time, string value, string description, string tagType)
        {
            this.name = name;
            this.time = time;
            this.value = value;
            this.description = description;
            this.tagType = tagType;
        }
        public Tag(string name, DateTime time, string value)
        {
            this.name = name;
            this.time = time;
            this.value = value;
        }
    

        public string name { get; set; }
        public DateTime time { get; set; }
        public string value { get; set; }
        public string description { get; set; }
        public string tagType { get; set; }
    }
}
