using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassOver1
{
    class Program
    {
        static void Main(string[] args)
        {
            double x = 1;
            double y = 1;
            CalcDao calcDao = new CalcDao();
            List<double> listX = new List<double>();
            List<double> listY = new List<double>();
            while (x > 0 && y > 0)
            {
                Console.WriteLine("Enter x");
                x = double.Parse(Console.ReadLine());
                if (x <= 0)
                    break;
                Console.WriteLine("Enter y");
                y = double.Parse(Console.ReadLine());
                if (y <= 0)
                    break;
                listX.Add(x);
                listY.Add(y);
            }
            calcDao.GetNumbers(listX, listY);
        }
    }
}
