﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuickHull
{
    public partial class Form1 : Form
    {
        private List<Point> points;
        private List<Point> hullPoints;

        public Form1()
        {
            InitializeComponent();
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Graphics graphics = Graphics.FromImage(pictureBox1.Image);
            graphics.Clear(Color.White);
            points = new List<Point>();
            hullPoints = new List<Point>();
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            Graphics graphics = Graphics.FromImage(pictureBox1.Image);
            Pen pen = new Pen(Color.Blue);
            pen.Width=3;
            if (e.Button == MouseButtons.Left)
            {
                points.Add(new Point(e.X, e.Y));
                graphics.DrawRectangle(pen, e.X, e.Y, 1, 1);
                pictureBox1.Invalidate();
            }
        }

        private int Side(Point p1, Point p2, Point p)
        {
            int val = (p.Y - p1.Y) * (p2.X - p1.X) - (p2.Y - p1.Y) * (p.X - p1.X);

            if (val > 0)
                return 1;
            if (val < 0)
                return -1;
            return 0;
        }

        private int Distance(Point p1, Point p2, Point p)
        {
            return Math.Abs((p.Y - p1.Y) * (p2.X - p1.X) - (p2.Y - p1.Y) * (p.X - p1.X));
        }


        private void KirkpatrickSeidel()
        {

            if (points.Count <= 3)
            {
                foreach (var p in points)
                {
                    hullPoints.Add(p);
                }
                return;
            }

            Point pmin = points
                .Select(p => new { point = p, x = p.X })
                .Aggregate((p1, p2) => p1.x < p2.x ? p1 : p2).point;

            Point pmax = points.Select(p => new { point = p, x = p.X }).Aggregate((p1, p2) => p1.x > p2.x ? p1 : p2).point;

            hullPoints.Add(pmin);
            hullPoints.Add(pmax);

            List<Point> left = new List<Point>();
            List<Point> right = new List<Point>();

            for (int i = 0; i < points.Count; i++)
            {
                Point p = points[i];
                if (Side(pmin, pmax, p) == 1)
                    left.Add(p);
                else
                if (Side(pmin, pmax, p) == -1)
                    right.Add(p);
            }
            CreateHull(pmin, pmax, left);
            CreateHull(pmax, pmin, right);
        }

        private void CreateHull(Point a, Point b, List<Point> points)
        {
            int pos = hullPoints.IndexOf(b);
            if (points.Count == 0)
                return;

            if (points.Count == 1)
            {
                Point pp = points[0];
                hullPoints.Insert(pos, pp);
                return;
            }

            int dist = int.MinValue;
            int point = 0;

            for (int i = 0; i < points.Count; i++)
            {
                Point pp = points[i];
                int distance = Distance(a, b, pp);
                if (distance > dist)
                {
                    dist = distance;
                    point = i;
                }
            }

            Point p = points[point];
            hullPoints.Insert(pos, p);
            List<Point> ap = new List<Point>();
            List<Point> pb = new List<Point>();

            for (int i = 0; i < points.Count; i++)
            {
                Point pp = points[i];
                if (Side(a, p, pp) == 1)
                {
                    ap.Add(pp);
                }
            }
            for (int i = 0; i < points.Count; i++)
            {
                Point pp = points[i];
                if (Side(p, b, pp) == 1)
                {
                    pb.Add(pp);
                }
            }
            CreateHull(a, p, ap);
            CreateHull(p, b, pb);
        }

        private void build_Click(object sender, EventArgs e)
        {
            hullPoints.Clear();
            if (points.Count != 0)
            {
                pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
                Graphics graphics1 = Graphics.FromImage(pictureBox1.Image);
                graphics1.Clear(Color.White);
                Pen pen1 = new Pen(Color.Green);
                pen1.Width = 3;
                foreach (var p in points)
                {
                    graphics1.DrawRectangle(pen1, p.X, p.Y, 1, 1);
                    pictureBox1.Invalidate();
                }

            }
            Graphics graphics = Graphics.FromImage(pictureBox1.Image);
            Pen pen = new Pen(Color.Green);
            pen.Width = 4;
            KirkpatrickSeidel();
            graphics.DrawPolygon(pen, hullPoints.ToArray());
            pictureBox1.Invalidate();
        }

        private void clear_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Graphics graphics = Graphics.FromImage(pictureBox1.Image);
            graphics.Clear(Color.White);
            points.Clear();
            hullPoints.Clear();
        }
    }
}
