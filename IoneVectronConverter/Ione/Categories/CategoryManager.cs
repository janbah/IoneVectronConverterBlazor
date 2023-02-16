using IoneVectronConverter.Ione.Models;
using Newtonsoft.Json;
using Order2VPos.Core.IoneApi.ItemCategories;

namespace IoneVectronConverter.Ione.Categories;

public class CategoryManager
{
    private readonly IIoneClient _iIoneClient;
    private readonly IConfiguration _iConfiguration;

    public CategoryManager(IIoneClient iIoneClient, IConfiguration iConfiguration)
    {
        _iIoneClient = iIoneClient;
        _iConfiguration = iConfiguration;
    }

    public async Task SendPluList()
    {
        // Hauptkategorie ermitteln bzw. übertragen

        var mainCategoryIoneRefId = await getMainCategoryId();

        // Kategorien verarbeiten

        
        await processCategories(mainCategoryIoneRefId);

        // Artikel für Webshop aus Kasse ermitteln

        // var vposMainPlusForWebShop = identifyArticlesForWebshop(vposMasterData, itemListResponse, itemLinkLayersListResponse, out var vposCondimentPlusForWebShop, out var vposPlusForWebShop, out var mappingArticles, out var orphandItems);

        // Artikel zum Webshop übertragen

        // await sendArticleToWebshop(allItems, vposPlusForWebShop, mappingArticles, vposMainPlusForWebShop, vposCondimentPlusForWebShop, dbContext, vposMasterData, itemListResponse);

        // Artikelauswahlen zum Webshop übertragen

        // await sendArticleSelectionToWebshop(vposMainPlusForWebShop, vposMasterData, mappingArticles);

        // Artikel im Webshop deaktivieren

        //await deactivateArticleInWebshop(itemListResponse, orphandItems);

        //Todo: Logging
        //new LogWriter().WriteEntry($"Artikelstammdaten wurden erfolgreich zum Webshop übertragen!", System.Diagnostics.EventLogEntryType.Information, 200);
    }

    private async Task processCategories(int mainCategoryIoneRefId)
    {
        var branchAdressId = _iConfiguration.GetSection("Vectron").GetValue<int>("BranchAddressId");
        
        var categories = _iIoneClient.GetCategoriesAsync().Result.Data;

        var currentCategories = categories.Where(c =>
            c.BranchAddressIdList.Contains(branchAdressId) && 
            c.ParentId == mainCategoryIoneRefId);

        foreach (var category in currentCategories)
        {
            insertOrUpdateCategoryInDb(category);
        }


        sendNewCategories(currentCategories);

    }

    private void sendNewCategories(IEnumerable<ItemCategory> currentCategories)
    {
        
        List<Category> categoriesFromDb = getCategoriesFromDb();

        foreach (var dbCategory  in categoriesFromDb)
        {

            ItemCategory itemCategory = currentCategories.FirstOrDefault(c => c.Id == dbCategory.IoneRefId);

            if (dbCategoryShouldBeSend(dbCategory, itemCategory))
            {
                
            }

        }
    }

    private bool dbCategoryShouldBeSend(Category dbCategory, ItemCategory itemCategory)
    {
        int mainCategoryId = getMainCategoryId().Result;
        
        if (dbCategory.IoneRefId == 0)
        {
            return true;
        }

        if (itemCategory == null)
        {
            return true;
        }

        if (dbCategory.Name != itemCategory.Name)
        {
            return true;
        }

        if (itemCategory.ParentId != mainCategoryId)
        {
            return true;
        }
        //
        // if (!(
        //         itemCategory != null && 
        //         dbCategory.Name == itemCategory.Name && 
        //         itemCategory.ParentId == mainCategoryId))
        // {
        //     return true;
        // }

        return false;
    }

    private List<Category> getCategoriesFromDb()
    {
        throw new NotImplementedException();
    }

    private void insertOrUpdateCategoryInDb(ItemCategory category)
    {
        throw new NotImplementedException();
    }

    private async Task<int> getMainCategoryId()
    {
        int mainCategoryIoneRefId = await _iIoneClient.GetMainCategoryId();
        return mainCategoryIoneRefId;
    }


}