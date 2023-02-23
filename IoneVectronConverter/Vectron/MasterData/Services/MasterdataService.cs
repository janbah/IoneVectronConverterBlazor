using IoneVectronConverter.Vectron.MasterData;

namespace IoneVectronConverter.Ione.Services;

public class MasterdataService : IMasterdataService
{
    private readonly PluRepository _pluRepository;
    private readonly TaxRepository _taxRepository;
    private readonly DepartmentRepository _departmentRepository;
    private readonly SelWinRepository _selWinRepository;

    public MasterdataService(PluRepository pluRepository, TaxRepository taxRepository, DepartmentRepository departmentRepository, SelWinRepository selWinRepository)
    {
        _pluRepository = pluRepository;
        _taxRepository = taxRepository;
        _departmentRepository = departmentRepository;
        _selWinRepository = selWinRepository;
    }

    public MasterDataResponse GetMasterdataResponse()
    {
        throw new NotImplementedException();
    }

    public void PersistMasterdataResponse()
    {
        throw new NotImplementedException();
    }
}