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
        /// Элемент змейки
        /// </summary>
        public class Part
        {
            public int X;                                           // координаты
            public int Y;                                           // элемента тела змейки
            public Part(int x, int y)
            {
                X = x;
                Y = y;
            }
        }

        public int dX { get; set; }                                 // сдвиг по горизонтали
        public int dY { get; set; }                                 // сдвиг по вертикали
        public static Direction direction { get; set; }             // направление змейки
        public static int SnakeSize { get; set; }                   // размер змейки
        public List<Part> body = new List<Part>();                  // список элементов змейки
        public int Speed { get; set; }                              // скорость движения

        /// <summary>
        /// Свойство, возвращающее true если змея не вылезла за пределы игрового поля.
        /// </summary>
        public bool IsInField
        {
            get
            {
                if (body[0].X > 0 && body[0].X < 480 && body[0].Y > 0 && body[0].Y < 480)
                    return true;
                else
                    return false;
            }
        }

        /// <summary>
        /// Конструктор змейки по умолчанию
        /// </summary>
        public Snake()
        {
            SnakeSize = 3;
            direction = Direction.Right;
            Speed = 5;
            Random beginPoint = new Random();
            int x, y;
            x = beginPoint.Next(10, 50);
            y = beginPoint.Next(10, 470);
            Part head = new Part(x, y);
            body.Add(head);
            body.Add(head);
            body.Add(head);
        }

        /// <summary>
        /// Метод, сдвигающий список элементов змеи справа налево.
        /// </summary>
        public void Move()
        {
            for (int i = body.Count - 1; i > 0; i--)
            {
                body[i].X = body[i - 1].X;
                body[i].Y = body[i - 1].Y;
            }
            body[0].X += dX;
            body[0].Y += dY;
        }

        /// <summary>
        /// Тип "Направление"
        /// </summary>
        public enum Direction { Up, Down, Left, Right }             // тип "Направление"
    }
}
