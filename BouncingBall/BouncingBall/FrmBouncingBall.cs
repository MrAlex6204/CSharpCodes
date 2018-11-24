using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace BouncingBall {
    public partial class FrmBouncing : Form {

        #region FormDesigner
        private Timer Tmr;
        private PictureBox PictureBall;
        private IContainer components;
        private NotifyIcon SystrayIcon;
        private ContextMenuStrip CtxBouncingBall;
        private ToolStripMenuItem mnuItemClose;
        #endregion

        private int Vx = 0, Vy = 0, xLeftBound = 0, xRightBound = 0, StellarMargin = 3, Counter = 1;
        private Point PosBall = new Point(30, 30);
        private bool IsStellarEnabled = false;
        private ToolStripMenuItem mnuSettings;
        private ToolStripMenuItem enableStellarToolStripMenuItem;
        private Queue<PictureBox> BallStellar = new Queue<PictureBox>();
        public FrmBouncing() {
            InitializeComponent();
            var Rnd = new Random();

            //Get Random speed for x & y 
            Vx = Rnd.Next(10, 20);
            Vy = Rnd.Next(10, 20);

            if (Screen.AllScreens.Length > 1) {
                /*
                 * The user has more than one screen
                 * Lets try to get the X Left & Right Bounds
                 */
                xLeftBound = 0;
                xRightBound = Screen.PrimaryScreen.Bounds.Size.Width * Screen.AllScreens.Length;
                this.WindowState = FormWindowState.Normal;
                this.Size = new Size(Screen.PrimaryScreen.Bounds.Size.Width * Screen.AllScreens.Length, Screen.PrimaryScreen.Bounds.Size.Height);
                this.Location = new Point(0, 0);

            } else {
                this.WindowState = FormWindowState.Maximized;
                xLeftBound = Screen.PrimaryScreen.Bounds.Left;
                xRightBound = Screen.PrimaryScreen.Bounds.Right;
            }

        }

        private void Tmr_Tick(object sender, EventArgs e) {
            Draw();
        }

        private void enableStellarToolStripMenuItem_Click(object sender, EventArgs e) {
            enableStellarToolStripMenuItem.Checked = !enableStellarToolStripMenuItem.Checked;
            IsStellarEnabled = enableStellarToolStripMenuItem.Checked;

            if (!IsStellarEnabled) {//Clear ball stellar
                foreach(var iBall in BallStellar) {
                    this.Controls.Remove(iBall);
                    iBall.Dispose();
                }
                BallStellar.Clear();
            }

        }

        private void Form1_Load(object sender, EventArgs e) {
            SystrayIcon.ShowBalloonTip(5500, "Bouncing Ball", "Running in background", ToolTipIcon.Info);
            SystrayIcon.Visible = true;
        }

        private void Form1_Paint(object sender, PaintEventArgs e) {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;
        }

        private void mnuItemClose_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void Draw() {

            /*
             * Get the new position of x & y
             * Every time Draw function is called
             */
            PosBall.X = PosBall.X + Vx;
            PosBall.Y = PosBall.Y + Vy;

            //Check new position Bounds
            if (PosBall.X <= xLeftBound || (PosBall.X + PictureBall.Size.Width) >= xRightBound) {
                Vx = -Vx;// Reverse the value sign from positive to negative or vice versa
                PosBall.X = PosBall.X + Vx;
            }

            if (PosBall.Y <= 0 || PosBall.Y + PictureBall.Size.Height >= this.Height) {
                Vy = -Vy;// Reverse the value sign from positive to negative or vice versa
                PosBall.Y = PosBall.Y + Vy;
            }


            PictureBall.SendToBack();
            PictureBall.Image.RotateFlip(RotateFlipType.Rotate90FlipNone);
            PictureBall.Refresh();
            PictureBall.Location = PosBall;

            if (IsStellarEnabled) {//Do this only when Stellar is enabled

                if ((Counter % StellarMargin) == 0) {
                    //Create a new Picture box and copy the main properties from the curret position 
                    PictureBox ItemBall = new PictureBox() { Image = PictureBall.Image, Location = PictureBall.Location, SizeMode = PictureBoxSizeMode.AutoSize };

                    BallStellar.Enqueue(ItemBall);

                    if (BallStellar.Count > 10) {
                        //Remove Ball from the queue
                        PictureBox ItemBallRemoved = BallStellar.Dequeue();
                        this.Controls.Remove(ItemBallRemoved);
                        ItemBallRemoved.Dispose();
                    }

                    if (BallStellar.Count > 1) {
                        //Render Ball Stellar
                        foreach (var iBall in BallStellar) {
                            iBall.Image.RotateFlip(RotateFlipType.Rotate90FlipNone);
                            iBall.Refresh();
                            this.Controls.Add(iBall);
                        }
                    }
                }

                Counter++;
            }

        }

        #region FormDesigner
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmBouncing));
            this.Tmr = new System.Windows.Forms.Timer(this.components);
            this.PictureBall = new System.Windows.Forms.PictureBox();
            this.SystrayIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.CtxBouncingBall = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuItemClose = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.enableStellarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBall)).BeginInit();
            this.CtxBouncingBall.SuspendLayout();
            this.SuspendLayout();
            // 
            // Tmr
            // 
            this.Tmr.Enabled = true;
            this.Tmr.Interval = 50;
            this.Tmr.Tick += new System.EventHandler(this.Tmr_Tick);
            // 
            // PictureBall
            // 
            this.PictureBall.Image = global::BouncingBall.Properties.Resources.Ball;
            this.PictureBall.Location = new System.Drawing.Point(12, 12);
            this.PictureBall.Name = "PictureBall";
            this.PictureBall.Size = new System.Drawing.Size(48, 48);
            this.PictureBall.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.PictureBall.TabIndex = 0;
            this.PictureBall.TabStop = false;
            // 
            // SystrayIcon
            // 
            this.SystrayIcon.ContextMenuStrip = this.CtxBouncingBall;
            this.SystrayIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("SystrayIcon.Icon")));
            this.SystrayIcon.Text = "Bouncing Ball";
            this.SystrayIcon.Visible = true;
            // 
            // CtxBouncingBall
            // 
            this.CtxBouncingBall.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuSettings,
            this.mnuItemClose});
            this.CtxBouncingBall.Name = "CtxBouncingBall";
            this.CtxBouncingBall.Size = new System.Drawing.Size(153, 70);
            // 
            // mnuItemClose
            // 
            this.mnuItemClose.Name = "mnuItemClose";
            this.mnuItemClose.Size = new System.Drawing.Size(152, 22);
            this.mnuItemClose.Text = "Close";
            this.mnuItemClose.Click += new System.EventHandler(this.mnuItemClose_Click);
            // 
            // mnuSettings
            // 
            this.mnuSettings.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.enableStellarToolStripMenuItem});
            this.mnuSettings.Name = "mnuSettings";
            this.mnuSettings.Size = new System.Drawing.Size(152, 22);
            this.mnuSettings.Text = "Settings";
            // 
            // enableStellarToolStripMenuItem
            // 
            this.enableStellarToolStripMenuItem.Name = "enableStellarToolStripMenuItem";
            this.enableStellarToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.enableStellarToolStripMenuItem.Text = "Enable Stellar";
            this.enableStellarToolStripMenuItem.Click += new System.EventHandler(this.enableStellarToolStripMenuItem_Click);
            // 
            // FrmBouncing
            // 
            this.BackColor = System.Drawing.Color.Magenta;
            this.ClientSize = new System.Drawing.Size(351, 231);
            this.Controls.Add(this.PictureBall);
            this.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmBouncing";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TopMost = true;
            this.TransparencyKey = System.Drawing.Color.Magenta;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.PictureBall)).EndInit();
            this.CtxBouncingBall.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

    }
}
