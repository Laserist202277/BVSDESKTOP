
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace MVVM
{
    public class JsonFileService : IFileService
    {
        public List<Notes> Open(string filename)
        {
            List<Notes> notes = new List<Notes>();
            DataContractJsonSerializer jsonFormatter =
                new DataContractJsonSerializer(typeof(List<Notes>));
            using (FileStream fs = new FileStream(filename, FileMode.OpenOrCreate))
            {
                notes = jsonFormatter.ReadObject(fs) as List<Notes>;
            }

            return notes;
        }

        public void Save(string filename, List<Notes> notesList)
        {
            using (StreamWriter myOutputStream = new StreamWriter("Myfile.csv"))
            {
                foreach (var item in notesList)
                {
                    StringBuilder buf = new StringBuilder();
                    buf.Append("\"" + item.Id + "\";");
                    buf.Append("\"" + item.Name + "\";");
                    buf.Append("\"" + item.Nominal + "\";");
                    buf.Append("\"" + item.Serial_Number + "\";");
                    buf.Append("\"" + item.Data_Time + "\";");
                    buf.Append("\"" + item.Dest + "\";");
                    buf.Append("\"" + item.Version + "\";");
                    buf.Append("\"" + item.Currency_Code + "\";");
                    buf.Append("\"" + item.Attribute + "\";");

                    myOutputStream.WriteLine(buf.ToString());
                    
                }
            }

        }
    }
}
