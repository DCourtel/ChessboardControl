namespace ChessboardControl
{
    public enum ChessMoveRejectedReason
    {
        None,
        NoPieceOnTheSquare,
        NoPieceToCapture,
        NotYourTurn,
        PutKingInCheck,
        NotCapturingLikeThis,
        NotMovingLikeThis,
        BlockingPiece,
        CannotCaptureOwnPieces,
        DestinationSquareEqualsSourceSquare,
        Unspecified = int.MaxValue
    }
}
