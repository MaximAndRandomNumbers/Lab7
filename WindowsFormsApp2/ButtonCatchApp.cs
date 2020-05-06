using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ButtonCatch
{
    public partial class ButtonCatchApp : Form
    {
        int CursorX, CursorY;
        
        //Прямоугольная область
        Rectangle area;
        public ButtonCatchApp()
        {
            CursorX = Cursor.Position.X;
            CursorY = Cursor.Position.Y;
            InitializeComponent();
            //Генерируем область
            area = GenerateRectangle();
        }

        //Генерируем положение и размеры прямоугольной области
        private Rectangle GenerateRectangle()
        {
            Random rand = new Random();
            Rectangle rect = new Rectangle();
            rect.Width = rand.Next(100, ClientSize.Width/2);
            rect.Height = rand.Next(100, ClientSize.Height/2);
            rect.X = rand.Next(0, ClientSize.Width - rect.Width);
            rect.Y = rand.Next(0, ClientSize.Height - rect.Height);
            return rect;
        }

        //Рисуем сгенерированный прямоугольник
        private void DrawArea(PaintEventArgs e)
        {
            Pen pen = new Pen(Color.FromArgb(0, 0, 0), 4);
            e.Graphics.DrawRectangle(pen, area);
        }

        //Событие отрисовки формы
        private void Form_Paint(object sender, PaintEventArgs e)
        {
            DrawArea(e);
        }

        //Проверка на попадание в область (если хотя бы часть кнопки в области)
        private bool isButtonInArea()
        {
            return button.Location.X >= area.X
                && button.Location.X <= area.X + area.Width
                && button.Location.Y >= area.Y
                && button.Location.Y <= area.Y + Height;
        }
        private void button_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Поздравляем! Вы смогли нажать на кнопку!");
        }
        
        private int GetDistanceToButton(int x, int y)
        {
            int btnX = button.Location.X,
                btnY = button.Location.Y;

            return (int)Math.Sqrt((x-btnX)*(x-btnX) + (y-btnY)*(y-btnY));
        }


        protected override void OnMouseMove(MouseEventArgs e)
        {
            //Метод для проверки вызывается при движении мыши
            if(isButtonInArea())
            {
                CursorX = e.X;
                CursorY = e.Y;
                return;
            }
            
            if (GetDistanceToButton(CursorX, CursorY) < GetDistanceToButton(e.X, e.Y))
            {
                CursorX = e.X;
                CursorY = e.Y;
                return;
            }

            int deltaX = e.X - CursorX,
                deltaY = e.Y - CursorY;

            Point btnLoc = button.Location;
            Random rand = new Random();
            if (btnLoc.X + deltaX < 0)
            {
                deltaX = rand.Next(30,ClientSize.Width);
            }
            if (btnLoc.X + deltaX + button.Width > ClientSize.Width)
            {
                deltaX = -rand.Next(30, ClientSize.Width);
            }

            if (btnLoc.Y + deltaY < 0)
            {
                deltaY = rand.Next(30, ClientSize.Height);
            }
            if(btnLoc.Y + deltaY + button.Height > ClientSize.Height)
            {
                deltaY = -rand.Next(30, ClientSize.Height);
            }

            button.Location  = new Point(btnLoc.X + deltaX, btnLoc.Y + deltaY);
            CursorX = e.X;
            CursorY = e.Y;
        }
    }
}
