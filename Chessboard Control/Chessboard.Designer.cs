
namespace ChessboardControl
{
    partial class Chessboard
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur de composants

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // Chessboard
            // 
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Chessboard_MouseDown);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Chessboard_MouseUp);
            this.Resize += new System.EventHandler(this.Chessboard_Resize);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
