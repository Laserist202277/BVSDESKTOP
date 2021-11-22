//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace WPFDEMONSTRATIONAPP
//{
//    class Notes
//    {
//    }
//}
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System;
using System.Data.Entity;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore;

namespace MVVM
{
    public class Notes : INotifyPropertyChanged
    {
        private static int m_Counter = 1;

        public Notes(string data_time, string name, int currency_code, int nominal, string serial_number, string dest, string version, string attribute)
        {
            this.Name = name;
            this.Currency_Code =currency_code;
            this.Nominal = nominal;
            this.Version = version;
            this.Serial_Number =serial_number;
            this.Data_Time =data_time;
            this.Dest =dest;
            this.Attribute = attribute;

            Id = m_Counter++;
        }

        public int Id
        {
            get;
            set;
        }
        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }
        //public Currency Currency { get; set; }
        private int nominal;
        public int Nominal
        {
            get { return nominal; }
            set
            {
                nominal = value;
                OnPropertyChanged("Version");
            }

        }

        private string version;
        public string Version
        {
            get { return version; }
            set
            {
                version = value;
                OnPropertyChanged("Version");
            }

        }
        private string serial_number;
        public string Serial_Number
        {
            get { return serial_number; }
            set
            {
                serial_number = value;
                OnPropertyChanged("Serial_Number");
            }
        }
        private string data_time;
        public string Data_Time
        {
            get { return data_time; }
            set
            {
                data_time = value;
                OnPropertyChanged("Data_Time");
            }
        }
        private string dest;
        public string Dest
        {
            get { return dest; }
            set
            {
                dest = value;
                OnPropertyChanged("Dest");
            }
        }
        private int currency_code;
        public int Currency_Code
        {
            get { return currency_code; }
            set
            {
                currency_code = value;
                OnPropertyChanged("CurrencyCode");
            }
        }

        private string attribute;
        public string Attribute
        {
            get { return attribute; }
            set
            {
                attribute = value;
                OnPropertyChanged("Attribute");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
    //public class Currency
    //{
    //    public Currency(int currency_code, string name)
    //    {
    //        CurrencyCode = currency_code;
    //        CurrencyName = name;
    //    }

    //    public int CurrencyCode { get; }

    //    public string CurrencyName { get; }
    //}
    //}

 

