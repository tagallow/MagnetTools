using System;
using System.Collections.Generic;
using System.Web;

namespace MagnetTools
{
    public class MagnetURI{
        public string hash { get; set; }
        public string name { get; set; }
        private List<string> trackers { get; set; }
        private readonly string _prefix = "magnet:?xt=urn:btih:";
        private readonly string _dn = "&dn=";
        private readonly string _tr = "&tr=";
        private readonly int _hashLength = 40;
        public MagnetURI(){
            this.hash = string.Empty;
            this.name = string.Empty;
            trackers = new List<string>();
        }
        public MagnetURI(string magnetLink){
            setLink(magnetLink);
        }
        
        public void setLink(string magnetLink){
            if(!magnetLink.Contains(_prefix) || !magnetLink.Contains(_dn))
            {
                Console.WriteLine("Invalid link");
                return;
            }
            trackers = new List<String>();
            this.hash = magnetLink.Substring(_prefix.Length, _hashLength);
            string nameAndTrackers = magnetLink.Substring(_prefix.Length + _hashLength + _dn.Length);
            nameAndTrackers = HttpUtility.UrlDecode(nameAndTrackers);
            if (!nameAndTrackers.Contains(_tr))
            {
                this.name = nameAndTrackers;
            }
            else
            {
                this.name = nameAndTrackers.Substring(0, nameAndTrackers.IndexOf(_tr));
                string trackersOnly = nameAndTrackers.Substring(this.name.Length);
                foreach (string trackerURL in trackersOnly.Split(_tr))
                {
                    if (!string.IsNullOrWhiteSpace(trackerURL))
                    {
                        this.trackers.Add(trackerURL);
                    }
                }

            }
        }
        public override string ToString(){
            string uri = string.Format(_prefix + hash + _dn + "{0}", name);
            foreach(string tr in trackers){
                uri += _tr + tr;
            }
            return HttpUtility.UrlEncode(uri);
        }
        public void printReadout(){
            Console.WriteLine();
            Console.WriteLine("Name: {0}", name);
            Console.WriteLine("Hash: {0}", hash);
            Console.WriteLine();
            Console.WriteLine("Trackers:");
            foreach(string tr in trackers){
                Console.WriteLine(tr);
            }
            Console.WriteLine();
            //Console.WriteLine(this.ToString());
            printJson();
        }
        public void printJson(){
            Console.WriteLine(string.Format("{{name: {0}, hash: {1}}}", name, hash));
        }

    }
}