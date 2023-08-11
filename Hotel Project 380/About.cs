using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hotel_Project_380
{
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();
        }

        public void loadform(object Form)
        {
            if (this.panelDesktop.Controls.Count > 0)
                this.panelDesktop.Controls.RemoveAt(0);
            Form f = Form as Form;

            f.TopLevel = false;
            f.Dock = DockStyle.Fill;
            this.panelDesktop.Controls.Add(f);
            this.panelDesktop.Tag = f;
            f.BringToFront();
            f.Show();

        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {

        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            loadform(new New_Reservation());
        }

        private void iconButton6_Click(object sender, EventArgs e)
        {
            loadform(new New_Reservation());
        }

        private void iconButton4_Click(object sender, EventArgs e)
        {
            loadform(new About());
        }

        private void iconButton3_Click(object sender, EventArgs e)
        {
            loadform(new Edit());
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            loadform(new Profile());
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
        }

        private void panelDesktop_Paint(object sender, PaintEventArgs e)
        {
            aboutTitle.Parent = pictureBox1;
            aboutTitle.BackColor = Color.Transparent;
            aboutHeader.Parent = pictureBox1;
            aboutHeader.BackColor = Color.Transparent;
            aboutDescription.Parent = pictureBox1;
            aboutDescription.BackColor = Color.Transparent;
            wifilogo.Parent = pictureBox1;
            wifilogo.BackColor = Color.Transparent;
            roomservicelogo.Parent = pictureBox1;
            roomservicelogo.BackColor = Color.Transparent;
            hotelgym.Parent = pictureBox1;
            hotelgym.BackColor = Color.Transparent;
            noSmoking.Parent = pictureBox1;
            noSmoking.BackColor = Color.Transparent;
        }

        private void aboutHeader_Click(object sender, EventArgs e)
        {

        }
    }
}
