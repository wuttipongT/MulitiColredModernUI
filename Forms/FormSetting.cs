using MulitiColredModernUI.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Resources;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace MulitiColredModernUI.Forms
{
    public partial class FormSetting : Form
    {
        //Fields
        private Button currentButton;
        private Random random;
        private int tempIndex;
        private Form activeForm;
        public List<Control> panelSubMenu = new List<Control>();
        public FormSetting()
        {
            InitializeComponent();
            random = new Random();
            btnCloseChildForm.Visible = false;
            this.Text = string.Empty;
            this.ControlBox = false;
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;

            List<Menu> m = new List<Menu>()
            {
                new Menu(){
                    Name="btnProducts",
                    Text="  Products",
                    Image=global::MulitiColredModernUI.Properties.Resources.alarm__1_,
                    DoWork=(sender, EventArgs)=> {
                        OpenChildForm(new Forms.FormProduct(), sender);
                    },
                    Items=new List<MenuItem>{
                        new MenuItem() {
                            Name="btn2",
                            Text="  test",
                            Image=global::MulitiColredModernUI.Properties.Resources.shopping_list,
                        },
                        new MenuItem() {
                            Name="btn4",
                            Text="  Test3",
                            Image=global::MulitiColredModernUI.Properties.Resources.value__1_,
                        }
                    },
                },
                new Menu(){
                    Name="btnCustomers",
                    Text="  Customer",
                    Image=global::MulitiColredModernUI.Properties.Resources.alarm__1_,
                    Items=null,
                    DoWork=(sender, EventArgs)=> {
                        OpenChildForm(new Forms.FormCustomers(), sender);
                    },
                },
                new Menu() {
                    Name="btnOrder",
                    Text="  Orders",
                    Image=global::MulitiColredModernUI.Properties.Resources.bar_chart,
                    DoWork=(sender, EventArgs)=> {
                        OpenChildForm(new Forms.FormOrders(), sender);
                    },
                    Items=new List<MenuItem>{ 
                        new MenuItem() {
                            Name="btnCar",
                            Text="  Car",
                            Image=global::MulitiColredModernUI.Properties.Resources.shopping_list,
                            
                        },
                        new MenuItem() {
                            Name="btnFan",
                            Text="  Fan",
                            Image=global::MulitiColredModernUI.Properties.Resources.value__1_,
                        }
                    }
                }
            };

            m.Reverse();

            foreach (var o in m) {
                Button btn = new Button();
                btn.Name = o.Name;
                btn.Text = o.Text;
                btn.Image = o.Image;

                btn.Dock = System.Windows.Forms.DockStyle.Top;
                btn.FlatAppearance.BorderSize = 0;
                btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                btn.ForeColor = System.Drawing.Color.Gainsboro;
                btn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
                btn.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
                btn.Size = new System.Drawing.Size(230, 60);
                btn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
                btn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
                
                if (o.Items != null)
                {
                    Panel p = new Panel();
                    p.Name = btn.Name + "SubMenuPanel";
                    p.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(32)))), ((int)(((byte)(39)))));
                    p.Dock = System.Windows.Forms.DockStyle.Top;
                    p.AutoSize = true;
                    p.Visible = false;

                    o.Items.Reverse();

                    foreach (var item in o.Items)
                    {
                        Button b = new Button();
                        b.Name = item.Name;
                        b.Text = item.Text;
                        b.Image = item.Image;

                        b.Dock = System.Windows.Forms.DockStyle.Top;
                        b.FlatAppearance.BorderSize = 0;
                        b.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(38)))), ((int)(((byte)(46)))));
                        b.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(38)))), ((int)(((byte)(46)))));
                        b.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                        b.ForeColor = System.Drawing.Color.Silver;
                        b.Location = new System.Drawing.Point(0, 0);
                        b.Padding = new System.Windows.Forms.Padding(35, 0, 0, 0);
                        b.Size = new System.Drawing.Size(183, 60);
                        b.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
                        b.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
                        b.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
                        b.UseVisualStyleBackColor = true;

                        p.Controls.Add(b);

                    }
                    panelMenu.Controls.Add(p);
                    panelSubMenu.Add(p);
                    btn.Click += (sender, EventArgs) => {
                        ShowSubMenu(p);  
                      o.DoWork(sender, EventArgs); 
                    };
                }
                else {
                    btn.Click += (sender, EventArgs) => {
                        HideSubMenu();
                        o.DoWork(sender, EventArgs); 
                    };
                }

                panelMenu.Controls.Add(btn);
            }

            panelMenu.Controls.SetChildIndex(panelLogo, -1);
           
        }

        private void FormSetting_Load(object sender, EventArgs e)
        {

        }

        private void ShowSubMenu(Panel subMenu)
        {
            
            if (subMenu.Visible == false)
            {
                HideSubMenu();
                subMenu.Visible = true;
            }
            else
                subMenu.Visible = false;
        }

        private void HideSubMenu()
        {
            foreach (var ctn in panelSubMenu) {
                ctn.Visible = false;
            }
        }

        public delegate void ShowSubMenuDelegate(object sender, EventArgs e, Panel p);

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private Color SelectThemeColor()
        {
            int index = random.Next(ThemeColor.coloList.Count);
            while (tempIndex == index)
            {
                index = random.Next(ThemeColor.coloList.Count);
            }
            tempIndex = index;
            string color = ThemeColor.coloList[index];
            return ColorTranslator.FromHtml(color);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        
        private void btnMaximize_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Maximized;
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
            }
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void panelTitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        //METODO PARA ARRASTRAR EL FORMULARIO---------------------------------------------------------------------
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void btnCloseChildForm_Click(object sender, EventArgs e)
        {
            if (activeForm != null)
            {
                activeForm.Close();
            }

            Reset();
        }

        private void ActiveButton(object btnSender)
        {
            if (btnSender != null)
            {
                if (currentButton != (Button)btnSender)
                {
                    DisableButton();
                    Color color = SelectThemeColor();
                    currentButton = (Button)btnSender;
                    currentButton.BackColor = color;
                    currentButton.ForeColor = Color.White;
                    currentButton.Font = new System.Drawing.Font("Segoe UI", 12.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
                    panelTitleBar.BackColor = color;
                    panelLogo.BackColor = ThemeColor.ChangeColorBrightness(color, -0.3);
                    ThemeColor.PrimaryColor = color;
                    ThemeColor.SecondaryColor = ThemeColor.ChangeColorBrightness(color, -0.3);
                    btnCloseChildForm.Visible = true;
                }
            }
        }
        private void DisableButton()
        {
            foreach (Control previosBtn in panelMenu.Controls)
            {
                if (previosBtn.GetType() == typeof(Button))
                {
                    previosBtn.BackColor = Color.FromArgb(51, 51, 76);
                    previosBtn.ForeColor = Color.Gainsboro;
                    previosBtn.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
                }
            }
        }

        private void OpenChildForm(Form childForm, object btnSender)
        {
            if (activeForm != null)
            {
                activeForm.Close();
            }

            ActiveButton(btnSender);
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            this.panelDesktopPane.Controls.Add(childForm);
            this.panelDesktopPane.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
            lblTitle.Text = childForm.Text;

        }
        private void Reset()
        {
            DisableButton();
            lblTitle.Text = "HOME";
            panelTitleBar.BackColor = Color.FromArgb(0, 150, 136);
            panelLogo.BackColor = Color.FromArgb(39, 39, 58);
            currentButton = null;
            btnCloseChildForm.Visible = false;
        }
    }

    public class Menu { 
        public string Name { get; set; }
        public string Text { get; set; }
        public Bitmap Image { get; set; }
        public List<MenuItem> Items { get; set; }

        public delegate void CallbackDelegate(object sender, EventArgs e);
        public CallbackDelegate DoWork { get; set; }

        
    }

    public class MenuItem {
        public string Name { get; set; }
        public string Text { get; set; }
        public Bitmap Image { get; set; }

        public delegate void CallbackDelegate(object sender, EventArgs e);
        public CallbackDelegate DoWork { get; set; }
    }
}
