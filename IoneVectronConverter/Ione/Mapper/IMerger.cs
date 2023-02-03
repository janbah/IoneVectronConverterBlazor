using IoneVectronConverter.Ione.Models;
using IoneVectronConverter.Vectron;

namespace IoneVectronConverter.Ione.Mapper;

public interface IMerger
{
    Order Merge(Order orderData, VPosResponse response);
}