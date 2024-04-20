using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Program
{
    public sealed partial class Form1 : Form
    {
        private readonly List<Point> _points = new ();
        private readonly List<Color> _colors = new (); 
        private readonly Random _random = new ();
        private bool _singleThreadMode = true;
        
        public Form1()
        {
            InitializeComponent();

            DoubleBuffered = false;

            MaximumSize = new Size(Width, Height);
            MinimumSize = new Size(Width, Height);
        }

        private void DrawVertices(Graphics graphics)
        {
            foreach (Point point in _points)
            {
                graphics.FillEllipse(Brushes.Black, point.X - 3, point.Y - 3, 5, 5);
            }
        }
        
        private void Form_Paint(object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            
            if (_points.Count >= 2)
            {
                if (_singleThreadMode)
                {
                    DrawMultiThread(graphics);
                }
                else
                {
                    DrawMultiThread(graphics);
                }
            }
            
            DrawVertices(graphics);
        }
        
        private void Form_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point clickedPoint = e.Location;

                bool pointRemoved = false;

                for (int i = 0; i < _points.Count; i++)
                {
                    if (Distance(clickedPoint.X, clickedPoint.Y, _points[i]) <= 5)
                    {
                        _points.RemoveAt(i);
                        _colors.RemoveAt(i);
                        pointRemoved = true;
                        break;
                    }
                }

                if (!pointRemoved)
                {
                    _points.Add(clickedPoint);
                    _colors.Add(Color.FromArgb(_random.Next(256), _random.Next(256), _random.Next(256)));
                }

                Invalidate();
            }
        }
        
        private void btnRandomPoints_Click(object sender, EventArgs e)
        {
            Clear(sender, e);
            
            //int numberPoints = _random.Next(5, 20);
            int numberPoints = 500;
            for (int i = 0; i < numberPoints; i++)
            {
                _points.Add(new Point(_random.Next(ClientSize.Width), _random.Next(ClientSize.Height)));
                _colors.Add(Color.FromArgb(_random.Next(256), _random.Next(256), _random.Next(256)));
            }

            Invalidate();
        }

        private void DrawSingleThread(Graphics graphics)
        {
            for (int x = 0; x < ClientSize.Width; x++)
            {
                for (int y = 0; y < ClientSize.Height; y++)
                {
                    Point closestPoint = FindClosestPoint(x, y);
                    int index = _points.IndexOf(closestPoint);
                    
                    graphics.FillRectangle(new SolidBrush(_colors[index]), x, y, 1, 1);
                }
            }
        }

        private void DrawMultiThread(Graphics graphics)
        {
            int segmentWidth = ClientSize.Width / Environment.ProcessorCount;
            int segmentHeight = ClientSize.Height;
            
            List<Task> tasks = new List<Task>();

            for (int t = 0; t < Environment.ProcessorCount; t++)
            {
                int startX = t * segmentWidth;
                int endX = (t + 1) * segmentWidth;
                
                Task task = Task.Run(() =>
                {
                    for (int x = startX; x < endX; x++)
                    {
                        for (int y = 0; y < segmentHeight; y++)
                        {
                            Point closestPoint = FindClosestPoint(x, y);
                            int index = _points.IndexOf(closestPoint);
                            
                            lock (graphics) 
                            {
                                graphics.FillRectangle(new SolidBrush(_colors[index]), x, y, 1, 1);
                            }
                        }
                    }
                });
                tasks.Add(task);
            }

            Task.WaitAll(tasks.ToArray());
        }
        
        private Point FindClosestPoint(int x, int y)
        {
            Point closestPoint = _points[0];
            
            double minDistance = Distance(x, y, closestPoint);

            for (int i = 1; i < _points.Count; i++)
            {
                double distance = Distance(x, y, _points[i]);

                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestPoint = _points[i];
                }
            }

            return closestPoint;
        }

        private double Distance(int x1, int y1, Point point)
        {
            return Math.Sqrt(Math.Pow(x1 - point.X, 2) + Math.Pow(y1 - point.Y, 2));
        }
        
        private void Clear(object sender, EventArgs e)
        {
            _points.Clear();
            Invalidate();
        }
        
        private void MultiThread_CheckedChanged(object sender, EventArgs e)
        {
            _singleThreadMode = MultiThread.Checked;
        }
    }
}
