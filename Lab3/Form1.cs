using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Windows.Forms;

namespace Lab3
{
    public partial class Form1 : Form
    {
        private List<ClickPoints> coordinates = new List<ClickPoints>();
        const int WIDTH = 20;
        const int HEIGHT = 20;
        private class ClickPoints
        {
            public Point p;
            //if ture it is black if false it is red
            public bool black;
           
            public ClickPoints(Point mypoint,bool isblack)
            {
                p = mypoint;
                black = isblack;
            }

        }
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                //Add new Click to the Array
                Point p = new Point(e.X, e.Y);
                // Add point and Black status to the ClickPoints Class, then add class to List
                ClickPoints myClick = new ClickPoints(p,true);
                coordinates.Add(myClick);
                Invalidate();
            }
            if (e.Button == MouseButtons.Right)
            {
                //Loop from last element to first element in case an element needs to be removed
                for (int i = (coordinates.Count)-1; i > -1; i--)
                {                   
                    ClickPoints Clicks = coordinates[i];
                    //check if the click is within the point
                    if (Clicks.p.X - WIDTH <= e.X && Clicks.p.X + WIDTH >= e.X && Clicks.p.Y - HEIGHT <= e.Y && Clicks.p.Y + HEIGHT >= e.Y)
                    {
                        //If it is black turn it to red and modify the list, else its already red so remove it from list
                        if (Clicks.black == true)
                        {
                            Clicks.black = false;
                            coordinates[i] = Clicks;
                        }
                        else
                        {                           
                            coordinates.RemoveAt(i);                           
                        }
                        
                    }
                }
                
                Invalidate();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            
            Graphics g = e.Graphics;

            
            foreach (ClickPoints Clicks in coordinates)
            {
                //Set default brush to red, if CLicks.black is TRUE change it to black
                Brush myBrush = Brushes.Red;
                if (Clicks.black)
                {
                    myBrush = Brushes.Black;
                }
                g.FillEllipse(myBrush, Clicks.p.X - WIDTH / 2, Clicks.p.Y - WIDTH / 2, WIDTH, HEIGHT);
                //Commented out code from Lab 2 per request of Prof. Skinner
                //    string placement = "(" + p.X + ", " + p.Y + ")";
                //    g.DrawString(placement, Font, Brushes.Black, p.X + 20, p.Y - 7);

            }
        }

        private void Clear_Click(object sender, EventArgs e)
        {
            coordinates.Clear();
            Invalidate();
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            coordinates.Clear();
            Invalidate();
        }
    }
}
