namespace ChessboardControl
{
    public enum BoardDirection
    {
        BlackOnTop,
        WhiteOnTop
    }

    public enum ChessPiece
    {
        None,
        BlackPawn,
        BlackRook,
        BlackKnight,
        BlackBishop,
        BlackKing,
        BlackQueen,
        WhitePawn,
        WhiteRook,
        WhiteKnight,
        WhiteBishop,
        WhiteKing,
        WhiteQueen
    }

    public enum XCoordinate : int
    {
        a,
        b,
        c,
        d,
        e,
        f,
        g,
        h,
        None = int.MaxValue
    }

    public enum YCoordinate : int
    {
        _1,
        _2,
        _3,
        _4,
        _5,
        _6,
        _7,
        _8,
        None = int.MaxValue
    }
}
