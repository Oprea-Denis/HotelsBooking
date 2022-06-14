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
        public partial class RezervariView : Form, IRezervariView
    {
            //Fields
            private string message;
            private bool isSuccessful;
            private bool isEdit;
    
        //Constructor
        public RezervariView()
            {
                InitializeComponent();
                AssociateAndRaiseViewEvents();
                tabControl1.TabPages.Remove(tabPageRezervariDetail);
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
                //Delete
                btnDelete.Click += delegate
                {
                    var result = MessageBox.Show("Are you sure you want to delete the selected Rezervations?", "Warning",
                          MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        DeleteEvent?.Invoke(this, EventArgs.Empty);
                        MessageBox.Show(Message);
                    }
                };
            }

            //Properties
            public string RezervariId
            {
                get { return txtRezervariId.Text; }
                set { txtRezervariId.Text = value; }
            }

            public string RezervariName
            {
                get { return txtRezervariName.Text; }
                set { txtRezervariName.Text = value; }
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
            public event EventHandler DeleteEvent;
            public event EventHandler SaveEvent;
            public event EventHandler CancelEvent;

            //Methods
            public void SetRezervariListBindingSource(BindingSource RezervariList)
            {
                dataGridView.DataSource = RezervariList;
            }

            //Singleton pattern (Open a single form instance)
            private static RezervariView instance;
            public static RezervariView GetInstace(Form parentContainer)
            {
                if (instance == null || instance.IsDisposed)
                {
                    instance = new RezervariView();
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

            private void panel1_Paint(object sender, PaintEventArgs e)
            {

            }
        }
    }
