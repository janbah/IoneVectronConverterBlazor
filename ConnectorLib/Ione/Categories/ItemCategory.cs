namespace ConnectorLib.Ione.Categories
{
    public class ItemCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string APIObjectId { get; set; }
        public int[] BranchAddressIdList { get; set; }

        public int LevelId { get; set; } = 2;
        public int StatusId { get; set; } = 1;

        public int ItemCategoryWebshopLink { get; set; } = 1;

        public int? ParentId { get; set; }
        
        public bool IsMain { get; set; }
        public bool IsSent { get; set; }
    }
}
