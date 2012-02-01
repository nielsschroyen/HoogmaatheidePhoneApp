using System;
using System.Collections.Generic;
using System.Net;
using System.Windows;
using HoogmaatheideApp.Models;

namespace HoogmaatheideApp.Helpers
{
    public class WebRequester
    {
        public delegate void GetNestenEventHandler(object sender, GetRassenCallbackArguments e);
        public event GetNestenEventHandler RetrieveNestenCompleted;

        public void GetNestenAsync(string url)
        {
            var client = new WebClient();

            client.DownloadStringCompleted += Downloaded;
            try
            {
                client.DownloadStringAsync(new Uri(url));
            }
            catch (Exception ex)
            {
                RetrieveNestenCompleted(this, new GetRassenCallbackArguments { Exception = ex });
            }
        }

        private void Downloaded(object sender, DownloadStringCompletedEventArgs e)
        {
            try
            {
    
                if (!e.Cancelled)
                {
                    RetrieveNestenCompleted(this, new GetRassenCallbackArguments { Rassen = JsonSerialiser.DeSerialise(e.Result) });

                }
                else
                {

                    throw new Exception("Cancelled Download");
                }

            }
            catch (Exception ex)
            {
                RetrieveNestenCompleted(this, new GetRassenCallbackArguments { Exception = ex });
            }

        }
    }

    public class GetRassenCallbackArguments
    {
        public List<Ras> Rassen { get; set; }
        public Exception Exception { get; set; }
    }
}


