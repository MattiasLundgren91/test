using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace GuernicaML
{
    public partial class MainWindow : Window
    {
        public bool _isDrawing { get; set; }
        private bool _Lineb = false;
        private bool _Rektb = false;
        private bool _Ellib = false;
        private bool _FreeHandb = false;
        private Point currentPoint;
        Point _start;
        Point _end;
        private MyShape _currShape;


        private enum MyShape
        {
            Line, Ellipse, Rectangle, FreeHand
        }

        public MainWindow()
        {
            InitializeComponent();
        }
        private void LineButton_Click(object sender, RoutedEventArgs e)
        {
            _Lineb = true;
            _Rektb = false;
            _Ellib = false;
            _FreeHandb = false;
            _currShape = MyShape.Line;
        }

        private void EllipseButton_Click(object sender, RoutedEventArgs e)
        {
            _Lineb = false;
            _Rektb = false;
            _Ellib = true;
            _FreeHandb = false;
            _currShape = MyShape.Ellipse;
        }

        private void RectangleButton_Click(object sender, RoutedEventArgs e)
        {
            _Lineb = false;
            _Rektb = true;
            _Ellib = false;
            _FreeHandb = false;
            _currShape = MyShape.Rectangle;
        }

        private void FreeHandButton_click(object sender, RoutedEventArgs e)
        {
            _Lineb = false;
            _Rektb = false;
            _Ellib = false;
            _FreeHandb = true;
            _currShape = MyShape.FreeHand;
        }

        private void Clear_OnClick(object sender, RoutedEventArgs e)
        {
            MyCanvas.Children.Clear();
        }

        private void MyCanvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ((Canvas)sender).ReleaseMouseCapture();

        }

        private void MyCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isDrawing)
                return;

            if (e.LeftButton == MouseButtonState.Pressed && _Lineb.Equals(true))
            {
                Line line = new Line();
                MyCanvas = (Canvas)sender;
                line = MyCanvas.Children.OfType<Line>().LastOrDefault();
                if (line != null)
                {
                    var endPoint = e.GetPosition(MyCanvas);
                    line.X2 = endPoint.X;
                    line.Y2 = endPoint.Y;
                }
                _end = e.GetPosition(MyCanvas);
            }

            if (e.LeftButton == MouseButtonState.Pressed && _Rektb.Equals(true))
            {
                MyCanvas = (Canvas)sender;
                var Rekt = MyCanvas.Children.OfType<Rectangle>().LastOrDefault();
                if (Rekt != null)
                {
                    var endPoint = e.GetPosition(MyCanvas);
                    if (_end.X >= _start.X)
                    {
                        Rekt.SetValue(Canvas.LeftProperty, _start.X);
                        Rekt.Width = _end.X - _start.X;
                    }
                    else
                    {
                        Rekt.SetValue(Canvas.LeftProperty, _end.X);
                        Rekt.Width = _start.X - _end.X;
                    }
                    if (_end.Y >= _start.Y)
                    {
                        Rekt.SetValue(Canvas.TopProperty, _start.Y - 50);
                        Rekt.Height = _end.Y - _start.Y;
                    }
                    else
                    {
                        Rekt.SetValue(Canvas.TopProperty, _end.Y - 50);
                        Rekt.Height = _start.Y - _end.Y;
                    }
                    _end = endPoint;
                }
            }

            if (e.LeftButton == MouseButtonState.Pressed && _Ellib.Equals(true))
            {
                MyCanvas = (Canvas)sender;
                var Eli = MyCanvas.Children.OfType<Ellipse>().LastOrDefault();
                if (Eli != null)
                {
                    var endPoint = e.GetPosition(MyCanvas);
                    if (_end.X >= _start.X)
                    {
                        Eli.SetValue(Canvas.LeftProperty, _start.X);
                        Eli.Width = _end.X - _start.X;
                    }
                    else
                    {
                        Eli.SetValue(Canvas.LeftProperty, _end.X);
                        Eli.Width = _start.X - _end.X;
                    }

                    if (_end.Y >= _start.Y)
                    {
                        Eli.SetValue(Canvas.TopProperty, _start.Y);
                        Eli.Height = _end.Y - _start.Y;
                    }
                    else
                    {
                        Eli.SetValue(Canvas.TopProperty, _end.Y);
                        Eli.Height = _start.Y - _end.Y;
                    }
                    _end = endPoint;
                }
            }

            if (e.LeftButton == MouseButtonState.Pressed && _FreeHandb.Equals(true))
            {
                Line line = new Line();
                line.Stroke = new SolidColorBrush(Colors.Black);
                line.StrokeThickness = 2;
                line.X1 = currentPoint.X;
                line.Y1 = currentPoint.Y;
                line.X2 = e.GetPosition(MyCanvas).X;
                line.Y2 = e.GetPosition(MyCanvas).Y;
                currentPoint = e.GetPosition(MyCanvas);
                MyCanvas.Children.Add(line);
            }
        }

        private void MyCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _isDrawing = true;
            currentPoint = e.GetPosition(MyCanvas);
            _start = e.GetPosition(MyCanvas);
            if (e.ButtonState == MouseButtonState.Pressed && _Lineb.Equals(true))
            {
                var canvas = (Canvas)sender;
                if (canvas.CaptureMouse())
                {
                    var startPoint = e.GetPosition(canvas);
                    var line = new Line
                    {
                        Stroke = Brushes.Green,
                        StrokeThickness = 3,
                        X1 = startPoint.X,
                        Y1 = startPoint.Y,
                        X2 = startPoint.X,
                        Y2 = startPoint.Y,
                    };
                    canvas.Children.Add(line);
                }
            }

            if (e.ButtonState == MouseButtonState.Pressed && _Rektb.Equals(true))
            {
                var canvas = (Canvas)sender;
                if (canvas.CaptureMouse())
                {
                    var startPoint = e.GetPosition(MyCanvas);
                    Rectangle newRectangle = new Rectangle()
                    {
                        Stroke = Brushes.Orange,
                        Fill = Brushes.Blue,
                        StrokeThickness = 2,
                    };
                    Canvas.SetTop(newRectangle, startPoint.Y + startPoint.X);
                    canvas.Children.Add(newRectangle);
                }
            }

            if (e.ButtonState == MouseButtonState.Pressed && _Ellib.Equals(true))
            {
                var canvas = (Canvas)sender;
                if (canvas.CaptureMouse())
                {
                    var startPoint = e.GetPosition(MyCanvas);
                    Ellipse newEllipse = new Ellipse()
                    {
                        Stroke = Brushes.Green,
                        Fill = Brushes.Red,
                        StrokeThickness = 2,
                        Height = 10,
                        Width = 10,
                    };
                    Canvas.SetTop(newEllipse, startPoint.Y);
                    Canvas.SetLeft(newEllipse, startPoint.X);
                    canvas.Children.Add(newEllipse);
                }
            }
            _isDrawing = false;
        }
    }
}

