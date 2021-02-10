using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;
using System.Globalization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MagnetTools
{
    public class MagnetURI{
        public MagnetURI(){
            this.hash = string.Empty;
            this.name = string.Empty;
            trackers = new List<string>();
        }
        public MagnetURI(string magnetLink){
            setLink(magnetLink);
        }
        public void setLink(string magnetLink){
            this.hash = magnetLink.Substring(prefix.Length, _hashLength);
            string linkAfterHash = magnetLink.Substring(prefix.Length + _hashLength + _dn.Length);
            if (!linkAfterHash.Contains(_tr))
            {
                this.name = linkAfterHash;
                trackers = new List<string>();
            }
            else
            {
                this.name = linkAfterHash.Substring(0, linkAfterHash.IndexOf(_tr));
                string trackersOnly = linkAfterHash.Substring(this.name.Length);
                trackers = new List<String>();
                foreach (string s in trackersOnly.Split(_tr))
                {
                    if (!string.IsNullOrWhiteSpace(s))
                    {
                        trackers.Add(HttpUtility.UrlDecode(s));
                    }
                }

            }
        }
        public override string ToString(){
            string uri = string.Format(prefix + hash + _dn + "{0}", name);
            foreach(string tr in trackers){
                uri += _tr + tr;
            }
            return uri;
        }
        public void printReadout(){
            Console.WriteLine("Name: {0}", name);
            Console.WriteLine("Hash: {0}", hash);
            Console.WriteLine("");
            Console.WriteLine("Trackers:");
            foreach(string tr in trackers){
                Console.WriteLine(tr);
            }
            Console.WriteLine();
        }
        public void printJson(){
            Console.WriteLine(string.Format("{{name: {0}, hash: {1}}}", name, hash));
        }
        public string hash{ get; set; }
        public string name { get; set; }
        private List<string> trackers { get; set; }
        private readonly string prefix = "magnet:?xt=urn:btih:";
        private readonly string _dn = "&dn=";
        private readonly string _tr = "&tr=";
        private readonly int _hashLength = 40;
    }
}