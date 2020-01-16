using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gra
{
    public partial class Form1 : Form
    {
        string[] kolory, alfabet;
        string kolor;
        int flaga;
        int punkty;
        int[] vels = new int[10];
        int[] dirs = new int[10];
        public Form1(string[] kolory, string[] alfabet, string kolor, int flaga, int[] vels, int[] dirs, int punkty)
        {
            this.kolory = kolory;
            this.alfabet = alfabet;
            this.kolor = kolor;
            this.flaga = flaga;
            this.vels = vels;
            this.dirs = dirs;
            this.punkty = punkty;
            InitializeComponent();
        }

        private void Label1_Click(object sender, EventArgs e)
        {
            tableLayoutPanel1.Visible = false;
            tableLayoutPanel2.Visible = true;
            tableLayoutPanel3.Visible = true;
            Losowanie();
            timer1.Start();
        }

        private void Label2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        
        private void Losowanie()
        {
            flaga = 0;
            Label[] labels = { label7, label8, label9, label10, label11, label12, label13, label14, label15, label16 };
            List<int> miejsca_x = new List<int>(){ 0, 50, 100, 150, 200, 250, 300, 350, 400, 450, 500, 550, 600, 650, 700, 750, 800 };
            List<int> miejsca_y = new List<int>(){ 0, 50, 100, 150, 200, 250, 300, 350, 400, 450, 500 };
            var rand = new Random();

            for (int i = 0; i < labels.Length; i++)
            {
                int x = rand.Next(miejsca_x.Count);
                int y = rand.Next(miejsca_y.Count);

                labels[i].Location = new Point(miejsca_x[x], miejsca_y[y]);
                labels[i].Visible = true;

                miejsca_x.RemoveAt(x);
                miejsca_y.RemoveAt(y);
            }

            gen_liter(labels);
        }

        private void gen_liter(Label[] labels)
        {
            var rand = new Random();

            int x = rand.Next(kolory.Length);
            string wybrany_kolor = kolory[x];
            label17.ForeColor = Color.FromName(wybrany_kolor);
            x = rand.Next(kolory.Length);
            wybrany_kolor = kolory[x];
            label17.Text = wybrany_kolor;
            kolor = wybrany_kolor;


            for (int i = 0; i < wybrany_kolor.Length; i++)
            {
                int font_size = rand.Next(25, 35);
                labels[i].Text = wybrany_kolor[i].ToString();
                labels[i].Font = new Font("Arial", font_size, FontStyle.Bold);
            }

            for (int i = wybrany_kolor.Length; i < labels.Length; i++)
            {
                int font_size = rand.Next(25, 35);
                int y = rand.Next(alfabet.Length);
                labels[i].Text = alfabet[y];
                labels[i].Font = new Font("Arial", font_size, FontStyle.Bold);
            }
        }

        private void Label7_Click(object sender, EventArgs e)
        {
            char[] eks = { ']' };
            Label iconLabel = sender as Label;
            try
            {
                System.Console.WriteLine(kolor[0]);
                System.Console.WriteLine(iconLabel.Text);
                if (Equals(kolor[0].ToString(), iconLabel.Text))
                {
                    if (Equals(label17.Text.ToLower(), label17.ForeColor.ToString().ToLower().Remove(0, 7).TrimEnd(eks)))
                    {
                        punkty += 1;
                        iconLabel.Visible = false;
                        kolor = kolor.Substring(1);
                    }
                }
                else
                    iconLabel.Visible = true;

                if (kolor.Length == 0)
                {
                    punkty += 10;
                    Losowanie();
                    timer1.Start();
                }
            }
            catch (System.IndexOutOfRangeException e1)
            {
            }
        }

        private void Label3_Click(object sender, EventArgs e)
        {
            Label[] labels = { label7, label8, label9, label10, label11, label12, label13, label14, label15, label16 };
            tableLayoutPanel2.Visible = false;
            tableLayoutPanel1.Visible = true;
            tableLayoutPanel3.Visible = false;
            foreach(Label label in labels)
            {
                label.Visible = false;
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {
            punkty = 0;
            Losowanie();
            timer1.Start();
        }

        private void label18_Click(object sender, EventArgs e)
        {
            char[] x = { ']' };
            for (int i = 0; i < kolory.Length; i++)
            {
                try
                {
                    if (Equals(kolory[i].ToLower(), label17.ForeColor.ToString().ToLower().Remove(0, 7).TrimEnd(x)))
                    {
                        label17.ForeColor = Color.FromName(kolory[i + 1]);
                        break;
                    }
                }
                catch (System.IndexOutOfRangeException e2)
                {
                    label17.ForeColor = Color.FromName(kolory[0]);
                }

            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Label[] labels = { label7, label8, label9, label10, label11, label12, label13, label14, label15, label16 };
            var rand = new Random();
            label5.Text = punkty.ToString();
            for(int i = 0; i < labels.Length; i++)
            {
                if (flaga < 10)
                {
                    vels[i] = rand.Next(1, 3);
                    dirs[i] = rand.Next(2);
                    flaga++;
                }
                
                if(labels[i].Location.X < 15 && dirs[i] == 1)
                {
                    dirs[i] = 0;
                }
                if (labels[i].Location.X > 740 && dirs[i] == 0)
                {
                    dirs[i] = 1;
                }
                
                if (dirs[i] == 0)
                {
                    labels[i].Location = new Point(labels[i].Location.X + vels[i], labels[i].Location.Y);
                }
                else
                {
                    labels[i].Location = new Point(labels[i].Location.X - vels[i], labels[i].Location.Y);
                }

            }
            timer1.Stop();
            timer1.Start();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            char[] x = {']'};
            for(int i=0; i<kolory.Length; i++)
            {
                try
                {
                    if (Equals(kolory[i].ToLower(), label17.ForeColor.ToString().ToLower().Remove(0, 7).TrimEnd(x)))
                    {
                        label17.ForeColor = Color.FromName(kolory[i - 1]);
                    }
                }
                catch (System.IndexOutOfRangeException e2)
                {
                    label17.ForeColor = Color.FromName(kolory[kolory.Length-1]);
                    break;
                }
            }

        }
    }
}
