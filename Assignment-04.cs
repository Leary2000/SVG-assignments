using System;
using System.Collections.Generic;

//OS Windows 10
//VSCode version 1.73.1

namespace MyCommand
{

    class Program
    {
        // The Canvas (Receiver) class - holds a list of shapes (model)
        // Note - creating this class to hid how it is implemented and provide 
        //        add and remove methods (which are just push and pop operations)
        public class Canvas
        {

            
            // Use a stack here only because we are working with Stack<T> classes
            // I tend to prefer List<T> classes and I have control over manipulating
            // the data structure - however, the stack data structure works fine here
            private Stack<Shape> canvas = new Stack<Shape>();


            public void Add(Shape s)
            {
                canvas.Push(s);
                Console.WriteLine("Added Shape to canvas: {0}" + Environment.NewLine, s);
            }
            public Shape Remove()
            {
                Shape s = canvas.Pop();
                Console.WriteLine("Removed Shape from canvas: {0}" + Environment.NewLine, s);
                return s;
            }

            public Canvas()
            {
                Console.WriteLine("\nCreated a new Canvas!"); Console.WriteLine();
            }

            public override string ToString()
            {
                String str = "";
               
                for(int i = 0; i < canvas.Count; i++)
                {
                    //stops a new line being added if last shape in canvas
                    if( i == canvas.Count - 1)
                    str = str + canvas.ElementAt(i);
                    
                    else
                    str = str + canvas.ElementAt(i) + Environment.NewLine;
                }
                return str;
            }

        }

        // Abstract Shape class 
        public abstract class Shape
        {
            public override string ToString()
            {
                return "Shape!";
            }
        }

        // Circle Shape class
        public class Circle : Shape
        {

            public int X { get; private set; }
            public int Y { get; private set; }
            public int R { get; private set; }

            public Circle(int x, int y, int r)
            {
                X = x; Y = y; R = r;
            }

            public override string ToString()
            {
                return "<circle cx =" + "\"" + this.X + "\"" + " cy=" + "\"" + this.Y + "\"" + " r=" + "\"" + this.R +"\"" + " />";
            }
        }

         public class Square : Shape
        {
           

            public int X { get; private set; }
            public int Y { get; private set; }
            public int H { get; private set; }
            public int W { get; private set; }


            public Square(int x, int y, int h, int w)
            {
                X = x; Y = y; H = h; W = w;
            }

            
            public override string ToString()
            {
                return "<square cx =" + "\"" + this.X + "\"" + " cy=" + "\"" + this.Y + "\"" + " width=" + "\"" + this.W +  "\"" +" height=" + "\"" + this.H +"\"" + " />";
                
            }
        }

         public class Ellipse : Shape
        {

            public int X { get; private set; }
            public int Y { get; private set; }
            public int RX { get; private set; }
            public int RY { get; private set; }


            public Ellipse(int x, int y, int rx, int ry)
            {
                X = x; Y = y; RX = rx; RY = ry;
            }

            public override string ToString()
            {
                return "<ellipse cx =" + "\"" + this.X + "\"" + " cy=" + "\"" + this.Y + "\"" + " rx=" + "\"" + this.RX +  "\"" +" ry=" + "\"" + this.RY +"\"" + " />";
                
            }
        }

         public class Line : Shape
        {

            public int X1 { get; private set; }
            public int Y1 { get; private set; }
            public int X2 { get; private set; }
            public int Y2 { get; private set; }


            public Line(int x1, int y1, int x2, int y2)
            {
                X1 = x1; Y1 = y1; X2 = x2; Y2 = y2;
            }

            public override string ToString()
            {
                return "<line x1 =" + "\"" + this.X1 + "\"" + " y1=" + "\"" + this.Y1 + "\"" + " x2=" + "\"" + this.X2 +  "\"" +" Y2=" + "\"" + this.Y2 +"\"" + " />";
                
            }
        }

         public class PolyLine : Shape
        {

