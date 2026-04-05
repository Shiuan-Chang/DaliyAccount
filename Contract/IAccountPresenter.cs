using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DailyAccount.Models;

namespace DailyAccount.Contract
{
    public interface IAccountPresenter
    {
        //拿到原始資料
        void LoadData(DateTime startDate, DateTime endDate, List<string> conditionTypes, List<string> analyzeTypes);


        //拿到原始資料並回傳groupby後的結果
        


    }
}
