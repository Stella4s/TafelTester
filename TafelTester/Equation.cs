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
        private int _numA, _numB, _numC, _NumAntwoord;
        private string _strAntwoord;
        private bool _isCorrect, _isAnswered;

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
        public int NumAntwoord
        {
            get { return _NumAntwoord; }
            set
            {
                _NumAntwoord = value;
                OnPropertyChanged();
            }
        }
        public bool IsCorrect
        {
            get { return _isCorrect; }
            set
            {
                _isCorrect = value;
                OnPropertyChanged();
            }
        }
        public bool IsAnswered
        {
            get { return _isAnswered; }
            set
            {
                _isAnswered = value;
                OnPropertyChanged();
            }
        }
        public string StrAntwoord
        {
            get { return _strAntwoord; }
            set
            {
                _strAntwoord = value;
                //To ensure no crash happens if user fills in numbers.
                try
                {
                    _NumAntwoord = Convert.ToInt32(StrAntwoord);
                }
                catch
                {
                    _NumAntwoord = 0;
                    StrAntwoord = null;
                }
                OnPropertyChanged();
            }
        }
        #endregion

        public void GetSum()
        {
            NumC = NumA * NumB;
        }
    
        public void CheckAntwoord()
        {
            if (NumAntwoord == 0)
                IsAnswered = false;
            else
            {
                IsAnswered = true;
                if (NumAntwoord == NumC)
                    IsCorrect = true;
                else
                    IsCorrect = false;
            }
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
