/*
 * Name: Angelo Marcial
 * Program: Business Information Technology
 * Course: ADEV-2008 Programming 2
 * Created: 2023-11-10
 * Updated: 2023-12-09
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
using System.IO;
using ACE.BIT.ADEV.CarWash;
using Marcial.Angelo.Business;

namespace Marcial.Angelo.RRCAGApp
{
    public partial class CarWashForm : ACE.BIT.ADEV.Forms.CarWashForm
    {
        private BindingList<Package> packages;
        private BindingSource packageBindingSource;
        private BindingSource fragranceBindingSource;
        private BindingList<CarWashItem> fragrances;

        private List<string> standardInteriorServices;
        private List<string> standardExteriorServices;

        private List<string> deluxeInteriorServices;
        private List<string> deluxeExteriorServices;

        private List<string> executiveInteriorServices;
        private List<string> executiveExteriorServices;

        private List<string> luxuryInteriorServices;
        private List<string> luxuryExteriorServices;

        private CarWashInvoice carWashInvoiceInstance;
        private BindingSource carWashInvoiceSource;


        // Represents an instance of the Car Wash Form.
        public CarWashForm()
        {
            InitializeComponent();

            // Creates a new list of the car wash item type.
            this.fragrances = new BindingList<CarWashItem>();

            // Creates a new list of package type.
            this.packages = new BindingList<Package>();

            // Creates a new binding source for fragrances.
            this.fragranceBindingSource = new BindingSource();
            this.fragranceBindingSource.DataSource = this.fragrances;

            // Sets the data source for fragranceBindingSource.
            this.cboFragrance.DataSource = fragranceBindingSource;

            // Sets the display member to be the names of the packages.
            this.cboPackage.DisplayMember = "Description";

            // Creates a new binding source for packages.
            this.packageBindingSource = new BindingSource();
            this.packageBindingSource.DataSource = this.packages;

            // Sets the data source for packageBindingSource.
            this.cboPackage.DataSource = packageBindingSource;

            // Sets the display member to be the names of the fragrances.
            this.cboFragrance.DisplayMember = "Description";

            // Creates a new list for Standard Interior and Exterior services.
            this.standardInteriorServices = new List<string>();
            this.standardExteriorServices = new List<string>();

            // Creates a new list for the Deluxe Interior and Exterior services.
            this.deluxeInteriorServices = new List<string>();
            this.deluxeExteriorServices = new List<string>();


            // Creates a new list for the Executive Interior and Exterior services.
            this.executiveInteriorServices = new List<string>();
            this.executiveExteriorServices = new List<string>();

            // Creates a new list for the Luxury Interior and Exterior services.
            this.luxuryInteriorServices = new List<string>();
            this.luxuryExteriorServices = new List<string>();

            // Creates a new instance of a binding source related to CarWashInvoice.
            this.carWashInvoiceSource = new BindingSource();

            // Code responsible for finding and loading the fragrances.txt file.
            string filepath = "fragrances.txt";

            FileStream fileStream;

            fileStream = new FileStream(filepath, FileMode.Open, FileAccess.Read);

            StreamReader fileReader;
            fileReader = new StreamReader(fileStream);

            // Creates a new instance of CarWashItem.
            CarWashItem carWashItem = new CarWashItem();

            // Creates a new list to be able to sort the binding list.
            List<CarWashItem> sortList = new List<CarWashItem>();

            // While loop that goes through the fragrances.txt file and adds the data into the sortList List.
            while (fileReader.Peek() != -1)
            {
                string record = fileReader.ReadLine();

                char[] delimiters = { ',' };

                string[] fields = record.Split(delimiters);

                string description = fields[0];

                decimal price = Decimal.Parse(fields[1]);

                carWashItem = new CarWashItem();

                carWashItem.Description = description;

                carWashItem.Price = price;

                sortList.Add(carWashItem);
            }

            carWashItem = new CarWashItem();

            // Adds Pine to the sortList List.
            carWashItem.Description = "Pine";

            carWashItem.Price = 0.00m;

            sortList.Add(carWashItem);

            // Sorts sortList.
            sortList.Sort();

            // Goes through each item in sortList and adds it to the fragrances Binding List.
            for (int i = 0; i < sortList.Count; i++)
            {
                this.fragrances.Add(sortList[i]);
            }

            // Adds the standard services to its respective list.
            this.standardInteriorServices.Add("Fragrance");
            this.standardExteriorServices.Add("Hand Wash");

            // Adds the deluxe services to its respective list.
            this.deluxeInteriorServices.Add("Fragrance");
            this.deluxeInteriorServices.Add("Shampoo Carpets");
            this.deluxeExteriorServices.Add("Hand Wash");
            this.deluxeExteriorServices.Add("Hand Wax");

            // Adds the executive services to its respective list.
            this.executiveInteriorServices.Add("Fragrance");
            this.executiveInteriorServices.Add("Shampoo Carpets");
            this.executiveInteriorServices.Add("Shampoo Upholstery");

            this.executiveExteriorServices.Add("Hand Wash");
            this.executiveExteriorServices.Add("Hand Wax");
            this.executiveExteriorServices.Add("Wheel Polish");

            // Adds the luxury services to its respective list.
            this.luxuryInteriorServices.Add("Fragrance");
            this.luxuryInteriorServices.Add("Shampoo Carpets");
            this.luxuryInteriorServices.Add("Shampoo Upholstery");
            this.luxuryInteriorServices.Add("Interior Protection Coat");

            this.luxuryExteriorServices.Add("Hand Wash");
            this.luxuryExteriorServices.Add("Hand Wax");
            this.luxuryExteriorServices.Add("Wheel Polish");
            this.luxuryExteriorServices.Add("Detail Engine");

            // Creates the Standard Package.
            Package standard = new Package();
            standard.Description = "Standard";
            standard.Price = 7.50M;
            standard.InteriorServices = standardInteriorServices;
            standard.ExteriorServices = standardExteriorServices;

            // Creates the Deluxe Package.
            Package deluxe = new Package();
            deluxe.Description = "Deluxe";
            deluxe.Price = 15.00M;
            deluxe.InteriorServices = deluxeInteriorServices;
            deluxe.ExteriorServices = deluxeExteriorServices;

            // Creates the Executive Package.
            Package executive = new Package();
            executive.Description = "Executive";
            executive.Price = 35.00M;
            executive.InteriorServices = executiveInteriorServices;
            executive.ExteriorServices = executiveExteriorServices;

            // Creates the Luxury Package.
            Package luxury = new Package();
            luxury.Description = "Luxury";
            luxury.Price = 55.00M;
            luxury.InteriorServices = luxuryInteriorServices;
            luxury.ExteriorServices = luxuryExteriorServices;

            // Populates the list with the needed items.
            this.packages.Add(standard);
            this.packages.Add(deluxe);
            this.packages.Add(executive);
            this.packages.Add(luxury);

            // Subscription to the needed events.
            this.Load += CarWashForm_Load;
            this.cboPackage.SelectedIndexChanged += CboPackageAndFragrance_SelectedIndexChanged;
            this.cboFragrance.SelectedIndexChanged += CboPackageAndFragrance_SelectedIndexChanged;
            this.mnuToolsGenerateInvoice.Click += MnuToolsGenerateInvoice_Click;
            this.mnuFileClose.Click += MnuFileClose_Click;

        }

        // Handles the File Close click event.
        private void MnuFileClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Handles the Generate Invoice click event.
        private void MnuToolsGenerateInvoice_Click(object sender, EventArgs e)
        {
            CarWashInvoiceForm carWashInvoice = new CarWashInvoiceForm(this.carWashInvoiceSource);

            carWashInvoice.FormClosed += CarWashForm_Load;

            carWashInvoice.ShowDialog();
        }

        // Handles the load event of the Car Wash Form.
        private void CarWashForm_Load(object sender, EventArgs e)
        {
            this.mnuToolsGenerateInvoice.Enabled = false;

            this.lstInterior.Items.Clear();
            this.lstExterior.Items.Clear();

            this.lblSubtotal.ResetText();
            this.lblGoodsAndServicesTax.ResetText();
            this.lblProvincialSalesTax.ResetText();
            this.lblTotal.ResetText();

            // Makes the default selected package blank.
            this.cboPackage.SelectedIndex = -1;

            // Makes the default selected fragrance Pine.
            this.cboFragrance.SelectedIndex = 4;
        }

        // Method for updating Package and Fragrance 
        private void UpdatePackageAndFragrance(Package currentPackage, CarWashItem currentFragrance)
        {
            this.lstInterior.Items.Clear();
            this.lstExterior.Items.Clear();

            // Add the new items
            for (int i = 0; i < currentPackage.InteriorServices.Count; i++)
            {
                this.lstExterior.Items.Add(currentPackage.ExteriorServices[i]);
                this.lstInterior.Items.Add(currentPackage.InteriorServices[i]);
            }

            // Responsible for making the selected fragrance the current one whenever the user changes the selected package.
            this.lstInterior.Items.RemoveAt(0);
            this.lstInterior.Items.Insert(0, String.Format("Fragrance - {0}", this.cboFragrance.SelectedItem));

            // Initializes variables for the CarWashInvoice Object below.
            decimal provincialSalesTax = 0;
            decimal goodsAndServicesTaxRate = 0.13m;
            decimal packageCost = currentPackage.Price;
            decimal fragranceCost = currentFragrance.Price;

            // Construct instance of carWashInvoice object.
            this.carWashInvoiceInstance = new CarWashInvoice(provincialSalesTax, goodsAndServicesTaxRate, packageCost, fragranceCost);

            // Construct instance of CarWashInvoice BindingSource.
            this.carWashInvoiceSource.DataSource = this.carWashInvoiceInstance;

            UpdateOutputLabels();

            this.mnuToolsGenerateInvoice.Enabled = true;
        }

        // Handles the SelectedIndexChanged event of Package and Fragrance
        private void CboPackageAndFragrance_SelectedIndexChanged(object sender, EventArgs e)
        {
            Package currentPackage = (Package)this.cboPackage.SelectedItem;
            CarWashItem currentFragrance = (CarWashItem)this.cboFragrance.SelectedItem;

            if (this.cboPackage.SelectedItem != null)
            {
                UpdatePackageAndFragrance(currentPackage, currentFragrance);
            }
        }

        // Method that is responsible for updating the output labels.
        private void UpdateOutputLabels()
        {
            // Clears the Data Bindings whenever the selected index of fragrance changes.
            this.lblSubtotal.DataBindings.Clear();
            this.lblGoodsAndServicesTax.DataBindings.Clear();
            this.lblProvincialSalesTax.DataBindings.Clear();
            this.lblTotal.DataBindings.Clear();

            // Creates new DataBinds for the output labels whenever the selected index of fragrance changes.
            Binding sub = new Binding("Text", this.carWashInvoiceSource, "SubTotal");
            this.lblSubtotal.DataBindings.Add(sub);
            sub.FormattingEnabled = true;
            sub.FormatString = "C";

            Binding goodsAndServices = new Binding("Text", this.carWashInvoiceSource, "GoodsAndServicesTaxCharged");
            this.lblGoodsAndServicesTax.DataBindings.Add(goodsAndServices);
            goodsAndServices.FormattingEnabled = true;
            goodsAndServices.FormatString = "N2";

            Binding provincial = new Binding("Text", this.carWashInvoiceSource, "ProvincialSalesTaxCharged");
            this.lblProvincialSalesTax.DataBindings.Add(provincial);
            provincial.FormattingEnabled = true;
            provincial.FormatString = "N2";

            Binding total = new Binding("Text", this.carWashInvoiceSource, "Total");
            this.lblTotal.DataBindings.Add(total);
            total.FormattingEnabled = true;
            total.FormatString = "C";
        }
    }
}









