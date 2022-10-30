using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;

namespace WPFTours
{
    public class MainViewModelTourse : INotifyPropertyChanged
    {
        public Tourse tourse1;
        public MainViewModelTourse() { }
        public MainViewModelTourse(Tourse tourse) {
            tourse1 = tourse;  
            direct = new ObservableCollection<string>();
            foreach (Direction dir in Enum.GetValues(typeof(Direction)))
            {
                string item = dir.ToString();
                Direct.Add(item);
            }
            SelectedIndex = 0;
            Time = tourse1.DateDeparture;
            NumberNight = tourse1.NumberNight;
            CostVac = tourse1.CostVac;
            NumberVac = tourse1.NumberVac;
            Surcharges = tourse1.Surcharges;
            form = new InfoTourseForm();
            form.DataContext = this;
        }
        public MainViewModelTourse(ObservableCollection<Tourse> tourse, int index)
        {
            tourse1 = tourse[index];
            direct = new ObservableCollection<string>();
            var count = 0;
            foreach (Direction dir in Enum.GetValues(typeof(Direction)))
            {
                string item = dir.ToString();
                if (tourse1.direction.ToString() == item) SelectedIndex = count;
                count++;
                Direct.Add(item);
            }
            SelectedItem = tourse1.direction;
            Time = tourse1.DateDeparture;
            NumberNight = tourse1.NumberNight;
            CostVac = tourse1.CostVac;
            NumberVac = tourse1.NumberVac;
            Wi_Fi = tourse1.Wi_Fi;
            Surcharges = tourse1.Surcharges;
            form = new InfoTourseForm();
            form.DataContext = this;
        }

        #region Save
        private RelayCommand save;
        public RelayCommand Save
        {
            
            get => save ?? (save = new RelayCommand(x =>
            {
                 tourse1.TotalAmount = 0;
                 tourse1.TotalAmount += (tourse1.NumberNight * tourse1.NumberVac * tourse1.CostVac) + tourse1.Surcharges;
                 form.DialogResult = true;
                 form.Close();
            }
            ));
 
        }
        
        public bool? ShowModal()
        {
            return form.ShowDialog();
        }
        private InfoTourseForm form { get; set; }




        #endregion

        #region Direction

        #region Direct
        private ObservableCollection<string> direct;

        public ObservableCollection<string> Direct
        {
            get => direct;
            set
            {
                direct = value;
                
                OnPropertyChanged();
            }
        }

        #endregion
        #region SelectedItem

        private Direction selectedItem;
        public Direction SelectedItem
        {
            get => selectedItem;
            set
            { 
                selectedItem = value;
                tourse1.direction = selectedItem;
                OnPropertyChanged();
            }
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

        #endregion
        #region DatePick

        private DateTime time;

        public DateTime Time
        {
            get => time;
            set
            {
                time = value;
                tourse1.DateDeparture = time;
                OnPropertyChanged();
            }
        }

        #endregion
        #region NumberNight

        private int numNight;

        public int NumberNight
        {
            get => numNight;
            set
            {
                numNight = value;
                tourse1.NumberNight = numNight;
                OnPropertyChanged();
            }
        }

        #endregion
        #region CostVac

        private decimal costVac;

        public decimal CostVac
        {
            get => costVac;
            set
            {
                costVac = value;
                tourse1.CostVac = costVac;
                OnPropertyChanged();
            }
        }

        #endregion
        #region Wi_Fi

        private bool wi_fi;

        public bool Wi_Fi
        {
            get => wi_fi;
            set
            {
                wi_fi = value;
                tourse1.Wi_Fi = wi_fi;
                OnPropertyChanged();
            }
        }

        #endregion
        #region NumberVac

        private int numVac;

        public int NumberVac
        {
            get => numVac;
            set
            {
                numVac = value;
                tourse1.NumberVac = numVac;
                OnPropertyChanged();
            }
        }

        #endregion
        #region Surcharges

        private decimal surcharges;

        public decimal Surcharges
        {
            get => surcharges;
            set
            {
                surcharges = value;
                tourse1.Surcharges = surcharges;
                OnPropertyChanged();
            }
        }

        #endregion

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));

    }
}
