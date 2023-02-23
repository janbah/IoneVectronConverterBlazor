using IoneVectronConverter.Ione.Services;
using IoneVectronConverter.Vectron.Client;

namespace IoneVectronConverter.Vectron.MasterData;

public class MasterdataReceiver
{
    private readonly IMasterdataService _masterdataService;
    private readonly IVectronClient _vectronClient;
    
    public MasterdataReceiver(MasterdataService masterdataService, IVectronClient vectronClient)
    {
        _masterdataService = masterdataService;
        _vectronClient = vectronClient;
    }

    public void ReceiveAndStoreMasterdata()
    {
        
    }
}