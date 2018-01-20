using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Cut;


namespace Cut
{
    static public class SaveAndLoad
    {
        static public async Task SaveTextAsync(string filename, string text)
        {
            using (FileStream SourceStream = File.Open(HttpRuntime.AppDomainAppPath +"db\\"+ filename, FileMode.Create))
            {
                UnicodeEncoding uniencoding = new UnicodeEncoding();
                byte[] result = uniencoding.GetBytes(text);
                await SourceStream.WriteAsync(result, 0, result.Length);
            }
        }

        static public async Task AppendTextAsync(string filename, string text)
        {
            using (FileStream SourceStream = File.Open(HttpRuntime.AppDomainAppPath + "db\\" + filename, FileMode.Append))
            {
                UnicodeEncoding uniencoding = new UnicodeEncoding();
                byte[] result = uniencoding.GetBytes(text);
                await SourceStream.WriteAsync(result, 0, result.Length);
            }
        }

        static public async Task<string> LoadTextAsync(string filename)
        {
            byte[] result;
            using (FileStream SourceStream = File.Open(HttpRuntime.AppDomainAppPath + "db\\" + filename, FileMode.Open))
            {
                result = new byte[SourceStream.Length];
                await SourceStream.ReadAsync(result, 0, (int)SourceStream.Length);
            }
            //return string(result);
            return System.Text.Encoding.Unicode.GetString(result);
        }

        static public  void SaveText(string filename, string text)
        {
            using (FileStream SourceStream = File.Open(HttpRuntime.AppDomainAppPath + "db\\" + filename, FileMode.Create))
            {
                UnicodeEncoding uniencoding = new UnicodeEncoding();
                byte[] result = uniencoding.GetBytes(text);
                SourceStream.Write(result, 0, result.Length);
            }
        }

        static public void AppendText(string filename, string text)
        {
            using (FileStream SourceStream = File.Open(HttpRuntime.AppDomainAppPath + "db\\" + filename, FileMode.Append))
            {
                UnicodeEncoding uniencoding = new UnicodeEncoding();
                byte[] result = uniencoding.GetBytes(text);
                SourceStream.Write(result, 0, result.Length);
            }
        }

        static public string LoadText(string filename)
        {
            byte[] result;
            using (FileStream SourceStream = File.Open(HttpRuntime.AppDomainAppPath + "db\\" + filename, FileMode.Open))
            {
                result = new byte[SourceStream.Length];
                SourceStream.Read(result, 0, (int)SourceStream.Length);
            }
            //return string(result);
            return System.Text.Encoding.Unicode.GetString(result);
        }
        static public bool FileExists(string filename)
        {
            return File.Exists(HttpRuntime.AppDomainAppPath + "db\\" + filename);
        }
        
    }
}
