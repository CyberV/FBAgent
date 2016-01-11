using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SHDocVw;
using mshtml;
using System.Threading;
using System.Diagnostics;

namespace FBAgent
{
    class Browser
    {
        InternetExplorer ie;
        Thread thrdIe;
        bool busyBefore = false;

        public delegate void DocUpdatedEventHandler();
        public event DocUpdatedEventHandler DocUpdated;

        public bool Visible
        {
            get 
            {
                return ie.Visible;   
            }
            set
            {
                ie.Visible = value;
            }
        }

        public HTMLDocument Document
        {
            get
            {
                try
                {
                    return (HTMLDocument)ie.Document;
                }
                catch (Exception ee)
                {
                    return null;
                }
            }
        }

        public Browser()
        {
            ie = new InternetExplorer();
            ie.Visible = true;
            ie.DownloadComplete += new DWebBrowserEvents2_DownloadCompleteEventHandler(ie_DownloadComplete);
            ie.DocumentComplete += new DWebBrowserEvents2_DocumentCompleteEventHandler(ie_DocumentComplete);

            thrdIe = new Thread(CheckIEBusy);
            thrdIe.SetApartmentState(ApartmentState.STA);
            thrdIe.Start();
        }

        ~Browser()
        {
            if (thrdIe.IsAlive)
                try
                {
                    thrdIe.Abort();
                }
                catch { }

            if (ie != null)
            {
                try
                {
                    ie.Quit();
                }

                catch { }
                finally
                {
                    ie = null;
                }

            }
        }

        public void Navigate(String url)
        {
            ie.Navigate(url);
        }

        void CheckIEBusy()
        {
            while(ie!= null)
            {
                try
                {
                    if (!ie.Busy && busyBefore)
                        DocUpdated();

                    busyBefore = ie.Busy;
                }
                catch
                {
                    break;
                }
                Thread.Sleep(1000);
            }
        }

        void ie_DownloadComplete()
        {
            //throw new NotImplementedException();
        }

        void ie_DocumentComplete(object pDisp, ref object URL)
        {
            //throw new NotImplementedException();
        }
    }
}
