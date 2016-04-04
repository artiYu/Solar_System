using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SolarSystem
{
    public partial class Form1 : Form
    {
        private Graphics gr;

        //кисти для отрисовки планет
        private Brush yellowBrush;
        private Brush greenBrush;
        private Brush whiteBrush;

        //координаты левых верхних углов прямоугольников, в которые вписываются круги-планеты
        private float solarX;
        private float solarY;
        private float earthX;
        private float earthY;
        private float luneX;
        private float luneY;

        //константы-радиусы планет
        private const int solarRadius = 150;
        private const int earthRadius = 50;
        private const int luneRadius = 10;

        //координаты центров планет
        private float solarCX;
        private float earthCX;
        private float luneCX;
        private float solarCY;
        private float earthCY;
        private float luneCY;

        //переменные для расчета движения планет
        private float earthAngle = 0.0f;
        private float luneAngle = 0.0f;
        private float eX, eY;
        private float lX, lY;

        private float radRotEarth;
        private float radRotLune;


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //инициализируем все необходимые переменные
            gr = CreateGraphics();
            BackColor = Color.Black;
            yellowBrush = new SolidBrush(Color.Yellow);
            greenBrush = new SolidBrush(Color.DeepSkyBlue);
            whiteBrush = new SolidBrush(Color.White);

            solarX = Width/2 - 80;
            solarY = Height/2 - 100;

            solarCX = solarX + solarRadius/2;
            solarCY = solarY + solarRadius/2;

            earthX = solarX + solarRadius/2;
            earthY = solarY + solarRadius + 30;

            earthCX = earthX + earthRadius/2;
            earthCY = earthY + earthRadius / 2 + 5;

            luneX = earthX -5;
            luneY = earthY -5;

            radRotLune = earthCX + luneX;
            radRotEarth = earthRadius;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            //Солнце
            gr.FillEllipse(yellowBrush, solarX, solarY, solarRadius, solarRadius);
            //Земля
            gr.FillEllipse(greenBrush, earthX, earthY, earthRadius, earthRadius);
            //Луна
            gr.FillEllipse(whiteBrush, luneX, luneY, luneRadius, luneRadius);


            radRotEarth = 2*solarRadius + 2*earthRadius+1000;
            radRotLune = 2*earthRadius + 2*luneRadius+20;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //старое положение Земли (формула Эйлера)
            eX = (float)Math.Cos(earthAngle) * (radRotEarth);
            eY = (float)Math.Sin(earthAngle) * (radRotEarth);

            //меняем угол
            earthAngle += 0.1f;

            //считаем новое положение по формуле Эйлера
            earthX = (float)Math.Cos(earthAngle) * (radRotEarth) - eX + solarX + earthRadius;
            earthY = (float)Math.Sin(earthAngle) * (radRotEarth) - eY + solarY + earthRadius;

            //старое положение Луны (формула Эйлера)
            lX = (float)Math.Cos(luneAngle) * (radRotLune);
            lY = (float)Math.Sin(luneAngle) * (radRotLune);

            //меняем угол
            luneAngle += 0.3f;

            //считаем новое положение по формуле Эйлера
            luneX = (float)Math.Cos(luneAngle) * (radRotLune) - lX + earthX + luneRadius+10;
            luneY = (float)Math.Sin(luneAngle) * (radRotLune) - lY + earthY + luneRadius+10;

            //обновляется полотно
            Invalidate();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            timer1.Stop();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }
    }
}
