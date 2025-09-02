namespace Properties.Domain.Entities;

public partial class PropertyImage
{
    public Guid IdPropertyImage { get; set; }
    public Guid IdProperty { get; set; }
    public byte[] File { get; set; }
    public bool Enabled { get; set; }

    public virtual Property IdPropertyNavigation { get; set; }
}
