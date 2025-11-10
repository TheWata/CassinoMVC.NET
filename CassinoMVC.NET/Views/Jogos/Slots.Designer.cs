using System.Windows.Forms;
using System.Drawing;

namespace CassinoMVC.Views.Jogos
{
    partial class Slots
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            txtValorAposta = new NumericUpDown();
            label1 = new Label();
            btnGirarSlot = new Button();
            txtSlots = new TextBox();
            label2 = new Label();
            lblFichas = new Label();
            txtFichas = new TextBox();
            ((System.ComponentModel.ISupportInitialize)txtValorAposta).BeginInit();
            SuspendLayout();
            // 
            // txtValorAposta
            // 
            txtValorAposta.Location = new Point(285, 311);
            txtValorAposta.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            txtValorAposta.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            txtValorAposta.Name = "txtValorAposta";
            txtValorAposta.Size = new Size(120, 23);
            txtValorAposta.TabIndex = 0;
            txtValorAposta.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(285, 293);
            label1.Name = "label1";
            label1.Size = new Size(44, 15);
            label1.TabIndex = 3;
            label1.Text = "Aposta";
            // 
            // btnGirarSlot
            // 
            btnGirarSlot.Location = new Point(434, 311);
            btnGirarSlot.Name = "btnGirarSlot";
            btnGirarSlot.Size = new Size(75, 23);
            btnGirarSlot.TabIndex = 4;
            btnGirarSlot.Text = "Girar";
            btnGirarSlot.UseVisualStyleBackColor = true;
            btnGirarSlot.Click += btnGirarSlot_Click;
            // 
            // txtSlots
            // 
            txtSlots.Location = new Point(354, 162);
            txtSlots.Name = "txtSlots";
            txtSlots.ReadOnly = true;
            txtSlots.Size = new Size(155, 23);
            txtSlots.TabIndex = 5;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(384, 144);
            label2.Name = "label2";
            label2.Size = new Size(32, 15);
            label2.TabIndex = 6;
            label2.Text = "Slots";
            // 
            // lblFichas
            // 
            lblFichas.AutoSize = true;
            lblFichas.Location = new Point(12, 10);
            lblFichas.Name = "lblFichas";
            lblFichas.Size = new Size(40, 15);
            lblFichas.TabIndex = 2;
            lblFichas.Text = "Fichas";
            // 
            // txtFichas
            // 
            txtFichas.Location = new Point(12, 28);
            txtFichas.Name = "txtFichas";
            txtFichas.ReadOnly = true;
            txtFichas.Size = new Size(100, 23);
            txtFichas.TabIndex = 1;
            // 
            // Slots
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label2);
            Controls.Add(txtSlots);
            Controls.Add(btnGirarSlot);
            Controls.Add(label1);
            Controls.Add(lblFichas);
            Controls.Add(txtFichas);
            Controls.Add(txtValorAposta);
            Name = "Slots";
            Text = "Slots";
            ((System.ComponentModel.ISupportInitialize)txtValorAposta).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private NumericUpDown txtValorAposta;
        private Label label1;
        private Button btnGirarSlot;
        private TextBox txtSlots;
        private Label label2;
        private Label lblFichas;
        private TextBox txtFichas;
    }
}