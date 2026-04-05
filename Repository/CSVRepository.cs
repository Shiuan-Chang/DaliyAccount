using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DailyAccount.Models;
using DailyAccount.Utility;
using CSVHelper;
using System.Runtime.InteropServices.ComTypes;
using System.Collections;

namespace DailyAccount.Repository
{
    public class CSVRepository : IRepository
    {
        // Default path points to the original data folder.
        // Tests can inject a temp path to avoid touching real data.
        private readonly string _baseFolderPath;

        public CSVRepository(string basePath = @"C:\Users\icewi\OneDrive\桌面\testCSV")
        {
            _baseFolderPath = basePath;
        }

        public bool AddData(AddFormRawDataDAO dao)
        {
            if (!DateTime.TryParse(dao.Date, out var parsedDate))
            {
                throw new FormatException($"Invalid date format: {dao.Date}");
            }

            string formattedDate = parsedDate.ToString("yyyy-MM-dd");
            string folderPath = Path.Combine(_baseFolderPath, formattedDate);

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            string filePath = Path.Combine(folderPath, "data.csv");

            CSV.WriteCSV(filePath, new List<AddFormRawDataDAO> { dao });
            return true;
        }

        public List<NotFormRawDataDAO> GetDatasByDate(DateTime start, DateTime end)
        {
            List<NotFormRawDataDAO> lists = new List<NotFormRawDataDAO>();
            TimeSpan dateSpan = end - start;
            int timePeriod = dateSpan.Days;

            for (int i = 0; i <= timePeriod; i++)
            {
                string folderPath = Path.Combine(_baseFolderPath, start.AddDays(i).ToString("yyyy-MM-dd"), "data.csv");
                if (File.Exists(folderPath))
                {
                    List<NotFormRawDataDAO> periodList = CSV.ReadCSV<NotFormRawDataDAO>(folderPath, true);
                    lists.AddRange(periodList);
                }
            }
            return lists;
        }

        public List<AnalysisRawDataDAO> AnalysisGetDatasByDate(DateTime start, DateTime end)
        {
            List<AnalysisRawDataDAO> lists = new List<AnalysisRawDataDAO>();
            TimeSpan dateSpan = end - start;
            int timePeriod = dateSpan.Days;

            for (int i = 0; i <= timePeriod; i++)
            {
                string folderPath = Path.Combine(_baseFolderPath, start.AddDays(i).ToString("yyyy-MM-dd"), "data.csv");
                if (File.Exists(folderPath))
                {
                    List<AnalysisRawDataDAO> periodList = CSV.ReadCSV<AnalysisRawDataDAO>(folderPath, true);
                    lists.AddRange(periodList);
                }
            }
            return lists;
        }

        public List<AccountRawDataDAO> accountFormGetDatasByDate(DateTime start, DateTime end)
        {
            List<AccountRawDataDAO> lists = new List<AccountRawDataDAO>();
            TimeSpan dateSpan = end - start;
            int timePeriod = dateSpan.Days;

            for (int i = 0; i <= timePeriod; i++)
            {
                string folderPath = Path.Combine(_baseFolderPath, start.AddDays(i).ToString("yyyy-MM-dd"), "data.csv");
                if (File.Exists(folderPath))
                {
                    List<AccountRawDataDAO> periodList = CSV.ReadCSV<AccountRawDataDAO>(folderPath, true);
                    lists.AddRange(periodList);
                }
            }
            return lists;
        }

        public bool ModifyData(RawData data)
        {
            return true;
        }

        public bool RemoveData(string date)
        {
            if (!DateTime.TryParse(date, out var parsedDate))
            {
                throw new FormatException($"Invalid date format: {date}");
            }

            bool dataRemoved = false;

            if (Directory.Exists(_baseFolderPath))
            {
                var dataFolders = Directory.GetDirectories(_baseFolderPath);
                foreach (var dataFolder in dataFolders)
                {
                    string filePath = Path.Combine(dataFolder, "data.csv");
                    if (File.Exists(filePath))
                    {
                        List<NotFormRawDataDAO> folderData = CSV.ReadCSV<NotFormRawDataDAO>(filePath, true);

                        int initialCount = folderData.Count;
                        folderData.RemoveAll(item =>
                        {
                            if (DateTime.TryParse(item.Date, out var itemDate))
                            {
                                Console.WriteLine($"Item Date: {item.Date}, Parsed Date: {parsedDate.Date}");
                                return itemDate.Date == parsedDate.Date;
                            }
                            return false;
                        });

                        if (folderData.Count < initialCount)
                        {
                            using (var fileStream = new FileStream(filePath, FileMode.Truncate))
                            {
                                fileStream.SetLength(0);
                            }

                            CSV.WriteCSV(filePath, folderData);
                            Console.WriteLine($"File updated: {filePath}");
                            dataRemoved = true;
                        }
                    }
                }
            }
            return dataRemoved;
        }

        public List<AnalysisRawDataDAO> GetChartDatas(DateTime start, DateTime end)
        {
            List<AnalysisRawDataDAO> lists = new List<AnalysisRawDataDAO>();
            TimeSpan dateSpan = end - start;
            int timePeriod = dateSpan.Days;

            for (int i = 0; i <= timePeriod; i++)
            {
                string folderPath = Path.Combine(_baseFolderPath, start.AddDays(i).ToString("yyyy-MM-dd"), "data.csv");
                if (File.Exists(folderPath))
                {
                    List<AnalysisRawDataDAO> periodList = CSV.ReadCSV<AnalysisRawDataDAO>(folderPath, true);
                    lists.AddRange(periodList);
                }
            }
            return lists;
        }
    }
}