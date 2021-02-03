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
        }
        private Movie movie;
        private MovieTime time;
        private void TreeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Level==1)
            {
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
            var totalprice=ms.BuyTickets(movie.MovieName, time.MovieTimes, lt);
            LoadFlowLayoutPanel((treeView1.SelectedNode.Tag as MovieTime).Tickets);
            MessageBox.Show($"一共花费{totalprice}");

        }
    }
}
