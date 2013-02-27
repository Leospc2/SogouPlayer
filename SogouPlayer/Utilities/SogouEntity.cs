using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SogouPlayer.Utilities
{
    /// <summary>
    /// 排行榜
    /// </summary>
    class RankingListEntity
    {
        public string Id { get; set; }
        public List<RankItemEntity> List { get; set; }
    }

    /// <summary>
    /// 排行榜子项
    /// </summary>
    public class RankItemEntity
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }
    }

    /// <summary>
    /// 音乐列表
    /// </summary>
    public class MusicListEntity
    {
        public string Id { get; set; }
        public List<MusicListItemEntity> List { get; set; }
    }

    /// <summary>
    /// 音乐列表子项
    /// </summary>
    public class MusicListItemEntity
    {
        public string Title { get; set; }
        public string Singer { get; set; }

        /// <summary>
        /// 未编码的Title和Singer，中间为空格分隔
        /// </summary>
        public string TitleAndSinger
        {
            get { return Title + " " + Singer; }
        }

        /// <summary>
        /// 编码过的Title和Singer
        /// </summary>
        public string EncodedTitleAndSinger
        {
            get { return System.Web.HttpUtility.UrlEncode(Title + " " + Singer); }
        }
    }

    /// <summary>
    /// 音乐实体属性
    /// </summary>
    public class MusicItemEntity
    {
        public string Id { get; set; }
        public List<MusicItemInfoEntity> List { get; set; }
    }

    /// <summary>
    /// 音乐实体子项属性
    /// </summary>
    public class MusicItemInfoEntity
    {
        public string Tid { get; set; }
        public string Speed { get; set; }
        public string Consistency { get; set; }
        public string M { get; set; }
        public string Url { get; set; }
        public long Size { get; set; }
        public string Gid { get; set; }
        public string Cid { get; set; }
    }

}
