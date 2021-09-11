using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Configuration;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using Graphs_1_0;

namespace ConsoleMaker
{
    class Program
    {
        static void Main(string[] args)
        {
            FileStream A;
            BinaryFormatter B;
            GraphBuilder gb = new GraphBuilder();
            GraphElementFactory gef = new GraphElementFactory();
            //gb.buildPart(gef.CreateVertex(30, 40));
            //gb.buildPart(gef.CreateVertex(70, 70));
            //gb.buildPart(gef.CreateEdge(1, 2));
            //Добавить

            gb.buildPart(gef.CreateVertex(1, 70, 180));
            gb.buildPart(gef.CreateVertex(2, 115, 180));
            gb.buildPart(gef.CreateVertex(3, 160, 180));
            gb.buildPart(gef.CreateVertex(4, 130, 115));
            gb.buildPart(gef.CreateVertex(5, 100, 115));
            gb.buildPart(gef.CreateVertex(6, 115, 50));

            gb.buildPart(gef.CreateEdge(1, 2));
            gb.buildPart(gef.CreateEdge(1, 5));
            gb.buildPart(gef.CreateEdge(2, 3));
            gb.buildPart(gef.CreateEdge(2, 4));
            gb.buildPart(gef.CreateEdge(2, 5));
            gb.buildPart(gef.CreateEdge(3, 4));
            gb.buildPart(gef.CreateEdge(4, 5));
            gb.buildPart(gef.CreateEdge(4, 6));
            gb.buildPart(gef.CreateEdge(5, 6));

            A = new FileStream("C:/Users/Lenovo/Documents/Graph1.grap", FileMode.OpenOrCreate);
            B = new BinaryFormatter();
            B.Serialize(A, gb);
            A.Close();

            GraphBuilder gb1 = new GraphBuilder();
            //GraphElementFactory gef1 = new GraphElementFactory();

            gb1.buildPart(gef.CreateVertex(1, 94, 46));
            gb1.buildPart(gef.CreateVertex(2, 121, 73));
            gb1.buildPart(gef.CreateVertex(3, 308, 107));
            gb1.buildPart(gef.CreateVertex(4, 230, 115));
            
            /*gb1.buildPart(gef.CreateVertex(5, 100, 115));
            gb1.buildPart(gef.CreateVertex(6, 115, 50));

            /*gb1.buildPart(gef.CreateEdge(1, 2));
            gb1.buildPart(gef.CreateEdge(1, 5));
            gb1.buildPart(gef.CreateEdge(2, 3));*/

            gb1.buildPart(gef.CreateEdge(2, 4));

            /*gb1.buildPart(gef.CreateEdge(2, 5));
            gb1.buildPart(gef.CreateEdge(3, 4));
            gb1.buildPart(gef.CreateEdge(4, 5));
            gb1.buildPart(gef.CreateEdge(4, 6));
            gb1.buildPart(gef.CreateEdge(5, 6));*/

            A = new FileStream("C:/Users/Lenovo/Documents/Graph2.grap", FileMode.OpenOrCreate);
            B = new BinaryFormatter();
            B.Serialize(A, gb1);
            A.Close();

            Console.WriteLine("Всё прошло хорошо.");
            Console.ReadKey();
        }
    }
}
