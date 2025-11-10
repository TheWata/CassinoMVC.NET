using System.Windows.Forms;
using System.Drawing;

namespace CassinoMVC.Views.Jogos
{
    partial class BlackJack
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
            lblFichas = new Label();
            txtFichas = new TextBox();
            lblAposta = new Label();
            numAposta = new NumericUpDown();
            btnIniciar = new Button();
            flowJogador = new FlowLayoutPanel();
            flowDealer = new FlowLayoutPanel();
            lblJogador = new Label();
            lblDealer = new Label();
            btnHit = new Button();
            btnStay = new Button();
            btnDouble = new Button();
            lblStatus = new Label();
            lblPontosJogador = new Label();
            lblPontosDealer = new Label();
            ((System.ComponentModel.ISupportInitialize)numAposta).BeginInit();
            SuspendLayout();
            // lblFichas
            lblFichas.AutoSize = true;
            lblFichas.Location = new Point(12, 9);
            lblFichas.Name = "lblFichas";
            lblFichas.Size = new Size(40, 15);
            lblFichas.TabIndex = 0;
            lblFichas.Text = "Fichas";
            // txtFichas
            txtFichas.Location = new Point(12, 27);
            txtFichas.Name = "txtFichas";
            txtFichas.ReadOnly = true;
            txtFichas.Size = new Size(100, 23);
            txtFichas.TabIndex = 1;
            // lblAposta
            lblAposta.AutoSize = true;
            lblAposta.Location = new Point(130, 9);
            lblAposta.Name = "lblAposta";
            lblAposta.Size = new Size(44, 15);
            lblAposta.TabIndex = 2;
            lblAposta.Text = "Aposta";
            // numAposta
            numAposta.Location = new Point(130, 27);
            numAposta.Minimum = new decimal(new int[] {1,0,0,0});
            numAposta.Maximum = new decimal(new int[] {1000000,0,0,0});
            numAposta.Name = "numAposta";
            numAposta.Size = new Size(100, 23);
            numAposta.TabIndex = 3;
            numAposta.Value = new decimal(new int[] {1,0,0,0});
            // btnIniciar
            btnIniciar.Location = new Point(250, 26);
            btnIniciar.Name = "btnIniciar";
            btnIniciar.Size = new Size(90, 23);
            btnIniciar.TabIndex = 4;
            btnIniciar.Text = "Iniciar";
            btnIniciar.UseVisualStyleBackColor = true;
            btnIniciar.Click += btnIniciar_Click;
            // flowJogador
            flowJogador.AutoScroll = true;
            flowJogador.BorderStyle = BorderStyle.FixedSingle;
            flowJogador.Location = new Point(12, 110);
            flowJogador.Name = "flowJogador";
            flowJogador.Size = new Size(400, 150);
            flowJogador.TabIndex = 5;
            // flowDealer
            flowDealer.AutoScroll = true;
            flowDealer.BorderStyle = BorderStyle.FixedSingle;
            flowDealer.Location = new Point(12, 300);
            flowDealer.Name = "flowDealer";
            flowDealer.Size = new Size(400, 130);
            flowDealer.TabIndex = 6;
            // lblJogador
            lblJogador.AutoSize = true;
            lblJogador.Location = new Point(12, 92);
            lblJogador.Name = "lblJogador";
            lblJogador.Size = new Size(51, 15);
            lblJogador.TabIndex = 7;
            lblJogador.Text = "Jogador";
            // lblDealer
            lblDealer.AutoSize = true;
            lblDealer.Location = new Point(12, 282);
            lblDealer.Name = "lblDealer";
            lblDealer.Size = new Size(41, 15);
            lblDealer.TabIndex = 8;
            lblDealer.Text = "Dealer";
            // btnHit
            btnHit.Location = new Point(430, 110);
            btnHit.Name = "btnHit";
            btnHit.Size = new Size(90, 30);
            btnHit.TabIndex = 9;
            btnHit.Text = "Hit";
            btnHit.UseVisualStyleBackColor = true;
            btnHit.Click += btnHit_Click;
            // btnStay
            btnStay.Location = new Point(430, 150);
            btnStay.Name = "btnStay";
            btnStay.Size = new Size(90, 30);
            btnStay.TabIndex = 10;
            btnStay.Text = "Stay";
            btnStay.UseVisualStyleBackColor = true;
            btnStay.Click += btnStay_Click;
            // btnDouble
            btnDouble.Location = new Point(430, 190);
            btnDouble.Name = "btnDouble";
            btnDouble.Size = new Size(90, 30);
            btnDouble.TabIndex = 11;
            btnDouble.Text = "Double";
            btnDouble.UseVisualStyleBackColor = true;
            btnDouble.Click += btnDouble_Click;
            // lblStatus
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(430, 235);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(42, 15);
            lblStatus.TabIndex = 12;
            lblStatus.Text = "Status";
            // lblPontosJogador
            lblPontosJogador.AutoSize = true;
            lblPontosJogador.Location = new Point(80, 92);
            lblPontosJogador.Name = "lblPontosJogador";
            lblPontosJogador.Size = new Size(62, 15);
            lblPontosJogador.TabIndex = 13;
            lblPontosJogador.Text = "Pontos: 0";
            // lblPontosDealer
            lblPontosDealer.AutoSize = true;
            lblPontosDealer.Location = new Point(70, 282);
            lblPontosDealer.Name = "lblPontosDealer";
            lblPontosDealer.Size = new Size(62, 15);
            lblPontosDealer.TabIndex = 14;
            lblPontosDealer.Text = "Pontos: 0";
            // BlackJack
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(550, 450);
            Controls.Add(lblPontosDealer);
            Controls.Add(lblPontosJogador);
            Controls.Add(lblStatus);
            Controls.Add(btnDouble);
            Controls.Add(btnStay);
            Controls.Add(btnHit);
            Controls.Add(lblDealer);
            Controls.Add(lblJogador);
            Controls.Add(flowDealer);
            Controls.Add(flowJogador);
            Controls.Add(btnIniciar);
            Controls.Add(numAposta);
            Controls.Add(lblAposta);
            Controls.Add(txtFichas);
            Controls.Add(lblFichas);
            Name = "BlackJack";
            Text = "BlackJack";
            ((System.ComponentModel.ISupportInitialize)numAposta).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblFichas;
        private TextBox txtFichas;
        private Label lblAposta;
        private NumericUpDown numAposta;
        private Button btnIniciar;
        private FlowLayoutPanel flowJogador;
        private FlowLayoutPanel flowDealer;
        private Label lblJogador;
        private Label lblDealer;
        private Button btnHit;
        private Button btnStay;
        private Button btnDouble;
        private Label lblStatus;
        private Label lblPontosJogador;
        private Label lblPontosDealer;
    }
}