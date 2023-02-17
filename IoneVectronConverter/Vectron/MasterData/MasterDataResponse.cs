namespace IoneVectronConverter.Vectron.MasterData
{
    public class MasterDataResponse
    {
        public Tax[] Taxes { get; set; }
        public _PLU[] PLUs { get; set; }
        public SelWin[] SelWins { get; set; }
        public Department[] Departments { get; set; }
    }
}
