using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TafelTester
{
    public class Equation : INotifyPropertyChanged
    {
        private int _numA, _numB, _numC;
        private Random getRandom;

        public Equation()
        {
            getRandom = new Random();
        }

        #region properties
        public int NumA
        {
            get { return _numA; }
            set
            {
                _numA = value;
                OnPropertyChanged();
            }
        }

        public int NumB
        {
            get { return _numB; }
            set
            {
                _numB = value;
                OnPropertyChanged();
            }
        }

        public int NumC
        {
            get { return _numC; }
            set
            {
                _numC = value;
                OnPropertyChanged();
            }
        }
        #endregion

        public void getSum()
        {
            NumB = getRandom.Next(1, 11);
            NumC = NumA * NumB;
        }


        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion
    }
}
