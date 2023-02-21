using System.Net;
using IoneVectronConverter.Ione;
using Moq;
using Newtonsoft.Json;
using Order2VPos.Core.IoneApi.Items;

namespace IoneVectronConverterUnitTests.Mocks;

public class IoneClientMock : Mock<IIoneClient>
{
    public IoneClientMock MockGetItems()
    {
        Setup(x => x.GetItemsAsync()).Returns(getItemListResponse());
        return this;
    }
    
    public IoneClientMock MockPostAsync()
    {
        Setup(c => c.PostAsync(It.IsAny<Uri>(), It.IsAny<StringContent>())).Returns(getSaveResponse());
        return this;
    }
    
    
    private ItemListResponse getItemListResponse()
    {
        Item item = new()
        {
        };
        
        ItemListResponse response = new()
        {
            Data = new []{item}
        };
        return response;
    }
    
    private async Task<HttpResponseMessage> getSaveResponse()
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
}