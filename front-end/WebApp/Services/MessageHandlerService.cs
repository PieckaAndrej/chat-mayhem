﻿using System.IO;
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
        public bool IsConnected
        {
            get { return _tcpClient.Connected; }
        }
        private StreamReader? _streamReader;
        private StreamWriter? _streamWriter;
        private bool IsRunning { get; set; }

        public MessageHandlerService(Streamer streamer)
        {
            Streamer = streamer;
            _tcpClient = new TcpClient();
        }

        public async Task<bool> Connect()
        {
            await _tcpClient.ConnectAsync(IP, PORT);
            _streamReader = new StreamReader(_tcpClient.GetStream());
            _streamWriter = new StreamWriter(_tcpClient.GetStream())
            { NewLine = "\r\n", AutoFlush = true };

            await _streamWriter.WriteLineAsync($"PASS {Streamer.AccessToken}");
            await _streamWriter.WriteLineAsync($"NICK {Streamer.Name}");
            await _streamWriter.WriteLineAsync($"JOIN #{Streamer.Name}");

            return _tcpClient.Connected;
        }

        public async Task Listen()
        {
            if (IsConnected && _streamReader != null && _streamWriter != null)
            {
                IsRunning = true;
                while (IsRunning)
                {
                    string? line = await _streamReader.ReadLineAsync();
                    Console.WriteLine(line);

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

                        Console.WriteLine();
                        Console.WriteLine($"{username} said '{message}'");
                        await _streamWriter.WriteLineAsync($"PRIVMSG #{Streamer.Name} :{message} deez nuts");
                    }
                }
            }
        }
    }
}
