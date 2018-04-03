using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientGraphique
{
    public partial class Form1 : Form
    {
        private string city;
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            city = textBox1.Text;
            ServiceVelibReference.WSVelibClient client = new ServiceVelibReference.WSVelibClient();
            string[] stations = client.GetListStations(textBox1.Text);
         

            listBox1.DataSource = stations;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

         

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            ServiceVelibReference.WSVelibClient client = new ServiceVelibReference.WSVelibClient();
            ServiceVelibReference.VelibStation station;
            System.Console.WriteLine(listBox1.SelectedItem.ToString());
            station = client.getStation(city, listBox1.SelectedItem.ToString());
            label3.Text = station.Name;
            label4.Text = "Available bikes " + station.AvailableBikes.ToString();
            label5.Text = "Available bike stands " + station.AvailableBikeStands.ToString();
            label6.Text = "Bike stands " + station.BikeStands.ToString();
            
            label7.Text = (station.Banking) ? "Accepts credit card" : "Doesn't accept credit card ";

            
            
        }
    }
}
