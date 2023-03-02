using Dapper.Contrib.Extensions;

namespace IoneVectronConverter.Common.Models
{
    [System.ComponentModel.DataAnnotations.Schema.Table("sel_win")]
    public class SelWin
    {
        [Key] 
        public int Id { get; set; }
        public int SelectCompulsion { get; set; }
        
        [Write(false)]
        public string[] PLUs { get; set; }

        [Write(false)]
        public int[] PLUNos => PLUs.Where(x => !x.StartsWith("*")).Select(x => Convert.ToInt32(x)).ToArray();
    
        [Write(false)]
        public int SelectCountIone => SelectCount == 0 ? 1 : SelectCount;
        public int SelectCount { get; set; }
        public string Name { get; set; }
        public int Number { get; set; }
        public bool ZeroPriceAllowed { get; set; }
    }
}
