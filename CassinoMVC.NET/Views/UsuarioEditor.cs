using System;
using System.ComponentModel;
using System.Windows.Forms;
using CassinoMVC.Controllers;

namespace CassinoMVC.Views
{
    public class UsuarioEditor : Form
    {
        private readonly UsuariosController _controller = new UsuariosController();
        private readonly int? _idUsuarioEdicao;
        private readonly CassinoMVC.Models.Usuario _usuarioLogado;

        private TextBox txtNomeCompleto;
        private TextBox txtNomeUsuario;
        private TextBox txtSenha;
        private ComboBox cbCargo;
        private NumericUpDown numSaldoInicial;
        private Label lblSaldoInicial;
        private Button btnSalvar;
        private Button btnCancelar;

        // Construtor sem parâmetros para o Designer
        public UsuarioEditor()
        {
            InitializeComponent();
            if (!IsDesignMode())
            {
                // Padrão criação: Jogador, saldo 0
                cbCargo.SelectedItem = CassinoMVC.Models.CargoUsuario.Jogador;
                if (numSaldoInicial != null) numSaldoInicial.Value = 0;
                AtualizarVisibilidadeSaldo();
            }
        }

        public UsuarioEditor(CassinoMVC.Models.Usuario usuarioLogado, int? idUsuarioEdicao = null)
        {
            _usuarioLogado = usuarioLogado;
            _idUsuarioEdicao = idUsuarioEdicao;
            InitializeComponent();
            if (!IsDesignMode())
            {
                Carregar();
            }
        }

        private static bool IsDesignMode()
        {
            return LicenseManager.UsageMode == LicenseUsageMode.Designtime;
        }

        private void InitializeComponent()
        {
            Text = (_idUsuarioEdicao.HasValue ? "Editar Usuário" : "Adicionar Usuário");
            Width = 420;
            Height = 320;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            StartPosition = FormStartPosition.CenterParent;

            var lblNome = new Label { Left = 12, Top = 20, Width = 120, Text = "Nome" };
            txtNomeCompleto = new TextBox { Left = 140, Top = 18, Width = 250 };
            var lblLogin = new Label { Left = 12, Top = 55, Width = 120, Text = "Usuário" };
            txtNomeUsuario = new TextBox { Left = 140, Top = 53, Width = 250 };
            var lblSenha = new Label { Left = 12, Top = 90, Width = 120, Text = "Senha" };
            txtSenha = new TextBox { Left = 140, Top = 88, Width = 250, UseSystemPasswordChar = true };
            var lblCargo = new Label { Left = 12, Top = 125, Width = 120, Text = "Cargo" };
            cbCargo = new ComboBox { Left = 140, Top = 123, Width = 250, DropDownStyle = ComboBoxStyle.DropDownList };
            cbCargo.Items.Add(CassinoMVC.Models.CargoUsuario.Administrador);
            cbCargo.Items.Add(CassinoMVC.Models.CargoUsuario.Jogador);
            cbCargo.SelectedIndexChanged += (s, e) => AtualizarVisibilidadeSaldo();

            lblSaldoInicial = new Label { Left = 12, Top = 160, Width = 120, Text = "Saldo inicial" };
            numSaldoInicial = new NumericUpDown { Left = 140, Top = 158, Width = 250, DecimalPlaces = 2, Maximum = 100000000, Minimum = 0 };

            btnSalvar = new Button { Left = 230, Top = 210, Width = 80, Text = "Salvar" };
            btnCancelar = new Button { Left = 320, Top = 210, Width = 80, Text = "Cancelar" };

            btnSalvar.Click += BtnSalvar_Click;
            btnCancelar.Click += (s, e) => Close();

            Controls.Add(lblNome);
            Controls.Add(txtNomeCompleto);
            Controls.Add(lblLogin);
            Controls.Add(txtNomeUsuario);
            Controls.Add(lblSenha);
            Controls.Add(txtSenha);
            Controls.Add(lblCargo);
            Controls.Add(cbCargo);
            Controls.Add(lblSaldoInicial);
            Controls.Add(numSaldoInicial);
            Controls.Add(btnSalvar);
            Controls.Add(btnCancelar);
        }

        private void AtualizarVisibilidadeSaldo()
        {
            // Evita NRE se nada selecionado no ComboBox
            bool ehJogador = false;
            if (cbCargo != null && cbCargo.SelectedItem is CassinoMVC.Models.CargoUsuario cargoSel)
            {
                ehJogador = cargoSel == CassinoMVC.Models.CargoUsuario.Jogador;
            }
            bool mostrar = ehJogador && !_idUsuarioEdicao.HasValue;
            if (lblSaldoInicial != null) lblSaldoInicial.Visible = mostrar;
            if (numSaldoInicial != null) numSaldoInicial.Visible = mostrar;
        }

        private void Carregar()
        {
            if (_idUsuarioEdicao.HasValue)
            {
                var u = _controller.ObterPorId(_idUsuarioEdicao.Value);
                if (u == null)
                {
                    MessageBox.Show("Usuário não encontrado");
                    Close();
                    return;
                }
                txtNomeCompleto.Text = u.NomeCompleto;
                txtNomeUsuario.Text = u.NomeUsuario;
                cbCargo.SelectedItem = u.Cargo;
            }
            else
            {
                cbCargo.SelectedItem = CassinoMVC.Models.CargoUsuario.Jogador;
                numSaldoInicial.Value = 0;
            }
            AtualizarVisibilidadeSaldo();
        }

        private void BtnSalvar_Click(object sender, EventArgs e)
        {
            if (_usuarioLogado == null)
            {
                MessageBox.Show("Usuário logado não identificado.");
                return;
            }

            var nome = txtNomeCompleto.Text.Trim();
            var login = txtNomeUsuario.Text.Trim();
            var senha = txtSenha.Text;
            if (!(cbCargo.SelectedItem is CassinoMVC.Models.CargoUsuario cargo))
            {
                MessageBox.Show("Selecione um cargo.");
                return;
            }

            if (_idUsuarioEdicao.HasValue)
            {
                if (_controller.Atualizar(_idUsuarioEdicao.Value, nome, login, senha, cargo, _usuarioLogado.IdUsuario, out string msgEdit))
                {
                    MessageBox.Show(msgEdit);
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    MessageBox.Show(msgEdit);
                }
            }
            else
            {
                if (_usuarioLogado.Cargo != CassinoMVC.Models.CargoUsuario.Administrador)
                {
                    MessageBox.Show("Apenas administradores podem criar usuários.");
                    return;
                }
                var saldoInicial = numSaldoInicial.Value;
                if (_controller.Criar(nome, login, senha, cargo, saldoInicial, out var novo, out string msgNew))
                {
                    MessageBox.Show(msgNew);
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    MessageBox.Show(msgNew);
                }
            }
        }
    }
}
