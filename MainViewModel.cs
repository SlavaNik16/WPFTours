using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;

namespace WPFTours
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public static Tourse tourse;
        public MainViewModel()
        {
            tourse = new Tourse
            {
                direction = Direction.Турция,
                DateDeparture = DateTime.Now.AddDays(5),
                NumberNight = 1,
                CostVac = 100,
                NumberVac = 1,
                Surcharges = 0,
                TotalAmount = 0,
            };
            Tourses = new ObservableCollection<Tourse>();
            SelectedIndex = -1;
            OutpurStatus();
        }

        #region AddButton

        private RelayCommand addButton;
        public RelayCommand AddButton
        {
            get => addButton ?? (addButton = new RelayCommand(AddButtonExecute));
        }
        private void AddButtonExecute(object x)
        {   
            var m = new MainViewModelTourse((Tourse)tourse.Clone());
            if (m.ShowModal() == true) {
                Tourses.Add(m.tourse1);
                OutpurStatus();
               
            } 
        }
        
        #endregion
        #region ChangeButton

        private RelayCommand changeButton;
        public RelayCommand ChangeButton
        {
            get => changeButton ?? (changeButton = new RelayCommand(changeButtonExecute,ChangeButtonCanExecute));
        }
        private void changeButtonExecute(object x)
        {
            var index = SelectedIndex;
            var m = new MainViewModelTourse(Tourses,SelectedIndex);
            if (m.ShowModal() == true)
            {
                
                Tourses.Insert(SelectedIndex, m.tourse1);
                Tourses.RemoveAt(SelectedIndex);
                OutpurStatus();
                SelectedIndex = index;

            }
        }
        private bool ChangeButtonCanExecute(object x)
        {
            return (SelectedIndex >= 0);
        }

        #endregion
        #region DeleteButton

        private RelayCommand deleteButton;
        public RelayCommand DeleteButton
        {
            get => deleteButton ?? (deleteButton = new RelayCommand(_=> 
            {
                if (MessageBox.Show($"Вы действительно хотите удалить " +
                    $"{Tourses[SelectedIndex].direction} в {Tourses[SelectedIndex].DateDeparture:d} !","Удаление",
                    MessageBoxButton.OKCancel, MessageBoxImage.Information) == MessageBoxResult.OK)
                {
                    Tourses.RemoveAt(SelectedIndex);
                    OutpurStatus();
                }
            }, DeleteButtonCanExecute));
        }
        private bool DeleteButtonCanExecute(object x)
        {
            return SelectedIndex >= 0;
        }

        #endregion

        #region SelectedIndex

        private int selectedIndex;
        public int SelectedIndex
        {
            get => selectedIndex;
            set
            {
                selectedIndex = value;
                OnPropertyChanged();
            }
        }

        #endregion
        #region CountTours

        private string countTours;
        public string CountTours
        {
            get => countTours;
            set
            {
                countTours = value;
                OnPropertyChanged();
            }
        }

        #endregion 
        #region AmountTotal

        private string amountTotal;
        public string AmountTotal
        {
            get => amountTotal;
            set
            {
                amountTotal = value;
                OnPropertyChanged();
            }
        }

        #endregion
        #region CountToursWithSur

        private string countToursWithSur;
        public string CountToursWithSur
        {
            get => countToursWithSur;
            set
            {
                countToursWithSur = value;
                OnPropertyChanged();
            }
        }

        #endregion
        #region AmountTotalWithSur

        private string amountTotalWithSur;
        public string AmountTotalWithSur
        {
            get => amountTotalWithSur;
            set
            {
                amountTotalWithSur = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Help

        private RelayCommand help;
        public RelayCommand Help
        {
            get => help ?? (help = new RelayCommand(x => 
            MessageBox.Show("Работу выполнил Николаев В.А ИП-20-3","Информация",
                MessageBoxButton.OK,MessageBoxImage.Information)));
        }

        #endregion
        #region Exit

        private RelayCommand exit;
        public RelayCommand Exit
        {
            get => exit ?? (exit = new RelayCommand(x => Application.Current.Shutdown()));
        }

        #endregion
       
        public ObservableCollection<Tourse> Tourses { get; set; }
        public void OutpurStatus()
        {
            CountTours = $"Кол - во туров: {Tourses.Count} |";
            var total = Tourses.Sum(x => x.TotalAmount);
            AmountTotal = $"Общая сумма: {total} |";
            var SurCount = Tourses.Where(x => x.Surcharges != 0).Count();
            CountToursWithSur = $"Кол-во туров с доплатами: {SurCount} |";
            var AmountTotalSur = Tourses.Sum(y => y.Surcharges);
            AmountTotalWithSur = $"Общая сумма доплат: {AmountTotalSur}";
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}