            public int X { get; private set; }
            public int Y { get; private set; }
            public string POINTS { get; private set; }


            public PolyLine(int x, string points, int y)
            {
                X = x; POINTS = points; Y = y;;
            }

            public override string ToString()
            {
                return "<polyline points=\"" + this.X + "," + this.POINTS +" ," + this.Y + "\"" +" />";
                
            }
        }

        public class Polygon : Shape
        {

            public int X { get; private set; }
            public int Y { get; private set; }
            public string POINTS { get; private set; }


            public Polygon(int x, string points, int y)
            {
                X = x; POINTS = points; Y = y;;
            }

            public override string ToString()
            {
                return "<polygon points=\"" + this.X + "," + this.POINTS +" ," + this.Y +"\"" +" />";
                
            }
        }

         public class Path : Shape
        {

          
            public string POINTS { get; private set; }


            public Path(string points)
            {
                POINTS = points;
            }

            public override string ToString()
            {
                return "<path points=\"" + this.POINTS +"\"" + " />";
                
            }
        }

        // The User (Invoker) Class
        public class User
        {
            private Stack<Command> undo;
            private Stack<Command> redo;

            public int UndoCount { get => undo.Count; }
            public int RedoCount { get => redo.Count; }
            public User()
            {
                Reset();
                Console.WriteLine("Created a new User!");
            }
            public void Reset()
            {
                undo = new Stack<Command>();
                redo = new Stack<Command>();
            }

            public void Action(Command command)
            {
                // first update the undo - redo stacks
                undo.Push(command);  // save the command to the undo command
                redo.Clear();        // once a new command is issued, the redo stack clears

                // next determine  action from the Command object type
                // this is going to be AddShapeCommand or DeleteShapeCommand
                Type t = command.GetType();
                if (t.Equals(typeof(AddShapeCommand)))
                {
                    Console.WriteLine("Command Received: Add new Shape!" + Environment.NewLine);
                    command.Do();
                }
                if (t.Equals(typeof(DeleteShapeCommand)))
                {
                    Console.WriteLine("Command Received: Delete last Shape!" + Environment.NewLine);
                    command.Do();
                }
            }

            // Undo
            public void Undo()
            {
                Console.WriteLine("Undoing operation!"); Console.WriteLine();
                if (undo.Count > 0)
                {
                    Command c = undo.Pop(); c.Undo(); redo.Push(c);
                }
            }

            // Redo
            public void Redo()
            {
                Console.WriteLine("Redoing operation!"); Console.WriteLine();
                if (redo.Count > 0)
                {
                    Command c = redo.Pop(); c.Do(); undo.Push(c);
                }
            }

        }


        // Abstract Command (Command) class - commands can do something and also undo
        public abstract class Command
        {
            public abstract void Do();     // what happens when we execute (do)
            public abstract void Undo();   // what happens when we unexecute (undo)
        }


        // Add Shape Command - it is a ConcreteCommand Class (extends Command)
        // This adds a Shape (Circle) to the Canvas as the "Do" action
        public class AddShapeCommand : Command
        {
            Shape shape;
            Canvas canvas;

            public AddShapeCommand(Shape s, Canvas c)
            {
                shape = s;
                canvas = c;
            }

            // Adds a shape to the canvas as "Do" action
            public override void Do()
            {
                canvas.Add(shape);
            }
            // Removes a shape from the canvas as "Undo" action
            public override void Undo()
            {
                shape = canvas.Remove();
            }

        }

        // Delete Shape Command - it is a ConcreteCommand Class (extends Command)
        // This deletes a Shape (Circle) from the Canvas as the "Do" action
        public class DeleteShapeCommand : Command
        {

            Shape shape;
            Canvas canvas;

            public DeleteShapeCommand(Canvas c)
            {
                canvas = c;
            }

            // Removes a shape from the canvas as "Do" action
            public override void Do()
            {
                shape = canvas.Remove();
            }

