using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualBasic;
using System.Threading;

namespace DiscordBot
{

    class Program
    {
        bool _waitFlg = false; // 綾鷹が黙る時間
        private DiscordSocketClient _client;
        public static CommandService _commands;
        public static IServiceProvider _services;

        static void Main(string[] args)
            => new Program().MainAsync().GetAwaiter().GetResult();

        public async Task MainAsync()
        {

            _client = new DiscordSocketClient(new DiscordSocketConfig
            {
                LogLevel = LogSeverity.Info
            });
            _client.Log += Log;
            _commands = new CommandService();
            _services = new ServiceCollection().BuildServiceProvider();
            _client.MessageReceived += CommandRecieved;
            //次の行に書かれているstring token = "hoge"に先程取得したDiscordTokenを指定する。
            string token = "NzU0NTU1Mzk4MzQxNjU2Njk2.X12ceQ.4xPO-Ii8iccOYgA3Ogu7hrmwIGg";
            await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), _services);
            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();

            await Task.Delay(-1);
        }

        /// <summary>
        /// 何かしらのメッセージの受信
        /// </summary>
        /// <param name="msgParam"></param>
        /// <returns></returns>
        private async Task CommandRecieved(SocketMessage messageParam)
        {

            var message = messageParam as SocketUserMessage;

            //デバッグ用メッセージを出力
            Console.WriteLine("{0} {1}:{2}", message.Channel.Name, message.Author.Username, message);
            //メッセージがnullの場合
            if (message == null)
                return;

            //発言者がBotの場合無視する
            if (message.Author.IsBot)
                return;

            if (_waitFlg)
                return;

            var context = new CommandContext(_client, message);

            //ここから記述--------------------------------------------------------------------------
            var CommandContext = StringConvert(message.Content);

            // コマンド("おはよう")かどうか判定
            if (CommandContext.Contains("!Team") || CommandContext.Contains("!team"))
            {
                await DevideTeam(message);
            }
            else if (CommandContext.Contains("!Help") || CommandContext.Contains("!help"))
            {
                await Help(message);
            }
            else if (CommandContext.Contains("すま") || CommandContext.Contains("ごめ") || CommandContext.Contains("すみま"))
            {
                await Gomen(message);
            }
            else if (CommandContext.Contains("おは") || CommandContext.Contains("こん"))
            {
                await Ohayou(message);
            }
            else if (CommandContext.Contains("おやす") || CommandContext.Contains("おつ"))
            {
                await Otukare(message);
            }
            else if (CommandContext.Contains("ないす"))
            {
                await Nice(message);
            }
            else if (CommandContext.Contains("離") || CommandContext.Contains("りせき") || CommandContext.Contains("はなれ"))
            {
                await Riseki(message);
            }
            else if (CommandContext.Contains("勝") || CommandContext.Contains("負"))
            {
                await Win(message);
            }
            else if (CommandContext.Contains("わかる") || CommandContext.Contains("せやな") || CommandContext.Contains("せやろ"))
            {
                await Seyaro(message);
            }
            else if (CommandContext.Contains("よー\r\n") || CommandContext.Contains("ろー\r\n") || CommandContext.Contains("やろ\r\n") || CommandContext.Contains("しよ\r\n"))
            {
                await Lets(message);
            }
            else if (CommandContext.Contains("うるさ") || CommandContext.Contains("だま") || CommandContext.Contains("黙"))
            {
                await Damare(message);
            }
            else if (CommandContext.Contains("か？") || CommandContext.Contains("ん？") || CommandContext.Contains("い？"))
            {
                await Question(message);
            }
            else if (CommandContext.Contains("おわ") || CommandContext.Contains("やめ"))
            {
                await Owaro(message);
            }
            else if (CommandContext.Contains("ww") || CommandContext.Contains("ｗｗ") || CommandContext.Contains("草") || CommandContext.Contains("わろ"))
            {
                await Warota(message);
            }
            else if (CommandContext.Contains("面白い話して"))
            {
                await Omoshiro(message);
            }

        }

        private async Task DevideTeam(SocketUserMessage message)
        {
            string res = default;
            ulong generalId = 490411213998522378; // generalチャンネル
            SocketChannel sd = _client.GetChannel(generalId);
            IReadOnlyCollection<SocketUser> suArray = sd.Users;
            List<string> nameArray = new List<string>();
            foreach (SocketUser su in suArray)
            {
                nameArray.Add(su.Username);
            }
            int cnt = nameArray.Count;
            Random rnd = new Random();
            if (cnt <= 1)
            {
                res = "チーム分けれないじゃん・・・！";
                await message.Channel.SendMessageAsync(res);
                return;
            }
            for (int i = 0; i < cnt; i++)
            {
                int ranNum = rnd.Next(nameArray.Count);
                if (i < cnt / 2)
                {
                    if (i == 0)
                    {
                        res = res + "-------- 1チーム --------\r\n";
                    }
                    res = res + nameArray[ranNum] + "\r\n";
                }
                else
                {
                    if (i < ((cnt / 2) + 1))
                    {
                        res = res + "-------- 2チーム --------\r\n";
                    }
                    res = res + nameArray[ranNum] + "\r\n";
                }
                nameArray.RemoveAt(ranNum);
            }

            await message.Channel.SendMessageAsync(res);
        }

        private async Task Help(SocketUserMessage message)
        {
            await message.Channel.SendMessageAsync("うぃーっす！綾鷹Botだゾ～。\r\n" +
                                                "「!team」とコメントすることでgeneralにいるメンバーをチーム分けするゾ～。\r\n" +
                                                "あと適当なコメントにも反応するゾ～。");
        }


        private Task Log(LogMessage message)
        {
            Console.WriteLine(message.ToString());
            return Task.CompletedTask;
        }

        /// <summary>
        /// Hello!
        /// </summary>
        /// <param name="msgParam"></param>
        /// <returns></returns>
        private async Task Ohayou(SocketUserMessage message)
        {
            await message.Channel.SendMessageAsync("あ、" + message.Author.Username + "さん、ちぃーっす！");
        }

        /// <summary>
        /// GoodNight!
        /// </summary>
        /// <param name="msgParam"></param>
        /// <returns></returns>
        private async Task Otukare(SocketUserMessage message)
        {
            await message.Channel.SendMessageAsync("あ、" + message.Author.Username + "さん、おつかれーっす！ｽﾔｧ...");
        }

        /// <summary>
        /// ないす
        /// </summary>
        /// <param name="msgParam"></param>
        /// <returns></returns>
        private async Task Nice(SocketUserMessage message)
        {
            await message.Channel.SendMessageAsync(message.Author.Username + "、ナイスゥゥゥゥゥゥゥ～～～～～!");
        }

        /// <summary>
        /// 離籍
        /// </summary>
        /// <param name="msgParam"></param>
        /// <returns></returns>
        private async Task Riseki(SocketUserMessage message)
        {
            await message.Channel.SendMessageAsync("あ、自分もちょっとお酒入れてきます・・・");
        }

        /// <summary>
        /// 勝ち
        /// </summary>
        /// <param name="msgParam"></param>
        /// <returns></returns>
        private async Task Win(SocketUserMessage message)
        {
            await message.Channel.SendMessageAsync("イェ～イ！俺の勝ち！");
        }

        /// <summary>
        /// ワロタ
        /// </summary>
        /// <param name="msgParam"></param>
        /// <returns></returns>
        private async Task Warota(SocketUserMessage message)
        {
            await message.Channel.SendMessageAsync(message.Author.Username + "さんクソワロタｗｗｗｗｗ");
        }

        /// <summary>
        /// ごめん
        /// </summary>
        /// <param name="msgParam"></param>
        /// <returns></returns>
        private async Task Gomen(SocketUserMessage message)
        {
            await message.Channel.SendMessageAsync(message.Author.Username + "も反省してるみたいだし、許してあげようよ、ね？");
        }

        /// <summary>
        /// GoodNight!
        /// </summary>
        /// <param name="msgParam"></param>
        /// <returns></returns>
        private async Task Question(SocketUserMessage message)
        {
            await message.Channel.SendMessageAsync("え、いいなー！" + message.Author.Username + "さん、俺も入れて―！");
        }

        /// <summary>
        /// おわろ
        /// </summary>
        /// <param name="msgParam"></param>
        /// <returns></returns>
        private async Task Owaro(SocketUserMessage message)
        {
            await message.Channel.SendMessageAsync("え、もうやめちゃうんすか？");
        }

        /// <summary>
        /// だまれ
        /// </summary>
        /// <param name="msgParam"></param>
        /// <returns></returns>
        private async Task Damare(SocketUserMessage message)
        {
            await message.Channel.SendMessageAsync("あっごめん" + message.Author.Username + "さん、少し黙るわ・・・");
            Thread.Sleep(60000);
            _waitFlg = false;
            await message.Channel.SendMessageAsync("いぇーい！もう喋ってええやんな？");
        }

        /// <summary>
        /// せやろ
        /// </summary>
        /// <param name="msgParam"></param>
        /// <returns></returns>
        private async Task Seyaro(SocketUserMessage message)
        {
            await message.Channel.SendMessageAsync("わかる―");
        }

        /// <summary>
        /// やろ
        /// </summary>
        /// <param name="msgParam"></param>
        /// <returns></returns>
        private async Task Lets(SocketUserMessage message)
        {
            await message.Channel.SendMessageAsync("まじで！？俺もやる―！");
        }

        /// <summary>
        /// おもしろい話して
        /// </summary>
        /// <param name="msgParam"></param>
        /// <returns></returns>
        private async Task Omoshiro(SocketUserMessage message)
        {
            await message.Channel.SendMessageAsync("俺が1週間SMAPだった話する？");
        }

        private string StringConvert(string val)
        {
            try
            {
                string res = default;
                res = Strings.StrConv(val.Trim(), VbStrConv.Hiragana | VbStrConv.Wide);
                return res;
            }
            catch
            {
                return "";
            }
        }
    }
}
