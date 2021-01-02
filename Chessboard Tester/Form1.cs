using ChessboardControl;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Chessboard_Tester
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

        private void chessboard1_OnSquareSelected(Chessboard sender, BoardCoordinates origin, ChessPiece selectedPiece)
        {
            LblSelectedSquare.Text = $"Selected square: {origin.Letter}{origin.Digit}";
            LblSelectedPiece.Text = $"Selected piece: {selectedPiece}";
        }

        private void chessboard1_OnPieceMoved(Chessboard sender, BoardCoordinates from, BoardCoordinates to, ChessPiece movedPiece, ChessPiece capturedPiece)
        {
            LblLastMove.Text = $"Last move: {movedPiece} from {from} to {to} {(capturedPiece == ChessPiece.None ? "No capture" : $"{capturedPiece} was captured")}";
        }

        private void chessboard1_OnPieceRemoved(Chessboard sender, BoardCoordinates from, ChessPiece removedPiece, Point dropPoint)
        {
            LblRemovedPiece.Text = $"Removed piece: {removedPiece} from {from} to {dropPoint.X},{dropPoint.Y}";
        }

        private void BtnMovePiece_Click(object sender, EventArgs e)
        {
            try
            {
                var from = new BoardCoordinates((XCoordinate)CmbBxMoveFromLetter.SelectedIndex, (YCoordinate)CmbBxMoveFromDigit.SelectedIndex);
                var to = new BoardCoordinates((XCoordinate)CmbBxMoveToLetter.SelectedIndex, (YCoordinate)CmbBxMoveToDigit.SelectedIndex);
                var capturedPiece = chessboard1.MovePiece(from, to);
                LblLastMove.Text = $"Last move: {from} to {to} {(capturedPiece == ChessPiece.None ? "No capture" : $"{capturedPiece} was captured")}";
                MessageBox.Show($"Captured piece: {capturedPiece}");
            }
            catch (ChessboardControl.Exceptions.InvalidCoordinatesException invalidCoordinates)
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
    }
}
