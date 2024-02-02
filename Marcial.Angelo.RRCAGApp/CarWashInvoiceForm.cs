/*
 * Name: Angelo Marcial
 * Program: Business Information Technology
 * Course: ADEV-2008 Programming 2
 * Created: 2023-11-10
 * Updated: 2023-11-28
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
using Marcial.Angelo.Business;

namespace Marcial.Angelo.RRCAGApp
{
    public partial class CarWashInvoiceForm : ACE.BIT.ADEV.Forms.CarWashInvoiceForm
    {
        private BindingSource carWashInvoiceDataSource;

        // Represents an instance of the Car Wash Invoice Form.
        public CarWashInvoiceForm(BindingSource dataSource)
        {
            InitializeComponent();

            carWashInvoiceDataSource = dataSource;

            this.Load += CarWashInvoiceForm_Load;
        }

        private void CarWashInvoiceForm_Load(object sender, EventArgs e)
        {
            BindingControls();
        }

        private void BindingControls()
        {
            Binding packagePrice = new Binding("Text", this.carWashInvoiceDataSource, "PackageCost");
            this.lblPackagePrice.DataBindings.Add(packagePrice);
            packagePrice.FormattingEnabled = true;
            packagePrice.FormatString = "C";

            Binding fragrancePrice = new Binding("Text", this.carWashInvoiceDataSource, "FragranceCost");
            this.lblFragrancePrice.DataBindings.Add(fragrancePrice);
            fragrancePrice.FormattingEnabled = true;
            fragrancePrice.FormatString = "N2";

            Binding sub = new Binding("Text", this.carWashInvoiceDataSource, "Subtotal");
            this.lblSubtotal.DataBindings.Add(sub);
            sub.FormattingEnabled = true;
            sub.FormatString = "C";

            Binding pst = new Binding("Text", this.carWashInvoiceDataSource, "ProvincialSalesTaxCharged");
            this.lblProvincialSalesTax.DataBindings.Add(pst);
            pst.FormattingEnabled = true;
            pst.FormatString = "N2";

            Binding gst = new Binding("Text", this.carWashInvoiceDataSource, "GoodsAndServicesTaxCharged");
            this.lblGoodsAndServicesTax.DataBindings.Add(gst);
            gst.FormattingEnabled = true;
            gst.FormatString = "N2";

            Binding total = new Binding("Text", this.carWashInvoiceDataSource, "Total");
            this.lblTotal.DataBindings.Add(total);
            total.FormattingEnabled = true;
            total.FormatString = "C";
        }
    }
}
