using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Graphs_1_0
{
    public partial class Form1 : Form
    {
        private int locnum, maxnum;
        private LBFS lbfs;
        private MinDegreeTree mdt;
        private NestedDissection nd;
        private MCS mcs;
        private MinFillIn mfi;
        private SimpleGraph sg;
        private GraphBuilder sgb;
        private List<int> sigma = new List<int>();
        private List<Vertex> vers = new List<Vertex>();
        private List<Edge> eds = new List<Edge>();
        private bool ishovercir = false;
        private bool isclickedcir = false;
        private Point oldmouscoor;
        private int ind = -1;
        Random getR = new Random(DateTime.Now.Millisecond);
        public Form1()
        {
            InitializeComponent();
            maxnum = 0;
            lbfs = new LBFS();
            mdt = new MinDegreeTree();
            nd = new NestedDissection();
            mcs = new MCS();
            mfi = new MinFillIn();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Graph in file
            int i;
            sgb = new GraphBuilder();
            for (i = 0; i < vers.Count; i++)
            {
                sgb.buildPart(vers[i]);
            }
            for (i = 0; i < eds.Count; i++)
            {
                sgb.buildPart(eds[i]);
            }
            //Определить степени вершин и задать входные и выходные рёбра - сделано

            SaveFileDialog sav = new SaveFileDialog();
            sav.FileName = "Graph1";
            sav.Filter = "Graph files (.grap)|*.grap";
            sav.Title = "Сохранить как";
            sav.DefaultExt = ".grap"; //Think it!
            if (sav.ShowDialog() == DialogResult.OK)
            {
                FileStream A = new FileStream(sav.FileName, FileMode.OpenOrCreate);
                BinaryFormatter B = new BinaryFormatter();
                B.Serialize(A, sgb);
                A.Close();
            }
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            int i, num;
            for (i = 0; i < eds.Count; i++)
            {
                e.Graphics.DrawLine(new Pen(Color.Black), vers[sigma.IndexOf(eds[i].GetLessNum())].X, vers[sigma.IndexOf(eds[i].GetLessNum())].Y, vers[sigma.IndexOf(eds[i].GetSupNum())].X, vers[sigma.IndexOf(eds[i].GetSupNum())].Y);
            }
            for (i = 0; i < vers.Count; i++)
            {
                vers[i].Draw(e);
                num = vers[i].Number;
                e.Graphics.DrawString(num.ToString(), new Font("Arial", 10, FontStyle.Regular), new SolidBrush(Color.Black), vers[i].X - 15, vers[i].Y - 15);
            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                if (ishovercir == true)
                {
                    isclickedcir = true;
                }
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            double dist;
            ishovercir = false;
            Point chan_coord = new Point(e.X - oldmouscoor.X, e.Y - oldmouscoor.Y);
            if (isclickedcir == true)
            {
                Point new_coord_cir = new Point();

                new_coord_cir.X = vers[ind].X + chan_coord.X;
                new_coord_cir.Y = vers[ind].Y + chan_coord.Y;

                if (InPanel(new_coord_cir) == true)
                {
                    vers[ind].X = new_coord_cir.X;
                    vers[ind].Y = new_coord_cir.Y;
                    pictureBox1.Invalidate();
                }
            }
            else
            {
                for (int i = 0; i < vers.Count; i++)
                {
                    dist = Math.Sqrt(Math.Pow(e.X - vers[i].X, 2) + Math.Pow(e.Y - vers[i].Y, 2));
                    if (dist <= 20)
                    {
                        ishovercir = true;
                        ind = i;
                        Cursor = Cursors.Cross;
                        oldmouscoor = e.Location;
                        break;
                    }
                }
            }
            if (ishovercir == false)
            {
                Cursor = Cursors.Arrow;
            }
            oldmouscoor = e.Location;
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            ishovercir = false;
            isclickedcir = false;
        }
        private bool InPanel(Point a)
        {
            if ((a.X <= pictureBox1.Location.X) || (a.X >= (pictureBox1.Location.X + pictureBox1.Width)) || (a.Y <= pictureBox1.Location.Y) || (a.Y >= (pictureBox1.Location.X + pictureBox1.Height)))
            {
                return false;
            }
            return true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            vers.Add(new Vertex(maxnum + 1));
            vers[maxnum].X = pictureBox1.Location.X + getR.Next(pictureBox1.Width / 2);
            vers[maxnum].Y = pictureBox1.Location.Y + getR.Next(pictureBox1.Height / 2);
            maxnum++;
            sigma.Add(maxnum);
            button3.Enabled = true;
            button9.Enabled = true;
            label6.Visible = true;
            comboBox1.Enabled = true;
            textBox5.Enabled = true;
            textBox5.Visible = true;
            pictureBox1.Invalidate();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            label4.Visible = true;
            button3.Enabled = false;
            textBox3.Enabled = true;
            textBox3.Visible = true;
            button7.Enabled = true;
            button7.Visible = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Enabled = true;
            textBox1.Visible = true;
            textBox2.Enabled = true;
            textBox2.Visible = true;
            label2.Visible = true;
            label3.Visible = true;
            button5.Enabled = false;
            button6.Enabled = true;
            button6.Visible = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox1.Enabled = true;
            textBox1.Visible = true;
            textBox2.Enabled = true;
            textBox2.Visible = true;
            label2.Visible = true;
            label3.Visible = true;
            button4.Enabled = false;
            button6.Enabled = true;
            button6.Visible = true;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            int i, j, k, v1, v2;
            try
            {
                i = int.Parse(textBox1.Text);
                j = int.Parse(textBox2.Text);
                if ((i > 0) && (i <= maxnum))
                {
                    if ((j > 0) && (j <= maxnum))
                    {
                        if (button4.Enabled == true)
                        {
                            eds.Add(new Edge(i, j));
                        }
                        else
                        {
                            for (k = 0; k < eds.Count; k++)
                            {
                                v1 = eds[k].GetLessNum();
                                v2 = eds[k].GetSupNum();
                                if ((v1 == i) && (v2 == j))
                                {
                                    eds.RemoveAt(k);
                                }
                                if ((v2 == i) && (v1 == j))
                                {
                                    eds.RemoveAt(k);
                                }
                            }
                        }
                        if (eds.Count == 0)
                        {
                            button5.Enabled = false;
                        }
                        pictureBox1.Invalidate();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            button4.Enabled = true;
            if (eds.Count == 0)
            {
                button5.Enabled = false;
            }
            else
            {
                button5.Enabled = true;
            }
            textBox1.Enabled = false;
            textBox1.Visible = false;
            textBox2.Enabled = false;
            textBox2.Visible = false;
            label2.Visible = false;
            label3.Visible = false;
            button6.Enabled = false;
            button6.Visible = false;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            int i, j, v1, v2, num1;
            try
            {
                i = int.Parse(textBox3.Text);
                if ((i > 0) && (i <= maxnum))
                {
                    vers.RemoveAt(sigma.IndexOf(i));
                    sigma.Remove(i);
                    for (j = 0; j < eds.Count; j++)
                    {
                        v1 = eds[j].GetLessNum();
                        v2 = eds[j].GetSupNum();
                        if ((v1 == i) || (v2 == i))
                        {
                            eds.RemoveAt(j);
                        }
                    }
                    for (j = 0; j < vers.Count; j++)
                    {
                        if (vers[j].Number > i)
                        {
                            num1 = vers[j].Number;
                            sigma.Remove(num1);
                            num1--;
                            vers[j].Renum(num1);
                            sigma.Add(num1);
                        }
                    }
                    for (j = 0; j < eds.Count; j++)
                    {
                        v1 = eds[j].GetLessNum();
                        v2 = eds[j].GetSupNum();
                        if (v1 > i)
                        {
                            v1--;
                            eds[j].SetLessNum(v1);
                        }
                        if (v2 > i)
                        {
                            v2--;
                            eds[j].SetSupNum(v2);
                        }
                    }
                    maxnum--;
                    pictureBox1.Invalidate();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            if (vers.Count == 0)
            {
                button3.Enabled = false;
            }
            else
            {
                button3.Enabled = true;
            }
            label4.Visible = false;
            textBox3.Enabled = false;
            textBox3.Visible = false;
            button7.Enabled = false;
            button7.Visible = false;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            //Вызвать файловый диалог для выбора файла с графом
            int i;
            string name;
            //Из файла выкладываем рабочий граф
            //SimpleGraph sg;
            //Выбрать файл - как один из нескольких
            OpenFileDialog fd = new OpenFileDialog();
            fd.Title = "Выбрать граф";
            fd.Filter = "Graph files (.grap)|*.grap";
            fd.DefaultExt = ".grap";
            if (fd.ShowDialog() == DialogResult.OK)
            {
                name = fd.FileName;
                FileStream A = new FileStream(name, FileMode.Open);
                BinaryFormatter B = new BinaryFormatter();
                var buf = B.Deserialize(A);
                sg = ((GraphBuilder)buf).GetResult();
                A.Close();
                //Записать граф из файла и нарисовать
                //sg.Draw(e);
                vers = sg.GetVertexs();
                eds = sg.GetEdges();
                for (i = 0; i < vers.Count; i++)
                {
                    sigma.Add(vers[i].Number);
                    if(vers[i].Number > maxnum)
                    {
                        maxnum = vers[i].Number;
                    }
                }
                pictureBox1.Invalidate();
                button3.Enabled = true;
                button5.Enabled = true;
                button8.Visible = true;
                button8.Enabled = false;
                button9.Enabled = true;
                label6.Visible = true;
                comboBox1.Enabled = true;
                textBox5.Enabled = true;
                textBox5.Visible = true;
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                MessageBox.Show("Вы не выбрали операцию!");
            }
            else
            {
                int i;
                sgb = new GraphBuilder();
                for (i = 0; i < vers.Count; i++)
                {
                    sgb.buildPart(vers[i]);
                }
                for (i = 0; i < eds.Count; i++)
                {
                    sgb.buildPart(eds[i]);
                }
                sg = sgb.GetResult();
                if (comboBox1.Text == "Лексикографический поиск")
                {
                    try
                    {
                        locnum = int.Parse(textBox5.Text);
                        StartNumber.SetNumber(locnum);
                        lbfs.Execute(sg);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                if (comboBox1.Text == "Minimum degree search")
                {
                    mdt.Execute(sg);
                }
                if (comboBox1.Text == "Nested dissection")
                {
                    try
                    {
                        locnum = int.Parse(textBox5.Text);
                        StartNumber.SetNumber(locnum);
                        nd.Execute(sg);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                if (comboBox1.Text == "Maximum cardinality search")
                {
                    try
                    {
                        locnum = int.Parse(textBox5.Text);
                        StartNumber.SetNumber(locnum);
                        mcs.Execute(sg);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                if (comboBox1.Text == "Minimum Fill-in")
                {
                    mfi.Execute(sg);
                }
                textBox4.Text = Answer.GetAnswer();
            }
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == "")
            {
                label6.Visible = false;
                textBox5.Enabled = false;
                textBox5.Visible = false;
            }
            if (comboBox1.Text == "Лексикографический поиск")
            {
                label6.Text = "Стартовый номер: ";
                label6.Visible = true;
                textBox5.Enabled = true;
                textBox5.Visible = true;
            }
            if (comboBox1.Text == "Minimum degree search")
            {
                label6.Visible = false;
                textBox5.Enabled = false;
                textBox5.Visible = false;
            }
            if (comboBox1.Text == "Nested dissection")
            {
                label6.Text = "Кол. итераций: ";
                label6.Visible = true;
                textBox5.Enabled = true;
                textBox5.Visible = true;
            }
            if (comboBox1.Text == "Maximum cardinality search")
            {
                label6.Text = "Стартовый номер: ";
                label6.Visible = true;
                textBox5.Enabled = true;
                textBox5.Visible = true;
            }
            if (comboBox1.Text == "Minimum Fill-in")
            {
                label6.Visible = false;
                textBox5.Enabled = false;
                textBox5.Visible = false;
            }
        }
    }
}