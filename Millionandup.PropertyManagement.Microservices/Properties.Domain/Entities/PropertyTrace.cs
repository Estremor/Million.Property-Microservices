namespace Properties.Domain.Entities;
public partial class PropertyTrace
{
    public Guid IdPropertyTrace { get; set; }
    public string DataSale { get; set; }
    public string Name { get; set; }
    public decimal? Value { get; set; }
    public decimal? Tax { get; set; }
    public Guid IdProperty { get; set; }

    public virtual Property IdPropertyNavigation { get; set; }
}
