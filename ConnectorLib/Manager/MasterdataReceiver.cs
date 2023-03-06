using ConnectorLib.Client;
using ConnectorLib.Masterdata.Services;

namespace ConnectorLib.Manager;

public class MasterdataReceiver : IMasterdataReceiver
{
    private readonly IPluService _pluService;
    private readonly ITaxService _taxService;
    private readonly ISelWinService _selWinService;
    private readonly IDepartmentService _departmentService;
    
    private readonly IVectronClient _vectronClient;
    
    public MasterdataReceiver(IVectronClient vectronClient, IPluService pluService, ITaxService taxService, ISelWinService selWinService, IDepartmentService departmentService)
    {
        _vectronClient = vectronClient;
        _pluService = pluService;
        _taxService = taxService;
        _selWinService = selWinService;
        _departmentService = departmentService;
    }

    public void ReceiveAndStoreMasterdata()
    {
        var masterdataResponse = _vectronClient.GetMasterData();
        
        _pluService.StorePlus(masterdataResponse.PLUs);
        _taxService.StoreTaxesIfNew(masterdataResponse.Taxes);
        _selWinService.StoreSelWinsIfNew(masterdataResponse.SelWins);
    }
}