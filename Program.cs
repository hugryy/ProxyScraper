using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace Proxies
{
    class Program
    {

        private static void WriteLine(string text, ConsoleColor consoleColor)
        {
            ConsoleColor currentForeground = Console.ForegroundColor;
            Console.ForegroundColor = consoleColor;
            Console.WriteLine(text);
            Console.ForegroundColor = currentForeground;
        }
        public static string GetBetween(string strSource, string strStart, string strEnd)
        {
            if (strSource.Contains(strStart) && strSource.Contains(strEnd))
            {
                int Start, End;
                Start = strSource.IndexOf(strStart, 0) + strStart.Length;
                End = strSource.IndexOf(strEnd, Start);
                return strSource.Substring(Start, End - Start);
            }
            return "";
        }
        public static List<string> ProxiesUrls = new List<string>();
        public static List<string> Proxies = new List<string>();
        private static void InstaBypass()
        {
            string[] instabypass = new WebClient().DownloadString("https://www.instabypass.com/proxy/proxy/?C=M;O=D").Split('\n');
            int x = int.Parse(DateTime.Now.ToString("dd"));
            for (int i = 0; i < x; i++)
                if (x != 1)
                {
                    x--;
                    for (int a = 0; a < 50; a++)
                        if (instabypass[a].Contains(DateTime.Now.ToString("yyyy-MM") + "-0" + x))
                        {
                            ProxiesUrls.Add("https://www.instabypass.com/proxy/proxy/" + GetBetween(GetBetween(instabypass[a], "<tr><td valign=\"top\">&nbsp;</td><td><a href=", "</td><td align=\"right\">" + DateTime.Now.ToString("yyyy-MM") + "-0" + x), "\"", "\">"));
                            WriteLine("[+] URL added: https://www.instabypass.com/proxy/proxy/" + GetBetween(GetBetween(instabypass[a], "<tr><td valign=\"top\">&nbsp;</td><td><a href=", "</td><td align=\"right\">" + DateTime.Now.ToString("yyyy-MM") + "-0" + x), "\"", "\">"), ConsoleColor.Green);
                        }
                }
        }
#pragma warning disable IDE1006 // Naming Styles
        public class CheckerProxyJSON
        {
            public int id { get; set; }
            public int local_id { get; set; }
            public string report_id { get; set; }
            public string addr { get; set; }
            public int type { get; set; }
            public int kind { get; set; }
            public int timeout { get; set; }
            public bool cookie { get; set; }
            public bool referer { get; set; }
            public bool post { get; set; }
            public string ip { get; set; }
            public string addr_geo_iso { get; set; }
            public string addr_geo_country { get; set; }
            public string addr_geo_city { get; set; }
            public string ip_geo_iso { get; set; }
            public string ip_geo_country { get; set; }
            public string ip_geo_city { get; set; }
            public DateTime created_at { get; set; }
            public DateTime updated_at { get; set; }
            public bool skip { get; set; }
            public bool from_cache { get; set; }
        }
#pragma warning restore IDE1006 // Naming Styles
        private static void CheckerProxy()
        {
            string data = new WebClient().DownloadString("https://checkerproxy.net/getAllProxy");
            try
            {
                int count = 0;
                string one = new WebClient().DownloadString("https://checkerproxy.net/api" + GetBetween(data, "</p><ul><li><a href=\"", "\">"));
                var jsoninfo = JsonConvert.DeserializeObject<List<CheckerProxyJSON>>(one);
                for (int z = 0; z < jsoninfo.Count; z++)
                {
                    Proxies.Add(Regex.Replace(jsoninfo[z].addr, @"[^0-9\s.:]", ""));
                    count += 1;
                }
                WriteLine($"[+] CheckerProxy.Net 1#, {count} proxies scraped.", ConsoleColor.Green);
            }
            catch { }
            try
            {
                int count = 0;
                string two = new WebClient().DownloadString("https://checkerproxy.net/api" + GetBetween(data, "</a></li><li><a href=\"", "\">"));
                var jsoninfo = JsonConvert.DeserializeObject<List<CheckerProxyJSON>>(two);
                for (int z = 0; z < jsoninfo.Count; z++)
                {
                    Proxies.Add(Regex.Replace(jsoninfo[z].addr, @"[^0-9\s.:]", ""));
                    count += 1;
                }
                WriteLine($"[+] CheckerProxy.Net 2#, {count} proxies scraped.", ConsoleColor.Green);
            }
            catch { }
        }
        private static void ParseURLs(string name)
        {
            string[] lines = File.ReadAllLines(name);
            for (int a = 0; a < lines.Count(); a++)
            {
                ProxiesUrls.Add(lines[a]);
                WriteLine("[+] URL added: " + lines[a], ConsoleColor.Green);
            }
            for (int o = 0; o < ProxiesUrls.Count; o++)
            {
                Proxies.Add(Regex.Replace(new WebClient().DownloadString(ProxiesUrls[o]), @"[^0-9\s.:]", ""));
            }
            WriteLine($"[+] All URL's was scraped.", ConsoleColor.Green);
        }
        private static void ProxyScrapeCom()
        {
            string data = Regex.Replace(new WebClient().DownloadString("https://api.proxyscrape.com/proxytable.php").ToLower().Replace("\":1", "\n").Replace("\":2", "\n").Replace("\":3", "\n").Replace("\"http\":", "").Replace("\"https\":", "").Replace("\"socks4\":", "").Replace("\"socks5\":", ""), @"[^0-9\s.:]", "");
            Proxies.Add(data);
            WriteLine($"[+] CheckerProxy.Net, {data.Split('\n').Count()} proxies scraped.", ConsoleColor.Green);
        }
        private static void Proxs()
        {
            string data = Regex.Replace(GetBetween(new WebClient().DownloadString("https://proxs.ru/freeproxy.php"), "<table><tr><td>", "</td></tr></table></div><br /><br />").Replace("</td><td width=1%><div class=\"proxy-flag\"></div></td><td>&nbsp;", "\n"), @"[^0-9\s.:]", "");
            Proxies.Add(data);
            WriteLine($"[+] CheckerProxy.Net, {data.Split('\n').Count()} proxies scraped.", ConsoleColor.Green);
        }
        
        private static void HideMyName()
        {
            string url = "https://hidemy.name/en/proxy-list/";
            var output = "";
            var data = new Leaf.xNet.HttpRequest
            {
                CharacterSet = Encoding.UTF8,
                KeepAlive = true,
                KeepAliveTimeout = 1000,
                UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/85.0.4183.121 Safari/537.36 OPR/71.0.3770.323"
            };
            string response = data.Get(url).ToString();
            var matches = Regex.Matches(response, @"(\d{1,3}.\d{1,3}.\d{1,3}.\d{1,3}</td>)|(\d{2,5}</td>)");
            int matchesCount = matches.Count;
            for (int i = 0; i < matchesCount; i += 2)
                if (!Regex.Match(matches[i].Value, @"([A-Za-z-])").Success || matches[i].Value.Contains("<"))
                    output += matches[i].Value.Replace("</td>", "") + ":" + matches[i + 1].Value.Replace("</td>", "") + Environment.NewLine;
            Proxies.Add(output);
            WriteLine($"[+] HideMy.Name, {output.Split('\n').Count()} proxies scraped.", ConsoleColor.Green);
        }
        private static void SocksProxyPack()
        {
            string one = GetBetween(new WebClient().DownloadString("https://www.socks-proxy.net/"), " UTC.", "</textarea>");
            Proxies.Add(one);
            WriteLine($"[+] Socks-Proxy.Net, {one.Split('\n').Count()} proxies scraped.", ConsoleColor.Green);

            string two = GetBetween(new WebClient().DownloadString("https://free-proxy-list.net/"), " UTC.", "</textarea>");
            Proxies.Add(two);
            WriteLine($"[+] Free-Proxy-List.Net, {two.Split('\n').Count()} proxies scraped.", ConsoleColor.Green);

            string three = GetBetween(new WebClient().DownloadString("https://www.sslproxies.org/"), " UTC.", "</textarea>");
            Proxies.Add(three);
            WriteLine($"[+] SslProxies.Org, {three.Split('\n').Count()} proxies scraped.", ConsoleColor.Green);
        }
        private static void ProxyNova()
        {
            int count = 0;
            string url = "https://www.proxynova.com/proxy-server-list/";
            string data = new WebClient().DownloadString(url);
            string[] lines = data.Replace("\r\n", "\n").Replace("\r", "\n").Split('\n');
            Regex ip = new Regex(@"\b\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}\b");

            for (int i = 0; i < lines.Count(); i++)
            {
                if (lines[i].Contains("<script>"))
                {
                    if (ip.Matches(lines[i]).Count > 0)
                    {
                        try
                        {
                            var engine = new Jurassic.ScriptEngine();
                            string script = lines[i].Replace("<script>", "").Replace(")</script>", "").Replace("document.write(", "");

                            Proxies.Add(Regex.Replace(engine.Evaluate(script) + ":" + GetBetween(data.Replace("title=\"Port 8080 proxies\">8080</a>", "").Replace("title=\"Port 80 proxies\">80</a>", "").Replace("title=\"Port 3128 proxies\">3128</a>", ""), lines[i], "<time"), @"[^0-9\s.:]", "").Replace("\n", "").Replace(" ", ""));
                            count += 1;
                        }
                        catch { }
                    }
                }
            }
            WriteLine($"[+] ProxyNova, {count} proxies scraped.", ConsoleColor.Green);
        }
        /*public class HidestarJSON
        {
            public string IP { get; set; }
            public int PORT { get; set; }
            public int latest_check { get; set; }
            public int ping { get; set; }
            public int connection_delay { get; set; }
            public string country { get; set; }
            public int down_speed { get; set; }
            public int up_speed { get; set; }
            public object proxiescol { get; set; }
            public string anonymity { get; set; }
            public string type { get; set; }
            public int google_proxy { get; set; }
        }
        private static void Hidester()
        {
            string url = "https://hidester.com/proxydata/php/data.php?mykey=data&limit=99999&orderBy=latest_check&sortOrder=DESC";
            var data = new Leaf.xNet.HttpRequest
            {
                CharacterSet = Encoding.UTF8,
                KeepAlive = true,
                UseCookies = true,
                Cookies = ,
                KeepAliveTimeout = 1000,
                UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/85.0.4183.121 Safari/537.36 OPR/71.0.3770.323"
            };
            string lol = data.Get(url).ToString();
            var myDeserializedClass = JsonConvert.DeserializeObject<List<HidestarJSON>>(lol);

            for (int i = 0; i < myDeserializedClass.Count; i++)
            {
                Proxies.Add(myDeserializedClass[i].IP + ":" + myDeserializedClass[i].PORT);
                Console.WriteLine(myDeserializedClass[i].IP + ":" + myDeserializedClass[i].PORT);
            }
        }*/

        /*private static void PremProxy()
        {
            //Console.WriteLine(GetBetween(new WebClient().DownloadString("https://premproxy.com/list/ip-port/1.htm"), "<!-- IP:Port list -->", "<!-- End of IP:Port list -->"));
            for (int i = 1; i < 999; i++)
            {
                string data = GetBetween(new WebClient().DownloadString("https://premproxy.com/list/ip-port/" + i + ".htm"), "<!-- IP:Port list -->", "<!-- End of IP:Port list -->").Replace("<li>", "");
                int a = Regex.Matches(data, "<span class=\"").Count;
                for (int z = 0; z < a; z++)
                {
                    data = data.Replace("<span class=\"" + GetBetween(data, " <span class=\"", "\">") + "\">", "");
                }
                //Console.WriteLine(data);
            }
        }*/
        private static void OpenProxy()
        {
            int count = 0;
            MatchCollection mc = Regex.Matches(
            new WebClient().DownloadString("https://openproxy.space/list/http") +
            new WebClient().DownloadString("https://openproxy.space/list/socks4") +
            new WebClient().DownloadString("https://openproxy.space/list/socks5"),
            @"\b\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}\b:\d{1,6}");

            foreach (Match m in mc)
            {
                Proxies.Add(m.ToString());
                count += 1;
            }

            WriteLine($"[+] OpenProxy.Space, {count} proxies scraped.", ConsoleColor.Green);
        }

        static void Main()
        {
            List<Thread> Threads = new List<Thread>
            {
                new Thread(() => OpenProxy()),
                new Thread(() => ProxyNova()),
                new Thread(() => ProxyScrapeCom()),
                new Thread(() => SocksProxyPack()),
                new Thread(() => HideMyName()),
                new Thread(() => InstaBypass()),
                new Thread(() => Proxs()),
                new Thread(() => CheckerProxy())
            };

            foreach (Thread t in Threads)
                t.Start();
            foreach (Thread t in Threads)
                t.Join();

            ParseURLs(@"urls.txt");
            
            if (File.Exists("proxies.txt"))
            {
                WriteLine("[!] proxies.txt is already exists, deleting...", ConsoleColor.Red);
                File.Delete("proxies.txt");
            }

            File.WriteAllText("proxies.txt", string.Join(Environment.NewLine, Proxies.Distinct().ToArray()));

            WriteLine("[★] Done!", ConsoleColor.Yellow);
            Console.WriteLine("Scraped Total Proxies: " + File.ReadAllLines("proxies.txt").Count());
            Console.ReadLine();
        }
    }
}
