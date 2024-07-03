using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyingChess
{
    public class Points
    {
        public int x;
        public ConsoleColor color;
        public List<Points> next;

        
        public Points(int x, ConsoleColor color, List<Points> next)
        {
            this.x = x;
            this.color = color;
            this.next = next;

        }
        public Points(int x, ConsoleColor color)
        {
            this.x = x;
            this.color = color;
            this.next = new List<Points>();
        }
        public Points()
        {
            this.x = -1;
            this.color = ConsoleColor.Black;
            this.next = new List<Points>();
        }
    }

}
