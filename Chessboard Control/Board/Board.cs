/*
 * Based on the work of dayjur (https://github.com/dayjur/Chess.cs) and Jeff Hlywa (https://github.com/jhlywa/chess.js)
 * Copyright (c) 2017, Jeff Hlywa (jhlywa@gmail.com) Original author https://github.com/jhlywa/chess.js
 * Copyright (c) 2018, Richard Foster (richf2000@outlook.com)
 * All rights reserved.
 *
 * Redistribution and use in source and binary forms, with or without
 * modification, are permitted provided that the following conditions are met:
 *
 * 1. Redistributions of source code must retain the above copyright notice,
 *    this list of conditions and the following disclaimer.
 * 2. Redistributions in binary form must reproduce the above copyright notice,
 *    this list of conditions and the following disclaimer in the documentation
 *    and/or other materials provided with the distribution.
 *
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
 * AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE
 * IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE
 * ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE
 * LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR
 * CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF
 * SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS
 * INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN
 * CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE)
 * ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE
 * POSSIBILITY OF SUCH DAMAGE.
 *
 *----------------------------------------------------------------------------
 * @license
 * Copyright (c) 2017, Jeff Hlywa (jhlywa@gmail.com)
 * Released under the BSD license
 * https://github.com/jhlywa/chess.js/blob/master/LICENSE
 */

using System;
using System.Collections.Generic;
using System.Linq;

namespace ChessboardControl
{
    public class Board
    {
        private const int EMPTY_SQUARE = -1;

        public static readonly string INITIAL_FEN_POSITION = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1";

        private enum PawnMove
        {
            MoveOneSquare = 0,
            MoveTwoSquare = 1,
            CaptureOnLeft = 2,
            CaptureOnRight = 3
        }

        private readonly Dictionary<ChessColor, int[]> PAWN_MOVE_OFFSETS = new Dictionary<ChessColor, int[]>()
        {
            {ChessColor.Black,new int[]{16,32,17,15}},
            {ChessColor.White,new int[]{-16,-32,-17,-15}}
        };

        private readonly Dictionary<ChessPieceKind, int[]> PIECE_OFFSETS = new Dictionary<ChessPieceKind, int[]>()
        {
            {ChessPieceKind.Knight,new int[]{-18, -33, -31, -14,  18, 33, 31,  14}},
            {ChessPieceKind.Bishop,new int[]{-17, -15,  17,  15}},
            {ChessPieceKind.Rook,new int[]{-16,   1,  16,  -1}},
            {ChessPieceKind.Queen,new int[]{-17, -16, -15,   1,  17, 16, 15,  -1}},
            {ChessPieceKind.King,new int[]{-17, -16, -15,   1,  17, 16, 15,  -1}}
        };

        private readonly int[] ATTACKS = new int[]
         {
            20, 0, 0, 0, 0, 0, 0, 24,  0, 0, 0, 0, 0, 0,20, 0,
            0,20, 0, 0, 0, 0, 0, 24,  0, 0, 0, 0, 0,20, 0, 0,
            0, 0,20, 0, 0, 0, 0, 24,  0, 0, 0, 0,20, 0, 0, 0,
            0, 0, 0,20, 0, 0, 0, 24,  0, 0, 0,20, 0, 0, 0, 0,
            0, 0, 0, 0,20, 0, 0, 24,  0, 0,20, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0,20, 2, 24,  2,20, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 2,53, 56, 53, 2, 0, 0, 0, 0, 0, 0,
            24,24,24,24,24,24,56,  0, 56,24,24,24,24,24,24, 0,
            0, 0, 0, 0, 0, 2,53, 56, 53, 2, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0,20, 2, 24,  2,20, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0,20, 0, 0, 24,  0, 0,20, 0, 0, 0, 0, 0,
            0, 0, 0,20, 0, 0, 0, 24,  0, 0, 0,20, 0, 0, 0, 0,
            0, 0,20, 0, 0, 0, 0, 24,  0, 0, 0, 0,20, 0, 0, 0,
            0,20, 0, 0, 0, 0, 0, 24,  0, 0, 0, 0, 0,20, 0, 0,
            20, 0, 0, 0, 0, 0, 0, 24,  0, 0, 0, 0, 0, 0,20
         };

        private readonly int[] RAYS = new int[]
        {
            17,  0,  0,  0,  0,  0,  0, 16,  0,  0,  0,  0,  0,  0, 15, 0,
            0, 17,  0,  0,  0,  0,  0, 16,  0,  0,  0,  0,  0, 15,  0, 0,
            0,  0, 17,  0,  0,  0,  0, 16,  0,  0,  0,  0, 15,  0,  0, 0,
            0,  0,  0, 17,  0,  0,  0, 16,  0,  0,  0, 15,  0,  0,  0, 0,
            0,  0,  0,  0, 17,  0,  0, 16,  0,  0, 15,  0,  0,  0,  0, 0,
            0,  0,  0,  0,  0, 17,  0, 16,  0, 15,  0,  0,  0,  0,  0, 0,
            0,  0,  0,  0,  0,  0, 17, 16, 15,  0,  0,  0,  0,  0,  0, 0,
            1,  1,  1,  1,  1,  1,  1,  0, -1, -1,  -1,-1, -1, -1, -1, 0,
            0,  0,  0,  0,  0,  0,-15,-16,-17,  0,  0,  0,  0,  0,  0, 0,
            0,  0,  0,  0,  0,-15,  0,-16,  0,-17,  0,  0,  0,  0,  0, 0,
            0,  0,  0,  0,-15,  0,  0,-16,  0,  0,-17,  0,  0,  0,  0, 0,
            0,  0,  0,-15,  0,  0,  0,-16,  0,  0,  0,-17,  0,  0,  0, 0,
            0,  0,-15,  0,  0,  0,  0,-16,  0,  0,  0,  0,-17,  0,  0, 0,
            0,-15,  0,  0,  0,  0,  0,-16,  0,  0,  0,  0,  0,-17,  0, 0,
            -15,  0,  0,  0,  0,  0,  0,-16,  0,  0,  0,  0,  0,  0,-17
        };

        private readonly Dictionary<ChessPieceKind, int> SHIFTS = new Dictionary<ChessPieceKind, int>()
        {
            {ChessPieceKind.Pawn,0},
            {ChessPieceKind.Knight,1},
            {ChessPieceKind.Bishop,2},
            {ChessPieceKind.Rook,3},
            {ChessPieceKind.Queen,4},
            {ChessPieceKind.King,5},
        };

        private const int RANK_2 = 6;
        private const int RANK_7 = 1;

