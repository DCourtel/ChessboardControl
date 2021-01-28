# ChessboardControl
A .Net control to display a chessboard.

![Initial_Position](/Screenshots/SicilianDefense.gif)

The control displays a set of chess pieces and allows to move these pieces in respect of chess rules. It can show available moves once a piece has been selected. Take a look at the «Chessboard Tester» project to have an idea of how to use the control.

* 01/28/21: Implement == and != operators. Prevents from picking a piece of the opposite color than the side whose is the turn.
* 01/27/21: Gray out and disable the control when the Enabled property is set to false.
* 01/18/21: Added a method to convert a ChessMove into its Standard Algebraic Notation (SAN), added a button onto Chessboard Tester to undo moves.
* 01/17/21: Added public methods, properties, and events.
* 01/14/21: Added moves validation, visual hints to show what squares can be reached, pawn promotion, and an indicator on the bottom left to show who’s turn it is.
* 01/02/21: First release.


Based on the work of dayjur (https://github.com/dayjur/Chess.cs) and Jeff Hlywa (https://github.com/jhlywa/chess.js)
