namespace WineMsEvolutionGui
{
  partial class FrmMain
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
      this.menuStrip1 = new System.Windows.Forms.MenuStrip();
      this.mniFile = new System.Windows.Forms.ToolStripMenuItem();
      this.mniOpenLogFolder = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
      this.mniExit = new System.Windows.Forms.ToolStripMenuItem();
      this.mniProcess = new System.Windows.Forms.ToolStripMenuItem();
      this.mniProcessGeneralLedger = new System.Windows.Forms.ToolStripMenuItem();
      this.mniProcessSalesOrders = new System.Windows.Forms.ToolStripMenuItem();
      this.mniProcessPurchaseOrders = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
      this.mniCancel = new System.Windows.Forms.ToolStripMenuItem();
      this.statusStrip1 = new System.Windows.Forms.StatusStrip();
      this.tsVersion = new System.Windows.Forms.ToolStripStatusLabel();
      this.tsWineMSDatabase = new System.Windows.Forms.ToolStripStatusLabel();
      this.tsProgressBar = new System.Windows.Forms.ToolStripProgressBar();
      this.mniCreditNotes = new System.Windows.Forms.ToolStripMenuItem();
      this.menuStrip1.SuspendLayout();
      this.statusStrip1.SuspendLayout();
      this.SuspendLayout();
      // 
      // menuStrip1
      // 
      this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mniFile,
            this.mniProcess});
      this.menuStrip1.Location = new System.Drawing.Point(0, 0);
      this.menuStrip1.Name = "menuStrip1";
      this.menuStrip1.Size = new System.Drawing.Size(800, 24);
      this.menuStrip1.TabIndex = 0;
      this.menuStrip1.Text = "menuStrip1";
      // 
      // mniFile
      // 
      this.mniFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mniOpenLogFolder,
            this.toolStripMenuItem2,
            this.mniExit});
      this.mniFile.Name = "mniFile";
      this.mniFile.Size = new System.Drawing.Size(37, 20);
      this.mniFile.Text = "File";
      // 
      // mniOpenLogFolder
      // 
      this.mniOpenLogFolder.Name = "mniOpenLogFolder";
      this.mniOpenLogFolder.Size = new System.Drawing.Size(157, 22);
      this.mniOpenLogFolder.Text = "Open log folder";
      this.mniOpenLogFolder.Click += new System.EventHandler(this.mniOpenLogFolder_Click);
      // 
      // toolStripMenuItem2
      // 
      this.toolStripMenuItem2.Name = "toolStripMenuItem2";
      this.toolStripMenuItem2.Size = new System.Drawing.Size(154, 6);
      // 
      // mniExit
      // 
      this.mniExit.Name = "mniExit";
      this.mniExit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
      this.mniExit.Size = new System.Drawing.Size(157, 22);
      this.mniExit.Text = "Exit";
      this.mniExit.Click += new System.EventHandler(this.mniExit_Click);
      // 
      // mniProcess
      // 
      this.mniProcess.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mniCreditNotes,
            this.mniProcessGeneralLedger,
            this.mniProcessPurchaseOrders,
            this.mniProcessSalesOrders,
            this.toolStripMenuItem1,
            this.mniCancel});
      this.mniProcess.Name = "mniProcess";
      this.mniProcess.Size = new System.Drawing.Size(59, 20);
      this.mniProcess.Text = "Process";
      // 
      // mniProcessGeneralLedger
      // 
      this.mniProcessGeneralLedger.Name = "mniProcessGeneralLedger";
      this.mniProcessGeneralLedger.Size = new System.Drawing.Size(180, 22);
      this.mniProcessGeneralLedger.Text = "General Ledger";
      this.mniProcessGeneralLedger.Click += new System.EventHandler(this.mniProcessGeneralLedger_Click);
      // 
      // mniProcessSalesOrders
      // 
      this.mniProcessSalesOrders.Name = "mniProcessSalesOrders";
      this.mniProcessSalesOrders.Size = new System.Drawing.Size(180, 22);
      this.mniProcessSalesOrders.Text = "Sales Orders";
      this.mniProcessSalesOrders.Click += new System.EventHandler(this.mniProcessSalesOrders_Click);
      // 
      // mniProcessPurchaseOrders
      // 
      this.mniProcessPurchaseOrders.Name = "mniProcessPurchaseOrders";
      this.mniProcessPurchaseOrders.Size = new System.Drawing.Size(180, 22);
      this.mniProcessPurchaseOrders.Text = "Purchase Orders";
      this.mniProcessPurchaseOrders.Click += new System.EventHandler(this.mniProcessPurchaseOrders_Click);
      // 
      // toolStripMenuItem1
      // 
      this.toolStripMenuItem1.Name = "toolStripMenuItem1";
      this.toolStripMenuItem1.Size = new System.Drawing.Size(177, 6);
      // 
      // mniCancel
      // 
      this.mniCancel.Enabled = false;
      this.mniCancel.Name = "mniCancel";
      this.mniCancel.Size = new System.Drawing.Size(180, 22);
      this.mniCancel.Text = "Cancel";
      this.mniCancel.Click += new System.EventHandler(this.mniCancel_Click);
      // 
      // statusStrip1
      // 
      this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsVersion,
            this.tsWineMSDatabase,
            this.tsProgressBar});
      this.statusStrip1.Location = new System.Drawing.Point(0, 428);
      this.statusStrip1.Name = "statusStrip1";
      this.statusStrip1.Size = new System.Drawing.Size(800, 22);
      this.statusStrip1.TabIndex = 1;
      this.statusStrip1.Text = "statusStrip1";
      // 
      // tsVersion
      // 
      this.tsVersion.Name = "tsVersion";
      this.tsVersion.Size = new System.Drawing.Size(22, 17);
      this.tsVersion.Text = "1.0";
      // 
      // tsWineMSDatabase
      // 
      this.tsWineMSDatabase.Name = "tsWineMSDatabase";
      this.tsWineMSDatabase.Size = new System.Drawing.Size(71, 17);
      this.tsWineMSDatabase.Text = "WineMS-DB";
      // 
      // tsProgressBar
      // 
      this.tsProgressBar.Name = "tsProgressBar";
      this.tsProgressBar.Size = new System.Drawing.Size(100, 16);
      this.tsProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
      this.tsProgressBar.Visible = false;
      // 
      // mniCreditNotes
      // 
      this.mniCreditNotes.Name = "mniCreditNotes";
      this.mniCreditNotes.Size = new System.Drawing.Size(180, 22);
      this.mniCreditNotes.Text = "Credit Notes";
      this.mniCreditNotes.Click += new System.EventHandler(this.mniCreditNotes_Click);
      // 
      // FrmMain
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(800, 450);
      this.Controls.Add(this.statusStrip1);
      this.Controls.Add(this.menuStrip1);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MainMenuStrip = this.menuStrip1;
      this.Name = "FrmMain";
      this.Text = "WineMS-Sage-Evolution";
      this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
      this.Load += new System.EventHandler(this.FrmMain_Load);
      this.menuStrip1.ResumeLayout(false);
      this.menuStrip1.PerformLayout();
      this.statusStrip1.ResumeLayout(false);
      this.statusStrip1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.MenuStrip menuStrip1;
    private System.Windows.Forms.ToolStripMenuItem mniFile;
    private System.Windows.Forms.ToolStripMenuItem mniExit;
    private System.Windows.Forms.StatusStrip statusStrip1;
    private System.Windows.Forms.ToolStripStatusLabel tsVersion;
    private System.Windows.Forms.ToolStripStatusLabel tsWineMSDatabase;
    private System.Windows.Forms.ToolStripMenuItem mniProcess;
    private System.Windows.Forms.ToolStripMenuItem mniProcessGeneralLedger;
    private System.Windows.Forms.ToolStripMenuItem mniProcessSalesOrders;
    private System.Windows.Forms.ToolStripMenuItem mniProcessPurchaseOrders;
    private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
    private System.Windows.Forms.ToolStripMenuItem mniCancel;
    private System.Windows.Forms.ToolStripProgressBar tsProgressBar;
    private System.Windows.Forms.ToolStripMenuItem mniOpenLogFolder;
    private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
    private System.Windows.Forms.ToolStripMenuItem mniCreditNotes;
  }
}

