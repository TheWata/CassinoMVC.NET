using System.Collections.Generic;

using CassinoMVC.Services;

namespace CassinoMVC.Controllers
{
    /// <summary>
    /// Controller de Relatórios: consulta e resume apostas para telas administrativas.
    /// </summary>
    public class RelatorioController
    {
        public List<RelatorioService.ApostaConsultaDto> Consultar(string usuario, string jogo)
            => RelatorioService.ConsultarApostas(usuario, jogo);

        public RelatorioService.ResumoApostas ResumoJogador(int idJogador)
            => RelatorioService.GetResumoJogador(idJogador);

        public RelatorioService.ResumoApostas ResumoGeral()
            => RelatorioService.GetResumoGeral();
    }
}