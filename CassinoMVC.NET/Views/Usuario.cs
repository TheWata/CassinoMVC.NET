using System;
using System.Windows.Forms;
using CassinoMVC.Services;

namespace CassinoMVC.Views
{
    public partial class Usuario : Form
    {
        public event EventHandler SalvarSolicitado;
        private AdminService.UsuarioFormModel _modelo;

        public Usuario()
        {
            InitializeComponent();
            cbCargo.SelectedIndexChanged += (s, e) => AtualizarSaldoVisibilidade();
            btnSalvar.Click += (s, e) => SalvarSolicitado?.Invoke(this, EventArgs.Empty);
            btnCancelar.Click += (s, e) => Close();
        }

        // Recebe o modelo preparado pelo service
        public void AplicarModelo(AdminService.UsuarioFormModel modelo)
        {
            _modelo = modelo;
            Text = modelo.Titulo;
            txtNome.Text = modelo.NomeCompleto;
            txtUsuario.Text = modelo.NomeUsuario;
            cbCargo.Items.Clear();
            cbCargo.Items.Add(CassinoMVC.Models.CargoUsuario.Administrador);
            cbCargo.Items.Add(CassinoMVC.Models.CargoUsuario.Jogador);
            cbCargo.SelectedItem = modelo.Cargo;
            txtSenha.Text = modelo.Senha;
            numSaldoInicial.Value = modelo.Saldo;
            lblSaldoInicial.Text = "Saldo";
            AtualizarSaldoVisibilidade();
        }

        private void AtualizarSaldoVisibilidade()
        {
            if (_modelo == null) { lblSaldoInicial.Visible = false; numSaldoInicial.Visible = false; return; }
            bool ehJogador = cbCargo.SelectedItem is CassinoMVC.Models.CargoUsuario cargo && cargo == CassinoMVC.Models.CargoUsuario.Jogador;
            bool mostrar = !_modelo.ModoCriacao && ehJogador && _modelo.MostrarSaldo;
            lblSaldoInicial.Visible = mostrar;
            numSaldoInicial.Visible = mostrar;
        }

        // Getters expostos para controller
        public string NomeCompleto => txtNome.Text.Trim();
        public string NomeUsuario => txtUsuario.Text.Trim();
        public string Senha => txtSenha.Text;
        public CassinoMVC.Models.CargoUsuario CargoSelecionado => (CassinoMVC.Models.CargoUsuario)cbCargo.SelectedItem;
        public decimal Saldo => numSaldoInicial.Value;
    }
}
