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
            string token = "NzU0NTU1Mzk4MzQxNjU2Njk2.X12ceQ.Y0HU19jzLD755Y4-gJIgcRqsCJQ";
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

            CommandContext context = new CommandContext(_client, message);

            // 綾鷹Bot用に文字変換
            string messageText = StringConvert(message.Content);
            // コマンド("おはよう")かどうか判定
            if (messageText.Contains("!Team") || messageText.Contains("!team"))
            {
                await DevideTeam(message);
            }
            else if (messageText.Contains("!Help") || messageText.Contains("!help"))
            {
                await Help(message);
            }
            else if (messageText.Contains("＠"))
            {
                await AtMark(message);
            }
            else if (messageText.Contains("面白") || messageText.Contains("おもしろ") || messageText.Contains("おもろ")
                || messageText.Contains("綾鷹") || messageText.Contains("あやたか"))
            {
                await Omosiro(message);
            }
            else if (messageText.Contains("ｈｔｔｐ"))
            {
                await Http(message);
            }
            else if (messageText.Contains("すま") || messageText.Contains("ごめ") || messageText.Contains("すみま"))
            {
                await Gomen(message);
            }
            else if (messageText.Contains("はる") || messageText.Contains("るんこ"))
            {
                await Oharu(message);
            }
            else if (messageText.Contains("きゃべ"))
            {
                await Cabagge(message);
            }
            else if (messageText.Contains("ｗｉｌ") || messageText.Contains("うぃる"))
            {
                await Wilkinson(message);
            }
            else if (messageText.Contains("ばた") || messageText.Contains("Arik"))
            {
                await Bata(message);
            }
            else if (messageText.Contains("おは") || messageText.Contains("こん"))
            {
                await Ohayou(message);
            }
            else if (messageText.Contains("おやす") || messageText.Contains("おつ"))
            {
                await Otukare(message);
            }
            else if (messageText.Contains("ないす"))
            {
                await Nice(message);
            }
            else if (messageText.Contains("離") || messageText.Contains("りせき") || messageText.Contains("はなれ")
                || messageText.Contains("ちょっと") || messageText.Contains("ちょま"))
            {
                await Riseki(message);
            }
            else if (messageText.Contains("勝") || messageText.Contains("負"))
            {
                await Win(message);
            }
            else if (messageText.Contains("わかる") || messageText.Contains("せやな") || messageText.Contains("せやろ"))
            {
                await Seyaro(message);
            }
            else if (messageText.Contains("ろー") || messageText.Contains("やろう") || messageText.Contains("しよ"))
            {
                await Yarou(message);
            }
            else if (messageText.Contains("すご") || messageText.Contains("すげ"))
            {
                await Sugoi(message);
            }
            else if (messageText.Contains("うるさ") || messageText.Contains("だま") || messageText.Contains("黙"))
            {
                await Damare(message);
            }
            else if (messageText.Contains("か？") || messageText.Contains("ん？") || messageText.Contains("い？"))
            {
                await Question(message);
            }
            else if (messageText.Contains("おわ") || messageText.Contains("やめ"))
            {
                await Owaro(message);
            }
            else if (messageText.Contains("すご") || messageText.Contains("やめ"))
            {
                await Owaro(message);
            }
            else if (messageText.Contains("ｗｗ") || messageText.Contains("草") || messageText.Contains("わろ"))
            {
                await Warota(message);
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
        /// すごい
        /// </summary>
        /// <param name="msgParam"></param>
        /// <returns></returns>
        private async Task Sugoi(SocketUserMessage message)
        {
            await message.Channel.SendMessageAsync("え？すごい！？もう一回言って！");
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
            await message.Channel.SendMessageAsync("わかる―！俺もわかるわーぞれ！");
        }

        /// <summary>
        /// やろ
        /// </summary>
        /// <param name="msgParam"></param>
        /// <returns></returns>
        private async Task Yarou(SocketUserMessage message)
        {
            await message.Channel.SendMessageAsync("まじで！？俺もやる―！");
        }

        /// <summary>
        /// おはる
        /// </summary>
        /// <param name="msgParam"></param>
        /// <returns></returns>
        private async Task Oharu(SocketUserMessage message)
        {
            await message.Channel.SendMessageAsync("おはるさんのマネしまーす！\r\n「ココニ銃アルヨ(中国語)」");
        }

        /// <summary>
        /// きゃべつ
        /// </summary>
        /// <param name="msgParam"></param>
        /// <returns></returns>
        private async Task Cabagge(SocketUserMessage message)
        {
            await message.Channel.SendMessageAsync("きゃべつのマネしまーす！\r\n「あ。。。すみません。。。ほんま、すみません。。。」");
        }

        /// <summary>
        /// ばた
        /// </summary>
        /// <param name="msgParam"></param>
        /// <returns></returns>
        private async Task Bata(SocketUserMessage message)
        {
            await message.Channel.SendMessageAsync("ばたのマネしまーす！\r\n「ｲｰﾔツンヨーーーーーーーーｿﾚｯ」");
        }

        /// <summary>
        /// Wilkinson
        /// </summary>
        /// <param name="msgParam"></param>
        /// <returns></returns>
        private async Task Wilkinson(SocketUserMessage message)
        {
            await message.Channel.SendMessageAsync("Wilkinsonのマネしまーす！\r\n「これ↑ねぇ↓・・・マジめっちゃ強↑いんよ↓・・・！」");
        }

        /// <summary>
        /// Http
        /// </summary>
        /// <param name="msgParam"></param>
        /// <returns></returns>
        private async Task Http(SocketUserMessage message)
        {
            await message.Channel.SendMessageAsync("あ～！これ俺もしってる～！！");
        }

        /// <summary>
        /// 面白
        /// </summary>
        /// <param name="msgParam"></param>
        /// <returns></returns>
        private async Task Omosiro(SocketUserMessage message)
        {
            await message.Channel.SendMessageAsync("俺が1週間SMAPだった話する？");
        }

        /// <summary>
        /// アットマーク
        /// </summary>
        /// <param name="msgParam"></param>
        /// <returns></returns>
        private async Task AtMark(SocketUserMessage message)
        {
            if (!message.Content.Contains("@everyone"))
            {
                await message.Channel.SendMessageAsync("@everyone " + message.Author.Username + "さん、他に誰かやらんなら自分やりますよー？");
            }
        }



        /// <summary>
        /// 文字変換メソッド
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        private string StringConvert(string val)
        {
            string res = default;
            try
            {

                if (val.Contains("!"))
                {
                    res = val;
                }
                else
                {
                    res = Strings.StrConv(val.Trim(), VbStrConv.Hiragana | VbStrConv.Wide);
                }
                return res;
            }
            catch (Exception ex)
            {
                Console.WriteLine("不明なエラー文字列;{0}", val);
                return res;
            }
        }
    }
}
