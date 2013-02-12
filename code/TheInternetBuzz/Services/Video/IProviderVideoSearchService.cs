using System;

using TheInternetBuzz.Data.Video;

namespace TheInternetBuzz.Services.Video
{
    public interface IProviderVideoSearchService
    {
        VideoList Search(string query);
    }
}
