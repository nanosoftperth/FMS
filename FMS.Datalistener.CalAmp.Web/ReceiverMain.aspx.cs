using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FMS.Datalistener.CalAmp.Web
{
    public partial class ReceiverMain : System.Web.UI.Page
    {
        Owin.CalAmp.Client.CanReceiverClient receiver = new Owin.CalAmp.Client.CanReceiverClient("http://localhost:8080");
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Get1_Click(object sender, EventArgs e)
        {
            receiver.GetCanReceiver();
        }

        protected void Get2_Click(object sender, EventArgs e)
        {
            receiver.GetCanReceiver("1");
        }

        protected void Get3_Click(object sender, EventArgs e)
        {
            receiver.GetCanReceiver("param1", "param2");
        }

        protected void Get4_Click(object sender, EventArgs e)
        {
            receiver.GetCanReceiver("param1", 1, 2, "timex");
        }

        protected void Post1_Click(object sender, EventArgs e)
        {
            receiver.PostCanReceiver("sample param");
        }
    }
}