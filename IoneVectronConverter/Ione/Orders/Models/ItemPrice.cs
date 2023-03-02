namespace IoneVectronConverter.Ione.Orders.Models
{
    public class ItemPrice
    {
        public int PriceListId { get; set; }
        public int PriceListType { get; set; }
        public string PriceListTypeText { get; set; }
        public string BasePriceWithTax { get; set; }
        public string TaxPercentage { get; set; }
    }
}
