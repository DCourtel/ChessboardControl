using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ChessboardControl
{
    public static class FENValidator
    {
        private static string chessPieces = "prnbkqPRNBKQ";

        public static FENValidationResult Validate(string fen)
        {
            //  rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1

            if (string.IsNullOrWhiteSpace(fen)) { return new FENValidationResult("The string is null or empty."); }

            fen = fen.Trim();
            string[] tokens = Regex.Split(fen, @"\s+");

            //  Should have 6 tokens
            if (tokens.Length != 6) { return new FENValidationResult("Argument must have 6 parts."); }

            //  First token should be 8 valid ranks
            string[] ranks = tokens[0].Split(new char[] { '/' });
            if (ranks.Length != 8) { return new FENValidationResult("Argument must have 8 rank descriptions."); }
            for (int i = 0; i < 8; i++)
            {
                if (!IsValidRank(ranks[i])) { return new FENValidationResult($"{ranks[i]} is not a valid rank description."); }
            }

            //  9th token should be "w" or "b"
            if (tokens[1].ToLower() != "w" && tokens[1].ToLower() != "b") { return new FENValidationResult($"{tokens[1]} is not a valid turn description."); }

            //  10th token should be a valid castling description
            if (!IsValidCastlingDescription(tokens[2])) { return new FENValidationResult($"{tokens[2]} is not a valid castling description."); }

            //  11th token should "-" or algebraic coordinates
            if (tokens[3] != "-" && !IsAlgebraicCoordinate(tokens[3])) { return new FENValidationResult($"{tokens[3]} is not a valid En-Passant description."); }

            //  12th token should be an integer
            if (!int.TryParse(tokens[4], out int _)) { return new FENValidationResult($"{tokens[4]} is not a valid integer."); }

            //  13th token should be an integer
            if (!int.TryParse(tokens[5], out int _)) { return new FENValidationResult($"{tokens[5]} is not a valid integer."); }

            return new FENValidationResult() { IsValid = true };
        }

        private static bool IsValidRank(string token)
        {
            //  rnbqkbnr
            //  rn1qkb2
            var digitTotal = 0;
            var letterCount = 0;

            for (int i = 0; i < token.Length; i++)
            {
                if (char.IsDigit(token[i]))
                {
                    if (!int.TryParse(token[i].ToString(), out int blankSquareCount)) { return false; }
                    if (blankSquareCount < 1 || blankSquareCount > 8) { return false; }
                    digitTotal += blankSquareCount;
                }
                else
                {
                    if (!chessPieces.Contains(token[i])) { return false; }
                    letterCount++;
                }
            }
            if (digitTotal + letterCount != 8) { return false; }

            return true;
        }

        private static bool IsValidCastlingDescription(string token)
        {
            if (token == "-") { return true; }

            int whiteKingCount = token.Count(c => c == 'K');
            int blackKingCount = token.Count(c => c == 'k');
            int whiteQueenCount = token.Count(c => c == 'Q');
            int blackQueenCount = token.Count(c => c == 'q');

            if(whiteKingCount <2 && 
                blackKingCount <2 && 
                whiteQueenCount <2 && 
                blackQueenCount < 2 &&
                whiteKingCount + blackKingCount + whiteQueenCount + blackQueenCount == token.Length) { return true; }

            return false;
        }

        private static bool IsAlgebraicCoordinate(string token)
        {
            if (token.Length != 2) { return false; }
            if(!"abcdefgh".Contains(token[0]) ) { return false; }
            
            if (!char.IsDigit(token[1])) { return false; }
            int digit = int.Parse(token[1].ToString());
            if (digit != 6 && digit != 3) { return false; }

            return true;
        }
    }
}
