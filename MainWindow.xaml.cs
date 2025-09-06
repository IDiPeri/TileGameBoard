using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TileGameBoard;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private Point _dragStartPoint;
    private bool _isDragging = false;
    private UIElement _draggedElement;

    private void DraggableShape_MouseDown(object sender, MouseButtonEventArgs e)
    {
        if (e.LeftButton == MouseButtonState.Pressed)
        {
            _draggedElement = sender as UIElement;
            _dragStartPoint = e.GetPosition(MyCanvas);
            _isDragging = true;
            var captured = _draggedElement.CaptureMouse(); // Capture mouse to ensure MouseMove events are received
            //int i;
            //i = 0;
        }
    }

    private void MyCanvas_MouseMove(object sender, MouseEventArgs e)
    {
        if (_isDragging && e.LeftButton == MouseButtonState.Pressed)
        {
            Point currentPosition = e.GetPosition(MyCanvas);
            double deltaX = currentPosition.X - _dragStartPoint.X;
            double deltaY = currentPosition.Y - _dragStartPoint.Y;

            Canvas.SetLeft(_draggedElement, Canvas.GetLeft(_draggedElement) + deltaX);
            Canvas.SetTop(_draggedElement, Canvas.GetTop(_draggedElement) + deltaY);

            _dragStartPoint = currentPosition; // Update drag start point for continuous dragging
        }
    }

    private void DraggableShape_MouseUp(object sender, MouseButtonEventArgs e)
    {
        if (_isDragging)
        {
            _isDragging = false;
            _draggedElement.ReleaseMouseCapture(); // Release mouse capture
            _draggedElement = null;
        }
    }

    private void MyCanvas_DragOver(object sender, DragEventArgs e)
    {
        // This event is typically used for visual feedback during a drag-and-drop operation
        // when using the built-in WPF DragDrop.DoDragDrop method.
        // For simple shape dragging as implemented here, it's not strictly necessary for movement.
        e.Effects = DragDropEffects.Move; // Indicate that a move operation is possible
    }

    private void MyCanvas_Drop(object sender, DragEventArgs e)
    {
        // This event is also typically used with the built-in WPF DragDrop.DoDragDrop method.
        // For simple shape dragging, the positioning is handled in MouseMove, so this can be empty
        // or used for additional logic if needed (e.g., dropping data from external sources).
        int i;
        i = 0;
    }
}