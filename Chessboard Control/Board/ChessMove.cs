﻿namespace ChessboardControl
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
        public ChessPieceKind PromotedTo {
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

        public override string ToString()
        {
            return $"{MovingPiece.Kind} {From}{(CapturedPiece != ChessPieceKind.None ? "x" : " ")}{To} ({(IsValid ? "Legal" : "Illegal")}){(IsValid ? "" : $"({IllegalReason})")}";
        }

        #endregion Methods
    }
}
