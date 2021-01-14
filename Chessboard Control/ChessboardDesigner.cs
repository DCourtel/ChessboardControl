using System.Windows.Forms.Design;
using System.ComponentModel.Design;

namespace ChessboardControl
{
  public  class ChessboardDesigner: ControlDesigner
    {
        private DesignerActionListCollection actionLists;

        public override DesignerActionListCollection ActionLists
        {
            get
            {
                if (actionLists == null)
                {
                    actionLists = new DesignerActionListCollection();
                    actionLists.Add(new ChessboardDesignerActionList(this.Component));
                }
                return actionLists;
            }
        }
    }
}
