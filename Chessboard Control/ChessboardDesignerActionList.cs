using System.ComponentModel.Design;
using System.ComponentModel;
using System.Drawing;

namespace ChessboardControl
{
    public class ChessboardDesignerActionList : DesignerActionList
    {
        private Chessboard _control;

        public ChessboardDesignerActionList(IComponent component) : base(component)
        {
            _control = component as Chessboard;
        }

        private void SetPropValue(string propName, object value)
        {
            PropertyDescriptor prop = TypeDescriptor.GetProperties(_control)[propName];
            if (prop != null) { prop.SetValue(_control, value); }
        }        

        public Color DarkSquaresColor
        {
            get { return _control.DarkSquaresColor; }
            set { SetPropValue("DarkSquaresColor", value); }
        }

        public Color LightSquaresColor
        {
            get { return _control.LightSquaresColor; }
            set { SetPropValue("LightSquaresColor", value); }
        }

        public BoardDirection BoardDirection
        {
            get { return _control.BoardDirection; }
            set { SetPropValue("BoardDirection", value); }
        }

        public void Reset()
        {
            DarkSquaresColor = Color.SteelBlue;
            LightSquaresColor = Color.WhiteSmoke;
            BoardDirection = BoardDirection.BlackOnTop;
        }

        public override DesignerActionItemCollection GetSortedActionItems()
        {
            DesignerActionItemCollection items = new DesignerActionItemCollection();

            items.Add(new DesignerActionHeaderItem("Appearance"));
            items.Add(new DesignerActionPropertyItem("DarkSquaresColor", "Dark squares color", "Appearance"));
            items.Add(new DesignerActionPropertyItem("LightSquaresColor", "Light squares color", "Appearance"));
            items.Add(new DesignerActionPropertyItem("BoardDirection", "Board direction", "Appearance"));
            items.Add(new DesignerActionMethodItem(this, "Reset", "Reset settings", true));

            return items;
        }
    }
}
