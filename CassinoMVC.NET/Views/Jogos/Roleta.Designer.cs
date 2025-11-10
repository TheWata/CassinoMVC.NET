using System.Windows.Forms;
using System.Drawing;

namespace CassinoMVC.Views.Jogos
{
    partial class Roleta
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
            labelAposta = new Label();
            txtValorAposta = new NumericUpDown();
            btnApostar = new Button();
            listNumeros = new ListView();
            colNumero = new ColumnHeader();
            colCor = new ColumnHeader();
            groupApostasExternas = new GroupBox();
            rb25_36 = new RadioButton();
            rb13_24 = new RadioButton();
            rb1_12 = new RadioButton();
            rbImpar = new RadioButton();
            rbPar = new RadioButton();
            rbPreto = new RadioButton();
            rbVermelho = new RadioButton();
            lblModo = new Label();
            lblResultado = new Label();
            lstHistoricoRoleta = new ListBox();
            ((System.ComponentModel.ISupportInitialize)txtValorAposta).BeginInit();
            groupApostasExternas.SuspendLayout();
            SuspendLayout();
            // 
            // lblFichas
            // 
            lblFichas.AutoSize = true;
            lblFichas.Location = new Point(12, 9);
            lblFichas.Name = "lblFichas";
            lblFichas.Size = new Size(40, 15);
            lblFichas.TabIndex = 0;
            lblFichas.Text = "Fichas";
            // 
            // txtFichas
            // 
            txtFichas.Location = new Point(12, 27);
            txtFichas.Name = "txtFichas";
            txtFichas.ReadOnly = true;
            txtFichas.Size = new Size(120, 23);
            txtFichas.TabIndex = 1;
            // 
            // labelAposta
            // 
            labelAposta.AutoSize = true;
            labelAposta.Location = new Point(12, 109);
            labelAposta.Name = "labelAposta";
            labelAposta.Size = new Size(44, 15);
            labelAposta.TabIndex = 2;
            labelAposta.Text = "Aposta";
            // 
            // txtValorAposta
            // 
            txtValorAposta.Location = new Point(12, 127);
            txtValorAposta.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            txtValorAposta.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            txtValorAposta.Name = "txtValorAposta";
            txtValorAposta.Size = new Size(192, 23);
            txtValorAposta.TabIndex = 3;
            txtValorAposta.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // btnApostar
            // 
            btnApostar.Location = new Point(210, 121);
            btnApostar.Name = "btnApostar";
            btnApostar.Size = new Size(180, 30);
            btnApostar.TabIndex = 4;
            btnApostar.Text = "Apostar";
            btnApostar.UseVisualStyleBackColor = true;
            btnApostar.Click += btnApostar_Click;
            // 
            // listNumeros
            // 
            listNumeros.CheckBoxes = true;
            listNumeros.Columns.AddRange(new ColumnHeader[] { colNumero, colCor });
            listNumeros.FullRowSelect = true;
            listNumeros.Location = new Point(12, 171);
            listNumeros.Name = "listNumeros";
            listNumeros.Size = new Size(192, 365);
            listNumeros.TabIndex = 5;
            listNumeros.UseCompatibleStateImageBehavior = false;
            listNumeros.View = View.Details;
            listNumeros.ItemChecked += listNumeros_ItemChecked;
            // 
            // colNumero
            // 
            colNumero.Text = "Número";
            // 
            // colCor
            // 
            colCor.Text = "Cor";
            // 
            // groupApostasExternas
            // 
            groupApostasExternas.Controls.Add(rb25_36);
            groupApostasExternas.Controls.Add(rb13_24);
            groupApostasExternas.Controls.Add(rb1_12);
            groupApostasExternas.Controls.Add(rbImpar);
            groupApostasExternas.Controls.Add(rbPar);
            groupApostasExternas.Controls.Add(rbPreto);
            groupApostasExternas.Controls.Add(rbVermelho);
            groupApostasExternas.Location = new Point(210, 156);
            groupApostasExternas.Name = "groupApostasExternas";
            groupApostasExternas.Size = new Size(180, 210);
            groupApostasExternas.TabIndex = 6;
            groupApostasExternas.TabStop = false;
            groupApostasExternas.Text = "Aposta Externa";
            // 
            // rb25_36
            // 
            rb25_36.AutoSize = true;
            rb25_36.Location = new Point(10, 180);
            rb25_36.Name = "rb25_36";
            rb25_36.Size = new Size(54, 19);
            rb25_36.TabIndex = 6;
            rb25_36.TabStop = true;
            rb25_36.Text = "25-36";
            rb25_36.UseVisualStyleBackColor = true;
            // 
            // rb13_24
            // 
            rb13_24.AutoSize = true;
            rb13_24.Location = new Point(10, 155);
            rb13_24.Name = "rb13_24";
            rb13_24.Size = new Size(54, 19);
            rb13_24.TabIndex = 5;
            rb13_24.TabStop = true;
            rb13_24.Text = "13-24";
            rb13_24.UseVisualStyleBackColor = true;
            // 
            // rb1_12
            // 
            rb1_12.AutoSize = true;
            rb1_12.Location = new Point(10, 130);
            rb1_12.Name = "rb1_12";
            rb1_12.Size = new Size(48, 19);
            rb1_12.TabIndex = 4;
            rb1_12.TabStop = true;
            rb1_12.Text = "1-12";
            rb1_12.UseVisualStyleBackColor = true;
            // 
            // rbImpar
            // 
            rbImpar.AutoSize = true;
            rbImpar.Location = new Point(10, 105);
            rbImpar.Name = "rbImpar";
            rbImpar.Size = new Size(56, 19);
            rbImpar.TabIndex = 3;
            rbImpar.TabStop = true;
            rbImpar.Text = "Ímpar";
            rbImpar.UseVisualStyleBackColor = true;
            // 
            // rbPar
            // 
            rbPar.AutoSize = true;
            rbPar.Location = new Point(10, 80);
            rbPar.Name = "rbPar";
            rbPar.Size = new Size(42, 19);
            rbPar.TabIndex = 2;
            rbPar.TabStop = true;
            rbPar.Text = "Par";
            rbPar.UseVisualStyleBackColor = true;
            // 
            // rbPreto
            // 
            rbPreto.AutoSize = true;
            rbPreto.Location = new Point(10, 55);
            rbPreto.Name = "rbPreto";
            rbPreto.Size = new Size(53, 19);
            rbPreto.TabIndex = 1;
            rbPreto.TabStop = true;
            rbPreto.Text = "Preto";
            rbPreto.UseVisualStyleBackColor = true;
            // 
            // rbVermelho
            // 
            rbVermelho.AutoSize = true;
            rbVermelho.Location = new Point(10, 30);
            rbVermelho.Name = "rbVermelho";
            rbVermelho.Size = new Size(75, 19);
            rbVermelho.TabIndex = 0;
            rbVermelho.TabStop = true;
            rbVermelho.Text = "Vermelho";
            rbVermelho.UseVisualStyleBackColor = true;
            // 
            // lblModo
            // 
            lblModo.AutoSize = true;
            lblModo.Location = new Point(12, 153);
            lblModo.Name = "lblModo";
            lblModo.Size = new Size(84, 15);
            lblModo.TabIndex = 7;
            lblModo.Text = "Aposta Interna";
            // 
            // lblResultado
            // 
            lblResultado.AutoSize = true;
            lblResultado.Location = new Point(12, 75);
            lblResultado.Name = "lblResultado";
            lblResultado.Size = new Size(59, 15);
            lblResultado.TabIndex = 8;
            lblResultado.Text = "Resultado";
            // 
            // lstHistoricoRoleta
            // 
            lstHistoricoRoleta.FormattingEnabled = true;
            lstHistoricoRoleta.ItemHeight = 15;
            lstHistoricoRoleta.Location = new Point(210, 367);
            lstHistoricoRoleta.Name = "lstHistoricoRoleta";
            lstHistoricoRoleta.Size = new Size(180, 169);
            lstHistoricoRoleta.TabIndex = 9;
            // 
            // Roleta
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(410, 583);
            Controls.Add(lstHistoricoRoleta);
            Controls.Add(lblResultado);
            Controls.Add(lblModo);
            Controls.Add(groupApostasExternas);
            Controls.Add(listNumeros);
            Controls.Add(btnApostar);
            Controls.Add(txtValorAposta);
            Controls.Add(labelAposta);
            Controls.Add(txtFichas);
            Controls.Add(lblFichas);
            Name = "Roleta";
            Text = "Roleta";
            Load += Roleta_Load;
            ((System.ComponentModel.ISupportInitialize)txtValorAposta).EndInit();
            groupApostasExternas.ResumeLayout(false);
            groupApostasExternas.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblFichas;
        private TextBox txtFichas;
        private Label labelAposta;
        private NumericUpDown txtValorAposta;
        private Button btnApostar;
        private ListView listNumeros;
        private ColumnHeader colNumero;
        private ColumnHeader colCor;
        private GroupBox groupApostasExternas;
        private RadioButton rb25_36;
        private RadioButton rb13_24;
        private RadioButton rb1_12;
        private RadioButton rbImpar;
        private RadioButton rbPar;
        private RadioButton rbPreto;
        private RadioButton rbVermelho;
        private Label lblModo;
        private Label lblResultado;
        private ListBox lstHistoricoRoleta;
    }
}