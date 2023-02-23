using IoneVectronConverter.Vectron.MasterData;

namespace IoneVectronConverter.Ione.Services;

public interface IMasterdataService
{
    MasterDataResponse GetMasterdataResponse();

    void PersistMasterdataResponse();
}