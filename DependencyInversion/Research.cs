using System;
using System.Collections.Generic;
using System.Linq;

namespace DependencyInversion
{
    public enum Relationship
    {
        Parent,
        Child,
        Sibling
    }

    public class Person
    {
        public string Name { get; set; }
    }

    public interface IRelationshipsBrowser
    {
        IEnumerable<Person> FindAllChildren(string name);
    }

    public class Relationships : IRelationshipsBrowser
    {
        private readonly List<(Person, Relationship, Person)> _relations = new List<(Person, Relationship, Person)>();

        public void AddParentToChild(Person parent, Person child)
        {
            _relations.Add((parent, Relationship.Parent, child));
            _relations.Add((child, Relationship.Child, parent));
        }

        public IEnumerable<Person> FindAllChildren(string name)
        {
            return _relations.Where(r => r.Item1.Name == name && r.Item2 == Relationship.Parent)
                .Select(relation => relation.Item3);
        }
    }

    public class Research
    {
        public Research(IRelationshipsBrowser browser)
        {
            foreach (var p in browser.FindAllChildren("Savindu"))
            {
                Console.WriteLine($"Savindu has a child {p.Name}");
            }
        }

        private static void Main()
        {
            var parent = new Person {Name = "Savindu"};
            var child1 = new Person {Name = "Child1"};
            var child2 = new Person {Name = "Child2"};

            var relationships = new Relationships();
            relationships.AddParentToChild(parent, child1);
            relationships.AddParentToChild(parent, child2);

            var research = new Research(relationships);
        }
    }
}