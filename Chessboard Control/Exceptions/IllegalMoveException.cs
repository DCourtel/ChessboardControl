using System;

namespace ChessboardControl
{
    public class IllegalMoveException : Exception
    {
        public IllegalMoveException(ChessSquare from, ChessSquare to)
        {
            Message = $"Unable to move from {from.AlgebraicNotation} to {to.AlgebraicNotation}.";
        }

        public IllegalMoveException(ChessMove moveValidation)
        {
            Message = $"Unable to move from {moveValidation.From.AlgebraicNotation} to {moveValidation.To.AlgebraicNotation}.";
            ValidationResult = moveValidation;
        }

        public override string Message { get; }

        public ChessMove ValidationResult { get; private set; }
    }
}
