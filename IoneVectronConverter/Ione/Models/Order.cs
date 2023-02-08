using Dapper.Contrib.Extensions;

namespace IoneVectronConverter.Ione.Models
{
    [Table ("ione_order")]
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public int IoneRefId { get; set; }
        public string IoneId { get; set; }
        public OrderStatus Status { get; set; }
        public decimal OrderTotal { get; set; }
        public decimal ReceiptTotal { get; set; }
        public string ReceiptUUId { get; set; }
        public int ReceiptMainNo { get; set; }
        public string Message { get; set; }
        public int VPosErrorNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public bool IsCanceledOnPos { get; set; }

    }
}
