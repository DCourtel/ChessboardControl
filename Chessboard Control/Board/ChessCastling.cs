using System;

namespace ChessboardControl
{
    [Flags]
    public enum ChessCastling
    {
        None = 0,
        KingSide = 1,
        QueenSide = 2
    }
}
