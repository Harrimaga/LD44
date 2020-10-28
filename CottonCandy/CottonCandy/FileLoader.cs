using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace CottonCandy
{
    public class FileLoader
    {
        public static T Read<T>(string path)
        {
            using (FileStream reader = new FileStream(path, FileMode.Open))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                return (T)formatter.Deserialize(reader);
            };
        }

        public static void Write(object obj, string path)
        {
            try
            {
                using (FileStream writer = new FileStream(path, FileMode.Create))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(writer, obj);
                };
            }
            catch(Exception e)
            {
                if(e is DirectoryNotFoundException)
                {
                    Directory.CreateDirectory("Levels");
                    try
                    {
                        Write(obj, path);
                        return;
                    }
                    catch(Exception e2)
                    {
                        WriteText("[ERROR " + DateTime.Now.ToLongTimeString() + " at FileLoader.cs->Write()] Exception:" + e.Message, "log.txt");
                        WriteText("[ERROR " + DateTime.Now.ToLongTimeString() + " at FileLoader.cs->Write()] Exception:" + e2.Message, "log.txt");
                        return;
                    }
                }
                WriteText("[ERROR " + DateTime.Now.ToLongTimeString() + " at FileLoader.cs->Write()] Exception:" + e.Message, "log.txt");
                if(!(e is IOException||e is SerializationException))
                {
                    throw;
                }
            }
            
        }

        public static bool FileExists(string path)
        {
            return File.Exists(path) && new FileInfo(path).Length != 0;
        }

        public static void WriteText(string text, string path)
        {
            if (!FileExists(path))
            {
                using (StreamWriter streamWriter = new StreamWriter(File.Create(path)))
                {
                    streamWriter.WriteLine(text);
                };
            }
            else
            {
                using (StreamWriter streamWriter = new StreamWriter(path, true))
                {
                    streamWriter.WriteLine(text);
                };
            }

        }
    }
}
