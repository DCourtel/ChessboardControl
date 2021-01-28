using System;

namespace ChessboardControl
{
    public class ChessPiece
    {
        public ChessPiece(ChessPieceKind kind, ChessColor color)
        {
            this.Kind = kind;
            this.Color = color;
        }

        public ChessPieceKind Kind { get; set; }

        public ChessColor Color { get; set; }

        public static bool operator== (ChessPiece first, ChessPiece second)
        {
            if (ReferenceEquals(first, second)) { return true; }
            if (first is null && second is null) { return true; }
            if (first is null || second is null) { return false; }

            return first.Kind == second.Kind && first.Color == second.Color;
        }

        public static bool operator!= (ChessPiece first, ChessPiece second)
        {
            return !(first == second);
        }

        public override string ToString()
        {
            return $"{Color} {Kind}";
        }
    }
}
