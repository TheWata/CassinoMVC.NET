using System.Windows.Forms;
using System.Drawing;
using System;

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
            this.btnRoleta = new System.Windows.Forms.Button();
            this.btnBlackJack = new System.Windows.Forms.Button();
            this.btnSlots = new System.Windows.Forms.Button();
            this.lblPerfil = new System.Windows.Forms.Label();
            this.txtFichas = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.btnRegistros = new System.Windows.Forms.Button();
            this.btnUsuarios = new System.Windows.Forms.Button();
            this.txtUsuário = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnRoleta
            // 
            this.btnRoleta.Location = new System.Drawing.Point(10, 183);
            this.btnRoleta.Name = "btnRoleta";
            this.btnRoleta.Size = new System.Drawing.Size(102, 20);
            this.btnRoleta.TabIndex = 0;
            this.btnRoleta.Text = "Roleta";
            this.btnRoleta.UseVisualStyleBackColor = true;
            // 
            // btnBlackJack
            // 
            this.btnBlackJack.Location = new System.Drawing.Point(10, 158);
            this.btnBlackJack.Name = "btnBlackJack";
            this.btnBlackJack.Size = new System.Drawing.Size(102, 20);
            this.btnBlackJack.TabIndex = 1;
            this.btnBlackJack.Text = "BlackJack";
            this.btnBlackJack.UseVisualStyleBackColor = true;
            // 
            // btnSlots
            // 
            this.btnSlots.Location = new System.Drawing.Point(10, 208);
            this.btnSlots.Name = "btnSlots";
            this.btnSlots.Size = new System.Drawing.Size(102, 20);
            this.btnSlots.TabIndex = 2;
            this.btnSlots.Text = "Slots";
            this.btnSlots.UseVisualStyleBackColor = true;
            // 
            // lblPerfil
            // 
            this.lblPerfil.AutoSize = true;
            this.lblPerfil.Location = new System.Drawing.Point(10, 8);
            this.lblPerfil.Name = "lblPerfil";
            this.lblPerfil.Size = new System.Drawing.Size(43, 13);
            this.lblPerfil.TabIndex = 3;
            this.lblPerfil.Text = "Usuário";
            // 
            // txtFichas
            // 
            this.txtFichas.Location = new System.Drawing.Point(10, 49);
            this.txtFichas.Name = "txtFichas";
            this.txtFichas.ReadOnly = true;
            this.txtFichas.Size = new System.Drawing.Size(86, 20);
            this.txtFichas.TabIndex = 4;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(101, 48);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(103, 20);
            this.button1.TabIndex = 5;
            this.button1.Text = "Comprar Fichas";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // btnRegistros
            // 
            this.btnRegistros.Location = new System.Drawing.Point(279, 183);
            this.btnRegistros.Name = "btnRegistros";
            this.btnRegistros.Size = new System.Drawing.Size(64, 20);
            this.btnRegistros.TabIndex = 6;
            this.btnRegistros.Text = "Registros";
            this.btnRegistros.UseVisualStyleBackColor = true;
            // 
            // btnUsuarios
            // 
            this.btnUsuarios.Location = new System.Drawing.Point(279, 208);
            this.btnUsuarios.Name = "btnUsuarios";
            this.btnUsuarios.Size = new System.Drawing.Size(64, 20);
            this.btnUsuarios.TabIndex = 7;
            this.btnUsuarios.Text = "Usuários";
            this.btnUsuarios.UseVisualStyleBackColor = true;
            // 
            // txtUsuário
            // 
            this.txtUsuário.Location = new System.Drawing.Point(10, 23);
            this.txtUsuário.Name = "txtUsuário";
            this.txtUsuário.ReadOnly = true;
            this.txtUsuário.Size = new System.Drawing.Size(86, 20);
            this.txtUsuário.TabIndex = 8;
            // 
            // HomePage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(354, 244);
            this.Controls.Add(this.txtUsuário);
            this.Controls.Add(this.btnUsuarios);
            this.Controls.Add(this.btnRegistros);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtFichas);
            this.Controls.Add(this.lblPerfil);
            this.Controls.Add(this.btnSlots);
            this.Controls.Add(this.btnBlackJack);
            this.Controls.Add(this.btnRoleta);
            this.Name = "HomePage";
            this.Text = "HomePage";
            this.Load += new System.EventHandler(this.HomePage_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

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
    }
}