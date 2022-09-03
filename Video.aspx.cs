using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AFEXChile
{
    public partial class Video : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string idVideo = Request.QueryString["Id"];
            showVideo(idVideo);
        }

        private void showVideo(string url) {

            string script = "<iframe width='1200' height='800' src='https://www.youtube.com/embed/" + url + "?autoplay=1'" + 
                "title='YouTube video player' frameborder='0' allow='accelerometer; autoplay; clipboard-write;" +
                " encrypted-media; gyroscope; picture-in-picture' allowfullscreen></iframe>";
            this.video.InnerHtml = script;
        }
    }
}