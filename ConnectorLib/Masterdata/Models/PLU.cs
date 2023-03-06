

using Dapper.Contrib.Extensions;
using IoneVectronConverter.Vectron.Models;

namespace ConnectorLib.Masterdata.Models
{
    [Table("plu")]
    public class PLU
    {
        [Key]
        public int Id { get; set; }
        public int PLUno { get; set; }
        public string Name1 { get; set; }
        public string Name2 { get; set; }
        public string Name3 { get; set; }
        public bool SaleAllowed { get; set; }
        public int TaxNo { get; set; }
        
        [Write(false)]
        public PriceData[] Prices { get; set; }

        [Write(false)]
        public int[] SelectWin { get; set; }
        public int DepartmentNo { get; set; }
        public string Attributes { get; set; }
        public int MainGroupA { get; set; }
        public int MainGroupB { get; set; }

        public bool IsForWebShop { get; set; }
        //public bool IsForWebShop => Attributes?.Length >= AppSettings.Default.AttributeNoForWebShop && Attributes.Substring(AppSettings.Default.AttributeNoForWebShop - 1,1) == "1";
    }
}
