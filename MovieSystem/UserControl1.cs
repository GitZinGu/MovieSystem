using MovieSystem.Model;
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
    public partial class UserControl1 : UserControl
    {
        public UserControl1()
        {
            InitializeComponent();
        }
        public Ticket ticket { get; set; }
        public UserControl1(Ticket _ticket)
        {
            InitializeComponent();
            ticket = _ticket;
            label1.Text = _ticket.TicketNo;
            if (_ticket.IsBuy)
            {
                this.BackColor = Color.Red;
            }
            else
            {
                this.BackColor = SystemColors.Control;
            }
            this.Click += UserControl1_Click;
            label1.Click += UserControl1_Click;
        }

        private void UserControl1_Click(object sender, EventArgs e)
        {
            if (ticket.IsBuy)
            {
                MessageBox.Show("此座位已售出！");
            }
            else
            {
                if (this.BackColor ==Color.Yellow)
                {
                    this.BackColor = SystemColors.Control;
                }
                else
                {
                    this.BackColor = Color.Yellow;
                }
            }
        }
    }
}
