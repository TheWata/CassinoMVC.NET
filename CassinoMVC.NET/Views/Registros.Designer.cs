using System.Windows.Forms;
using System.Drawing;

namespace CassinoMVC.Views
{
    partial class Registros
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
            lstRegistros = new ListBox();
            btnConsultarRegistro = new Button();
            txtRegistrosUsuario = new TextBox();
            label1 = new Label();
            cbJogo = new ComboBox();
            label2 = new Label();
            SuspendLayout();
            // 
            // lstRegistros
            // 
            lstRegistros.FormattingEnabled = true;
            lstRegistros.ItemHeight = 15;
            lstRegistros.Location = new Point(12, 59);
            lstRegistros.Name = "lstRegistros";
            lstRegistros.Size = new Size(691, 364);
            lstRegistros.TabIndex = 0;
            // 
            // btnConsultarRegistro
            // 
            btnConsultarRegistro.Location = new Point(628, 30);
            btnConsultarRegistro.Name = "btnConsultarRegistro";
            btnConsultarRegistro.Size = new Size(75, 23);
            btnConsultarRegistro.TabIndex = 1;
            btnConsultarRegistro.Text = "Consultar";
            btnConsultarRegistro.UseVisualStyleBackColor = true;
            // 
            // txtRegistrosUsuario
            // 
            txtRegistrosUsuario.Location = new Point(12, 31);
            txtRegistrosUsuario.Name = "txtRegistrosUsuario";
            txtRegistrosUsuario.Size = new Size(161, 23);
            txtRegistrosUsuario.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 12);
            label1.Name = "label1";
            label1.Size = new Size(55, 15);
            label1.TabIndex = 3;
            label1.Text = "Registros";
            // 
            // cbJogo
            // 
            cbJogo.FormattingEnabled = true;
            cbJogo.Location = new Point(179, 30);
            cbJogo.Name = "cbJogo";
            cbJogo.Size = new Size(121, 23);
            cbJogo.TabIndex = 4;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(179, 12);
            label2.Name = "label2";
            label2.Size = new Size(32, 15);
            label2.TabIndex = 5;
            label2.Text = "Jogo";
            // 
            // Registros
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(715, 431);
            Controls.Add(label2);
            Controls.Add(cbJogo);
            Controls.Add(label1);
            Controls.Add(txtRegistrosUsuario);
            Controls.Add(btnConsultarRegistro);
            Controls.Add(lstRegistros);
            Name = "Registros";
            Text = "Registros";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox lstRegistros;
        private Button button1;
        private TextBox txtRegistrosUsuario;
        private Label label1;
        private ComboBox cbJogo;
        private Label label2;
        private Button btnConsultarRegistro;
    }
}