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
    public partial class Save_edit : Form
    {
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\angel\OneDrive\Documents\Hotel.mdf;Integrated Security=True;Connect Timeout=30");
        public static Save_edit instance;
        public int room_num = 0;
        public string email = "";
        public string phoneNumber = "";
        public string lastName = "";
      
        public Save_edit()
        {
            InitializeComponent();
            instance = this;

        }
     
        private void cancelbtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Savebtn_Click(object sender, EventArgs e)
        {

            if (phonenum_txt.Text == "" || address_txt.Text == "" || zipcode_txt.Text == "" || state_txt.Text == "")
            {
                MessageBox.Show("Please fill information information.");
            }
            else if (totalguests.SelectedItem == null)
            {
                MessageBox.Show("Please pick number of guest.");
            }
            else
            {
                DateTime original_checkin;
                DateTime original_checkout;
                string original_checkIn = "";
                string original_checkOut = "";
                int original_nights = 0;
                string selectQuery = "SELECT * FROM Cart_Table WHERE RoomNumber = @RoomNumber";
                SqlCommand selectCommand = new SqlCommand(selectQuery, Con);
                selectCommand.Parameters.AddWithValue("@RoomNumber", room_num);

                Con.Open();

                using (SqlDataReader reader = selectCommand.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        // Assuming the column indices match the order in your SELECT statement
                        original_checkin = reader.GetDateTime(7); // Assuming checkin is in the 8th column (index 7)
                        original_checkout = reader.GetDateTime(8); // Assuming checkout is in the 9th (index 8)
                        original_nights = reader.GetInt32(10); // Assuming nights is in the 11th (index 10)
                        original_checkIn = original_checkin.ToString();
                        original_checkOut = original_checkout.ToString();  
                        
                    }
                }
                Con.Close();

                DateTime d1 = editcheckIn.Value;
                DateTime d2 = editCheckOut.Value;
                TimeSpan time = d2 - d1;
                double Days = time.TotalDays;
                int days = Convert.ToInt32(Days);
                string checkin = editcheckIn.Value.ToString();
                string checkout = editCheckOut.Value.ToString();

                

                Con.Open();
                string query = "UPDATE Cart_Table SET PhoneNumber = '" + phonenum_txt.Text + "', Address = '" + address_txt.Text + "', ZipCode = '" + zipcode_txt.Text + "', " +
                    "State = '" + state_txt.Text + "', CheckIn = '" + checkin + "', CheckOut = '" + checkout + "', Guests = '" + totalguests.Text + "', Nights = '" + days + "' WHERE RoomNumber = '" + room_num + "' ";
                SqlCommand save = new SqlCommand(query, Con);
                save.ExecuteNonQuery();
                Con.Close();
                Send_Edit_Message(original_checkIn, original_checkOut, original_nights, phonenum_txt.Text, checkin, checkout, days);
                MessageBox.Show("Information Updated!");
                this.Close();
            }
          
        }

        private void Send_Edit_Message(string original_checkin,string original_checkout,int original_nights,string phone, string checkin, string checkout, int days)
        {
            MailMessage msg = new MailMessage();
            msg.From = new MailAddress("BlissHotel01@gmail.com");
            msg.To.Add(email);
            msg.Subject = "Hotel Reservation Confirmation";
            msg.Body = $@"<html>
                <body style='font-family: Arial, sans-serif;'>
                    <h2>Bliss Hotel: Reservation Update</h2>
                    <p>Dear Mr./Ms. {lastName},</p>
                    <p>We have received your request to update your reservation details at Hotel Bliss.</p>
                    <p>Your original reservation details were as follows:</p>
                    <ul>
                        <li>Original Phone Number: {phoneNumber}</li>
                        <li>Original Check-in Date: {original_checkin}</li>
                        <li>Original Check-out Date: {original_checkout}</li>
                        <li>Original Night(s): {original_nights}</li>
                    </ul>
                    <p>Your updated reservation details are as follows:</p>
                    <ul>
                        <li>New Phone Number: {phone}</li>
                        <li>New Check-in Date: {checkin}</li>
                        <li>New Check-out Date: {checkout}</li>
                        <li>New Night(s): {days}</li>
                    </ul>
                    <p>Your reservation has been updated successfully.</p>
                    <p>If you have any questions or need further assistance, please feel free to contact us.</p>
                    <p>We look forward to welcoming you to Hotel Bliss!</p>
                    <p>Best regards,</p>
                    <p>The Hotel Bliss Team</p>
                </body>
            </html>";
            msg.IsBodyHtml = true;
            msg.ReplyToList.Add(new MailAddress(email, lastName));

            using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
            {
                smtp.Credentials = new NetworkCredential("BlissHotel01@gmail.com", " kkvjnveupeyffqll");
                smtp.EnableSsl = true;
                smtp.Send(msg);
            }

        }

        private void Save_edit_Load(object sender, EventArgs e)
        {

        }

        private void firsttxt_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