            // Restores a shape to the canvas as an "Undo" action
            public override void Undo()
            {
                canvas.Add(shape);
            }
        }


        static void createFile(Canvas canvas)
        {
            //Creates file in local folder called Canvas.svg
            String path = @"Canvas.svg";
            Console.WriteLine("Creating " + path + " file.");

            //Will delete the file if it already exists
            if(File.Exists(path))
            File.Delete(path);

             using (StreamWriter fs = File.CreateText(path))    
                {  
                    //Adds svg tags to start and end of file
                    fs.WriteLine("<svg viewBox= \"0 0 500 500\" xmlns= \"http://www.w3.org/2000/svg\" >");

                    //Get canvas shapes and add to file
                    fs.WriteLine(canvas.ToString());

                    fs.WriteLine("</svg>");
                }
        }

         static void addShape(string shape,Canvas canvas, User user)
        {
            Random rnd = new Random();

            //Used for shapes that have a path element
            string RandomPath = "";

            for(int i = 0; i < rnd.Next(1,6); i++)
            {
                RandomPath = RandomPath + (rnd.Next(10,100) + " " + rnd.Next(10,100) + ",");
            }

            switch(shape.ToLower())
            {
                case "circle":
                user.Action(new AddShapeCommand(new Circle(rnd.Next(1, 200), rnd.Next(1, 200), rnd.Next(1, 200)), canvas));
                break;

                case "square":
                user.Action(new AddShapeCommand(new Square(rnd.Next(1, 200), rnd.Next(1, 200), rnd.Next(1, 400), rnd.Next(1, 500)), canvas));
                break;

                case "ellipse":
                user.Action(new AddShapeCommand(new Ellipse(rnd.Next(1, 300), rnd.Next(1, 200), rnd.Next(1, 300), rnd.Next(1, 400)), canvas));
                break;

                case "line":
                user.Action(new AddShapeCommand(new Line(rnd.Next(1, 500), rnd.Next(1, 500), rnd.Next(1, 500), rnd.Next(1, 500)), canvas));
                break;

                case "polyline":
                user.Action(new AddShapeCommand(new PolyLine(rnd.Next(1, 500), RandomPath, rnd.Next(1, 500)), canvas));
                break;                

                case "polygon":
                user.Action(new AddShapeCommand(new Polygon(rnd.Next(1, 200), RandomPath, rnd.Next(1, 300)), canvas));
                break;

                case "path":
                user.Action(new AddShapeCommand(new Path(RandomPath), canvas));
                break;
            }
        }

        //
        // Entry point into application
        //
        static void Main()
        {
            Console.Clear();
            bool exit = false;
            // Create a Canvas which will hold the list of shapes drawn on canvas
            Canvas canvas = new Canvas();

            // Create user and allow user actions (add and delete) shapes to a canvas
            User user = new User();

            //user.Action(new DeleteShapeCommand(canvas));
            
            //While loop until users enters Q
            Console.WriteLine("Enter H to see commands");
               while(exit == false)
            {

            String[] choice = Console.ReadLine().Split(' ');
            switch (choice[0])
            {

            case "H":
            Console.WriteLine("Commands:");
            Console.WriteLine("       -H    Help");
            Console.WriteLine("       -A    Add <shape>");
            Console.WriteLine("       -U    Undo");
            Console.WriteLine("       -R    Redo");
            Console.WriteLine("       -D    Display canvas");
            Console.WriteLine("       -C    Clear canvas");
            Console.WriteLine("       -Q    Quit ");
            break;

            case "A":
            addShape(choice[1],canvas,user);
            break;

            case "U":
            user.Undo();
            break;

            case "R":
            user.Redo();
            break;

            case "D":
            Console.WriteLine(canvas.ToString());
            break;

            case "Q":
            Console.WriteLine("Goodbye!");    
            createFile(canvas);
            exit = true;
            break;

            }
           
        }

        }
    }


}