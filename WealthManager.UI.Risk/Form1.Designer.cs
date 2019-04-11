namespace WealthManager.UI.Risk
{
    partial class Form1
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
            this.lblCash = new System.Windows.Forms.Label();
            this.lblStocks = new System.Windows.Forms.Label();
            this.lblBonds = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.lblSpeculative = new System.Windows.Forms.Label();
            this.lblAsset = new System.Windows.Forms.Label();
            this.lblAllocation = new System.Windows.Forms.Label();
            this.lblStocksP = new System.Windows.Forms.Label();
            this.lblBondsP = new System.Windows.Forms.Label();
            this.lblSpecP = new System.Windows.Forms.Label();
            this.lblCashP = new System.Windows.Forms.Label();
            this.lblRiskT = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblCash
            // 
            this.lblCash.AutoSize = true;
            this.lblCash.Location = new System.Drawing.Point(13, 98);
            this.lblCash.Name = "lblCash";
            this.lblCash.Size = new System.Drawing.Size(34, 13);
            this.lblCash.TabIndex = 0;
            this.lblCash.Text = "Cash:";
            // 
            // lblStocks
            // 
            this.lblStocks.AutoSize = true;
            this.lblStocks.Location = new System.Drawing.Point(13, 126);
            this.lblStocks.Name = "lblStocks";
            this.lblStocks.Size = new System.Drawing.Size(43, 13);
            this.lblStocks.TabIndex = 1;
            this.lblStocks.Text = "Stocks:";
            // 
            // lblBonds
            // 
            this.lblBonds.AutoSize = true;
            this.lblBonds.Location = new System.Drawing.Point(13, 156);
            this.lblBonds.Name = "lblBonds";
            this.lblBonds.Size = new System.Drawing.Size(40, 13);
            this.lblBonds.TabIndex = 2;
            this.lblBonds.Text = "Bonds:";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(12, 38);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(195, 21);
            this.comboBox1.TabIndex = 3;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // lblSpeculative
            // 
            this.lblSpeculative.AutoSize = true;
            this.lblSpeculative.Location = new System.Drawing.Point(12, 189);
            this.lblSpeculative.Name = "lblSpeculative";
            this.lblSpeculative.Size = new System.Drawing.Size(66, 13);
            this.lblSpeculative.TabIndex = 4;
            this.lblSpeculative.Text = "Speculative:";
            // 
            // lblAsset
            // 
            this.lblAsset.AutoSize = true;
            this.lblAsset.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAsset.Location = new System.Drawing.Point(12, 62);
            this.lblAsset.Name = "lblAsset";
            this.lblAsset.Size = new System.Drawing.Size(55, 20);
            this.lblAsset.TabIndex = 5;
            this.lblAsset.Text = "Asset";
            // 
            // lblAllocation
            // 
            this.lblAllocation.AutoSize = true;
            this.lblAllocation.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAllocation.Location = new System.Drawing.Point(121, 62);
            this.lblAllocation.Name = "lblAllocation";
            this.lblAllocation.Size = new System.Drawing.Size(88, 20);
            this.lblAllocation.TabIndex = 6;
            this.lblAllocation.Text = "Allocation";
            // 
            // lblStocksP
            // 
            this.lblStocksP.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblStocksP.Location = new System.Drawing.Point(125, 126);
            this.lblStocksP.Name = "lblStocksP";
            this.lblStocksP.Size = new System.Drawing.Size(43, 13);
            this.lblStocksP.TabIndex = 7;
            // 
            // lblBondsP
            // 
            this.lblBondsP.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblBondsP.Location = new System.Drawing.Point(125, 156);
            this.lblBondsP.Name = "lblBondsP";
            this.lblBondsP.Size = new System.Drawing.Size(43, 13);
            this.lblBondsP.TabIndex = 8;
            // 
            // lblSpecP
            // 
            this.lblSpecP.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSpecP.Location = new System.Drawing.Point(125, 188);
            this.lblSpecP.Name = "lblSpecP";
            this.lblSpecP.Size = new System.Drawing.Size(43, 13);
            this.lblSpecP.TabIndex = 10;
            // 
            // lblCashP
            // 
            this.lblCashP.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCashP.Location = new System.Drawing.Point(125, 97);
            this.lblCashP.Name = "lblCashP";
            this.lblCashP.Size = new System.Drawing.Size(43, 13);
            this.lblCashP.TabIndex = 9;
            // 
            // lblRiskT
            // 
            this.lblRiskT.AutoSize = true;
            this.lblRiskT.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRiskT.Location = new System.Drawing.Point(52, 12);
            this.lblRiskT.Name = "lblRiskT";
            this.lblRiskT.Size = new System.Drawing.Size(123, 23);
            this.lblRiskT.TabIndex = 11;
            this.lblRiskT.Text = "Risk Tolerance";
            this.lblRiskT.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(221, 212);
            this.Controls.Add(this.lblRiskT);
            this.Controls.Add(this.lblSpecP);
            this.Controls.Add(this.lblCashP);
            this.Controls.Add(this.lblBondsP);
            this.Controls.Add(this.lblStocksP);
            this.Controls.Add(this.lblAllocation);
            this.Controls.Add(this.lblAsset);
            this.Controls.Add(this.lblSpeculative);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.lblBonds);
            this.Controls.Add(this.lblStocks);
            this.Controls.Add(this.lblCash);
            this.Name = "Form1";
            this.Text = "Risk";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblCash;
        private System.Windows.Forms.Label lblStocks;
        private System.Windows.Forms.Label lblBonds;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label lblSpeculative;
        private System.Windows.Forms.Label lblAsset;
        private System.Windows.Forms.Label lblAllocation;
        private System.Windows.Forms.Label lblStocksP;
        private System.Windows.Forms.Label lblBondsP;
        private System.Windows.Forms.Label lblSpecP;
        private System.Windows.Forms.Label lblCashP;
        private System.Windows.Forms.Label lblRiskT;
    }
}

