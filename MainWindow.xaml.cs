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

        m_AllDraggableShapes.Add(TileShape001);
        m_AllDraggableShapes.Add(TileShape002);
    }

    private List<UIElement> m_AllDraggableShapes = new List<UIElement>();
    private Point m_DragStartPoint;
    private bool m_IsDragging = false;
    private UIElement m_DraggedElement;

    private void BringDraggingShapeToFront(UIElement shapeToFocus)
    {
        foreach(var shape in m_AllDraggableShapes)
        {
            if (shape == shapeToFocus)
            {
                // Set selected shape to the highest Z
                Canvas.SetZIndex(shape, 2);
            }
            else
            {
                // Set all other shapes to the lowest Z
                Canvas.SetZIndex(shape, 1);
            }
        }
    }

    private void DraggableShape_MouseDown(object sender, MouseButtonEventArgs e)
    {
        if (e.LeftButton == MouseButtonState.Pressed)
        {
            m_DraggedElement = sender as UIElement;
            m_DragStartPoint = e.GetPosition(MyCanvas);
            m_IsDragging = true;
            var captured = m_DraggedElement.CaptureMouse(); // Capture mouse to ensure MouseMove events are received

            // Make sure the currently dragging item is a higher Z than any other shape on the canvas
            BringDraggingShapeToFront(m_DraggedElement);

            // Set the target location to where the tile started
            Canvas.SetLeft(TargetLocationShape, Canvas.GetLeft(m_DraggedElement));
            Canvas.SetTop(TargetLocationShape, Canvas.GetTop(m_DraggedElement));
            TargetLocationShape.Visibility = Visibility.Visible;
        }
    }

    private void MyCanvas_MouseMove(object sender, MouseEventArgs e)
    {
        if (m_IsDragging && e.LeftButton == MouseButtonState.Pressed)
        {
            Point currentPosition = e.GetPosition(MyCanvas);
            double deltaX = currentPosition.X - m_DragStartPoint.X;
            double deltaY = currentPosition.Y - m_DragStartPoint.Y;

            Canvas.SetLeft(m_DraggedElement, Canvas.GetLeft(m_DraggedElement) + deltaX);
            Canvas.SetTop(m_DraggedElement, Canvas.GetTop(m_DraggedElement) + deltaY);

            m_DragStartPoint = currentPosition; // Update drag start point for continuous dragging
        }
    }

    private void DraggableShape_MouseUp(object sender, MouseButtonEventArgs e)
    {
        if (m_IsDragging)
        {
            m_IsDragging = false;
            m_DraggedElement.ReleaseMouseCapture(); // Release mouse capture
            m_DraggedElement = null;
            TargetLocationShape.Visibility = Visibility.Hidden;
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