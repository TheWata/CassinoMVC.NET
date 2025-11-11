using System;
using System.Windows.Forms;
using CassinoMVC.Controllers;

namespace CassinoMVC.Views
{
    public partial class Usuarios : Form
    {
        private readonly UsuariosController _controller = new UsuariosController();
        private CassinoMVC.Models.Usuario _usuarioLogado;

        public Usuarios()
        {
            InitializeComponent();
            Load += delegate { _controller.CarregarLista(lstUsuarios); };
            btnPesquisar.Click += delegate { _controller.CarregarLista(lstUsuarios, txtUsuarios.Text.Trim()); };
            btnRemover.Click += delegate { _controller.RemoverSelecionado(this, lstUsuarios, _usuarioLogado); };
            lstUsuarios.DoubleClick += delegate { _controller.EditarSelecionado(this, lstUsuarios, _usuarioLogado); };
            btnAdicionar.Click += delegate { _controller.Adicionar(this, lstUsuarios, _usuarioLogado); };
        }

        public void DefinirUsuarioLogado(CassinoMVC.Models.Usuario usuario)
        {
            _usuarioLogado = usuario;
        }
    }
}
