using System;

namespace ChessboardControl
{
    public class Board
    {
        private ChessPiece[,] pieces = new ChessPiece[8, 8];

        #region Methods

        internal void ClearBoard()
        {
            pieces = new ChessPiece[8, 8];
        }

        internal ChessPiece GetPieceAt(BoardCoordinates squareCoordinates)
        {
            return GetPieceAt((int)squareCoordinates.Letter, (int)squareCoordinates.Digit);
        }

        internal ChessPiece GetPieceAt(int letter, int digit)
        {
            if (letter < 0 || letter > 7) { throw new ArgumentOutOfRangeException(nameof(letter), (object)letter, string.Empty); }
            if (digit < 0 || digit > 7) { throw new ArgumentOutOfRangeException(nameof(digit), (object)digit, string.Empty); }

            return pieces[letter, digit];
        }

        internal static System.Drawing.Bitmap GetPieceImage(ChessPiece piece)
        {
            switch (piece)
            {
                case ChessPiece.BlackPawn:
                    return Properties.Resources.BlackPawn;
                case ChessPiece.BlackRook:
                    return Properties.Resources.BlackRook;
                case ChessPiece.BlackKnight:
                    return Properties.Resources.BlackKnight;
                case ChessPiece.BlackBishop:
                    return Properties.Resources.BlackBishop;
                case ChessPiece.BlackKing:
                    return Properties.Resources.BlackKing;
                case ChessPiece.BlackQueen:
                    return Properties.Resources.BlackQueen;
                case ChessPiece.WhitePawn:
                    return Properties.Resources.WhitePawn;
                case ChessPiece.WhiteRook:
                    return Properties.Resources.WhiteRook;
                case ChessPiece.WhiteKnight:
                    return Properties.Resources.WhiteKnight;
                case ChessPiece.WhiteBishop:
                    return Properties.Resources.WhiteBishop;
                case ChessPiece.WhiteKing:
                    return Properties.Resources.WhiteKing;
                case ChessPiece.WhiteQueen:
                    return Properties.Resources.WhiteQueen;
            }
            throw new ArgumentOutOfRangeException();
        }

        internal ChessPiece MovePiece(BoardCoordinates from, BoardCoordinates to)
        {
            var capturedPiece = GetPieceAt(to);
            SetPieceAt(to, GetPieceAt(from));
            SetPieceAt(from, ChessPiece.None);

            return capturedPiece;
        }

        internal void SetupInitialPosition()
        {
            //  Remove all pieces
            ClearBoard();

            //  Add Black pieces
            SetPieceAt(new BoardCoordinates(XCoordinate.a, YCoordinate._8), ChessPiece.BlackRook);
            SetPieceAt(new BoardCoordinates(XCoordinate.b, YCoordinate._8), ChessPiece.BlackKnight);
            SetPieceAt(new BoardCoordinates(XCoordinate.c, YCoordinate._8), ChessPiece.BlackBishop);
            SetPieceAt(new BoardCoordinates(XCoordinate.d, YCoordinate._8), ChessPiece.BlackQueen);
            SetPieceAt(new BoardCoordinates(XCoordinate.e, YCoordinate._8), ChessPiece.BlackKing);
            SetPieceAt(new BoardCoordinates(XCoordinate.f, YCoordinate._8), ChessPiece.BlackBishop);
            SetPieceAt(new BoardCoordinates(XCoordinate.g, YCoordinate._8), ChessPiece.BlackKnight);
            SetPieceAt(new BoardCoordinates(XCoordinate.h, YCoordinate._8), ChessPiece.BlackRook);
            SetPieceAt(new BoardCoordinates(XCoordinate.a, YCoordinate._7), ChessPiece.BlackPawn);
            SetPieceAt(new BoardCoordinates(XCoordinate.b, YCoordinate._7), ChessPiece.BlackPawn);
            SetPieceAt(new BoardCoordinates(XCoordinate.c, YCoordinate._7), ChessPiece.BlackPawn);
            SetPieceAt(new BoardCoordinates(XCoordinate.d, YCoordinate._7), ChessPiece.BlackPawn);
            SetPieceAt(new BoardCoordinates(XCoordinate.e, YCoordinate._7), ChessPiece.BlackPawn);
            SetPieceAt(new BoardCoordinates(XCoordinate.f, YCoordinate._7), ChessPiece.BlackPawn);
            SetPieceAt(new BoardCoordinates(XCoordinate.g, YCoordinate._7), ChessPiece.BlackPawn);
            SetPieceAt(new BoardCoordinates(XCoordinate.h, YCoordinate._7), ChessPiece.BlackPawn);
                                                                    
            //  Add white pieces
            SetPieceAt(new BoardCoordinates(XCoordinate.a, YCoordinate._1), ChessPiece.WhiteRook);
            SetPieceAt(new BoardCoordinates(XCoordinate.b, YCoordinate._1), ChessPiece.WhiteKnight);
            SetPieceAt(new BoardCoordinates(XCoordinate.c, YCoordinate._1), ChessPiece.WhiteBishop);
            SetPieceAt(new BoardCoordinates(XCoordinate.d, YCoordinate._1), ChessPiece.WhiteQueen);
            SetPieceAt(new BoardCoordinates(XCoordinate.e, YCoordinate._1), ChessPiece.WhiteKing);
            SetPieceAt(new BoardCoordinates(XCoordinate.f, YCoordinate._1), ChessPiece.WhiteBishop);
            SetPieceAt(new BoardCoordinates(XCoordinate.g, YCoordinate._1), ChessPiece.WhiteKnight);
            SetPieceAt(new BoardCoordinates(XCoordinate.h, YCoordinate._1), ChessPiece.WhiteRook);
            SetPieceAt(new BoardCoordinates(XCoordinate.a, YCoordinate._2), ChessPiece.WhitePawn);
            SetPieceAt(new BoardCoordinates(XCoordinate.b, YCoordinate._2), ChessPiece.WhitePawn);
            SetPieceAt(new BoardCoordinates(XCoordinate.c, YCoordinate._2), ChessPiece.WhitePawn);
            SetPieceAt(new BoardCoordinates(XCoordinate.d, YCoordinate._2), ChessPiece.WhitePawn);
            SetPieceAt(new BoardCoordinates(XCoordinate.e, YCoordinate._2), ChessPiece.WhitePawn);
            SetPieceAt(new BoardCoordinates(XCoordinate.f, YCoordinate._2), ChessPiece.WhitePawn);
            SetPieceAt(new BoardCoordinates(XCoordinate.g, YCoordinate._2), ChessPiece.WhitePawn);
            SetPieceAt(new BoardCoordinates(XCoordinate.h, YCoordinate._2), ChessPiece.WhitePawn);
        }

        internal void SetPieceAt(BoardCoordinates squareCoordinate, ChessPiece piece)
        {
            pieces[(int)squareCoordinate.Letter, (int)squareCoordinate.Digit] = piece;
        }

        #endregion Methods
    }
}
