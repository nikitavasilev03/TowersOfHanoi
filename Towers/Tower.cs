using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;

namespace Towers
{
    class Tower
    {
        public Panel Owner { get; set; } // Панель на которой находиться башня
        public Graphics Graph { get; set; } // Для рисования колец
        public int CountRing { get => tower.Count; } // Количество колец в башне

        private Stack<Ring> tower;  // Стек колец
        public Tower()
        {
            tower = new Stack<Ring>();
        }
        public void Draw() // Рисование колец в башне
        {
            foreach (var item in tower)
                item.Draw();
        }
        public int Push(Ring ring) // Добавление кольца в башню
        {
            if (CountRing == 0) // Если колец в башне нет
            {
                ring.X = Owner.Width / 2; // Задаем параметры кольцу
                ring.Y = Owner.Height - ring.Height;
                ring.Owner = this; // Передаем кольцу с ссылку на эту башню
                ring.Graph = Graph; // Для рисования кольца 
                tower.Push(ring); // Добавляем кольцо в стек
                Owner.Refresh(); // Перерисвываем
                return 0; // Так как добавление прошло успешно возвращаем еденицу
            }
            Ring peek = tower.Peek(); // Получаем верхнее кольцо башни
            if (ring.Value > peek.Value) // Если добавляемое кольцо меньше верхнего кольца
            {
                ring.X = peek.X; // Задаем параметры относительно верзнего кольца
                ring.Y = peek.Y - ring.Height;
                ring.Owner = this; // Передаем кольцу с ссылку на эту башню
                ring.Graph = Graph; // Для рисования кольца 
                tower.Push(ring); // Добавляем кольцо в стек
                Owner.Refresh(); // Перерисвываем
                return 0; // Так как добавление прошло успешно возвращаем еденицу
            }
            return 1; // Кольцо не было добавленно, возвращаем 1
        }
        public Ring Pop() // Извлекаем кольцо из башни
        {
            if (CountRing != 0 && tower.Peek().Active) // Если кольца в башне есть и верхнее является активным
            {
                Ring ring = tower.Pop(); // Извлекаем кольцо из стека
                Owner.Refresh(); // Перерисвываем
                return ring; // Возвращаем кольцо
            }
            return null; // иначе пустая ссылка
        }
        public Ring MouseClick(int mouse_x, int mouse_y)
        {
            if (tower.Count != 0) // Если кольца в башне есть
            {
                Ring peek = tower.Peek(); // Получаем верхнее кольцо башни
                if (mouse_x >= peek.X - peek.R && mouse_x <= peek.X + peek.R && mouse_y >= peek.Y && mouse_y <= peek.Y + peek.Height) // Если мышка в верхнем кольце
                {
                    return peek; // Возвращаем верхнее кольцо
                }
                else
                    return null; // иначе пустая ссылка
            }
            return null; // иначе пустая ссылка
        }
    }
}
