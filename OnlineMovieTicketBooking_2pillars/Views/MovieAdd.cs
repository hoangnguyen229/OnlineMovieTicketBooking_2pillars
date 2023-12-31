﻿using OnlineMovieTicketBooking_2pillars.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OnlineMovieTicketBooking_2pillars.Views
{
    public partial class frm_MovieAdd : Form
    {
        public static MovieDBContext context = new MovieDBContext();
        List<Movie> listMovie = context.Movies.ToList();
        public frm_MovieAdd()
        {
            InitializeComponent();
            BindGrid(listMovie);
        }

        private void BindGrid(List<Movie> listMovie)
        {
            dgv_MovieList.Rows.Clear();
            foreach (var item in listMovie)
            {
                int index = dgv_MovieList.Rows.Add();
                dgv_MovieList.Rows[index].Cells[0].Value = item.ID;
                dgv_MovieList.Rows[index].Cells[1].Value = item.Name;
                dgv_MovieList.Rows[index].Cells[2].Value = item.Employee.FullName;
            }
        }

        private bool CheckInput()
        {
            err_Warning.Clear();
            if (string.IsNullOrEmpty(txt_MovieID.Text))
            {
                err_Warning.SetError(txt_MovieID, "Mã phim không được để trống!");
                return false;
            }
            if (txt_MovieID.Text.Any(Char.IsLetter))
            {
                err_Warning.SetError(txt_MovieID, "Mã phim phải là số!");
                return false;
            }
            if (string.IsNullOrEmpty(txt_MovieName.Text))
            {
                err_Warning.SetError(txt_MovieName, "Tên phim không được để trống!");
                return false;
            }
            return true;
        }

        private bool CheckInput2()
        {
            err_Warning.Clear();
            if (string.IsNullOrEmpty(txt_MovieID.Text))
            {
                err_Warning.SetError(txt_MovieID, "Mã phim không được để trống!");
                return false;
            }
            return true;
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            if (CheckInput())
            {
                int id = int.Parse(txt_MovieID.Text);
                Movie existingMovie = context.Movies.FirstOrDefault(s => s.ID == id);
                if (existingMovie == null)
                {
                    Movie movie = new Movie()
                    {
                        ID = int.Parse(txt_MovieID.Text),
                        Name = txt_MovieName.Text,
                        EmployeeID = GlobalVariables.UserID
                    };
                    context.Movies.Add(movie);
                    context.SaveChanges();
                    MessageBox.Show("Thêm thông tin thành công!");
                }
                else
                    MessageBox.Show("Mã phim đã tồn tại!");
            }
            List<Movie> listMovie = context.Movies.Include("Employee").ToList();
            BindGrid(listMovie);
        }

        private void btn_Update_Click(object sender, EventArgs e)
        {
            if (CheckInput())
            {
                int id = int.Parse(txt_MovieID.Text);
                Movie existingMovie = context.Movies.FirstOrDefault(s => s.ID == id);
                if (existingMovie != null)
                {
                    existingMovie.Name = txt_MovieName.Text;
                    context.SaveChanges();
                    MessageBox.Show("Sửa thông tin thành công!");
                }
                else
                    MessageBox.Show("Mã phim không tồn tại!");
            }
            List<Movie> listMovie = context.Movies.Include("Employee").ToList();
            BindGrid(listMovie);
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            if (CheckInput2())
            {
                try
                {
                    int id = int.Parse(txt_MovieID.Text);
                    Movie existingMovie = context.Movies.FirstOrDefault(s => s.ID == id);
                    if (existingMovie != null)
                    {
                        ScheduledMovie existingSchedule = context.ScheduledMovies.FirstOrDefault(p => p.MovieID == id);
                        if (existingSchedule != null)
                            throw new Exception("Phim đã có lịch chiếu! Không thể xóa!");
                        else
                        {
                            DialogResult dr = MessageBox.Show("Bạn có muốn xóa không?", "CẢNH BÁO", MessageBoxButtons.YesNo);
                            if (dr == DialogResult.Yes)
                            {
                                context.Movies.Remove(existingMovie);
                                context.SaveChanges();
                                MessageBox.Show("Xóa thông tin thành công!");
                                List<Movie> listMovie = context.Movies.Include("Employee").ToList();
                                BindGrid(listMovie);
                            }
                        }
                    }
                    else
                        MessageBox.Show("Mã phim không tồn tại!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btn_Back_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgv_MovieList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                int index = e.RowIndex;
                txt_MovieID.Text = dgv_MovieList.Rows[index].Cells[0].Value.ToString();
                txt_MovieName.Text = dgv_MovieList.Rows[index].Cells[1].Value.ToString();
            }
        }

        private void frm_MovieAdd_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
        }
    }
}
