﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace Hotel_Project_380
{
    public partial class New_Reservation : Form
    {
        Image studio;
        Image suite;
        Image pent;
        Image Pres;
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\angel\OneDrive\Documents\Hotel.mdf;Integrated Security=True;Connect Timeout=30");
        public New_Reservation()
        {
            InitializeComponent();

        }

        private void New_Reservation_Load(object sender, EventArgs e)
        {
           
        }

        private void bunifuTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void customTextBox1_Load(object sender, EventArgs e)
        {

        }

        private void customTextBox1__TextChanged(object sender, EventArgs e)
        {

        }

        private void customTextBox2__TextChanged(object sender, EventArgs e)
        {

        }

        private void customTextBox1__TextChanged_1(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void datein_ValueChanged(object sender, EventArgs e)
        {
           
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void radioBtnSuite_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void radioBtnPentHouse_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void radioButton1_CheckedChanged_1(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void radioBtnStudio_CheckedChanged(object sender, EventArgs e)
        {
           
        }


        /// <summary>
        /// Make Reservation
        /// Angel Reyes
        /// 08/16/2022
        /// 
        /// The New Reservation form contains a "Book Now" module/class that will allow the user to book a New Reservation once the number of guest, CheckIn/CheckOut,
        /// and Room Type has been provided. User will not be able to move foward to the cart form unless all information is present. Once all data is provided
        /// a new cart form will display as a confirmation allowing the user to enter personal data and payment. Once all data is accquired the user just checks out
        /// and creates the new reservation.
        /// 
        /// Important funtctions I used in this class were conversion functions such as .Tostring() or toInt32(). These were mainly used because important input and output values
        /// such as CheckIn/Checkout, number of guest, total nights, total price, and images were all used to be manipulated accross forms (between new reservation and cart).
        /// 
        /// An important structure/algorithm I used was making instances to transfer data across forms to be allowed to use those input/outputs across forms.
        /// I selected this algorithm because this way I can use the inputs/outputs that are important between both forms. 
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void iconButton1_Click(object sender, EventArgs e)
        {
            Cart form = new Cart(); 

            if (guestnum.SelectedItem == null) 
            {
                MessageBox.Show("Please choose the number of guests.");
            }
           else if (!radioBtnStudio.Checked && !radioBtnSuite.Checked && !radioBtnPentHouse.Checked && !radiobtnPres.Checked) 
           {
                MessageBox.Show("You can't enjoy your stay with us\nif you don't choose a room. :)");
           }

            if (radioBtnStudio.Checked && guestnum.SelectedItem != null)
            {
                studio = Image.FromFile(@"C:\Users\angel\source\repos\Hotel-Project-380\Hotel Project 380\pics\studio5.jpg");
                Cart.instance.lab1.Text = "Studio Deluxe Stay";
                Cart.instance.lab2.Text = "One King or\nTwo Queen";
                Cart.instance.lab3.Text = CheckIn_calender.Value.ToString();
                Cart.instance.lab4.Text = Checkout_calender.Value.ToString();
                Cart.instance.lab5.Text = guestnum.SelectedItem.ToString();
                Cart.instance.pic.Image = studio;

                DateTime d1 = CheckIn_calender.Value;
                DateTime d2 = Checkout_calender.Value;
                TimeSpan time = d2 - d1;
                double Days = time.TotalDays;
                int days = Convert.ToInt32(Days);
                Cart.instance.lab6.Text = days.ToString();
                int total_price = days * 500;
                Cart.instance.lab7.Text = total_price.ToString();
                Cart.instance.typeofroom = "Studio";

                SqlCommand myCommand = new SqlCommand("SELECT COUNT(1) FROM RoomInfo_Table WHERE RoomType = 'Studio' and CheckIn IS NULL and CheckOut IS NULL ", Con);
                myCommand.Connection.Open();
                object obj = myCommand.ExecuteScalar();
                if (Convert.ToInt32(obj) > 0)
                {
                    form.Show();
                }
                else
                {
                    MessageBox.Show("Room selection are all booked.\nOur apologies. :(");
                }
                

                myCommand.Connection.Close();

            }
            else if (radioBtnSuite.Checked && guestnum.SelectedItem != null)
            {
                suite = Image.FromFile(@"C:\Users\angel\source\repos\Hotel-Project-380\Hotel Project 380\pics\suitepic2.jpg");
                Cart.instance.lab1.Text = "Panoramic Suite Stay";
                Cart.instance.lab2.Text = "One Bedroom\nOne King or\nTwo Queen";
                Cart.instance.lab3.Text = CheckIn_calender.Value.ToString();
                Cart.instance.lab4.Text = Checkout_calender.Value.ToString();
                Cart.instance.lab5.Text = guestnum.SelectedItem.ToString();
                Cart.instance.pic.Image = suite;

                DateTime d1 = CheckIn_calender.Value;
                DateTime d2 = Checkout_calender.Value;
                TimeSpan time = d2 - d1;
                double Days = time.TotalDays;
                int days = Convert.ToInt32(Days);
                Cart.instance.lab6.Text = days.ToString();
                int total_price = days * 1000;
                Cart.instance.lab7.Text = total_price.ToString();
                Cart.instance.typeofroom = "Suite";

                SqlCommand myCommand = new SqlCommand("SELECT COUNT(1) FROM RoomInfo_Table WHERE RoomType = 'Suite' and CheckIn IS NULL and CheckOut IS NULL ", Con);
                myCommand.Connection.Open();
                object obj = myCommand.ExecuteScalar();
                if (Convert.ToInt32(obj) > 0)
                {
                    form.Show();
                }
                else
                {
                    MessageBox.Show("Room selection are all booked.\nOur apologies. :(");
                }


                myCommand.Connection.Close();


            }
            else if (radioBtnPentHouse.Checked && guestnum.SelectedItem != null)
            {
                pent = Image.FromFile(@"C:\Users\angel\source\repos\Hotel-Project-380\Hotel Project 380\pics\pent.jpg");
                Cart.instance.lab1.Text = "Penthouse Sky View Stay";
                Cart.instance.lab2.Text = "Double Master\nBedroom with\n Two King Beds";
                Cart.instance.lab3.Text = CheckIn_calender.Value.ToString();
                Cart.instance.lab4.Text = Checkout_calender.Value.ToString();
                Cart.instance.lab5.Text = guestnum.SelectedItem.ToString();
                Cart.instance.pic.Image = pent;

                DateTime d1 = CheckIn_calender.Value;
                DateTime d2 = Checkout_calender.Value;
                TimeSpan time = d2 - d1;
                double Days = time.TotalDays;
                int days = Convert.ToInt32(Days);
                Cart.instance.lab6.Text = days.ToString();
                int total_price = days * 2000;
                Cart.instance.lab7.Text = total_price.ToString();
                Cart.instance.typeofroom = "PentHouse";

                SqlCommand myCommand = new SqlCommand("SELECT COUNT(1) FROM RoomInfo_Table WHERE RoomType = 'PentHouse' and CheckIn IS NULL and CheckOut IS NULL ", Con);
                myCommand.Connection.Open();
                object obj = myCommand.ExecuteScalar();
                if (Convert.ToInt32(obj) > 0)
                {
                    form.Show();
                }
                else
                {
                    MessageBox.Show("Room selection are all booked.\nOur apologies. :(");
                }


                myCommand.Connection.Close();

            }
            else if (radiobtnPres.Checked && guestnum.SelectedItem != null)
            {
                Pres = Image.FromFile(@"C:\Users\angel\source\repos\Hotel-Project-380\Hotel Project 380\pics\the palms4.jpg");
                Cart.instance.lab1.Text = "Presidential Sky Villa";
                Cart.instance.lab2.Text = "Tripe Master\nBedroom with Pool\nTable/Private Pool";
                Cart.instance.lab3.Text = CheckIn_calender.Value.ToString();
                Cart.instance.lab4.Text = Checkout_calender.Value.ToString();
                Cart.instance.lab5.Text = guestnum.SelectedItem.ToString();
                Cart.instance.pic.Image = Pres;


                DateTime d1 = CheckIn_calender.Value;
                DateTime d2 = Checkout_calender.Value;
                TimeSpan time = d2 - d1;
                double Days = time.TotalDays;
                int days = Convert.ToInt32(Days);
                Cart.instance.lab6.Text = days.ToString();
                int total_price = days * 5000;
                Cart.instance.lab7.Text = total_price.ToString();
                Cart.instance.typeofroom = "Presidential";


                SqlCommand myCommand = new SqlCommand("SELECT COUNT(1) FROM RoomInfo_Table WHERE RoomType = 'Presidential' and CheckIn IS NULL and CheckOut IS NULL ", Con);
                myCommand.Connection.Open();
                object obj = myCommand.ExecuteScalar();
                if (Convert.ToInt32(obj) > 0)
                {
                    form.Show();
                }
                else
                {
                    MessageBox.Show("Room selection are all booked.\nOur apologies. :(");
                }


                myCommand.Connection.Close();
            }
        }

        private void guestnum_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void radioBtnSuite_CheckedChanged_1(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void CheckIn_calender_ValueChanged(object sender, EventArgs e)
        {

        }

        private void Checkout_calender_ValueChanged(object sender, EventArgs e)
        {

        }

        private void Guest_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

      

    }
}
