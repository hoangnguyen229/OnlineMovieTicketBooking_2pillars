﻿using OnlineMovieTicketBooking_2pillars.Models;
using OnlineMovieTicketBooking_2pillars.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace OnlineMovieTicketBooking_2pillars.Views
{
    public partial class frm_BookingTicketsUI : Form
    {
        private BookingInfo bookingInfo;
        private Customer customer;
        private int reserID;
        private decimal total = 0;
        private bool isBookingSucessful = false;

        public frm_BookingTicketsUI()
        {
            InitializeComponent();
            ControlSetting();
        }

        private void BookingTicketsUI_Load(object sender, EventArgs e)
        {
            try
            {
                this.CenterToScreen();
                // Button
                SeatInit();
                DefaultSetting();

                using (var dbContext = new MovieDBContext())
                {
                    cmb_MovieTitle.DataSource = dbContext.Movies.Select(c => new { c.ID, c.Name }).ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void DefaultSetting()
        {
            txt_MovieTitle.Text = string.Empty;
            txt_Date.Text = string.Empty;
            txt_Time.Text = string.Empty;

            list_SeatSelected.Items.Clear();
            total = 0;
            txt_Total.Text = total.ToString();
        }

        //  Button Seat Handle 
        #region "Button Seat Handle"

        private void SeatInit()
        {
            int x = 65, y = 68, i = 1;
            using (var dbContext = new MovieDBContext())
            {
                int seatSpacing = 108; // Khoảng cách mặc định giữa các ghế
                foreach (var seat in dbContext.Seats.ToList())
                {
                    SeatsDrawing(x, y, seat.Name, seat.ID);

                    if (seat.ID >= 21 && seat.ID <= 23)
                    {
                        seatSpacing = 190; // Đặt khoảng cách lớn hơn cho các ghế từ id 21 đến 23
                    }
                    else
                    {
                        seatSpacing = 108; // Sử dụng khoảng cách mặc định cho các ghế khác
                    }

                    if (i++ % 5 == 0)
                    {
                        y += 70;
                        x = 65;
                    }
                    else
                    {
                        x += seatSpacing;
                    }
                }
            }
        }
        private void SeatsDrawing(int x, int y, string name, int id)
        {
            Button btn = new Button();
            btn.Text = name;
            btn.TextAlign = ContentAlignment.MiddleCenter;
            btn.Tag = id;

            if (id >= 1 && id <= 20)
            {
                btn.Size = new Size(58, 38);
                btn.BackColor = Color.Yellow;

            }
            else if (id > 20 && id <= 23)
            {
                btn.Size = new Size(116, 38);
                btn.BackColor = Color.Aqua;
            }

            btn.Location = new Point(x, y);
            pnl_ListSeat.Controls.Add(btn);
            btn.Click += Btn_Click;
        }


        private decimal PriceSeat(Button button)
        {
            int btn = (int)button.Tag;
            decimal price = 0;
            if (btn >= 1 && btn <= 10)
            {
                price = 60000;
            }
            else if (btn >= 11 && btn <= 20)
            {
                price = 70000;
            }
            else if (btn >= 21 && btn <= 23)
            {
                price = 150000;
            }
            return price;
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            txt_Total.Font = new Font(txt_Total.Font, FontStyle.Bold);
            btn_Confirm.Enabled = true;

            int btnID = (int)btn.Tag; // Lấy ID từ Tag của nút

            if (btn.BackColor == Color.Yellow)
            {
                btn.BackColor = Color.Red;
                total += PriceSeat(btn);
                txt_Total.Text = total.ToString();

                // Info selected
                list_SeatSelected.Items.Add(btn.Text);
            }
            else if (btn.BackColor == Color.Aqua)
            {
                btn.BackColor = Color.Red;
                total += PriceSeat(btn);
                txt_Total.Text = total.ToString();

                list_SeatSelected.Items.Add(btn.Text);
            }
            else if (btn.BackColor == Color.Red)
            {
                if (btnID >= 1 && btnID <= 20)
                {
                    btn.BackColor = Color.Yellow;
                }
                else if (btnID >= 21 && btnID <= 23)
                {
                    btn.BackColor = Color.Aqua;
                }

                total -= PriceSeat(btn);
                txt_Total.Text = total.ToString();

                list_SeatSelected.Items.Remove(btn.Text);
            }
            else if (btn.BackColor == Color.Gray)
            {
                btn_Confirm.Enabled = false;
                MessageBox.Show("Ghế đã được đặt!");
            }
        }

        private void pnl_ListSeat_Paint(object sender, PaintEventArgs e)
        {
            pnl_ListSeat.BorderStyle = BorderStyle.FixedSingle;
        }

        private void UpdateButtonState()
        {
            using (var dbContext = new MovieDBContext())
            {
                foreach (var seat in dbContext.Seats.ToList())
                {
                    Button button = pnl_ListSeat.Controls.OfType<Button>().FirstOrDefault(b => (int)b.Tag == seat.ID);
                    if (button != null)
                    {
                        if (bookingInfo.SelectedSeats.Contains(seat.Name))
                        {
                            button.BackColor = Color.Gray;
                            // Vô hiệu hóa button
                        }
                    }
                }
            }
        }
        #endregion

        // Combobox Movie Handle
        #region "Combobox Movie and Combobox ScheduledMovie Handle"
        private void ControlSetting()
        {
            cmb_MovieTitle.DisplayMember = "Name";
            cmb_MovieTitle.ValueMember = "ID";
            cmb_MovieTitle.DropDownStyle = ComboBoxStyle.DropDownList;

            cmb_ShowTime.DisplayMember = "FullDateTime";
            cmb_ShowTime.ValueMember = "ID";
            cmb_ShowTime.DropDownStyle = ComboBoxStyle.DropDownList;
        }
        private void cmb_MovieTitle_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_MovieTitle.SelectedValue != null)
            {
                int selectedMovieID = int.Parse(cmb_MovieTitle.SelectedValue.ToString());

                using (var dbContext = new MovieDBContext())
                {
                    var scheduledMovies = dbContext.ScheduledMovies
                        .Where(s => s.MovieID == selectedMovieID)
                        .ToList();

                    var scheduledMoviesWithDateTime = scheduledMovies
                        .Select(s => new
                        {
                            s.ID,
                            FullDateTime = s.Date + "   " + s.Time // Kết hợp ngày và giờ thành một chuỗi
                        })
                        .ToList();

                    cmb_ShowTime.DataSource = scheduledMoviesWithDateTime;
                }
            }
        }
        #endregion

        // Groupbox Infor Selected
        #region "Groupbox infor selected"
        private void cmb_ShowTime_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_ShowTime.SelectedValue != null)
            {
                string selectedShowTime = cmb_ShowTime.Text;
                string[] parts = selectedShowTime.Split(new string[] { "   " }, StringSplitOptions.None);
                int idScheduledMovie = int.Parse(cmb_ShowTime.SelectedValue.ToString());
                int movieID = int.Parse(cmb_MovieTitle.SelectedValue.ToString());

                if (parts.Length >= 2)
                {
                    string showDate = parts[0];
                    string showTime = parts[1];

                    txt_Time.Text = showTime;
                    txt_Date.Text = showDate;
                    txt_MovieTitle.Text = cmb_MovieTitle.Text;
                }
                ResetSeats(movieID, idScheduledMovie);
            }
        }
        private void ResetSeats(int? movieID, int idScheduledMovie)
        {
            using (var dbContext = new MovieDBContext())
            {
                var exist = dbContext.Reservations.FirstOrDefault(x => x.ScheduledMovie.ID == idScheduledMovie && x.ScheduledMovie.MovieID == movieID);
                if (exist == null)
                {
                    foreach (Button button in pnl_ListSeat.Controls.OfType<Button>())
                    {
                        if (button.Tag is int btnID)
                        {
                            if (btnID >= 1 && btnID <= 20)
                            {
                                button.BackColor = Color.Yellow;
                            }
                            else if (btnID >= 21 && btnID <= 23)
                            {
                                button.BackColor = Color.Aqua;
                            }
                        }
                    }
                    list_SeatSelected.Items.Clear();
                    total = 0;
                    txt_Total.Text = total.ToString();
                }
                else
                {
                    // Đặt màu lại cho tất cả ghế trước khi cập nhật lại màu của các ghế đã đặt
                    foreach (Button button in pnl_ListSeat.Controls.OfType<Button>())
                    {
                        if (button.Tag is int btnID)
                        {
                            if (btnID >= 1 && btnID <= 20)
                            {
                                button.BackColor = Color.Yellow;
                            }
                            else if (btnID >= 21 && btnID <= 23)
                            {
                                button.BackColor = Color.Aqua;
                            }
                        }
                    }
                    list_SeatSelected.Items.Clear();

                    var listSeatExist = dbContext.SeatDetails.Where(s => s.Reservation.ScheduledMovie.ID == idScheduledMovie && s.Reservation.ScheduledMovie.MovieID == movieID).ToList();

                    foreach (var seat in listSeatExist)
                    {
                        Button button = pnl_ListSeat.Controls.OfType<Button>().FirstOrDefault(b => (int)b.Tag == seat.SeatID);

                        if (button != null)
                        {
                            button.BackColor = Color.Gray;
                        }
                    }
                }
            }
        }
        #endregion

        // Button Confirm
        #region "Button Confirm"
        public void Btn_Confirm_Click(object sender, EventArgs e)
        {
            try
            {
                if (InputChecked() == false)
                    throw new Exception("Vui lòng chọn đầy đủ thông tin trước khi Xác nhận!");
                else 
                {
                    using (var dbContext = new MovieDBContext())
                    {

                        DialogResult result = MessageBox.Show("Bạn có muốn xác nhận thông tin đã chọn?"
                            , "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (result == DialogResult.Yes)
                        {
                            var infoCustomerForm = new frm_InfoCustomer(this);
                            result = infoCustomerForm.ShowDialog();

                            customer = new Customer
                            {
                                FullName = infoCustomerForm.CustomerName,
                                Phone = infoCustomerForm.CustomerPhone,
                                Email = infoCustomerForm.CustomerEmail
                            };

                            dbContext.Customers.Add(customer);
                            dbContext.SaveChanges();

                            // Tiếp tục xử lý thông tin vé ở đây
                            bookingInfo = new BookingInfo
                            {
                                MovieID = int.Parse(cmb_MovieTitle.SelectedValue.ToString()),
                                ScheduledMovieID = int.Parse(cmb_ShowTime.SelectedValue.ToString()),
                                SelectedSeats = list_SeatSelected.Items.Cast<string>().ToList(),
                                TotalPrice = total
                            };

                            int customerID = customer.ID;

                            var reservation = new Reservation
                            {
                                CustomerID = customerID,
                                ScheduleID = bookingInfo.ScheduledMovieID,
                                TotalPrice = bookingInfo.TotalPrice
                            };
                            foreach (var seatName in bookingInfo.SelectedSeats)
                            {
                                var seat = dbContext.Seats.SingleOrDefault(s => s.Name == seatName);
                                if (seat != null)
                                {
                                    reservation.SeatDetails.Add(new SeatDetail
                                    {
                                        SeatID = seat.ID
                                    });
                                }
                            }
                            dbContext.Reservations.Add(reservation);
                            dbContext.SaveChanges();

                            reserID = reservation.ID;
                            MessageBox.Show("Đặt vé thành công!");

                            DefaultSetting();
                            UpdateButtonState();
                            isBookingSucessful = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        // Button PayAway
        #region "Button PayAway"
        private void btn_PayAway_Click(object sender, EventArgs e)
        {
            try
            {
                // Đặt vé thành công rồi mới được tiếp tục
                if (isBookingSucessful == true)
                {
                    MessageBox.Show("Bạn đã thanh toán thành công");

                    frm_TicketsUI frmTickets = new frm_TicketsUI(this);
                    frmTickets.SetTicketInformation(reserID.ToString(), cmb_MovieTitle.Text, cmb_ShowTime.Text
                                                    , customer.FullName, customer.Phone, customer.Email
                                                    , bookingInfo.TotalPrice.ToString()
                                                    , list_SeatSelected.Items.Cast<string>().ToList());

                    frmTickets.SetListSeats(bookingInfo.SelectedSeats);
                    frmTickets.Show();
                }
                else
                {
                    MessageBox.Show("Hãy nhập đầy đủ thông tin trước khi thanh toán");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        // Input Checked
        #region "Input Checked"
        private bool InputChecked()
        {
            
            err_Warning.Clear();
            if (string.IsNullOrEmpty(txt_MovieTitle.Text) && string.IsNullOrWhiteSpace(txt_MovieTitle.Text))
            {
                err_Warning.SetError(txt_MovieTitle, "Vui lòng chọn phim!");
                return false;
            }
            if (string.IsNullOrEmpty(txt_Date.Text) && string.IsNullOrWhiteSpace(txt_Date.Text))
            {
                err_Warning.SetError(txt_Date, "Vui lòng chọn ngày chiếu!");
                return false;
            }
            if (string.IsNullOrEmpty(txt_Time.Text) && string.IsNullOrWhiteSpace(txt_Time.Text))
            {
                err_Warning.SetError(txt_Time, "Vui lòng chọn giờ chiếu!");
                return false;
            }
            if (list_SeatSelected.Items.Count == 0)
            {
                err_Warning.SetError(list_SeatSelected, "Vui lòng chọn ghế!");
                return false;
            }
            return true;
        }
        #endregion

        private void btn_Close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void menuItem_Login_Click(object sender, EventArgs e)
        {
            this.Hide();
            frm_Login frm = new frm_Login();
            frm.Show();
        }

        private void đăngNhậpToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frm_Login frm_Login = new frm_Login();
            frm_Login.ShowDialog();
        }
    }
}

