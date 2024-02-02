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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace Marcial.Angelo.RRCAGApp
{
    public partial class VehicleDataForm : ACE.BIT.ADEV.Forms.VehicleDataForm
    {
        private OleDbConnection vehicleConnection;
        private OleDbDataAdapter vehicleAdapter;
        private DataSet vehicleDataset;
        private BindingSource vehicleBindingSource;

        // Represents an instance of the Vehicle Form.
        public VehicleDataForm()
        {
            InitializeComponent();

            this.vehicleBindingSource = new BindingSource();

            RetrieveData();

            BindControls();

            this.Load += new EventHandler(VehicleDataForm_Load);
            this.mnuFileClose.Click += MnuFileClose_Click;
            this.mnuFileSave.Click += MnuFileSave_Click;
            this.mnuEditRemove.Click += MnuEditDelete_Click;
            this.dgvVehicles.SelectionChanged += DgvVehicles_SelectionChanged;
            this.dgvVehicles.CellValueChanged += DgvVehicles_CellValueChanged;
            this.FormClosing += VehicleDataForm_FormClosing;
            this.dgvVehicles.RowsAdded += DgvVehicles_RowsAdded;
        }

        /// <summary>
        /// Handles the rows added event of dgvVehicles.
        /// </summary>
        private void DgvVehicles_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            if (this.dgvVehicles.SelectedRows.Count == 1 && (this.dgvVehicles.CurrentRow.IsNewRow))
            {
                this.dgvVehicles.CurrentRow.Cells[this.dgvVehicles.Columns["SortBy"].Index].Value = 0;
            }
        }

        /// <summary>
        /// Handles the FormClosing event of the VehicleDataForm.
        /// </summary>
        private void VehicleDataForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (this.mnuFileSave.Enabled == true)
                {
                    DialogResult result = MessageBox.Show("Do you wish to save changes?", "Save", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning,
                        MessageBoxDefaultButton.Button3);

                    switch (result)
                    {
                        case DialogResult.Cancel:
                            e.Cancel = true;
                            break;

                        case DialogResult.No:
                            vehicleConnection.Close();
                            vehicleConnection.Dispose();
                            break;

                        case DialogResult.Yes:
                            this.vehicleAdapter.Update(this.vehicleDataset, "VehicleStock");
                            this.dgvVehicles.EndEdit();
                            this.vehicleBindingSource.EndEdit();
                            break;
                    }
                }
            }
            catch (Exception)
            {
                DialogResult result = MessageBox.Show("An error occurred while saving. Do you still wish to close?", "Save Error", MessageBoxButtons.YesNo, MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button2);

                switch (result)
                {
                    case DialogResult.Yes:
                        vehicleConnection.Close();
                        vehicleConnection.Dispose();
                        break;

                    case DialogResult.No:
                        e.Cancel = true;
                        break;
                }
            }
        }

        /// <summary>
        /// Handles the Delete Click event of dgvVehicles.
        /// </summary>
        private void MnuEditDelete_Click(object sender, EventArgs e)
        {

            string message = "Are you sure you want to permanently delete stock item " + this.dgvVehicles.CurrentCell.Value.ToString() + "?";
            string caption = "Delete Stock Item";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            MessageBoxIcon icon = MessageBoxIcon.Warning;
            DialogResult result;

            result = MessageBox.Show(message, caption, buttons, icon, MessageBoxDefaultButton.Button2);

            try
            {
                switch (result)
                {
                    case DialogResult.Yes:
                        this.dgvVehicles.Rows.RemoveAt(this.dgvVehicles.CurrentRow.Index);
                        this.Text = "Vehicle Data";
                        this.mnuEditRemove.Enabled = false;
                        this.vehicleAdapter.Update(this.vehicleDataset, "VehicleStock");
                        this.dgvVehicles.EndEdit();
                        this.vehicleBindingSource.EndEdit();
                        this.mnuFileSave.Enabled = false;
                        break;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("An error occurred while deleting the selected vehicle.", "Deletion Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        /// <summary>
        /// Handles the cell value changed event of dgvVehicles.
        /// </summary>
        private void DgvVehicles_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            this.mnuFileSave.Enabled = true;
            this.Text = "* Vehicle Data";
        }

        /// <summary>
        /// Handles the selection changed event of dgvVehicles.
        /// </summary>
        private void DgvVehicles_SelectionChanged(object sender, EventArgs e)
        {
            if (this.dgvVehicles.CurrentRow.Selected)
            {
                this.mnuEditRemove.Enabled = true;
            }

            if (!this.dgvVehicles.CurrentRow.Selected || this.dgvVehicles.CurrentRow.IsNewRow)
            {
                this.mnuEditRemove.Enabled = false;
            }
        }

        /// <summary>
        /// Handles the event when the user clicks save on the VehicleDataForm.
        /// </summary>
        private void MnuFileSave_Click(object sender, EventArgs e)
        {
            try
            {
                this.vehicleAdapter.Update(this.vehicleDataset, "VehicleStock");

                this.dgvVehicles.EndEdit();

                this.vehicleBindingSource.EndEdit();

                this.mnuFileSave.Enabled = false;
            }
            catch (Exception)
            {
                MessageBox.Show("An error occurred while saving the changes to the vehicle data.", "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        /// <summary>
        /// Handles the event when the user clicks close on the VehicleDataForm.
        /// </summary>
        private void MnuFileClose_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        /// <summary>
        /// Handles the Load event of this form.
        /// </summary>
        private void VehicleDataForm_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            this.dgvVehicles.AllowUserToDeleteRows = false;
            this.dgvVehicles.AllowUserToResizeRows = false;
            this.dgvVehicles.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.mnuFileSave.Enabled = false;
            this.mnuEditRemove.Enabled = false;
            this.dgvVehicles.MultiSelect = false;

        }

        /// <summary>
        /// Queries the AMDatabase, populates the dataset, and builds needed commands.
        /// </summary>
        private void RetrieveData()
        {
            string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=AMDatabase.mdb";

            vehicleConnection = new OleDbConnection(connectionString);

            vehicleConnection.Open();

            string query = "SELECT ID, StockNumber, ManufacturedYear, Make, Model, Mileage, Automatic, Colour, BasePrice, SoldBy, OptionsPrice FROM VehicleStock";

            OleDbCommand command = new OleDbCommand(query, vehicleConnection);

            vehicleAdapter = new OleDbDataAdapter(command);

            vehicleDataset = new DataSet();

            vehicleAdapter.Fill(vehicleDataset, "VehicleStock");


            OleDbCommandBuilder builder = new OleDbCommandBuilder();

            builder.DataAdapter = vehicleAdapter;

            builder.ConflictOption = ConflictOption.OverwriteChanges;

            builder.GetInsertCommand();

            builder.GetUpdateCommand();

            builder.GetDeleteCommand();
        }

        /// <summary>
        /// Method for Binding the Controls.
        /// </summary>
        private void BindControls()
        {
            this.vehicleBindingSource.DataSource = this.vehicleDataset.Tables[0];
            this.dgvVehicles.DataSource = this.vehicleBindingSource;
            this.dgvVehicles.Columns["ID"].Visible = false;
            this.dgvVehicles.Columns["SoldBy"].Visible = false;
        }
    }
}
