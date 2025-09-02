namespace Properties.Domain.Entities;
    public partial class Owner
    {
        public Owner()
        {
            Properties = new HashSet<Property>();
        }

        public Guid IdOwner { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Document { get; set; }
        public byte[] Photo { get; set; }
        public DateTime Birthday { get; set; }

        public virtual ICollection<Property> Properties { get; set; }
    }
