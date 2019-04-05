using System;
using System.Collections.Generic;
using JohnVerbiest.Remarkable.Api.ServiceClient.Dto;

namespace JohnVerbiest.Remarkable.Api.ServiceClient
{
    internal interface IServiceClient
    {
        IEnumerable<ListingItem> GetItems(bool withDownloadUrls = false, Guid? singleItem = null);
    }
}