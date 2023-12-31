﻿namespace OnlineMovieTicketBooking_2pillars.Views
{
    partial class frm_ReserChecking
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dgv_ReserList = new System.Windows.Forms.DataGridView();
            this.col_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_CustomerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_Phone = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_Email = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_MovieName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_Date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_Time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_TotalPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btn_Back = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_ReserList)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv_ReserList
            // 
            this.dgv_ReserList.AllowUserToAddRows = false;
            this.dgv_ReserList.AllowUserToDeleteRows = false;
            this.dgv_ReserList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_ReserList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_ReserList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.col_ID,
            this.col_CustomerName,
            this.col_Phone,
            this.col_Email,
            this.col_MovieName,
            this.col_Date,
            this.col_Time,
            this.col_TotalPrice});
            this.dgv_ReserList.Location = new System.Drawing.Point(0, 170);
            this.dgv_ReserList.Name = "dgv_ReserList";
            this.dgv_ReserList.ReadOnly = true;
            this.dgv_ReserList.RowHeadersWidth = 51;
            this.dgv_ReserList.RowTemplate.Height = 24;
            this.dgv_ReserList.Size = new System.Drawing.Size(1368, 447);
            this.dgv_ReserList.TabIndex = 0;
            // 
            // col_ID
            // 
            this.col_ID.HeaderText = "Mã vé";
            this.col_ID.MinimumWidth = 6;
            this.col_ID.Name = "col_ID";
            this.col_ID.ReadOnly = true;
            // 
            // col_CustomerName
            // 
            this.col_CustomerName.HeaderText = "Tên khách hàng";
            this.col_CustomerName.MinimumWidth = 6;
            this.col_CustomerName.Name = "col_CustomerName";
            this.col_CustomerName.ReadOnly = true;
            // 
            // col_Phone
            // 
            this.col_Phone.HeaderText = "Số điện thoại";
            this.col_Phone.MinimumWidth = 6;
            this.col_Phone.Name = "col_Phone";
            this.col_Phone.ReadOnly = true;
            // 
            // col_Email
            // 
            this.col_Email.HeaderText = "Email";
            this.col_Email.MinimumWidth = 6;
            this.col_Email.Name = "col_Email";
            this.col_Email.ReadOnly = true;
            // 
            // col_MovieName
            // 
            this.col_MovieName.HeaderText = "Tên phim";
            this.col_MovieName.MinimumWidth = 6;
            this.col_MovieName.Name = "col_MovieName";
            this.col_MovieName.ReadOnly = true;
            // 
            // col_Date
            // 
            this.col_Date.HeaderText = "Ngày";
            this.col_Date.MinimumWidth = 6;
            this.col_Date.Name = "col_Date";
            this.col_Date.ReadOnly = true;
            // 
            // col_Time
            // 
            this.col_Time.HeaderText = "Giờ";
            this.col_Time.MinimumWidth = 6;
            this.col_Time.Name = "col_Time";
            this.col_Time.ReadOnly = true;
            // 
            // col_TotalPrice
            // 
            this.col_TotalPrice.HeaderText = "Tổng tiền";
            this.col_TotalPrice.MinimumWidth = 6;
            this.col_TotalPrice.Name = "col_TotalPrice";
            this.col_TotalPrice.ReadOnly = true;
            // 
            // btn_Back
            // 
            this.btn_Back.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Back.Location = new System.Drawing.Point(1249, 623);
            this.btn_Back.Name = "btn_Back";
            this.btn_Back.Size = new System.Drawing.Size(110, 38);
            this.btn_Back.TabIndex = 1;
            this.btn_Back.Text = "Quay lại";
            this.btn_Back.UseVisualStyleBackColor = true;
            this.btn_Back.Click += new System.EventHandler(this.btn_Back_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(489, 69);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(400, 38);
            this.label1.TabIndex = 2;
            this.label1.Text = "THÔNG TIN VÉ ĐÃ ĐẶT";
            // 
            // frm_ReserChecking
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1371, 673);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_Back);
            this.Controls.Add(this.dgv_ReserList);
            this.Name = "frm_ReserChecking";
            this.Text = "Booked Ticket Information";
            this.Load += new System.EventHandler(this.frm_ReserChecking_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_ReserList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv_ReserList;
        private System.Windows.Forms.Button btn_Back;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_CustomerName;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_Phone;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_Email;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_MovieName;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_Date;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_Time;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_TotalPrice;
    }
}