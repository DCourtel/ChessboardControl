namespace ChessboardControl
{
    public class UndoMoveResult
    {
        public ChessColor MovedPieceColor;
        public ChessSquare From;
        public ChessSquare To;
        public ChessPieceKind MovedPiece = ChessPieceKind.None;
    }
}
