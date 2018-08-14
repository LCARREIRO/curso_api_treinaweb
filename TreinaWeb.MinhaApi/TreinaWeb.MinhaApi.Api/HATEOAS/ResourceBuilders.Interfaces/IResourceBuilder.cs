using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TreinaWeb.MinhaApi.Api.HATEOAS.ResourceBuilders.Interfaces
{
    interface IResourceBuilder
    {
        void BuildResource(object resource, HttpRequestMessage request);
    }
}
