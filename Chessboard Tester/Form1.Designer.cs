
namespace ChessboardTester
{
    partial class Form1
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.BtnClear = new System.Windows.Forms.Button();
            this.BtnSetup = new System.Windows.Forms.Button();
            this.LblSelectedSquare = new System.Windows.Forms.Label();
            this.LblSelectedPiece = new System.Windows.Forms.Label();
            this.LblLastMove = new System.Windows.Forms.Label();
            this.LblRemovedPiece = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.CmbBxMoveFromLetter = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.CmbBxMoveToLetter = new System.Windows.Forms.ComboBox();
            this.CmbBxMoveFromDigit = new System.Windows.Forms.ComboBox();
            this.CmbBxMoveToDigit = new System.Windows.Forms.ComboBox();
            this.BtnMovePiece = new System.Windows.Forms.Button();
            this.ChkBxFlipBoard = new System.Windows.Forms.CheckBox();
            this.BtnSaveToFile = new System.Windows.Forms.Button();
            this.ChkBxShowVisualHints = new System.Windows.Forms.CheckBox();
            this.chessboard1 = new ChessboardControl.Chessboard();
            this.SuspendLayout();
            // 
            // BtnClear
            // 
            this.BtnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnClear.Location = new System.Drawing.Point(358, 12);
            this.BtnClear.Name = "BtnClear";
            this.BtnClear.Size = new System.Drawing.Size(75, 23);
            this.BtnClear.TabIndex = 1;
            this.BtnClear.Text = "Clear";
            this.BtnClear.UseVisualStyleBackColor = true;
            this.BtnClear.Click += new System.EventHandler(this.BtnClear_Click);
            // 
            // BtnSetup
            // 
            this.BtnSetup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnSetup.Location = new System.Drawing.Point(358, 41);
            this.BtnSetup.Name = "BtnSetup";
            this.BtnSetup.Size = new System.Drawing.Size(75, 23);
            this.BtnSetup.TabIndex = 2;
            this.BtnSetup.Text = "Setup";
            this.BtnSetup.UseVisualStyleBackColor = true;
            this.BtnSetup.Click += new System.EventHandler(this.BtnSetup_Click);
            // 
            // LblSelectedSquare
            // 
            this.LblSelectedSquare.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.LblSelectedSquare.AutoSize = true;
            this.LblSelectedSquare.Location = new System.Drawing.Point(12, 360);
            this.LblSelectedSquare.Name = "LblSelectedSquare";
            this.LblSelectedSquare.Size = new System.Drawing.Size(89, 13);
            this.LblSelectedSquare.TabIndex = 10;
            this.LblSelectedSquare.Text = "Selected Square:";
            // 
            // LblSelectedPiece
            // 
            this.LblSelectedPiece.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.LblSelectedPiece.AutoSize = true;
            this.LblSelectedPiece.Location = new System.Drawing.Point(12, 383);
            this.LblSelectedPiece.Name = "LblSelectedPiece";
            this.LblSelectedPiece.Size = new System.Drawing.Size(81, 13);
            this.LblSelectedPiece.TabIndex = 11;
            this.LblSelectedPiece.Text = "Selected piece:";
            // 
            // LblLastMove
            // 
            this.LblLastMove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.LblLastMove.AutoSize = true;
            this.LblLastMove.Location = new System.Drawing.Point(12, 402);
            this.LblLastMove.Name = "LblLastMove";
            this.LblLastMove.Size = new System.Drawing.Size(59, 13);
            this.LblLastMove.TabIndex = 12;
            this.LblLastMove.Text = "Last move:";
            // 
            // LblRemovedPiece
            // 
            this.LblRemovedPiece.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.LblRemovedPiece.AutoSize = true;
            this.LblRemovedPiece.Location = new System.Drawing.Point(12, 420);
            this.LblRemovedPiece.Name = "LblRemovedPiece";
            this.LblRemovedPiece.Size = new System.Drawing.Size(86, 13);
            this.LblRemovedPiece.TabIndex = 13;
            this.LblRemovedPiece.Text = "Removed Piece:";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(359, 103);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Move from:";
            // 
            // CmbBxMoveFromLetter
            // 
            this.CmbBxMoveFromLetter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CmbBxMoveFromLetter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbBxMoveFromLetter.FormattingEnabled = true;
            this.CmbBxMoveFromLetter.Items.AddRange(new object[] {
            "a",
            "b",
            "c",
            "d",
            "e",
            "f",
            "g",
            "h"});
            this.CmbBxMoveFromLetter.Location = new System.Drawing.Point(362, 120);
            this.CmbBxMoveFromLetter.Name = "CmbBxMoveFromLetter";
            this.CmbBxMoveFromLetter.Size = new System.Drawing.Size(71, 21);
            this.CmbBxMoveFromLetter.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(359, 192);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Move to:";
            // 
            // CmbBxMoveToLetter
            // 
            this.CmbBxMoveToLetter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CmbBxMoveToLetter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbBxMoveToLetter.FormattingEnabled = true;
            this.CmbBxMoveToLetter.Items.AddRange(new object[] {
            "a",
            "b",
            "c",
            "d",
            "e",
            "f",
            "g",
            "h"});
            this.CmbBxMoveToLetter.Location = new System.Drawing.Point(362, 209);
            this.CmbBxMoveToLetter.Name = "CmbBxMoveToLetter";
            this.CmbBxMoveToLetter.Size = new System.Drawing.Size(71, 21);
            this.CmbBxMoveToLetter.TabIndex = 7;
            // 
            // CmbBxMoveFromDigit
            // 
            this.CmbBxMoveFromDigit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CmbBxMoveFromDigit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbBxMoveFromDigit.FormattingEnabled = true;
            this.CmbBxMoveFromDigit.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8"});
            this.CmbBxMoveFromDigit.Location = new System.Drawing.Point(362, 147);
            this.CmbBxMoveFromDigit.Name = "CmbBxMoveFromDigit";
            this.CmbBxMoveFromDigit.Size = new System.Drawing.Size(71, 21);
            this.CmbBxMoveFromDigit.TabIndex = 5;
            // 
            // CmbBxMoveToDigit
            // 
            this.CmbBxMoveToDigit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CmbBxMoveToDigit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbBxMoveToDigit.FormattingEnabled = true;
            this.CmbBxMoveToDigit.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8"});
            this.CmbBxMoveToDigit.Location = new System.Drawing.Point(362, 236);
            this.CmbBxMoveToDigit.Name = "CmbBxMoveToDigit";
            this.CmbBxMoveToDigit.Size = new System.Drawing.Size(71, 21);
            this.CmbBxMoveToDigit.TabIndex = 8;
            // 
            // BtnMovePiece
            // 
            this.BtnMovePiece.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnMovePiece.Location = new System.Drawing.Point(358, 263);
            this.BtnMovePiece.Name = "BtnMovePiece";
            this.BtnMovePiece.Size = new System.Drawing.Size(75, 23);
            this.BtnMovePiece.TabIndex = 9;
            this.BtnMovePiece.Text = "Move";
            this.BtnMovePiece.UseVisualStyleBackColor = true;
            this.BtnMovePiece.Click += new System.EventHandler(this.BtnMovePiece_Click);
            // 
            // ChkBxFlipBoard
            // 
            this.ChkBxFlipBoard.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ChkBxFlipBoard.AutoSize = true;
            this.ChkBxFlipBoard.Location = new System.Drawing.Point(358, 335);
            this.ChkBxFlipBoard.Name = "ChkBxFlipBoard";
            this.ChkBxFlipBoard.Size = new System.Drawing.Size(72, 17);
            this.ChkBxFlipBoard.TabIndex = 14;
            this.ChkBxFlipBoard.Text = "Flip board";
            this.ChkBxFlipBoard.UseVisualStyleBackColor = true;
            this.ChkBxFlipBoard.CheckedChanged += new System.EventHandler(this.ChkBxFlipBoard_CheckedChanged);
            // 
            // BtnSaveToFile
            // 
            this.BtnSaveToFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnSaveToFile.Location = new System.Drawing.Point(358, 360);
            this.BtnSaveToFile.Name = "BtnSaveToFile";
            this.BtnSaveToFile.Size = new System.Drawing.Size(75, 23);
            this.BtnSaveToFile.TabIndex = 15;
            this.BtnSaveToFile.Text = "Save to file…";
            this.BtnSaveToFile.UseVisualStyleBackColor = true;
            this.BtnSaveToFile.Click += new System.EventHandler(this.BtnSaveToFile_Click);
            // 
            // ChkBxShowVisualHints
            // 
            this.ChkBxShowVisualHints.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ChkBxShowVisualHints.AutoSize = true;
            this.ChkBxShowVisualHints.Location = new System.Drawing.Point(358, 312);
            this.ChkBxShowVisualHints.Name = "ChkBxShowVisualHints";
            this.ChkBxShowVisualHints.Size = new System.Drawing.Size(78, 17);
            this.ChkBxShowVisualHints.TabIndex = 17;
            this.ChkBxShowVisualHints.Text = "Show hints";
            this.ChkBxShowVisualHints.UseVisualStyleBackColor = true;
            this.ChkBxShowVisualHints.CheckedChanged += new System.EventHandler(this.ChkBxShowVisualHints_CheckedChanged);
            // 
            // chessboard1
            // 
            this.chessboard1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chessboard1.Location = new System.Drawing.Point(12, 12);
            this.chessboard1.Name = "chessboard1";
            this.chessboard1.ShowVisualHints = false;
            this.chessboard1.Size = new System.Drawing.Size(340, 340);
            this.chessboard1.TabIndex = 18;
            this.chessboard1.Text = "chessboard1";
            this.chessboard1.OnSquareSelected += new ChessboardControl.Chessboard.SelectedSquareEventHandler(this.chessboard1_OnSquareSelected);
            this.chessboard1.OnPieceMoved += new ChessboardControl.Chessboard.PieceMovedEventHandler(this.chessboard1_OnPieceMoved);
            this.chessboard1.OnPieceRemoved += new ChessboardControl.Chessboard.PieceRemovedEventHandler(this.chessboard1_OnPieceRemoved);
            this.chessboard1.OnSquareUnselected += new System.EventHandler(this.chessboard1_OnSquareUnselected);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(442, 462);
            this.Controls.Add(this.chessboard1);
            this.Controls.Add(this.ChkBxShowVisualHints);
            this.Controls.Add(this.BtnSaveToFile);
            this.Controls.Add(this.ChkBxFlipBoard);
            this.Controls.Add(this.BtnMovePiece);
            this.Controls.Add(this.CmbBxMoveToLetter);
            this.Controls.Add(this.CmbBxMoveToDigit);
            this.Controls.Add(this.CmbBxMoveFromDigit);
            this.Controls.Add(this.CmbBxMoveFromLetter);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.LblRemovedPiece);
            this.Controls.Add(this.LblLastMove);
            this.Controls.Add(this.LblSelectedPiece);
            this.Controls.Add(this.LblSelectedSquare);
            this.Controls.Add(this.BtnSetup);
            this.Controls.Add(this.BtnClear);
            this.Name = "Form1";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chessboard Control Tester";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BtnClear;
        private System.Windows.Forms.Button BtnSetup;
        private System.Windows.Forms.Label LblSelectedSquare;
        private System.Windows.Forms.Label LblSelectedPiece;
        private System.Windows.Forms.Label LblLastMove;
        private System.Windows.Forms.Label LblRemovedPiece;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox CmbBxMoveFromLetter;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox CmbBxMoveToLetter;
        private System.Windows.Forms.ComboBox CmbBxMoveFromDigit;
        private System.Windows.Forms.ComboBox CmbBxMoveToDigit;
        private System.Windows.Forms.Button BtnMovePiece;
        private System.Windows.Forms.CheckBox ChkBxFlipBoard;
        private System.Windows.Forms.Button BtnSaveToFile;
        private System.Windows.Forms.CheckBox ChkBxShowVisualHints;
        private ChessboardControl.Chessboard chessboard1;
    }
}

