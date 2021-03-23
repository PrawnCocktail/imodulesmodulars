using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace imodulesmodulars
{
    class Program
    {
        static void Main(string[] args)
        {
            using (WebClient client = new WebClient())
            {
                client.Headers["User-Agent"] = "Mozilla/5.0 (Windows NT 6.1; Win64; x64; rv:86.0) Gecko/20100101 Firefox/86.0";
                var channelInfoJson = client.DownloadString("https://api.gotostage.com/channels/imodulesmodulars");
                var channelInfo = JsonConvert.DeserializeObject<ChannelInfo>(channelInfoJson);

                var contentKeysJson = client.DownloadString("https://api.gotostage.com/channels/" + channelInfo.channelKey + "/groups?keys=" + channelInfo.defaultGroupKey);
                var contentKeyInfo = JsonConvert.DeserializeObject<List<ContentList>> (contentKeysJson);

                foreach (var video in contentKeyInfo[0].contentKeys)
                {
                    try
                    {
                        var videoInfo = client.DownloadString("https://api.gotostage.com/v2/contents/" + video + "/assets");
                        var videoResources = JsonConvert.DeserializeObject<VideoInfo>(videoInfo);

                        var videotitlejson = client.DownloadString("https://api.gotostage.com/contents?ids=" + video);
                        var videoTitle = JsonConvert.DeserializeObject<List<VideoTitle>>(videotitlejson);

                        foreach (var resource in videoResources.resources)
                        {
                            if (resource.resourceType == "recording")
                            {
                                foreach (var type in resource.attributes)
                                {
                                    if (type.name == "cdnlocation")
                                    {
                                        if (!File.Exists(videoTitle[0].title + ".mp4"))
                                        {
                                            Console.WriteLine("Downloading Video: " + videoTitle[0].title);
                                            client.DownloadFile(type.value, videoTitle[0].title + ".mp4");
                                        }
                                        else
                                        {
                                            Console.WriteLine(videoTitle[0].title + " video already exists, Skipping.");
                                        }
                                    }
                                }
                            }
                            if (resource.resourceType == "parsed_transcript")
                            {
                                foreach (var type in resource.attributes)
                                {
                                    if (type.name == "cdnlocation")
                                    {
                                        if (!File.Exists(videoTitle[0].title + "_transcript.json"))
                                        {
                                            Console.WriteLine("Downloading Transcript: " + videoTitle[0].title);
                                            client.DownloadFile(type.value, videoTitle[0].title + "_transcript.json");
                                        }
                                        else
                                        {
                                            Console.WriteLine(videoTitle[0].title + " transcript already exists, Skipping.");
                                        }
                                    }
                                }
                            }
                        }
                    }
                    catch (WebException ex)
                    {

                    }
                }

                Console.WriteLine("Finsihed Downloading! Have a nice day!");
                Console.ReadLine();
            }
        }
    }
}
