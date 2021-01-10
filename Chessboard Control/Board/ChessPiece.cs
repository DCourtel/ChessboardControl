using System;

namespace ChessboardControl
{
    public class ChessPiece:IEquatable<ChessPiece>
    {
        public ChessPiece(ChessPieceKind kind, ChessColor color)
        {
            this.Kind = kind;
            this.Color = color;
        }

        public ChessPieceKind Kind { get; set; }

        public ChessColor Color { get; set; }

        public bool Equals(ChessPiece other)
        {
            if(other == null) { return false; }

            return this.Kind == other.Kind && this.Color == other.Color;
        }
    }
}
