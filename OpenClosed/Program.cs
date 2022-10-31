using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenClosed
{
    internal class Program
    {
        public enum Color
        {
            Red,
            Green,
            Blue
        }

        public enum Size
        {
            Small,
            Medium,
            Large,
            Huge
        }

        public interface ISpecification<T>
        {
            bool IsSatisfied(T t);
        }

        public interface IFilter<T>
        {
            IEnumerable<T> Filter(IEnumerable<T> items, ISpecification<T> spec);
        }
        public class FilterWithSpec : IFilter<Product>
        {
            public IEnumerable<Product> Filter(IEnumerable<Product> items, ISpecification<Product> spec)
            {
                return items.Where(spec.IsSatisfied);
            }
        }


        public class ColorSpecification : ISpecification<Product>
        {
            public Color Color { get; }

            public ColorSpecification(Color color)
            {
                Color = color;
            }

            public bool IsSatisfied(Product t)
            {
                return t.Color == Color;
            }
        }

        public class SizeSpecification : ISpecification<Product>
        {
            public Size Size { get; }

            public SizeSpecification(Size size)
            {
                Size = size;
            }

            public bool IsSatisfied(Product t)
            {
                return t.Size == Size;
            }
        }

        public class AndSpecification<T> : ISpecification<T>
        {
            private readonly ISpecification<T> _first;
            private readonly ISpecification<T> _second;

            public AndSpecification(ISpecification<T> first, ISpecification<T> second)
            {
                _first = first;
                _second = second;
            }

            public bool IsSatisfied(T t)
            {
                return _first.IsSatisfied(t) && _second.IsSatisfied(t);
            }
        }

        public class Product
        {
            public string Name { get; set; }
            public Color Color { get; set; }
            public Size Size { get; set; }

            public Product(string name, Color color, Size size)
            {
                Name = name;
                Color = color;
                Size = size;
            }
        }

        private static void Main()
        {
            var apple = new Product("Apple", Color.Green, Size.Small);
            var tree = new Product("Tree", Color.Green, Size.Large);
            var house = new Product("House", Color.Blue, Size.Huge);

            Product[] products = {apple, tree, house};

            var filter = new FilterWithSpec();
            Console.WriteLine("Green Products");
            foreach (var product in filter.Filter(products, new ColorSpecification(Color.Green)))
            {
                Console.WriteLine($"{product.Name} is {product.Color}");
            }

            foreach (var product in filter.Filter(products, new SizeSpecification(Size.Huge)))
            {
                Console.WriteLine($"{product.Name} is {product.Size}");
            }

            foreach (var product in filter.Filter(products,
                         new AndSpecification<Product>(new ColorSpecification(Color.Blue),
                             new SizeSpecification(Size.Huge))))
            {
                Console.WriteLine($"{product.Name} is {product.Color} and it is {product.Size}");
            }
        }
    }
}