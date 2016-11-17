using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Timers;
using TwitchLib.TwitchAPIClasses;
using static TwitchLib.TwitchApi;


namespace ScamazCount
{
    class Program
    {
        private static string _totalnamelist;
        private static string _username;
        private static List<Chatter> _chatters;
        private static string _usercurrentpoints;
        private static MadMilkman.Ini.IniFile _secondini;
        private static string _usercurrenttime;


        static void Main()
        {
            //TwitchClient client = new TwitchClient(new ConnectionCredentials("scamazCounter", "oauth:8ecge1hywr8sm3wk0nllnx4mcvwoxv"));
            //client.OnConnected += ClientOnOnConnected;
            //client.OnMessageReceived += OnMessageReceived;
            //client.OnViewerJoined += ClientOnOnViewerJoined;
            //client.OnViewerLeft += ClientOnOnViewerLeft;
            //client.OnMessageReceived += OnMessageReceived;
            //client.Connect();
            //JoinedChannel channel = new JoinedChannel("amazhs");
            //client.JoinChannel("amazhs");
            //client.SetLoggingStatus(true);
            //client.SendMessage(channel, "test1");
            //client.OnReadLineTest(Console.ReadLine());
            //client.SendRaw("CAP REQ :twitch.tv/membership");
            Console.WriteLine("started SCAMAZING the viewers!");
            var clientid = File.ReadAllText("clientid.txt");
            if (clientid == "clientidhere" || string.IsNullOrEmpty(clientid))
            {
                Console.WriteLine("Please change clientid.txt with your client id for twitch. include oauth:");
                Console.ReadLine();
                Environment.Exit(0);
            }
            SetClientId(clientid, true);

            Timer mainTimer = new Timer
            {
                AutoReset = true,
                Interval = 60000
            };
            mainTimer.Elapsed += OnTimedEvent;
            mainTimer.Start();
            //IniFile mainini = new IniFile("main.ini");
            //mainini.Write("3", "1", "main");
            _secondini = new MadMilkman.Ini.IniFile();
            //Console.WriteLine(mainTimer.Interval);
            
            while (Console.ReadLine() != "q")
            {
            }
        }










        /*
        private static void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            Console.Clear();
            if (!BroadcasterOnline("amazhs").Result)
            {
                Console.WriteLine("amazhs is not broadcasting.");
                return;
            }
            
            _chatters = GetChatters("amazhs").Result;
            Console.WriteLine(_chatters);
            _totalnamelist = "";
            
            _secondini.Load("main.ini");
            //_section = _secondini.Sections.Add("main");
            _section = _secondini.Sections["main"];
            _newini = new MadMilkman.Ini.IniFile();
            _newSection = _newini.Sections.Add("main");
            foreach (var name in _chatters)
            {
                _username = name.Username;

                _totalnamelist = _totalnamelist + ", " + _username;
                Console.WriteLine(_username);
                
                
                if (_section.Keys.Contains(_username))
                {
                    _usercurrentpoints = _section.Keys[_username].Value;
                    int usernewpoints = int.Parse(_usercurrentpoints) + 1;
                    _newSection.Keys.Add(_username, usernewpoints.ToString());
                    //_section.Keys[_username].Value = usernewpoints.ToString();
                    Console.WriteLine(usernewpoints);
                }
                else
                {
                    _usercurrentpoints = "1";
                    _section.Keys.Add(_username, "1");
                    _newSection.Keys.Add(_username, "1");
                }
                Console.WriteLine(_usercurrentpoints);

            }
            Console.WriteLine(_totalnamelist);
            File.Delete("main.ini");
            
            _newini.Save("main.ini");
            _secondini.Sections.Clear();
            
        }
        */

        private static void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            Console.Clear();
            if (!BroadcasterOnline("amazhs").Result)
            {
                Console.WriteLine("amazhs is not broadcasting.");
                return;
            }

