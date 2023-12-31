﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OnlineMovieTicketBooking_2pillars.Models;
using BCrypt;
using BCrypt.Net;

namespace OnlineMovieTicketBooking_2pillars.Views
{
    public partial class frm_Login : Form
    {
        private static MovieDBContext context = new MovieDBContext();
        public frm_Login()
        {
            InitializeComponent();
            txt_Password.PasswordChar = '*';
        }

        private void btn_Login_Click(object sender, EventArgs e)
        {
            try
            {
                Account existingAccount = context.Accounts.FirstOrDefault(s => s.Username.Equals(txt_Username.Text));
                if (existingAccount != null)
                {
                    bool isPasswordMatch = BCrypt.Net.BCrypt.Verify(txt_Password.Text, existingAccount.Password);
                    if (isPasswordMatch)
                    {
                        if (existingAccount.RoleID == -1)
                            throw new Exception("Tài khoản đã bị vô hiệu!");
                        GlobalVariables.UserID = existingAccount.UserID;
                        GlobalVariables.UserRole = existingAccount.Role.ID;
                        GlobalVariables.AccountID = existingAccount.ID;
                        this.Hide();
                        frm_EmployeeHome frm = new frm_EmployeeHome();
                        frm.ShowDialog();
                    }
                    else
                        throw new Exception("Tài khoản hoặc mật khẩu không đúng!");
                }
                else
                {
                    throw new Exception("Tài khoản hoặc mật khẩu không đúng!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo", MessageBoxButtons.OK);
            }
        }

        private void btn_Back_Click(object sender, EventArgs e)
        {
            GlobalVariables.UserID = -1;
            GlobalVariables.UserRole = -1;
            GlobalVariables.AccountID = -1;
            Application.Restart();
        }

        private void frm_Login_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
        }
    }
}
