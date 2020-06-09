using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TafelTester
{
    public class ViewModel : INotifyPropertyChanged
    {
        private Equation[] _equations;
        private readonly int somAantal = 6;
        private int _tafelNummer;
        private int[] _tafelOpties;

        public ViewModel()
        {
            Equations = new Equation[somAantal];
            TafelOpties = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        }

        #region properties
        public Equation[] Equations
        {
           get { return _equations; }
            set
            {
                _equations = value;
                OnPropertyChanged();
            }
        }
        public int TafelNummer
        {
            get { return _tafelNummer; }
            set
            {
                _tafelNummer = value;
                OnPropertyChanged();
            }
        }
        public int[] TafelOpties
        {
            get { return _tafelOpties; }
            set
            {
                _tafelOpties = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region methods 
        public void GetEquations()
        {
            foreach (Equation equation in Equations)
            {
                equation.NumA = TafelNummer;
            }
        }
        #endregion

        #region command
        private ICommand _GetEquationsCmd;
        public ICommand GetEquationsCmd
        {
            get
            {
                if (_GetEquationsCmd == null)
                {
                    _GetEquationsCmd = new RelayCommand(
                        p => GetEquations());
                }
                return _GetEquationsCmd;
            }
        }
        #endregion

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion
    }
}
