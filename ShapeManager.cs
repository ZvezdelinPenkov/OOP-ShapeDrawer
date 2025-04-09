using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Project_Third
{
    public class ShapeManager
    {
        private List<Shape> shapes = new List<Shape>();

        public void AddShape(Shape shape)
        {
            if (shape == null)
            {
                throw new ArgumentNullException(nameof(shape), "Cannot add a null shape to the collection.");
            }

            shapes.Add(shape);
        }

        public bool RemoveShape(Shape shape)
        {
            if (shape == null)
            {
                throw new ArgumentNullException(nameof(shape), "Cannot remove a null shape from the collection.");
            }

            bool removed = shapes.Remove(shape);
            if (!removed)
            {
                throw new InvalidOperationException("The shape to remove was not found in the collection.");
            }

            return removed;
        }

        public List<Shape> GetShapes()
        {
            return new List<Shape>(shapes);
        }
    }
}
