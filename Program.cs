// Copyright (c) VAVE 2022. All rights reserved.
// For the full license, see https://github.com/vave-studios/hasher/blob/trunk/LICENSE
// Program.cs - The main file
// 

using DSharpPlus;
using DSharpPlus.Entities;

namespace HasherBot {
    class Program {
        static string prefix = BotConfiguration.Prefix;

        static void Main(string[] args) {
            MainAsync().GetAwaiter().GetResult();
        }

        static async Task MainAsync() {
            var discord = new DiscordClient(new DiscordConfiguration() {
                Token = BotConfiguration.BotToken,
                TokenType = TokenType.Bot
            });

            discord.MessageCreated += async (s, e) => {
                if (e.Message.Content.ToLower().StartsWith(prefix + "hash")) {
                    var message = e.Message;
                    var attachment = message.Attachments.FirstOrDefault();

#pragma warning disable CS8602 // Dereference of a possibly null reference.
                    string attachmentUrl = attachment.Url;
#pragma warning restore CS8602 // Dereference of a possibly null reference.
                    string fileName = attachment.FileName;

                    string file = Classes.DownloadManager.Download(attachmentUrl, fileName);

                    var hashes = Classes.HashCalculator.GetHashesFromFile(file);

                    var embed = new DiscordEmbedBuilder() {
                        Title = "Hashes for " + fileName,
                        Description = "Hashes in MD5 and SHA-256"
                    };

                    embed.AddField("MD5", hashes.MD5);
                    embed.AddField("SHA-256", hashes.SHA256);
                    embed.AddField("Download URL", attachmentUrl);

                    File.Delete(file);

                    await message.RespondAsync(embed);
                }
            };

            await discord.ConnectAsync();
            await Task.Delay(-1);
        }
    }
}