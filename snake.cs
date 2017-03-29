/*
 * Создано в SharpDevelop.
 * Пользователь: user
 * Дата: 29.03.2017
 * Время: 18:36
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System;

namespace newWorm
{
    /// <summary>
    /// Description of snake.
    /// </summary>
    public class snake
    {
        Coord coord;
        direction Direction { get; set; }                       // направление червя
        public int WormSize { get; set; }                       // размер червя
        public snake()
        {
            Random random = new Random();
            coord.X = random.Next(10, 100);
            coord.Y = random.Next(10, 50);
            WormSize = 3;
            Direction = direction.Left;
        }
    }    
    /// <summary>
    /// Тип "Направление"
    /// </summary>
    enum direction { Up, Down, Left, Right }                    // тип "Направление"
    /// <summary>
    /// Координаты червя
    /// </summary>
    struct Coord
    {
        public int X;
        public int Y;
    }
}
