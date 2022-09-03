using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using AFEXChile.Components;
using AFEXChile.Domain.Context;
using AFEXChile.Domain.DTO;
using AFEXChile.Domain.Entity;
using AFEXChile.Infrastructure.Persistence;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;

namespace AFEXChile
{
    public partial class _Default : Page
    {
        private const string YoutubeLinkRegex = "(?:.+?)?(?:\\/v\\/|watch\\/|\\?v=|\\&v=|youtu\\.be\\/|\\/v=|^youtu\\.be\\/)([a-zA-Z0-9_-]{11})+";
        private static Regex regexExtractId = new Regex(YoutubeLinkRegex, RegexOptions.Compiled);
        private VideosRepositoryImpl videosRepository;
        protected void Page_Load(object sender, EventArgs e)
        {
            videosRepository = new VideosRepositoryImpl(new AppDbContext());
            if (!IsPostBack)
            {
                GetVideos();
                GenerateControls();
            }
            else
            {
                GenerateControls();
            }
        }

        private async Task<YouTubeVideoDetail> SearchVideo(string link)
        {
            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = Credentials.APIKey
            });
            var searchRequest = youtubeService.Videos.List("snippet,contentDetails");
            searchRequest.Id = link;
            var searchResponse = await searchRequest.ExecuteAsync();
            var youTubeVideo = searchResponse.Items.FirstOrDefault();
            YouTubeVideoDetail videoDetails = new YouTubeVideoDetail();
            if (youTubeVideo!=null)
            {
                string time = youTubeVideo.ContentDetails.Duration.Replace("PT", "");
                time = time.Replace("M", ":");
                time = time.Replace("S", "");

                videoDetails = new YouTubeVideoDetail()
                {
                    VideoId = youTubeVideo.Id,
                    Description = youTubeVideo.Snippet.Description,
                    Title = youTubeVideo.Snippet.Title,
                    ChannelTitle = youTubeVideo.Snippet.ChannelTitle,
                    PublicationDate = youTubeVideo.Snippet.PublishedAt,
                    Thumbnail = youTubeVideo.Snippet.Thumbnails.Medium.Url,
                    Duration = time
                };
            }
            return videoDetails;
        }

        protected async void btnSearch_Click(object sender, EventArgs e)
        {
            var id = ExtractVideoIdFromUrl(txtSearch.Text);
            if (id != null)
            {
                List<HtmlGenericControl> controls = new List<HtmlGenericControl>();
                if (Session["listControls"] != null)
                    controls = (List<HtmlGenericControl>)Session["listControls"];

                YouTubeVideoDetail videoDetails = await SearchVideo(id);

                Videos v = SaveVideo(videoDetails);
                videoDetails.Id = v.Id;
                HtmlGenericControl control = CreateVideoElement(videoDetails);
                controls.Add(control);
                Session["listControls"] = controls;
                panelVideos.Controls.Add(control);
                txtSearch.Text = "";
                GenerateControls();
            }
        }

        private HtmlGenericControl CreateVideoElement(YouTubeVideoDetail videoDetails) {
            HtmlGenericControl image = new HtmlGenericControl("div");
            HtmlGenericControl trash = new HtmlGenericControl("div");
            HtmlGenericControl duration = new HtmlGenericControl("div");

            image.ID = videoDetails.Id + "Image";
            image.Attributes["class"] = "video-item btn-show-video ml-4 mr-4 mt-4 mb-4";
            image.ClientIDMode = ClientIDMode.Static;
            image.Attributes["style"] = "background-image: url('" + videoDetails.Thumbnail + "');";
            image.Attributes["data-id-video"] = videoDetails.Id.ToString();
            image.Attributes["data-url-video"] = videoDetails.VideoId;
            image.Attributes["data-url-thumbnail"] = videoDetails.Thumbnail;
            image.Attributes["data-title"] = videoDetails.Title;
            image.Attributes["data-description"] = videoDetails.Description;

            trash.ID = videoDetails.Id + "BtnDelete";
            trash.InnerHtml = "&times;";  
            trash.Attributes["class"] = "button-item btn-delete-video";
            trash.Attributes["data-id-video"] = videoDetails.Id.ToString();

            duration.ID = videoDetails.Id + "Duration";
            duration.InnerText = videoDetails.Duration;
            duration.Attributes["class"] = "duration-video";

            image.Controls.Add(duration);
            image.Controls.Add(trash);
            return image;
        }

        private void GenerateControls()
        {
            if (Session["listControls"] != null) { 
                List<HtmlGenericControl> controls = (List<HtmlGenericControl>)Session["listControls"];
                panelVideos.Controls.Clear();
                foreach (HtmlGenericControl c in controls) panelVideos.Controls.Add(c);
            }
        }

        private void GetVideos()
        {
            VideosRepositoryImpl videosRepository = new VideosRepositoryImpl(new AppDbContext());
            List<Videos> list = videosRepository.List();
            List<HtmlGenericControl> controls = new List<HtmlGenericControl>();
            YouTubeVideoDetail videoDetails;
            foreach (Videos v in list)
            {
                videoDetails = new YouTubeVideoDetail();
                videoDetails.Id = v.Id;
                videoDetails.Title = v.Title;
                videoDetails.Description = v.Description;
                videoDetails.Thumbnail = v.Image;
                videoDetails.VideoId = v.Url;
                videoDetails.Duration = v.Duration;
                controls.Add(CreateVideoElement(videoDetails));
            }
            Session["listControls"] = controls;
        }

        private Videos SaveVideo(YouTubeVideoDetail videoDetails)
        {
            Videos n = new Videos();
            n.Image = videoDetails.Thumbnail;
            n.Description = videoDetails.Description;
            n.Title = videoDetails.Title;
            n.Url = videoDetails.VideoId;
            n.Duration = videoDetails.Duration;
            return videosRepository.Add(n);
        }
        public string ExtractVideoIdFromUrl(string url)
        {
            var regRes = regexExtractId.Match(url);
            if (regRes.Success)
            {
                return regRes.Groups[1].Value;
            }
            return null;
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public static bool DeleteVideo(int idVideo)
        {
            try
            {
                HttpContext context = HttpContext.Current;
                List<HtmlGenericControl> controls = new List<HtmlGenericControl>();
                if (context.Session["listControls"] != null)
                controls = (List<HtmlGenericControl>)context.Session["listControls"];
                VideosRepositoryImpl videosRepository = new VideosRepositoryImpl(new AppDbContext());
                videosRepository.Delete(idVideo);
                HtmlGenericControl findControl = controls.
                    Where(x => x.Attributes["data-id-video"] == idVideo.ToString()).FirstOrDefault();
                var result = controls.Remove(findControl);
                context.Session["listControls"] = controls;
            }
            catch (Exception e) {
                return false;
            }
            return true;
        }
    }
}