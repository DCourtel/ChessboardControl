using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ChessboardControl
{
    public class Board
    {
        private const int EMPTY_SQUARE = -1;

        public static readonly string DEFAULT_FEN_POSITION = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1";

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

        private const int RANK_1 = 7;
        private const int RANK_2 = 6;
        private const int RANK_7 = 1;
        private const int RANK_8 = 0;

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

        private Stack<BoardState> history;

        // ================ Constructor ====================

        /// <summary>
        /// Creates an instance of the class initialized with the default position.
        /// </summary>
        public Board()
        {
            LoadFEN(DEFAULT_FEN_POSITION);
        }

        /// <summary>
        /// Creates an instance of the class initialized with the given position.
        /// </summary>
        /// <param name="fen">A string describing the position of pieces in the Forsyth–Edwards Notation (FEN).</param>
        public Board(string fen)
        {
            LoadFEN(fen);
        }

        #region Properties

        /// <summary>
        /// Gets or sets whose turn it is.
        /// </summary>
        public ChessColor Turn { get; internal set; } = ChessColor.White;

        #endregion Properties

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
            history = new Stack<BoardState>();
        }

        /// <summary>
        /// Gets the castling possibility for the given side.
        /// </summary>
        /// <param name="side"></param>
        /// <returns></returns>
        public ChessCastling GetCastlingRights(ChessColor side)
        {
            return castling[side];
        }

        /// <summary>
        /// Resets the board’s state and load the initial position.
        /// </summary>
        public void Reset()
        {
            LoadFEN(DEFAULT_FEN_POSITION);
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
                throw new ArgumentException();
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
                        PutPiece(new ChessPiece(FenToChessPiece(piece), color), ChessSquare.GetAlgebraicNotation(square));
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
                        fen += ChessPieceToFEN(board[squareIndex].Kind, board[squareIndex].Color);
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
            string castlingFlags =  $"{((castling[ChessColor.White] & ChessCastling.KingSide) != 0 ? "K" : "")}" +
                                    $"{((castling[ChessColor.White] & ChessCastling.QueenSide) != 0 ? "Q" : "")}" +
                                    $"{((castling[ChessColor.Black] & ChessCastling.KingSide) != 0 ? "k" : "")}" +
                                    $"{((castling[ChessColor.Black] & ChessCastling.QueenSide) != 0 ? "q" : "")}";
            if (castlingFlags == "") { castlingFlags = "-"; }

            //  En-Passant
            string epflags = ep_square == EMPTY_SQUARE ? "-" : ChessSquare.GetAlgebraicNotation(ep_square);

            return ($"{fen} {(Turn == ChessColor.White ? "w" : "b")} {castlingFlags} {epflags} {half_moves} {move_number}");
        }

        /// <summary>
        /// Gets the piece on the given square or null if there is no piece on the square.
        /// </summary>
        /// <param name="square"></param>
        /// <returns></returns>
        public ChessPiece GetPieceAt(ChessSquare square)
        {
            return board[square.x88Notation];
        }

        /// <summary>
        /// Puts a piece on the board.
        /// </summary>
        /// <param name="piece"></param>
        /// <param name="square"></param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="piece"/> or <paramref name="square"/> are null.</exception>
        /// <exception cref="ArgumentException">Thrown whene <paramref name="square"/> is not valid or when you try to put two king of the same color.</exception>
        public void PutPiece(ChessPiece piece, string square)
        {
            //  Check for valid Piece
            if(piece == null)
            {
                throw new ArgumentNullException(nameof(piece));
            }
            //  Check for valid square
            if(square == null)
            {
                throw new ArgumentNullException(nameof(square));
            }
            if (!SQUARES.ContainsKey(square))
            {
                throw new ArgumentException($"'{square}' is not valid coordinates.");
            }

            int sq = SQUARES[square];

            /* don't let the user place more than one king */
            if (piece.Kind == ChessPieceKind.King
            && !(kings[piece.Color] == EMPTY_SQUARE || kings[piece.Color] == sq))
            {
                throw new ArgumentException("You cannot put two kings of the same color.");
            }

            board[sq] = piece;
            if (piece.Kind == ChessPieceKind.King)
            {
                kings[piece.Color] = sq;
            }
        }

        /// <summary>
        /// Removes a piece from the board.
        /// </summary>
        /// <param name="square"></param>
        /// <returns>An instance of the removed piece or null if there is no piece on the square.</returns>
        public ChessPiece Remove(ChessSquare square)
        {
            ChessPiece piece = GetPieceAt(square);
            board[square.x88Notation] = null;
            if (piece != null && piece.Kind == ChessPieceKind.King)
            {
                kings[piece.Color] = EMPTY_SQUARE;
            }

            return piece;
        }

        private Move build_move(ChessPiece[] board, int from, int to, ChessMoveType flags, ChessPieceKind promotion = ChessPieceKind.None)
        {
            Move move = new Move()
            {
                color = Turn,
                from = from,
                to = to,
                flags = flags,
                piece = board[from].Kind
            };

            if (promotion != ChessPieceKind.None)
            {
                move.flags |= ChessMoveType.Promotion;
                move.promotion = promotion;
            }

            if (board[to] != null)
            {
                move.captured = board[to].Kind;
            }
            else if ((flags & ChessMoveType.EP_Capture) != 0)
            {
                move.captured = ChessPieceKind.Pawn;
            }

            return move;
        }

        private void add_move(ChessPiece[] board, Stack<Move> moves, int from, int to, ChessMoveType flags)
        {
            if (board[from].Kind == ChessPieceKind.Pawn && (rank(to) == RANK_8 || rank(to) == RANK_1))
            {
                ChessPieceKind[] pieces = { ChessPieceKind.Queen, ChessPieceKind.Rook, ChessPieceKind.Bishop, ChessPieceKind.Knight };
                for (int i = 0; i < pieces.Length; i++)
                {
                    moves.Push(build_move(board, from, to, flags, pieces[i]));
                }
            }
            else
            {
                moves.Push(build_move(board, from, to, flags));
            }

        }

        private Stack<Move> generate_moves(Dictionary<string, object> options = null)
        {
            //    Stack<Move> moves = new Stack<Move>();
            //    ChessColor us = turn;
            //    ChessColor them = swap_color(us);
            //    Dictionary<ChessColor, int> second_rank = new Dictionary<ChessColor, int>() { { ChessColor.Black, RANK_7 }, { ChessColor.White, RANK_2 } };
            //    int first_sq = SQUARES["a8"];
            //    int last_sq = SQUARES["h1"];
            //    bool single_square = false;

            //    bool legal = (options != null && options.ContainsKey("legal")) ? (bool)options["legal"] : true;

            //    if (options != null && options.ContainsKey("square"))
            //    {
            //        string sq = (string)options["square"];
            //        if (SQUARES.ContainsKey(sq))
            //        {
            //            first_sq = last_sq = SQUARES[sq];
            //            single_square = true;
            //        }
            //        else
            //        {
            //            /* invalid square */
            //            return null;
            //        }
            //    }

            //    for (int fromSquareIndex = first_sq; fromSquareIndex <= last_sq; fromSquareIndex++)
            //    {
            //        if ((fromSquareIndex & 0x88) != 0)
            //        {
            //            fromSquareIndex += 7;
            //            continue;
            //        }

            //        ChessPiece movingPiece = board[fromSquareIndex];
            //        if (movingPiece == null) continue;
            //        if (movingPiece.Color != us) continue;

            //        if (movingPiece.Kind == ChessPieceKind.Pawn)
            //        {
            //            /* single square, non-capturing */
            //            int toSquareIndex = fromSquareIndex + PAWN_MOVE_OFFSETS[us][(int)PawnMove.MoveOneSquare];
            //            if (board[toSquareIndex] == null)
            //            {
            //                add_move(board, moves, fromSquareIndex, toSquareIndex, ChessMoveType.Normal);
            //                /* double square */
            //                toSquareIndex = fromSquareIndex + PAWN_MOVE_OFFSETS[us][(int)PawnMove.MoveTwoSquare];
            //                if (second_rank[us] == rank(fromSquareIndex) && board[toSquareIndex] == null)
            //                {
            //                    add_move(board, moves, fromSquareIndex, toSquareIndex, ChessMoveType.Big_Pawn);
            //                }
            //            }

            //            /* pawn captures */
            //            for (int j = 2; j < 4; j++)
            //            {
            //                toSquareIndex = fromSquareIndex + PAWN_MOVE_OFFSETS[us][j];
            //                if ((toSquareIndex & 0x88) != 0) continue;
            //                if (board[toSquareIndex] != null && board[toSquareIndex].Color == them)
            //                {
            //                    add_move(board, moves, fromSquareIndex, toSquareIndex, ChessMoveType.Capture);
            //                }
            //                else if (toSquareIndex == ep_square)
            //                {
            //                    add_move(board, moves, fromSquareIndex, ep_square, ChessMoveType.EP_Capture);
            //                }
            //            }
            //        }
            //        else
            //        {
            //            for (int j = 0; j < PIECE_OFFSETS[movingPiece.Kind].Length; j++)
            //            {
            //                int offset = PIECE_OFFSETS[movingPiece.Kind][j];
            //                int square = fromSquareIndex;
            //                while (true)
            //                {
            //                    square += offset;
            //                    if ((square & 0x88) != 0) break;
            //                    if (board[square] == null)
            //                    {
            //                        add_move(board, moves, fromSquareIndex, square, ChessMoveType.Normal);
            //                    }
            //                    else
            //                    {
            //                        if (board[square].Color == them)
            //                        {
            //                            add_move(board, moves, fromSquareIndex, square, ChessMoveType.Capture);
            //                        }
            //                        break;
            //                    }

            //                    /* break, if knight or king */
            //                    if (movingPiece.Kind == ChessPieceKind.Knight || movingPiece.Kind == ChessPieceKind.King) break;
            //                }
            //            }
            //        }

            //    }

            //    /* check for castling if: a) we're generating all moves, or b) we're doing
            //* single square move generation on the king's square
            //*/

            //    if ((!single_square) || last_sq == kings[us])
            //    {
            //        /* king-side castling */
            //        if ((castling[us] & ChessMoveType.KSide_Castle) != 0)
            //        {
            //            int castling_from = kings[us];
            //            int castling_to = castling_from + 2;
            //            if (board[castling_from + 1] == null && board[castling_to] == null &&
            //            !IsSquareAttacked(them, kings[us]) &&
            //            !IsSquareAttacked(them, castling_from + 1) &&
            //            !IsSquareAttacked(them, castling_to))
            //            {
            //                add_move(board, moves, kings[us], castling_to, ChessMoveType.KSide_Castle);
            //            }
            //        }

            //        /* queen-side castling */
            //        if ((castling[us] & ChessMoveType.QSide_Castle) != 0)
            //        {
            //            int castling_from = kings[us];
            //            int castling_to = castling_from - 2;
            //            if (board[castling_from - 1] == null &&
            //            board[castling_from - 2] == null &&
            //            board[castling_from - 3] == null &&
            //            !IsSquareAttacked(them, kings[us]) &&
            //            !IsSquareAttacked(them, castling_from - 1) &&
            //            !IsSquareAttacked(them, castling_to))
            //            {
            //                add_move(board, moves, kings[us], castling_to, ChessMoveType.QSide_Castle);
            //            }
            //        }
            //    }
            //    /* return all pseudo-legal moves (this includes moves that allow the king
            //       * to be captured)
            //       */
            //    if (!legal)
            //    {
            //        return moves;
            //    }

            //    /* filter out illegal moves */
            //    Stack<Move> legal_moves = new Stack<Move>();
            //    for (var i = 0; i < moves.Count; i++)
            //    {
            //        MovePiece(moves.ElementAt(i));
            //        if (!IsKingAttacked(us))
            //        {
            //            legal_moves.Push(moves.ElementAt(i));
            //        }

            //        undo_move();

            //    }

            //    return legal_moves;
            return null;
        }

        private string move_to_san(Move move, bool sloppy = false)
        {
            //string output = "";
            //if ((move.flags & ChessMoveType.KSide_Castle) != 0)
            //{
            //    output = "O-O";
            //}
            //else if ((move.flags & ChessMoveType.QSide_Castle) != 0)
            //{
            //    output = "O-O-O";
            //}
            //else
            //{
            //    string disambiguator = get_disambiguator(move, sloppy);
            //    if (move.piece != ChessPieceKind.Pawn)
            //    {
            //        output += ChessPieceToFEN(move.piece, ChessColor.White) + disambiguator;
            //    }
            //    if ((move.flags & (ChessMoveType.Capture | ChessMoveType.EP_Capture)) != 0)
            //    {
            //        if (move.piece == ChessPieceKind.Pawn)
            //        {
            //            output += algebraic(move.from).Substring(0, 1);
            //        }
            //        output += "x";
            //    }

            //    output += algebraic(move.to);

            //    if ((move.flags & ChessMoveType.Promotion) != 0)
            //    {
            //        output += "=" + ChessPieceToFEN(move.promotion, ChessColor.White);
            //    }
            //}

            //MovePiece(move);
            //if (in_check())
            //{
            //    if (in_checkmate())
            //    {
            //        output += "#";
            //    }
            //    else
            //    {
            //        output += "+";
            //    }
            //}
            //undo_move();

            //return output;
            return null;
        }

        // parses all of the decorators out of a SAN string
        private string stripped_san(string move)
        {
            string rt = Regex.Replace(move, @"=", "");
            rt = Regex.Replace(rt, @"[+#]?[?!]*$", "");
            return rt;
        }

        private bool IsSquareAttacked(ChessColor attackerColor, int targetedSquare)
        {
            for (int attackerSquareIndex = SQUARES["a8"]; attackerSquareIndex <= SQUARES["h1"]; attackerSquareIndex++)
            {
                /* did we run off the end of the board */
                if ((attackerSquareIndex & 0x88) != 0) { attackerSquareIndex += 7; continue; }
                /* if empty square or wrong color */
                if (board[attackerSquareIndex] == null || board[attackerSquareIndex].Color != attackerColor) continue;

                ChessPiece attackerPiece = board[attackerSquareIndex];
                int difference = attackerSquareIndex - targetedSquare;
                int index = difference + 119;

                if ((ATTACKS[index] & (1 << SHIFTS[attackerPiece.Kind])) != 0)
                {
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
                        continue;
                    }

                    /* if the piece is a knight or a king */
                    if (attackerPiece.Kind == ChessPieceKind.Knight || attackerPiece.Kind == ChessPieceKind.King) return true;

                    int offset = RAYS[index];
                    int j = attackerSquareIndex + offset;

                    bool obstructingPieceFound = false;
                    while (j != targetedSquare)
                    {
                        if (board[j] != null) { obstructingPieceFound = true; break; }
                        j += offset;
                    }

                    if (!obstructingPieceFound) return true;
                }
            }
            return false;
        }

        private bool IsKingAttacked(ChessColor kingColor)
        {
            return IsSquareAttacked(swap_color(kingColor), kings[kingColor]);
        }

        public ChessMove GetMoveValidity(ChessSquare from, ChessSquare to)
        {
            var validationResult = new ChessMove(from, to);
            var movingPiece = board[from.x88Notation];
            var capturedPiece = board[to.x88Notation];

            //  Check there is a piece on the From square
            if (movingPiece == null)
            {
                validationResult.IllegalReason = ChessMoveRejectedReason.NoPieceOnTheSquare;
                return validationResult;
            }

            validationResult.MovingPiece = board[from.x88Notation].Kind;

            //  Check the piece being moving has the right color
            if (movingPiece.Color != Turn)
            {
                validationResult.IllegalReason = ChessMoveRejectedReason.NotYourTurn;
                return validationResult;
            }

            //  Checks if from == to
            if (from.Equals(to))
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
                    //  Moves two square ahead
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
                    if (PieceCanAttack(from, to))
                    {
                        validationResult.MoveKind = capturedPiece != null ? ChessMoveType.Capture : ChessMoveType.Normal;
                        moveIdentified = true;
                    }
                    break;
                case ChessPieceKind.Bishop:
                    if (PieceCanAttack(from, to))
                    {
                        validationResult.MoveKind = capturedPiece != null ? ChessMoveType.Capture : ChessMoveType.Normal;
                        moveIdentified = true;
                    }
                    break;
                case ChessPieceKind.Rook:
                    if (PieceCanAttack(from, to))
                    {
                        validationResult.MoveKind = capturedPiece != null ? ChessMoveType.Capture : ChessMoveType.Normal;
                        moveIdentified = true;
                    }
                    break;
                case ChessPieceKind.Queen:
                    if (PieceCanAttack(from, to))
                    {
                        validationResult.MoveKind = capturedPiece != null ? ChessMoveType.Capture : ChessMoveType.Normal;
                        moveIdentified = true;
                    }
                    break;
                case ChessPieceKind.King:
                    //  Normal move or capture
                    if (PieceCanAttack(from, to))
                    {
                        validationResult.MoveKind = capturedPiece != null ? ChessMoveType.Capture : ChessMoveType.Normal;
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
                MovePiece(validationResult);
                Turn = swap_color(Turn);
                if (IsKingAttacked(Turn))
                {
                    validationResult.IllegalReason = ChessMoveRejectedReason.PutKingInCheck;
                }
                else
                {
                    validationResult.IsValid = true;
                    validationResult.IllegalReason = ChessMoveRejectedReason.None;
                }
                undo_move();
            }
            else
            {
                if (validationResult.IllegalReason == ChessMoveRejectedReason.Unspecified)
                { validationResult.IllegalReason = ChessMoveRejectedReason.NotMovingLikeThis; }
            }


            return validationResult;
        }

        private bool PieceCanAttack(ChessSquare from, ChessSquare to)
        {
            ChessPiece attackerPiece = board[from.x88Notation];
            int difference = from.x88Notation - to.x88Notation;
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
                int j = from.x88Notation + offset;

                bool obstructingPieceFound = false;
                while (j != to.x88Notation)
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

        private bool in_check()
        {
            return IsKingAttacked(Turn);
        }

        private bool in_checkmate()
        {
            return in_check() && generate_moves().Count == 0;
        }

        private bool in_stalemate()
        {
            return !in_check() && generate_moves().Count == 0;
        }

        private bool insufficient_material()
        {
            Dictionary<ChessPieceKind, int> pieces = new Dictionary<ChessPieceKind, int>();
            Stack<int> bishops = new Stack<int>();
            int num_pieces = 0;
            int sq_color = 0;

            for (int i = SQUARES["a8"]; i <= SQUARES["h1"]; i++)
            {
                sq_color = (sq_color + 1) % 2;
                if ((i & 0x88) != 0) { i += 7; continue; }

                ChessPiece piece = board[i];
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

        private bool in_threefold_repetition()
        {
            //bool repetition = false;
            //Stack<Move> moves = new Stack<Move>();
            //Dictionary<string, int> positions = new Dictionary<string, int>();

            //while (true)
            //{
            //    Move move = undo_move();
            //    if (move == null) break;
            //    moves.Push(move);
            //}

            //while (true)
            //{
            //    /* remove the last two fields in the FEN string, they're not needed
            //     * when checking for draw by rep */
            //    string fen = generate_fen();
            //    string[] fenArray = fen.Split(' ');
            //    fen = fenArray[0] + " " + fenArray[1] + " " + fenArray[2] + " " + fenArray[3];

            //    /* has the position occurred three or move times */
            //    positions[fen] = (positions.ContainsKey(fen)) ? positions[fen] + 1 : 1;
            //    if (positions[fen] >= 3)
            //    {
            //        repetition = true;
            //    }

            //    if (moves.Count < 1)
            //    {
            //        break;
            //    }
            //    MovePiece(moves.Pop());
            //}

            //return repetition;
            return true;
        }

        private void push(ChessMove move)
        {
            BoardState historyMove = new BoardState()
            {
                move = move,
                kings = new Dictionary<ChessColor, int>() { { ChessColor.Black, kings[ChessColor.Black] }, { ChessColor.White, kings[ChessColor.White] } },
                turn = Turn,
                castling = new Dictionary<ChessColor, ChessCastling>() { { ChessColor.Black, castling[ChessColor.Black] }, { ChessColor.White, castling[ChessColor.White] } },
                ep_square = ep_square,
                half_moves = half_moves,
                move_number = move_number
            };
            history.Push(historyMove);
        }

        private void MovePiece(ChessMove move)
        {
            ChessColor us = Turn;
            ChessColor them = swap_color(us);
            push(move);

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
            if ((move.MoveKind & ChessMoveType.Promotion) != 0)
            {
                board[move.To.x88Notation] = new ChessPiece(move.PromotedTo, us);
            }

            /* if we moved the king */
            if (board[move.To.x88Notation].Kind == ChessPieceKind.King)
            {
                kings[board[move.To.x88Notation].Color] = move.To.x88Notation;
                /* if we castled, move the rook next to the king */
                if ((move.MoveKind & ChessMoveType.KSide_Castle) != 0)
                {
                    int castling_to = move.To.x88Notation - 1;
                    int castling_from = move.To.x88Notation + 1;
                    board[castling_to] = board[castling_from];
                    board[castling_from] = null;
                }
                else if ((move.MoveKind & ChessMoveType.QSide_Castle) != 0)
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
            if ((move.MoveKind & ChessMoveType.Big_Pawn) != 0)
            {
                if (Turn == ChessColor.Black)
                {
                    ep_square = move.To.x88Notation - 16;
                }
                else
                {
                    ep_square = move.To.x88Notation + 16;
                }
            }
            else
            {
                ep_square = EMPTY_SQUARE;
            }

            /* reset the 50 move counter if a pawn is moved or a piece is captured */
            if (move.MovingPiece == ChessPieceKind.Pawn)
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
            Turn = swap_color(Turn);
        }

        private ChessMove undo_move()
        {
            if (history.Count == 0)
            {
                return null;
            }
            BoardState old = history.Pop();
            if (old == null) { return null; }

            ChessMove move = old.move;
            kings = old.kings;
            Turn = old.turn;
            castling = old.castling;
            ep_square = old.ep_square;
            half_moves = old.half_moves;
            move_number = old.move_number;

            ChessColor us = Turn;
            ChessColor them = swap_color(Turn);

            board[move.From.x88Notation] = board[move.To.x88Notation];
            board[move.From.x88Notation].Kind = move.MovingPiece; // to undo any promotions
            board[move.To.x88Notation] = null;

            if ((move.MoveKind & ChessMoveType.Capture) != 0)
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

        /* this function is used to uniquely identify ambiguous moves */
        private string get_disambiguator(Move move, bool sloppy)
        {
            Stack<Move> moves = generate_moves(new Dictionary<string, object> { { "legal", !sloppy } });
            int from = move.from;
            int to = move.to;
            ChessPieceKind piece = move.piece;

            int ambiguities = 0;
            int same_rank = 0;
            int same_file = 0;

            for (int i = 0; i < moves.Count; i++)
            {
                int ambig_from = moves.ElementAt(i).from;
                int ambig_to = moves.ElementAt(i).to;
                ChessPieceKind ambig_piece = moves.ElementAt(i).piece;

                /* if a move of the same piece type ends on the same to square, we'll
                * need to add a disambiguator to the algebraic notation
                */
                if (piece == ambig_piece && from != ambig_from && to == ambig_to)
                {
                    ambiguities++;
                    if (rank(from) == rank(ambig_from))
                    {
                        same_rank++;
                    }
                    if (file(from) == file(ambig_from))
                    {
                        same_file++;
                    }
                }

            }

            if (ambiguities > 0)
            {
                /* if there exists a similar moving piece on the same rank and file as
                * the move in question, use the square as the disambiguator
                */
                if (same_rank > 0 && same_file > 0)
                {
                    return ChessSquare.GetAlgebraicNotation(from);
                }

                /* if the moving piece rests on the same file, use the rank symbol as the
                * disambiguator
                */
                else if (same_file > 0)
                {
                    return ChessSquare.GetAlgebraicNotation(from).Substring(1, 1);
                }
                /* else use the file symbol */
                else
                {
                    return ChessSquare.GetAlgebraicNotation(from).Substring(0, 1);
                }
            }
            return "";
        }

        private string ascii()
        {
            var s = "   +------------------------+\n";
            for (var i = SQUARES["a8"]; i <= SQUARES["h1"]; i++)
            {
                /* display the rank */
                if (file(i) == 0)
                {
                    s += " " + "87654321".Substring(rank(i), 1) + " |";
                }

                /* empty piece */
                if (board[i] == null)
                {
                    s += " . ";
                }
                else
                {
                    ChessPieceKind piece = board[i].Kind;
                    ChessColor color = board[i].Color;
                    string symbol = ChessPieceToFEN(piece, color);
                    s += " " + symbol + " ";
                }

                if (((i + 1) & 0x88) != 0)
                {
                    s += "|\n";
                    i += 8;
                }
            }

            s += "   +------------------------+\n";
            s += "     a  b  c  d  e  f  g  h\n";
            return s;
        }

        // convert a move from Standard Algebraic Notation (SAN) to 0x88 coordinates
        private Move move_from_san(string move, bool sloppy)
        {
            //// strip off any move decorations: e.g Nf3+?!
            //string clean_move = stripped_san(move);
            //// if we're using the sloppy parser run a regex to grab piece, to, and from
            //// this should parse invalid SAN like: Pe2-e4, Rc1c4, Qf3xf7
            //string[] matches = { };
            //string piece = null;
            //string from = null;
            //string to = null;
            //string promotion = null;
            //if (sloppy)
            //{
            //    Regex rx = new Regex(@"([pnbrqkPNBRQK])?([a-h][1-8])x?-?([a-h][1-8])([qrbnQRBN])?");
            //    matches = rx.Split(clean_move);
            //    if (matches.Length > 1)
            //    {
            //        if (matches.Length == 4)
            //        {
            //            from = matches[1];
            //            to = matches[2];
            //        }

            //        if (matches.Length == 5)
            //        {
            //            if (matches[1].Length == 1)
            //            {
            //                piece = matches[1];
            //                from = matches[2];
            //                to = matches[3];
            //            }
            //            else
            //            {
            //                from = matches[1];
            //                to = matches[2];
            //                promotion = matches[3];
            //            }
            //        }

            //        if (matches.Length == 6)
            //        {
            //            piece = matches[1];
            //            from = matches[2];
            //            to = matches[3];
            //            promotion = matches[4];
            //        }

            //    }
            //}

            //Stack<Move> moves = generate_moves(); // edgeway- get all legal moves for current player

            //for (int i = 0; i < moves.Count; i++)
            //{
            //    // try the strict parser first, then the sloppy parser if requested
            //    // by the user
            //    if ((clean_move == stripped_san(move_to_san(moves.ElementAt(i)))) || (sloppy && clean_move == stripped_san(move_to_san(moves.ElementAt(i), true))))
            //    {
            //        return moves.ElementAt(i);
            //    }
            //    else
            //    {
            //        if (matches.Length > 1 &&
            //        (string.IsNullOrEmpty(piece) || FenToChessPiece(piece) == moves.ElementAt(i).piece) &&
            //        SQUARES[from] == moves.ElementAt(i).from &&
            //        SQUARES[to] == moves.ElementAt(i).to &&
            //        (string.IsNullOrEmpty(promotion) || FenToChessPiece(promotion) == moves.ElementAt(i).promotion))
            //        {
            //            return moves.ElementAt(i);
            //        }
            //    }
            //}
            return null;
        }



        // UTILITY FUNCTIONS
        // ****************************************************************************
        private static int rank(int i)
        {
            return i >> 4;
        }

        private static int file(int i)
        {
            return i & 15;
        }

        private static ChessColor swap_color(ChessColor c)
        {
            return c == ChessColor.White ? ChessColor.Black : ChessColor.White;
        }

        private static bool isNAN(string i)
        {
            return !int.TryParse(i, out _);
        }

        /// <summary>
        /// Returns the FEN abreviation of the piece.
        /// </summary>
        /// <param name="piece"></param>
        /// <returns></returns>
        public static string ChessPieceToFEN(ChessPieceKind piece, ChessColor color = ChessColor.Black)
        {
            var result = "";

            switch (piece)
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

            return (color == ChessColor.White ? result.ToUpper() : result.ToLower());
        }

        /// <summary>
        /// Returns a ChessPiece corresponding to its FEN abbreviation
        /// </summary>
        /// <param name="fenPiece">Case insensitive FEN abbreviation. Possible values are: p, n, b, r, q, k.</param>
        /// <returns></returns>
        public static ChessPieceKind FenToChessPiece(string fenPiece)
        {
            return FenToChessPiece(fenPiece[0]);
        }

        /// <summary>
        /// Returns the kind of a chess piece corresponding to its FEN abbreviation.
        /// </summary>
        /// <param name="fenPiece">Case insensitive FEN abbreviation. Possible values are: p, n, b, r, q, k.</param>
        /// <returns></returns>
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


        // ================================ public API ===============================

        public string Ascii()
        {
            return ascii();
        }

        public bool InCheck()
        {
            return IsKingAttacked(Turn);
        }

        public bool InCheckmate()
        {
            return in_checkmate();
        }

        public bool InStalemate()
        {
            return in_stalemate();
        }

        public bool InsufficientMaterial()
        {
            return insufficient_material();
        }

        public bool InThreefoldRepetition()
        {
            return in_threefold_repetition();
        }

        public bool FiftyMoveRule()
        {
            return half_moves >= 100;
        }

        public bool InDraw()
        {
            return (FiftyMoveRule() || in_stalemate() || insufficient_material() || in_threefold_repetition());
        }

        public bool GameOver()
        {
            return (FiftyMoveRule() || in_checkmate() || in_stalemate() || insufficient_material() || in_threefold_repetition());
        }

        /// <summary>
        /// Move a piece from one square to another square.
        /// </summary>
        /// <param name="move">An instance of a validated move. <see cref="GetMoveValidity(ChessSquare, ChessSquare)(ChessSquare, ChessSquare)"/></param>
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
        /// Move a piece from one square to another square.
        /// </summary>
        /// <param name="from">Origin square.</param>
        /// <param name="to">Destination square.</param>
        /// <param name="promotedTo">Type of piece used in case of a pawn promotion.</param>
        /// <exception cref="IllegalMoveException">Thrown when the requested move is illegal.</exception>
        public void Move(ChessSquare from, ChessSquare to, ChessPieceKind promotedTo = ChessPieceKind.Queen)
        {
            var moveValidation = GetMoveValidity(from, to);
            moveValidation.PromotedTo = promotedTo;

            Move(moveValidation);
        }

        public string[] MoveHistory()
        {
            //Stack<Move> reversed_history = new Stack<Move>();
            //Stack<string> move_history = new Stack<string>();
            //while (history.Count > 0)
            //{
            //    reversed_history.Push(undo_move());
            //}

            //while (reversed_history.Count > 0)
            //{
            //    var move = reversed_history.Pop();
            //    move_history.Push(move_to_san(move));
            //    MovePiece(move, move.promotion);
            //}

            //string[] h = new string[move_history.Count];
            //for (int i = h.Length - 1; i > -1; i--)
            //{
            //    h[i] = move_history.Pop();
            //}

            //return h;
            return null;
        }

        public UndoMoveResult Undo()
        {
            //Move move = undo_move();
            //if (move == null)
            //{
            //    return null;
            //}

            //UndoMoveResult undoMoveArgs = new UndoMoveResult();
            //undoMoveArgs.color = move.color;
            //undoMoveArgs.from = algebraic(move.from);
            //undoMoveArgs.to = algebraic(move.to);
            //undoMoveArgs.piece = move.piece;
            //return undoMoveArgs;
            return null;
        }

        public string[] LegalMovesAll()
        {
            //Stack<Move> moves = generate_moves();
            //Stack<string> legalMoves = new Stack<string>();
            //for (int i = 0; i < moves.Count; i++)
            //{
            //    legalMoves.Push(move_to_san(moves.ElementAt(i), false));
            //}
            //return legalMoves.ToArray();
            return null;
        }

        public string[] LegalMovesSquare(string square)
        {
            //Stack<Move> moves = generate_moves(new Dictionary<string, object>() { { "square", square } });
            //Stack<string> legalMoves = new Stack<string>();
            //if (moves != null)
            //{
            //    for (int i = 0; i < moves.Count; i++)
            //    {
            //        legalMoves.Push(move_to_san(moves.ElementAt(i), false));
            //    }
            //}
            //return legalMoves.ToArray();
            return null;
        }

        public FENValidationResult ValidateFen(string fen)
        {
            return FENValidator.Validate(fen);
        }
    }



    // =============================== CLASSES =================================

    public class Move
    {
        public ChessColor color { get; set; }
        public int from { get; set; }
        public int to { get; set; }
        public ChessMoveType flags { get; set; }

        public ChessPieceKind piece { get; set; }
        public ChessPieceKind promotion { get; set; }
        public ChessPieceKind captured { get; set; }
    }

    internal class BoardState
    {
        public ChessMove move { get; set; }
        public Dictionary<ChessColor, int> kings { get; set; }
        public ChessColor turn { get; set; }
        public Dictionary<ChessColor, ChessCastling> castling { get; set; }
        public int ep_square { get; set; }
        public int half_moves { get; set; }
        public int move_number { get; set; }
    }

    public class UndoMoveResult
    {
        public ChessColor color;
        public string from = "";
        public string to = "";
        public ChessPieceKind piece = ChessPieceKind.None;
    }
}