using System;
using System.Collections.Generic;
using System.Text;

namespace PresentLottery
{
    public class PeoplesList
    {
        private string ListName;
        private List<Person> PeopleList;
        public void CreateList(string name)
        {
            ListName = name;
            PeopleList = new List<Person>();
        }
        public void ClearList()
        {
            PeopleList = new List<Person>();
        }
        public void AddToList(Person person)
        {
            PeopleList.Add(person);
        }
        public List<Person> GetList()
        {
            return PeopleList;
        }
        public List<int> GetAllNumbers()
        {
            List<int> res = new List<int>();
            foreach (Person per in PeopleList)
            {
                res.Add(per.Number);
            }
            return res;
        }
        public void RemoveFromList(Person person)
        {
            PeopleList.Remove(person);
        }
    }
}
