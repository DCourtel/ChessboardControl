using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Drawing.Imaging;

namespace ChessboardControl
{
    [Designer(typeof(ChessboardDesigner))]
    [DefaultEvent("OnPieceMoved")]
    [ToolboxBitmap(@"C:\Users\AdminSRV\source\repos\Chessboard Control\Chessboard Control\Chessboard.bmp")]
    public partial class Chessboard : Control
    {
        //  Sizes
        private const int MINIMUM_CONTROL_HEIGHT = 255;
        private const int MINIMUM_CONTROL_WIDTH = 255;
        private const int MINIMUM_COORDINATE_AREA_HEIGHT = MINIMUM_CONTROL_HEIGHT / 17;
        private const int MINIMUM_COORDINATE_AREA_WIDTH = MINIMUM_CONTROL_WIDTH / 17;
        private const int MINIMUM_SQUARE_HEGHT = (int)(MINIMUM_CONTROL_HEIGHT / 8.5);
        private const int MINIMUM_SQUARE_WIDTH = (int)(MINIMUM_CONTROL_WIDTH / 8.5);
        //  Colors
        private readonly Color DEFAULT_WHITE_SQUARE_COLOR = Color.WhiteSmoke;
        private readonly Color DEFAULT_BLACK_SQUARE_COLOR = Color.SteelBlue;
        private readonly Color DEFAULT_COORDINATE_BACKGROUND_COLOR = Color.Gainsboro;
        private readonly Brush DEFAULT_COORDINATE_FORGROUND_BRUSH = Brushes.DimGray;

        private struct DragOperation
        {
            internal DragOperation(ChessPiece selectedPiece, ChessSquare source)
            {
                DraggedPiece = selectedPiece;
                Origin = source;
            }

            internal ChessPiece DraggedPiece { get; set; }
            internal ChessSquare Origin { get; set; }
        }

        public Chessboard()
        {
            InitializeComponent();
            SetStyle(ControlStyles.OptimizedDoubleBuffer |
                    ControlStyles.UserPaint |
                    ControlStyles.AllPaintingInWmPaint, true);
            Size = new Size(340, 340);
            LightSquaresColor = DEFAULT_WHITE_SQUARE_COLOR;
            DarkSquaresColor = DEFAULT_BLACK_SQUARE_COLOR;
            CoordinateAreaBackColor = DEFAULT_COORDINATE_BACKGROUND_COLOR;
        }

        #region Properties

        private Color _darkSquaresColor;
        /// <summary>
        /// Gets or sets the color used to draw dark squares.
        /// </summary>
        [DefaultValue(typeof(Color), "#FF4682B4")]
        public Color DarkSquaresColor
        {
            get { return _darkSquaresColor; }
            set
            {
                if (value != _darkSquaresColor)
                {
                    _darkSquaresColor = value;
                    Invalidate();
                }
            }
        }

        private BoardDirection _boardDirection = BoardDirection.BlackOnTop;
        /// <summary>
        /// Gets or sets the orientation of the board.
        /// </summary>
        [DefaultValue(typeof(BoardDirection), "BlackOnTop")]
        public BoardDirection BoardDirection
        {
            get { return _boardDirection; }
            set
            {
                if (value != _boardDirection)
                {
                    _boardDirection = value;
                    Invalidate();
                }
            }
        }

        private Color _coordinateAreaBackColor;
        /// <summary>
        /// Gets or sets the backcolor of the area where coordinates are drawn.
        /// </summary>
        [DefaultValue(typeof(Color), "#FFDCDCDC")]
        public Color CoordinateAreaBackColor
        {
            get { return _coordinateAreaBackColor; }
            set
            {
                if (value != _coordinateAreaBackColor)
                {
                    _coordinateAreaBackColor = value;
                    Invalidate();
                }
            }
        }

        /// <summary>
        /// Gets the width of the area where digits of the coordinates are drawn.
        /// </summary>
        private int DigitAreaWidth
        {
            get { return Math.Max(MINIMUM_COORDINATE_AREA_WIDTH, this.Size.Width / 17); }
        }

