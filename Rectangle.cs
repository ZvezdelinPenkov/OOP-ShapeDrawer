namespace OOP_Project_Third
{
    public class Rectangle : Shape
    {
        public double Height { get; set; }
        public double Width { get; set; }

        public Rectangle(Point location, Color color, double width, double height)
            : base(location, color)
        {
            Width = width;
            Height = height;
        }

        public override double CalculateArea()
        {
            return Height * Width;
        }

        public override void Draw(Graphics graphics)
        {
            using (Pen pen = new Pen(Color, 3))
            {
                graphics.DrawRectangle(pen, Location.X, Location.Y, (float)Width, (float)Height);
            }
        }

        public override void Fill(Graphics graphics, Color color)
        {
            using (SolidBrush brush = new SolidBrush(color))
            {
                graphics.FillRectangle(brush, Location.X, Location.Y, (float)Width, (float)Height);
            }
        }

        public override bool ContainsPoint(Point point)
        {
            return point.X >= Location.X && point.X <= Location.X + Width &&
                   point.Y >= Location.Y && point.Y <= Location.Y + Height;
        }

        public override object Clone()
        {
            return new Rectangle(this.Location, this.Color, this.Width, this.Height);
        }
    }
}
