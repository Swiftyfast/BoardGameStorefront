using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassesLibrary;


namespace CSharpReview
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime dob1 = new DateTime(2020, 11, 30);
            DateTime dob2 = new DateTime(2020, 11, 28);
            DateTime dob3 = new DateTime(2020, 11, 17);
            DateTime dob4 = new DateTime(2020, 10, 30);
            DateTime dob5 = new DateTime(2020, 10, 31);
            //char m = 'm';
            //char f = 'f';

            NewBorn timmy = new NewBorn(45, "Timothy", "Jones", dob1, 'm');
            NewBorn sandy = new NewBorn(46, "Sandy", "Smith", dob2, 'f');
            NewBorn aaron = new NewBorn(47, "Aaron", "Webster", dob3, 'm');
            NewBorn shelby = new NewBorn(48, "Shelby", "Conrad", dob4, 'f');
            NewBorn bethany = new NewBorn(49, "Bethany", "Codsworth", dob5, 'f');

            List<NewBorn> babies = new List<NewBorn> { timmy, sandy, aaron, shelby, bethany };

            foreach (NewBorn babyName in babies)
            {
                Console.WriteLine(babyName);
            }
        }
    }
}
