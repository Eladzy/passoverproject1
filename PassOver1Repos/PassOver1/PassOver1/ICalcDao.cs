using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassOver1
{
    interface ICalcDao
    {
        void GetNumbers(List<double> listX, List<double> listY);
        void TableCreation();
    }
}
