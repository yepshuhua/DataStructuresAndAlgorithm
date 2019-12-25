using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chicken
{
    class Program
    {
        
        static void Main(string[] args)
        {
            int x, y, z=0;
            for (x = 0; x < 20; x++)
            {
                for(y = 0; y < 33; y++)
                {
                    z = 100 - x - y;
                    if (x + y + z == 100 && 7 * x + 4 * y == 100)
                    {
                        Console.WriteLine("{0}只公鸡,{1}只母鸡,{2}只小鸡",x,y,z);
                        
                    }
                }
                
            }
            Console.Read();
        }
    }
}
