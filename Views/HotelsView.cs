using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HotelsBooking.Views
{
    public partial class HotelsView : Form, IHotelsView
    {
        //Fields
        private string message;
        private bool isSuccessful;
        private bool isEdit;

        //Constructor
        public HotelsView()
        {
            InitializeComponent();
            AssociateAndRaiseViewEvents();
            tabControl1.TabPages.Remove(tabPageHotelsDetail);
            btnClose.Click += delegate { this.Close(); };
        }

        private void AssociateAndRaiseViewEvents()
        {
            //Search
            btnSearch.Click += delegate { SearchEvent?.Invoke(this, EventArgs.Empty); };
            txtSearch.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                    SearchEvent?.Invoke(this, EventArgs.Empty);
            };
            //Add new
            btnAddNew.Click += delegate
            {
                AddNewEvent?.Invoke(this, EventArgs.Empty);
                tabControl1.TabPages.Remove(tabPageHotelsList);
                tabControl1.TabPages.Add(tabPageHotelsDetail);
                tabPageHotelsDetail.Text = "Add new Hotels";
            };
            ////Reserv
            //btnRezervareNew.Click += delegate
            //{
            //    RezervareNewEvent?.Invoke(this, EventArgs.Empty);
            //    tabControl1.TabPages.Remove(tabPageHotelsList);
            //    tabControl1.TabPages.Rezervare(tabPageHotelsDetail);
            //    tabPageHotelsDetail.Text = "Reserv new Hotels";
            //};
            //Edit
            btnEdit.Click += delegate
            {
                EditEvent?.Invoke(this, EventArgs.Empty);
                tabControl1.TabPages.Remove(tabPageHotelsList);
                tabControl1.TabPages.Add(tabPageHotelsDetail);
                tabPageHotelsDetail.Text = "Edit Hotels";
            };
            //Save changes
            btnSave.Click += delegate
            {
                SaveEvent?.Invoke(this, EventArgs.Empty);
                if (isSuccessful)
                {
                    tabControl1.TabPages.Remove(tabPageHotelsDetail);
                    tabControl1.TabPages.Add(tabPageHotelsList);
                    //tabControl1.TabPages.Rezervare(tabPageHotelsList);
                }
                MessageBox.Show(Message);
            };
            //Cancel
            btnCancel.Click += delegate
            {
                CancelEvent?.Invoke(this, EventArgs.Empty);
                tabControl1.TabPages.Remove(tabPageHotelsDetail);
                tabControl1.TabPages.Add(tabPageHotelsList);
                //tabControl1.TabPages.Rezervare(tabPageHotelsList);
            };
            //Delete
            btnDelete.Click += delegate
            {
                var result = MessageBox.Show("Are you sure you want to delete the selected Hotels?", "Warning",
                      MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    DeleteEvent?.Invoke(this, EventArgs.Empty);
                    MessageBox.Show(Message);
                }
            };
        }

        //Properties
        public string HotelsId
        {
            get { return txtHotelsId.Text; }
            set { txtHotelsId.Text = value; }
        }

        public string HotelsName
        {
            get { return txtHotelsName.Text; }
            set { txtHotelsName.Text = value; }
        }
        public string SearchValue
        {
            get { return txtSearch.Text; }
            set { txtSearch.Text = value; }
        }

        public bool IsEdit
        {
            get { return isEdit; }
            set { isEdit = value; }
        }

        public bool IsSuccessful
        {
            get { return isSuccessful; }
            set { isSuccessful = value; }
        }

        public string Message
        {
            get { return message; }
            set { message = value; }
        }

        //Events
        public event EventHandler SearchEvent;
        public event EventHandler AddNewEvent;
        //public event EventHandler RezervareNewEvent;
        public event EventHandler EditEvent;
        public event EventHandler DeleteEvent;
        public event EventHandler SaveEvent;
        public event EventHandler CancelEvent;

        //Methods
        public void SetHotelsListBindingSource(BindingSource HotelsList)
        {
            dataGridView.DataSource = HotelsList;
        }

        //Singleton pattern (Open a single form instance)
        private static HotelsView instance;
        public static HotelsView GetInstace(Form parentContainer)
        {
            if (instance == null || instance.IsDisposed)
            {
                instance = new HotelsView();
                instance.MdiParent = parentContainer;
                instance.FormBorderStyle = FormBorderStyle.None;
                instance.Dock = DockStyle.Fill;
            }
            else
            {
                if (instance.WindowState == FormWindowState.Minimized)
                    instance.WindowState = FormWindowState.Normal;
                instance.BringToFront();
            }
            return instance;
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tabPageHotelsList_Click(object sender, EventArgs e)
        {

        }
    }
}
