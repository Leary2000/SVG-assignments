using System;
class SVGModels {
   static void Main() {
      bool exit = false;
      //string canvas;
      List < object > canvas = new List < object > ();
      canvas.Add("<svg viewBox= \"0 0 300 100\" xmlns= \"http://www.w3.org/2000/svg\" >");

      Console.Clear();

      //loop until user selects exit
      while (exit == false) {

         Console.WriteLine("1) Create Circle");
         Console.WriteLine("2) Create Square");
         Console.WriteLine("3) Create Ellipse");
         Console.WriteLine("4) Create Line");
         Console.WriteLine("5) Create PolyLine");
         Console.WriteLine("6) Create Polygon");
         Console.WriteLine("7) Create Path");
         Console.WriteLine("8) Update Style");
         Console.WriteLine("9) Update SVG");
         Console.WriteLine("10) Swap Elements");
         Console.WriteLine("11) Delete Element");
         Console.WriteLine("12) Exit");
         Console.WriteLine();
         Console.WriteLine("---------------------");
         Console.WriteLine();

         int choice = int.Parse(Console.ReadLine());
         //switch statment to will create object based off choice var
         switch (choice) {
         case 1:
            canvas.Add(makeCircle());
            Console.WriteLine();

            break;

         case 2:
            canvas.Add(makeSquare());
            Console.WriteLine();
            break;

         case 3:
            canvas.Add(makeEllipse());
            Console.WriteLine();
            break;

         case 4:
            canvas.Add(makeLine());
            Console.WriteLine();
            break;

         case 5:
            canvas.Add(makePolyLine());
            Console.WriteLine();
            break;

         case 6:
            canvas.Add(makePolygon());
            Console.WriteLine();
            break;

         case 7:

            canvas.Add(makePath());
            Console.WriteLine();
            break;


        //Style update
         case 8:
            Console.WriteLine("Choose element to update.");
            for (int i = 1; i < canvas.Count; i++)
               Console.WriteLine(i + " - " + canvas[i]);

            int update2 = int.Parse(Console.ReadLine());
            updateStyle(canvas, update2);
            Console.WriteLine();
            break;


        //update svg
         case 9:
            Console.WriteLine("Choose element to update.");
            for (int i = 1; i < canvas.Count; i++)
               Console.WriteLine(i + " - " + canvas[i]);
            int update = int.Parse(Console.ReadLine());
            updateSVG(canvas, update);
            Console.WriteLine();
            break;

        //swap elements
         case 10:
            for (int i = 1; i < canvas.Count; i++)
               Console.WriteLine(i + " - " + canvas[i]);
            int a = int.Parse(Console.ReadLine());
            int b = int.Parse(Console.ReadLine());
            swapElement(canvas, a, b);
            Console.WriteLine();
            break;


        //delete
         case 11:
            Console.WriteLine("Select element to Delete:");
            for (int i = 1; i < canvas.Count; i++)
               Console.WriteLine(i + " - " + canvas[i]);

            int delete = int.Parse(Console.ReadLine());
            if (delete != 0 | delete != canvas.Count)
               DeleteElement(canvas, delete);
            else
               Console.WriteLine("error, please try again");

            Console.WriteLine();
            break;

        //stops loop 
         case 12:
            exit = true;
            canvas.Add("</svg>");
            Console.WriteLine("Exited");
            break;
         }
      }

      //end
      //prints out canvas
      canvas.ForEach(Console.WriteLine);

      //turns canvas into a file
      createFile(canvas);
   }

   public static void swapElement(List < object > list, int a, int b) {
      object temp = list[a];
      list[a] = list[b];
      list[b] = temp;

   }

   public static void updateSVG(List < object > canvas, int a) {
      string old = canvas[a].ToString();
      if (old.Contains("circle")) {
         canvas[a] = makeCircle();
      } else if (old.Contains("polyline")) {
         canvas[a] = makePolyLine();
      } else if (old.Contains("rect")) {
         canvas[a] = makeSquare();
      } else if (old.Contains("ellipse")) {
         canvas[a] = makeEllipse();
      } else if (old.Contains("line")) {
         canvas[a] = makeLine();
      } else if (old.Contains("polygon")) {
         canvas[a] = makePolygon();
      } else if (old.Contains("path")) {
         canvas[a] = makePath();
      }

   }

