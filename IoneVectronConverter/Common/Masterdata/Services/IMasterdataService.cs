using IoneVectronConverter.Vectron.MasterData.Models;

namespace IoneVectronConverter.Common.Masterdata.Services;

public interface IMasterdataService
{
    MasterDataResponse GetMasterdataResponse();

    void PersistMasterdataResponse();
}