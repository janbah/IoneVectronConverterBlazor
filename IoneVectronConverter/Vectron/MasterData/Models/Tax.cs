using System.ComponentModel.DataAnnotations.Schema;

namespace IoneVectronConverter.Vectron.MasterData
{
    [Dapper.Contrib.Extensions.Table("tax")]
    public class Tax
    {
        public int TaxNo { get; set; }
        public decimal Rate { get; set; }
        public string Name { get; set; }
    }
}
