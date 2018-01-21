using System;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace Cut.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);

            return Task.CompletedTask;
        }
        private void Init() {

            int data_version = 18;

            //await Voc.InitAsync();
            
            if (!SaveAndLoad.FileExists("setting.txt"))
                SaveAndLoad.SaveText("setting.txt", "");

            Voc.setting = Voc.GetDoc("setting", false);

            if (!Voc.setting.exists("website"))
                Voc.setting.add("website", "http://joe59491.azurewebsites.net");
            if (!Voc.setting.exists("sound_url"))
                Voc.setting.add("sound_url", "http://dictionary.reference.com/browse/");
            if (!Voc.setting.exists("sound_url2"))
                Voc.setting.add("sound_url2", "http://static.sfdict.com/staticrep/dictaudio");
            if (!Voc.setting.exists("sound_type"))
                Voc.setting.add("sound_type", ".mp3");
            if (!Voc.setting.exists("data_version"))
                Voc.setting.add("data_version", "0");
            Voc.SavingSetting();

            if (!SaveAndLoad.FileExists("favorite.txt"))
                SaveAndLoad.SaveText("favorite.txt", "");
            Voc.favorite = Voc.GetDoc("favorite", false);
            if (!SaveAndLoad.FileExists("words.txt") || int.Parse(Voc.setting["data_version"]) < data_version)
                Voc.DumpAppFile("words.txt");
            if (!SaveAndLoad.FileExists("root.txt") || int.Parse(Voc.setting["data_version"]) < data_version)
                Voc.DumpAppFile("root.txt");
            if (!SaveAndLoad.FileExists("prefix.txt") || int.Parse(Voc.setting["data_version"]) < data_version)
                Voc.DumpAppFile("prefix.txt");
            if (!SaveAndLoad.FileExists("suffix.txt") || int.Parse(Voc.setting["data_version"]) < data_version)
                Voc.DumpAppFile("suffix.txt");
            if (!SaveAndLoad.FileExists("note.txt") || int.Parse(Voc.setting["data_version"]) < data_version)
                Voc.DumpAppFile("note.txt");
            if (int.Parse(Voc.setting["data_version"]) < data_version)
                Voc.setting["data_version"] = data_version.ToString();

            Voc.words =Voc.GetDoc("words", true);
            var tmp = Voc.words.val("apple");
            foreach (var x in Voc.words.data)
            {
                var y = x;
            }

            Voc.root = Voc.GetDoc("root", true);

            Voc.prefix = Voc.GetDoc("prefix", true);

            Voc.suffix = Voc.GetDoc("suffix", true);

            Voc.note = Voc.GetDoc("note", true);
        }
        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            //Voc.inited_rwl.EnterReadLock();
            //Voc.inited_rwl.ExitReadLock();
            using (var lk = await Voc.inited_rwl.WriterLockAsync())
            {
                bool inited = Voc.inited;
                if (!inited)
                {
                    Init();
                    Voc.inited = true;
                }
            }
            var activity = await result as Activity;

            // calculate something for us to return
            int length = (activity.Text ?? string.Empty).Length;

            // return our reply to the user
            //Voc.Show2(activity.Text);
            //string res = "";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.AppendLine($"## {activity.Text}");
            sb.AppendLine(new string('-', 20));
            foreach(var x in Voc.Show(activity.Text))
            {
                //res += x+" ";
                sb.AppendLine($"**{x.Item2}**");
                sb.AppendLine($"    {x.Item1}");
                //await context.PostAsync(x);
            }
            var s = sb.ToString().Replace("\n", "<br/>").Replace("(",System.Web.HttpUtility.HtmlEncode("("))/*.Replace(")","\\)").Replace("[","\\[").Replace("]","\\]")*/;
            //s = System.Web.HttpUtility.UrlEncode(s);
            System.Diagnostics.Trace.WriteLine(s);
            await context.PostAsync(s);
            //await context.PostAsync($"You sent {activity.Text} which is {res} !");
            //await context.PostAsync(HttpRuntime.AppDomainAppPath + "db\\");
            
            //await context.PostAsync($"You sent {activity.Text} which is {activity.Text} ");

            context.Wait(MessageReceivedAsync);
        }
    }
}