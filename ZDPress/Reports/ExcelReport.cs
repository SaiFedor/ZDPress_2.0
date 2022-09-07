using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Interop.Excel;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using ZDPress.Dal.Entities;
using ZDPress.Opc;
using System.IO;

namespace ZDPress.UI.Reports
{
    public class ExcelReport
    {
        /// <summary>
        /// Путь к шаблону журнала.
        /// </summary>
        public string RegisterTemplatePath 
        {
            get 
            {
                return Environment.CurrentDirectory + @"\Resources\Templates\register.xlsx";
            }
        }

        /// <summary>
        /// Путь к шаблону пасспорта.
        /// </summary>
        public string PassportTemplatePath
        {
            get
            {                
                return Environment.CurrentDirectory + @"\Resources\Templates\passport.xlsx";
            }
        }

        public string PassportsArhivePath
        {
            get
            {
                return ConfigurationManager.AppSettings["PassportsArhivePath"];
            }
        }

        public string RegisterArhivePath
        {
            get
            {
                return ConfigurationManager.AppSettings["RegisterArhivePath"];
            }
        }


        const int max_rows_in_sheet = 45;
        private static Microsoft.Office.Interop.Excel.Application application;
        private static Workbook workBook;
        private static Worksheet worksheet;

        //Запись в журнал
        public bool CreateExcel(PressOperation operation) 
        {
            //Открываем шаблон
            //Приложение самого Excel
            application = new Microsoft.Office.Interop.Excel.Application
            {
                DisplayAlerts = false
                // Visible = false
            };
 
            try
            {
                string filePath = RegisterArhivePath + @"\register.xlsx";
                Logger.Log.Debug("Файл журнала: " + filePath);
                Logger.Log.Debug("Файл шаблона журнала: " + RegisterTemplatePath);
                if (File.Exists(filePath))
                {
                    workBook = application.Workbooks.Open(Filename: RegisterArhivePath + @"\register.xlsx");

                    Logger.Log.Debug("Файл журнала есть: " + filePath);
                }
                else
                {
                    Logger.Log.Debug("Файла журнала не: " + filePath);
                    File.Copy(RegisterTemplatePath, RegisterArhivePath + @"\register.xlsx");
                    workBook = application.Workbooks.Open(Filename: RegisterTemplatePath);
                }                
            }
            catch (Exception)
            {
                Logger.Log.Debug("Ошибка создания файла журнала: ");
                MessageBox.Show(@"Ошибка записи! Файл не существует или занят другим приложением!");
                return false;
            }  
                worksheet = workBook.ActiveSheet as Worksheet;
                //Cчитаем мколичество заполненных строк на листе
                int iRows = worksheet.Cells[3, 1].CurrentRegion.Rows.Count + 1;
                //Ищем строку с таким-же номером оси
                char[] separ = { ' ', '.', '-' };
                int flag = 0;
                for (int i = 4; i <= iRows; i++)
                {
                    if (Convert.ToString(worksheet.Cells[i, 2].Value) == Convert.ToString(operation.DiagramNumber))
                    {
                        flag = i;
                        break;
                    }
                }
                //Если такой строки еще нет
                if (flag == 0)
                {
                    //Заполняем все общие строки
                    //Заполняем дату
                    worksheet.Cells[iRows + 1, 1].Value = DateTime.Now.ToShortDateString();
                    //Заполняем номер диаграммы
                    worksheet.Cells[iRows + 1, 2].Value = operation.DiagramNumber;//номор диаграммы
                    //Заполняем номер оси (колесной пары)
                    worksheet.Cells[iRows + 1, 4].Value = operation.FactoryNumber + operation.AxisNumber;//1 номор завода 2 номер колесной пары
                    //Заполняем тип колесной пары
                    worksheet.Cells[iRows + 1, 5].Value = operation.WheelType;// тип колесной пары
                    //Заполняем следующий столбец константой
                    worksheet.Cells[iRows + 1, 6].Value = 20;
                    //Определяем какую сторону необходимо заполнять
                    int col = 0;
                    if (string.Compare(operation.Side, "левая", true) == 0)
                    {
                        col = 14;
                    }
                    else if (string.Compare(operation.Side, "правая", true) == 0)
                    {
                        col = 7;
                    }
                    else
                    {
                        col = 7;//default value
                    }
                    //Заполняем номер ЦКК (номер колеса)
                    worksheet.Cells[iRows + 1, col].Value = operation.WheelNumber;
                    //Заполняем диаметр подступичной части
                    worksheet.Cells[iRows + 1, col + 1].Value = operation.DWheel;
                    //Заполняем внутренний диаметр ступицы
                    worksheet.Cells[iRows + 1, col + 2].Value = operation.DAxis;
                    //Заполняем длину ступицы
                    worksheet.Cells[iRows + 1, col + 3].Value = operation.LengthStup;
                    //Заполняем принятый натяг
                    worksheet.Cells[iRows + 1, col + 4].Value = operation.Natiag;
                    //Заполняем конечное давление
                    worksheet.Cells[iRows + 1, col + 5].Value = operation.MaxPower;
                }
                //Если строка уже есть
                else
                {
                    //Определяем какую сторону необходимо заполнять
                    int col = 7;
                    if (string.Compare(operation.Side, "левая", true) == 0)
                    {
                        col = 14;
                    }
                    //else if (string.Compare(operation.Side, "правая", true) == 0)
                    //{
                    //    col = 7;
                    //}

                    //Заполняем номер ЦКК
                    worksheet.Cells[flag, col].Value = operation.WheelNumber;
                    //Заполняем диаметр подступичной части
                    worksheet.Cells[flag, col + 1].Value = operation.DWheel;
                    //Заполняем внутренний диаметр ступицы
                    worksheet.Cells[flag, col + 2].Value = operation.DAxis;
                    //Заполняем длину ступицы
                    worksheet.Cells[flag, col + 3].Value = operation.LengthStup;
                    //Заполняем принятый натяг
                    worksheet.Cells[flag, col + 4].Value = operation.Natiag;
                    //Заполняем конечное давление
                    worksheet.Cells[flag, col + 5].Value = operation.MaxPower;

                    //Тут же проверяем заполненность всех строк на листе
                    //на шапку добавляем 4 строки

                    //Если заполнены все необходимые строки
                    if (iRows == max_rows_in_sheet + 4)
                    {
                        //Проверяем заполненность столбцов
                        bool flag_data = true;
                        for (int j = 5; j <= iRows; j++)
                            if (worksheet.Cells[j, 7].Value == null || worksheet.Cells[j, 13].Value == null)
                                flag_data = false;
                        //Если заполнены и правая и левая стороны для всех строк
                        if (flag_data == true)
                        {
                            //Печатаем файл
                            worksheet.PrintOutEx();
                            //!!!!!!!!!!!!!!!!!!!!!!!!!ТУТ ФОРМИРУЕТСЯ ИМЯ СОХРАНЯЕМОГО ФАЙЛА И ПУТЬ!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                            //Сохраняем файл в архив
                            //формат названия файла: год - от 01 до 99, месяц - от 01 до 12, день - от 01 до 31, часы - от 00 до 23 и т.д.
                            workBook.SaveCopyAs(@"D:\registerArchive\" + DateTime.Now.ToString("yy_M_dd_HH_mm_ss") + ".xlsx");
                            //Очищаем файл от данных
                            for (int k = max_rows_in_sheet + 4; k > 4; k--)
                            {
                                worksheet.Cells[k, 1].EntireRow.Delete();
                            }
                            worksheet.PageSetup.PrintArea = "$A$1:$T$" + (max_rows_in_sheet + 4);
                        }
                    }
                }
                workBook.Save();
                //Закрываем Excel
                CloseExcel();
                return true;
        }

        private static void CloseExcel()
        {
            if (application != null)
            {
                int excelProcessId = -1;
                GetWindowThreadProcessId(application.Hwnd, ref excelProcessId);

                Marshal.ReleaseComObject(worksheet);
                workBook.Close();
                Marshal.ReleaseComObject(workBook);
                application.Quit();
                Marshal.ReleaseComObject(application);

                application = null;
                // Прибиваем висящий процесс
                try
                {
                    Process process = Process.GetProcessById(excelProcessId);
                    process.Kill();
                }
                catch(Exception ex)
                {
                    Logger.Log.Error("CloseExcel", ex);
                }
            }
        }

        [DllImport("user32.dll", SetLastError = true)]
        static extern uint GetWindowThreadProcessId(int hWnd, ref int lpdwProcessId);

        // Создание паспорта
        public bool CreatePassportExcel(PressOperation left, PressOperation right)
        {
            application = new Microsoft.Office.Interop.Excel.Application
            {
                DisplayAlerts = false
            };


            char[] separ = { ' ', '.', '-' };
            try
            {
                //Открываем шаблон
                //Приложение самого Excel
                workBook = application.Workbooks.Open(PassportTemplatePath);
                worksheet = workBook.ActiveSheet as Worksheet;
                //Заполняем файл (шаблон)
                //Заполняем дату формирования КП
                worksheet.Range["B12"].Value = Convert.ToString(DateTime.Now.Month);
                worksheet.Range["D12"].Value = Convert.ToString(DateTime.Now.Year);
                worksheet.Range["A28"].Value = String.Format("Паспорт соствален {0} {1} {2}г.", Convert.ToString(DateTime.Now.Day), Convert.ToString(DateTime.Now.Month), Convert.ToString(DateTime.Now.Year));
                //Заполняем тип колесной пары
                worksheet.Range["B5"].Value = left.WheelType;

                //Заполняем номер диаграммы
                worksheet.Range["B11"].Value = left.DiagramNumber;
                //заполняем данные по номеру колесной пары
                string[] parce_pair = new string[2];
                if (left == null || left.AxisNumber == null)
                {
                    MessageBox.Show(@"Ошибка записи! Не заполнено поле номер колёсной пары.");
                    return false;
                }
                parce_pair = left.AxisNumber.Split(separ);
                if (parce_pair.Length < 2)
                {
                    MessageBox.Show(@"Неверный формат заполнения колёсной пары. В паспорте могут быть ошибки.");
                    //return false;
                }
                //Заполняем условный номер изготовления черновой оси
                worksheet.Range["B3"].Value = left.FactoryNumber;
                //Заполняем порядковый номер черновой оси
                worksheet.Range["C3"].Value = parce_pair.Length > 0 ? parce_pair[0] : string.Empty;
                //Заполняем год изготовления черновой оси
                worksheet.Range["E3"].Value = Convert.ToString(parce_pair.Length > 1 ? parce_pair[1] : string.Empty);

                if (left == null || left.WheelNumber == null)
                {
                    MessageBox.Show(@"Ошибка записи! Не заполнено поле номер колеса(левого).");
                    return false;
                }
                string[] parce_left_wheel = left.WheelNumber.Split(separ);
                if (parce_left_wheel.Length < 5)
                {
                    MessageBox.Show(@"Неверный формат заполнения номера колеса(левого). В паспорте могут быть ошибки.");
                    return false;
                }
                worksheet.Cells[17, 4].Value2 = parce_left_wheel.Length > 0 ? parce_left_wheel[0] : string.Empty;
                worksheet.Cells[19, 4].Value2 = parce_left_wheel.Length > 1 ? parce_left_wheel[1] : string.Empty;
                worksheet.Cells[18, 4].Value2 = parce_left_wheel.Length > 2 ? parce_left_wheel[2] : string.Empty;
                worksheet.Cells[15, 4].Value2 = parce_left_wheel.Length > 3 ? parce_left_wheel[3] : string.Empty;
                worksheet.Cells[16, 4].Value2 = parce_left_wheel.Length > 4 ? parce_left_wheel[4] : string.Empty;

                if (left == null || left.WheelNumber == null)
                {
                    MessageBox.Show(@"Ошибка записи! Не заполнено поле номер колеса(правого).");
                    return false;
                }
                string[] parce_right_wheel = right.WheelNumber.Split(separ);
                if (parce_right_wheel.Length < 5)
                {
                    MessageBox.Show(@"Неверный формат заполнения номера колеса(правого). В паспорте могут быть ошибки.");
                    return false;
                }
                worksheet.Cells[17, 2].Value2 = parce_right_wheel.Length > 0 ? parce_right_wheel[0] : string.Empty;
                worksheet.Cells[19, 2].Value2 = parce_right_wheel.Length > 1 ? parce_right_wheel[1] : string.Empty;
                worksheet.Cells[18, 2].Value2 = parce_right_wheel.Length > 2 ? parce_right_wheel[2] : string.Empty;
                worksheet.Cells[15, 2].Value2 = parce_right_wheel.Length > 3 ? parce_right_wheel[3] : string.Empty;
                worksheet.Cells[16, 2].Value2 = parce_right_wheel.Length > 4 ? parce_right_wheel[4] : string.Empty;


                //Пока сохраняем
                workBook.SaveAs(PassportsArhivePath + "\\" + left.AxisNumber);

                //Печатаем файл
                //worksheet.PrintOutEx();
                CloseExcel();
                return true;
            }
             catch (Exception)
            {
                MessageBox.Show(@"Ошибка записи! Файл не существует или занят другим приложением!");
                return false;
            }
               
        }
    }
    
}
