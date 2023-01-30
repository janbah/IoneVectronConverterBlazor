namespace IoneVectronConverter.Ione.Orders.Models
{
    public class BranchAddress
    {
        public int Id { get; set; }
        public string HouseNumber { get; set; }
        public string FloorName { get; set; }
        public string PositionName { get; set; }
        public string HouseCode { get; set; }
        public string PostalCode { get; set; }
        public string StreetName { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int StateId { get; set; }
        public string Country { get; set; }
        public int CountryId { get; set; }
        public string FullAddress { get; set; }
    }
}
