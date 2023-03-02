namespace IoneVectronConverter.Common.Config;

public class VectronSettings
{

    #region VectronPos

    public int OperatorCode { get; set; }
    public int AttributeNoForWebShop { get; set; }
    public int Operator { get; set; }
    public int ReceiptMediaNo { get; set; }
    public int TipDiscountNumber { get; set; }
    public string VPosIPAddress { get; set; }
    public List<GcRange> GcRanges { get; set; }
    public int VPosIpPort { get; set; }

    #endregion

    #region IoneApi
        
    public string ApiBaseAddress { get; set; }
    public int BranchAddressId { get; set; }
    public string IoneApiIdentifier { get; set; }
    public string IoneApiToken { get; set; }
    public List<PriceListAssignment> PriceListAssignmentList { get; set; }

    #endregion

    #region Allgemein

    public bool TimerActive { get; set; }

    #endregion

    #region Webservice

    public string WebServiceUrlPrefix { get; set; }

    #endregion

}