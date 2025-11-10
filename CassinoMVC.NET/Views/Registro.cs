using System;
using System.Windows.Forms;
using CassinoMVC.Controllers;
using CassinoMVC.Models;

namespace CassinoMVC.Views
{
    public partial class Registro : Form
    {
        private readonly UsuarioController _usuarioController = new UsuarioController();
        private readonly JogadorController _jogadorController = new JogadorController();

        public Registro()
        {
            InitializeComponent();
        }

        private void btnCancelarRegistro_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnCadastrarRegistro_Click(object sender, EventArgs e)
        {
            var nome = txtRegistroNome.Text.Trim();
            var login = txtRegistroUsuario.Text.Trim();
            var senha = txtRegistroSenha.Text;

            if (string.IsNullOrWhiteSpace(nome) || string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(senha))
            {
                MessageBox.Show("Preencha nome, usuário e senha.");
                return;
            }

            // Cria usuário com cargo Jogador
            var usuario = _usuarioController.CriarUsuario(nome, login, senha, CargoUsuario.Jogador);
            // Cria jogador associado com saldo inicial 0
            _jogadorController.Criar(login, 0m, string.Empty, usuario.IdUsuario);

            MessageBox.Show("Cadastro realizado com sucesso!");
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
