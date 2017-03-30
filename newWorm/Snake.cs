/*
 * Создано в SharpDevelop.
 * Пользователь: user
 * Дата: 29.03.2017
 * Время: 18:36
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System;
using System.Collections.Generic;

namespace newWorm
{        
/// <summary>
/// Класс змейки.
/// </summary>
    public class Snake
    {
        /// <summary>
        /// Элемент червя
        /// </summary>
        public class Part
        {
            public int X;                                           // координаты
            public int Y;                                           // элемента тела червя
            public Part(int x, int y)
            {
                X = x;
                Y = y;
            }
        }
        
        public int dX{get;set;}
        public int dY{get;set;}
        public static Direction direction { get; set; }             // направление червя
        public static int WormSize { get; set; }                    // размер червя
        public List<Part> body = new List<Part>();
        public int Speed { get; set; }                              // скорость движения
        public bool IsInField
        {
            get
            {
                if(true)
                return false; //FIXME return anything
            }
        }
        /// <summary>
        /// Конструктор змейки
        /// </summary>
        public Snake()
        {
            direction = Direction.Left;
            Speed = 1;
            Random beginPoint = new Random();
            int a,b;
            a = beginPoint.Next(10, 200);
            b = beginPoint.Next(10, 100);
            Part head = new Part(a, b);
            body.Add(head);
            body.Add(head);
            body.Add(head);
        }
        /// <summary>
        /// Головной элемент червя. Наследует класс Part.
        /// </summary>
        
        public void Move()
        {
            var temp = body[0];
            for(int i=0; i<WormSize-1; i++)
            {
                body[i] = body[i+1];
            }
            
            body[0].X = temp.X + dX;
            body[0].Y = temp.Y + dY;
        }
        /// <summary>
        /// Тип "Направление"
        /// </summary>
        public enum Direction { Up, Down, Left, Right }             // тип "Направление"
    }
}
