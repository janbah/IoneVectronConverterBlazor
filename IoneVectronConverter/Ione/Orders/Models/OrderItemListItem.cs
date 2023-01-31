namespace IoneVectronConverter.Ione.Orders.Models
{
    public class OrderItemListItem
    {
        public string APIObjectId { get; set; }
        public int Id { get; set; }
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public string ItemDescription { get; set; }
        public string ItemIoneId { get; set; }
        public string Quantity { get; set; }
        public int QuantityUnitId { get; set; }
        public string QuantityUnitText { get; set; }
        public string Price { get; set; }
        public string TaxPercentage { get; set; }
        public string Discount { get; set; }
        public string DiscountedQuantity { get; set; }
        public int DiscountUnit { get; set; }
        public string DiscountUnitText { get; set; }
        public string DiscountDescription { get; set; }
        public string Total { get; set; }
        public string SerialNo { get; set; }
        public int ParentId { get; set; }
        public string ItemObjectType { get; set; }
        public OrderItemListItem[] ItemList { get; set; }
        public bool SelectionCalculation { get; set; }

    }
}
