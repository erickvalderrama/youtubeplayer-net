using System;

namespace AFEXChile.Domain.DTO
{
    public class YouTubeVideoDetail
    {
        public int Id { get; set; }
        public string VideoId { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public string ChannelTitle { get; set; }
        public DateTime? PublicationDate { get; set; }
        public string Thumbnail { get; set; }
        public string Duration { get; set; }
    }
}