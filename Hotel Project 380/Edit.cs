using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace Hotel_Project_380
{
    public partial class Edit : Form
    {

        //Add local Database connection
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\angel\OneDrive\Documents\Hotel.mdf;Integrated Security=True;Connect Timeout=30");
      
        
        /// <summary>
        /// Initializes the GUI Edit form
        /// </summary>
        public Edit()
        {
            InitializeComponent();
        }

        private void Edit_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Con.Open();

            string selectCountQuery = "SELECT COUNT(*) FROM Cart_Table WHERE LastName = @LastName AND PhoneNumber = @PhoneNumber AND Email = @Email";
            SqlCommand cmd = new SqlCommand(selectCountQuery, Con);
            cmd.Parameters.AddWithValue("@LastName", lastnametb.Text);
            cmd.Parameters.AddWithValue("@PhoneNumber", Phonetb.Text);
            cmd.Parameters.AddWithValue("@Email", emailreservationtb.Text);
            int reservationCount = (int)cmd.ExecuteScalar();

            if (reservationCount > 0)
            {
                string Myquery = "SELECT * FROM Cart_Table WHERE LastName = @LastName AND PhoneNumber = @PhoneNumber AND Email = @Email";
                SqlDataAdapter da = new SqlDataAdapter(Myquery, Con);
                da.SelectCommand.Parameters.AddWithValue("@LastName", lastnametb.Text);
                da.SelectCommand.Parameters.AddWithValue("@PhoneNumber", Phonetb.Text);
                da.SelectCommand.Parameters.AddWithValue("@Email", emailreservationtb.Text);
                DataTable edit = new DataTable();
                da.Fill(edit);
                ReservationDisplay.DataSource = edit;
            }
            else
            {
                MessageBox.Show("No reservation found.");
            }

            Con.Close();

        }
        private void button3_Click(object sender, EventArgs e)
        {
            
            if ( roomnum.Text == "")
            {
                MessageBox.Show("Please enter room number.");
            }
            else
            {
                Con.Open();
                string cmd = "SELECT COUNT(*) FROM Cart_Table WHERE RoomNumber = @RoomNumber";
                SqlCommand myCommand = new SqlCommand(cmd, Con);
                myCommand.Parameters.AddWithValue("@RoomNumber", roomnum.Text);
                int Count = (int)myCommand.ExecuteScalar();
                Con.Close();

                if (Count > 0)
                {
                    Con.Open();
                    string query = "DELETE FROM Cart_Table WHERE RoomNumber = @RoomNumber";
                    SqlCommand delete_cmd = new SqlCommand(query, Con);
                    delete_cmd.Parameters.AddWithValue("@RoomNumber", roomnum.Text);
                    delete_cmd.ExecuteNonQuery();
                    Con.Close();

                    Con.Open();
                    string update_room = "UPDATE RoomInfo_Table SET CheckIn = NULL, CheckOut = NULL WHERE RoomNumber = @RoomNumber";
                    SqlCommand reset_room = new SqlCommand(update_room, Con);
                    reset_room.Parameters.AddWithValue("@RoomNumber", roomnum.Text);
                    reset_room.ExecuteNonQuery();
                    Con.Close();

                    Send_Cancellation_Message(lastnametb.Text, emailreservationtb.Text);
                    MessageBox.Show("Reservation Cancelled.");
                }
                else
                {
                    MessageBox.Show("Invalid Room Number.");
                }
          
            }

        }

        protected void Send_Cancellation_Message(string name, string email)
        {
            MailMessage msg = new MailMessage();
            msg.From = new MailAddress("BlissHotel01@gmail.com");
            msg.To.Add(email);
            msg.Subject = "Bliss Hotel Reservation Email";
            msg.Body = $@"<html>
                <body style='font-family: Arial, sans-serif;'>
                    <h2>Bliss Hotel: Reservation Cancellation</h2>
                    <p>Dear Mr./Ms. {name},</p>
                    <p>We are sorry to hear that you've decided to cancel your reservation at Hotel Bliss.</p>
                    <p>Your reservation has been canceled successfully.</p>
                    <p>You will recieve a full refund to your bank within 3 to 4 business days.</p>
                    <p>If you have any questions or need further assistance, please feel free to contact us.</p>
                    <p>We hope to have the opportunity to serve you in the future.</p>
                    <p>Best regards,</p>
                    <p>The Hotel Bliss Team</p>
                </body>
            </html>";
            msg.IsBodyHtml = true;
            msg.ReplyToList.Add(new MailAddress(email, name));

            using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
            {
                smtp.Credentials = new NetworkCredential("BlissHotel01@gmail.com", " kkvjnveupeyffqll");
                smtp.EnableSsl = true;
                smtp.Send(msg);
            }

        }
        private void ReservationDisplay_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
          
        }
       
        private void Editbtn_Click(object sender, EventArgs e)
        {
            
            if (roomnum.Text == "")
            {
                MessageBox.Show("Please enter room number.");
            }
            else
            {
                Save_edit form = new Save_edit();
                Con.Open();
                string cmd = "SELECT COUNT(*) FROM Cart_Table WHERE RoomNumber = @RoomNumber";
                SqlCommand myCommand = new SqlCommand(cmd, Con);
                myCommand.Parameters.AddWithValue("@RoomNumber", roomnum.Text);
                int Count = (int)myCommand.ExecuteScalar();
                Con.Close();

                if (Count > 0)
                {
                    Save_edit.instance.room_num = Convert.ToInt32(roomnum.Text);
                    Save_edit.instance.email = Convert.ToString(emailreservationtb.Text);
                    Save_edit.instance.phoneNumber = Convert.ToString(Phonetb.Text);
                    Save_edit.instance.lastName = Convert.ToString(lastnametb.Text);


                    form.BringToFront();
                    form.Show();
                }
                else
                {
                    MessageBox.Show("Invalid room number.");
                }
                
            }
           

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}