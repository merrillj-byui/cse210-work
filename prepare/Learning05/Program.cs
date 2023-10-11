using System;

class Program
{
    static void Main(string[] args)
    {
        Square square = new Square("blue", 10);
        Console.WriteLine($"Square Area: {square.GetArea()}");

        Rectangle rectangle = new Rectangle("yellow", 5, 6);
        Console.WriteLine($"Rectangle Area: {rectangle.GetArea()}");

        List<Shape> shapes = new List<Shape>();
        shapes.Add(new Square("blue", 10));
        shapes.Add(new Rectangle("yellow", 5, 6));

        foreach (Shape shape in shapes)
        {
            Console.WriteLine($"Color: {shape.GetColor()}  Area: {shape.GetArea()}");
        }
    }
}