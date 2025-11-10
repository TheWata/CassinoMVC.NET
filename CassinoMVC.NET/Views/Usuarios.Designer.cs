using System.Windows.Forms;
using System.Drawing;

namespace CassinoMVC.Views
{
    partial class Usuarios
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
            this.lstUsuarios = new System.Windows.Forms.ListBox();
            this.txtUsuarios = new System.Windows.Forms.TextBox();
            this.btnPesquisar = new System.Windows.Forms.Button();
            this.btnRemover = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnAdicionar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lstUsuarios
            // 
            this.lstUsuarios.FormattingEnabled = true;
            this.lstUsuarios.ItemHeight = 15;
            this.lstUsuarios.Location = new System.Drawing.Point(12, 55);
            this.lstUsuarios.Name = "lstUsuarios";
            this.lstUsuarios.Size = new System.Drawing.Size(356, 154);
            this.lstUsuarios.TabIndex = 0;
            // 
            // txtUsuarios
            // 
            this.txtUsuarios.Location = new System.Drawing.Point(12, 26);
            this.txtUsuarios.Name = "txtUsuarios";
            this.txtUsuarios.Size = new System.Drawing.Size(275, 23);
            this.txtUsuarios.TabIndex = 1;
            // 
            // btnPesquisar
            // 
            this.btnPesquisar.Location = new System.Drawing.Point(293, 25);
            this.btnPesquisar.Name = "btnPesquisar";
            this.btnPesquisar.Size = new System.Drawing.Size(75, 23);
            this.btnPesquisar.TabIndex = 2;
            this.btnPesquisar.Text = "Pesquisar";
            this.btnPesquisar.UseVisualStyleBackColor = true;
            // 
            // btnRemover
            // 
            this.btnRemover.Location = new System.Drawing.Point(12, 228);
            this.btnRemover.Name = "btnRemover";
            this.btnRemover.Size = new System.Drawing.Size(170, 23);
            this.btnRemover.TabIndex = 3;
            this.btnRemover.Text = "Remover";
            this.btnRemover.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 15);
            this.label1.TabIndex = 4;
            this.label1.Text = "Usuário";
            // 
            // btnAdicionar
            // 
            this.btnAdicionar.Location = new System.Drawing.Point(198, 228);
            this.btnAdicionar.Name = "btnAdicionar";
            this.btnAdicionar.Size = new System.Drawing.Size(170, 23);
            this.btnAdicionar.TabIndex = 5;
            this.btnAdicionar.Text = "Adicionar";
            this.btnAdicionar.UseVisualStyleBackColor = true;
            // 
            // Usuarios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(380, 263);
            this.Controls.Add(this.btnAdicionar);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnRemover);
            this.Controls.Add(this.btnPesquisar);
            this.Controls.Add(this.txtUsuarios);
            this.Controls.Add(this.lstUsuarios);
            this.Name = "Usuarios";
            this.Text = "Usuarios";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.ListBox lstUsuarios;
        private System.Windows.Forms.TextBox txtUsuarios;
        private System.Windows.Forms.Button btnPesquisar;
        private System.Windows.Forms.Button btnRemover;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnAdicionar;
    }
}