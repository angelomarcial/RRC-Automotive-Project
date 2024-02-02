/*
 * Name: Angelo Marcial
 * Program: Business Information Technology
 * Course: ADEV-2008 Programming 2
 * Created: 2023-10-28
 * Updated: 2023-12-12
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Marcial.Angelo.RRCAGApp
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            this.mnuFileOpenSalesQuote.Click += MnuFileOpenSalesQuote_Click;
            this.mnuFileOpenCarWash.Click += MnuFileOpenCarWash_Click;
            this.mnuFileExit.Click += MnuFileExit_Click;
            this.mnuDataVehicles.Click += MnuDataVehicles_Click;
            this.mnuHelpAbout.Click += MnuHelpAbout_Click;

            this.WindowState = FormWindowState.Maximized;
        }

        /// <summary>
        /// Handles the Vehicle menu item click event.
        /// </summary>
        private void MnuDataVehicles_Click(object sender, EventArgs e)
        {
            foreach (Form vehicle in this.MdiChildren)
            {
                if (vehicle.GetType() == typeof(VehicleDataForm))
                {
                    vehicle.Activate();
                    return;
                }
            }

            try
            {
                VehicleDataForm instance = new VehicleDataForm();
                instance.MdiParent = MainForm.ActiveForm;
                instance.Show();

            }
            catch (Exception)
            {
                MessageBox.Show("Unable to load vehicle data.", "Data Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Handles the CarWash menu item click event.
        /// </summary>
        private void MnuFileOpenCarWash_Click(object sender, EventArgs e)
        {
            try
            {
                CarWashForm form = new CarWashForm();

                form.MdiParent = MainForm.ActiveForm;

                form.Show();
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("Fragrances data file not found.", "Data File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            catch (Exception)
            {
                MessageBox.Show("An error occurred while reading the data file.", "Data File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Handles the Click event of the about menu item.
        /// </summary>
        private void MnuHelpAbout_Click(object sender, EventArgs e)
        {
            AboutForm form = new AboutForm();

            form.ShowDialog();
        }

        /// <summary>
        /// Handles the Click event of the exit menu item.
        /// </summary>
        private void MnuFileExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Handles the Click event of the sales quote menu item.
        /// </summary>
        private void MnuFileOpenSalesQuote_Click(object sender, EventArgs e)
        {
            SalesQuoteForm form = new SalesQuoteForm();

            form.MdiParent = this;

            form.Show();
        }
    }
}
