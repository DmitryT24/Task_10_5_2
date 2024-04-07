using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_10_5_2
{
    internal class Program
    {
        static ILogger logger { get; set; }
        static void UserInData(string StrValueInfo, out double num, ILogger logger)
        {
            logger.Event($"Введите {StrValueInfo} число (для продолжения нажмите - Enter):");
            if (!double.TryParse(Console.ReadLine(), out num))
            {
                throw new MyException("Ошибка! Вы ввели не корректные данные! Пожалуйста, повторите ввод!");
            }
        }
        /// <summary>
        /// Реализация зависимости
        /// public class LogsFile: ILogger
        /// {
        ///     void Event(string message){ отправка данных в файл }
        ///     void Error(string message){ отправка данных в файл }
        ///     void LogsMsg(string message){ вывод на экран }
        /// }
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            bool isTry = false;

            var calculator = new Calculator();
            logger = new Logger();

            int index = 0;
            logger.LogsMsg("Здравствуйте, вас приветствует программа калькулятор!");
            logger.LogsMsg("Давайте попробуем сложить пару чисел!");
            double number1 = 0;
            double number2 = 0;
            do
            {
                try
                {
                    switch (index)
                    {
                        case 0:
                            UserInData("первое", out number1, logger);
                            index++;
                            break;
                        case 1:
                            UserInData("второе", out number2, logger);
                            index++;
                            break;
                        case 2:
                            logger.Event($"Результат сложения чисел {number1} + {number2} = {calculator.Sum(number1, number2)}");
                            isTry = true;
                            break;
                    }
                }
                catch (Exception ex) when (ex is MyException)
                {
                    logger.Error(ex.Message);
                }
                finally
                {
                    if (isTry) logger.LogsMsg("Вычисление выполнено!");
                }
            } while (!isTry);
            Console.ReadKey();

        }
    }
}
