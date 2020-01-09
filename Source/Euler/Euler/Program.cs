using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Euler
{
    class Program
    {
        static void Main(string[] args)
        {

            string[] list = new string[]{
                   @"a-b,-c-d,-e-a",@"a-e,-d-c,-b-a", @"a-d,-a-b,-a-e",
                 @"b-a,-e-d-,c-b",@"b-c,-d-e,-a-b"
        };
        
            foreach(var item in list)
            {
                if (Solve(item))
                {
                    Console.WriteLine(item);
                }
                Console.WriteLine(Solve(item));
            }
            Console.Read();
        }

        static bool Solve(string input)
        {
            var query = input.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Split('-')[0] + x.Split('-')[1]);
            var query1 = string.Concat(query).Distinct().Select(x => query.Count(y => y.Contains(x)));
            int query2 = query1.Count(x => x % 2 == 1);
            return (query2 == 0 || query2 == 2);
        }
    }
}
