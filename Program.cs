using System;

namespace MagnetTools
{
    class Program
    {
        static void Main(string[] args)
        {
            //https://en.wikipedia.org/wiki/Magnet_URI_scheme#Format
            string magnetURI = "magnet:?xt=urn:btih:ec7a402ff515d80f30f6244847b672ae9fbe5d7a&dn=2021-01-11-raspios-buster-armhf-lite.zip&tr=http%3a%2f%2ftracker.raspberrypi.org%3a6969%2fannounce";

            string magnetURI2 = "magnet:?xt=urn:btih:1b4a6fbaa086ed8424cbbcc695f8a93c747e7603&dn=chloe%20lamb&tr=udp%3a%2f%2ftracker.openbittorrent.com%3a6969%2f&tr=udp%3a%2f%2ftracker.cyberia.is%3a6969%2fannounce&tr=udp%3a%2f%2ftracker.torrent.eu.org%3a451&tr=udp%3a%2f%2fexodus.desync.com%3a6969%2fannounce&tr=udp%3a%2f%2ftracker.internetwarriors.net%3a1337%2fannounce&tr=udp%3a%2f%2f9.rarbg.me%3a2770%2fannounce&tr=udp%3a%2f%2ftracker.opentrackr.org%3a1337%2fannounce&tr=udp%3a%2f%2fexplodie.org%3a6969%2fannounce&tr=udp%3a%2f%2fipv4.tracker.harry.lu%3a80%2fannounce&tr=udp%3a%2f%2fipv6.tracker.harry.lu%3a80%2fannounce";

            MagnetURI uri = new MagnetURI(magnetURI2);
            //uri.printJson();
            uri.printJson();
            uri.setLink(magnetURI);
            uri.printJson();
        }
    }
}
