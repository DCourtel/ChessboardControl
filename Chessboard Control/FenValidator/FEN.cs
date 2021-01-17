using System;
using System.Text.RegularExpressions;

namespace ChessboardControl
{
    public class FEN
    {
        /// <summary>
        /// Creates an instance of a FEN position.
        /// </summary>
        /// <param name="fen"></param>
        /// <exception cref="ArgumentException">Thrown when <paramref name="fen"/> is null or empty.</exception>
        public FEN(string fen)
        {
            if (string.IsNullOrWhiteSpace(fen)) { throw new ArgumentException("Argument cannot be null or empty.",nameof(fen)); }
            InitializeFromString(fen);
        }

        #region Properties

        public string[] Ranks { get; } = new string[8];

        public ChessColor Turn { get; set; } = ChessColor.White;

        public ChessCastling CastlingForWhite { get; set; } = ChessCastling.None;

        public ChessCastling CastlingForBlack { get; set; } = ChessCastling.None;

        public string EnPassant { get; set; } = string.Empty;

        public int HalfMoveCount { get; set; }

        public int MoveCount { get; set; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Returns the FEN abbreviation of a piece.
        /// </summary>
        /// <param name="piece">Piece to get the FEN  abbreviation from.</param>
        /// <returns></returns>
        /// <remarks>Returns string.Empty if <paramref name="piece"/> points to ChessPieceKind.None</remarks>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="piece"/> is null.</exception>
        public static string ChessPieceToFEN(ChessPiece piece)
        {
            if (piece == null) { throw new ArgumentNullException(nameof(piece)); }
            var result = "";

            switch (piece.Kind)
            {
                case ChessPieceKind.None:
                    result = "";
                    break;
                case ChessPieceKind.Pawn:
                    result = "p";
                    break;
                case ChessPieceKind.Knight:
                    result = "n";
                    break;
                case ChessPieceKind.Bishop:
                    result = "b";
                    break;
                case ChessPieceKind.Rook:
                    result = "r";
                    break;
                case ChessPieceKind.Queen:
                    result = "q";
                    break;
                case ChessPieceKind.King:
                    result = "k";
                    break;
            }

            return (piece.Color == ChessColor.White ? result.ToUpper() : result.ToLower());
        }

        /// <summary>
        /// Returns the kind of a chess piece corresponding to its FEN abbreviation.
        /// </summary>
        /// <param name="fenPiece">Case insensitive FEN abbreviation. Possible values are: p, n, b, r, q, k.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the char doesn’t match any FEN formatted chess piece kind.</exception>
        public static ChessPieceKind FenToChessPiece(char fenPiece)
        {
            switch (char.ToLower(fenPiece))
            {
                case 'p':
                    return ChessPieceKind.Pawn;
                case 'n':
                    return ChessPieceKind.Knight;
                case 'b':
                    return ChessPieceKind.Bishop;
                case 'r':
                    return ChessPieceKind.Rook;
                case 'q':
                    return ChessPieceKind.Queen;
                case 'k':
                    return ChessPieceKind.King;
            }
            throw new ArgumentOutOfRangeException(nameof(fenPiece), $"Unable to translate {fenPiece} into a ChessPiece.");
        }

        private void InitializeFromString(string fen)
        {
            //  rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1

            fen = fen.Trim();
            var validationResult = FENValidator.Validate(fen);
            if (!validationResult.IsValid) { throw new ArgumentException(validationResult.ErrorMessage); }

            string[] tokens = Regex.Split(fen, @"\s+");

            //  Ranks
            var extractedRanks = tokens[0].Split(new char[] { '/' });
            for (int i = 0; i < extractedRanks.Length; i++)
            {
                Ranks[i] = extractedRanks[i];
            }
            
            //  Turn
            Turn = tokens[1].ToLower() == "w"? ChessColor.White: ChessColor.Black;

            //  Casteling
            if (tokens[2].Contains("K")) { CastlingForWhite |= ChessCastling.KingSide; }
            if (tokens[2].Contains("k")) { CastlingForBlack     |= ChessCastling.KingSide; }
            if (tokens[2].Contains("Q")) { CastlingForWhite |= ChessCastling.QueenSide; }
            if (tokens[2].Contains("q")) { CastlingForBlack |= ChessCastling.QueenSide; }

            //  En-Passant
            EnPassant = tokens[3];

            //  HalfMove count
            HalfMoveCount = int.Parse(tokens[4]);

            //  Move count
            MoveCount = int.Parse(tokens[5]);
        }

        #endregion Methods
    }
}
