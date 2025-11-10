using System.Windows.Forms;
using System.Drawing;

namespace CassinoMVC.Views
{
    partial class Inicio
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
            BtnLogin = new Button();
            txtUser = new TextBox();
            txtSenha = new TextBox();
            lblUsuario = new Label();
            lblSenha = new Label();
            btnRegistro = new Button();
            SuspendLayout();
            // 
            // BtnLogin
            // 
            BtnLogin.Location = new Point(265, 242);
            BtnLogin.Name = "BtnLogin";
            BtnLogin.Size = new Size(75, 23);
            BtnLogin.TabIndex = 0;
            BtnLogin.Text = "Login";
            BtnLogin.UseVisualStyleBackColor = true;
            BtnLogin.Click += BtnLogin_Click;
            // 
            // txtUser
            // 
            txtUser.Location = new Point(255, 169);
            txtUser.Name = "txtUser";
            txtUser.Size = new Size(188, 23);
            txtUser.TabIndex = 1;
            // 
            // txtSenha
            // 
            txtSenha.Location = new Point(255, 213);
            txtSenha.Name = "txtSenha";
            txtSenha.Size = new Size(188, 23);
            txtSenha.TabIndex = 2;
            // 
            // lblUsuario
            // 
            lblUsuario.AutoSize = true;
            lblUsuario.Location = new Point(255, 151);
            lblUsuario.Name = "lblUsuario";
            lblUsuario.Size = new Size(47, 15);
            lblUsuario.TabIndex = 3;
            lblUsuario.Text = "Usuário";
            // 
            // lblSenha
            // 
            lblSenha.AutoSize = true;
            lblSenha.Location = new Point(255, 195);
            lblSenha.Name = "lblSenha";
            lblSenha.Size = new Size(39, 15);
            lblSenha.TabIndex = 4;
            lblSenha.Text = "Senha";
            // 
            // btnRegistro
            // 
            btnRegistro.Location = new Point(356, 242);
            btnRegistro.Name = "btnRegistro";
            btnRegistro.Size = new Size(75, 23);
            btnRegistro.TabIndex = 5;
            btnRegistro.Text = "Registro";
            btnRegistro.UseVisualStyleBackColor = true;
            btnRegistro.Click += btnRegistro_Click;
            // 
            // Inicio
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(739, 453);
            Controls.Add(btnRegistro);
            Controls.Add(lblSenha);
            Controls.Add(lblUsuario);
            Controls.Add(txtSenha);
            Controls.Add(txtUser);
            Controls.Add(BtnLogin);
            Name = "Inicio";
            Text = "Inicio";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button BtnLogin;
        private TextBox txtUser;
        private TextBox txtSenha;
        private Label lblUsuario;
        private Label lblSenha;
        private Button btnRegistro;
    }
}