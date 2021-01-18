namespace ChessboardControl
{
    public class ChessMove
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

        /// <summary>
        /// Creates a clone of this instance.
        /// </summary>
        /// <returns>A deep clone of this instance.</returns>
        public ChessMove Clone()
        {
            ChessMove clone = (ChessMove)this.MemberwiseClone();

            clone.From = this.From;
            clone.To = this.To;
            clone.MovingPiece = this.MovingPiece;

            return clone;
        }

        #endregion Properties

        #region Methods

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

        public override string ToString()
        {
            return ToSAN;
        }

        #endregion Methods
    }
}
