using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

using TheInternetBuzz.Util;
using TheInternetBuzz.Data.Video;

namespace TheInternetBuzz.Services.Video
{
    static public class VideoListSerializer
    {
        private static Object guard = new Object();

        static public void Serialize(VideoList videoList)
        {
            string filepath = System.Web.Hosting.HostingEnvironment.MapPath("~/data/VideoTrends.xml");

            Type type = typeof(VideoList);
            Type[] extraTypes = new Type[] { typeof(VideoItem) };

            lock (guard)
            {
                XMLSerializer.Serialize(videoList, type, extraTypes, filepath);
            }
        }

        static public VideoList Deserialize()
        {
            string filepath = System.Web.Hosting.HostingEnvironment.MapPath("~/data/VideoTrends.xml");

            VideoList videoList = null;

            Type type = typeof(VideoList);
            Type[] extraTypes = new Type[] { typeof(VideoItem)};

            lock (guard)
            {
                videoList = (VideoList)XMLSerializer.Deserialize(filepath, type, extraTypes);
            }

            return videoList;
        }
    }
}