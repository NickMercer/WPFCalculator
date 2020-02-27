using System;
using System.Collections.Generic;
using System.Linq;
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
        double lastNumber, result;
        SelectedOperator selectedOperator;

        public MainWindow()
        {
            InitializeComponent();

            acButton.Click += AcButton_Click;
            negativeButton.Click += NegativeButton_Click;
            percentageButton.Click += PercentageButton_Click;
            equalButton.Click += EqualButton_Click;
        }

        private void EqualButton_Click(object sender, RoutedEventArgs e)
        {
            double newNumber;
            if (double.TryParse(resultLabel.Content.ToString(), out newNumber))
            {
                switch (selectedOperator)
                {
                    case SelectedOperator.Addition:
                        result = SimpleMath.Add(lastNumber, newNumber);
                        break;

                    case SelectedOperator.Subtraction:
                        result = SimpleMath.Subtract(lastNumber, newNumber);
                        break;

                    case SelectedOperator.Multiplication:
                        result = SimpleMath.Multiply(lastNumber, newNumber);
                        break;

                    case SelectedOperator.Division:
                        result = SimpleMath.Divide(lastNumber, newNumber);
                        break;
                }

                resultLabel.Content = result.ToString();
                lastNumberLabel.Content = "";
            }
        }

        private void PercentageButton_Click(object sender, RoutedEventArgs e)
        {
            double tempNumber;
            if (double.TryParse(resultLabel.Content.ToString(), out tempNumber))
            {
                tempNumber = tempNumber / 100;
                if (lastNumber != 0)
                    tempNumber *= lastNumber;
                resultLabel.Content = tempNumber.ToString();
            }
        }

        private void NegativeButton_Click(object sender, RoutedEventArgs e)
        {
            if(double.TryParse(resultLabel.Content.ToString(), out lastNumber))
            {
                lastNumber = lastNumber * -1;
                resultLabel.Content = lastNumber.ToString();
            }
        }

        private void AcButton_Click(object sender, RoutedEventArgs e)
        {
            resultLabel.Content = "0";
            lastNumberLabel.Content = "";
        }

        private void NumberButton_Click(object sender, RoutedEventArgs e)
        {
            int selectedValue = 0;
            if (sender == zeroButton)   selectedValue = 0;
            if (sender == oneButton)    selectedValue = 1;
            if (sender == twoButton)    selectedValue = 2;
            if (sender == threeButton)  selectedValue = 3;
            if (sender == fourButton)   selectedValue = 4;
            if (sender == fiveButton)   selectedValue = 5;
            if (sender == sixButton)    selectedValue = 6;
            if (sender == sevenButton)  selectedValue = 7;
            if (sender == eightButton)  selectedValue = 8;
            if (sender == nineButton)   selectedValue = 9;

            if(resultLabel.Content.ToString() == "0")
            {
                resultLabel.Content = $"{selectedValue}";
            }
            else
            {
                resultLabel.Content = $"{resultLabel.Content}{selectedValue}";
            }
        }

        private void decimalButton_Click(object sender, RoutedEventArgs e)
        {
            if (!resultLabel.Content.ToString().Contains("."))
            {
                resultLabel.Content = $"{resultLabel.Content}.";
            }
        }

        private void OperationButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender == divisionButton)       selectedOperator = SelectedOperator.Division;
            if (sender == multiplicationButton) selectedOperator = SelectedOperator.Multiplication;
            if (sender == subtractionButton)    selectedOperator = SelectedOperator.Subtraction;
            if (sender == additionButton)       selectedOperator = SelectedOperator.Addition;

            if (double.TryParse(resultLabel.Content.ToString(), out lastNumber))
            {
                lastNumberLabel.Content = lastNumber.ToString();
                resultLabel.Content = "0";
            }
        }
    }

    public enum SelectedOperator
    {
        Addition,
        Subtraction,
        Multiplication,
        Division
    }

    public static class SimpleMath
    {
        public static double Add(double numberOne, double numberTwo)
        {
            return numberOne + numberTwo;
        }

        public static double Subtract(double numberOne, double numberTwo)
        {
            return numberOne - numberTwo;
        }

        public static double Multiply(double numberOne, double numberTwo)
        {
            return numberOne * numberTwo;
        }

        public static double Divide(double numberOne, double numberTwo)
        {
            if(numberTwo == 0)
            {
                MessageBox.Show("Division by 0 is not supported", "Wrong Operation", MessageBoxButton.OK, MessageBoxImage.Error);
                return 0;
            }

            return numberOne / numberTwo;
        }
    }

}
