using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Towers
{
    class Ring
    {
        public Color Color { get => brush.Color; set => brush.Color = value; } // Цвет закраски кольца
        public int X { get; set;} // координата середины кольца по оси X
        public int Y { get; set; } // координата по оси Y
        public int Height { get; set; } // высота кольца
        public int R { get; set; } // растояни от значения свойства X до границы кольца по оси X
        public int Value { get; set; } // значение кольца
        public bool Active { get; set; } // является ли кольцо активным
        public Tower Owner { get; set; } // башня в которой находиться кольцо
        public Graphics Graph { get; set; } // для отрисовки кольца

        private SolidBrush brush;
        private Pen pen;
        public Ring() // Значения по умолчанию 
        {   
            pen = new Pen(Color.Black); 
            brush = new SolidBrush(Color.FromArgb(0, 0, 0, 0));
            pen.Width = 5;
            Value = 0;
        }
        public void Draw() // Рисуем кольцо
        { 
            Graph.FillRectangle(brush, X - R, Y, R * 2, Height);
            Graph.DrawRectangle(pen, X - R, Y, R * 2, Height);
        }
        public void SetActive() // Активируем кольцо
        {
            pen.Color = Color.FromArgb(255, 0, 0);
            Active = true;
            Draw();
        }
        public void DisActive() // Деактивируем кольцо
        {
            pen.Color = Color.Black;
            Active = false;
            Draw();
        }
    }
}
