using System;
using System.Windows.Forms;
using CassinoMVC.Services;

namespace CassinoMVC.Views
{
    public partial class HomePage : Form
    {
        private CassinoMVC.Models.Usuario _usuarioLogado;
        private CassinoMVC.Models.Jogador _jogadorLogado;
        private readonly CassinoMVC.Controllers.JogadorController _jogadorController = new CassinoMVC.Controllers.JogadorController();
        private readonly CassinoMVC.Controllers.CompraFichasController _compraController = new CassinoMVC.Controllers.CompraFichasController();

        public HomePage()
        {
            InitializeComponent();
            btnSlots.Click += BtnSlots_Click;
            button1.Click += BtnComprarFichas_Click;
            btnRegistros.Click += BtnRegistros_Click;
            btnRoleta.Click += BtnRoleta_Click;
            btnBlackJack.Click += BtnBlackJack_Click;
            btnUsuarios.Click += BtnUsuarios_Click;
        }

        public void CarregarContexto(CassinoMVC.Models.Usuario usuario, CassinoMVC.Models.Jogador jogador)
        {
            _usuarioLogado = usuario;
            _jogadorLogado = jogador;
            txtUsuário.Text = usuario.NomeUsuario;
            txtFichas.Text = jogador.Saldo.ToString("F2");
            bool ehAdmin = usuario.Cargo == CassinoMVC.Models.CargoUsuario.Administrador;
            btnRegistros.Visible = ehAdmin;
            btnUsuarios.Visible = ehAdmin;
            //btnEditarUsuario.Visible = ehAdmin;
        }

        private void AbrirSlots()
        {
            if (_usuarioLogado == null || _jogadorLogado == null) return;
            int idSessao = RegistroSessaoService.IniciarSessao("Caça-níqueis", _usuarioLogado.NomeUsuario, _jogadorLogado.Saldo);
            using (var frm = new Jogos.Slots())
            {
                frm.CarregarContexto(_usuarioLogado, _jogadorLogado);
                frm.FormClosed += delegate { RegistroSessaoService.FinalizarSessao(idSessao, _jogadorLogado.Saldo); txtFichas.Text = _jogadorLogado.Saldo.ToString("F2"); };
                frm.ShowDialog(this);
            }
        }

        private void AbrirRoleta()
        {
            if (_usuarioLogado == null || _jogadorLogado == null) return;
            int idSessao = RegistroSessaoService.IniciarSessao("Roleta", _usuarioLogado.NomeUsuario, _jogadorLogado.Saldo);
            using (var frm = new Jogos.Roleta())
            {
                frm.CarregarContexto(_usuarioLogado, _jogadorLogado);
                frm.FormClosed += delegate { RegistroSessaoService.FinalizarSessao(idSessao, _jogadorLogado.Saldo); txtFichas.Text = _jogadorLogado.Saldo.ToString("F2"); };
                frm.ShowDialog(this);
            }
        }

        private void AbrirBlackjack()
        {
            if (_usuarioLogado == null || _jogadorLogado == null) return;
            int idSessao = RegistroSessaoService.IniciarSessao("Blackjack", _usuarioLogado.NomeUsuario, _jogadorLogado.Saldo);
            using (var frm = new Jogos.BlackJack())
            {
                frm.CarregarContexto(_usuarioLogado, _jogadorLogado);
                frm.FormClosed += delegate { RegistroSessaoService.FinalizarSessao(idSessao, _jogadorLogado.Saldo); txtFichas.Text = _jogadorLogado.Saldo.ToString("F2"); };
                frm.ShowDialog(this);
            }
        }

        private void BtnSlots_Click(object sender, EventArgs e) { AbrirSlots(); }
        private void BtnRoleta_Click(object sender, EventArgs e) { AbrirRoleta(); }
        private void BtnBlackJack_Click(object sender, EventArgs e) { AbrirBlackjack(); }

        private void BtnComprarFichas_Click(object sender, EventArgs e)
        {
            if (_jogadorLogado == null || _usuarioLogado == null) return;
            string input = MostrarInput("Quantidade de fichas:", "Comprar Fichas", "100");
            decimal valor;
            if (decimal.TryParse(input, out valor) && valor >0)
            {
                int idSessao = RegistroSessaoService.IniciarSessao("Compra de fichas", _usuarioLogado.NomeUsuario, _jogadorLogado.Saldo);
                if (_compraController.Comprar(_jogadorLogado.IdJogador, valor))
                {
                    RegistroSessaoService.FinalizarSessao(idSessao, _jogadorLogado.Saldo);
                    txtFichas.Text = _jogadorLogado.Saldo.ToString("F2");
                    MessageBox.Show("Fichas compradas com sucesso.");
                }
            }
        }

        private string MostrarInput(string mensagem, string titulo, string valorPadrao)
        {
            using (Form prompt = new Form())
            {
                prompt.Width =300; prompt.Height =150; prompt.Text = titulo; prompt.FormBorderStyle = FormBorderStyle.FixedDialog; prompt.StartPosition = FormStartPosition.CenterParent; prompt.MaximizeBox = false; prompt.MinimizeBox = false;
                Label lbl = new Label(); lbl.Left =10; lbl.Top =10; lbl.Text = mensagem; lbl.AutoSize = true;
                TextBox box = new TextBox(); box.Left =10; box.Top =35; box.Width =260; box.Text = valorPadrao;
                Button ok = new Button(); ok.Text = "OK"; ok.Left =110; ok.Top =70; ok.Width =75; ok.DialogResult = DialogResult.OK;
                prompt.Controls.Add(lbl); prompt.Controls.Add(box); prompt.Controls.Add(ok);
                prompt.AcceptButton = ok;
                return prompt.ShowDialog(this) == DialogResult.OK ? box.Text : string.Empty;
            }
        }

        private void BtnRegistros_Click(object sender, EventArgs e)
        {
            if (_usuarioLogado == null || _usuarioLogado.Cargo != CassinoMVC.Models.CargoUsuario.Administrador) return;
            using (var frm = new Registros())
            {
                frm.ShowDialog(this);
            }
        }
        private void BtnUsuarios_Click(object sender, EventArgs e)
        {
            if (_usuarioLogado == null || _usuarioLogado.Cargo != CassinoMVC.Models.CargoUsuario.Administrador) return;
            using (var frm = new Usuarios())
            {
                frm.DefinirUsuarioLogado(_usuarioLogado);
                frm.ShowDialog(this);
            }
        }

        private void HomePage_Load(object sender, EventArgs e)
        {

        }
    }
}
