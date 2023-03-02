namespace IoneVectronConverter.Ione.Orders.Models
{
    public class ItemLinkLayer
    {
        public int Id { get; set; }
         public string Name { get; set; }
       public int ItemID { get; set; }
        public string APIObjectId { get; set; }
        public int? BranchAddressId { get; set; }
        public ItemLinkLayer[] ItemLinkLayerList { get; set; }
        public bool Nullprice { get; set; }
        //public int? ItemSequence { get; set; }
        public int? SelectionCounter { get; set; }
        public bool SelectionConstraint { get; set; }
        //public bool SelectionItem { get; set; }
        //public bool SelectionCalculation { get; set; }
        //public bool DefaultSelection { get; set; }
        //public int? MaxQuantity { get; set; }
        //public int[] AllowedCombination { get; set; }

    }
}
