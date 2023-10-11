public class Rectangle : Shape
{
    private float _length = 0;
    private float _width = 0;

    public Rectangle(string color, float length, float width) : base (color)
    {
        _length = length;
        _width = width;
    }

    public void SetLength(float length)
    {
        _length = length;
    }

    public float GetLength()
    {
        return _length;
    }

    public void SetWidth(float width)
    {
        _width = width;
    }

    public float GetWidth()
    {
        return _width;
    }

    public override float GetArea()
    {
        return _length * _width;
    }
}