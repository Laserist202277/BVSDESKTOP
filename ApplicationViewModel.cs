//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System;
using System.Linq;
using WPFDEMONSTRATIONAPP;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;

namespace MVVM
{

    public class ApplicationViewModel : INotifyPropertyChanged
    {
        IFileService fileService;
        IDialogCSVInterface dialogCSV;

        public ApplicationViewModel(IDialogCSVInterface dialogCSV, IFileService fileService)
        {
            this.dialogCSV = dialogCSV;
            this.fileService = fileService;

            Notess = new ObservableCollection<Notes>
            {
                new Notes ("06.08.2021/14:07", "USD", 710, 100, "*12356sdf43", "SLOT", "V02", "ATM"),
                new Notes ("06.08.2021/14:06", "RUB", 810, 500, "*12356gjhk43", "SLOT", "V02", "FIT"),
                new Notes ("01.08.2021/15:01", "EUR", 810, 50, "*12345fgh463", "SLOT", "V02", "UNF"),
                new Notes ("12.08.2021/14:08", "RUB", 810, 1000, "*12345ghf63", "SLOT", "V02", "ATM")
            };
        }

        private Notes selectedNotes;
        public ObservableCollection<Notes> Notess { get; set; }

        // команда добавления нового объекта
        private RelayCommand addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??
                  (addCommand = new RelayCommand(obj =>
                  {
                      //Notes Notes = new Notes { Id=5, Data_Time = "06.08.2021/14:01", Currency = "USD", Nominal = "500", Dest = "SLOT", Serial_Number = "*12343", Version = "V02" };
                      Random rnd = new Random();
                      int nomrand = rnd.Next();
                      int banknoterand = rnd.Next();
                      int attributerand = rnd.Next()%3;
                      string curr;
                      string str_attribute;
                      if(nomrand %3==0)
                      {
                          nomrand = 100;
                      }
                      else if (nomrand %5==0)
                      {
                          nomrand = 500;
                      }
                      else if(nomrand %7==0)
                      {
                          nomrand = 5000;
                      }
                      else { nomrand = 1000; }

                      if(banknoterand %2 == 0 )
                      {
                          curr = "RUB";
                      }
                      else if(banknoterand % 5 == 0)
                      {
                          curr = "USD";
                      }
                      else
                      {
                          curr = "EUR";
                      }

                      if(attributerand == 0)
                      {
                          str_attribute = "ATM";
                      } 
                      else if(attributerand == 1)
                      {
                          str_attribute = "FIT";
                      }
                      else { str_attribute = "UNF"; }


                      Notes Notes = new Notes("06.08.2021/14:01", curr, 810, nomrand, "*12343", "SLOT", "V02", str_attribute);
                      Notess.Add(Notes);
                      SelectedNotes = Notes;

                      // Добавляем новую запись в базу данных
                      DbContextFactory contextFactory = (Application.Current as App).GetHost().Services.GetRequiredService<DbContextFactory>();
                      using (NotesDbContext context = contextFactory.CreateDbContext())
                      {
                          // Создаём новую запись в базе данных
                          var notes = new WPFDEMONSTRATIONAPP.Notes { Name = curr, Nominal = nomrand, SerialNo = "hahaha2", Dest= "SLOT", Version = "V02", uAttribute = str_attribute, date_time = "06.08.2021/14:01" };
                          context.Add<WPFDEMONSTRATIONAPP.Notes>(notes);
                          context.SaveChanges();
                      }
                  }));
            }
        }

        public Notes SelectedNotes
        {
            get { return selectedNotes; }
            set
            {
                selectedNotes = value;
                OnPropertyChanged("SelectedNotes");
            }
        }
        private RelayCommand saveCommand;
        public RelayCommand SaveCommand
        {
            get
            {
                return saveCommand ??
                  (saveCommand = new RelayCommand(obj =>
                  {
                      try
                      {
                          if (dialogCSV.SaveFileDialog() == true)
                          {
                              fileService.Save(dialogCSV.FilePath, Notess.ToList());
                              dialogCSV.ShowMessage("Файл сохранен");
                          }
                      }
                      catch (Exception ex)
                      {
                          dialogCSV.ShowMessage(ex.Message);
                      }
                  }));
            }
        }

        //public void GenerateData()
        //{
        //    // Сбрасываем предыдущий результат выполнения операции
        //    Batch.Clear();

        //    // Параметры вызова: номинал, количество, код валюты...
        //    // Вместо имени валюты выводим текст из строки ввода
        //    Batch.Add(new Notes { Id = 0, Data_Time = "09.07.2021/14:01", Currency = "RUB", Nominal = "500", Dest = "SLOT", Serial_Number = "*12343", Version = "V02" });
        //    Batch.Add(new Notes {Id= 1, Data_Time = "09.07.2021/14:11", Currency = "USD", Nominal = "100", Dest = "SLOT", Serial_Number = "M5657", Version = "V02" });
        //    Batch.Add(new Notes{Id = 1, Data_Time = "09.07.2021/14:11", Currency = "USD", Nominal = "100", Dest = "SLOT", Serial_Number = "M5657", Version = "V02" });
        //}


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = null)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        public ObservableCollection<Notes> Batch { get; set; }


    }
}