using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using Newtonsoft;
using Newtonsoft.Json;

namespace SogouPlayer.Utilities
{
    class SogouUtility
    {
        /// <summary>
        /// 获取排行榜
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static RankingListEntity GetRankingList(string url)
        {
            string host = @"player.mbox.sogou.com";
            RankingListEntity rankingList = new RankingListEntity();

            if (string.IsNullOrEmpty(url))
                return rankingList;

            string responseText = GetWebJsonText(url, host);
            rankingList = JsonConvert.DeserializeObject<RankingListEntity>(responseText);
            return rankingList;
        }

        public static MusicListEntity GetMusicList(string url)
        {
            string host = @"music.sogou.com";
            MusicListEntity musicList = new MusicListEntity();
            string responseText = GetWebJsonText(url, host);
            musicList = JsonConvert.DeserializeObject<MusicListEntity>(responseText);

            return musicList;
        }

        public static MusicItemEntity GetMusicByName(string url, string name)
        {
            string host = @"mp3.sogou.com";
            url = url.Replace(" ", "+") + "&query=" + name;

            MusicItemEntity music = new MusicItemEntity();
            string responseText = GetWebJsonText(url, host);
            music = JsonConvert.DeserializeObject<MusicItemEntity>(responseText);
            return music;
        }

        public static MusicItemEntity GetMusic(string url)
        {
            string host = @"mp3.sogou.com";

            MusicItemEntity music = new MusicItemEntity();
            string responseText = GetWebJsonText(url, host);
            music = JsonConvert.DeserializeObject<MusicItemEntity>(responseText);
            return music;
        }


        #region Helper
        private static string GetWebJsonText(string url, string host)
        {
            HttpWebRequest request = HttpWebRequest.Create(url) as HttpWebRequest;
            request.Host = host;
            request.Referer = @"http://player.mbox.sogou.com/player";
            request.Accept = "*/*";

            Stream responseStream = request.GetResponse().GetResponseStream();
            string responseText = string.Empty;
            using (StreamReader sr = new StreamReader(responseStream, Encoding.Default))
            {
                responseText = sr.ReadToEnd();
            }
            responseText = responseText.Replace(@"\n", "");
            responseText = responseText.Substring(responseText.IndexOf("{"), responseText.LastIndexOf("}") - responseText.IndexOf("{") + 1);

            return responseText;
        }
        #endregion

    }
}
