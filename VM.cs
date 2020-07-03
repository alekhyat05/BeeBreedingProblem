using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BeeBreeds
{
    internal class VM : INotifyPropertyChanged
    {
        const int INITIAL_VALUE = 0;
        const int FIRST_SIDE = 1;
        const int SECOND_SIDE = 2;
        const int THIRD_SIDE = 3;
        const int FOURTH_SIDE = 4;
        const int FIFTH_SIDE = 5;
        const int FirstCell = 1;
        private string inputNumber;
        public string InputNumber
        {
            get { return inputNumber; }
            set { inputNumber = value; changed(); }
        }
        private bool calcBtnEnabled = false;
        public bool CalcBtnEnabled
        {
            get { return calcBtnEnabled; }
            set { calcBtnEnabled = value; changed(); }
        }
        private bool addBtnEnabled = true;
        public bool AddBtnEnabled
        {
            get { return addBtnEnabled; }
            set { addBtnEnabled = value; changed(); }
        }
        private bool resetBtnEnabled = false;
        public bool ResetBtnEnabled
        {
            get { return resetBtnEnabled; }
            set { resetBtnEnabled = value; changed(); }
        }
        public ObservableCollection<string> Input { get; set; } = new ObservableCollection<string>();
        public ObservableCollection<string> Output { get; set; } = new ObservableCollection<string>();
        public void addInput()
        {
            if (InputNumber == "0 0")
            {
                CalcBtnEnabled = true;
                ResetBtnEnabled = true;
                AddBtnEnabled = false;
            }
            else
            {
                Input.Add(InputNumber);
                Output.Clear();
                Data();
                InputNumber = string.Empty;
                
            }
        }
        public void Data()
        {
            try{

                foreach (string number in Input)
                {
                    string[] inputArray = number.Split(' ');
                    int[] HexagonArray = new int[2];
                    HexagonArray[0] = int.Parse(inputArray[0]);
                    HexagonArray[1] = int.Parse(inputArray[1]);
                    if (HexagonArray[0] > 10000 || HexagonArray[1]>10000)
                    {
                     // Input.RemoveAt(Input.Count - 1);//check
                        InputNumber =string.Empty;
                       
                    }
                    else
                    {
                        DistanceCalculation(HexagonArray[0], HexagonArray[1]);
                    }                    
                }
            }
            catch(Exception e)
            {

                InputNumber = string.Empty;
                 Input.RemoveAt(Input.Count - 1);
            }
        }
        public void DistanceCalculation(int firstValue, int secondValue)
        {
            int result = INITIAL_VALUE;
            if (firstValue == INITIAL_VALUE && secondValue == INITIAL_VALUE)
            {
                Output.Add(result.ToString());
            }
            else
            {
                Point p = new Point(INITIAL_VALUE, INITIAL_VALUE);
                p = CalculateCoordinates(firstValue);
                Point p1 = new Point(p.getX(), p.getY());
                p = CalculateCoordinates(secondValue);
                Point p2 = new Point(p.getX(), p.getY());
                int x = p1.getX() - p2.getX();
                int y = p1.getY() - p2.getY();
                if (x * y <= INITIAL_VALUE)
                    result = Math.Max(Math.Abs(x), Math.Abs(y));
                else
                    result = Math.Abs(x + y);
                Output.Add("The distance between " + firstValue + " and " + secondValue + " is: " + result.ToString());
            }
        }
        public static Point CalculateCoordinates(int hexagonNumber)
        {
            if (hexagonNumber == FirstCell)
            {
                return new Point(INITIAL_VALUE, INITIAL_VALUE);
            }
            int x;
            int y;
            int level = INITIAL_VALUE;
            while (THIRD_SIDE * (level - FIRST_SIDE) * level + FIRST_SIDE < hexagonNumber)
                level++;
            level--;
            hexagonNumber -= THIRD_SIDE * (level - FIRST_SIDE) * level + FIRST_SIDE;
            if (hexagonNumber <= level)
            {
                x = level;
                y = -hexagonNumber;
            }
            else if (hexagonNumber > level && hexagonNumber <= SECOND_SIDE * level)
            {
                x = SECOND_SIDE * level - hexagonNumber;
                y = -level;
            }
            else if (hexagonNumber > SECOND_SIDE * level && hexagonNumber <= THIRD_SIDE * level)
            {
                x = SECOND_SIDE * level - hexagonNumber;
                y = -level - x;
            }
            else if (hexagonNumber > THIRD_SIDE * level && hexagonNumber <= FOURTH_SIDE * level)
            {
                x = -level;
                y = hexagonNumber - THIRD_SIDE * level;
            }
            else if (hexagonNumber > FOURTH_SIDE * level && hexagonNumber <= FIFTH_SIDE * level)
            {
                x = hexagonNumber - FIFTH_SIDE * level;
                y = level;
            }
            else
            {
                x = hexagonNumber - FIFTH_SIDE * level;
                y = level - x;
            }
            Point p = new Point(x, y);
            return p;
        }
        public void Reset()
        {
            Input.Clear();
            Output.Clear();
            InputNumber = string.Empty;
            AddBtnEnabled = true;
            ResetBtnEnabled = false;
            CalcBtnEnabled = false;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void changed([CallerMemberName] string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