            _chatters = GetChatters("amazhs").Result;
            
            _totalnamelist = "";

            _secondini.Load("main.ini");
            //_section = _secondini.Sections.Add("main");
            //_section = _secondini.Sections["main"];
            
            //_newSection = _newini.Sections.Add("main");
            foreach (var name in _chatters)
            {
                _username = name.Username;
                

                _totalnamelist = _totalnamelist + ", " + _username;


                //Console.WriteLine(_username);

                
                if (_secondini.Sections[_username] != null)
                {
                    _usercurrenttime = _secondini.Sections[_username].Keys["Time"].Value;
                    _usercurrentpoints = _secondini.Sections[_username].Keys["Points"].Value;
                    int usernewpoints = int.Parse(_usercurrentpoints) + 1;
                    int usernewtime = int.Parse(_usercurrenttime) + 1;
                    //_newini.Sections.Add(_username).Keys.Add("Points", usernewpoints.ToString());
                    _secondini.Sections[_username].Keys["Time"].Value = usernewpoints.ToString();
                    _secondini.Sections[_username].Keys["Points"].Value = usernewtime.ToString();
                    //_section.Keys[_username].Value = usernewpoints.ToString();
                    //Console.WriteLine(usernewpoints);
                }
                else
                {
                    _usercurrentpoints = "1";
                    _usercurrenttime = "1";
                    int usernewpoints = 1;
                    int usernewtime = 1;
                    //_newini.Sections.Add(_username);
                    _secondini.Sections.Add(_username).Keys.Add("Points", usernewpoints.ToString());
                    _secondini.Sections.Add(_username).Keys.Add("Time", usernewtime.ToString());
                }
                //Console.WriteLine(_usercurrentpoints);

            }
            Console.WriteLine(_totalnamelist);
            File.Delete("main.ini");

            _secondini.Save("main.ini");
            _secondini.Sections.Clear();

        }

        /*
        private static void ClientOnOnConnected(object sender, TwitchClient.OnConnectedArgs onConnectedArgs)
        {
            Console.WriteLine("Connected with " + onConnectedArgs.Username);
            Console.ReadLine();
        }

        private static void ClientOnOnViewerLeft(object sender, TwitchClient.OnViewerLeftArgs onViewerLeftArgs)
        {
            Console.WriteLine(onViewerLeftArgs.Username + " left " + onViewerLeftArgs.Channel);
            Console.ReadLine();
        }

        private static void ClientOnOnViewerJoined(object sender, TwitchClient.OnViewerJoinedArgs onViewerJoinedArgs)
        {
            Console.WriteLine(onViewerJoinedArgs.Username + " joined " + onViewerJoinedArgs.Channel);
            Console.ReadLine();
        }

        private static void OnMessageReceived(object sender, TwitchClient.OnMessageReceivedArgs onMessageReceivedArgs)
        {
            Console.WriteLine("message: " + onMessageReceivedArgs.ChatMessage);
            Console.ReadLine();
        }*/

        public static void FtpUpload()
        {
            // Get the object used to communicate with the server.
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://10.0.0.23");
            request.Method = WebRequestMethods.Ftp.UploadFile;

            // This example assumes the FTP site uses anonymous logon.
            request.Credentials = new NetworkCredential("me", "8767087hr");

            // Copy the contents of the file to the request stream.
            StreamReader sourceStream = new StreamReader("main.ini");
            byte[] fileContents = Encoding.UTF8.GetBytes(sourceStream.ReadToEnd());
            sourceStream.Close();
            request.ContentLength = fileContents.Length;

            System.IO.Stream requestStream = request.GetRequestStream();
            requestStream.Write(fileContents, 0, fileContents.Length);
            requestStream.Close();

            FtpWebResponse response = (FtpWebResponse)request.GetResponse();

            Console.WriteLine("Upload File Complete, status {0}", response.StatusDescription);

            response.Close();
        }
    }
    
}
