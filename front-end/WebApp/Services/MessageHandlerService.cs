using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Net.Sockets;
using WebApp.Models;

namespace WebApp.Services
{
    public class MessageHandlerService
    {
        private const string IP = "irc.chat.twitch.tv";
        private const int PORT = 6667;
        public Streamer Streamer { get; set; }
        private TcpClient _tcpClient;
        private QuestionService questionService;

        public bool IsConnected
        {
            get { return _tcpClient.Connected; }
        }
        private StreamReader? _streamReader;
        private StreamWriter? _streamWriter;
        public bool IsRunning { get; set; }

        public MessageHandlerService(Streamer streamer)
        {
            Streamer = streamer;
        }

        public async Task<bool> Connect(Action<string> func)
        {
            // Validate if the token is still valid
            TwitchValidate? validate = await TwitchService.ValidateToken(Streamer.AccessToken);

            if (validate?.UserId == null)
            {
                string? access = await StreamerService.RefreshToken(
                    Streamer.UserId, Streamer.AccessToken);

                if (!String.IsNullOrEmpty(access))
                {
                    Streamer.AccessToken = access;
                    func(access);
                }
                //TODO something
                return false;
            }

            _tcpClient = new TcpClient();
            await _tcpClient.ConnectAsync(IP, PORT);
            _streamReader = new StreamReader(_tcpClient.GetStream());
            _streamWriter = new StreamWriter(_tcpClient.GetStream())
            { NewLine = "\r\n", AutoFlush = true };

            //has to be lower case for some reason, maybe get the login instead of name, because
            //it might be something else sometimes
            string streamerName = Streamer.Name.ToLower();

            await _streamWriter.WriteLineAsync($"PASS oauth:{Streamer.AccessToken}");
            await _streamWriter.WriteLineAsync($"NICK {streamerName}");
            await _streamWriter.WriteLineAsync($"JOIN #{streamerName}");

            return _tcpClient.Connected;
        }

        public void StopListening()
        {
            IsRunning = false;
            _streamReader?.Dispose();
            _streamWriter?.Dispose();
            _tcpClient.Close();
        }

        public async Task Listen(Func<string, string, string, Task> function)
        {
            if (IsConnected && _streamReader != null && _streamWriter != null)
            {
                IsRunning = true;
                while (IsRunning)
                {
                    string line = await _streamReader.ReadLineAsync() ?? "";
                    if (!String.IsNullOrWhiteSpace(line))
                    {
                        Console.WriteLine(line);
                    }

                    string[] split = line.Split(" ");

                    //PING :tmi.twitch.tv
                    //Respond with PONG :tmi.twitch.tv
                    if (line.StartsWith("PING"))
                    {
                        Console.WriteLine("PING");
                        await _streamWriter.WriteLineAsync($"PONG {split[1]}");
                    }

                    if (split.Length > 1 && split[1] == "PRIVMSG")
                    {
                        //:mytwitchchannel!mytwitchchannel@mytwitchchannel.tmi.twitch.tv 
                        // ^^^^^^^^
                        //Grab this name here
                        int exclamationPointPosition = split[0].IndexOf("!");

                        //Skip the first character, the first colon, then find the next colon
                        int secondColonPosition = line.IndexOf(':', 1);//the 1 here is what skips the first character

                        string username = split[0].Substring(1, exclamationPointPosition - 1);
                        string message = line.Substring(secondColonPosition + 1);//Everything past the second colon

                        await function(username, message, Streamer.UserId);
                        //await QuestionService.InsertAnswers(new ViewerAnswer(username, message), Streamer.UserId, question);
                    }
                }
            }
            // for debugging
            Console.WriteLine("Run end");
        }
    }
}
