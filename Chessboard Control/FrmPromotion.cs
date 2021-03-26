using System;
using System.Windows.Forms;

namespace ChessboardControl
{
    public partial class FrmPromotion : Form
    {
        public FrmPromotion(ChessColor turn)
        {
            InitializeComponent();

            switch (turn)
            {
                case ChessColor.Black:
                    PctBxQueen.Image = Properties.Resources.BlackQueen;
                    PctBxRook.Image = Properties.Resources.BlackRook;
                    PctBxBishop.Image = Properties.Resources.BlackBishop;
                    PctBxKnight.Image = Properties.Resources.BlackKnight;
                    break;
                case ChessColor.White:
                    PctBxQueen.Image = Properties.Resources.WhiteQueen;
                    PctBxRook.Image = Properties.Resources.WhiteRook;
                    PctBxBishop.Image = Properties.Resources.WhiteBishop;
                    PctBxKnight.Image = Properties.Resources.WhiteKnight;
                    break;
            }
        }

        /// <summary>
        /// Gets or sets what piece has been chosen.
        /// </summary>
        public ChessPieceKind ChoosePiece { get; private set; } = ChessPieceKind.None;

        private void PctBxQueen_Click(object sender, EventArgs e)
        {
            ChoosePiece = ChessPieceKind.Queen;
            DialogResult = DialogResult.OK;
        }

        private void PctBxRook_Click(object sender, EventArgs e)
        {
            ChoosePiece = ChessPieceKind.Rook;
            DialogResult = DialogResult.OK;
        }

        private void PctBxBishop_Click(object sender, EventArgs e)
        {
            ChoosePiece = ChessPieceKind.Bishop;
            DialogResult = DialogResult.OK;
        }

        private void PctBxKnight_Click(object sender, EventArgs e)
        {
            ChoosePiece = ChessPieceKind.Knight;
            DialogResult = DialogResult.OK;
        }
    }
}