        private readonly static Dictionary<string, int> SQUARES = new Dictionary<string, int>()
        {
            {"a8", 0},{"b8", 1},
            {"c8", 2}, {"d8", 3}, {"e8", 4}, {"f8", 5}, {"g8", 6}, {"h8", 7},
            {"a7", 16}, {"b7", 17}, {"c7", 18}, {"d7", 19}, {"e7", 20}, {"f7", 21}, {"g7", 22}, {"h7", 23},
            {"a6", 32}, {"b6", 33}, {"c6", 34}, {"d6", 35}, {"e6", 36}, {"f6", 37}, {"g6", 38}, {"h6", 39},
            {"a5", 48}, {"b5", 49}, {"c5", 50}, {"d5", 51}, {"e5", 52}, {"f5", 53}, {"g5", 54}, {"h5", 55},
            {"a4", 64}, {"b4", 65}, {"c4", 66}, {"d4", 67}, {"e4", 68}, {"f4", 69}, {"g4", 70}, {"h4", 71},
            {"a3", 80}, {"b3", 81}, {"c3", 82}, {"d3", 83}, {"e3", 84}, {"f3", 85}, {"g3", 86}, {"h3", 87},
            {"a2", 96}, {"b2", 97}, {"c2", 98}, {"d2", 99}, {"e2", 100}, {"f2", 101}, {"g2", 102}, {"h2", 103},
            {"a1", 112}, {"b1", 113}, {"c1", 114}, {"d1", 115}, {"e1", 116}, {"f1", 117}, {"g1", 118}, {"h1", 119}
        };

        private Dictionary<ChessColor, Dictionary<string, int>[]> ROOKS = new Dictionary<ChessColor, Dictionary<string, int>[]>()
        {
            { ChessColor.White,new Dictionary<string,int>[]
                {
                    new Dictionary<string,int>{ {"square", SQUARES["a1"]}, {"flag", (int)ChessCastling.QueenSide}},
                    new Dictionary<string,int>{ {"square", SQUARES["h1"]}, {"flag", (int)ChessCastling .KingSide} }
                }
            },
            { ChessColor.Black,new Dictionary<string,int>[]
                {
                    new Dictionary<string,int>{ {"square", SQUARES["a8"]}, {"flag", (int)ChessCastling.QueenSide} },
                    new Dictionary<string,int>{ {"square", SQUARES["h8"]}, {"flag", (int)ChessCastling.KingSide} }
                }
            }
        };

        private ChessPiece[] board = new ChessPiece[128];
        private Dictionary<ChessColor, int> kings;
        private Dictionary<ChessColor, ChessCastling> castling;
        private int ep_square = EMPTY_SQUARE;
        private int half_moves = 0;
        private int move_number = 1;
        private Stack<BoardState> MoveHistory;

        #region Constructors

        /// <summary>
        /// Creates an instance of the class initialized with the initial position.
        /// </summary>
        public Board()
        {
            LoadFEN(INITIAL_FEN_POSITION);
        }

