using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace imodulesmodulars
{
    public class ChannelInfo
    {
        public string channelKey { get; set; }
        public string channelTitle { get; set; }
        public List<object> groupKeys { get; set; }
        public string defaultGroupKey { get; set; }
        public bool isLimitedAudience { get; set; }
        public List<string> aliases { get; set; }
        public DateTime createTime { get; set; }
        public DateTime updateTime { get; set; }
        public string channelDescription { get; set; }
    }

    public class ContentList
    {
        public string groupKey { get; set; }
        public string groupName { get; set; }
        public string channelKey { get; set; }
        public List<string> contentKeys { get; set; }
        public DateTime createTime { get; set; }
        public DateTime updateTime { get; set; }
    }

    public class Attribute
    {
        public int id { get; set; }
        public string name { get; set; }
        public string value { get; set; }
    }

    public class Resource
    {
        public string resourceKey { get; set; }
        public string status { get; set; }
        public string statusId { get; set; }
        public string name { get; set; }
        public string resourceType { get; set; }
        public string resourceTypeId { get; set; }
        public string provider { get; set; }
        public int version { get; set; }
        public List<Attribute> attributes { get; set; }
    }

    public class VideoInfo
    {
        public string status { get; set; }
        public List<Resource> resources { get; set; }
    }

    public class VideoTitle
    {
        public string title { get; set; }
    }
}
