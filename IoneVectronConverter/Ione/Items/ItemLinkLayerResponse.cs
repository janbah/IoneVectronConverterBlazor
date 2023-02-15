using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IoneVectronConverter.Ione;

namespace Order2VPos.Core.IoneApi.Items
{
    public class ItemLinkLayerResponse : ApiResponse
    {
        public ItemLinkLayer[] Data { get; set; }
    }
}
