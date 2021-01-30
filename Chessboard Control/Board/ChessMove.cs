using System;

namespace ChessboardControl
{
    public class ChessMove:IEquatable<ChessMove>
    {
        internal ChessMove(ChessSquare from, ChessSquare to)
        {
            this.From = from;
            this.To = to;
        }

        #region Properties

        /// <summary>
        /// Gets or sets the captuded piece if any.
        /// </summary>
        public ChessPieceKind CapturedPiece { get; internal set; } = ChessPieceKind.None;

        /// <summary>
        /// Gets or sets the square where the piece move from.
        /// </summary>
        public ChessSquare From { get; internal set; }

        /// <summary>
        /// Gets or sets the reason why the move is illegal.
        /// </summary>
        public ChessMoveRejectedReason IllegalReason { get; internal set; } = ChessMoveRejectedReason.Unspecified;

        /// <summary>
        /// Gets or sets whether the move is valid.
        /// </summary>
        public bool IsValid { get; internal set; }

        /// <summary>
        /// Gets or sets the kind of move.
        /// </summary>
        public ChessMoveType MoveKind { get; internal set; } = ChessMoveType.None;

        /// <summary>
        /// Gets or sets the type of the piece being moved.
        /// </summary>
        public ChessPiece MovingPiece { get; internal set; }

        private ChessPieceKind _promotedTo = ChessPieceKind.None;
        /// <summary>
        /// Gets or sets the type of the piece created during a pawn promotion.
        /// </summary>
        public ChessPieceKind PromotedTo
        {
            get { return _promotedTo; }
            set
            {
                switch (value)
                {
                    case ChessPieceKind.None:
                    case ChessPieceKind.Knight:
                    case ChessPieceKind.Bishop:
                    case ChessPieceKind.Rook:
                    case ChessPieceKind.Queen:
                        _promotedTo = value;
                        break;
                    default:
                        throw new System.ArgumentOutOfRangeException("You cannot promote a Pawn to a Pawn or a King.");
                }
            }
        }

        private string _toSAN = null;
        /// <summary>
        /// Gets or sets the move expressed in the Standard Algebraic Notation (SAN).
        /// </summary>
        public string ToSAN
        {
            get
            {
                if (_toSAN == null)
                {
                    _toSAN = GetPartialSAN();
                }
                return _toSAN;
            }

            internal set { _toSAN = value; }
        }

        /// <summary>
        /// Gets or sets the square where the piece move to.
        /// </summary>
        public ChessSquare To { get; internal set; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Creates a clone of this instance.
        /// </summary>
        /// <returns>A deep clone of this instance.</returns>
        public ChessMove Clone()
        {
            ChessMove clone = (ChessMove)this.MemberwiseClone();

            clone.From = this.From.Clone();
            clone.To = this.To.Clone();
            clone.MovingPiece = this.MovingPiece.Clone();

            return clone;
        }

        private string GetPartialSAN()
        {
            if (MovingPiece.Kind == ChessPieceKind.Pawn)
            {
                if (CapturedPiece == ChessPieceKind.None)
                {
                    return To.AlgebraicNotation;
                }
                else
                {
                    if ((MoveKind & ChessMoveType.EP_Capture) == ChessMoveType.EP_Capture)
                    {
                        return $"{From.File}x{To.AlgebraicNotation} e.p.";
                    }
                    return $"{From.File}x{To.AlgebraicNotation}";
                }
            }
            else
            {
                if (MoveKind == ChessMoveType.KSide_Castle) { return "O-O"; }
                if (MoveKind == ChessMoveType.QSide_Castle) { return "O-O-O"; }
                if ((MoveKind & ChessMoveType.Capture) == ChessMoveType.Capture)
                {
                    return $"{FEN.ChessPieceToFEN(MovingPiece).ToUpper()}x{To.AlgebraicNotation}";
                }
                return $"{FEN.ChessPieceToFEN(MovingPiece).ToUpper()}{To.AlgebraicNotation}";
            }
        }

        public static bool operator ==(ChessMove first, ChessMove second)
        {
            if (ReferenceEquals(first, second)) { return true; }
            if (first is null && second is null) { return true; }
            if (first is null || second is null) { return false; }

            return first.From == second.From &&
                    first.To == second.To &&
                    first.CapturedPiece == second.CapturedPiece &&
                    first.PromotedTo == second.PromotedTo;
        }

        public static bool operator !=(ChessMove first, ChessMove second)
        {
            return !(first == second);
        }

        public bool Equals(ChessMove other)
        {
            return Equals((object)other);
        }

        public override bool Equals(object obj)
        {
            if (obj is null) { return false; }
            if (!(obj is ChessMove)) { return false; }
            if (ReferenceEquals(this, obj)) { return true; }

            return this == (ChessMove)obj;
        }

        public override int GetHashCode()
        {
            return $"{From}{To}{CapturedPiece}{IllegalReason}{IsValid}{MoveKind}{MovingPiece}{PromotedTo}".GetHashCode();
        }

        public override string ToString()
        {
            return ToSAN;
        }

        #endregion Methods
    }
}
