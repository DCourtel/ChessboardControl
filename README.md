# ChessboardControl
A .Net control to display a chessboard.

Nuget package: https://www.nuget.org/packages/Codasoft.Control.Chessboard

![Initial_Position](/Screenshots/SicilianDefense.gif)

The control displays a set of chess pieces and allows to move these pieces in respect of chess rules. It can show available moves once a piece has been selected. Take a look at the «Chessboard Tester» project to have an idea of how to use the control.

* 03/22/21: Fixed a bug where the En_Passant flag may not be reset. Add a method to convert a SAN string to a ChessMove object.
* 03/14/21: Fixed a bug where GetMoveValidity was throwing an exception when the move was illegal.
* 02/23/21: Set the GetMoveValidity method to always update the ToSAN property to the full notation (with disambiguation character if needed).
* 02/21/21: Various bug fix. Add a button to load a FEN string onto Chessboard Tester.
* 01/30/21: Fixed the Clone method of the ChessMove class and added JSON serialization properties.
* 01/28/21: Implement == and != operators. Prevents from picking a piece of the opposite color than the side whose is the turn.
* 01/27/21: Gray out and disable the control when the Enabled property is set to false.
* 01/18/21: Added a method to convert a ChessMove into its Standard Algebraic Notation (SAN), added a button onto Chessboard Tester to undo moves.
* 01/17/21: Added public methods, properties, and events.
* 01/14/21: Added moves validation, visual hints to show what squares can be reached, pawn promotion, and an indicator on the bottom left to show who’s turn it is.
* 01/02/21: First release.


Based on the work of dayjur (https://github.com/dayjur/Chess.cs) and Jeff Hlywa (https://github.com/jhlywa/chess.js)
