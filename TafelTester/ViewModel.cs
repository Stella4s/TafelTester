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
        private Random getRandom;
        private readonly int somAantal = 6;
        private int _tafelNummer;
        private int[] _tafelOpties, _RandomNumArr;
        private bool _ShowAnswerCheck;


        public ViewModel()
        {
            getRandom = new Random();
            RandomNumArr = new int[somAantal];
            InitializeEquations();
            ArrTafelOpties = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
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
        public int[] ArrTafelOpties
        {
            get { return _tafelOpties; }
            set
            {
                _tafelOpties = value;
                OnPropertyChanged();
            }
        }
        public int[] RandomNumArr
        {
            get { return _RandomNumArr; }
            set
            {
                _RandomNumArr = value;
                OnPropertyChanged();
            }
        }
        public bool ShowAnswerCheck
        {
            get { return _ShowAnswerCheck; }
            set
            {
                _ShowAnswerCheck = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region methods 
        /// <summary>
        /// For initializing the Equations array.
        /// </summary>
        public void InitializeEquations()
        {
            Equations = new Equation[somAantal];
            for (int i = 0; i < somAantal; i++)
            {
                Equations[i] = new Equation { NumA = 0, NumB = 0, NumC = 0 };
            }
        }
        /// <summary>
        /// Calls FillRandomArray method, then sets NumA and NumB before calling getSum to get NumC.
        /// </summary>
        public void GetEquations()
        {
            FillRandomArray();
            for (int i = 0; i < somAantal; i++)
            {
                Equations[i].NumA = TafelNummer;
                Equations[i].NumB = RandomNumArr[i];
                Equations[i].GetSum();
            }
        }
        public void GetAnswers()
        {
            ShowAnswerCheck = true;
            foreach (Equation equation in Equations)
            {
                equation.CheckAntwoord();
                if (equation.IsAnswered == false)
                    ShowAnswerCheck = false;
            }
        }

        //Variables used in this method.
        bool isSame, repeat;
        /// <summary>
        /// Generates random numbers to fill the array the lenght of somAantal.
        /// Using a for-loop that only checks the numbers in RandomNummArr prior to the generated number. 
        /// Which cuts down on unnecessary checking of numbers after the generated number that will be changed anyways.
        /// Whilst the do while loop ensures the process will be at least checked once and repeated if the number generated was already used.
        /// </summary>
        public void FillRandomArray()
        {
            isSame = false; repeat = true;
            //Creates a temporary list from 1 to 10 that can have items removed as they are picked.
            //To both ensure randomness, without endless looping to check if the random array already contains said value.
            List<int> tempList = new List<int>(ArrTafelOpties);
            for (int i = 0; i < somAantal; i++)
            {
                isSame = false;
                do
                {
                    int numX = getRandom.Next(1, tempList.Count);
                    for (int j = 0; j < i; j++)
                    {
                        if (tempList[numX] == RandomNumArr[j])
                        {
                            isSame = true;
                            break;
                        }
                    }
                    if (!isSame)
                    {
                        repeat = false;
                        RandomNumArr[i] = tempList[numX];
                        tempList.RemoveAt(numX);
                    }
                    else
                        repeat = true;
                } while (repeat);
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
        private ICommand _GetAnswersCmd;
        public ICommand GetAnswersCmd
        {
            get
            {
                if (_GetAnswersCmd == null)
                {
                    _GetAnswersCmd = new RelayCommand(
                        p => GetAnswers());
                }
                return _GetAnswersCmd;
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
