using System.Text.RegularExpressions;
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
            string trackersOnly,nameAndTrackers = string.Empty;

            if(!magnetLink.Contains(_prefix) || !magnetLink.Contains(_dn))
            {
                Console.WriteLine("invalid link");
                return;
            }
            trackers = new List<String>();
            try
            {
                this.hash = magnetLink.Substring(_prefix.Length, _hashLength);
                if(!Regex.IsMatch(hash,"^[a-zA-Z0-9]*$")){
                    // throw new Exception("invalid hash");
                    Console.WriteLine("invalid hash");
                }
            }
            catch
            {
                Console.WriteLine("invalid link");
                return;
            }
            try
            {
                nameAndTrackers = magnetLink.Substring(_prefix.Length + _hashLength + _dn.Length);
                nameAndTrackers = HttpUtility.UrlDecode(nameAndTrackers);
            }
            catch
            {
                Console.WriteLine("invalid link");
                return;
            }
            if (!nameAndTrackers.Contains(_tr))
            {
                this.name = nameAndTrackers;
            }
            else
            {
                try
                {
                    this.name = nameAndTrackers.Substring(0, nameAndTrackers.IndexOf(_tr));
                    trackersOnly = nameAndTrackers.Substring(this.name.Length);
                }
                catch
                {
                    Console.WriteLine("invalid link");
                    return;
                }
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
            if (!string.IsNullOrWhiteSpace(this.hash))
            {
                Console.WriteLine();
                Console.WriteLine("Name: {0}", name);
                Console.WriteLine("Hash: {0}", hash);
                Console.WriteLine();
                Console.WriteLine("Trackers:");
                foreach (string tr in trackers)
                {
                    Console.WriteLine(tr);
                }
                Console.WriteLine();
                printJson();
            }
            else
            {
                Console.WriteLine("no data");
            }
        }
        public void printJson(){
            if (!string.IsNullOrWhiteSpace(this.hash))
            {
                Console.WriteLine(string.Format("{{name: \"{0}\", hash: \"{1}\"}},", name, hash));
            }
            else
            {
                Console.WriteLine("no data");
            }
        }
    }
}