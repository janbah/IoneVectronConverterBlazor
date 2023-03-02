using System.Net;
using System.Net.Http.Json;
using IoneVectronConverter.Ione;
using IoneVectronConverter.Ione.Categories;
using IoneVectronConverter.Ione.Orders.Models;
using Moq;
using Newtonsoft.Json;

namespace IoneVectronConverterUnitTests.Mocks;

public class IoneClientMock : Mock<IIoneClient>
{
    public List<ItemCategory> SentCategories { get; set; }
    public List<Item> SentItems { get; set; }
    public List<List<ItemLinkLayer>> ItemLinkLayers { get; set; }
    
    public IoneClientMock MockGetItems()
    {
        Setup(x => x.GetItemsAsync()).Returns(getItemListResponse());
        return this;
    }
    
    public IoneClientMock MockSaveItemPostAsync()
    {
        SentItems = new();
        Setup(c => c.PostAsync(new Uri("SaveItem",UriKind.Relative), It.IsAny<StringContent>())).Returns(getSaveItemResponse()).Callback<Uri, StringContent>(
            (uri, content) =>
            {
                Item item = null;
                try
                {
                    item = content.ReadFromJsonAsync<Item>().Result;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

                if (item is not null)
                {
                    SentItems.Add(item);
                }
            });
        return this;
    }
    
    public IoneClientMock MockSaveItemLinkLayerPostAsync()
    {
        ItemLinkLayers = new();
        Setup(c => c.PostAsync(new Uri("SaveItemLinkLayer",UriKind.Relative), It.IsAny<StringContent>())).Returns(getSaveItemResponse()).Callback<Uri, StringContent>(
            (uri, content) =>
            {
                List<ItemLinkLayer> itemLinkLayers = null;
                try
                {
                  itemLinkLayers = content.ReadFromJsonAsync<List<ItemLinkLayer>>().Result;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

                if (itemLinkLayers is not null)
                {
                    ItemLinkLayers.Add(itemLinkLayers);
                }
            });
        return this;
    }

    
    public IoneClientMock MockSaveCategoryPostAsync()
    {
        SentCategories = new();
        Setup(c=>c.SaveCategoryAsync(It.IsAny<ItemCategory>())).ReturnsAsync(1).Callback<ItemCategory>(cat => 
            {
                if (cat is not null)
                {
                    SentCategories.Add(cat);
                }
            });
        return this;
    }

   
    
    
    private ItemListResponse getItemListResponse()
    {
        Item item = new()
        {
            Id = 42,
            APIObjectId = "2",
            TaxPercentage = "19",
            ItemCategoryId = 1,
            Name = "test item",
            ItemPriceList = new List<ItemPrice>( )
            {
                new()
                {
                    PriceListId = 1,
                    TaxPercentage = "19",
                    PriceListType = 1,
                    BasePriceWithTax = "10",
                    PriceListTypeText = "common"
                }
            },
            BasePriceWithTax = "20",
            ItemWebshopLink = true,
            BranchAddressIdList = new []{1}
        };
        
        ItemListResponse response = new()
        {
            Data = new []{item}
        };
        return response;
    }
    
    private async Task<HttpResponseMessage> getSaveItemResponse()
    {
        ItemResponse itemResponse = new()
        {
            Data = new Item()
            {
                Id = 37,
                ItemCategoryId = 1,
            }
        };
        
        HttpResponseMessage response = new HttpResponseMessage();
        response.StatusCode = HttpStatusCode.OK;
        response.Content = new StringContent(JsonConvert.SerializeObject(itemResponse));

        return response;
    }
    
    private async Task<HttpResponseMessage> getSaveCatgoryResponse()
    {
        ItemCategoryResponse itemResponse = new()
        {
            Data = new ItemCategory()
            {
                Id = 37,
                Name = "test"
            }
        };
        
        HttpResponseMessage response = new HttpResponseMessage();
        response.StatusCode = HttpStatusCode.OK;
        response.Content = new StringContent(JsonConvert.SerializeObject(itemResponse));

        return response;
    }

}