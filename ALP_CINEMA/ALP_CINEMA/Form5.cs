using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace ALP_CINEMA
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }
        string path = @".\alp_cinema\listcinema.txt";
        List<string> judul = new List<string>();
        List<string> poster = new List<string>();
        private int currentImageIndex = 0;


        private void Form5_Load(object sender, EventArgs e)
        {
            timer1.Start();
            using (StreamReader p = new StreamReader(path))
            {
                string b;
                while ((b = p.ReadLine()) != null)
                {
                    string[] parts = b.Split(',');
                    if (parts.Length == 2)
                    {
                        judul.Add(parts[0]);
                        poster.Add(parts[1]);
                    }
                }
            }
            int count = 0;
            int hello = 1;
            for (int i = 0; i < judul.Count; i++)
            {
                if (hello > 0)
                {
                    pictureBox1.Image = Image.FromFile(poster[i]);
                }
                PictureBox picturebox = new PictureBox();
                picturebox.Image = Image.FromFile(poster[i]);
                picturebox.Size = new Size(200, 200);
                picturebox.SizeMode = PictureBoxSizeMode.Zoom;
                picturebox.Location = new Point((count % 3) * 220 + 50, (count / 3) * 300 + 250);
                this.Controls.Add(picturebox);
                count++;
                picturebox.Tag = poster[i];
                /*picturebox.Click += PictureBox_Click;*/

                Label label = new Label();
                label.Text = judul[i];
                label.Font = new Font("Constantia", 12, FontStyle.Bold);
                label.Size = new Size(picturebox.Size.Width - 20, 50);
                label.Location = new Point(picturebox.Left + (picturebox.Width - label.Width) / 2, picturebox.Bottom + 5);
                label.AutoSize = false;
                label.TextAlign = ContentAlignment.TopCenter;
                label.ForeColor = Color.White;

                this.Controls.Add(label);
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            int slideSpeed = 5; // Adjust the sliding speed as per your preference
            int panelWidth = panel1.Width;
            int pictureWidth = pictureBox1.Width;

            if (pictureBox1.Left + pictureWidth <= 0)
            {
                // Picture has moved off the panel, reset its position to the right
                pictureBox1.Left = panelWidth;

                // Move to the next image in the ImageList
                currentImageIndex++;
                if (currentImageIndex >= poster.Count)
                {
                    currentImageIndex = 0; // Start from the first image if all have been displayed
                }
                pictureBox1.Image = Image.FromFile(poster[currentImageIndex]);
            }
            else
            {
                // Move the picture towards the left
                pictureBox1.Left -= slideSpeed;
            }
        }
        private void panel1_Resize(object sender, EventArgs e)
        {
            pictureBox1.Left = panel1.Width; // Reset picture's position to the right
        }

    }
}
