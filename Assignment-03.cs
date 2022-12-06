using System;
    class App
    {
        static void Main ()
        {            
            Console.Clear();

            //will exit loop and quit program when set to true
            bool exit = false;

            List<object> canvas = new List<object>();
            Originator originator = new Originator();
            Caretaker caretaker = new Caretaker();

            canvas.Add("<svg viewBox= \"0 0 300 100\" xmlns= \"http://www.w3.org/2000/svg\" >");
            Console.WriteLine("Canvas created - use commands to add shapes to the canvas	");

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
            addShape(choice[1],canvas);
            //save current canvas

            originator.SetCanvas(canvas);
            //add memento of current canvas to the caretaker
            caretaker.add(originator.CreateMemento());

            //clear redo list when a new shape has been added
            caretaker.ClearRedo();
            break;

            case "U":
            try
            {
            caretaker.undo();
            canvas = caretaker.getMemento().getCanvas();
            }
            catch(Exception err)
            {
                //if Undo list is empty
                Console.WriteLine("Cannot Undo");
            }
            break;

            case "R":
            try{

            caretaker.redo();
            canvas = caretaker.getMemento().getCanvas();
            }
             catch(Exception err)
            {
               //if Redo list is empty
                Console.WriteLine("Cannot Redo");
            }
            break;

            case "C":
            canvas.Clear();
            //add svg line to cleared canvas
            canvas.Add("<svg viewBox= \"0 0 300 100\" xmlns= \"http://www.w3.org/2000/svg\" >");
            //save current canvas
            originator.SetCanvas(canvas);
            caretaker.add(originator.CreateMemento());
            break;

            case "D":
            canvas.ForEach(Console.WriteLine);
            Console.WriteLine("</svg>");
            break;

            case "Q":
            Console.WriteLine("Goodbye!");
            canvas = caretaker.getMemento().getCanvas();
            canvas.Add("</svg>");
            canvas.ForEach(Console.WriteLine);
            exit = true;
            break;

            }
           
        }

            //end
            //creates a file containing the final state of canvas
           createFile(canvas);
    }

   
        static void addShape(string shape,List<object> canvas)
        {
            Random r = new Random();
            string RandomPath = "";

            for(int i = 0; i < r.Next(1,6); i++)
            {
                RandomPath = RandomPath + (r.Next(10,100) + " " + r.Next(10,100) + ",");
            }

            switch(shape.ToLower())
            {
                case "circle":
                Circle myCircle = new Circle(r.Next(10,100),r.Next(10,100),r.Next(10,100));
                canvas.Add(myCircle.printShape());
                break;


                case "square":
                Square mySquare = new Square(r.Next(10,100),r.Next(10,100),r.Next(10,100),r.Next(10,100));
                canvas.Add(mySquare.printShape());
                break;

                case "ellipse":
                Ellipse myEllipse = new Ellipse(r.Next(10,100),r.Next(10,100),r.Next(10,100),r.Next(10,100));
                canvas.Add(myEllipse.printShape());
                break;

                case "line":
                Line myLine = new Line(r.Next(10,100),r.Next(10,100),r.Next(10,100),r.Next(10,100));
                canvas.Add(myLine.printShape());
                break;

                case "polyline":
                PolyLine myPolyLine = new PolyLine(r.Next(10,100),RandomPath,r.Next(10,100));
                canvas.Add(myPolyLine.printShape());
                break;                

                case "polygon":
                Polygon myPolygon = new Polygon(r.Next(10,100),RandomPath,r.Next(10,100));
                canvas.Add(myPolygon.printShape());
                break;

                case "path":
                Path myPath = new Path(RandomPath);
                canvas.Add(myPath.printShape());
                break;
            }

            Console.WriteLine(shape.ToLower() + " added to canvas");
        }

        static void createFile(List<object> canvas)
        {
            String path = @"Canvas.svg";
          

            if(File.Exists(path))
            File.Delete(path);

             using (StreamWriter fs = File.CreateText(path))    
                {  

                canvas.ForEach(fs.WriteLine);
                }
        }

     
         class Circle
        {
             public int x{get; set;}
             public int y{get; set;}
             public int radius{get; set;}

             public string stroke  {get; set;}
             public string fill{get; set;}
                public Circle(int x, int y, int r)
                {
                    this.x = x;
                    this.y = y;
                    this.radius = r;
                    this.stroke = "yellow";
                    this.fill = "yellow";
                }

                public string printShape()
                {

                    string svg = "<circle cx =" + "\"" + this.x + "\"" + " cy=" + "\"" + this.y + "\"" + " r=" + "\"" + this.radius + "\"" +" stroke=\"" + this.stroke + "\"" + " stroke-width=\"4\"" +  " fill=\"" + this.stroke + "\"" + "/>";

                    return svg;
                }

        }

         class Square
        {
            private int x{get; set;}
            private int y{get; set;}

            private int height{get; set;}
            private int width{get; set;}


                public Square(int x, int y, int h,int w)
                {
                    this.x = x;
                    this.y = y;
                    this.height = h;
                    this.width = w;
                }

                public string printShape()
                {
                    string svg = "<rect x =" +  "\"" + this.x + "\"" + " y=" + "\"" + this.y + "\"" + " width=" + "\"" + this.width + "\"" +" height=" + "\"" +this.height +"\""  +" stroke=\"grey\"" + " stroke-width=\"1\"" +  " fill=\"grey\""+" />";
                    return svg;
                }

        }

         class Ellipse
        {
            private int x {get; set;}
            private int y {get; set;}
            private int rx {get; set;}
            private int ry {get; set;}
           

            public Ellipse(int x, int y ,int rx ,int ry)
            {
                this.x = x;
                this.y = y;
                this.rx = rx;
                this.ry = ry;
    
            }

            public string printShape()
                {
                    string svg = "<ellipse cx =" +  "\"" + this.x +  "\"" + " cy=" + "\"" +  this.y + "\"" + " rx=" +  "\"" + this.rx +  "\"" + " ry=" +  "\"" + this.ry +  "\"" +" stroke=\"blue\"" + " stroke-width=\"1\"" +  " fill=\"blue\""+" />";
                    return svg;
                }

            
        }

         class Line
        {
            private int x1{get; set;}
            private int y1{get; set;}
            private int x2{get; set;}
            private int y2{get; set;}
       


                public Line(int x1, int y1, int x2,int y2)
                {
                    this.x1 = x1;
                    this.y1 = y1;
                    this.x2 = x2;
                    this.y2 = y2;
                }

                public string printShape()
                {
                    string svg = "<line x1 =" +  "\""+ this.x1 +  "\"" +" y1=" +  "\"" + this.y1 +  "\"" + " x2=" +  "\""+ this.x2 +  "\"" + " y2=" +  "\"" + this.y2  +  "\"" +" stroke=\"black\"" + " stroke-width=\"1\"" +  " fill=\"black\""+" />";
                    return svg;
                }

        }

        class PolyLine
        {
            private int x {get; set;}
            private string points {get; set;}
            private int y { get; set;}


            public PolyLine(int x, string points, int y)
            {
                this.x = x;
                this.points = points;
                this.y = y;
            }

             public string printShape()
                {
                    string svg = "<polyline points=\"" + this.x + "," + this.points  + this.y +"\" stroke=\"cyan\"" + " stroke-width=\"1\"" +  " fill=\"cyan\""+" />";
                    return svg;
                }
        }

        class Polygon
        {
            private int x {get; set;}
            private string points {get; set;}
            private int y { get; set;}


            public Polygon(int x, string points, int y)
            {
                this.x = x;
                this.points = points;
                this.y = y;
            }

             public string printShape()
                {
                    string svg = "<polygon points=\"" + this.x + "," + this.points  + this.y +"\" stroke=\"red\"" + " stroke-width=\"1\"" +  " fill=\"pink\""+" />";
                    return svg;
                }
        }

        class Path
        {
            private string points {get; set;}


            public Path(string points)
            {
                this.points = points;
            }

             public string printShape()
                {
                    string svg = "<path d=\"" + this.points + "\" stroke=\"red\"" + " stroke-width=\"1\"" +  " fill=\"pink\""+" />";
                    return svg;
                }
        }


        
        public  class Memento
        {
            private List<object> canvas = new List<object>();

            public Memento(List<object> canvas)
            {
                this.canvas = canvas;
            }

            public List<object> getCanvas()
            {
                return canvas;
            }

        }

        


        public class Originator
        {
            public List<object> canvas;

            public void SetCanvas(List<object> canvas)
            {
                this.canvas = new List<object>(canvas);
            }

            public List<object> GetCanvas()
            {
                return canvas;
            }

            public void setMemento(Memento memento)
            {
                canvas = memento.getCanvas();
            }

             public Memento CreateMemento()
            {
            return new Memento(canvas);
            }


        }



      
        class Caretaker
        {
            private List<Memento> Mementos = new List<Memento>();
            private List<Memento> MementosHistory = new List<Memento>();

            public void add(Memento CurrentMemento)
            {
            Mementos.Add(CurrentMemento);
            }
            public void ClearRedo()
            {
                MementosHistory.Clear();
            }


            public void undo()
            {
                int n = Mementos.Count - 1;
               
                MementosHistory.Add(Mementos[n]);
                Mementos.RemoveAt(n);
               
            }

            public void redo()
            {
                int n = MementosHistory.Count - 1;
                Mementos.Add(MementosHistory[n]);
                MementosHistory.RemoveAt(n);
                
            }

            public Memento getMemento()
            {
                return Mementos[Mementos.Count - 1];
            }
        }
}