        /// <summary>
        /// Gets or sets the current DragDrop operation.
        /// </summary>
        private DragOperation DragDropOperation { get; set; }

        /// <summary>
        /// Gets whether the King is attacked.
        /// </summary>
        /// <returns></returns>
        public bool IsCheck { get { return ChessEngine.IsCheck; } }

        /// <summary>
        /// Gets whether the King is checkmate.
        /// </summary>
        public bool IsCheckmate { get { return ChessEngine.IsCheckmate; } }

        /// <summary>
        /// Gets whether the game is in draw.
        /// </summary>
        /// <returns></returns>
        public bool IsDraw { get { return ChessEngine.IsDraw; } }

        /// <summary>
        /// Gets whether the game is draw by the fifty moves rule.
        /// </summary>
        public bool IsDrawByFiftyMoveRule { get { return ChessEngine.FiftyMoveRule; } }

        /// <summary>
        /// Gets whether there is not enough material to win.
        /// </summary>
        /// <returns></returns>
        public bool IsDrawByInsufficientMaterial { get { return ChessEngine.IsDrawByInsufficientMaterial; } }

        /// <summary>
        /// Returns whether the same position has repeated three time.
        /// </summary>
        /// <returns></returns>
        public bool IsDrawByThreefoldRepetition { get { return ChessEngine.IsDrawByThreefoldRepetition; } }

        /// <summary>
        /// Gets whether the game is over.
        /// </summary>
        public bool IsGameOver { get { return ChessEngine.GameOver; } }

        /// <summary>
        /// Gets whether the King is stalemate.
        /// </summary>
        public bool IsStalemate { get { return ChessEngine.IsStalemate; } }

        /// <summary>
        /// Gets the height of the area where letters of the coordinates are drawn.
        /// </summary>
        private int LetterAreaHeight
        {
            get { return Math.Max(MINIMUM_COORDINATE_AREA_HEIGHT, this.Size.Height / 17); }
        }

        /// <summary>
        /// Gets the position of all pieces.
        /// </summary>
        private Board ChessEngine { get; } = new Board();

        private Color _lightSquaresColor;
        /// <summary>
        /// Gets or sets the color used to draw light squares.
        /// </summary>
        [DefaultValue(typeof(Color), "#FFF5F5F5")]
        public Color LightSquaresColor
        {
            get { return _lightSquaresColor; }
            set
            {
                if (value != _lightSquaresColor)
                {
                    _lightSquaresColor = value;
                    Invalidate();
                }
            }
        }

        /// <summary>
        /// Gets or sets whether to show visual hints.
        /// </summary>
        public bool ShowVisualHints { get; set; } = false;

        /// <summary>
        /// Gets or sets the size of the control. Minimum value is 255x255.
        /// </summary>
        [DefaultValue(typeof(Size), "340,340")]
        public new Size Size
        {
            get
            {
                return base.Size;
            }
            set
            {
                if (value.Width != base.Size.Width || value.Height != base.Size.Height)
                {
                    var newWidth = Math.Max(MINIMUM_CONTROL_WIDTH, (int)Math.Floor(value.Width / 17f) * 17);
                    var newHeight = Math.Max(MINIMUM_CONTROL_HEIGHT, (int)Math.Floor(value.Height / 17f) * 17);
                    base.Size = new Size(newWidth, newHeight);
                    Invalidate();
                }
            }
        }

        /// <summary>
        /// Gets the height of a square.
        /// </summary>
        private int SquareHeight
        {
            get { return Math.Max(MINIMUM_SQUARE_HEGHT, (int)(this.Size.Height / 8.5)); }
        }

        /// <summary>
        /// Gets the width of a square.
        /// </summary>
        private int SquareWidth
        {
            get { return Math.Max(MINIMUM_SQUARE_WIDTH, (int)(this.Size.Width / 8.5)); }
        }

        /// <summary>
        /// Gets whose turn it is.
        /// </summary>
        public ChessColor Turn { get { return ChessEngine.Turn; } }

        #endregion Properties

        #region Public Methods

