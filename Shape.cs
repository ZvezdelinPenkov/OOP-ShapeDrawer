using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Project_Third
{
    public abstract class Shape : ICloneable
    {
        private Point location;
        private Color color;

        public Point Location { get => location; set => location = value; }
        public Color Color { get => color; set => color = value; }


        public Shape(Point location, Color color)
        {
            Location = location;
            Color = color;
        }

        public abstract double CalculateArea();
        public abstract void Draw(Graphics graphics);
        public abstract void Fill(Graphics graphics, Color color);
        public abstract bool ContainsPoint(Point point);

        public virtual object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}

