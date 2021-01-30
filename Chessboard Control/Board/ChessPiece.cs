using System;
using Newtonsoft.Json;

namespace ChessboardControl
{
    public class ChessPiece:IEquatable<ChessPiece>
    {
        [JsonConstructor]
        public ChessPiece(ChessPieceKind kind, ChessColor color)
        {
            this.Kind = kind;
            this.Color = color;
        }

        #region Properties

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Include)]
        public ChessPieceKind Kind { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Include)]
        public ChessColor Color { get; set; }
        
        #endregion Properties

        #region Methods

        public ChessPiece Clone()
        {
            ChessPiece clone = (ChessPiece)this.MemberwiseClone();

            return clone;
        }

        public static bool operator ==(ChessPiece first, ChessPiece second)
        {
            if (ReferenceEquals(first, second)) { return true; }
            if (first is null && second is null) { return true; }
            if (first is null || second is null) { return false; }

            return first.Kind == second.Kind && first.Color == second.Color;
        }

        public static bool operator !=(ChessPiece first, ChessPiece second)
        {
            return !(first == second);
        }

        public bool Equals(ChessPiece other)
        {
            return Equals((object)other);
        }

        public override bool Equals(object obj)
        {
            if (obj is null) { return false; }
            if (!(obj is ChessPiece)) { return false; }
            if (ReferenceEquals(this, obj)) { return true; }

            return this == (ChessPiece)obj;
        }

        public override int GetHashCode()
        {
            return $"{Kind}{Color}".GetHashCode();
        }

        public override string ToString()
        {
            return $"{Color} {Kind}";
        }

        #endregion Methods
    }
}
