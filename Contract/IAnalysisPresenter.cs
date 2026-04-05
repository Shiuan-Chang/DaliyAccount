using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DailyAccount.Models;

namespace DailyAccount.Contract
{
    public interface IAnalysisPresenter
    {
        //拿到原始資料
        void LoadData(DateTime startDate, DateTime endDate);

    }
}
