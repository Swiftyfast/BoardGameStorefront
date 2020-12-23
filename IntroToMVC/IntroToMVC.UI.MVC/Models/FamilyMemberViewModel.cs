using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IntroToMVC.UI.MVC.Models
{
    public class FamilyMemberViewModel
    {
        //fields

        //props
        public int ID { get; } //Readonly - Must be assigned at declaration or via ctor assignment
        public string Name { get; set; }
        public string Relation { get; set; }
        //Containment - When a complex data type is used as a field or property within another class
        public DateTime Birthday { get; set; }

        //ctors
        public FamilyMemberViewModel() { }

        public FamilyMemberViewModel(int id, string name, string relation, DateTime birthday)
        {
            ID = id;
            Name = name;
            Relation = relation;
            Birthday = birthday;
        }

        //method
        //Override the ToString()
        public override string ToString()
        {
            return string.Format(
                $"Name: {Name} <br/>Relationship: {Relation} <br/>Birthday: {Birthday.ToString("MM/dd/yyy")}");
        }
        

    }
}