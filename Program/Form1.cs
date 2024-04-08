using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Program
{
    public sealed partial class Form1 : Form
    {
        private readonly List<Point> _points = new();
        private bool _singleThreadMode = true;

        public Form1()
        {
            InitializeComponent();
            DoubleBuffered = true;
        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            // Малювання вершин
            foreach (Point p in _points)
            {
                g.FillEllipse(Brushes.Black, p.X - 3, p.Y - 3, 8, 8);
            }

            // Побудова діаграми Вороного
            if (_points.Count >= 2)
            {
                if (_singleThreadMode)
                    DrawVoronoiDiagramSingleThread(g);
                else
                    DrawVoronoiDiagramMultiThread(g);
            }
        }

        // Додавання вершини за допомогою кліка миші
        // Додавання або видалення вершини за допомогою кліка миші
        private void MainForm_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point clickedPoint = e.Location;
                bool pointRemoved = false;

                // Перевіряємо, чи клікнуто біля існуючої вершини
                for (int i = 0; i < _points.Count; i++)
                {
                    if (Distance(clickedPoint.X, clickedPoint.Y, _points[i]) <= 5) // Перевірка на відстань від кліку до вершини
                    {
                        _points.RemoveAt(i);
                        pointRemoved = true;
                        break;
                    }
                }

                // Якщо вершину не видалили, то додаємо нову
                if (!pointRemoved)
                {
                    _points.Add(clickedPoint);
                }

                Invalidate();
            }
        }


        // Генерація випадкових вершин
        private void btnRandomPoints_Click(object sender, EventArgs e)
        {
            Random rand = new Random();
            int numPoints = rand.Next(5, 15);

            _points.Clear();
            for (int i = 0; i < numPoints; i++)
            {
                _points.Add(new Point(rand.Next(ClientSize.Width), rand.Next(ClientSize.Height)));
            }

            Invalidate();
        }

        // Вибір режиму обчислень: однопоточний або багатопоточний
        private void SingleThread_CheckedChanged(object sender, EventArgs e)
        {
            _singleThreadMode = MultiThread.Checked;
            Invalidate();
        }

        // Побудова діаграми Вороного в однопоточному режимі
        private void DrawVoronoiDiagramSingleThread(Graphics g)
        {
            for (int x = 0; x < ClientSize.Width; x++)
            {
                for (int y = 0; y < ClientSize.Height; y++)
                {
                    Point closestPoint = _points[0];
                    double minDistance = Distance(x, y, closestPoint);

                    for (int i = 1; i < _points.Count; i++)
                    {
                        double dist = Distance(x, y, _points[i]);
                        if (dist < minDistance)
                        {
                            minDistance = dist;
                            closestPoint = _points[i];
                        }
                    }

                    g.FillRectangle(new SolidBrush(Color.FromArgb(100, closestPoint.X % 256, closestPoint.Y % 256, 255)), x, y, 1, 1);
                }
            }
        }

        // Побудова діаграми Вороного в багатопоточному режимі
        private void DrawVoronoiDiagramMultiThread(Graphics g)
        {
            const int numThreads = 4;
            
            List<Task> tasks = new List<Task>();

            for (int t = 0; t < numThreads; t++)
            {
                int threadId = t;
                
                Task task = Task.Run(() =>
                {
                    for (int x = threadId; x < ClientSize.Width; x += numThreads)
                    {
                        for (int y = 0; y < ClientSize.Height; y++)
                        {
                            Point closestPoint = _points[0];
                            double minDistance = Distance(x, y, closestPoint);

                            for (int i = 1; i < _points.Count; i++)
                            {
                                double dist = Distance(x, y, _points[i]);
                                if (dist < minDistance)
                                {
                                    minDistance = dist;
                                    closestPoint = _points[i];
                                }
                            }

                            lock (g)
                            {
                                g.FillRectangle(new SolidBrush(Color.FromArgb(100, closestPoint.X % 256, closestPoint.Y % 256, 255)), x, y, 1, 1);
                            }
                        }
                    }
                });
                tasks.Add(task);
            }

            Task.WaitAll(tasks.ToArray());
        }

        // Обчислення відстані між двома точками
        private double Distance(int x1, int y1, Point p2)
        {
            int dx = x1 - p2.X;
            int dy = y1 - p2.Y;
            
            return Math.Sqrt(dx * dx + dy * dy);
        }
    }
}
