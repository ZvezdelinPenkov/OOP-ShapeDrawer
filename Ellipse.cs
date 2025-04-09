using OOP_Project_Third;
using System.Drawing;

namespace OOP_Project_Third
{
    public class Ellipse : Shape
    {
        public double Height { get; set; }
        public double Width { get; set; }

        public Ellipse(Point location, Color color, double width, double height)
           : base(location, color)
        {
            this.Width = width;
            this.Height = height;
        }

        public override double CalculateArea()
        {
            return Math.PI * Width * Height;
        }

        public override void Draw(Graphics graphics)
        {
            using (Pen pen = new Pen(Color, 3))
            {
                graphics.DrawEllipse(pen, Location.X, Location.Y, (float)Width, (float)Height);
            }
        }

        public override void Fill(Graphics graphics, Color color)
        {
            using (SolidBrush brush = new SolidBrush(color))
            {
                graphics.FillEllipse(brush, Location.X, Location.Y, (float)Width, (float)Height);
            }
        }

        public override bool ContainsPoint(Point point)
        {
            double centerX = Location.X + Width / 2;
            double centerY = Location.Y + Height / 2;
            double radiusX = Width / 2;
            double radiusY = Height / 2;

            double normalizedX = (point.X - centerX) / radiusX;
            double normalizedY = (point.Y - centerY) / radiusY;

            return normalizedX * normalizedX + normalizedY * normalizedY <= 1;
        }
        public override object Clone()
        {
            return new Ellipse(this.Location, this.Color, this.Width, this.Height);
        }
    }
}