        /// <summary>
        /// Creates an instance of the class initialized with the given position.
        /// </summary>
        /// <param name="fen">A string describing the position of pieces in the Forsyth–Edwards Notation (FEN).</param>
        public Board(string fen)
        {
            LoadFEN(fen);
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets whether the game is draw by the fifty moves rule.
        /// </summary>
        public bool FiftyMoveRule
        {
            get { return half_moves >= 100; }
        }

        /// <summary>
        /// Gets whether the game is over.
        /// </summary>
        public bool GameOver
        {
            get { return FiftyMoveRule || IsCheckmate || IsStalemate || IsDrawByInsufficientMaterial || IsDrawByThreefoldRepetition; }
        }

        /// <summary>
        /// Gets whether the King is attacked.
        /// </summary>
        /// <returns></returns>
        public bool IsCheck
        {
            get { return IsKingAttacked(Turn); }
        }

        /// <summary>
        /// Gets whether the King is checkmate.
        /// </summary>
        public bool IsCheckmate
        {
            get { return IsCheck && GenerateMoves().Count == 0; }
        }

        /// <summary>
        /// Gets whether the game is in draw.
        /// </summary>
        /// <returns></returns>
        public bool IsDraw
        {
            get { return FiftyMoveRule || IsStalemate || IsDrawByInsufficientMaterial || IsDrawByThreefoldRepetition; }
        }

        /// <summary>
        /// Gets whether there is not enough material to win.
        /// </summary>
        /// <returns></returns>
        public bool IsDrawByInsufficientMaterial
        {
            get
            {
                Dictionary<ChessPieceKind, int> pieces = new Dictionary<ChessPieceKind, int>();
                Stack<int> bishops = new Stack<int>();
                int num_pieces = 0;
                int sq_color = 0;

                for (int file = 0; file < 7; file++)
                {
                    for (int rank = 0; rank < 7; rank++)
                    {
                        var squareIndex = 16 * rank + file;
                        sq_color = (sq_color + 1) % 2;

                        ChessPiece piece = board[squareIndex];
                        if (piece != null)
                        {
                            pieces[piece.Kind] = pieces.ContainsKey(piece.Kind) ? pieces[piece.Kind] += 1 : 1;
                            if (piece.Kind == ChessPieceKind.Bishop)
                            {
                                bishops.Push(sq_color);
                            }
                            num_pieces++;
                        }
                    }
                }

                /* k vs. k */
                if (num_pieces == 2) { return true; }

                /* k vs. kn .... or .... k vs. kb */
                else if (num_pieces == 3)
                {
                    if (pieces.ContainsKey(ChessPieceKind.Bishop))
                    {
                        if (pieces[ChessPieceKind.Bishop] == 1) return true;
                    }
                    if (pieces.ContainsKey(ChessPieceKind.Knight))
                    {
                        if (pieces[ChessPieceKind.Knight] == 1) return true;
                    }
                }
                /* kb vs. kb where any number of bishops are all on the same color */
                else if (pieces.ContainsKey(ChessPieceKind.Bishop))
                {
                    if (num_pieces == pieces[ChessPieceKind.Bishop] + 2)
                    {
                        var sum = 0;
                        var len = bishops.Count;
                        for (var i = 0; i < len; i++)
                        {
                            sum += bishops.ElementAt(i);
                        }
                        if (sum == 0 || sum == len) return true;
                    }
                }

                return false;
            }
        }

        /// <summary>
        /// Returns whether the same position has repeated three time.
        /// </summary>
        /// <returns></returns>
        public bool IsDrawByThreefoldRepetition
        {
            get
            {
                //  ToDo: Write unit-tests
                bool repetition = false;
                Stack<ChessMove> moves = new Stack<ChessMove>();
                Dictionary<string, int> positions = new Dictionary<string, int>();

                while (true)
                {
                    ChessMove move = UndoMove();
                    if (move == null) break;
                    moves.Push(move);
                }

                while (true)
                {
                    //  Remove the last two fields in the FEN string, they're not needed when checking for draw by rep
                    FEN fenObj = new FEN(GetFEN());
                    string fen = $"{fenObj.PiecesPosition} {fenObj.Turn} {fenObj.CastlingForWhite}{fenObj.CastlingForBlack} {fenObj.EnPassant}";

                    //  Has the position occurred three or more times?
                    positions[fen] = positions.ContainsKey(fen) ? positions[fen] + 1 : 1;
                    if (positions[fen] >= 3)
                    {
                        repetition = true;
                    }

                    if (moves.Count < 1)
                    {
                        break;
                    }
                    MovePiece(moves.Pop());
                }

                return repetition;
            }
        }

        /// <summary>
        /// Gets whether the King is stalemate.
        /// </summary>
        public bool IsStalemate
        {
            get { return !IsCheck && GenerateMoves().Count == 0; }
        }

        /// <summary>
        /// Gets or sets whose turn it is.
        /// </summary>
        public ChessColor Turn { get; internal set; } = ChessColor.White;

        #endregion Properties

        #region Public Methods

        /// <summary>
        /// Returns an ASCII representation of the board.
        /// </summary>
        /// <returns></returns>
        public string Ascii()
        {
            //  Header
            var ascii = new System.Text.StringBuilder("   +------------------------+\n");

            //  Body
            for (int rank = 0; rank < 8; rank++)
            {
                ascii.Append($" {8 - rank} |");

                for (int file = 0; file < 8; file++)
                {
                    var piece = board[16 * rank + file];
                    ascii.Append(piece == null ? " . " : $" {FEN.ChessPieceToFEN(piece)} ");
                }
                ascii.Append("|\n");
            }

            //  Footer
            ascii.Append("   +------------------------+\n");
            ascii.Append("     a  b  c  d  e  f  g  h\n");

            return ascii.ToString();
        }

        /// <summary>
        /// Resets the board’s state to an empty chessboard.
        /// </summary>
        public void Clear()
        {
            board = new ChessPiece[128];
            kings = new Dictionary<ChessColor, int>() { { ChessColor.White, EMPTY_SQUARE }, { ChessColor.Black, EMPTY_SQUARE } };
            Turn = ChessColor.White;
            castling = new Dictionary<ChessColor, ChessCastling>() { { ChessColor.White, ChessCastling.None }, { ChessColor.Black, ChessCastling.None } };
            ep_square = EMPTY_SQUARE;
            half_moves = 0;
            move_number = 1;
            MoveHistory = new Stack<BoardState>();
        }

        /// <summary>
        /// Gets the castling possibilities for the given side.
        /// </summary>
        /// <param name="side"></param>
        /// <returns></returns>
        public ChessCastling GetCastlingRights(ChessColor side)
        {
            return castling[side];
        }

        /// <summary>
        /// Gets a string describing the position in the Forsyth–Edwards Notation (FEN).
        /// </summary>
        /// <returns></returns>
        public string GetFEN()
        {
            string fen = "";

            //  Ranks
            int emptySquareCount = 0;
            int squareIndex = 0;

            for (int rank = 0; rank < 8; rank++)
            {
                for (int file = 0; file < 8; file++)
                {
                    squareIndex = 16 * rank + file;
                    if (board[squareIndex] == null)
                    {
                        emptySquareCount++;
                    }
                    else
                    {
                        if (emptySquareCount > 0)
                        {
                            fen += emptySquareCount.ToString();
                            emptySquareCount = 0;
                        }
                        fen += FEN.ChessPieceToFEN(board[squareIndex]);
                    }
                }
                if (emptySquareCount > 0)
                {
                    fen += emptySquareCount.ToString();
                }
                if (squareIndex != SQUARES["h1"])
                { fen += "/"; }
                emptySquareCount = 0;
            }

            //  Castling
            string castlingFlags = $"{((castling[ChessColor.White] & ChessCastling.KingSide) != 0 ? "K" : "")}" +
                                    $"{((castling[ChessColor.White] & ChessCastling.QueenSide) != 0 ? "Q" : "")}" +
                                    $"{((castling[ChessColor.Black] & ChessCastling.KingSide) != 0 ? "k" : "")}" +
                                    $"{((castling[ChessColor.Black] & ChessCastling.QueenSide) != 0 ? "q" : "")}";
            if (castlingFlags.Length == 0) { castlingFlags = "-"; }

            //  En-Passant
            string epflags = ep_square == EMPTY_SQUARE ? "-" : ChessSquare.GetAlgebraicNotation(ep_square);

            return ($"{fen} {(Turn == ChessColor.White ? "w" : "b")} {castlingFlags} {epflags} {half_moves} {move_number}");
        }

        /// <summary>
        /// Returns all legal moves for the current position.
        /// </summary>
        /// <returns></returns>
        public List<ChessMove> GetLegalMoves()
        {
            return GenerateMoves();
        }

        /// <summary>
        /// Returns all legal moves for the current position for the given square.
        /// </summary>
        /// <param name="square">Square for which to compute legal moves.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">Thrown when the <paramref name="square"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown when the <paramref name="square"/> is empty.</exception>
        public List<ChessMove> GetLegalMoves(ChessSquare square)
        {
            if (square == null)
            {
                throw new ArgumentNullException(nameof(square));
            }
            if (board[square.x88Notation] == null)
            {
                throw new ArgumentException("There is no piece on the given square.");
            }
            var moves = GenerateMove(square.x88Notation);
            if (square.x88Notation == kings[Turn])
            {
                moves.AddRange(GenerateCastling());
            }

            return moves;
        }

        /// <summary>
        /// Returns an array of all the chess moves played so far.
        /// </summary>
        /// <returns></returns>
        public ChessMove[] GetMoveHistory()
        {
            BoardState[] moves = MoveHistory.ToArray();
            List<ChessMove> result = new List<ChessMove>();

            for (int i = moves.Length - 1; i >= 0; i--)
            {
                result.Add(moves[i].Move.Clone());
            }

            return result.ToArray<ChessMove>();
        }

        /// <summary>
        /// Validates a move against chess rules.
        /// </summary>
        /// <param name="from">The source square.</param>
        /// <param name="to">The targeted square.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="from"/> or <paramref name="to"/> are null.</exception>
        public ChessMove GetMoveValidity(ChessSquare from, ChessSquare to)
        {
            var moveValidationResult = CheckMoveValidity(from, to);
            if (moveValidationResult.IsValid)
            {
                moveValidationResult.ToSAN = MoveToSAN(moveValidationResult);
            }

            return moveValidationResult;
        }

        /// <summary>
        /// Gets the piece on a given square or null if there is no piece on the square.
        /// </summary>
        /// <param name="square"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="square"/> is null.</exception>
        public ChessPiece GetPieceAt(ChessSquare square)
        {
            if (square == null) { throw new ArgumentNullException(nameof(square)); }

            return board[square.x88Notation];
        }

        /// <summary>
        /// Resets the board’s state and setup pieces as given in the FEN.
        /// </summary>
        /// <param name="fen">A string describing the position of pieces in the Forsyth–Edwards Notation (FEN).</param>
        /// <exception cref="ArgumentException">Thrown when <paramref name="fen"/> is invalid.</exception>
        public void LoadFEN(string fen)
        {
            if (!FENValidator.Validate(fen).IsValid)
            {
                throw new ArgumentException("The given FEN string is invalid.");
            }
            var fenObj = new FEN(fen);
            int square = 0;

            Clear();

            for (var rank = 0; rank < fenObj.Ranks.Length; rank++)
            {
                for (int file = 0; file < fenObj.Ranks[rank].Length; file++)
                {
                    char piece = fenObj.Ranks[rank][file];
                    if (char.IsDigit(piece))
                    {
                        square += int.Parse(piece.ToString());
                    }
                    else
                    {
                        ChessColor color = (char.IsUpper(piece)) ? ChessColor.White : ChessColor.Black;
                        PutPiece(new ChessPiece(FEN.FenToChessPiece(piece), color), new ChessSquare(square));
                        square++;
                    }
                }
                square += 8;
            }

            Turn = fenObj.Turn;

            castling[ChessColor.White] = fenObj.CastlingForWhite;
            castling[ChessColor.Black] = fenObj.CastlingForBlack;

            ep_square = (fenObj.EnPassant == "-") ? EMPTY_SQUARE : SQUARES[fenObj.EnPassant];
            half_moves = fenObj.HalfMoveCount;
            move_number = fenObj.MoveCount;
        }

        /// <summary>
        /// Moves a piece from one square to another square.
        /// </summary>
        /// <param name="move">An instance of a validated move. <see cref="CheckMoveValidity(ChessSquare, ChessSquare)(ChessSquare, ChessSquare)"/></param>
        /// <exception cref="IllegalMoveException">Thrown when the requested move is illegal.</exception>
        public void Move(ChessMove move)
        {
            if (!move.IsValid)
            {
                throw new IllegalMoveException(move);
            }
            MovePiece(move);
        }

        /// <summary>
        /// Moves a piece from one square to another square.
        /// </summary>
        /// <param name="from">Origin square.</param>
        /// <param name="to">Destination square.</param>
        /// <param name="promotedTo">Type of piece used in case of a pawn promotion.</param>
        /// <exception cref="IllegalMoveException">Thrown when the requested move is illegal.</exception>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="from"/> or <paramref name="to"/> are null.</exception>
        public void Move(ChessSquare from, ChessSquare to, ChessPieceKind promotedTo = ChessPieceKind.Queen)
        {
            var moveValidation = CheckMoveValidity(from, to);
            if (moveValidation.MoveKind.HasFlag(ChessMoveType.Promotion)) { moveValidation.PromotedTo = promotedTo; }

            Move(moveValidation);
        }

        /// <summary>
        /// Converts a full <see cref="ChessMove"/> to its Standard Algebraic Notation (SAN).
        /// </summary>
        /// <param name="move">A valid <see cref="ChessMove"/> to convert to Standard Algebraic Notation (SAN).</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">Thrown when <paramref name="move"/> is not a valid move.</exception>
        public string MoveToSAN(ChessMove move)
        {
            if (!move.IsValid) { throw new ArgumentException("The move is not valid."); }
            var kingStatus = string.Empty;

            //  Check and Checkmate
            if (IsCheck)
            {
                kingStatus = "+";
                if (IsCheckmate) { kingStatus = "#"; }
            }

            //  Pawn
            if (move.MovingPiece.Kind == ChessPieceKind.Pawn)
            {
                var promotion = "";
                //  Promotion without capture
                if (move.MoveKind.HasFlag(ChessMoveType.Promotion)) { promotion = $"={FEN.ChessPieceKindToFEN(move.PromotedTo).ToUpper()}"; }
                if (move.MoveKind == ChessMoveType.Promotion || move.MoveKind == ChessMoveType.Normal) { return $"{move.To.AlgebraicNotation}{promotion}{kingStatus}"; }
                //  Move without capturing
                if (move.MoveKind == ChessMoveType.Normal || move.MoveKind == ChessMoveType.Big_Pawn) { return $"{move.To.AlgebraicNotation}{kingStatus}"; }
                //  Capture
                if (move.MoveKind.HasFlag(ChessMoveType.EP_Capture)) { return $"{move.From.File}x{move.To.AlgebraicNotation} e.p.{kingStatus}"; }
                return $"{move.From.File}x{move.To.AlgebraicNotation}{promotion}{kingStatus}";
            }

            //  King
            if (move.MovingPiece.Kind == ChessPieceKind.King)
            {
                //  Roque
                if (move.MoveKind == ChessMoveType.KSide_Castle) { return "O-O"; }
                if (move.MoveKind == ChessMoveType.QSide_Castle) { return "O-O-O"; }
                //  Move without capturing
                if (move.MoveKind == ChessMoveType.Normal) { return $"K{move.To.AlgebraicNotation}"; }
                //  Capture
                return $"Kx{move.To.AlgebraicNotation}{kingStatus}";
            }

            //  Sliding pieces
            var pieceAbrev = FEN.ChessPieceToFEN(move.MovingPiece).ToUpper();
            var disambiguator = GetDisambiguator(move);

            return $"{pieceAbrev}{disambiguator}{((move.MoveKind & ChessMoveType.Capture) == ChessMoveType.Capture ? "x" : "")}{move.To.AlgebraicNotation}{kingStatus}";
        }

        /// <summary>
        /// Puts a piece on the board.
        /// </summary>
        /// <param name="piece"></param>
        /// <param name="square"></param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="piece"/> or <paramref name="square"/> are null.</exception>
        /// <exception cref="ArgumentException">Thrown whene <paramref name="square"/> is not valid or when you try to put two king of the same color.</exception>
        public void PutPiece(ChessPiece piece, ChessSquare square)
        {
            //  Check for valid Piece
            if (piece == null)
            {
                throw new ArgumentNullException(nameof(piece));
            }
            //  Check for valid square
            if (square == null)
            {
                throw new ArgumentNullException(nameof(square));
            }

            int sq = square.x88Notation;

            //  Don't let the user place more than one king
            if (piece.Kind == ChessPieceKind.King && !(kings[piece.Color] == EMPTY_SQUARE || kings[piece.Color] == sq))
            {
                throw new ArgumentException("You cannot put two Kings of the same color.");
            }

            board[sq] = piece;
            if (piece.Kind == ChessPieceKind.King)
            {
                kings[piece.Color] = sq;
            }
        }

        /// <summary>
        /// Resets the board’s state and load the initial position.
        /// </summary>
        public void Reset()
        {
            LoadFEN(INITIAL_FEN_POSITION);
        }

        /// <summary>
        /// Converts a Standard Algebraic Notation (SAN) formatted move into a <see cref="ChessMove"/> object.
        /// </summary>
        /// <param name="sanMove">A move expressed in the SAN format like: Nf3 or Rdxb2</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="sanMove"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="sanMove"/> cannot be translated into a valid move.</exception>
        public ChessMove SANToMove(string sanMove)
        {
            if (sanMove == null) { throw new ArgumentNullException(); }
            if (sanMove.Length == 0) { throw new ArgumentException(); }

            var cleanedSanMove = CleanSANMove(sanMove);

            var allLegalMoves = GetLegalMoves();
            foreach (ChessMove move in allLegalMoves)
            {
                if (move.MoveKind.HasFlag(ChessMoveType.Promotion))
                {
                    if (cleanedSanMove.Contains("=") && cleanedSanMove.IndexOf("=") == cleanedSanMove.Length - 2)
                    {
                        try
                        {
                            move.PromotedTo = FEN.FenToChessPiece(cleanedSanMove[cleanedSanMove.Length - 1]);
                        }
                        catch (Exception) { }
                    }
                }
                move.ToSAN = MoveToSAN(move);
                if (CleanSANMove(move.ToSAN) == cleanedSanMove)
                {
                    return move;
                }
            }

            throw new ArgumentException($"{sanMove} cannot be converted to a valid ChessMove object.");
        }

        /// <summary>
        /// Removes the piece on the given square.
        /// </summary>
        /// <param name="square">Coordinates of the square where to remove the piece.</param>
        /// <returns>An instance of the removed piece or null if there was no piece on the square.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="square"/> is null.</exception>
        public ChessPiece RemovePieceAt(ChessSquare square)
        {
            if (square == null) { throw new ArgumentNullException(nameof(square)); }

            ChessPiece piece = GetPieceAt(square);
            board[square.x88Notation] = null;
            if (piece != null)
            {
                if (piece.Kind == ChessPieceKind.King)
                {
                    kings[piece.Color] = EMPTY_SQUARE;
                }
                else
                    if (piece.Kind == ChessPieceKind.Rook)  //  Update castling rights
                {
                    if (piece.Color == ChessColor.White)
                    {
                        if (square.AlgebraicNotation == "a1")
                        {
                            castling[ChessColor.White] = castling[ChessColor.White] & ChessCastling.KingSide;

                        }
                        if (square.AlgebraicNotation == "h1")
                        {
                            castling[ChessColor.White] = castling[ChessColor.White] & ChessCastling.QueenSide;
                        }
                    }
                    if (piece.Color == ChessColor.Black)
                    {
                        if (square.AlgebraicNotation == "a8")
                        {
                            castling[ChessColor.Black] = castling[ChessColor.Black] & ChessCastling.KingSide;

                        }
                        if (square.AlgebraicNotation == "h8")
                        {
                            castling[ChessColor.Black] = castling[ChessColor.Black] & ChessCastling.QueenSide;
                        }
                    }
                }
            }

            return piece;
        }

        /// <summary>
        /// Undoes the last played move.
        /// </summary>
        /// <returns>An instance of the last move or null if there is no moves in the MoveHistory.</returns>
        public ChessMove UndoMove()
        {
            if (MoveHistory.Count == 0)
            {
                return null;
            }
            BoardState old = MoveHistory.Pop();
            if (old == null) { return null; }

            ChessMove move = old.Move;
            kings = old.Kings;
            Turn = old.Turn;
            castling = old.Castling;
            ep_square = old.EP_Square;
            half_moves = old.Half_MoveCount;
            move_number = old.MoveCount;

            ChessColor us = Turn;
            ChessColor them = SwapColor(Turn);

            board[move.From.x88Notation] = board[move.To.x88Notation];
            board[move.From.x88Notation].Kind = move.MovingPiece.Kind; // to undo any promotions
            board[move.To.x88Notation] = null;

            if (move.MoveKind.HasFlag(ChessMoveType.Capture))
            {
                board[move.To.x88Notation] = new ChessPiece(move.CapturedPiece, them);
            }
            else if ((move.MoveKind & ChessMoveType.EP_Capture) != 0)
            {
                int index;
                if (us == ChessColor.Black)
                {
                    index = move.To.x88Notation - 16;
                }
                else
                {
                    index = move.To.x88Notation + 16;
                }
                board[index] = new ChessPiece(ChessPieceKind.Pawn, them);
            }

            if ((move.MoveKind & (ChessMoveType.KSide_Castle | ChessMoveType.QSide_Castle)) != 0)
            {
                int castling_to = 0;
                int castling_from = 0;
                if ((move.MoveKind & ChessMoveType.KSide_Castle) != 0)
                {
                    castling_to = move.To.x88Notation + 1;
                    castling_from = move.To.x88Notation - 1;

                }
                else if ((move.MoveKind & ChessMoveType.QSide_Castle) != 0)
                {
                    castling_to = move.To.x88Notation - 2;
                    castling_from = move.To.x88Notation + 1;
                }
                board[castling_to] = board[castling_from];
                board[castling_from] = null;
            }

            return move;
        }

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        /// Removes all optional annotations like (+, ++, #, e.p., !!, !, !?, ?!, ?, ??)
        /// </summary>
        /// <param name="sanMove"></param>
        /// <returns></returns>
        private string CleanSANMove(string sanMove)
        {
            var optionalChars = new string[] { "+", "#", "!", "?" };
            foreach (var optionalChar in optionalChars)
            {
                sanMove = sanMove.Replace(optionalChar, string.Empty);
            }
            sanMove = sanMove.Replace("e.p.", string.Empty);

            return sanMove.Replace(" ", string.Empty);
        }

        private string GetDisambiguator(ChessMove move)
        {
            Dictionary<ChessFile, int> matchingFiles = new Dictionary<ChessFile, int>();
            Dictionary<ChessRank, int> matchingRanks = new Dictionary<ChessRank, int>();
            List<ChessSquare> candidateSquares = GetAllAttackingPieces(move.MovingPiece.Kind, Turn, move.To.x88Notation);

            //  Remove illegal moves
            var legalCandidateSquares = new List<ChessSquare>();
            foreach (ChessSquare square in candidateSquares)
            {
                if (CheckMoveValidity(square, move.To).IsValid) { legalCandidateSquares.Add(square); }
            }

            if (legalCandidateSquares.Count < 2) { return string.Empty; }

            foreach (ChessSquare square in legalCandidateSquares)
            {
                if (matchingFiles.ContainsKey(square.File)) { matchingFiles[square.File]++; } else { matchingFiles.Add(square.File, 1); }
                if (matchingRanks.ContainsKey(square.Rank)) { matchingRanks[square.Rank]++; } else { matchingRanks.Add(square.Rank, 1); }
            }
            var cannotUseFile = false;
            var cannotUseRank = false;
            foreach (var fileCount in matchingFiles.Values)
            {
                if (fileCount > 1) { cannotUseFile = true; }
            }
            foreach (var rankCount in matchingRanks.Values)
            {
                if (rankCount > 1) { cannotUseRank = true; }
            }
            if (cannotUseFile && cannotUseRank) { return $"{move.From.File}{1 + (int)move.From.Rank}"; }
            if (cannotUseFile) { return (1 + (int)move.From.Rank).ToString(); }

            return move.From.File.ToString();
        }

        private List<ChessSquare> GetAllAttackingPieces(ChessPieceKind pieceKind, ChessColor color, int to)
        {
            List<ChessSquare> candidates = new List<ChessSquare>();

            for (int file = 0; file < 8; file++)
            {
                for (int rank = 0; rank < 8; rank++)
                {
                    var index = 16 * rank + file;
                    var piece = board[index];
                    if (piece != null && piece.Kind == pieceKind && piece.Color == color && PieceCanAttack(index, to))
                    {
                        candidates.Add(new ChessSquare(index));
                    }
                }
            }

            return candidates;
        }

        private List<ChessMove> GenerateCastling()
        {
            List<ChessMove> moves = new List<ChessMove>();
            ChessColor them = SwapColor(Turn);

            /* king-side castling */
            if ((castling[Turn] & ChessCastling.KingSide) != 0)
            {
                int castling_from = kings[Turn];
                int castling_to = castling_from + 2;
                if (board[castling_from + 1] == null && board[castling_to] == null &&
                !IsSquareAttacked(them, kings[Turn]) &&
                !IsSquareAttacked(them, castling_from + 1) &&
                !IsSquareAttacked(them, castling_to))
                {
                    var move = CheckMoveValidity(new ChessSquare(kings[Turn]), new ChessSquare(castling_to));

                    if (move.IsValid)
                    {
                        moves.Add(move);
                    }
                }
            }

            /* queen-side castling */
            if ((castling[Turn] & ChessCastling.QueenSide) != 0)
            {
                int castling_from = kings[Turn];
                int castling_to = castling_from - 2;
                if (board[castling_from - 1] == null &&
                board[castling_from - 2] == null &&
                board[castling_from - 3] == null &&
                !IsSquareAttacked(them, kings[Turn]) &&
                !IsSquareAttacked(them, castling_from - 1) &&
                !IsSquareAttacked(them, castling_to))
                {
                    var move = CheckMoveValidity(new ChessSquare(kings[Turn]), new ChessSquare(castling_to));

                    if (move.IsValid)
                    {
                        moves.Add(move);
                    }
                }
            }

            return moves;
        }

        private List<ChessMove> GenerateMove(int fromSquareIndex)
        {
            List<ChessMove> moves = new List<ChessMove>();
            ChessColor them = SwapColor(Turn);
            Dictionary<ChessColor, int> second_rank = new Dictionary<ChessColor, int>() { { ChessColor.Black, RANK_7 }, { ChessColor.White, RANK_2 } };
            ChessPiece movingPiece = board[fromSquareIndex];

            if (movingPiece.Kind == ChessPieceKind.Pawn)
            {
                /* single square, non-capturing */
                int toSquareIndex = fromSquareIndex + PAWN_MOVE_OFFSETS[Turn][(int)PawnMove.MoveOneSquare];
                if (board[toSquareIndex] == null)
                {
                    var move = CheckMoveValidity(new ChessSquare(fromSquareIndex), new ChessSquare(toSquareIndex));

                    if (move.IsValid)
                    {
                        moves.Add(move);
                    }
                    /* double square */
                    toSquareIndex = fromSquareIndex + PAWN_MOVE_OFFSETS[Turn][(int)PawnMove.MoveTwoSquare];
                    if (second_rank[Turn] == GetRank(fromSquareIndex) && board[toSquareIndex] == null)
                    {
                        move = CheckMoveValidity(new ChessSquare(fromSquareIndex), new ChessSquare(toSquareIndex));

                        if (move.IsValid)
                        {
                            moves.Add(move);
                        }
                    }
                }

                /* pawn captures */
                for (int j = 2; j < 4; j++)
                {
                    toSquareIndex = fromSquareIndex + PAWN_MOVE_OFFSETS[Turn][j];
                    if ((toSquareIndex & 0x88) != 0) continue;
                    if (board[toSquareIndex] != null && board[toSquareIndex].Color == them)
                    {
                        var move = CheckMoveValidity(new ChessSquare(fromSquareIndex), new ChessSquare(toSquareIndex));

                        if (move.IsValid)
                        {
                            moves.Add(move);
                        }
                    }
                    else if (toSquareIndex == ep_square)
                    {
                        var move = CheckMoveValidity(new ChessSquare(fromSquareIndex), new ChessSquare(toSquareIndex));

                        if (move.IsValid)
                        {
                            moves.Add(move);
                        }
                    }
                }
            }
            else
            {
                for (int j = 0; j < PIECE_OFFSETS[movingPiece.Kind].Length; j++)
                {
                    int offset = PIECE_OFFSETS[movingPiece.Kind][j];
                    int square = fromSquareIndex;
                    while (true)
                    {
                        square += offset;
                        if ((square & 0x88) != 0) break;
                        if (board[square] == null)
                        {
                            var move = CheckMoveValidity(new ChessSquare(fromSquareIndex), new ChessSquare(square));

                            if (move.IsValid)
                            {
                                moves.Add(move);
                            }
                        }
                        else
                        {
                            if (board[square].Color == them)
                            {
                                var move = CheckMoveValidity(new ChessSquare(fromSquareIndex), new ChessSquare(square));

                                if (move.IsValid)
                                {
                                    moves.Add(move);
                                }
                            }
                            break;
                        }

                        /* break, if knight or king */
                        if (movingPiece.Kind == ChessPieceKind.Knight || movingPiece.Kind == ChessPieceKind.King) break;
                    }
                }
            }

            return moves;
        }

        private List<ChessMove> GenerateMoves()
        {
            List<ChessMove> moves = new List<ChessMove>();

            for (int file = 0; file < 8; file++)
            {
                for (int rank = 0; rank < 8; rank++)
                {
                    var fromSquareIndex = 16 * rank + file;

                    ChessPiece movingPiece = board[fromSquareIndex];
                    if (movingPiece == null) continue;
                    if (movingPiece.Color != Turn) continue;
                    moves.AddRange(GenerateMove(fromSquareIndex));
                }
            }

            moves.AddRange(GenerateCastling());

            return moves;
        }

        private ChessMove CheckMoveValidity(ChessSquare from, ChessSquare to)
        {
            //  ToDo: Write unit-tests for other pieces.
            if (from == null) { throw new ArgumentNullException(nameof(from)); }
            if (to == null) { throw new ArgumentNullException(nameof(to)); }
            var validationResult = new ChessMove(from, to);
            var movingPiece = board[from.x88Notation];
            var capturedPiece = board[to.x88Notation];

            //  Check there is a piece on the From square
            if (movingPiece == null)
            {
                validationResult.IllegalReason = ChessMoveRejectedReason.NoPieceOnTheSquare;
                return validationResult;
            }

            validationResult.MovingPiece = board[from.x88Notation];

            //  Check the piece being moved has the right color
            if (movingPiece.Color != Turn)
            {
                validationResult.IllegalReason = ChessMoveRejectedReason.NotYourTurn;
                return validationResult;
            }

            //  Checks if from == to
            if (from == to)
            {
                validationResult.IllegalReason = ChessMoveRejectedReason.DestinationSquareEqualsSourceSquare;
                return validationResult;
            }

            //  Checks the piece being captured is the opposite side
            if (capturedPiece != null && capturedPiece.Color == Turn)
            {
                validationResult.IllegalReason = ChessMoveRejectedReason.CannotCaptureOwnPieces;
                return validationResult;
            }
            var moveIdentified = false;

            switch (movingPiece.Kind)
            {
                case ChessPieceKind.Pawn:
                    //  Moves one square ahead
                    if (to.x88Notation == from.x88Notation + PAWN_MOVE_OFFSETS[Turn][(int)PawnMove.MoveOneSquare])
                    {
                        //  Checks if the destination square is free
                        if (capturedPiece == null)
                        {
                            if (Turn == ChessColor.White && to.Rank == ChessRank._8 ||
                                Turn == ChessColor.Black && to.Rank == ChessRank._1)
                            {
                                validationResult.MoveKind = ChessMoveType.Promotion;
                            }
                            else
                            {
                                validationResult.MoveKind = ChessMoveType.Normal;
                            }
                            moveIdentified = true;
                            break;
                        }
                        else
                        {
                            validationResult.IllegalReason = ChessMoveRejectedReason.NotCapturingLikeThis;
                            break;
                        }
                    }
                    //  Moves two squares ahead
                    if (to.x88Notation == from.x88Notation + PAWN_MOVE_OFFSETS[Turn][(int)PawnMove.MoveTwoSquare] &&
                        ((Turn == ChessColor.White && from.Rank == ChessRank._2) || (Turn == ChessColor.Black && from.Rank == ChessRank._7)))
                    {
                        //  Checks if the destination square is free
                        if (capturedPiece == null)
                        {
                            //  Checks if the pawn is not stepping over another piece
                            if (board[from.x88Notation + PAWN_MOVE_OFFSETS[Turn][(int)PawnMove.MoveOneSquare]] == null)
                            {
                                validationResult.MoveKind = ChessMoveType.Big_Pawn;
                                moveIdentified = true;
                                break;
                            }
                            else
                            {
                                validationResult.IllegalReason = ChessMoveRejectedReason.BlockingPiece;
                                break;
                            }
                        }
                        else
                        {
                            validationResult.IllegalReason = ChessMoveRejectedReason.NotCapturingLikeThis;
                            break;
                        }
                    }
                    //  Captures on left or on right
                    if (to.x88Notation == from.x88Notation + PAWN_MOVE_OFFSETS[Turn][(int)PawnMove.CaptureOnLeft] ||
                        to.x88Notation == from.x88Notation + PAWN_MOVE_OFFSETS[Turn][(int)PawnMove.CaptureOnRight])
                    {
                        if (capturedPiece != null)
                        {
                            if (Turn == ChessColor.White && to.Rank == ChessRank._8 ||
                            Turn == ChessColor.Black && to.Rank == ChessRank._1)
                            {
                                validationResult.MoveKind = ChessMoveType.Capture | ChessMoveType.Promotion;
                            }
                            else
                            {
                                validationResult.MoveKind = ChessMoveType.Capture;
                            }
                            validationResult.CapturedPiece = capturedPiece.Kind;
                            moveIdentified = true;
                        }
                        else
                        {
                            //  Checks if captures en_passant
                            if (to.x88Notation == ep_square)
                            {
                                validationResult.MoveKind = ChessMoveType.EP_Capture;
                                validationResult.CapturedPiece = ChessPieceKind.Pawn;
                                moveIdentified = true;
                            }
                            else
                            {
                                validationResult.IllegalReason = ChessMoveRejectedReason.NoPieceToCapture;
                                break;
                            }
                        }
                    }
                    break;
                case ChessPieceKind.Knight:
                case ChessPieceKind.Bishop:
                case ChessPieceKind.Rook:
                case ChessPieceKind.Queen:
                    if (PieceCanAttack(from.x88Notation, to.x88Notation))
                    {
                        validationResult.MoveKind = capturedPiece != null ? ChessMoveType.Capture : ChessMoveType.Normal;
                        validationResult.CapturedPiece = capturedPiece != null ? capturedPiece.Kind : ChessPieceKind.None;
                        moveIdentified = true;
                    }
                    break;
                case ChessPieceKind.King:
                    //  Normal move or capture
                    if (PieceCanAttack(from.x88Notation, to.x88Notation))
                    {
                        validationResult.MoveKind = capturedPiece != null ? ChessMoveType.Capture : ChessMoveType.Normal;
                        validationResult.CapturedPiece = capturedPiece != null ? capturedPiece.Kind : ChessPieceKind.None;
                        moveIdentified = true;
                    }
                    else
                    {
                        //  Castling
                        if (Turn == ChessColor.White)
                        {
                            if (!IsKingAttacked(ChessColor.White))
                            {
                                if (from.AlgebraicNotation == "e1" && to.AlgebraicNotation == "g1")
                                {
                                    if ((castling[ChessColor.White] & ChessCastling.KingSide) == ChessCastling.KingSide &&
                                    !IsSquareAttacked(ChessColor.Black, from.x88Notation + 1) &&
                                    !IsSquareAttacked(ChessColor.Black, to.x88Notation) &&
                                    board[from.x88Notation + 1] == null &&
                                    board[from.x88Notation + 2] == null)
                                    {
                                        //  White - king side castling
                                        validationResult.MoveKind = ChessMoveType.KSide_Castle;
                                        moveIdentified = true;
                                    }
                                }
                                else if (from.AlgebraicNotation == "e1" && to.AlgebraicNotation == "c1")
                                {
                                    if ((castling[ChessColor.White] & ChessCastling.QueenSide) == ChessCastling.QueenSide &&
                                    !IsSquareAttacked(ChessColor.Black, from.x88Notation - 1) &&
                                    !IsSquareAttacked(ChessColor.Black, to.x88Notation) &&
                                    board[from.x88Notation - 1] == null &&
                                    board[from.x88Notation - 2] == null &&
                                    board[from.x88Notation - 3] == null)
                                    {
                                        //  White - queen side castling
                                        validationResult.MoveKind = ChessMoveType.QSide_Castle;
                                        moveIdentified = true;
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (!IsKingAttacked(ChessColor.Black))
                            {
                                if (from.AlgebraicNotation == "e8" && to.AlgebraicNotation == "g8")
                                {
                                    if ((castling[ChessColor.Black] & ChessCastling.KingSide) == ChessCastling.KingSide &&
                                    !IsSquareAttacked(ChessColor.White, from.x88Notation + 1) &&
                                    !IsSquareAttacked(ChessColor.White, to.x88Notation) &&
                                    board[from.x88Notation + 1] == null &&
                                    board[from.x88Notation + 2] == null)
                                    {
                                        //  Black - king side castling
                                        validationResult.MoveKind = ChessMoveType.KSide_Castle;
                                        moveIdentified = true;
                                    }
                                }
                                else if (from.AlgebraicNotation == "e8" && to.AlgebraicNotation == "c8")
                                {
                                    if ((castling[ChessColor.Black] & ChessCastling.QueenSide) == ChessCastling.QueenSide &&
                                    !IsSquareAttacked(ChessColor.White, from.x88Notation - 1) &&
                                    !IsSquareAttacked(ChessColor.White, to.x88Notation) &&
                                    board[from.x88Notation - 1] == null &&
                                    board[from.x88Notation - 2] == null &&
                                    board[from.x88Notation - 3] == null)
                                    {
                                        //  Black - queen side castling
                                        validationResult.MoveKind = ChessMoveType.QSide_Castle;
                                        moveIdentified = true;
                                    }
                                }
                            }
                        }
                    }
                    break;
            }

            if (moveIdentified)
            {
                //  Checks if making the move will put the King in Check
                MovePiece(validationResult);
                Turn = SwapColor(Turn);
                var isKingAttached = IsKingAttacked(Turn);
                UndoMove();

                if (isKingAttached)
                {
                    validationResult.IllegalReason = ChessMoveRejectedReason.PutKingInCheck;
                }
                else
                {
                    validationResult.IsValid = true;
                    validationResult.IllegalReason = ChessMoveRejectedReason.None;
                }
            }
            else
            {
                if (validationResult.IllegalReason == ChessMoveRejectedReason.Unspecified)
                { validationResult.IllegalReason = ChessMoveRejectedReason.NotMovingLikeThis; }
            }

            return validationResult;
        }

        private static int GetRank(int i)
        {
            return i >> 4;
        }

        private bool IsKingAttacked(ChessColor kingColor)
        {
            return IsSquareAttacked(SwapColor(kingColor), kings[kingColor]);
        }

        private bool IsSquareAttacked(ChessColor attackerColor, int targetedSquare)
        {
            for (int rank = 0; rank < 8; rank++)
            {
                for (int file = 0; file < 8; file++)
                {
                    int attackerSquareIndex = rank * 16 + file;
                    //  No piece on the square or same side
                    if (board[attackerSquareIndex] == null || board[attackerSquareIndex].Color != attackerColor) continue;

                    if (PieceCanAttack(attackerSquareIndex, targetedSquare)) { return true; }
                }
            }
            return false;
        }

        private void MovePiece(ChessMove move)
        {
            if (move.MoveKind.HasFlag(ChessMoveType.Promotion) && move.PromotedTo == ChessPieceKind.None) { move.PromotedTo = ChessPieceKind.Queen; }
            ChessColor us = Turn;
            ChessColor them = SwapColor(us);
            PushToMoveHistory(move);

            board[move.To.x88Notation] = board[move.From.x88Notation];
            board[move.From.x88Notation] = null;

            /* if ep capture, remove the captured pawn */
            if ((move.MoveKind & ChessMoveType.EP_Capture) != 0)
            {
                if (Turn == ChessColor.Black)
                {
                    board[move.To.x88Notation - 16] = null;
                }
                else
                {
                    board[move.To.x88Notation + 16] = null;
                }
            }

            /* if pawn promotion, replace with new piece */
            if (move.MoveKind.HasFlag(ChessMoveType.Promotion))
            {
                board[move.To.x88Notation] = new ChessPiece(move.PromotedTo, us);
            }

            /* if we moved the king */
            if (board[move.To.x88Notation].Kind == ChessPieceKind.King)
            {
                kings[board[move.To.x88Notation].Color] = move.To.x88Notation;
                /* if we castled, move the rook next to the king */
                if (move.MoveKind.HasFlag(ChessMoveType.KSide_Castle))
                {
                    int castling_to = move.To.x88Notation - 1;
                    int castling_from = move.To.x88Notation + 1;
                    board[castling_to] = board[castling_from];
                    board[castling_from] = null;
                }
                else if (move.MoveKind.HasFlag(ChessMoveType.QSide_Castle))
                {
                    int castling_to = move.To.x88Notation + 1;
                    int castling_from = move.To.x88Notation - 2;
                    board[castling_to] = board[castling_from];
                    board[castling_from] = null;
                }

                /* turn off castling */
                castling[us] = ChessCastling.None;
            }

            /* turn off castling if we move a rook */
            if (castling[us] != ChessCastling.None)
            {
                for (var i = 0; i < ROOKS[us].Length; i++)
                {
                    if (move.From.x88Notation == ROOKS[us][i]["square"] && (((int)castling[us] & ROOKS[us][i]["flag"]) != 0))
                    {
                        castling[us] ^= (ChessCastling)ROOKS[us][i]["flag"];
                        break;
                    }
                }
            }

            /* turn off castling if we capture a rook */
            if (castling[them] != ChessCastling.None)
            {
                for (var i = 0; i < ROOKS[them].Length; i++)
                {
                    if (move.To.x88Notation == ROOKS[them][i]["square"] && ((castling[them] & (ChessCastling)ROOKS[them][i]["flag"]) != 0))
                    {
                        castling[them] ^= (ChessCastling)ROOKS[them][i]["flag"];
                        break;
                    }
                }
            }

            /* if big pawn move, update the en passant square */
            ep_square = EMPTY_SQUARE;
            if (move.MoveKind.HasFlag(ChessMoveType.Big_Pawn))
            {
                var leftPiece = board[move.To.x88Notation - 1];
                var rightPiece = board[move.To.x88Notation + 1];
                if (Turn == ChessColor.Black)
                {
                    if ((leftPiece != null && leftPiece.Kind == ChessPieceKind.Pawn && leftPiece.Color == ChessColor.White) ||
                         (rightPiece != null && rightPiece.Kind == ChessPieceKind.Pawn && rightPiece.Color == ChessColor.White))
                    {
                        ep_square = move.To.x88Notation - 16;
                    }
                }
                else
                {
                    if ((leftPiece != null && leftPiece.Kind == ChessPieceKind.Pawn && leftPiece.Color == ChessColor.Black) ||
                         (rightPiece != null && rightPiece.Kind == ChessPieceKind.Pawn && rightPiece.Color == ChessColor.Black))
                    {
                        ep_square = move.To.x88Notation + 16;
                    }
                }
            }

            /* reset the 50 move counter if a pawn is moved or a piece is captured */
            if (move.MovingPiece.Kind == ChessPieceKind.Pawn)
            {
                half_moves = 0;
            }
            else if ((move.MoveKind & (ChessMoveType.Capture | ChessMoveType.EP_Capture)) != 0)
            {
                half_moves = 0;
            }
            else
            {
                half_moves++;
            }

            if (Turn == ChessColor.Black)
            {
                move_number++;
            }
            Turn = SwapColor(Turn);
        }

        private bool PieceCanAttack(int from, int to)
        {
            ChessPiece attackerPiece = board[from];
            int difference = from - to;
            int index = difference + 119;

            if ((ATTACKS[index] & (1 << SHIFTS[attackerPiece.Kind])) != 0)
            {
                //  Pawns
                if (attackerPiece.Kind == ChessPieceKind.Pawn)
                {
                    if (difference > 0)
                    {
                        if (attackerPiece.Color == ChessColor.White) return true;
                    }
                    else
                    {
                        if (attackerPiece.Color == ChessColor.Black) return true;
                    }
                    return false;
                }

                // Knights and Kings
                if (attackerPiece.Kind == ChessPieceKind.Knight || attackerPiece.Kind == ChessPieceKind.King) return true;

                //  Sliding pieces
                int offset = RAYS[index];
                int j = from + offset;

                bool obstructingPieceFound = false;
                while (j != to)
                {
                    if (board[j] != null)
                    {
                        obstructingPieceFound = true;
                        break;
                    }
                    j += offset;
                }

                if (!obstructingPieceFound) return true;
            }
            return false;
        }

        private void PushToMoveHistory(ChessMove move)
        {
            BoardState historyMove = new BoardState()
            {
                Move = move.Clone(),
                Kings = new Dictionary<ChessColor, int>() { { ChessColor.Black, kings[ChessColor.Black] }, { ChessColor.White, kings[ChessColor.White] } },
                Turn = Turn,
                Castling = new Dictionary<ChessColor, ChessCastling>() { { ChessColor.Black, castling[ChessColor.Black] }, { ChessColor.White, castling[ChessColor.White] } },
                EP_Square = ep_square,
                Half_MoveCount = half_moves,
                MoveCount = move_number
            };
            MoveHistory.Push(historyMove);
        }

        private static ChessColor SwapColor(ChessColor c)
        {
            return c == ChessColor.White ? ChessColor.Black : ChessColor.White;
        }

        #endregion Private Methods
    }
}