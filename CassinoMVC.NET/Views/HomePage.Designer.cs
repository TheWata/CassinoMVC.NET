using System.Windows.Forms;
using System.Drawing;

namespace CassinoMVC.Views
{
    partial class HomePage
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
            btnRoleta = new Button();
            btnBlackJack = new Button();
            btnSlots = new Button();
            lblPerfil = new Label();
            txtFichas = new TextBox();
            button1 = new Button();
            btnRegistros = new Button();
            btnUsuarios = new Button();
            txtUsuário = new TextBox();
            btnEditarUsuario = new Button();
            SuspendLayout();
            // 
            // btnRoleta
            // 
            btnRoleta.Location = new Point(12, 211);
            btnRoleta.Name = "btnRoleta";
            btnRoleta.Size = new Size(119, 23);
            btnRoleta.TabIndex = 0;
            btnRoleta.Text = "Roleta";
            btnRoleta.UseVisualStyleBackColor = true;
            // 
            // btnBlackJack
            // 
            btnBlackJack.Location = new Point(12, 182);
            btnBlackJack.Name = "btnBlackJack";
            btnBlackJack.Size = new Size(119, 23);
            btnBlackJack.TabIndex = 1;
            btnBlackJack.Text = "BlackJack";
            btnBlackJack.UseVisualStyleBackColor = true;
            // 
            // btnSlots
            // 
            btnSlots.Location = new Point(12, 240);
            btnSlots.Name = "btnSlots";
            btnSlots.Size = new Size(119, 23);
            btnSlots.TabIndex = 2;
            btnSlots.Text = "Slots";
            btnSlots.UseVisualStyleBackColor = true;
            // 
            // lblPerfil
            // 
            lblPerfil.AutoSize = true;
            lblPerfil.Location = new Point(12, 9);
            lblPerfil.Name = "lblPerfil";
            lblPerfil.Size = new Size(47, 15);
            lblPerfil.TabIndex = 3;
            lblPerfil.Text = "Usuário";
            // 
            // txtFichas
            // 
            txtFichas.Location = new Point(12, 56);
            txtFichas.Name = "txtFichas";
            txtFichas.ReadOnly = true;
            txtFichas.Size = new Size(100, 23);
            txtFichas.TabIndex = 4;
            // 
            // button1
            // 
            button1.Location = new Point(118, 55);
            button1.Name = "button1";
            button1.Size = new Size(120, 23);
            button1.TabIndex = 5;
            button1.Text = "Comprar Fichas";
            button1.UseVisualStyleBackColor = true;
            // 
            // btnRegistros
            // 
            btnRegistros.Location = new Point(326, 211);
            btnRegistros.Name = "btnRegistros";
            btnRegistros.Size = new Size(75, 23);
            btnRegistros.TabIndex = 6;
            btnRegistros.Text = "Registros";
            btnRegistros.UseVisualStyleBackColor = true;
            // 
            // btnUsuarios
            // 
            btnUsuarios.Location = new Point(326, 240);
            btnUsuarios.Name = "btnUsuarios";
            btnUsuarios.Size = new Size(75, 23);
            btnUsuarios.TabIndex = 7;
            btnUsuarios.Text = "Usuários";
            btnUsuarios.UseVisualStyleBackColor = true;
            // 
            // txtUsuário
            // 
            txtUsuário.Location = new Point(12, 27);
            txtUsuário.Name = "txtUsuário";
            txtUsuário.ReadOnly = true;
            txtUsuário.Size = new Size(100, 23);
            txtUsuário.TabIndex = 8;
            // 
            // btnEditarUsuario
            // 
            btnEditarUsuario.Location = new Point(118, 27);
            btnEditarUsuario.Name = "btnEditarUsuario";
            btnEditarUsuario.Size = new Size(120, 23);
            btnEditarUsuario.TabIndex = 9;
            btnEditarUsuario.Text = "Editar Usuário";
            btnEditarUsuario.UseVisualStyleBackColor = true;
            // 
            // HomePage
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(413, 282);
            Controls.Add(btnEditarUsuario);
            Controls.Add(txtUsuário);
            Controls.Add(btnUsuarios);
            Controls.Add(btnRegistros);
            Controls.Add(button1);
            Controls.Add(txtFichas);
            Controls.Add(lblPerfil);
            Controls.Add(btnSlots);
            Controls.Add(btnBlackJack);
            Controls.Add(btnRoleta);
            Name = "HomePage";
            Text = "HomePage";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnRoleta;
        private Button btnBlackJack;
        private Button btnSlots;
        private Label lblPerfil;
        private TextBox txtFichas;
        private Button button1;
        private Button btnRegistros;
        private Button btnUsuarios;
        private TextBox txtUsuário;
        private Button btnEditarUsuario;
    }
}