   public static void updateStyle(List < object > list, int a) {
      string[] l = list[a].ToString().Split("stroke");

      Console.WriteLine();
      Console.Write("New stroke: ");
      string stroke = Console.ReadLine();

      Console.WriteLine();
      Console.Write("New fill: ");
      string fill = Console.ReadLine();

      Console.WriteLine();
      Console.Write("New width: ");
      string width = Console.ReadLine();

      //append new stroke and fill onto end of first part of the svg string

      list[a] = l[0] + " stroke=\"" + stroke + "\" " + "stroke-width = \"" + width + "\"" + " fill= \"" + fill + "\"" + "/>";

   }

   public static void DeleteElement(List < object > list, int a) {
      for (int i = a; i < list.Count; i++) {
         if ((i + 1) != list.Count)
            list[i] = list[i + 1];

         else
            list.RemoveAt(i);
      }

   }

   static void createFile(List < object > canvas) {
      String path = @"..\svgmodels\test.svg";

      if (File.Exists(path))
         File.Delete(path);

      using(StreamWriter fs = File.CreateText(path)) {

         canvas.ForEach(fs.WriteLine);
      }
   }

   class Circle {
      public int x {
         get;
         set;
      }
      public int y {
         get;
         set;
      }
      public int radius {
         get;
         set;
      }

      public string stroke {
         get;
         set;
      }
      public string fill {
         get;
         set;
      }
      public Circle(int x, int y, int r) {
         this.x = x;
         this.y = y;
         this.radius = r;
         this.stroke = "yellow";
         this.fill = "yellow";
      }

      public string printShape() {

         string svg = "<circle cx =" + "\"" + this.x + "\"" + " cy=" + "\"" + this.y + "\"" + " r=" + "\"" + this.radius + "\"" + " stroke=\"" + this.stroke + "\"" + " stroke-width=\"4\"" + " fill=\"" + this.stroke + "\"" + "/>";

         return svg;
      }

   }

   class Square {
      private int x {
         get;
         set;
      }
      private int y {
         get;
         set;
      }

      private int height {
         get;
         set;
      }
      private int width {
         get;
         set;
      }

      public Square(int x, int y, int h, int w) {
         this.x = x;
         this.y = y;
         this.height = h;
         this.width = w;
      }

      public string printShape() {
         string svg = "<rect x =" + "\"" + this.x + "\"" + " y=" + "\"" + this.y + "\"" + " width=" + "\"" + this.width + "\"" + " height=" + "\"" + this.height + "\"" + " stroke=\"grey\"" + " stroke-width=\"1\"" + " fill=\"grey\"" + " />";
         return svg;
      }

   }

   class Ellipse {
      private int x {
         get;
         set;
      }
      private int y {
         get;
         set;
      }
      private int rx {
         get;
         set;
      }
      private int ry {
         get;
         set;
      }

      public Ellipse(int x, int y, int rx, int ry) {
         this.x = x;
         this.y = y;
         this.rx = rx;
         this.ry = ry;

      }

      public string printShape() {
         string svg = "<ellipse cx =" + "\"" + this.x + "\"" + " cy=" + "\"" + this.y + "\"" + " rx=" + "\"" + this.rx + "\"" + " ry=" + "\"" + this.ry + "\"" + " stroke=\"blue\"" + " stroke-width=\"1\"" + " fill=\"blue\"" + " />";
         return svg;
      }

   }

   class Line {
      private int x1 {
         get;
         set;
      }
      private int y1 {
         get;
         set;
      }
      private int x2 {
         get;
         set;
      }
      private int y2 {
         get;
         set;
      }

      public Line(int x1, int y1, int x2, int y2) {
         this.x1 = x1;
         this.y1 = y1;
         this.x2 = x2;
         this.y2 = y2;
      }

      public string printShape() {
         string svg = "<line x1 =" + "\"" + this.x1 + "\"" + " y1=" + "\"" + this.y1 + "\"" + " x2=" + "\"" + this.x2 + "\"" + " y2=" + "\"" + this.y2 + "\"" + " stroke=\"black\"" + " stroke-width=\"1\"" + " fill=\"black\"" + " />";
         return svg;
      }

   }

