using System;
using System.Collections.Generic;

namespace SemesterAlgorithms
{
    public class Person
    {
        public readonly string FirstName;
        public readonly string LastName;
        public readonly int Age;

        public Person(string firstName, string lastName, int age)
        {
            FirstName = firstName;
            LastName = lastName;
            Age = age;
        }

        public override bool Equals(Object obj) // BRED!!!!
        {
            var person = obj as Person;
            return String.Compare(FirstName, person.FirstName, StringComparison.Ordinal) == 0
                   && String.Compare(LastName, person.LastName, StringComparison.Ordinal) == 0
                   && Age == person.Age;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (FirstName != null ? FirstName.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (LastName != null ? LastName.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ Age;
                return hashCode;
            }
        }
    }
}
