using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using CassinoMVC.Controllers;

namespace CassinoMVC.Views.Jogos
{
    public partial class BlackJack : Form
    {
        private CassinoMVC.Models.Usuario _usuarioLogado;
        private CassinoMVC.Models.Jogador _jogadorLogado;
        private CassinoMVC.Models.BlackjackGame _jogo;
        private readonly BlackjackController _controller = new BlackjackController();
        private decimal _apostaInicial;
        private DateTime _inicioSessao;

        public BlackJack()
        {
            InitializeComponent();
            AtualizarBotoes(false);
            AtualizarPontuacao();
        }

        public void CarregarContexto(CassinoMVC.Models.Usuario usuario, CassinoMVC.Models.Jogador jogador)
        {
            _usuarioLogado = usuario;
            _jogadorLogado = jogador;
            txtFichas.Text = jogador.Saldo.ToString("F2");
        }

        private void btnIniciar_Click(object sender, EventArgs e)
        {
            if (_jogadorLogado == null) return;
            var aposta = numAposta.Value;
            if (aposta > (decimal)_jogadorLogado.Saldo)
            {
                MessageBox.Show("Saldo insuficiente.");
                return;
            }
            if (aposta <=0)
            {
                MessageBox.Show("Aposta inválida.");
                return;
            }
            _inicioSessao = DateTime.UtcNow;
            _apostaInicial = aposta;
            _jogo = _controller.Iniciar(aposta);
            _jogadorLogado.Saldo -= aposta; // debita aposta inicial
            RenderizarMaos();
            AtualizarPontuacao();
            lblStatus.Text = "Jogo iniciado";
            AtualizarBotoes(true);
            txtFichas.Text = _jogadorLogado.Saldo.ToString("F2");
        }

        private void btnHit_Click(object sender, EventArgs e)
        {
            if (_jogo == null) return;
            _controller.Hit(_jogo);
            RenderizarMaos();
            AtualizarPontuacao();
            if (_jogo.Estado == CassinoMVC.Models.BlackjackEstado.Finalizada)
            {
                Finalizar();
            }
        }

        private void btnStay_Click(object sender, EventArgs e)
        {
            if (_jogo == null) return;
            _controller.Stay(_jogo);
            RenderizarMaos();
            AtualizarPontuacao();
            Finalizar();
        }

        private void btnDouble_Click(object sender, EventArgs e)
        {
            if (_jogo == null || _jogadorLogado == null) return;
            var valorExtra = _jogo.ApostaAtual; // aposta adicional igual à original
            if (valorExtra > (decimal)_jogadorLogado.Saldo)
            {
                MessageBox.Show("Saldo insuficiente para Double Down.");
                return;
            }
            _jogadorLogado.Saldo -= valorExtra; // debita valor adicional
            _controller.DoubleDown(_jogo);
            RenderizarMaos();
            AtualizarPontuacao();
            Finalizar();
        }

        private void Finalizar()
        {
            if (_jogo == null || _jogadorLogado == null) return;
            var avaliacao = _controller.Avaliar(_jogo);
            string resultado = avaliacao.Item1;
            decimal mult = avaliacao.Item2;
            decimal retorno = _jogo.ApostaAtual * mult; // multiplicador0,1,2
            if (retorno >0)
            {
                _jogadorLogado.Saldo += retorno;
            }
            txtFichas.Text = _jogadorLogado.Saldo.ToString("F2");
            lblStatus.Text = string.Format("{0} | Jogador: {1} Dealer: {2}", resultado, _controller.PontosJogador(_jogo), _controller.PontosDealer(_jogo));
            AtualizarBotoes(false);
        }

        private void RenderizarMaos()
        {
            flowJogador.Controls.Clear();
            flowDealer.Controls.Clear();
            if (_jogo == null) return;
            foreach (var carta in _jogo.MaoJogador) flowJogador.Controls.Add(CriarCard(carta));
            foreach (var carta in _jogo.MaoDealer) flowDealer.Controls.Add(CriarCard(carta));
        }

        private static string GerarNomeArquivo(CassinoMVC.Models.Carta carta)
        {
            string naipeFile;
            switch (carta.Naipe)
            {
                case "Copas": naipeFile = "hearts"; break;
                case "Espadas": naipeFile = "spades"; break;
                case "Ouros": naipeFile = "diamonds"; break;
                case "Paus": naipeFile = "clubs"; break;
                default: naipeFile = carta.Naipe.ToLowerInvariant(); break;
            }
            string valorFile;
            switch (carta.Valor)
            {
                case "A": valorFile = "ace"; break;
                case "J": valorFile = "jack"; break;
                case "Q": valorFile = "queen"; break;
                case "K": valorFile = "king"; break;
                default: valorFile = carta.Valor.ToLowerInvariant(); break;
            }
            return valorFile + "_of_" + naipeFile + ".png";
        }

