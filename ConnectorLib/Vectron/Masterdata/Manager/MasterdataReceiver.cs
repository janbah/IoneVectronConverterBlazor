using ConnectorLib.Vectron.Client;
using ConnectorLib.Vectron.Masterdata.Services;

namespace ConnectorLib.Vectron.Masterdata.Manager;

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
        _taxService.StoreTaxes(masterdataResponse.Taxes);
        _selWinService.StoreSelWins(masterdataResponse.SelWins);
    }
}