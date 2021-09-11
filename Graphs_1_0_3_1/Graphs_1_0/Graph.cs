using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Graphs_1_0
{
    [Serializable] public class Vertex
    {
        protected int x, y, degree, number;
        //protected List<Edge> incoming;
        //protected List<Edge> outcoming;
        public Vertex(int num)
        {
            number = num;
            //incoming = new List<Edge>();
            //outcoming = new List<Edge>();
        }
        /*public void AddIncoming(Edge ed)
        {
            incoming.Add(ed);
        }
        public void AddOutcoming(Edge ed)
        {
            outcoming.Add(ed);
        }
        public List<Edge> GetIncoming()
        {
            return incoming;
        }
        public List<Edge> GetOutcoming()
        {
            return outcoming;
        }*/
        public void Renum(int i)
        {
            number = i;
        }
        public void SetDegree(int deg)
        {
            degree = deg;
        }
        public int GetDegree()
        {
            return degree;
        }
        public int Number { get { return number; } }
        public int X { get { return x; } set { x = value; } }
        public int Y { get { return y; } set { y = value; } }
        public void Draw(PaintEventArgs e)
        {
            e.Graphics.FillEllipse(new SolidBrush(Color.WhiteSmoke), x, y, 20, 20);
        }
    }
    [Serializable] public class Edge
    {
        protected int i, j;
        public Edge(int a, int b)
        {
            if(a <= b)
            {
                i = a;
                j = b;
            }
            else
            {
                i = b;
                j = a;
            }
        }
        public void SetLessNum(int a)
        {
            if (a <= j)
            {
                i = a;
            }
        }
        public void SetSupNum(int b)
        {
            if (b >= i)
            {
                j = b;
            }
        }
        public int GetLessNum()
        {
            return i;
        }
        public int GetSupNum()
        {
            return j;
        }
    }
    [Serializable] public class SimpleGraph
    {
        private List<Vertex> V;
        private List<Edge> E;
        public SimpleGraph()
        {
            E = new List<Edge>();
            V = new List<Vertex>();
        }
        public void AddVertex(Vertex ver)
        {
            V.Add(ver);
        }
        public void AddEdge(Edge ed)
        {
            int i, k = 0;
            for (i = 0; i < V.Count; i++)
            {
                if (ed.GetLessNum() == V[i].Number)
                {
                    k++;
                    
                }
                if (ed.GetSupNum() == V[i].Number)
                {
                    k++;
                }
            }
            if(k == 2)
            {
                E.Add(ed);
            }
                /*if ((ed.GetLessNum() >= 1) && (ed.GetLessNum() <= V.Count))
                {
                    if (ed.GetSupNum() <= V.Count)
                    {
                        /*V[ed.GetLessNum() - 1].AddIncoming(ed);
                        V[ed.GetLessNum() - 1].AddOutcoming(ed);
                        V[ed.GetSupNum() - 1].AddIncoming(ed);
                        V[ed.GetSupNum() - 1].AddOutcoming(ed);
                        E.Add(ed);
                    }
                }*/
        }
        public int[,] GetConMatrix()
        {
            //Матрица смежности
            int i, j, k;
            int[,] matrix = new int[V.Count, V.Count];
            for(i = 0; i < V.Count; i++)
            {
                for(j = 0; j < V.Count; j++)
                {
                    matrix[i, j] = 0;
                    for(k = 0; k < E.Count; k++)
                    {
                        if(V[i].Number == E[k].GetLessNum())
                        {
                            if(V[j].Number == E[k].GetSupNum())
                            {
                                matrix[i, j] = 1;
                            }
                        }
                        if (V[i].Number == E[k].GetSupNum())
                        {
                            if(V[j].Number == E[k].GetLessNum())
                            {
                                matrix[i, j] = 1;
                            }
                        }
                    }
                }
            }
            return matrix;
        }
        public int[,] GetDistMatrix()
        {
            //Матрица расстояний
            int i, j, k, min;
            int[,] matrix = new int[V.Count, V.Count];
            for (i = 0; i < V.Count; i++)
            {
                for (j = 0; j < V.Count; j++)
                {
                    matrix[i, j] = 0;
                    for (k = 0; k < E.Count; k++)
                    {
                        if (V[i].Number == E[k].GetLessNum())
                        {
                            if (V[j].Number == E[k].GetSupNum())
                            {
                                matrix[i, j] = 1;
                            }
                        }
                        if (V[i].Number == E[k].GetSupNum())
                        {
                            if (V[j].Number == E[k].GetLessNum())
                            {
                                matrix[i, j] = 1;
                            }
                        }
                    }
                }
            }
            for (i = 0; i < V.Count - 1; i++)
            {
                for(j = i + 1; j < V.Count; j++)
                {
                    if(matrix[i, j] == 0)
                    {
                        min = Int32.MaxValue;
                        for(k = 0; k < V.Count; k++)
                        {
                            if(matrix[i, k] > 0)
                            {
                                if(matrix[j, k] > 0)
                                {
                                    if(min > (matrix[i, k] + matrix[j, k]))
                                    {
                                        min = matrix[i, k] + matrix[j, k];
                                        matrix[i, j] = matrix[i, k] + matrix[j, k];
                                        matrix[j, i] = matrix[i, j];
                                    }
                                }
                            }
                        }
                    }
                }
            }
            for (i = V.Count - 1; i > 0; i--)
            {
                for (j = i - 1; j >= 0; j--)
                {
                    if (matrix[i, j] == 0)
                    {
                        min = Int32.MaxValue;
                        for (k = 0; k < V.Count; k++)
                        {
                            if (matrix[i, k] > 0)
                            {
                                if (matrix[j, k] > 0)
                                {
                                    if (min > (matrix[i, k] + matrix[j, k]))
                                    {
                                        min = matrix[i, k] + matrix[j, k];
                                        matrix[i, j] = matrix[i, k] + matrix[j, k];
                                        matrix[j, i] = matrix[i, j];
                                    }
                                }
                            }
                        }
                    }
                }
            }
            for (i = 0; i < V.Count; i++)
            {
                for (j = 0; j < V.Count; j++)
                {
                    if (matrix[i, j] == 0)
                    {
                        if(i != j)
                        {
                            matrix[i, j] = Int32.MaxValue;
                        }
                    }
                }
            }
            return matrix;
        }
        public int[,] GetIncMatrix()
        {
            //Матрица инцидентности
            int i, j;
            int[,] matrix = new int[V.Count, E.Count];
            for (i = 0; i < V.Count; i++)
            {
                for (j = 0; j < E.Count; j++)
                {
                    matrix[i, j] = 0;
                    if(V[i].Number == E[j].GetLessNum())
                    {
                        matrix[i, j] = 1;
                    }
                    if (V[i].Number == E[j].GetSupNum())
                    {
                        matrix[i, j] = 1;
                    }
                }
            }
            return matrix;
        }
        public List<Vertex> GetVertexs()
        {
            return V;
        }
        public List<Edge> GetEdges()
        {
            return E;
        }
        public void SetDegrees()
        {
            //Расчёт степеней вершин
            int i, j, k, v1, v2;
            for(i = 0; i < V.Count; i++)
            {
                k = 0;
                for(j = 0; j < E.Count; j++)
                {
                    v1 = E[j].GetLessNum();
                    v2 = E[j].GetSupNum();
                    if((v1 == V[i].Number) || (v2 == V[i].Number))
                    {
                        k++;
                    }
                }
                V[i].SetDegree(k);
            }
        }
        public void Draw(PaintEventArgs e)
        {
            int i, j, num;
            for (j = 0; j < E.Count; j++)
            {
                e.Graphics.DrawLine(new Pen(Color.Black), V[E[j].GetLessNum() - 1].X, V[E[j].GetLessNum() - 1].Y, V[E[j].GetSupNum() - 1].X, V[E[j].GetSupNum() - 1].Y);
            }
            for (i = 0; i < V.Count; i++)
            {
                V[i].Draw(e);
                num = V[i].Number;
                e.Graphics.DrawString(num.ToString(), new Font("Arial", 10, FontStyle.Regular), new SolidBrush(Color.Black), V[i].X - 15, V[i].Y - 15);
            }
        }
    };
    abstract class GraphCommand
    {
        protected SimpleGraph entgr;
        public abstract void Execute(SimpleGraph sgr);
    }
    class LBFS: GraphCommand
    {
        //Лексикографический поиск в ширину - it's working
        public override void Execute(SimpleGraph sgr)
        {
            int i, curver, max, nextver, start, v1, v2;
            string result = "";
            List<int> sigma = new List<int>();
            List<int> ex = new List<int>();
            List<int> curS = new List<int>();
            List<List<int>> S = new List<List<int>>();
            List<Edge> gre = new List<Edge>();
            List<Vertex> grv = new List<Vertex>();
            entgr = sgr;
            grv = entgr.GetVertexs();
            gre = entgr.GetEdges();
            for(i = 0; i < grv.Count; i++)
            {
                sigma.Add(grv[i].Number);
                S.Add(new List<int>(grv.Count));
            }
            curver = grv.Count / 2; //Задать начальную вершину
            start = StartNumber.GetNumber();
            if((start > 0) && (start <= grv.Count))
            {
                curver = start;
            }
            
            while(sigma.Count > 0)
            {
                sigma.Remove(curver);
                for(i = 0; i < gre.Count; i++)
                {
                    v1 = gre[i].GetLessNum();
                    v2 = gre[i].GetSupNum();
                    if(v1 == curver)
                    {
                        S[v2 - 1].Add(sigma.Count);
                    }
                    if (v2 == curver)
                    {
                        S[v1 - 1].Add(sigma.Count);
                    }
                }
                nextver = 0;
                max = 0;
                for (i = 0; i < sigma.Count; i++)
                {
                    if(S[sigma[i] - 1].Count >= 1)
                    {
                        if(max < S[sigma[i] - 1][0])
                        {
                            max = S[sigma[i] - 1][0];
                            nextver = sigma[i];
                        }
                    }
                }
                //result = result + curver + " - ";
                result = result + curver;
                if (sigma.Count > 0)
                {
                    result = result + " - ";
                }
                curver = nextver;
            }
            Answer.SetAnswer(result);
        }
    }
    class MinDegreeTree : GraphCommand
    {
        //Алгоритм минимальной степени - it's working
        public override void Execute(SimpleGraph sgr)
        {
            int i, mindeg, curver, v1, v2;
            string result = "";
            List<int> sigma = new List<int>();
            List<Edge> gre;
            List<Vertex> grv;
            List<Edge> gre1 = new List<Edge>();
            List<Vertex> grv1 = new List<Vertex>();
            entgr = sgr;
            grv = entgr.GetVertexs();
            gre = entgr.GetEdges();
            for (i = 0; i < gre.Count; i++)
            {
                gre1.Add(gre[i]);
            }
            for (i = 0; i < grv.Count; i++)
            {
                grv1.Add(grv[i]);
            }
            for (i = 0; i < grv1.Count; i++)
            {
                sigma.Add(grv1[i].Number);
            }

            while(sigma.Count > 0)
            {
                mindeg = grv1.Count;
                curver = 0;
                for (i = 0; i < grv1.Count; i++)
                {
                    if (mindeg > grv1[i].GetDegree())
                    {
                        mindeg = grv1[i].GetDegree();
                        curver = i;
                    }
                }
                //result = result + sigma[curver] + " - ";
                result = result + sigma[curver];
                if(sigma.Count > 1)
                {
                    result = result + " - ";
                }
                for (i = 0; i < gre1.Count; i++)
                {
                    v1 = gre1[i].GetLessNum();
                    v2 = gre1[i].GetSupNum();
                    if (v1 == sigma[curver])
                    {
                        grv1[sigma.IndexOf(v2)].SetDegree(grv1[sigma.IndexOf(v2)].GetDegree() - 1);
                    }
                    if (v2 == sigma[curver])
                    {
                        grv1[sigma.IndexOf(v1)].SetDegree(grv1[sigma.IndexOf(v1)].GetDegree() - 1);
                    }
                }
                //Удалить рёбра - сделано
                for (i = 0; i < gre1.Count; i++)
                {
                    v1 = gre1[i].GetLessNum();
                    v2 = gre1[i].GetSupNum();
                    if (v1 == sigma[curver])
                    {
                        gre1.RemoveAt(i);
                        i--;
                    }
                    if (v2 == sigma[curver])
                    {
                        gre1.RemoveAt(i);
                        i--;
                    }
                }
                sigma.RemoveAt(curver);
                grv1.RemoveAt(curver);
            }
            Answer.SetAnswer(result);
        }
    }
    class NestedDissection : GraphCommand
    {
        //Алгоритм вложенного разбиения - 3
        public override void Execute(SimpleGraph sgr)
        {
            int i, j, iter = 0, cir, start;
            string result = "";
            List<int> sigma = new List<int>();
            List<List<int>> S = new List<List<int>>();
            List<Vertex> grv = new List<Vertex>();
            entgr = sgr;
            grv = entgr.GetVertexs();
            cir = grv.Count / 2;
            start = StartNumber.GetNumber();
            if ((start > 0) && (start <= grv.Count))
            {
                cir = start;
            }
            for (i = 0; i < grv.Count; i++)
            {
                sigma.Add(grv[i].Number);
            }
            RecurAlgor1(iter++, sigma, S, cir);
            for(i = 0; i < S.Count; i++)
            {
                result = result + "{";
                for(j = 0; j < S[i].Count; j++)
                {
                    result = result + S[i][j];
                    if(j != (S[i].Count - 1))
                    {
                        result = result + ", ";
                    }
                }
                result = result + "}";
                if(i != (S.Count - 1))
                {
                    result = result + ", ";
                }
            }
            Answer.SetAnswer(result);
        }

        public void RecurAlgor1(int iter, List<int> sig, List<List<int>> S, int cir)
        {
            int i, j, k, deg, maxdeg, v1, v2;
            List<int> A = new List<int>();
            List<int> B = new List<int>();
            List<int> C = new List<int>();
            List<int> degs = new List<int>();
            List<Edge> gre, gre1 = new List<Edge>();
            List<Vertex> grv, grv1 = new List<Vertex>();
            SimpleGraph sg;
            GraphBuilder sgb = new GraphBuilder();
            grv = entgr.GetVertexs();
            gre = entgr.GetEdges();
            for (i = 0; i < grv.Count; i++)
            {
                if (sig.Contains(grv[i].Number) == true)
                {
                    sgb.buildPart(grv[i]);
                }
            }
            for (i = 0; i < gre.Count; i++)
            {
                v1 = gre[i].GetLessNum();
                v2 = gre[i].GetSupNum();
                if ((sig.Contains(v1) == true) && (sig.Contains(v2) == true))
                {
                    sgb.buildPart(gre[i]);
                }
            }
            sg = sgb.GetResult();
            grv1 = sg.GetVertexs();
            gre1 = sg.GetEdges();
            maxdeg = 0;
            for (i = 0; i < grv1.Count; i++)
            {
                deg = grv1[i].GetDegree();
                degs.Add(grv1[i].GetDegree());
                if (maxdeg < deg)
                {
                    maxdeg = deg;
                }
            }
            k = 0;
            i = 0;
            do
            {
                deg = -1;
                for (j = 0; j < grv1.Count; j++)
                {
                    if(grv1[j].Number == sig[i])
                    {
                        deg = grv1[j].GetDegree();
                    }
                }
                if ((deg == 0) || (deg == 1))
                {
                    B.Add(sig[i]);
                    for (j = 0; j < sig.Count; j++)
                    {
                        A.Add(sig[j]);
                    }
                    A.Remove(B[0]);
                    for (j = 0; j < gre1.Count; j++)
                    {
                        v1 = gre1[j].GetLessNum();
                        v2 = gre1[j].GetSupNum();
                        if (v1 == B[0])
                        {
                            if (v1 != v2)
                            {
                                //B.Add(v2);
                                C.Add(v2);
                                A.Remove(v2);
                            }
                        }
                        if (v2 == B[0])
                        {
                            if (v1 != v2)
                            {
                                //B.Add(v1);
                                C.Add(v1);
                                A.Remove(v1);
                            }
                        }
                    }
                    k++;
                }
                else if ((deg == 2) && (maxdeg != 2))
                {
                    C.Add(sig[i]);
                    if (CheckSeparator5(sig, A, B, C) == true)
                    {
                        k++;
                    }
                    if(k == 0)
                    {
                        C.Clear();
                    }
                }
                else
                {
                    if(deg >= maxdeg)
                    {
                        C.Add(sig[i]);
                        if (CheckSeparator5(sig, A, B, C) == true)
                        {
                            k++;
                        }
                        else
                        {
                            maxdeg--;
                            j = 0;
                            do
                            {
                                v1 = gre1[j].GetLessNum();
                                v2 = gre1[j].GetSupNum();
                                if (v1 == C[0])
                                {
                                    if ((v1 != v2) && (degs[sig.IndexOf(v2)] >= maxdeg))
                                    {
                                        C.Add(v2);
                                        if (CheckSeparator5(sig, A, B, C) == true)
                                        {
                                            k++;
                                        }
                                    }
                                }
                                if (v2 == C[0])
                                {
                                    if ((v1 != v2) && (degs[sig.IndexOf(v1)] >= maxdeg))
                                    {
                                        C.Add(v1);
                                        if (CheckSeparator5(sig, A, B, C) == true)
                                        {
                                            k++;
                                        }
                                    }
                                }
                                j++;
                            }
                            while ((C.Count <= (sig.Count / 2)) && (k == 0) && (j < gre1.Count));
                        }
                    }
                    if(k == 0)
                    {
                        C.Clear();
                        maxdeg--;
                    }
                }
                i++;
            }
            while ((k == 0) && (i < sig.Count));
            S.Add(C);
            if (A.Count > 1)
            {
                if(iter < cir)
                {
                    RecurAlgor1(iter++, A, S, cir);
                }
                else
                {
                    S.Add(A);
                }
            }
            else
            {
                S.Add(A);
            }
            if (B.Count > 1)
            {
                if (iter < cir)
                {
                    RecurAlgor1(iter++, B, S, cir);
                }
                else
                {
                    S.Add(B);
                }
            }
            else
            {
                S.Add(B);
            }
        }

        /*protected bool CheckSeparator3(List<int> sig, List<int> A, List<int> B, List<int> C)
        {
            int i, j, k, min;
            int[,] m1, m2;
            bool ans = false;
            List<Vertex> grv;
            List<Edge> gre;
            grv = entgr.GetVertexs();
            gre = entgr.GetEdges();
            m1 = entgr.GetDistMatrix();
            m2 = new int[grv.Count, grv.Count];
            for (i = 0; i < grv.Count; i++)
            {
                for (j = 0; j < grv.Count; j++)
                {
                    m2[i, j] = 0;
                    for (k = 0; k < gre.Count; k++)
                    {
                        if (grv[i].Number == gre[k].GetLessNum())
                        {
                            if (grv[j].Number == gre[k].GetSupNum())
                            {
                                if (C.Contains(gre[k].GetLessNum()) == false)
                                {
                                    if (C.Contains(gre[k].GetSupNum()) == false)
                                    {
                                        m2[i, j] = 1;
                                    }
                                }
                            }
                        }
                        if (grv[i].Number == gre[k].GetSupNum())
                        {
                            if (grv[j].Number == gre[k].GetLessNum())
                            {
                                if (C.Contains(gre[k].GetLessNum()) == false)
                                {
                                    if (C.Contains(gre[k].GetSupNum()) == false)
                                    {
                                        m2[i, j] = 1;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            for (i = 0; i < grv.Count - 1; i++)
            {
                for (j = i + 1; j < grv.Count; j++)
                {
                    if (m2[i, j] == 0)
                    {
                        min = Int32.MaxValue;
                        for (k = 0; k < grv.Count; k++)
                        {
                            if (m2[i, k] > 0)
                            {
                                if (m2[j, k] > 0)
                                {
                                    if (min > (m2[i, k] + m2[j, k]))
                                    {
                                        min = m2[i, k] + m2[j, k];
                                        m2[i, j] = m2[i, k] + m2[j, k];
                                        m2[j, i] = m2[i, j];
                                    }
                                }
                            }
                        }
                    }
                }
            }
            for (i = grv.Count - 1; i > 0; i--)
            {
                for (j = i - 1; j >= 0; j--)
                {
                    if (m2[i, j] == 0)
                    {
                        min = Int32.MaxValue;
                        for (k = 0; k < grv.Count; k++)
                        {
                            if (m2[i, k] > 0)
                            {
                                if (m2[j, k] > 0)
                                {
                                    if (min > (m2[i, k] + m2[j, k]))
                                    {
                                        min = m2[i, k] + m2[j, k];
                                        m2[i, j] = m2[i, k] + m2[j, k];
                                        m2[j, i] = m2[i, j];
                                    }
                                }
                            }
                        }
                    }
                }
            }
            for (i = 0; i < grv.Count; i++)
            {
                for (j = 0; j < grv.Count; j++)
                {
                    if (m2[i, j] == 0)
                    {
                        if (i != j)
                        {
                            m2[i, j] = Int32.MaxValue;
                        }
                    }
                }
            }
            for (i = 0; i < grv.Count - 1; i++)
            {
                for (j = i + 1; j < grv.Count; j++)
                {
                    if ((m1[i, j] > 0) && (m1[i, j] < Int32.MaxValue))
                    {
                        if (m2[i, j] == Int32.MaxValue)
                        {
                            if ((C.Contains(grv[i].Number) == false) && (C.Contains(grv[j].Number) == false))
                            {
                                if (B.Contains(grv[i].Number) == false)
                                {
                                    if(sig.Contains(grv[i].Number) == true)
                                    {
                                        //A.Add(grv[i].Number);
                                        B.Add(grv[j].Number);
                                        ans = true;
                                    }
                                }
                            }
                        }
                    }
                }
                if (B.Contains(grv[i++].Number) == true)
                {
                    i++;
                }
            }
            if(ans == true)
            {
                for (i = 0; i < sig.Count; i++)
                {
                    if (C.Contains(sig[i]) == false)
                    {
                        if (B.Contains(sig[i]) == false)
                        {
                            A.Add(sig[i]);
                        }
                    }
                }
            }
            return ans;
        }*/

        protected bool CheckSeparator5(List<int> sig, List<int> A, List<int> B, List<int> C)
        {
            int i, j, k, min;
            int[,] m2;
            bool ans = false;
            List<Vertex> grv;
            List<Edge> gre;
            grv = entgr.GetVertexs();
            gre = entgr.GetEdges();
            //m1 = entgr.GetDistMatrix();
            m2 = new int[grv.Count, grv.Count];
            for (i = 0; i < grv.Count; i++)
            {
                for (j = 0; j < grv.Count; j++)
                {
                    m2[i, j] = 0;
                    for (k = 0; k < gre.Count; k++)
                    {
                        if (grv[i].Number == gre[k].GetLessNum())
                        {
                            if (grv[j].Number == gre[k].GetSupNum())
                            {
                                if (C.Contains(gre[k].GetLessNum()) == false)
                                {
                                    if (C.Contains(gre[k].GetSupNum()) == false)
                                    {
                                        m2[i, j] = 1;
                                    }
                                }
                            }
                        }
                        if (grv[i].Number == gre[k].GetSupNum())
                        {
                            if (grv[j].Number == gre[k].GetLessNum())
                            {
                                if (C.Contains(gre[k].GetLessNum()) == false)
                                {
                                    if (C.Contains(gre[k].GetSupNum()) == false)
                                    {
                                        m2[i, j] = 1;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            for (i = 0; i < grv.Count - 1; i++)
            {
                for (j = i + 1; j < grv.Count; j++)
                {
                    if (m2[i, j] == 0)
                    {
                        min = Int32.MaxValue;
                        for (k = 0; k < grv.Count; k++)
                        {
                            if (m2[i, k] > 0)
                            {
                                if (m2[j, k] > 0)
                                {
                                    if (min > (m2[i, k] + m2[j, k]))
                                    {
                                        min = m2[i, k] + m2[j, k];
                                        m2[i, j] = m2[i, k] + m2[j, k];
                                        m2[j, i] = m2[i, j];
                                    }
                                }
                            }
                        }
                    }
                }
            }
            for (i = grv.Count - 1; i > 0; i--)
            {
                for (j = i - 1; j >= 0; j--)
                {
                    if (m2[i, j] == 0)
                    {
                        min = Int32.MaxValue;
                        for (k = 0; k < grv.Count; k++)
                        {
                            if (m2[i, k] > 0)
                            {
                                if (m2[j, k] > 0)
                                {
                                    if (min > (m2[i, k] + m2[j, k]))
                                    {
                                        min = m2[i, k] + m2[j, k];
                                        m2[i, j] = m2[i, k] + m2[j, k];
                                        m2[j, i] = m2[i, j];
                                    }
                                }
                            }
                        }
                    }
                }
            }
            for (i = 0; i < grv.Count; i++)
            {
                for (j = 0; j < grv.Count; j++)
                {
                    if (m2[i, j] == 0)
                    {
                        if (i != j)
                        {
                            m2[i, j] = Int32.MaxValue;
                        }
                    }
                }
            }
            for (i = 0; i < grv.Count - 1; i++)
            {
                for (j = i + 1; j < grv.Count; j++)
                {
                    if (m2[i, j] == Int32.MaxValue)
                    {
                        if ((C.Contains(grv[i].Number) == false) && (C.Contains(grv[j].Number) == false))
                        {
                            if (B.Contains(grv[i].Number) == false)
                            {
                                if (sig.Contains(grv[i].Number) == true)
                                {
                                    //A.Add(grv[i].Number);
                                    B.Add(grv[j].Number);
                                    ans = true;
                                }
                            }
                        }
                    }
                }
                if (B.Contains(grv[i++].Number) == true)
                {
                    i++;
                }
            }
            if (ans == true)
            {
                for (i = 0; i < sig.Count; i++)
                {
                    if (C.Contains(sig[i]) == false)
                    {
                        if (B.Contains(sig[i]) == false)
                        {
                            A.Add(sig[i]);
                        }
                    }
                }
            }
            return ans;
        }

    }
    class MCS : GraphCommand
    {
        //Алгоритм максимальной кардинальности - 1
        public override void Execute(SimpleGraph sgr)
        {
            int i, j, k, curver, deg, maxdeg, nextver, start, v1, v2;
            string result = "";
            List<int> sigma = new List<int>();
            List<int> ex = new List<int>();
            List<int> curS = new List<int>();
            List<Edge> gre = new List<Edge>();
            List<Vertex> grv = new List<Vertex>();
            entgr = sgr;
            grv = entgr.GetVertexs();
            gre = entgr.GetEdges();
            for (i = 0; i < grv.Count; i++)
            {
                sigma.Add(grv[i].Number);
            }
            curver = grv.Count / 2; //Задать начальную вершину
            start = StartNumber.GetNumber();
            if ((start > 0) && (start <= grv.Count))
            {
                curver = start;
            }
            while (sigma.Count > 0)
            {
                sigma.Remove(curver);
                ex.Add(curver);
                for (i = 0; i < gre.Count; i++)
                {
                    v1 = gre[i].GetLessNum();
                    v2 = gre[i].GetSupNum();
                    if (v1 == curver)
                    {
                        curS.Add(v2);
                    }
                    if (v2 == curver)
                    {
                        curS.Add(v1);
                    }
                }

                maxdeg = -1;
                nextver = 0;
                for (i = 0; i < curS.Count; i++)
                {
                    for (k = 0; k < ex.Count; k++)
                    {
                        if (curS.Count == 0)
                        {
                            break;
                        }
                        if (curS[i] == ex[k])
                        {
                            curS.RemoveAt(i);
                            if(i > 0)
                            {
                                i--;
                                
                            }
                        }
                    }
                    if (curS.Count == 0)
                    {
                        break;
                    }
                }
                for (i = 0; i < curS.Count; i++)
                {
                    deg = 0;
                    for (j = 0; j < gre.Count; j++)
                    {
                        v1 = gre[j].GetLessNum();
                        v2 = gre[j].GetSupNum();
                        if (v1 == curS[i])
                        {
                            for(k = 0; k < ex.Count; k++)
                            {
                                if(v2 == ex[k])
                                {
                                    deg++;
                                }
                            }
                        }
                        if (v2 == curS[i])
                        {
                            for (k = 0; k < ex.Count; k++)
                            {
                                if (v1 == ex[k])
                                {
                                    deg++;
                                }
                            }
                        }
                    }
                    if (maxdeg < deg)
                    {
                        maxdeg = deg;
                        nextver = curS[i];
                    }
                }
                //result = result + curver + " - ";
                result = result + curver;
                if(sigma.Count > 0)
                {
                    result = result + " - ";
                }
                curver = nextver;
            }
            Answer.SetAnswer(result);
        }
    }
    class MinFillIn : GraphCommand
    {
        //Алгоритм минимального покрытия - 2
        public override void Execute(SimpleGraph sgr)
        {
            int i, j, k1, k2, curdeg, mindeg, ednum, curver, v1, v2;
            string result = "";
            List<int> sigma = new List<int>();
            List<int> curS = new List<int>();
            List<Edge> gre;
            List<Vertex> grv;
            List<Edge> gre1 = new List<Edge>();
            List<Vertex> grv1 = new List<Vertex>();
            entgr = sgr;
            grv = entgr.GetVertexs();
            gre = entgr.GetEdges();
            for (i = 0; i < gre.Count; i++)
            {
                gre1.Add(gre[i]);
            }
            for (i = 0; i < grv.Count; i++)
            {
                grv1.Add(grv[i]);
            }
            for (i = 0; i < grv1.Count; i++)
            {
                sigma.Add(grv1[i].Number);
            }

            while (sigma.Count > 0)
            {
                mindeg = grv1.Count;
                curver = 0;
                for (i = 0; i < grv1.Count; i++)
                {
                    ednum = 0;
                    for (j = 0; j < gre1.Count; j++)
                    {
                        v1 = gre1[j].GetLessNum();
                        v2 = gre1[j].GetSupNum();
                        if ((v1 == sigma[curver]) && (v1 != v2))
                        {
                            ednum++;
                            curS.Add(v2);
                        }
                        if ((v2 == sigma[curver]) && (v1 != v2))
                        {
                            ednum++;
                            curS.Add(v1);
                        }
                    }
                    for (j = 0; j < gre1.Count; j++)
                    {
                        v1 = gre1[j].GetLessNum();
                        v2 = gre1[j].GetSupNum();
                        for (k1 = 0; k1 < curS.Count; k1++)
                        {
                            for (k2 = 0; k2 < curS.Count; k2++)
                            {
                                if(k1 != k2)
                                {
                                    if (((v1 == curS[k1]) && (v2 == curS[k2])) || ((v1 == curS[k2]) && (v2 == curS[k1])))
                                    {
                                        ednum++;
                                    }
                                }
                            }
                        }    
                    }
                    curdeg = (curS.Count * (curS.Count - 1)) / 2 - ednum;
                    if (mindeg > curdeg)
                    {
                        mindeg = curdeg;
                        curver = i;
                    }
                }
                //result = result + sigma[curver] + " - ";
                result = result + sigma[curver];
                if (sigma.Count > 1)
                {
                    result = result + " - ";
                }
                //Удалить рёбра - сделано
                for (i = 0; i < gre1.Count; i++)
                {
                    v1 = gre1[i].GetLessNum();
                    v2 = gre1[i].GetSupNum();
                    if (v1 == sigma[curver])
                    {
                        gre1.RemoveAt(i);
                        i--;
                    }
                    if (v2 == sigma[curver])
                    {
                        gre1.RemoveAt(i);
                        i--;
                    }
                }
                sigma.RemoveAt(curver);
                grv1.RemoveAt(curver);
                curS.Clear();
            }
            Answer.SetAnswer(result);
        }
    }
    [Serializable] public class GraphBuilder
    {
        SimpleGraph sgr;
        public GraphBuilder()
        {
            sgr = new SimpleGraph();
        }
        public void buildPart(Vertex ver)
        {
            sgr.AddVertex(ver);
        }
        public void buildPart(Edge ed)
        {
            sgr.AddEdge(ed);
            sgr.SetDegrees();
        }
        public SimpleGraph GetResult()
        {
            return sgr;
        }
    }
    public class GraphElementFactory
    {
        public Vertex CreateVertex(int num, int x, int y)
        {
            Vertex ver = new Vertex(num);
            ver.X = x;
            ver.Y = y;
            return ver;
        }
        public Edge CreateEdge(int a, int b)
        {
            return new Edge(a, b);
        }
    }
    static class Answer
    {
        private static string ans;
        public static void SetAnswer(string text)
        {
            ans = text;
        }
        public static string GetAnswer()
        {
            return ans;
        }
    }
    static class StartNumber
    {
        private static int num;
        public static void SetNumber(int number)
        {
            num = number;
        }
        public static int GetNumber()
        {
            return num;
        }
    }
}
