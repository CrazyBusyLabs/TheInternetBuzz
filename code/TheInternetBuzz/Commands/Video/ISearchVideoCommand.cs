using System;

using TheInternetBuzz.Data.Video;

namespace TheInternetBuzz.Commands.Video
{
    public interface ISearchVideoCommand
    {
        VideoList GetVideo();
    }
}