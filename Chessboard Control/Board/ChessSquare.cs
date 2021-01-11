using System;

namespace ChessboardControl
{
    public class ChessSquare : IEquatable<ChessSquare>
    {
        /// <summary>
        /// Returns an instance of this class initialized with the coordinates of the square.
        /// </summary>
        /// <param name="file"></param>
        /// <param name="rank"></param>
        public ChessSquare(ChessFile file, ChessRank rank)
        {
            this.File = file;
            this.Rank = rank;
        }

        /// <summary>
        /// Returns an instance of this class initialized with the PGN coordinates of the square.
        /// </summary>
        /// <param name="pgn">Coordinate of the square. Example: e2</param>
        public ChessSquare(string pgn)
        {
            if (pgn == null)
            {
                throw new ArgumentNullException();
            }
            if (pgn.Length != 2)
            {
                throw new ArgumentException($"{pgn} is not properly formatted. (Correct format: e2)");
            }
            var letter = pgn.Substring(0, 1)[0];
            var digit = pgn.Substring(1, 1)[0];

            switch (letter)
            {
                case 'a':
                    this.File = ChessFile.a;
                    break;
                case 'b':
                    this.File = ChessFile.b;
                    break;
                case 'c':
                    this.File = ChessFile.c;
                    break;
                case 'd':
                    this.File = ChessFile.d;
                    break;
                case 'e':
                    this.File = ChessFile.e;
                    break;
                case 'f':
                    this.File = ChessFile.f;
                    break;
                case 'g':
                    this.File = ChessFile.g;
                    break;
                case 'h':
                    this.File = ChessFile.h;
                    break;
                default:
                    throw new ArgumentOutOfRangeException($"{letter} is not in [a…h]");
            }
            if (!char.IsDigit(digit)) { throw new ArgumentOutOfRangeException($"{digit} is not a digit"); }

            switch (digit)
            {
                case '1':
                    this.Rank = ChessRank._1;
                    break;
                case '2':
                    this.Rank = ChessRank._2;
                    break;
                case '3':
                    this.Rank = ChessRank._3;
                    break;
                case '4':
                    this.Rank = ChessRank._4;
                    break;
                case '5':
                    this.Rank = ChessRank._5;
                    break;
                case '6':
                    this.Rank = ChessRank._6;
                    break;
                case '7':
                    this.Rank = ChessRank._7;
                    break;
                case '8':
                    this.Rank = ChessRank._8;
                    break;
                default:
                    throw new ArgumentOutOfRangeException($"{digit} is not in [1…8]");
            }
        }

        internal ChessSquare(int x88)
        {
            this.File = (ChessFile)(x88 & 15);
            this.Rank = (ChessRank)7 - (x88 >> 4);
        }

        #region Properties

        /// <summary>
        /// Gets the coordinate of the square in Algebraic notation (ex: e4)
        /// </summary>
        public string AlgebraicNotation
        {
            get
            {
                return $"{File}{1 + (int)Rank}";
            }
        }

        /// <summary>
        /// Gets the color of the square.
        /// </summary>
        public ChessColor Color
        {
            get
            {
                return ((int)File + (int)Rank) % 2 == 0 ? ChessColor.Black : ChessColor.White;
            }
        }

        public ChessFile File { get; set; }

        public ChessRank Rank { get; set; }

        /// <summary>
        /// Gets the coordinate of the square in 0x88 notation.
        /// </summary>
        public int x88Notation
        {
            get
            {
                return ((7 - (int)Rank) << 4) + (int)File; ;
            }
        }

        #endregion Properties

        internal static string GetAlgebraicNotation(int x88)
        {
            return $"{(ChessFile)(x88 & 15)}{8 - (x88 >> 4)}";
        }

        public override string ToString()
        {
            return AlgebraicNotation;
        }

        public bool Equals(ChessSquare other)
        {
            if (other == null) { return false; }

            return this.File == other.File && this.Rank == other.Rank;
        }
    }
}