        /// <summary>
        /// Checks whether a move is valid and give the type of move (normal, capture, promotion).
        /// In case the move is a promotion, set the <see cref="ChessMove.PromotedTo"/> property accordingly before to pass the return object to the <see cref="MovePiece(ChessMove)"/> method.
        /// </summary>
        /// <param name="from">Square the piece move from.</param>
        /// <param name="to">Square the piece move to.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="from"/> or <paramref name="to"/> are null.</exception>
        public ChessMove CheckMoveValidity(ChessSquare from, ChessSquare to)
        {
            return ChessEngine.GetMoveValidity(from, to);
        }

        /// <summary>
        /// Removes all pieces from the board.
        /// </summary>
        public void ClearBoard()
        {
            ChessEngine.Clear();
            Invalidate();
        }

        /// <summary>
        /// Flips the board. If Blacks were on top, they will be on bottom and vice versa.
        /// </summary>
        public void FlipBoard()
        {
            BoardDirection = (BoardDirection == BoardDirection.BlackOnTop ? BoardDirection.WhiteOnTop : BoardDirection.BlackOnTop);
        }

        /// <summary>
        /// Returns an ASCII representation of the current board.
        /// </summary>
        /// <returns></returns>
        public string GetASCII()
        {
            return ChessEngine.Ascii();
        }

        /// <summary>
        /// Gets the castling possibilities for the given side.
        /// </summary>
        /// <param name="side"></param>
        /// <returns></returns>
        public ChessCastling GetCastlingPossiblitities(ChessColor side)
        {
            return ChessEngine.GetCastlingRights(side);
        }

        /// <summary>
        /// Gets a string describing the position in the Forsyth–Edwards Notation (FEN).
        /// </summary>
        /// <returns></returns>
        public string GetFEN()
        {
            return ChessEngine.GetFEN();
        }

        /// <summary>
        /// Returns all legal moves for the current position.
        /// </summary>
        /// <returns></returns>
        public List<ChessMove> GetLegalMoves()
        {
            return ChessEngine.GetLegalMoves();
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
            return ChessEngine.GetLegalMoves(square);
        }

        /// <summary>
        /// Returns an array of all the chess moves played so far.
        /// </summary>
        /// <returns></returns>
        public ChessMove[] GetMoveHistory()
        {
            return ChessEngine.GetMoveHistory();
        }

        /// <summary>
        /// Gets the piece at the given position.
        /// </summary>
        /// <param name="square">Coordinates of the square where to look at.</param>
        /// <returns></returns>
        public ChessPiece GetPieceAt(ChessSquare square)
        {
            return ChessEngine.GetPieceAt(square);
        }

        /// <summary>
        /// Returns the bitmap for the given piece.
        /// </summary>
        /// <param name="piece"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="piece"/> has an invalid <see cref="ChessPieceKind"/>.</exception>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="piece"/> is null.</exception>
        public Bitmap GetPieceImage(ChessPiece piece)
        {
            if (piece == null) { throw new ArgumentNullException(); }

            switch (piece.Kind)
            {
                case ChessPieceKind.Pawn:
                    return piece.Color == ChessColor.White ? Properties.Resources.WhitePawn : Properties.Resources.BlackPawn;
                case ChessPieceKind.Rook:
                    return piece.Color == ChessColor.White ? Properties.Resources.WhiteRook : Properties.Resources.BlackRook;
                case ChessPieceKind.Knight:
                    return piece.Color == ChessColor.White ? Properties.Resources.WhiteKnight : Properties.Resources.BlackKnight;
                case ChessPieceKind.Bishop:
                    return piece.Color == ChessColor.White ? Properties.Resources.WhiteBishop : Properties.Resources.BlackBishop;
                case ChessPieceKind.King:
                    return piece.Color == ChessColor.White ? Properties.Resources.WhiteKing : Properties.Resources.BlackKing;
                case ChessPieceKind.Queen:
                    return piece.Color == ChessColor.White ? Properties.Resources.WhiteQueen : Properties.Resources.BlackQueen;
            }
            throw new ArgumentOutOfRangeException();
        }

