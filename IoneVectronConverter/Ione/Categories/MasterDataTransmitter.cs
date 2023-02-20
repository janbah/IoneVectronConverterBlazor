using IoneVectronConverter.Ione.Datastoring;
using IoneVectronConverter.Ione.Models;
using IoneVectronConverter.Vectron.Client;
using Order2VPos.Core.IoneApi.ItemCategories;

namespace IoneVectronConverter.Ione.Categories;

public class MasterDataTransmitter
{
    private readonly IIoneClient _ioneClient;
    private readonly IRepository<Category> _repo;
    private readonly IVectronClient _vectronClient;
    private readonly IEnumerable<ItemCategory> _categoriesFromVectron;
    private readonly IConfiguration _configuration;
    private readonly int _mainCategoryId;
    private readonly IEnumerable<ItemCategory> _categoriesFromIone;

    public MasterDataTransmitter(IIoneClient ioneClient, IRepository<Category> repo, IVectronClient vectronClient, IConfiguration configuration, IEnumerable<ItemCategory> categoriesFromIone)
    {
        _ioneClient = ioneClient;
        _repo = repo;
        _vectronClient = vectronClient;
        _configuration = configuration;
        _categoriesFromIone = categoriesFromIone;
        _categoriesFromVectron = _vectronClient.GetCategories();
        _categoriesFromIone = ioneClient.GetCategoriesAsync().Result.Data;

        _mainCategoryId = getMainCategoryId();
    }

    private int getMainCategoryId()
    {
        return 1;
    }


    public void Transmit()
    {
        
    }
    
    
    
    
}