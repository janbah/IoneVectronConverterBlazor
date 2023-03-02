using IoneVectronConverter.Common.Models;

namespace IoneVectronConverter.Vectron.MasterData.Models
{
    public class MasterDataResponse
    {
        public MasterDataResponse(Tax[] taxes, PLU[] plUs, SelWin[] selWins, Department[] departments)
        {
            Taxes = taxes;
            PLUs = plUs;
            SelWins = selWins;
            Departments = departments;
        }
        
        public MasterDataResponse(){}

        public Tax[] Taxes { get; set; }
        public PLU[] PLUs { get; set; }
        public SelWin[] SelWins { get; set; }
        public Department[] Departments { get; set; }
    }
}
