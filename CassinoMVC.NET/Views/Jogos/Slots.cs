using System;
using System.Linq;
using System.Windows.Forms;
using CassinoMVC.Data;

namespace CassinoMVC.Views.Jogos
{
    public partial class Slots : Form
    {
        private CassinoMVC.Models.Usuario _usuarioLogado;
        private CassinoMVC.Models.Jogador _jogadorLogado;
        private readonly CassinoMVC.Controllers.ApostaController _apostaController = new CassinoMVC.Controllers.ApostaController();

        public Slots()
        {
            InitializeComponent();
        }

        public void CarregarContexto(CassinoMVC.Models.Usuario usuario, CassinoMVC.Models.Jogador jogador)
        {
            _usuarioLogado = usuario;
            _jogadorLogado = jogador;
            txtFichas.Text = jogador.Saldo.ToString("F2");
        }

        private void btnGirarSlot_Click(object sender, EventArgs e)
        {
            if (_usuarioLogado == null || _jogadorLogado == null) return;

            var valorAposta = Convert.ToDecimal(txtValorAposta.Value);
            if (valorAposta <= 0 || valorAposta > _jogadorLogado.Saldo)
            {
                MessageBox.Show("Valor inválido ou saldo insuficiente.");
                return;
            }
            try
            {
                var aposta = _apostaController.ApostarSlots(_jogadorLogado.IdJogador, valorAposta);

                // Lógica ANTIGA (QUEBRADA):
                // var sessao = Db.Context.Sessoes.FirstOrDefault(s => s.IdSessao == aposta.IdSessao);

                // Lógica NOVA (CORRIGIDA):
                // O ApostaService cria uma Aposta e uma SessaoJogo.
                // A 'aposta' retornada tem o IdSessao. Precisamos buscar essa sessão.
                using (var ctx = new DataContext())
                {
                    var sessao = ctx.Sessoes.Find(aposta.IdSessao);
                    if (sessao != null) txtSlots.Text = sessao.Resultado;
                }

                txtFichas.Text = _jogadorLogado.Saldo.ToString("F2");
                MessageBox.Show(string.Format("{0} | Prêmio: {1:C}", aposta.Resultado, aposta.ValorPremio));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}