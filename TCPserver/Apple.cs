using System;

namespace TCPserver
{
    public class Apple
    {                                                          // координаты яблока 
        public int x, y;
        public Apple()
        {
            Random rend = new Random();                         
            int x = rend.Next(10, 390);
            int y = rend.Next(10, 390);
        }
    }
}
