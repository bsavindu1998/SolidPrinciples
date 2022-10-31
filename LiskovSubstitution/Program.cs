using System;

namespace LiskovSubstitution
{
    public class Rectangle
    {
        public virtual int Height { get; set; }
        public virtual int Width { get; set; }

        public Rectangle()
        {

        }

        public Rectangle(int height, int width)
        {
            Height = height;
            Width = width;
        }

        public override string ToString()
        {
            return $"{nameof(Width)}: {Width}, {nameof(Height)}: {Height}";
        }
    }

    public class Square : Rectangle
    {
        public override int Width
        {
            set => base.Width = base.Height = value;
        }

        public override int Height
        {
            set => base.Width = base.Height = value;
        }
    }
    internal class Program
    {
        public static int Area(Rectangle rectangle)
        {
            return rectangle.Width * rectangle.Height;
        }
        private static void Main()
        {
            var rectangle = new Rectangle(2,3);
            Console.WriteLine(Area(rectangle));

            Rectangle square = new Square();
            square.Width = 4;
            Console.WriteLine(Area(square));
        }
    }

}
