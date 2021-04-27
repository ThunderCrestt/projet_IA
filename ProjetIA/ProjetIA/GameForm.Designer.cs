
namespace ProjetIA
{
    partial class GameForm
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être  supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }  
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.titleLabel = new System.Windows.Forms.Label();
            this.redPawnLabel = new System.Windows.Forms.Label();
            this.yellowPawnLabel = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // titleLabel
            // 
            this.titleLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.titleLabel.BackColor = System.Drawing.SystemColors.Menu;
            this.titleLabel.Font = new System.Drawing.Font("Calibri", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titleLabel.ForeColor = System.Drawing.Color.Black;
            this.titleLabel.Location = new System.Drawing.Point(3, 0);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(456, 168);
            this.titleLabel.TabIndex = 0;
            this.titleLabel.Text = "Puissance 4";
            this.titleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // redPawnLabel
            // 
            this.redPawnLabel.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.redPawnLabel.ForeColor = System.Drawing.Color.Red;
            this.redPawnLabel.Location = new System.Drawing.Point(3, 168);
            this.redPawnLabel.Name = "redPawnLabel";
            this.redPawnLabel.Size = new System.Drawing.Size(456, 44);
            this.redPawnLabel.TabIndex = 1;
            this.redPawnLabel.Text = "Pions rouge : Joueur";
            this.redPawnLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // yellowPawnLabel
            // 
            this.yellowPawnLabel.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.yellowPawnLabel.ForeColor = System.Drawing.Color.Gold;
            this.yellowPawnLabel.Location = new System.Drawing.Point(3, 212);
            this.yellowPawnLabel.Name = "yellowPawnLabel";
            this.yellowPawnLabel.Size = new System.Drawing.Size(456, 46);
            this.yellowPawnLabel.TabIndex = 2;
            this.yellowPawnLabel.Text = "Pions jaune : Agent";
            this.yellowPawnLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.titleLabel);
            this.flowLayoutPanel1.Controls.Add(this.redPawnLabel);
            this.flowLayoutPanel1.Controls.Add(this.yellowPawnLabel);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(559, 28);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(462, 514);
            this.flowLayoutPanel1.TabIndex = 3;
            // 
            // GameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 554);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "GameForm";
            this.Text = "Puissance4";
            this.Load += new System.EventHandler(this.GameForm_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.GameForm_Paint_1);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.GameForm_MouseClick);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Label redPawnLabel;
        private System.Windows.Forms.Label yellowPawnLabel;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
    }
}

