using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Project_Third
{
    public class ShapeChangedEventArgs : EventArgs
    {
        public string Message { get; private set; }

        public ShapeChangedEventArgs(string message)
        {
            Message = message;
        }
    }
}
