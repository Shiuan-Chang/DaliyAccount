using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DailyAccount.Models;

namespace DailyAccount.Contract
{
    // 從model找出資料
    public interface INotePresenter
    {
        //拿到原始資料
        void LoadData(DateTime startDate, DateTime endDate);

        void UpdateData(UpdateNoteModel model);

        void DeleteData(DeleteNoteModel model);

    }
}
