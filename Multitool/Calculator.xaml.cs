using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace Multitool
{
    /// <summary>
    /// Логика взаимодействия для Calculator.xaml
    /// </summary>
    public partial class Calculator : UserControl
    {
        private double currentValue = 0; // Текущая сумма
        private string operation = ""; // Текущая операция
        private bool isOperationPerformed = false; // Флаг для отслеживания завершенной операции

        public Calculator()
        {
            InitializeComponent();
        }

        // Обработчик для нажатия на кнопки цифр
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;

            // Если была выполнена операция, то продолжаем ввод второго числа
            if (isOperationPerformed)
            {
                Display.Text += button.Content.ToString(); // Добавляем цифру ко второму числу
            }
            else
            {
                // Если операция не выполнена, продолжаем ввод первого числа
                if (Display.Text == "0")
                {
                    Display.Text = button.Content.ToString(); // Заменяем "0" на цифру
                }
                else
                {
                    Display.Text += button.Content.ToString(); // Добавляем цифру ко вводу
                }
            }
        }

        // Обработчик для кнопки "C" (очистить)
        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            Display.Text = ""; // Очищаем дисплей
            currentValue = 0; // Сбрасываем текущее значение
            operation = ""; // Сбрасываем операцию
        }

        // Обработчик для кнопки "=" (равно)
        private void Equals_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Извлекаем второе число из дисплея
                string[] parts = Display.Text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length < 3) return;

                double secondValue = double.Parse(parts[2]);

                // Выполняем выбранную операцию
                switch (operation)
                {
                    case "+":
                        Display.Text = (currentValue + secondValue).ToString();
                        break;
                    case "-":
                        Display.Text = (currentValue - secondValue).ToString();
                        break;
                    case "*":
                        Display.Text = (currentValue * secondValue).ToString();
                        break;
                    case "/":
                        // Проверяем деление на ноль
                        if (secondValue != 0)
                            Display.Text = (currentValue / secondValue).ToString();
                        else
                            Display.Text = "Ошибка: деление на 0";
                        break;
                }

                // После выполнения операции оставляем только результат на дисплее
                currentValue = double.Parse(Display.Text);
                operation = ""; // Сбрасываем операцию
                isOperationPerformed = false; // Ожидаем следующую операцию
            }
            catch (FormatException)
            {
                Display.Text = "Ошибка ввода";
            }
        }

        // Обработчик для кнопок операций (+, -, *, /)
        private void Operation_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;

            // Проверяем, не пустой ли дисплей перед выполнением операции
            if (string.IsNullOrEmpty(Display.Text))
                return;

            // Если уже была выбрана операция, выполняем ее с введенным значением
            if (!string.IsNullOrEmpty(operation))
            {
                Equals_Click(sender, e);
            }

            // Сохраняем текущее значение
            currentValue = double.Parse(Display.Text);

            // Добавляем знак операции к дисплею
            Display.Text += " " + button.Content.ToString() + " ";

            // Сохраняем выбранную операцию
            operation = button.Content?.ToString() ?? ""; // Проверка на null, если null подставляет пустую строку

            // Устанавливаем флаг, что операция выбрана
            isOperationPerformed = true;
        }
    }
}