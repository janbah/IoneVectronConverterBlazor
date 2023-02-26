using Dapper.Contrib.Extensions;

namespace IoneVectronConverter.Ione.Models
{
    [Table("category")]
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        
        public int VectronNo { get; set; }
        public int IoneRefId { get; set; }
        
        public bool IsSent { get; set; }


    }
}
