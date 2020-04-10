namespace FileTransfer
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
            this.ConnectBtn = new System.Windows.Forms.Button();
            this.ReloadItems = new System.Windows.Forms.Button();
            this.DownloadItems = new System.Windows.Forms.Button();
            this.DeleteItems = new System.Windows.Forms.Button();
            this.ShowItems = new System.Windows.Forms.Button();
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.ShowDir = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ConnectBtn
            // 
            this.ConnectBtn.Location = new System.Drawing.Point(34, 371);
            this.ConnectBtn.Name = "ConnectBtn";
            this.ConnectBtn.Size = new System.Drawing.Size(133, 67);
            this.ConnectBtn.TabIndex = 1;
            this.ConnectBtn.Text = "Connect";
            this.ConnectBtn.UseVisualStyleBackColor = true;
            this.ConnectBtn.Click += new System.EventHandler(this.ConnectBtn_Click);
            // 
            // ReloadItems
            // 
            this.ReloadItems.Location = new System.Drawing.Point(34, 250);
            this.ReloadItems.Name = "ReloadItems";
            this.ReloadItems.Size = new System.Drawing.Size(100, 32);
            this.ReloadItems.TabIndex = 3;
            this.ReloadItems.Text = "Reload Items";
            this.ReloadItems.UseVisualStyleBackColor = true;
            this.ReloadItems.Click += new System.EventHandler(this.ReloadItems_Click);
            // 
            // DownloadItems
            // 
            this.DownloadItems.Location = new System.Drawing.Point(34, 288);
            this.DownloadItems.Name = "DownloadItems";
            this.DownloadItems.Size = new System.Drawing.Size(100, 32);
            this.DownloadItems.TabIndex = 4;
            this.DownloadItems.Text = "Download Item";
            this.DownloadItems.UseVisualStyleBackColor = true;
            this.DownloadItems.Click += new System.EventHandler(this.DownloadItems_Click);
            // 
            // DeleteItems
            // 
            this.DeleteItems.Location = new System.Drawing.Point(34, 326);
            this.DeleteItems.Name = "DeleteItems";
            this.DeleteItems.Size = new System.Drawing.Size(100, 32);
            this.DeleteItems.TabIndex = 5;
            this.DeleteItems.Text = "Delete Item";
            this.DeleteItems.UseVisualStyleBackColor = true;
            // 
            // ShowItems
            // 
            this.ShowItems.Location = new System.Drawing.Point(34, 212);
            this.ShowItems.Name = "ShowItems";
            this.ShowItems.Size = new System.Drawing.Size(100, 32);
            this.ShowItems.TabIndex = 6;
            this.ShowItems.Text = "Show Items";
            this.ShowItems.UseVisualStyleBackColor = true;
            this.ShowItems.Click += new System.EventHandler(this.ShowItems_Click);
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Location = new System.Drawing.Point(34, 12);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(754, 184);
            this.checkedListBox1.TabIndex = 7;
            // 
            // ShowDir
            // 
            this.ShowDir.Location = new System.Drawing.Point(140, 212);
            this.ShowDir.Name = "ShowDir";
            this.ShowDir.Size = new System.Drawing.Size(100, 32);
            this.ShowDir.TabIndex = 8;
            this.ShowDir.Text = "Show Directory";
            this.ShowDir.UseVisualStyleBackColor = true;
            this.ShowDir.Click += new System.EventHandler(this.ShowDir_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.ShowDir);
            this.Controls.Add(this.checkedListBox1);
            this.Controls.Add(this.ShowItems);
            this.Controls.Add(this.DeleteItems);
            this.Controls.Add(this.DownloadItems);
            this.Controls.Add(this.ReloadItems);
            this.Controls.Add(this.ConnectBtn);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button ConnectBtn;
        private System.Windows.Forms.Button ReloadItems;
        private System.Windows.Forms.Button DownloadItems;
        private System.Windows.Forms.Button DeleteItems;
        private System.Windows.Forms.Button ShowItems;
        private System.Windows.Forms.CheckedListBox checkedListBox1;
        private System.Windows.Forms.Button ShowDir;
    }
}

