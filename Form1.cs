using Newtonsoft.Json;

namespace OOP_Project_Third
{
    public partial class Form1 : Form
    {
        private List<Shape> shapes = new List<Shape>();
        private bool paint = false;
        Point previousLocation;
        private Shape currentShape;
        Color selectedColor;
        ColorDialog cd = new ColorDialog();
        private bool eraserMode = false;


        Stack<Action> undoStack = new Stack<Action>();
        Stack<Action> redoStack = new Stack<Action>();


        public delegate void ShapeChangedEventHandler(object sender, ShapeChangedEventArgs e);
        public event ShapeChangedEventHandler CommandChanged;

        public Form1()
        {
            InitializeComponent();

            this.StartPosition = FormStartPosition.CenterScreen;

            InitializeEventHandlers();

            CommandChanged += Form1_ShapeChanged;
        }
        protected virtual void OnCommandChanged(string actionPerformed, string additionalInfo = "")
        {
            ShapeChangedEventArgs args = new ShapeChangedEventArgs(actionPerformed + " " + additionalInfo);
            CommandChanged?.Invoke(this, args);
        }

        private void Form1_ShapeChanged(object sender, ShapeChangedEventArgs e)
        {
            MessageBox.Show($"Action: {e.Message}");
        }

        private void InitializeEventHandlers()
        {
            pic.Paint += Pic_Paint;
            pic.MouseDown += pic_MouseDown;
            pic.MouseMove += pic_MouseMove;
            pic.MouseUp += pic_MouseUp;
            pic.MouseClick += pic_MouseClick;



            undoButton.Click += undoButton_Click;
            redoButton.Click += redoButton_Click;
            fillButton.Click += fillButton_Click;
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pic_MouseDown(object sender, MouseEventArgs e)
        {
            paint = true;
            previousLocation = e.Location;
        }

        private void ellipseButton_Click(object sender, EventArgs e)
        {
            currentShape = new Ellipse(Point.Empty, Color.Black, 0, 0);
        }

        private void rectangleButton_Click(object sender, EventArgs e)
        {
            currentShape = new Rectangle(Point.Empty, Color.Black, 0, 0);
        }

        private void squareButton_Click(object sender, EventArgs e)
        {
            currentShape = new Square(Point.Empty, Color.Black, 0);
        }

        private void pic_MouseMove(object sender, MouseEventArgs e)
        {
            if (paint && currentShape != null)
            {
                currentShape.Location = e.Location;

                if (currentShape is Ellipse ellipse)
                {
                    ellipse.Width = Math.Abs(e.X - previousLocation.X);
                    ellipse.Height = Math.Abs(e.Y - previousLocation.Y);
                }
                else if (currentShape is Rectangle rectangle)
                {
                    rectangle.Width = Math.Abs(e.X - previousLocation.X);
                    rectangle.Height = Math.Abs(e.Y - previousLocation.Y);
                }
                else if (currentShape is Square square)
                {
                    square.Side = Math.Max(Math.Abs(e.X - previousLocation.X), Math.Abs(e.Y - previousLocation.Y));
                }
                pic.Invalidate();
            }
        }

        private void Pic_Paint(object sender, PaintEventArgs e)
        {
            foreach (var shape in shapes)
            {
                shape.Draw(e.Graphics);
                shape.Fill(e.Graphics, selectedColor);
            }
            if (currentShape != null)
            {
                currentShape.Draw(e.Graphics);
            }
        }

        private void pic_MouseUp(object sender, MouseEventArgs e)
        {
            if (paint && currentShape != null)
            {
                shapes.Add(currentShape);
                pic.Invalidate();

                List<Shape> shapesBeforeAction = new List<Shape>(shapes);

                undoStack.Push(() =>
                {
                    shapes.Clear();
                    shapes.AddRange(shapesBeforeAction);
                    pic.Invalidate();
                });

                redoStack.Clear();

                OnCommandChanged("Shape Added", $"Type: {currentShape.GetType().Name}");

                currentShape = null;
            }
            paint = false;
        }

        private void fillButton_Click(object sender, EventArgs e)
        {
            foreach (var shape in shapes)
            {
                shape.Color = selectedColor;
            }
            pic.Invalidate();
        }

        private void colorButton_Click(object sender, EventArgs e)
        {
            if (cd.ShowDialog() == DialogResult.OK)
            {
                selectedColor = cd.Color;
                pic_color.BackColor = cd.Color;
                OnCommandChanged("Color Applied", $"Color: {selectedColor.Name}");
            }
        }

        private void eraserButton_Click(object sender, EventArgs e)
        {
            eraserMode = !eraserMode;
            string mode = eraserMode ? "ON" : "OFF";
            OnCommandChanged("Eraser Mode", mode);

            if (eraserMode)
            {
                pic.Cursor = Cursors.Cross;
            }
            else
            {
                pic.Cursor = Cursors.Default;
            }
        }

        private void pic_MouseClick(object sender, MouseEventArgs e)
        {
            if (eraserMode)
            {
                for (int i = shapes.Count - 1; i >= 0; i--)
                {
                    if (shapes[i].ContainsPoint(e.Location))
                    {
                        List<Shape> shapesBeforeAction = new List<Shape>(shapes);

                        undoStack.Push(() =>
                        {
                            shapes.Clear();
                            shapes.AddRange(shapesBeforeAction);
                            pic.Invalidate();
                        });



                        shapes.RemoveAt(i);
                        pic.Invalidate();

                        // Save the state after deletion for redo
                        var shapesAfterAction = CopyShapesList(shapes);
                        redoStack.Push(() =>
                        {
                            shapes.Clear();
                            shapes.AddRange(shapesAfterAction);
                            pic.Invalidate();
                        });

                        break;
                    }
                }
            }
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            List<Shape> shapesBeforeClear = new List<Shape>(shapes);

            undoStack.Push(() =>
            {
                shapes.Clear();
                shapes.AddRange(shapesBeforeClear);
                pic.Invalidate();
            });

            shapes.Clear();
            Refresh();
        }

        private void Undo()
        {
            if (undoStack.Count > 0)
            {
                List<Shape> currentStateAfterUndo = CopyShapesList(shapes);
                redoStack.Push(() =>
                {
                    shapes.Clear();
                    shapes.AddRange(currentStateAfterUndo);
                    pic.Invalidate();
                });

                Action undoAction = undoStack.Pop();
                undoAction.Invoke();

                pic.Invalidate();
            }
        }

        private void Redo()
        {
            if (redoStack.Count > 0)
            {
                List<Shape> currentStateAfterRedo = CopyShapesList(shapes);
                undoStack.Push(() =>
                {
                    shapes.Clear();
                    shapes.AddRange(currentStateAfterRedo);
                    pic.Invalidate();
                });

                Action redoAction = redoStack.Pop();
                redoAction.Invoke();

                pic.Invalidate();
            }
        }

        private List<Shape> CopyShapesList(List<Shape> original)
        {
            List<Shape> copy = new List<Shape>();
            foreach (Shape shape in original)
            {
                copy.Add((Shape)shape.Clone());
            }
            return copy;
        }


        private void undoButton_Click(object sender, EventArgs e)
        {
            Undo();
        }

        private void redoButton_Click(object sender, EventArgs e)
        {
            Redo();
        }


        private void saveButton_Click(object sender, EventArgs e)
        {
            SaveShapesState();
        }

        private void SaveShapesState()
        {
            var sfd = new SaveFileDialog();
            sfd.Filter = "JSON Files (*.json)|*.json|All Files (*.*)|*.*";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                string json = JsonConvert.SerializeObject(shapes, Formatting.Indented, new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Auto,
                    NullValueHandling = NullValueHandling.Ignore
                });

                File.WriteAllText(sfd.FileName, json);
                MessageBox.Show("Shapes state saved successfully.");
            }
        }

