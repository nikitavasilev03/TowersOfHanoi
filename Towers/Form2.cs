using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Towers
{
    public partial class Form2 : Form
    {
        private long time; // время в милисекундах
        private int step; // количесво шагов
        private Tower tower1, tower2, tower3; // 3 башни на каждую панель
        private int countRing; // Количесво всех колец
        private Ring activeRing; // Активное кольцо
        public Form2(int count)
        {
            InitializeComponent();
            countRing = count;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            tower1 = new Tower();
            tower1.Owner = panel1;
            tower1.Graph = panel1.CreateGraphics();

            tower2 = new Tower();
            tower2.Owner = panel2;
            tower2.Graph = panel2.CreateGraphics();

            tower3 = new Tower();
            tower3.Owner = panel3;
            tower3.Graph = panel3.CreateGraphics();

            int r = panel1.Width / 2 - 20;
            double k = 0.8;
            Random rnd = new Random();
            for (int i = 0; i < countRing; i++)
            {
                Ring ring = new Ring();
                ring.R = r;
                ring.Height = 40;
                ring.Color = Color.FromArgb(rnd.Next(100, 255), rnd.Next(100, 255), rnd.Next(100, 255));
                ring.Value = i + 1;
                tower1.Push(ring);

                r = (int)(r * k);
            }

            time = 0;
            step = 0; label4.Text = "0";
            timer1.Start();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            tower1.Draw();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            tower2.Draw();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {
            tower3.Draw();
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            Work(tower1, e.X, e.Y);    
        }

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            Work(tower2, e.X, e.Y);
        }

        private void panel3_MouseDown(object sender, MouseEventArgs e)
        {
            Work(tower3, e.X, e.Y);
            if (tower3.CountRing == countRing) // Если все кольца в последней башне
            {
                Form3 form3 = new Form3(label4.Text, label2.Text);
                DialogResult = form3.ShowDialog(this); // Возвращаемся на первую форму
            }
        }

        private void Work(Tower tower, int mouse_x, int mouse_y)
        {
            Ring ring = tower.MouseClick(mouse_x, mouse_y); // Проверяем попала ли мышка в верхнее кольцо
            if (ring != null) // если да
            {
                if (activeRing != null) //Если есть активное кольцо
                    activeRing.DisActive(); // Деактивируем его
                ring.SetActive(); // Активируем кольцо в которое папала мышка
                activeRing = ring; // сохраняем ссылку на кольцо
            }
            else
            {
                if (activeRing != null) // Если есть активное кольцо
                {
                    if (tower.Push(activeRing.Owner.Pop()) == 0) // Извлекаем активное кольцо из ее башни и добавляем в данную башню
                    {
                        step++; // если добавление прошло
                        label4.Text = step.ToString();
                    }
                    else
                        activeRing.Owner.Push(activeRing); // Иначе возвращаем кольцо откуда взяли
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            time += timer1.Interval;
            int timeSec = (int)(time / 1000);
            string h = (timeSec / 3600 / 10 % 10).ToString() + (timeSec / 3600 % 10).ToString();
            string m = (timeSec / 60 / 10 % 10).ToString() + (timeSec / 60 % 10).ToString();
            string s = (timeSec / 10 % 10).ToString() + (timeSec % 10).ToString();
            label2.Text = h + ":" + m + ":" + s;            
        }
    }
}