        /// <summary>
        /// Resets the board’s state and setup pieces as given in the FEN.
        /// </summary>
        /// <param name="fen">A string describing the position of pieces in the Forsyth–Edwards Notation (FEN).</param>
        /// <exception cref="ArgumentException">Thrown when <paramref name="fen"/> is invalid.</exception>
        /// <remarks>Use <see cref="FENValidator.Validate(string)"/> method to validate FEN strings.</remarks>
        public void LoadFEN(string fen)
        {
            ChessEngine.LoadFEN(fen);
            Invalidate();
        }

        /// <summary>
        /// Moves a piece from one square to another one.
        /// </summary>
        /// <returns>Returns the captured piece if any.</returns>
        /// <param name="move">An instance of a validated move. Use <see cref="CheckMoveValidity(ChessSquare, ChessSquare)"/> to get an instance of a validated <see cref="ChessMove"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="move"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown i <paramref name="move"/> is invalid.</exception>
        public void MovePiece(ChessMove move)
        {
            if (move == null) { throw new ArgumentNullException(); }
            if (!move.IsValid) { throw new ArgumentException(); }

            ChessEngine.Move(move);
            Invalidate();
        }

        /// <summary>
        /// Puts a new piece on the board.
        /// </summary>
        /// <param name="piece">Piece to add to the board.</param>
        /// <param name="square">Square where to put the piece.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="piece"/> or <paramref name="square"/> are null.</exception>
        /// <exception cref="ArgumentException">Thrown when putting the piece on the board would result to have more than one King of the same color.</exception>
        public void PutPieceAt(ChessPiece piece, ChessSquare square)
        {
            ChessEngine.PutPiece(piece, square);
        }

        /// <summary>
        /// Removes the piece on the given square.
        /// </summary>
        /// <param name="square">Coordinates of the square where to remove the piece.</param>
        /// <returns>An instance of the removed piece or null if there was no piece on the square.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="square"/> is null.</exception>
        public ChessPiece RemovePieceAt(ChessSquare square)
        {
            return ChessEngine.RemovePieceAt(square);
        }

        /// <summary>
        /// Saves the control as a PNG image to the given file.
        /// </summary>
        /// <param name="filePath">Full path of the file to save the control in.</param>
        public void SaveAsImage(string filePath)
        {
            Bitmap bmp = new Bitmap(this.Width, this.Height);

            this.DrawToBitmap(bmp, new Rectangle(Point.Empty, bmp.Size));
            bmp.Save(filePath, System.Drawing.Imaging.ImageFormat.Png);
        }

        /// <summary>
        /// Defines the piece at the given position.
        /// </summary>
        /// <param name="square">Coordinates of the square where to set the piece.</param>
        /// <param name="piece">Piece to set.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="piece"/> or <paramref name="square"/> are null.</exception>
        public void SetPieceAt(ChessPiece piece, ChessSquare square)
        {
            ChessEngine.PutPiece(piece, square);
        }

        /// <summary>
        /// Removes all pieces and add Black and White pieces into their initial position.
        /// </summary>
        public void SetupInitialPosition()
        {
            ChessEngine.LoadFEN(Board.INITIAL_FEN_POSITION);
            Invalidate();
        }

        /// <summary>
        /// Undoes the last played move.
        /// </summary>
        /// <returns>An instance of the last move or null if there is no moves in the MoveHistory.</returns>
        public ChessMove UndoMove()
        {
            var undoneMove = ChessEngine.UndoMove();
            Invalidate();

            return undoneMove;
        }

        #endregion Public Methods

        #region Private Methods