        private static string[] GetSearchRoots()
        {
            string exeDir = AppDomain.CurrentDomain.BaseDirectory; // ...\bin\Debug
            // sobe alguns níveis para procurar a pasta Resources no projeto
            return new string[]
            {
                exeDir,
                Path.GetFullPath(Path.Combine(exeDir, "..")),
                Path.GetFullPath(Path.Combine(exeDir, "..","..")),
                Path.GetFullPath(Path.Combine(exeDir, "..","..","..")),
                Path.GetFullPath(Path.Combine(exeDir, "..","..","..",".."))
            };
        }

        private Image CarregarImagemArquivo(CassinoMVC.Models.Carta carta)
        {
            string fileName = GerarNomeArquivo(carta);
            string altFileName = fileName.ToUpperInvariant();
            foreach (var root in GetSearchRoots())
            {
                string[] candidates = new string[]
                {
                    Path.Combine(root, fileName),
                    Path.Combine(root, "Resources", "PNG-cards-1.3", fileName),
                    Path.Combine(root, "Resources", fileName),
                    Path.Combine(root, "CassinoMVC.NET","Resources","PNG-cards-1.3", fileName),
                    Path.Combine(root, "CassinoMVC.NET","Resources", fileName)
                };
                foreach (var c in candidates)
                {
                    if (File.Exists(c)) { try { return Image.FromFile(c); } catch { } }
                    var upper = Path.ChangeExtension(c, null) + ".PNG";
                    if (File.Exists(upper)) { try { return Image.FromFile(upper); } catch { } }
                }
                // varredura por nome dentro de Resources
                try
                {
                    foreach (var folder in new []{ Path.Combine(root, "Resources"), Path.Combine(root, "CassinoMVC.NET","Resources") })
                    {
                        if (!Directory.Exists(folder)) continue;
                        var found = Directory.GetFiles(folder, fileName, SearchOption.AllDirectories).FirstOrDefault();
                        if (found != null) return Image.FromFile(found);
                        // tentar variação .PNG
                        found = Directory.GetFiles(folder, altFileName, SearchOption.AllDirectories).FirstOrDefault();
                        if (found != null) return Image.FromFile(found);
                    }
                }
                catch { }
            }
            return null;
        }

        private Image GerarImagemTexto(CassinoMVC.Models.Carta carta)
        {
            var bmp = new Bitmap(60,90);
            using (var g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.White);
                using (var f = new Font("Arial",9))
                using (var b = new SolidBrush(Color.Black))
                {
                    g.DrawString(string.Format("{0}\n{1}", carta.Valor, carta.Naipe), f, b, new RectangleF(2,2,56,86));
                }
                g.DrawRectangle(Pens.Black,0,0,59,89);
            }
            return bmp;
        }

        private Control CriarCard(CassinoMVC.Models.Carta carta)
        {
            var pb = new PictureBox();
            pb.Width =60; pb.Height =90; pb.SizeMode = PictureBoxSizeMode.StretchImage; pb.Margin = new Padding(3); pb.BorderStyle = BorderStyle.FixedSingle;
            Image img = null;
            // Tenta primeiro ImagemPath se definido
            if (!string.IsNullOrWhiteSpace(carta.ImagemPath))
            {
                var baseDir = AppDomain.CurrentDomain.BaseDirectory;
                var p = Path.Combine(baseDir, carta.ImagemPath.TrimStart(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar));
                if (File.Exists(p)) { try { img = Image.FromFile(p); } catch { img = null; } }
            }
            // Se falhou tenta por nome derivado e múltiplas raízes
            if (img == null) img = CarregarImagemArquivo(carta);
            // Fallback texto
            if (img == null) img = GerarImagemTexto(carta);
            pb.Image = img;
            return pb;
        }

        private void AtualizarBotoes(bool emJogo)
        {
            btnHit.Enabled = emJogo;
            btnStay.Enabled = emJogo;
            btnDouble.Enabled = emJogo;
            btnIniciar.Enabled = !emJogo;
        }

        private void AtualizarPontuacao()
        {
            if (_jogo == null)
            {
                lblPontosJogador.Text = "Pontos:0";
                lblPontosDealer.Text = "Pontos:0";
            }
            else
            {
                lblPontosJogador.Text = string.Format("Pontos: {0}", _controller.PontosJogador(_jogo));
                lblPontosDealer.Text = string.Format("Pontos: {0}", _controller.PontosDealer(_jogo));
            }
        }
    }
}
