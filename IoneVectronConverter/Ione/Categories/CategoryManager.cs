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

        var mainCategoryIoneRefId = getMainCategoryId();

        // Kategorien verarbeiten

        // await processCategories(categoryListResponse, mainCategoryIoneRefId, dbContext);

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

    private async Task<int> getMainCategoryId()
    {
        int mainCategoryIoneRefId = await _iIoneClient.GetMainCategoryId();
        return mainCategoryIoneRefId;
    }


}