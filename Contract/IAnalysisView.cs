using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;
using DailyAccount.Models;

namespace DailyAccount.Contract
{
    public interface IAnalysisView
    {
        void UpdateDataView(List<AnalysisModel> lists);
        void Reload();
        void DisplayChart(Chart chart); // 用於顯示圖表
        void LoadSeries(List<Series> series);
        List<string> GetConditionTypes(); // 獲取條件類型
        List<string> GetAnalyzeTypes(); // 獲取分析類型
    }
}
