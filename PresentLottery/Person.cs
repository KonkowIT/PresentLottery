using System;
using System.Collections.Generic;
using System.Text;

namespace PresentLottery
{
    public class Person
    {
        private string personName;
        private int personNumber;

        public Person(string personName, int personNumber)
        {
            this.personName = char.ToUpper(personName[0]) + personName.Substring(1);
            this.personNumber = personNumber;
        }

        public string Name => personName;
        public int Number => personNumber;
    }
}