        private void LoadShapesState()
        {
            var ofd = new OpenFileDialog();
            ofd.Filter = "JSON Files (*.json)|*.json|All Files (*.*)|*.*";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string json = File.ReadAllText(ofd.FileName);

                shapes = JsonConvert.DeserializeObject<List<Shape>>(json, new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Auto
                });

                pic.Invalidate();
                MessageBox.Show("Shapes state loaded successfully.");
            }
        }

        private void loadButton_Click(object sender, EventArgs e)
        {
            LoadShapesState();
        }

        private void infoButton_Click(object sender, EventArgs e)
        {
            var shapeSummaries = shapes.Select(shape => $"The {shape.GetType().Name.ToLower()} with area {shape.CalculateArea():0.00}.").ToList();

            double totalArea = shapes.Sum(shape => shape.CalculateArea());

            var largestShape = shapes.OrderByDescending(shape => shape.CalculateArea()).FirstOrDefault();

            var shapeCounts = shapes.GroupBy(shape => shape.GetType().Name)
                                    .Select(group => new { ShapeType = group.Key, Count = group.Count() })
                                    .ToList();

            string message = $"Total area of all shapes: {totalArea:F2}.\n\n" +
                             "Shape Details:\n" + string.Join("\n", shapeSummaries) +
                             (largestShape != null ? $"\n\nLargest shape: {largestShape.GetType().Name} with area {largestShape.CalculateArea():F2}." : "") +
                             "\n\nShape Counts:\n" + string.Join("\n", shapeCounts.Select(sc => $"{sc.ShapeType}: {sc.Count}"));

            MessageBox.Show(message, "Shape Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}