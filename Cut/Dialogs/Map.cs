using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Nito.AsyncEx;
namespace Cut
{
    public class Maps
    {
        //public SortedDictionary<string, string> data = new SortedDictionary<string, string>(StringComparer.CurrentCultureIgnoreCase);
        //public SortedDictionary<string, string> ok = new SortedDictionary<string, string>(StringComparer.CurrentCultureIgnoreCase);
        public Trie<string> data = new Trie<string>();
        public Trie<string> ok = new Trie<string>();
        private AsyncReaderWriterLock rwl = new AsyncReaderWriterLock();
        public string file_name;
        public void add(string key, string value = "")
        {
            using (var lk = rwl.WriterLock())
            {
                data.add(key, value);
                //data[key] = value;
                if (file_name != null)
                {
                    if (exists_ok_nolock(key))
                        SaveAndLoad.AppendText(file_name, "*" + val_ok_nolock(key) + "," + val_nolock(key) + "\n");
                    else
                        SaveAndLoad.AppendText(file_name, key + "," + val_nolock(key) + "\n");
                }
            }
        }
        public void add_ok(string key, string value = "")
        {
            using (var lk = rwl.WriterLock())
            {
                ok.add(key, value);
                //ok[key] = value;
                if (file_name != null)
                {
                    if (exists_ok_nolock(key))
                        SaveAndLoad.AppendText(file_name, "*" + val_ok_nolock(key) + "," + val_nolock(key) + "\n");
                    else
                        SaveAndLoad.AppendText(file_name, key + "," + val_nolock(key) + "\n");
                }
            }
        }
        public void remove(string key)
        {
            using (var lk = rwl.WriterLock())
            {
                data.remove(key);
                ok.remove(key);
                //data.Remove(key);
                //ok.Remove(key);
                if (file_name != null)
                {
                    SaveAndLoad.AppendText(file_name, "$" + key + "\n");
                }
            }
        }
        public bool exists(string key)
        {
            using (var lk = rwl.ReaderLock())
            {
                bool res = data.exists(key);
                return res;
            }
        }
        public bool exists_ok(string key)
        {
            using (var lk = rwl.ReaderLock())
            {
                return exists_ok_nolock(key);
            }
        }
        private bool exists_ok_nolock(string key)
        {
            bool res = ok.exists(key);
            return res;
        }
        public string val(string key)
        {
            using (var lk = rwl.ReaderLock())
            {
                return val_nolock(key);
            }
        }
        private string val_nolock(string key)
        {
            var res = data.find(key);
            if (res == null)
                return null;
            return res.val.Value;
        }
        public string this[string i]
        {
            get { return val(i); }
            set { add(i, value); }
        }
        public string val_ok(string key)
        {
            using (var lk = rwl.ReaderLock())
            {
                return val_ok_nolock(key);
            }
        }
        private string val_ok_nolock(string key)
        {
            using (var lk = rwl.ReaderLock())
            {
                var res = ok.find(key);
                if (res == null)
                    return null;
                return res.val.Value;
            }
        }
        public void clear()
        {
            using (var lk = rwl.WriterLock())
            {
                data = new Trie<string>();
                ok = new Trie<string>();
            }
        }
    }
}
