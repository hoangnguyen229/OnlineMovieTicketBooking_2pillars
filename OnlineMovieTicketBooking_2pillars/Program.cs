﻿using OnlineMovieTicketBooking_2pillars.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OnlineMovieTicketBooking_2pillars
{
    public static class GlobalVariables
    {
        public static int UserID { get; set; }
        public static int UserRole { get; set; }
        public static int AccountID { get; set; }
        public static int ReserID { get; set; }
    }
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new BookingTicketsUI());
            //Application.Run(new frm_EmployeeAdd());
        }
    }
}
   