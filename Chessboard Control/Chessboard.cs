using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ChessboardControl
{
    [DefaultEvent("OnPieceMoved")]
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
            internal DragOperation(ChessPieceKind selectedPiece, ChessSquare source)
            {
                DraggedPiece = selectedPiece;
                Origin = source;
            }

            internal ChessPieceKind DraggedPiece { get; set; }
            internal ChessSquare Origin { get; set; }
        }

        public Chessboard()
        {
            InitializeComponent();
            Size = new Size(340, 340);
            WhiteSquareColor = DEFAULT_WHITE_SQUARE_COLOR;
            BlackSquareColor = DEFAULT_BLACK_SQUARE_COLOR;
            CoordinateAreaBackColor = DEFAULT_COORDINATE_BACKGROUND_COLOR;
        }

        #region Properties

        private Color _blackSquareColor;
        /// <summary>
        /// Gets or sets the color used to draw black squares.
        /// </summary>
        [DefaultValue(typeof(Color), "#FF4682B4")]
        public Color BlackSquareColor
        {
            get { return _blackSquareColor; }
            set
            {
                if (value != _blackSquareColor)
                {
                    _blackSquareColor = value;
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

        private Color _whiteSquareColor;
        /// <summary>
        /// Gets or sets the color used to draw white squares.
        /// </summary>
        [DefaultValue(typeof(Color), "#FFF5F5F5")]
        public Color WhiteSquareColor
        {
            get { return _whiteSquareColor; }
            set
            {
                if (value != _whiteSquareColor)
                {
                    _whiteSquareColor = value;
                    Invalidate();
                }
            }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Removes all pieces from the board.
        /// </summary>
        public void ClearBoard()
        {
            ChessEngine.Clear();
            Invalidate();
        }

        /// <summary>
        /// Flip the board. If Blacks were on top, they will be on bottom and vice versa.
        /// </summary>
        public void FlipBoard()
        {
            BoardDirection = (BoardDirection == BoardDirection.BlackOnTop ? BoardDirection.WhiteOnTop : BoardDirection.BlackOnTop);
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
        private Bitmap GetPieceImage(ChessPiece piece)
        {
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
        /// Moves a piece from one square to another square.
        /// </summary>
        /// <returns>Returns the captured piece if any.</returns>
        /// <param name="from">Coordinates of the square where the piece move from.</param>
        /// <param name="to">Coordinates of the square where the piece move to.</param>
        /// <exception cref="Exceptions.InvalidCoordinatesException">Thrown if there is no piece on the <paramref name="from"/> coordinates.</exception>
        public void MovePiece(ChessSquare from, ChessSquare to)
        {
            var moveValidation = ChessEngine.GetMoveValidity(from, to);
            if (moveValidation.IsValid)
            {
                ChessEngine.Move(from, to);
                RedrawSquare(from);
                RedrawSquare(to);
            }
        }

        private void RedrawSquare(ChessSquare targetedSquare)
        {
            var g = this.CreateGraphics();
            var blackSquareBrush = new Pen(BlackSquareColor).Brush;
            var whiteSquareBrush = new Pen(WhiteSquareColor).Brush;
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
                if (DragDropOperation.Origin == null || !DragDropOperation.Origin.Equals(targetedSquare))
                {
                    g.DrawImage(GetPieceImage(currentPiece), square);
                }
            }

            //  Draw borders
            var borders = new Rectangle(0, 0, Width - 1, Height - 1);
            g.DrawRectangle(new Pen(Color.Black), borders);

            g.Flush();
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
        /// <param name="squareCoordinate">Coordinates of the square where to set the piece.</param>
        /// <param name="piece">Piece to set.</param>
        public void SetPieceAt(ChessSquare squareCoordinate, ChessPieceKind piece)
        {
            // ChessEngine.SetPieceAt(squareCoordinate, piece);
        }

        /// <summary>
        /// Removes all pieces and add Black and White pieces into their initial position.
        /// </summary>
        public void SetupInitialPosition()
        {
            ChessEngine.LoadFEN(Board.DEFAULT_FEN_POSITION);
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            this.SuspendLayout();
            var g = pe.Graphics;
            var coordinateAreaBrush = new Pen(CoordinateAreaBackColor).Brush;
            var blackSquareBrush = new Pen(BlackSquareColor).Brush;
            var whiteSquareBrush = new Pen(WhiteSquareColor).Brush;

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
            g.FillRectangle(ChessEngine.Turn == ChessColor.White ? whiteSquareBrush : blackSquareBrush, turnIndicatorSquare);
            g.DrawRectangle(new Pen(Color.Black), turnIndicatorBorder);

            //  Draw cells
            bool isWhiteSquare = true;
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    var square = new RectangleF(DigitAreaWidth + x * SquareWidth, y * SquareHeight, SquareWidth, SquareHeight);
                    g.FillRectangle(isWhiteSquare ? whiteSquareBrush : blackSquareBrush, square);
                    ChessSquare currentSquare = BoardDirection == BoardDirection.BlackOnTop ? new ChessSquare((ChessFile)x, (ChessRank)7 - y) : new ChessSquare((ChessFile)7 - x, (ChessRank)y);
                    ChessPiece currentPiece = ChessEngine.GetPieceAt(currentSquare);
                    if (currentPiece != null)
                    {
                        g.DrawImage(GetPieceImage(currentPiece), square);
                    }

                    isWhiteSquare = !isWhiteSquare;
                }
                isWhiteSquare = !isWhiteSquare;
            }

            //  Draw borders
            var borders = new Rectangle(0, 0, Width - 1, Height - 1);
            g.DrawRectangle(new Pen(Color.Black), borders);

            g.Flush();
            this.ResumeLayout();
        }

        #endregion Methods

        #region Delegates & Events

        public delegate void SelectedSquareEventHandler(Chessboard sender, ChessSquare from, ChessPieceKind selectedPiece);
        [Description("Fired when the user presses the left button of the mouse on a square.")]
        public event SelectedSquareEventHandler OnSquareSelected;

        public delegate void PieceMovedEventHandler(Chessboard sender, ChessSquare from, ChessSquare to, ChessPieceKind movedPiece, ChessPieceKind capturedPiece);
        [Description("Fired when the user moves a piece on the board.")]
        public event PieceMovedEventHandler OnPieceMoved;

        public delegate void PieceRemovedEventHandler(Chessboard sender, ChessSquare from, ChessPieceKind removedPiece, Point dropPoint);
        [Description("Fired when the user drags and drop a piece from the board to a location outside of the board.")]
        public event PieceRemovedEventHandler OnPieceRemoved;

        [Description("Fired when the user releases the left button of the mouse on a square.")]
        public event EventHandler OnSquareUnselected;

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
                    var resizedBitmap = new Bitmap(GetPieceImage(selectedPiece), new Size(SquareWidth, SquareHeight));
                    Cursor = new Cursor(resizedBitmap.GetHicon());
                    DragDropOperation = new DragOperation(selectedPiece.Kind, origin);
                    RedrawSquare(origin);
                    OnSquareSelected?.Invoke(this, origin, selectedPiece.Kind);
                }
            }
        }

        private void Chessboard_MouseUp(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Default;
            if (DragDropOperation.DraggedPiece != ChessPieceKind.None)
            {
                if (e.X > DigitAreaWidth && e.X < this.Width && e.Y < Height - LetterAreaHeight && e.Y > 0)
                {
                    //  Drops a piece on the board
                    var destinationSquare = (BoardDirection == BoardDirection.BlackOnTop ?
                        new ChessSquare((ChessFile)((e.X - DigitAreaWidth) / SquareWidth), (ChessRank)(7 - (e.Y / SquareHeight))) :
                        new ChessSquare((ChessFile)(7 - (e.X - DigitAreaWidth) / SquareWidth), (ChessRank)(e.Y / SquareHeight)));
                    var moveValidation = ChessEngine.GetMoveValidity(DragDropOperation.Origin, destinationSquare);
                    if (moveValidation.IsValid)
                    {
                        ChessEngine.Move(moveValidation);
                        Invalidate();
                        OnPieceMoved?.Invoke(this, DragDropOperation.Origin, destinationSquare, DragDropOperation.DraggedPiece, moveValidation.CapturedPiece);
                    }
                    else
                    {
                        Invalidate();
                    }
                }
                else
                {
                    //  Drops a piece outside of the board
                    RedrawSquare(DragDropOperation.Origin);
                    OnPieceRemoved?.Invoke(this, DragDropOperation.Origin, DragDropOperation.DraggedPiece, e.Location);
                }
            }
            DragDropOperation = new DragOperation(ChessPieceKind.None, null);
            OnSquareUnselected?.Invoke(this, new EventArgs());
        }

        private void Chessboard_Resize(object sender, EventArgs e)
        {
            Invalidate();
        }

        #endregion Events
    }
}
