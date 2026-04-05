using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyAccount
{
    internal class Student : IDisposable
    {
        public void Dispose()
        {
            Console.WriteLine("memory has been clear!!");
        }
    }
}
