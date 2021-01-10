using System;
using System.Text.RegularExpressions;

namespace ChessboardControl
{
    public class FEN
    {
        public FEN(string fen)
        {
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
