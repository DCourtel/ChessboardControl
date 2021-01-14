using ChessboardControl;
using System;
using System.Drawing;
using System.Windows.Forms;


namespace ChessboardTester
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            chessboard1.SetupInitialPosition();
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            chessboard1.ClearBoard();
        }

        private void BtnSetup_Click(object sender, EventArgs e)
        {
            chessboard1.SetupInitialPosition();
        }

        private void chessboard1_OnSquareUnselected(object sender, EventArgs e)
        {
            LblSelectedSquare.Text = $"Selected square:";
            LblSelectedPiece.Text = $"Selected piece:";
        }

        private void chessboard1_OnSquareSelected(Chessboard sender, ChessSquare origin, ChessPieceKind selectedPiece)
        {
            LblSelectedSquare.Text = $"Selected square: {origin.File}{origin.Rank}";
            LblSelectedPiece.Text = $"Selected piece: {selectedPiece}";
        }

        private void chessboard1_OnPieceMoved(Chessboard sender, ChessSquare from, ChessSquare to, ChessPieceKind movedPiece, ChessPieceKind capturedPiece)
        {
            LblLastMove.Text = $"Last move: {movedPiece} from {from} to {to} {(capturedPiece == ChessPieceKind.None ? "No capture" : $"{capturedPiece} was captured")}";
        }

        private void chessboard1_OnPieceRemoved(Chessboard sender, ChessSquare from, ChessPieceKind removedPiece, Point dropPoint)
        {
            LblRemovedPiece.Text = $"Removed piece: {removedPiece} from {from} to {dropPoint.X},{dropPoint.Y}";
        }

        private void BtnMovePiece_Click(object sender, EventArgs e)
        {
            try
            {
                var from = new ChessSquare((ChessFile)CmbBxMoveFromLetter.SelectedIndex, (ChessRank)CmbBxMoveFromDigit.SelectedIndex);
                var to = new ChessSquare((ChessFile)CmbBxMoveToLetter.SelectedIndex, (ChessRank)CmbBxMoveToDigit.SelectedIndex);
                chessboard1.MovePiece(from, to);
                LblLastMove.Text = $"Last move: {from} to {to}";
                //MessageBox.Show($"Captured piece: {capturedPiece}");
            }
            catch (InvalidCoordinatesException invalidCoordinates)
            {
                MessageBox.Show($"{invalidCoordinates.Message}: '{invalidCoordinates.ParameterName}'");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ChkBxFlipBoard_CheckedChanged(object sender, EventArgs e)
        {
            chessboard1.FlipBoard();
        }

        private void BtnSaveToFile_Click(object sender, EventArgs e)
        {
            var saveDlg = new SaveFileDialog();
            saveDlg.Filter = "PNG files|*.png|All files|*.*";
            
            if(saveDlg.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    chessboard1.SaveAsImage(saveDlg.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void ChkBxShowVisualHints_CheckedChanged(object sender, EventArgs e)
        {
            chessboard1.ShowVisualHints = ChkBxShowVisualHints.Checked;
        }
    }
}
