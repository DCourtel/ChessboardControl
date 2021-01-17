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
            LblGameStatus.Text = "Game status:";
        }

        private void BtnSetup_Click(object sender, EventArgs e)
        {
            chessboard1.SetupInitialPosition();
            LblGameStatus.Text = "Game status:";
        }

        private void chessboard1_OnSquareUnselected(object sender, EventArgs e)
        {
            LblSelectedSquare.Text = $"Selected square:";
            LblSelectedPiece.Text = $"Selected piece:";
        }

        private void chessboard1_OnSquareSelected(Chessboard sender, ChessSquare origin, ChessPiece selectedPiece)
        {
            LblSelectedSquare.Text = $"Selected square: {origin.File}{origin.Rank}";
            LblSelectedPiece.Text = $"Selected piece: {selectedPiece.Kind}";
        }

        private void chessboard1_OnPieceMoved(Chessboard sender, ChessMove move)
        {
            LblLastMove.Text = $"Last move: {move.MovingPiece.Kind} from {move.From} to {move.To} {(move.CapturedPiece == ChessPieceKind.None ? "No capture" : $"{move.CapturedPiece} was captured")}";
            LblGameStatus.Text = "Game status:";
        }

        private void chessboard1_OnPieceRemoved(Chessboard sender, ChessSquare from, ChessPiece removedPiece, Point dropPoint)
        {
            LblLastMove.Text = "Last move:";
            LblGameStatus.Text = "Game status:";
            LblRemovedPiece.Text = $"Removed piece: {removedPiece.Kind} from {from} to {dropPoint.X},{dropPoint.Y}";
        }

        private void BtnMovePiece_Click(object sender, EventArgs e)
        {
            try
            {
                var from = new ChessSquare((ChessFile)CmbBxMoveFromLetter.SelectedIndex, (ChessRank)CmbBxMoveFromDigit.SelectedIndex);
                var to = new ChessSquare((ChessFile)CmbBxMoveToLetter.SelectedIndex, (ChessRank)CmbBxMoveToDigit.SelectedIndex);

                chessboard1.MovePiece(chessboard1.CheckMoveValidity(from, to));
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

        private void chessboard1_OnCheck(object sender, EventArgs e)
        {
            LblGameStatus.Text = "Game status: Check";
        }

        private void chessboard1_OnCheckmate(object sender, EventArgs e)
        {
            LblGameStatus.Text = "Game status: Checkmate";
        }

        private void chessboard1_OnDraw(object sender, EventArgs e)
        {
            LblGameStatus.Text = "Game status: Draw";
        }
    }
}
