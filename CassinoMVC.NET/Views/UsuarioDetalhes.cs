using System;
using System.ComponentModel;
using System.Windows.Forms;
using CassinoMVC.Controllers;

namespace CassinoMVC.Views
{
    public class UsuarioDetalhes : Form
    {
        private Label lblUsuario;
        private Label lblNome;
        private Label lblCargo;
        private Label lblFichas;
        private Button btnFechar;

        private readonly UsuariosController _controller = new UsuariosController();
        private int _idUsuario;

        // Construtor sem parâmetros para o Designer
        public UsuarioDetalhes()
        {
            _idUsuario = 0;
            InitializeComponent();
            if (!IsDesignMode() && _idUsuario > 0)
                Carregar();
        }

        public UsuarioDetalhes(int idUsuario)
        {
            _idUsuario = idUsuario;
            InitializeComponent();
            if (!IsDesignMode())
                Carregar();
        }

        private static bool IsDesignMode()
        {
            return LicenseManager.UsageMode == LicenseUsageMode.Designtime;
        }

        private void InitializeComponent()
        {
            Text = "Detalhes do Usuário";
            Width = 360;
            Height = 220;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            StartPosition = FormStartPosition.CenterParent;

            lblUsuario = new Label { Left = 12, Top = 20, AutoSize = true };
            lblNome = new Label { Left = 12, Top = 50, AutoSize = true };
            lblCargo = new Label { Left = 12, Top = 80, AutoSize = true };
            lblFichas = new Label { Left = 12, Top = 110, AutoSize = true };

            btnFechar = new Button { Left = 240, Top = 140, Width = 90, Text = "Fechar" };
            btnFechar.Click += (s, e) => Close();

            Controls.Add(lblUsuario);
            Controls.Add(lblNome);
            Controls.Add(lblCargo);
            Controls.Add(lblFichas);
            Controls.Add(btnFechar);
        }

        private void Carregar()
        {
            var u = _controller.ObterPorId(_idUsuario);
            if (u == null)
            {
                MessageBox.Show("Usuário não encontrado");
                Close();
                return;
            }

            lblUsuario.Text = string.Format("Usuário: {0} (ID:{1})", u.NomeUsuario, u.IdUsuario);
            lblNome.Text = string.Format("Nome: {0}", u.NomeCompleto);
            lblCargo.Text = string.Format("Cargo: {0}", u.Cargo);
            lblFichas.Text = string.Format("Fichas: {0:F2}", _controller.ObterSaldoFichas(u.IdUsuario));
        }
    }
}
