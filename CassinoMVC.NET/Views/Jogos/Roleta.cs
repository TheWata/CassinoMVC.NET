using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using CassinoMVC.Controllers;

namespace CassinoMVC.Views.Jogos
{
    /// <summary>
    /// View (UI) da Roleta: apenas interage com a UI, chama o controller e apresenta resultados.
    /// Regras e dados ficam em Services/Models.
    /// </summary>
    public partial class Roleta : Form
    {
        // Contexto (preenchido pela HomePage)
        private CassinoMVC.Models.Usuario _usuarioLogado;
        private CassinoMVC.Models.Jogador _jogadorLogado;

        // Controller (lógica de controle)
        private readonly RoletaController _controller = new RoletaController();

        public Roleta()
        {
            InitializeComponent();
            this.Load += Roleta_Load; // prepara UI ao carregar
        }

        // Recebe usuário/jogador da HomePage
        public void CarregarContexto(CassinoMVC.Models.Usuario usuario, CassinoMVC.Models.Jogador jogador)
        {
            _usuarioLogado = usuario;
            _jogadorLogado = jogador;
            txtFichas.Text = jogador.Saldo.ToString("F2");
        }

        // Prepara listas e modo de aposta
        private void Roleta_Load(object sender, EventArgs e)
        {
            PrepararListas();
            AtualizarModoAposta();
        }

        // Preenche a lista de 0..36 e cores
        private void PrepararListas()
        {
            listNumeros.Items.Clear();
            for (int i = 0; i <= 36; i++)
            {
                var cor = CassinoMVC.Models.Roleta.ObterCor(i);
                var item = new ListViewItem(i.ToString());
                item.SubItems.Add(cor.ToString());
                item.Tag = i;
                listNumeros.Items.Add(item);
            }
        }

        // Habilita/Desabilita apostas externas conforme seleção interna
        private void AtualizarModoAposta()
        {
            int selecionados = listNumeros.CheckedItems.Count;
            groupApostasExternas.Enabled = selecionados == 0;
            lblModo.Text = selecionados > 0 ? "Aposta Interna" : "Aposta Externa";
        }

        // Limita a 3 números
        private void listNumeros_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (listNumeros.CheckedItems.Count > 3)
            {
                e.Item.Checked = false;
                MessageBox.Show("Máximo de 3 números por aposta interna.");
            }
            AtualizarModoAposta();
        }

        // Botão Apostar: valida inputs, decide modo e chama controller
        private void btnApostar_Click(object sender, EventArgs e)
        {
            if (_usuarioLogado == null || _jogadorLogado == null)
            {
                MessageBox.Show("Sessão inválida. Faça login novamente.");
                return;
            }

            var valor = Convert.ToDecimal(txtValorAposta.Value);
            if (valor <= 0) { MessageBox.Show("Valor de aposta inválido."); return; }
            if (valor > _jogadorLogado.Saldo) { MessageBox.Show("Saldo insuficiente."); return; }

            var selecionados = new List<int>();
            foreach (ListViewItem i in listNumeros.CheckedItems) selecionados.Add((int)i.Tag);
            if (selecionados.Count > 0)
            {
                var r = _controller.ApostarInterna(selecionados);
                AplicarSaldoEExibir(valor, r.Item3, r.Item4, r.Item1, r.Item2.ToString());
            }
            else
            {
                var tipo = ObterApostaExternaSelecionada();
                if (!tipo.HasValue) { MessageBox.Show("Selecione um tipo de aposta externa ou escolha números."); return; }
                var r = _controller.ApostarExterna(tipo.Value);
                AplicarSaldoEExibir(valor, r.Item3, r.Item4, r.Item1, r.Item2.ToString());
            }
        }

        // Converte os radios para o enum do controller
        private RoletaController.ApostaExternaTipo? ObterApostaExternaSelecionada()
        {
            if (rbVermelho.Checked) return RoletaController.ApostaExternaTipo.Vermelho;
            if (rbPreto.Checked) return RoletaController.ApostaExternaTipo.Preto;
            if (rbPar.Checked) return RoletaController.ApostaExternaTipo.Par;
            if (rbImpar.Checked) return RoletaController.ApostaExternaTipo.Impar;
            if (rb1_12.Checked) return RoletaController.ApostaExternaTipo.Dezenas_1_12;
            if (rb13_24.Checked) return RoletaController.ApostaExternaTipo.Dezenas_13_24;
            if (rb25_36.Checked) return RoletaController.ApostaExternaTipo.Dezenas_25_36;
            return null;
        }

        // Aplica saldo, atualiza UI e histórico
        private void AplicarSaldoEExibir(decimal valorAposta, bool ganhou, decimal multiplicador, int numeroSorteado, string cor)
        {
            decimal premio = ganhou ? valorAposta * multiplicador : 0m;
            _jogadorLogado.Saldo -= valorAposta;
            _jogadorLogado.Saldo += premio;
            txtFichas.Text = _jogadorLogado.Saldo.ToString("F2");
            lblResultado.Text = string.Format("Número: {0} Cor: {1} => {2} (x{3})", numeroSorteado, cor, ganhou ? "Ganhou" : "Perdeu", multiplicador);
            lstHistoricoRoleta.Items.Insert(0, lblResultado.Text);
        }
    }
}
