using IoneVectronConverter.Ione.Models;
using Order2VPos.Core.IoneApi.Items;

namespace IoneVectronConverter.Ione.Categories;

public interface IIoneCategoryManager
{
    void SynchronizeArticlesFromDatabaseToIoneClient();
}