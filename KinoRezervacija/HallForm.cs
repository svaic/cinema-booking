﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace KinoRezervacija
{
    [Serializable]
    partial class HallForm : Form
    {
        public Hall CurrHall { get; set; }
        public List<MovieTicket> SeatsBought { get; set; }

        private List<int> SelectedSeats;
        public HallForm(Hall hall)
        {
            InitializeComponent();

            CurrHall = hall;

            HallNameLB.Text = CurrHall.HallName;
            MovieTitleLB.Text = CurrHall.CurrentMoviePlaying.Name;
            SinglePriceLB.Text = string.Format("Price: {0} ден", CurrHall.CurrentMoviePlaying.Price.ToString());
            GenreLB.Text = string.Format("Genre: {0}", CurrHall.CurrentMoviePlaying.Genre);

            SelectedSeats = new List<int>();
            SeatsBought = new List<MovieTicket>();

            LoadSeats();
        }

        private void LoadSeats()
        {
            for (int i=0;i< CurrHall.Size;i++)
            {
                var pictureBox1 = new PictureBox();
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBox1.Image = Properties.Resources.HallSeat;
                pictureBox1.MouseEnter += PictureBox_MouseEnter;
                pictureBox1.MouseLeave += PictureBox_MouseLeave;
                pictureBox1.MouseClick += PictureBox_Click;

                pictureBox1.Tag = i;

                if (CurrHall.BookedSeats[i])
                    pictureBox1.BackColor = Color.Gray;

                SeatsFlowLayout.Controls.Add(pictureBox1);
            }

        }

        private void PictureBox_Click(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right) return;

            PictureBox pictureBox = (PictureBox)sender;
            int index = (int)pictureBox.Tag;

            if (CurrHall.BookedSeats[index]) return;

            if (!SelectedSeats.Contains(index))
            {
                pictureBox.BackColor = Color.Gray;
                SelectedSeats.Add(index);
            }
            else
            {
                pictureBox.BackColor = Color.Transparent;
                SelectedSeats.Remove(index);
            }
            RefreshPrice();
        }

        private void PictureBox_MouseLeave(object sender, EventArgs e)
        {
            var picturebox = (PictureBox)sender;
            int index = (int)picturebox.Tag;

            if (CurrHall.BookedSeats[index]) return;
            if (SelectedSeats.Contains(index)) return;

            picturebox.BackColor = Color.Transparent;
        }

        private void PictureBox_MouseEnter(object sender, EventArgs e)
        {
            var picturebox = (PictureBox)sender;
            int index = (int)picturebox.Tag;

            if (CurrHall.BookedSeats[index]) return;
            if (SelectedSeats.Contains(index)) return;

            picturebox.BackColor = Color.OrangeRed;
        }

        private void BuyBtn_Click(object sender, EventArgs e)
        {
            SeatsBought.Clear();
            foreach (int index in SelectedSeats)
            {
                CurrHall.BookedSeats[index] = true;
                SeatsBought.Add(new MovieTicket(CurrHall, index));
            }

            SelectedSeats.Clear();
            RefreshPrice();
            this.Close();
        }

        private void RefreshPrice()
        {
            PriceLB.Text = string.Format("Count: {0} To pay: {1} ден", SelectedSeats.Count, SelectedSeats.Count * CurrHall.CurrentMoviePlaying.Price);
        }

    }
}
