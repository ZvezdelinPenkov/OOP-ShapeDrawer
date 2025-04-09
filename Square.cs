using OOP_Project_Third;

namespace OOP_Project_Third
{
    public class Square : Shape
    {
        public double Side { get; set; }

        public Square(Point location, Color color, double side)
            : base(location, color)
        {
            Side = side;
        }

        public override double CalculateArea()
        {
            return Side * Side;
        }

        public override void Draw(Graphics graphics)
        {
            using (Pen pen = new Pen(Color, 3))
            {
                graphics.DrawRectangle(pen, Location.X, Location.Y, (float)Side, (float)Side);
            }
        }

        public override void Fill(Graphics graphics, Color color)
        {
            using (SolidBrush brush = new SolidBrush(color))
            {
                graphics.FillRectangle(brush, Location.X, Location.Y, (float)Side, (float)Side);
            }
        }

        public override bool ContainsPoint(Point point)
        {
            return point.X >= Location.X && point.X <= Location.X + Side &&
                   point.Y >= Location.Y && point.Y <= Location.Y + Side;
        }

        public override object Clone()
        {
            return new Square(this.Location, this.Color, this.Side);
        }

    }
}