   class PolyLine {
      private int x {
         get;
         set;
      }
      private string points {
         get;
         set;
      }
      private int y {
         get;
         set;
      }

      public PolyLine(int x, string points, int y) {
         this.x = x;
         this.points = points;
         this.y = y;
      }

      public string printShape() {
         string svg = "<polyline points=\"" + this.x + "," + this.points + "," + this.y + "\" stroke=\"cyan\"" + " stroke-width=\"1\"" + " fill=\"cyan\"" + " />";
         return svg;
      }
   }

   class Polygon {
      private int x {
         get;
         set;
      }
      private string points {
         get;
         set;
      }
      private int y {
         get;
         set;
      }

      public Polygon(int x, string points, int y) {
         this.x = x;
         this.points = points;
         this.y = y;
      }

      public string printShape() {
         string svg = "<polygon points=\"" + this.x + "," + this.points + "," + this.y + "\" stroke=\"red\"" + " stroke-width=\"1\"" + " fill=\"pink\"" + " />";
         return svg;
      }
   }

   class Path {
      private string points {
         get;
         set;
      }

      public Path(string points) {
         this.points = points;
      }

      public string printShape() {
         string svg = "<path d=\"" + this.points + "\" stroke=\"red\"" + " stroke-width=\"1\"" + " fill=\"pink\"" + " />";
         return svg;
      }
   }


    //makes shape and returns a string
   public static string makeCircle() {
      Console.Write("x: ");
      int x = int.Parse(Console.ReadLine());
      Console.Write("y: ");
      int y = int.Parse(Console.ReadLine());
      Console.Write("r: ");
      int r = int.Parse(Console.ReadLine());
      Circle myCircle = new Circle(x, y, r);
      return myCircle.printShape();
   }
   public static string makeSquare() {
      Console.Write("x: ");
      int x = int.Parse(Console.ReadLine());
      Console.Write("y: ");
      int y = int.Parse(Console.ReadLine());
      Console.Write("width: ");
      int width = int.Parse(Console.ReadLine());
      Console.Write("height: ");
      int height = int.Parse(Console.ReadLine());
      Square mySquare = new Square(x, y, width, height);
      return mySquare.printShape();
   }

   public static string makeEllipse() {
      Console.Write("x: ");
      int x = int.Parse(Console.ReadLine());
      Console.Write("y: ");
      int y = int.Parse(Console.ReadLine());
      Console.Write("rx: ");
      int rx = int.Parse(Console.ReadLine());
      Console.Write("ry: ");
      int ry = int.Parse(Console.ReadLine());
      Ellipse myEllipse = new Ellipse(x, y, rx, ry);
      return myEllipse.printShape();
   }

   public static string makeLine() {
      Console.Write("x1: ");
      int x1 = int.Parse(Console.ReadLine());
      Console.Write("y1: ");
      int y1 = int.Parse(Console.ReadLine());
      Console.Write("x2: ");
      int x2 = int.Parse(Console.ReadLine());
      Console.Write("y2: ");
      int y2 = int.Parse(Console.ReadLine());
      Line myLine = new Line(x1, y1, x2, y2);
      return myLine.printShape();
   }

   public static string makePolyLine() {
      Console.Write("x: ");
      int x = int.Parse(Console.ReadLine());
      Console.WriteLine("print as \"1 2, 3 4, 5 6, 7 8, 9 10 \"");
      Console.Write("points: ");
      string points = (Console.ReadLine());
      Console.WriteLine("y: ");
      int y = int.Parse(Console.ReadLine());
      PolyLine myPolyLine = new PolyLine(x, points, y);
      return myPolyLine.printShape();
   }

   public static string makePolygon() {
      Console.Write("x: ");
      int x = int.Parse(Console.ReadLine());
      Console.WriteLine("print as \"1 2, 3 4, 5 6, 7 8, 9 10 \"");
      Console.Write("points: ");
      string points = (Console.ReadLine());
      Console.WriteLine("y: ");
      int y = int.Parse(Console.ReadLine());
      Polygon myPolygon = new Polygon(x, points, y);
      return myPolygon.printShape();
   }

   public static string makePath() {
      Console.WriteLine("Example input: M 100 350 l 150 -300");
      string points = (Console.ReadLine());
      Path myPath = new Path(points);
      return myPath.printShape();
   }

}