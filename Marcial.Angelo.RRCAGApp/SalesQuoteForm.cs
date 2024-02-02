/*
 * Name: Angelo Marcial
 * Program: Business Information Technology
 * Course: ADEV-2008 Programming 2
 * Created: 2023-10-28
 * Updated: 2023-11-04
 */

using Marcial.Angelo.Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Marcial.Angelo.RRCAGApp
{
    public partial class SalesQuoteForm : Form
    {
        public SalesQuoteForm()
        {
            InitializeComponent();

            this.btnCalculateQuote.Click += BtnCalculateQuote_Click;
            this.txtVehicleSalePrice.TextChanged += TxtVehicleSalePriceTradeIn_TextChanged;
            this.txtTradeInValue.TextChanged += TxtVehicleSalePriceTradeIn_TextChanged;
            this.chkStereoSystem.CheckedChanged += ChkBoxRadNud_CheckedChanged;
            this.chkComputerNavigation.CheckedChanged += ChkBoxRadNud_CheckedChanged;
            this.chkLeatherInterior.CheckedChanged += ChkBoxRadNud_CheckedChanged;
            this.radStandard.CheckedChanged += ChkBoxRadNud_CheckedChanged;
            this.radPearlized.CheckedChanged += ChkBoxRadNud_CheckedChanged;
            this.radCustomizedDetailing.CheckedChanged += ChkBoxRadNud_CheckedChanged;
            this.nudNumberOfYears.ValueChanged += ChkBoxRadNud_CheckedChanged;
            this.nudAnnualInterestRate.ValueChanged += ChkBoxRadNud_CheckedChanged;
            this.btnReset.Click += BtnReset_Click;
        }

        /// <summary>
        /// Handles the Reset button click event.
        /// </summary>
        private void BtnReset_Click(object sender, EventArgs e)
        {
            string message = "Do you want to reset the form?";
            string caption = "Reset Form";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            MessageBoxIcon icon = MessageBoxIcon.Warning;
            DialogResult result;

            result = MessageBox.Show(message, caption, buttons, icon, MessageBoxDefaultButton.Button2);

            if (result == DialogResult.Yes)
            {
                this.ClearOutputs();
                this.txtTradeInValue.Text = "0";
                this.txtVehicleSalePrice.Text = "";
                this.nudAnnualInterestRate.Text = "5";
                this.nudNumberOfYears.Text = "1";
                this.radStandard.Checked = true;
                this.chkStereoSystem.Checked = false;
                this.chkLeatherInterior.Checked = false;
                this.chkComputerNavigation.Checked = false;
                this.errorProvider.SetError(this.txtVehicleSalePrice, string.Empty);
                this.errorProvider.SetError(this.txtTradeInValue, string.Empty);
                this.txtVehicleSalePrice.Focus();
            }
        }

        /// <summary>
        /// Handles the Click event of the calculate quote button.
        /// </summary>
        private void BtnCalculateQuote_Click(object sender, EventArgs e)
        {
            ErrorChecking();
            CalculateSalesQuote();
        }

        /// <summary>
        /// Method that calculates the required outputs.
        /// </summary>
        private void CalculateSalesQuote()
        {
            if (this.errorProvider.GetError(this.txtVehicleSalePrice).Equals(string.Empty) &&
                this.errorProvider.GetError(this.txtTradeInValue).Equals(string.Empty))
            {
                decimal vehicleSalePrice = Decimal.Parse(this.txtVehicleSalePrice.Text);
                decimal tradeInAmount = Decimal.Parse(this.txtTradeInValue.Text);
                decimal salesTaxRate = 0.13m;
                Accessories accessoriesChosen = Accessories.None;
                ExteriorFinish exteriorFinishChosen = ExteriorFinish.None;

                SalesQuote salesQuote = new SalesQuote(vehicleSalePrice, tradeInAmount, salesTaxRate, accessoriesChosen, exteriorFinishChosen);

                this.lblVehicleSalePrice.Text = vehicleSalePrice.ToString("C");

                if (tradeInAmount > 0)
                {
                    this.lblTradeIn.Text = $"-{tradeInAmount.ToString("0.00")}";
                }
                else
                {
                    this.lblTradeIn.Text = tradeInAmount.ToString();
                }

                // Accessories Calculations.
                // Stereo.
                if (this.chkStereoSystem.Checked)
                {
                    salesQuote.AccessoriesChosen = Accessories.StereoSystem;

                    // Stereo & Leather.
                    if (this.chkStereoSystem.Checked && this.chkLeatherInterior.Checked)
                    {
                        salesQuote.AccessoriesChosen = Accessories.StereoAndLeather;
                    }

                    // Stereo & Computer Navigation.
                    if (this.chkStereoSystem.Checked && this.chkComputerNavigation.Checked)
                    {
                        salesQuote.AccessoriesChosen = Accessories.StereoAndNavigation;
                    }
                }

                // Leather.
                else if (this.chkLeatherInterior.Checked)
                {
                    salesQuote.AccessoriesChosen = Accessories.LeatherInterior;

                    // Leather & Navigation.
                    if (this.chkLeatherInterior.Checked && this.chkComputerNavigation.Checked)
                    {
                        salesQuote.AccessoriesChosen = Accessories.LeatherAndNavigation;
                    }
                }

                // Computer Navigation.
                else if (this.chkComputerNavigation.Checked)
                {
                    salesQuote.AccessoriesChosen = Accessories.ComputerNavigation;
                }

                // None.
                else if (this.chkStereoSystem.Checked && this.chkLeatherInterior.Checked &&
                   this.chkComputerNavigation.Checked)
                {
                    salesQuote.AccessoriesChosen = Accessories.None;
                }

                // All.
                if (this.chkStereoSystem.Checked && this.chkLeatherInterior.Checked &&
                    this.chkComputerNavigation.Checked)
                {
                    salesQuote.AccessoriesChosen = Accessories.All;
                }

                // Exterior Finish Calculations.
                // Standard.
                if (this.radStandard.Checked)
                {
                    salesQuote.ExteriorFinishChosen = ExteriorFinish.Standard;
                }

                // Pearlized.
                if (this.radPearlized.Checked)
                {
                    salesQuote.ExteriorFinishChosen = ExteriorFinish.Pearlized;
                }

                // Custom.
                if (this.radCustomizedDetailing.Checked)
                {
                    salesQuote.ExteriorFinishChosen = ExteriorFinish.Custom;
                }

                // Options Calculation

                decimal totalOptions = salesQuote.TotalOptions;

                this.lblOptions.Text = totalOptions.ToString();

                // SubTotal Calculation
                decimal subtotal = salesQuote.SubTotal;

                this.lblSubtotal.Text = subtotal.ToString("C");

                // Sales Tax Amount Calculation
                decimal salesTaxAmount = salesQuote.SalesTax;

                this.lblSalesTax.Text = salesTaxAmount.ToString();

                // Total Calculation
                decimal total = salesQuote.Total;

                this.lblTotal.Text = total.ToString("C");

                // Amount Due Calculation
                decimal amountDue = salesQuote.AmountDue;

                this.lblAmountDue.Text = amountDue.ToString("C");

                // Monthly Payment
                decimal rate = (this.nudAnnualInterestRate.Value / 12) / 100;
                decimal numberOfPayments = this.nudNumberOfYears.Value * 12;
                decimal monthlyPayment = Financial.GetPayment(rate, Decimal.ToInt32(numberOfPayments), amountDue);

                this.lblMonthlyPayment.Text = monthlyPayment.ToString("C");
            }
        }

        private void ErrorChecking()
        {
            this.errorProvider.SetError(this.txtVehicleSalePrice, string.Empty);
            this.errorProvider.SetError(this.txtTradeInValue, string.Empty);

            decimal vehicleSalePrice = 0m;
            decimal tradeInAmount = 0m;

            try
            {
                vehicleSalePrice = Decimal.Parse(this.txtVehicleSalePrice.Text);

                if (vehicleSalePrice <= 0)
                {
                    this.errorProvider.SetError(this.txtVehicleSalePrice, "Vehicle price cannot be less than or equal to 0.");
                }
            }
            catch (FormatException)
            {

                if (this.txtVehicleSalePrice.Text.Equals(string.Empty))
                {
                    this.errorProvider.SetError(this.txtVehicleSalePrice, "Vehicle price is a required field.");
                }
                else
                {
                    this.errorProvider.SetError(this.txtVehicleSalePrice, "Vehicle price cannot contain letters or special characters.");
                }
            }

            try
            {
                tradeInAmount = Decimal.Parse(this.txtTradeInValue.Text);

                if (tradeInAmount < 0)
                {
                    this.errorProvider.SetError(this.txtTradeInValue, "Trade-in value cannot be less than 0.");
                }

                if (vehicleSalePrice > 0 && (tradeInAmount > vehicleSalePrice))
                {
                    this.errorProvider.SetError(this.txtTradeInValue, "Trade-in value cannot exceed the vehicle sale price.");
                }
            }
            catch (FormatException)
            {
                if (this.txtTradeInValue.Text.Equals(string.Empty))
                {
                    this.errorProvider.SetError(this.txtTradeInValue, "Trade-in value is a required field.");
                }
                else
                {
                    this.errorProvider.SetError(this.txtTradeInValue, "Trade-in value cannot contain letters or special characters.");
                }
            }
        }

        /// <summary>
        /// Method that clears the outputs.
        /// </summary>
        private void ClearOutputs()
        {
            this.lblVehicleSalePrice.Text = string.Empty;
            this.lblOptions.Text = string.Empty;
            this.lblSubtotal.Text = string.Empty;
            this.lblSalesTax.Text = string.Empty;
            this.lblTotal.Text = string.Empty;
            this.lblTradeIn.Text = string.Empty;
            this.lblAmountDue.Text = string.Empty;
            this.lblMonthlyPayment.Text = string.Empty;
        }

        /// <summary>
        /// Handles the event where Vehicle Sale Price and Trade In Value text is changed.
        /// </summary>
        private void TxtVehicleSalePriceTradeIn_TextChanged(object sender, EventArgs e)
        {
            ClearOutputs();
        }

        /// <summary>
        /// Handles the event where the check boxes, radio buttons, and the numeric up down buttons are changed.
        /// </summary>
        private void ChkBoxRadNud_CheckedChanged(object sender, EventArgs e)
        {
            if (this.lblMonthlyPayment.Text != (string.Empty))
            {
                CalculateSalesQuote();
            }
        }
    }
}
