using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace Program
{
    public sealed partial class Form1 : Form
    {
        
        private readonly List<Point> _points = new ();
        private readonly Dictionary<Point, Color> _color = new ();
        private readonly Random _random = new ();
        private bool _singleThreadMode = true;
        
        public Form1()
        {
            InitializeComponent();

            DoubleBuffered = true;

            MaximumSize = new Size(Width, Height);
            MinimumSize = new Size(Width, Height);
        }

        private void DrawVertices(Graphics graphics)
        {
            foreach (Point point in _points)
            {
                graphics.FillEllipse(Brushes.Black, point.X - 3, point.Y - 3, 8, 8);
            }
        }
        
        private void Form_Paint(object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;

            DrawVertices(graphics);

            if (_points.Count >= 2)
            {
                if (_singleThreadMode)
                {
                    DrawSingleThread(graphics);
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
                        pointRemoved = true;
                        break;
                    }
                }

                if (!pointRemoved)
                {
                    _points.Add(clickedPoint);
                    _color[clickedPoint] = GenerateRandomColor();
                }

                Invalidate();
            }
        }
        
        private void btnRandomPoints_Click(object sender, EventArgs e)
        {
            Clear(sender, e);
            
            int numberPoints = _random.Next(5, 20);
            
            for (int i = 0; i < numberPoints; i++)
            {
                _points.Add(new Point(_random.Next(ClientSize.Width), _random.Next(ClientSize.Height)));
                _color[_points[i]] = GenerateRandomColor();
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
                    
                    graphics.FillRectangle(new SolidBrush(_color[closestPoint]), x, y, 1, 1);
                }
            }
        }

        private void DrawMultiThread(Graphics graphics)
        {
            const int numberThreads = 4;
            
            List<Task> tasks = new List<Task>();

            for (int t = 0; t < numberThreads; t++)
            {
                int threadId = t;

                Task task = Task.Run(() =>
                {
                    for (int x = threadId; x < ClientSize.Width; x += numberThreads)
                    {
                        for (int y = 0; y < ClientSize.Height; y++)
                        {
                            Point closestPoint = FindClosestPoint(x, y);
                            
                            lock (graphics)
                            {
                                graphics.FillRectangle(new SolidBrush(_color[closestPoint]), x, y, 1, 1);
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
            int dx = x1 - point.X;
            int dy = y1 - point.Y;

            return Math.Sqrt(dx * dx + dy * dy);
        }

        private Color GenerateRandomColor()
        {
            while (true)
            {
                Color color = Color.FromArgb(_random.Next(256), _random.Next(256), _random.Next(256));

                if (!_color.Values.Contains(color))
                {
                    return color;
                }
            }
        }
        
        private void Clear(object sender, EventArgs e)
        {
            _points.Clear();
            _color.Clear();
            Invalidate();
        }
        
        private void MultiThread_CheckedChanged(object sender, EventArgs e)
        {
            _singleThreadMode = MultiThread.Checked;
            Invalidate();
        }
    }
}