        private void RedrawSquare(ChessSquare targetedSquare)
        {
            var g = this.CreateGraphics();
            var blackSquareBrush = new Pen(DarkSquaresColor).Brush;
            var whiteSquareBrush = new Pen(LightSquaresColor).Brush;
            var x = (int)targetedSquare.File;
            var y = (int)targetedSquare.Rank;
            var isWhiteSquare = (((int)targetedSquare.File + (int)targetedSquare.Rank) % 2) != 0;
            var square = (BoardDirection == BoardDirection.BlackOnTop ?
                new RectangleF(DigitAreaWidth + x * SquareWidth, (7 - y) * SquareHeight, SquareWidth, SquareHeight) :
                new RectangleF(DigitAreaWidth + (7 - x) * SquareWidth, (y) * SquareHeight, SquareWidth, SquareHeight));

            //  Draw square
            g.FillRectangle(isWhiteSquare ? whiteSquareBrush : blackSquareBrush, square);
            var currentPiece = GetPieceAt(targetedSquare);
            if (currentPiece != null)
            {
                if (DragDropOperation.Origin == null || DragDropOperation.Origin !=targetedSquare)
                {
                    g.DrawImage(GetPieceImage(currentPiece), square);
                }
            }

            //  Draw borders
            var borders = new Rectangle(0, 0, Width - 1, Height - 1);
            g.DrawRectangle(new Pen(Color.Black), borders);

            g.Flush();
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            var controlImage = new Bitmap(this.Width, this.Height);
            using (Graphics g = Graphics.FromImage(controlImage))
            {
                var coordinateAreaBrush = new Pen(CoordinateAreaBackColor).Brush;
                var darkSquaresBrush = new Pen(DarkSquaresColor).Brush;
                var lightSquaresBrush = new Pen(LightSquaresColor).Brush;

                //  Draw a filled rectangle for digits
                g.FillRectangle(coordinateAreaBrush, 0, 0, DigitAreaWidth, this.Height);

                //  Draw a filled rectangle for letters
                g.FillRectangle(coordinateAreaBrush, 0, this.Height - LetterAreaHeight, this.Width, LetterAreaHeight);

                //  Draw letters
                var coordinateFont = new Font(FontFamily.GenericMonospace, 11 + (SquareWidth - MINIMUM_SQUARE_WIDTH) / 4);
                for (int i = 0; i < 8; i++)
                {
                    var letter = (BoardDirection == BoardDirection.BlackOnTop ?
                        ((ChessFile)i).ToString() :
                         ((ChessFile)7 - i).ToString());
                    var characterSize = g.MeasureString(letter, coordinateFont);
                    var x = DigitAreaWidth + i * SquareWidth + (SquareWidth - characterSize.Width) / 2;
                    var y = this.Height - LetterAreaHeight + (LetterAreaHeight - characterSize.Height) / 2;
                    g.DrawString(letter, coordinateFont, DEFAULT_COORDINATE_FORGROUND_BRUSH, x, y);
                }

                //  Draw digits
                coordinateFont = new Font(FontFamily.GenericMonospace, 11 + (SquareHeight - MINIMUM_SQUARE_HEGHT) / 4);
                for (int i = 0; i < 8; i++)
                {
                    var digit = (BoardDirection == BoardDirection.BlackOnTop ?
                        (8 - i).ToString() :
                        (i + 1).ToString());
                    var characterSize = g.MeasureString(digit, coordinateFont);
                    var x = (DigitAreaWidth - characterSize.Width) / 2;
                    var y = i * SquareHeight + (SquareHeight - characterSize.Height) / 2;
                    g.DrawString(digit, coordinateFont, DEFAULT_COORDINATE_FORGROUND_BRUSH, x, y);
                }

                //  Draw Turn indicator
                var turnIndicatorBorder = new Rectangle(0, Height - LetterAreaHeight, DigitAreaWidth, LetterAreaHeight);
                var turnIndicatorSquare = new RectangleF(0, Height - LetterAreaHeight, DigitAreaWidth, LetterAreaHeight);
                g.FillRectangle(ChessEngine.Turn == ChessColor.White ? lightSquaresBrush : darkSquaresBrush, turnIndicatorSquare);
                g.DrawRectangle(new Pen(Color.Black), turnIndicatorBorder);

                //  Draw cells
                bool isLightSquare = true;
                for (int x = 0; x < 8; x++)
                {
                    for (int y = 0; y < 8; y++)
                    {
                        var square = new RectangleF(DigitAreaWidth + x * SquareWidth, y * SquareHeight, SquareWidth, SquareHeight);
                        g.FillRectangle(isLightSquare ? lightSquaresBrush : darkSquaresBrush, square);
                        ChessSquare currentSquare = BoardDirection == BoardDirection.BlackOnTop ?
                            new ChessSquare((ChessFile)x, (ChessRank)7 - y) :
                            new ChessSquare((ChessFile)7 - x, (ChessRank)y);
                        if (currentSquare != DragDropOperation.Origin)
                        {
                            ChessPiece currentPiece = ChessEngine.GetPieceAt(currentSquare);
                            if (currentPiece != null)
                            {
                                g.DrawImage(GetPieceImage(currentPiece), square);
                            }
                        }

                        isLightSquare = !isLightSquare;
                    }
                    isLightSquare = !isLightSquare;
                }

                //  Draw visual hints
                DrawVisualHints(g);

                //  Draw borders
                var borders = new Rectangle(0, 0, Width - 1, Height - 1);
                g.DrawRectangle(new Pen(Color.Black), borders);
                g.Flush();
            }

            pe.Graphics.DrawImage(this.Enabled ? controlImage : GrayOutImage(controlImage), new Point(0, 0));
        }

