
namespace ChessboardControl
{
    partial class FrmPromotion
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
            this.PctBxQueen = new System.Windows.Forms.PictureBox();
            this.PctBxRook = new System.Windows.Forms.PictureBox();
            this.PctBxBishop = new System.Windows.Forms.PictureBox();
            this.PctBxKnight = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.PctBxQueen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PctBxRook)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PctBxBishop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PctBxKnight)).BeginInit();
            this.SuspendLayout();
            // 
            // PctBxQueen
            // 
            this.PctBxQueen.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PctBxQueen.Location = new System.Drawing.Point(12, 12);
            this.PctBxQueen.Name = "PctBxQueen";
            this.PctBxQueen.Size = new System.Drawing.Size(60, 60);
            this.PctBxQueen.TabIndex = 0;
            this.PctBxQueen.TabStop = false;
            this.PctBxQueen.Click += new System.EventHandler(this.PctBxQueen_Click);
            // 
            // PctBxRook
            // 
            this.PctBxRook.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PctBxRook.Location = new System.Drawing.Point(78, 12);
            this.PctBxRook.Name = "PctBxRook";
            this.PctBxRook.Size = new System.Drawing.Size(60, 60);
            this.PctBxRook.TabIndex = 0;
            this.PctBxRook.TabStop = false;
            this.PctBxRook.Click += new System.EventHandler(this.PctBxRook_Click);
            // 
            // PctBxBishop
            // 
            this.PctBxBishop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PctBxBishop.Location = new System.Drawing.Point(144, 12);
            this.PctBxBishop.Name = "PctBxBishop";
            this.PctBxBishop.Size = new System.Drawing.Size(60, 60);
            this.PctBxBishop.TabIndex = 0;
            this.PctBxBishop.TabStop = false;
            this.PctBxBishop.Click += new System.EventHandler(this.PctBxBishop_Click);
            // 
            // PctBxKnight
            // 
            this.PctBxKnight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PctBxKnight.Location = new System.Drawing.Point(210, 12);
            this.PctBxKnight.Name = "PctBxKnight";
            this.PctBxKnight.Size = new System.Drawing.Size(60, 60);
            this.PctBxKnight.TabIndex = 0;
            this.PctBxKnight.TabStop = false;
            this.PctBxKnight.Click += new System.EventHandler(this.PctBxKnight_Click);
            // 
            // FrmPromotion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkGray;
            this.ClientSize = new System.Drawing.Size(282, 84);
            this.Controls.Add(this.PctBxKnight);
            this.Controls.Add(this.PctBxBishop);
            this.Controls.Add(this.PctBxRook);
            this.Controls.Add(this.PctBxQueen);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FrmPromotion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Promotion";
            ((System.ComponentModel.ISupportInitialize)(this.PctBxQueen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PctBxRook)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PctBxBishop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PctBxKnight)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox PctBxQueen;
        private System.Windows.Forms.PictureBox PctBxRook;
        private System.Windows.Forms.PictureBox PctBxBishop;
        private System.Windows.Forms.PictureBox PctBxKnight;
    }
}