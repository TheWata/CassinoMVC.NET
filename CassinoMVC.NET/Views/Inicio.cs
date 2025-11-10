using System;
using System.Windows.Forms;
using CassinoMVC.Controllers;
using CassinoMVC.Models;

namespace CassinoMVC.Views
{
    public partial class Inicio : Form
    {
        private readonly CassinoMVC.Controllers.UsuarioController _usuarioController = new CassinoMVC.Controllers.UsuarioController();
        private readonly CassinoMVC.Controllers.JogadorController _jogadorController = new CassinoMVC.Controllers.JogadorController();

        public Inicio()
        {
            InitializeComponent();
        }

        private void btnRegistro_Click(object sender, EventArgs e)
        {
            using (var frm = new Registro())
            {
                frm.ShowDialog(this);
            }
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            var nomeUsuario = txtUser.Text.Trim();
            var senha = txtSenha.Text;
            var usuario = _usuarioController.Login(nomeUsuario, senha);
            if (usuario == null)
            {
                MessageBox.Show("Usuário ou senha inválidos", "Login", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var jogador = _jogadorController.ObterPorUsuarioId(usuario.IdUsuario);
            var home = new HomePage();
            home.CarregarContexto(usuario, jogador);
            Hide();
            home.FormClosed += delegate { Show(); };
            home.Show();
        }
    }
}
