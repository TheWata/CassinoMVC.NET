using System;
using System.Windows.Forms;
using CassinoMVC.Controllers;
using CassinoMVC.Services;

namespace CassinoMVC.Views
{
    public partial class Usuarios : Form
    {
        private readonly UsuariosController _controller = new UsuariosController();
        private readonly JogadorController _jogadorController = new JogadorController();
        private readonly AdminService _admin = new AdminService();
        private CassinoMVC.Models.Usuario _usuarioLogado;

        public Usuarios()
        {
            InitializeComponent();
            Load += delegate { _controller.CarregarLista(lstUsuarios); };
            btnPesquisar.Click += delegate { _controller.CarregarLista(lstUsuarios, txtUsuarios.Text.Trim()); };
            btnRemover.Click += delegate { _controller.RemoverSelecionado(this, lstUsuarios, _usuarioLogado); };
            lstUsuarios.DoubleClick += LstUsuarios_DoubleClick;
            btnAdicionar.Click += BtnAdicionar_Click;
        }

        public void DefinirUsuarioLogado(CassinoMVC.Models.Usuario usuario)
        {
            _usuarioLogado = usuario;
        }

        private void LstUsuarios_DoubleClick(object sender, EventArgs e)
        {
            int id = _controller.ObterIdSelecionado(lstUsuarios);
            if (id <= 0) return;
            var u = _controller.ObterPorId(id);
            var modelo = _admin.PrepararEdicaoUsuario(u, _controller.ObterSaldoFichas(id));
            using (var frm = new Usuario())
            {
                frm.AplicarModelo(modelo);
                frm.SalvarSolicitado += (s, a) =>
                {
                    if (_controller.Atualizar(id, frm.NomeCompleto, frm.NomeUsuario, frm.Senha, frm.CargoSelecionado, _usuarioLogado.IdUsuario, out string msg))
                    {
                        var j = _jogadorController.ObterPorUsuarioId(id);
                        if (j != null) j.Saldo = frm.Saldo;
                        MessageBox.Show(msg);
                        frm.DialogResult = DialogResult.OK;
                        frm.Close();
                        _controller.CarregarLista(lstUsuarios, txtUsuarios.Text.Trim());
                    }
                    else
                    {
                        MessageBox.Show(msg);
                    }
                };
                frm.ShowDialog(this);
            }
        }

        private void BtnAdicionar_Click(object sender, EventArgs e)
        {
            var modelo = _admin.PrepararCriacaoUsuario();
            using (var frm = new Usuario())
            {
                frm.AplicarModelo(modelo);
                frm.SalvarSolicitado += (s, a) =>
                {
                    if (_controller.Criar(frm.NomeCompleto, frm.NomeUsuario, frm.Senha, frm.CargoSelecionado, 0m, out var novo, out string msg))
                    {
                        MessageBox.Show(msg);
                        frm.DialogResult = DialogResult.OK;
                        frm.Close();
                        _controller.CarregarLista(lstUsuarios, txtUsuarios.Text.Trim());
                    }
                    else
                    {
                        MessageBox.Show(msg);
                    }
                };
                frm.ShowDialog(this);
            }
        }
    }
}
