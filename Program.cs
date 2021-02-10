using System;
namespace MagnetTools
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = string.Empty;
            if(args.Length > 1 && !string.IsNullOrWhiteSpace(args[1]))
            {
                input = args[1];
            }
            if (string.IsNullOrWhiteSpace(input))
            {
                Console.Write("Enter magnet link:");
                input = Console.ReadLine();
                MagnetURI link = new MagnetURI(input);
                link.printReadout();
            }
        }
        static void test()
        {
            string magnetURI = "magnet:?xt=urn:btih:ec7a402ff515d80f30f6244847b672ae9fbe5d7a&dn=2021-01-11-raspios-buster-armhf-lite.zip&tr=http%3a%2f%2ftracker.raspberrypi.org%3a6969%2fannounce";

            MagnetURI link = new MagnetURI(magnetURI);
            link.printReadout();

            string magnetURI2 = "magnet:?xt=urn:btih:1b4a6fbaa086ed8424cbbcc695f8a93c747e7603&dn=chloe%20lamb&tr=udp%3a%2f%2ftracker.openbittorrent.com%3a6969%2f&tr=udp%3a%2f%2ftracker.cyberia.is%3a6969%2fannounce&tr=udp%3a%2f%2ftracker.torrent.eu.org%3a451&tr=udp%3a%2f%2fexodus.desync.com%3a6969%2fannounce&tr=udp%3a%2f%2ftracker.internetwarriors.net%3a1337%2fannounce&tr=udp%3a%2f%2f9.rarbg.me%3a2770%2fannounce&tr=udp%3a%2f%2ftracker.opentrackr.org%3a1337%2fannounce&tr=udp%3a%2f%2fexplodie.org%3a6969%2fannounce&tr=udp%3a%2f%2fipv4.tracker.harry.lu%3a80%2fannounce&tr=udp%3a%2f%2fipv6.tracker.harry.lu%3a80%2fannounce";

            link.setLink(magnetURI2);
            link.printReadout();
        }
    }
}
