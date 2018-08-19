using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private double _lastNumber, _result;
        int selectedValue;
        private SelectedOperator selectedOperator;

        public MainWindow()
        {
            
            InitializeComponent();
            BtnAc.Click += BtnAcOnClick;
            BtnNegative.Click += BtnNegative_Click;
            BtnPercent.Click += BtnPercent_Click;
            BtnResult.Click += BtnResult_Click;
        }

        private void BtnResult_Click(object sender, RoutedEventArgs e)
        {
             double newNumber;

            if (double.TryParse(LblResult.Content.ToString(), out newNumber))
            {
                switch (selectedOperator)
                {
                    case SelectedOperator.Addition:
                        _result = SimpleMath.Addition(_lastNumber, newNumber);
                        break;
                    case SelectedOperator.Subtraction:
                        _result = SimpleMath.Subtraction(_lastNumber, newNumber);
                        break;
                    case SelectedOperator.Multiplication:
                        _result = SimpleMath.Multiplication(_lastNumber, newNumber);
                        break;
                    case SelectedOperator.Division:
                        _result = SimpleMath.Division(_lastNumber, newNumber);
                        break;
                }

                LblResult.Content = _result.ToString();
            }
        }

        private void BtnPercent_Click(object sender, RoutedEventArgs e)
        {
            if (!double.TryParse(LblResult.Content.ToString(), out _lastNumber)) return;
            _lastNumber /= 100;
            LblResult.Content = _lastNumber.ToString();
        }

        private void BtnNegative_Click(object sender, RoutedEventArgs e)
        {
            _lastNumber = 0;

            if (!double.TryParse(LblResult.Content.ToString(), out _lastNumber)) return;
            _lastNumber *= -1;
            LblResult.Content = _lastNumber.ToString();
        }

        private void BtnAcOnClick(object sender, RoutedEventArgs e)
        {
            LblResult.Content = "0";
        }

        private void BtnOperation_Click(object sender, RoutedEventArgs e)
        {

            if (!double.TryParse(LblResult.Content.ToString(), out _lastNumber)) return;
            LblResult.Content = "0";

            MathOperations(sender, BtnMultiply, SelectedOperator.Multiplication);
            MathOperations(sender, BtnDivide, SelectedOperator.Division);
            MathOperations(sender, BtnPlus, SelectedOperator.Addition);
            MathOperations(sender, BtnMinus, SelectedOperator.Subtraction);

        }
        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {

            //The sender must be passed in, otherwise it does not work
            BtnClicks(sender, BtnZero, 0);
            BtnClicks(sender, BtnOne, 1);
            BtnClicks(sender, BtnTwo, 2);
            BtnClicks(sender, BtnThree, 3);
            BtnClicks(sender, BtnFour, 4);
            BtnClicks(sender, BtnFive, 5);
            BtnClicks(sender, BtnSix, 6);
            BtnClicks(sender, BtnSeven, 7);
            BtnClicks(sender, BtnEight, 8);
            BtnClicks(sender, BtnNine, 9);

            LblResult.Content = LblResult.Content.ToString() == "0" ? $"{selectedValue}" : $"{LblResult.Content}{selectedValue}";
        }

        private void MathOperations(object sender, object button, SelectedOperator operate )
        {
            if (sender == button)
                selectedOperator = operate;
        }
       private void BtnClicks(object sender, object button, int value)
        {
            if (sender == button)
                selectedValue = value;
        }

        private void BtnDecimal_OnClick(object sender, RoutedEventArgs e)
        {
            if(!LblResult.Content.ToString().Contains("."))
                LblResult.Content = $"{LblResult.Content}.";
        }
    }

    public enum SelectedOperator
    {
        Addition,
        Subtraction,
        Multiplication,
        Division
    }

    public class SimpleMath
    {
        public static double Addition(double n1, double n2)
        {
            return n1 + n2;
        }

        public static double Subtraction(double n1, double n2)
        {
            return n1 - n2;
        }

        public static double Multiplication(double n1, double n2)
        {
            return n1 * n2;
        }

        public static double Division(double n1, double n2)
        {
            if (n2 == 0)
            {
                MessageBox.Show("Divide by zero error!", "Divide By Zero", MessageBoxButton.OK, MessageBoxImage.Error);
                return 0;
            }
               
            return n1 / n2;
        }

    }

}
