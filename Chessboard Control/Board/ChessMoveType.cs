using System;

namespace ChessboardControl
{
    [Flags]
    public enum ChessMoveType
    {
        None = 0,
        Normal = 1,
        Capture = 2,
        Big_Pawn = 4,
        EP_Capture = 8,
        Promotion = 16,
        KSide_Castle = 32,
        QSide_Castle = 64
    }
}
