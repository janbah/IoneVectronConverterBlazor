using System.ComponentModel;
using Newtonsoft.Json;

namespace IoneVectronConverter.Common
{
    public class AppSettings
    
    {
        
        
        // AppSettings()
        // {
        //     //GcRanges = new List<GcRange>();
        //     //PriceListAssignmentList = new List<PriceListAssignment>();
        // }
        
        

        #region VectronPos

        public int OperatorCode { get; set; } = 42;
        public int AttributeNoForWebShop { get; set; }
        public int Operator { get; set; }
        public int ReceiptMediaNo { get; set; }
        public int TipDiscountNumber { get; set; }
        public string VPosIPAddress { get; set; }
        public List<GcRange> GcRanges { get; set; }
        public int VPosIpPort { get; set; } = 1050;

        #endregion

        #region IoneApi
        
        public string ApiBaseAddress { get; set; } = "https://qa-gatewayapi.ione.de.com/api/RESTAPI";
        public int BranchAddressId { get; set; }
        public string IoneApiIdentifier { get; set; }
        public string IoneApiToken { get; set; }
        public List<PriceListAssignment> PriceListAssignmentList { get; set; }

        #endregion

        #region Allgemein

        public bool TimerActive { get; set; }

        #endregion

        #region Webservice
        
        public string WebServiceUrlPrefix { get; set; } = "http://*:1080/";

        #endregion




        public NameNr NameNr { get; set; }


        public void Save()
        {
            Console.Write(JsonConvert.SerializeObject(this));
        }
    }
}
