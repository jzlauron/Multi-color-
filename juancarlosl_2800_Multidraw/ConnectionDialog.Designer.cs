namespace juancarlosl_2800_Multidraw
{
    partial class ConnectionDialog
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
            this.UI_lblAddress = new System.Windows.Forms.Label();
            this.UI_tbAddress = new System.Windows.Forms.TextBox();
            this.UI_lblPort = new System.Windows.Forms.Label();
            this.UI_numUDPOrt = new System.Windows.Forms.NumericUpDown();
            this.UI_btnConnect = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.UI_numUDPOrt)).BeginInit();
            this.SuspendLayout();
            // 
            // UI_lblAddress
            // 
            this.UI_lblAddress.AutoSize = true;
            this.UI_lblAddress.Location = new System.Drawing.Point(27, 13);
            this.UI_lblAddress.Name = "UI_lblAddress";
            this.UI_lblAddress.Size = new System.Drawing.Size(45, 13);
            this.UI_lblAddress.TabIndex = 0;
            this.UI_lblAddress.Text = "Address";
            // 
            // UI_tbAddress
            // 
            this.UI_tbAddress.Location = new System.Drawing.Point(30, 30);
            this.UI_tbAddress.Name = "UI_tbAddress";
            this.UI_tbAddress.Size = new System.Drawing.Size(100, 20);
            this.UI_tbAddress.TabIndex = 1;
            // 
            // UI_lblPort
            // 
            this.UI_lblPort.AutoSize = true;
            this.UI_lblPort.Location = new System.Drawing.Point(27, 65);
            this.UI_lblPort.Name = "UI_lblPort";
            this.UI_lblPort.Size = new System.Drawing.Size(26, 13);
            this.UI_lblPort.TabIndex = 2;
            this.UI_lblPort.Text = "Port";
            // 
            // UI_numUDPOrt
            // 
            this.UI_numUDPOrt.Location = new System.Drawing.Point(30, 81);
            this.UI_numUDPOrt.Maximum = new decimal(new int[] {
            49152,
            0,
            0,
            0});
            this.UI_numUDPOrt.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.UI_numUDPOrt.Name = "UI_numUDPOrt";
            this.UI_numUDPOrt.Size = new System.Drawing.Size(100, 20);
            this.UI_numUDPOrt.TabIndex = 4;
            this.UI_numUDPOrt.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // UI_btnConnect
            // 
            this.UI_btnConnect.Location = new System.Drawing.Point(30, 118);
            this.UI_btnConnect.Name = "UI_btnConnect";
            this.UI_btnConnect.Size = new System.Drawing.Size(100, 23);
            this.UI_btnConnect.TabIndex = 5;
            this.UI_btnConnect.Text = "Connect";
            this.UI_btnConnect.UseVisualStyleBackColor = true;
            this.UI_btnConnect.Click += new System.EventHandler(this.UI_btnConnect_Click);
            // 
            // ConnectionDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(163, 152);
            this.Controls.Add(this.UI_btnConnect);
            this.Controls.Add(this.UI_numUDPOrt);
            this.Controls.Add(this.UI_lblPort);
            this.Controls.Add(this.UI_tbAddress);
            this.Controls.Add(this.UI_lblAddress);
            this.Name = "ConnectionDialog";
            this.Text = "ConnectionDialog";
            ((System.ComponentModel.ISupportInitialize)(this.UI_numUDPOrt)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label UI_lblAddress;
        private System.Windows.Forms.TextBox UI_tbAddress;
        private System.Windows.Forms.Label UI_lblPort;
        private System.Windows.Forms.NumericUpDown UI_numUDPOrt;
        private System.Windows.Forms.Button UI_btnConnect;
    }
}