using MovieSystem.Common;
using MovieSystem.Model;
using MovieSystem.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MovieSystem
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            InitControl();
            Load += Form1_Load;
        }

        private void InitControl()
        { 
            label12.Text = "电影名：";
            label11.Text = "导演：";
            label10.Text = "演员：";
            label9.Text = "简介：";
            label8.Text = "类型：";
            label7.Text = "票价：";
            string[] names = Enum.GetNames(typeof(TicketType));
            comboBox1.Items.Clear();
            foreach (string name in names)
            {
                this.comboBox1.Items.Add(name); 
            }
            comboBox1.SelectedIndex = 0;
            comboBox2.Enabled = false;
            button1.Enabled = false;
        }
         private   MovieService ms ;
        private void Form1_Load(object sender, EventArgs e)
        {
            ms = new MovieService();
            foreach (var movie in ms.movies)
            {
                var treeroot = new TreeNode(movie.MovieName);
                treeroot.Tag = movie; 
                foreach (var time in movie.ListMovieTime)
                {
                    var times = new TreeNode(time.MovieTimes);
                    times.Tag = time;
                    treeroot.Nodes.Add(times);
                }
                treeView1.Nodes.Add(treeroot);
            }
            treeView1.AfterSelect += TreeView1_AfterSelect;
            comboBox1.SelectedIndexChanged += ComboBox1_SelectedIndexChanged;
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem.ToString()== "打折票")
            {
                comboBox2.Enabled = true;
            }
            else
            {
                comboBox2.Enabled = false;
            }
        }

        private Movie movie;
        private MovieTime time;
        private void TreeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Level==1)
            {
                button1.Enabled = true;
                movie = e.Node.Parent.Tag as Movie;
                label1.Text = movie.MovieName;
                label2.Text = movie.Director;
                label3.Text = movie.Actors;
                label4.Text = movie.Introduction;
                label5.Text = movie.MovieType;
                label6.Text = movie.Price.ToString();
                pictureBox1.Image = movie.image;

                time = e.Node.Tag as MovieTime;
                LoadFlowLayoutPanel(time.Tickets);
            }
        }
        private void LoadFlowLayoutPanel(List<Ticket> tickets) 
        {
            flowLayoutPanel1.Controls.Clear();
            foreach (var item in tickets)
            {
                flowLayoutPanel1.Controls.Add(new UserControl1(item));
            }
        
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<Ticket> lt = new List<Ticket>();
            foreach (UserControl1 item in flowLayoutPanel1.Controls)
            {
                if (item.BackColor==Color.Yellow)
                {
                    lt.Add(item.ticket);
                }
            }
            if (lt.Count==0)
            {
                MessageBox.Show("请先选择座位！");
                return;
            }
            var totalprice=ms.BuyTickets(movie.MovieName, time.MovieTimes, lt,(TicketType)Enum.Parse(typeof(TicketType),comboBox1.SelectedItem.ToString()),(double)comboBox2.SelectedItem);
            LoadFlowLayoutPanel((treeView1.SelectedNode.Tag as MovieTime).Tickets);
            MessageBox.Show($"一共花费{totalprice}");

        }
    }
}
