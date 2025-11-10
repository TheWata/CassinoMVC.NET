using System.Windows.Forms;
using System.Drawing;

namespace CassinoMVC.Views
{
    partial class Registro
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
            btnCadastrarRegistro = new Button();
            btnCancelarRegistro = new Button();
            lblRegistroUsuario = new Label();
            label1 = new Label();
            txtRegistroUsuario = new TextBox();
            txtRegistroSenha = new TextBox();
            txtRegistroNome = new TextBox();
            lblRegistroNome = new Label();
            SuspendLayout();
            // 
            // btnCadastrarRegistro
            // 
            btnCadastrarRegistro.Location = new Point(220, 361);
            btnCadastrarRegistro.Name = "btnCadastrarRegistro";
            btnCadastrarRegistro.Size = new Size(155, 41);
            btnCadastrarRegistro.TabIndex = 0;
            btnCadastrarRegistro.Text = "Registro";
            btnCadastrarRegistro.UseVisualStyleBackColor = true;
            btnCadastrarRegistro.Click += btnCadastrarRegistro_Click;
            // 
            // btnCancelarRegistro
            // 
            btnCancelarRegistro.Location = new Point(12, 361);
            btnCancelarRegistro.Name = "btnCancelarRegistro";
            btnCancelarRegistro.Size = new Size(155, 41);
            btnCancelarRegistro.TabIndex = 1;
            btnCancelarRegistro.Text = "Cancelar";
            btnCancelarRegistro.UseVisualStyleBackColor = true;
            btnCancelarRegistro.Click += btnCancelarRegistro_Click;
            // 
            // lblRegistroUsuario
            // 
            lblRegistroUsuario.AutoSize = true;
            lblRegistroUsuario.Location = new Point(12, 65);
            lblRegistroUsuario.Name = "lblRegistroUsuario";
            lblRegistroUsuario.Size = new Size(47, 15);
            lblRegistroUsuario.TabIndex = 2;
            lblRegistroUsuario.Text = "Usuário";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 115);
            label1.Name = "label1";
            label1.Size = new Size(39, 15);
            label1.TabIndex = 3;
            label1.Text = "Senha";
            // 
            // txtRegistroUsuario
            // 
            txtRegistroUsuario.Location = new Point(12, 83);
            txtRegistroUsuario.Name = "txtRegistroUsuario";
            txtRegistroUsuario.Size = new Size(100, 23);
            txtRegistroUsuario.TabIndex = 4;
            // 
            // txtRegistroSenha
            // 
            txtRegistroSenha.Location = new Point(12, 133);
            txtRegistroSenha.Name = "txtRegistroSenha";
            txtRegistroSenha.Size = new Size(100, 23);
            txtRegistroSenha.TabIndex = 5;
            // 
            // txtRegistroNome
            // 
            txtRegistroNome.Location = new Point(12, 39);
            txtRegistroNome.Name = "txtRegistroNome";
            txtRegistroNome.Size = new Size(100, 23);
            txtRegistroNome.TabIndex = 6;
            // 
            // lblRegistroNome
            // 
            lblRegistroNome.AutoSize = true;
            lblRegistroNome.Location = new Point(12, 21);
            lblRegistroNome.Name = "lblRegistroNome";
            lblRegistroNome.Size = new Size(96, 15);
            lblRegistroNome.TabIndex = 7;
            lblRegistroNome.Text = "Nome Completo";
            // 
            // Registro
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(387, 414);
            Controls.Add(lblRegistroNome);
            Controls.Add(txtRegistroNome);
            Controls.Add(txtRegistroSenha);
            Controls.Add(txtRegistroUsuario);
            Controls.Add(label1);
            Controls.Add(lblRegistroUsuario);
            Controls.Add(btnCancelarRegistro);
            Controls.Add(btnCadastrarRegistro);
            Name = "Registro";
            Text = "Registro";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnCadastrarRegistro;
        private Button btnCancelarRegistro;
        private Label lblRegistroUsuario;
        private Label label1;
        private TextBox txtRegistroUsuario;
        private TextBox txtRegistroSenha;
        private TextBox txtRegistroNome;
        private Label lblRegistroNome;
    }
}