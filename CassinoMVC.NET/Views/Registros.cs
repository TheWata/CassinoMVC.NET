using System;
using System.Windows.Forms;
using CassinoMVC.Services;

namespace CassinoMVC.Views
{
 public partial class Registros : Form
 {
 public Registros()
 {
 InitializeComponent();
 btnConsultarRegistro.Click += delegate { Carregar(); };
 Load += delegate { CarregarCombo(); Carregar(); };
 }

 private void CarregarCombo()
 {
 cbJogo.Items.Clear();
 cbJogo.Items.Add("");
 cbJogo.Items.Add("Roleta");
 cbJogo.Items.Add("Blackjack");
 cbJogo.Items.Add("Caça-níqueis");
 cbJogo.Items.Add("Compra de fichas");
 cbJogo.SelectedIndex =0;
 }

 private void Carregar()
 {
 string usuarioFiltro = txtRegistrosUsuario.Text.Trim();
 string jogo = cbJogo.SelectedItem != null ? cbJogo.SelectedItem.ToString() : null;
 if (string.IsNullOrWhiteSpace(jogo)) jogo = null;
 if (string.IsNullOrWhiteSpace(usuarioFiltro)) usuarioFiltro = null;
 var regs = RegistroSessaoService.Listar(usuarioFiltro, jogo);
 lstRegistros.Items.Clear();
 foreach (var r in regs)
 {
 lstRegistros.Items.Add(string.Format("ID:{0} | {1} | Usuario:{2} | SaldoIni:{3:F2} | SaldoFim:{4:F2} | Lucro:{5:F2} | Segs:{6}", r.Id, r.JogoOuAcao, r.Usuario, r.SaldoInicial, r.SaldoFinal, r.Lucro, r.DuracaoSegundos));
 }
 if (regs.Count ==0) lstRegistros.Items.Add("Nenhum registro");
 }
 }
}