        private void DrawVisualHints(Graphics g)
        {
            if (ShowVisualHints && DragDropOperation.Origin != null)
            {
                List<ChessMove> legalMoves = ChessEngine.GetLegalMoves(DragDropOperation.Origin);
                foreach (ChessMove chessMove in legalMoves)
                {
                    g.FillEllipse(new SolidBrush(Color.FromArgb(150, 92, 214, 92)),
                        BoardDirection == BoardDirection.BlackOnTop ?
                        GetHintRectangle((int)chessMove.To.File, 7 - (int)chessMove.To.Rank) :
                        GetHintRectangle(7 - (int)chessMove.To.File, (int)chessMove.To.Rank));
                }
            }
        }

        private RectangleF GetHintRectangle(int x, int y)
        {
            return new RectangleF(DigitAreaWidth + x * SquareWidth + SquareWidth / 4, y * SquareHeight + SquareHeight / 4, SquareWidth / 2f, SquareHeight / 2f);
        }

        /// <summary>
        /// Grayout the given image.
        /// </summary>
        /// <param name="originalImage"></param>
        /// <returns></returns>
        private Bitmap GrayOutImage(Bitmap originalImage)
        {
            Bitmap grayoutImage = new Bitmap(originalImage.Width, originalImage.Height);

            using (Graphics g = Graphics.FromImage(grayoutImage))
            {
                ColorMatrix colorMatrix = new ColorMatrix(
                   new float[][]
                   {
                     new float[] {.3f, .3f, .3f, 0, 0},
                     new float[] {.59f, .59f, .59f, 0, 0},
                     new float[] {.11f, .11f, .11f, 0, 0},
                     new float[] {0, 0, 0, 1, 0},
                     new float[] {0, 0, 0, 0, 1}
                   });

                using (ImageAttributes attributes = new ImageAttributes())
                {
                    attributes.SetColorMatrix(colorMatrix);
                    g.DrawImage(originalImage, new Rectangle(0, 0, originalImage.Width, originalImage.Height),
                                0, 0, originalImage.Width, originalImage.Height, GraphicsUnit.Pixel, attributes);
                }
            }
            return grayoutImage;
        }

        #endregion Private Methods

        #region Delegates & Events

        public delegate void SelectedSquareEventHandler(Chessboard sender, ChessSquare from, ChessPiece selectedPiece);
        [Description("Fired when the user presses the left button of the mouse on a square.")]
        public event SelectedSquareEventHandler OnSquareSelected;

        [Description("Fired when the user releases the left button of the mouse on a square.")]
        public event EventHandler OnSquareUnselected;

        public delegate void PieceMovedEventHandler(Chessboard sender, ChessMove move);
        [Description("Fired when the user moves a piece on the board.")]
        public event PieceMovedEventHandler OnPieceMoved;

        public delegate void PieceRemovedEventHandler(Chessboard sender, ChessSquare from, ChessPiece removedPiece, Point dropPoint);
        [Description("Fired when the user drags and drop a piece from the board to a location outside of the board.")]
        public event PieceRemovedEventHandler OnPieceRemoved;

        [Description("Fired when the player is checkmate.")]
        public event EventHandler OnCheckmate;

