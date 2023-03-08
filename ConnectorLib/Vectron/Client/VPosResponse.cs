namespace ConnectorLib.Vectron.Client
{
    public class VPosResponse
    {
        public bool IsError { get; set; }
        public bool IsCanceled { get; set; }
        public int VPosErrorNumber { get; set; }
        public string Message { get; set; }
        public int ReceiptMainNo { get; set; }
        public decimal SubTotal { get; set; }
        public string UUId { get; set; }
    }
}
