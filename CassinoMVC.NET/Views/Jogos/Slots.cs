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
                // 1. A aposta é registrada e o saldo é salvo no BANCO
                var aposta = _apostaController.ApostarSlots(_jogadorLogado.IdJogador, valorAposta);

                // 2. Busca a sessão (Corrigido)
                using (var ctx = new DataContext())
                {
                    var sessao = ctx.Sessoes.Find(aposta.IdSessao);
                    if (sessao != null) txtSlots.Text = sessao.Resultado;
                }

            
                _jogadorLogado.Saldo += (aposta.ValorPremio - aposta.ValorApostado);
                // ---------------------

                // 4. Agora o txtFichas usará o saldo local ATUALIZADO
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