        [Description("Fired when the player is check.")]
        public event EventHandler OnCheck;

        [Description("Fired when the position is Draw.")]
        public event EventHandler OnDraw;

        #endregion

        #region Events

        private void Chessboard_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.X > DigitAreaWidth && e.Y < Height - LetterAreaHeight)
            {
                var origin = (BoardDirection == BoardDirection.BlackOnTop ?
                    new ChessSquare((ChessFile)((e.X - DigitAreaWidth) / SquareWidth), (ChessRank)(7 - (e.Y / SquareHeight))) :
                    new ChessSquare((ChessFile)(7 - (e.X - DigitAreaWidth) / SquareWidth), (ChessRank)(e.Y / SquareHeight)));
                var selectedPiece = GetPieceAt(origin);
                
                if (selectedPiece != null)
                {
                    if (selectedPiece.Color != Turn)
                    {
                        Cursor = Cursors.No;
                        return;
                    }
                    var resizedBitmap = new Bitmap(GetPieceImage(selectedPiece), new Size(SquareWidth, SquareHeight));
                    Cursor = new Cursor(resizedBitmap.GetHicon());
                    DragDropOperation = new DragOperation(selectedPiece, origin);
                    RedrawSquare(origin);
                    Invalidate();
                    OnSquareSelected?.Invoke(this, origin, selectedPiece);
                }
            }
        }

        private void Chessboard_MouseUp(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Default;
            if (DragDropOperation.DraggedPiece != null && DragDropOperation.DraggedPiece.Kind != ChessPieceKind.None)
            {
                if (e.X > DigitAreaWidth && e.X < this.Width && e.Y < Height - LetterAreaHeight && e.Y > 0)
                {
                    //  Drops a piece on the board
                    var destinationSquare = (BoardDirection == BoardDirection.BlackOnTop ?
                        new ChessSquare((ChessFile)((e.X - DigitAreaWidth) / SquareWidth), (ChessRank)(7 - (e.Y / SquareHeight))) :
                        new ChessSquare((ChessFile)(7 - (e.X - DigitAreaWidth) / SquareWidth), (ChessRank)(e.Y / SquareHeight)));
                    var moveValidationResult = ChessEngine.GetMoveValidity(DragDropOperation.Origin, destinationSquare);
                    if (moveValidationResult.IsValid)
                    {
                        var promotionCancelled = false;
                        if ((moveValidationResult.MoveKind & ChessMoveType.Promotion) == ChessMoveType.Promotion)
                        {
                            var pieceChooser = new FrmPromotion(ChessEngine.Turn);
                            promotionCancelled = (pieceChooser.ShowDialog() != DialogResult.OK);
                            moveValidationResult.PromotedTo = pieceChooser.ChoosePiece;
                        }
                        if (!promotionCancelled)
                        {
                            MovePiece(moveValidationResult);
                            moveValidationResult.ToSAN = ChessEngine.MoveToSAN(moveValidationResult);   //  Update the SAN after promotion

                            OnPieceMoved?.Invoke(this, moveValidationResult);
                            if (ChessEngine.IsCheckmate)
                            { OnCheckmate?.Invoke(this, new EventArgs()); }
                            else if (ChessEngine.IsCheck)
                            { OnCheck?.Invoke(this, new EventArgs()); }
                            else if (ChessEngine.IsDraw)
                            { OnDraw?.Invoke(this, new EventArgs()); }
                        }
                        else
                        {
                            Invalidate();
                        }
                    }
                    else
                    {
                        Invalidate();
                    }
                }
                else
                {
                    //  Drops a piece outside of the board
                    ChessEngine.RemovePieceAt(DragDropOperation.Origin);
                    Invalidate();
                    OnPieceRemoved?.Invoke(this, DragDropOperation.Origin, DragDropOperation.DraggedPiece, e.Location);
                }
            }
            DragDropOperation = new DragOperation(null, null);
            OnSquareUnselected?.Invoke(this, new EventArgs());
        }

        private void Chessboard_Resize(object sender, EventArgs e)
        {
            Invalidate();
        }

        #endregion Events
    }
}
