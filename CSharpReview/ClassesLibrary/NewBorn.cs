using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassesLibrary
{
    public class NewBorn
    {
        //fields

        //props
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public char Gender { get; set; }

        //ctors
        public NewBorn(int id, string firstName, string lastName, DateTime dateOfBirth, char gender)
        {
            ID = id;
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            Gender = gender;
        }

        public NewBorn() { }

        //methods

        public override string ToString()
        {
            //string genderString;
            //if (Gender = )
            //{
            //    genderString = "boy";
            //}
            return string.Format(FirstName + " " + LastName + " was born on " + DateOfBirth + ".");
        }
    }
}
