using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using CassinoMVC.Data;
using CassinoMVC.Models;
using CassinoMVC.Services;

namespace CassinoMVC.Controllers
{
    public class UsuariosController
    {
        // ... (Classe UsuarioListItem permanece igual) ...
        public class UsuarioListItem
        {
            public int Id { get; set; }
            public string Texto { get; set; }
            public override string ToString() => Texto;
        }


        public Usuarios Listar(string filtro = null)
        {
            // Lógica ANTIGA:
            // var query = Db.Context.Usuarios.AsEnumerable(); // <-- Ineficiente!

            // Lógica NOVA:
            using (var ctx = new DataContext())
            {
                // AsQueryable() garante que o .Where() seja executado no SQL
                var query = ctx.Usuarios.AsQueryable();

                if (!string.IsNullOrWhiteSpace(filtro))
                {
                    var f = filtro.Trim();
                    query = query.Where(u =>
                        (!string.IsNullOrEmpty(u.NomeUsuario) && u.NomeUsuario.IndexOf(f, StringComparison.OrdinalIgnoreCase) >= 0) ||
                        (!string.IsNullOrEmpty(u.NomeCompleto) && u.NomeCompleto.IndexOf(f, StringComparison.OrdinalIgnoreCase) >= 0)
                    );
                }
                var ordenados = query.OrderBy(u => u.NomeUsuario).ToList();
                return new Usuarios(ordenados);
            }
        }

        // Métodos 'ListarFormatado', 'ListarItems', 'CarregarLista', 'ObterIdSelecionado'
        // não acessam Db.Context e permanecem IGUAIS.

        public List<string> ListarFormatado(string filtro = null)
        {
            return Listar(filtro)
                .Itens
                .Select(u => string.Format("ID:{0} | {1} | {2} | Cargo:{3}", u.IdUsuario, u.NomeUsuario, u.NomeCompleto, u.Cargo))
                .ToList();
        }

        public BindingList<UsuarioListItem> ListarItems(string filtro = null)
        {
            var usuarios = Listar(filtro).Itens;
            var items = usuarios
                .Select(u => new UsuarioListItem
                {
                    Id = u.IdUsuario,
                    Texto = string.Format("ID:{0} | {1} | {2} | Cargo:{3}", u.IdUsuario, u.NomeUsuario, u.NomeCompleto, u.Cargo)
                })
                .ToList();
            return new BindingList<UsuarioListItem>(items);
        }

        public void CarregarLista(ListBox listBox, string filtro = null)
        {
            listBox.DataSource = ListarItems(filtro);
            listBox.DisplayMember = nameof(UsuarioListItem.Texto);
            listBox.ValueMember = nameof(UsuarioListItem.Id);
        }

        public int ObterIdSelecionado(ListBox listBox)
        {
            if (listBox.SelectedItem is UsuarioListItem item) return item.Id;
            if (listBox.SelectedValue is int val) return val;
            return 0;
        }


        public Usuario ObterPorId(int idUsuario)
        {
            // Lógica ANTIGA:
            // return Db.Context.Usuarios.FirstOrDefault(u => u.IdUsuario == idUsuario);

            // Lógica NOVA:
            using (var ctx = new DataContext())
            {
                return ctx.Usuarios.Find(idUsuario); // .Find(PK)
            }
        }

        public decimal ObterSaldoFichas(int idUsuario)
        {
            // Lógica ANTIGA:
            // var j = Db.Context.Jogadores.FirstOrDefault(x => x.IdUsuario == idUsuario);

            // Lógica NOVA:
            using (var ctx = new DataContext())
            {
                var j = ctx.Jogadores.FirstOrDefault(x => x.IdUsuario == idUsuario);
                return j != null ? j.Saldo : 0m;
            }
        }

        public bool Remover(int idUsuarioSelecionado, int idUsuarioLogado, out string mensagem)
        {
            if (idUsuarioLogado <= idUsuarioSelecionado)
            {
                mensagem = "Você não pode remover este usuário.";
                return false;
            }

            // Lógica ANTIGA:
            // var usuario = Db.Context.Usuarios.FirstOrDefault(u => u.IdUsuario == idUsuarioSelecionado);
            // ...
            // Db.Context.Jogadores.Remove(jogador);
            // Db.Context.Usuarios.Remove(usuario);

            // Lógica NOVA:
            using (var ctx = new DataContext())
            {
                var usuario = ctx.Usuarios.Find(idUsuarioSelecionado);
                if (usuario == null)
                {
                    mensagem = "Usuário não encontrado.";
                    return false;
                }
                var jogador = ctx.Jogadores.FirstOrDefault(j => j.IdUsuario == idUsuarioSelecionado);
                if (jogador != null)
                {
                    ctx.Jogadores.Remove(jogador);
                }
                ctx.Usuarios.Remove(usuario);

                ctx.SaveChanges(); // <-- ADICIONADO

                mensagem = "Usuário removido com sucesso.";
                return true;
            }
        }

        public bool Criar(string nomeCompleto, string nomeUsuario, string senha, CargoUsuario cargo, decimal saldoInicialJogador, out Usuario usuarioCriado, out string mensagem)
        {
            usuarioCriado = null;
            if (string.IsNullOrWhiteSpace(nomeCompleto) || string.IsNullOrWhiteSpace(nomeUsuario) || string.IsNullOrWhiteSpace(senha))
            {
                mensagem = "Nome, usuário e senha são obrigatórios.";
                return false;
            }

            // Lógica ANTIGA:
            // if (Db.Context.Usuarios.Any(u => u.NomeUsuario.Equals(nomeUsuario, StringComparison.OrdinalIgnoreCase)))

            // Lógica NOVA (apenas para a verificação):
            using (var ctx = new DataContext())
            {
                if (ctx.Usuarios.Any(u => u.NomeUsuario.Equals(nomeUsuario, StringComparison.OrdinalIgnoreCase)))
                {
                    mensagem = "Já existe um usuário com este login.";
                    return false;
                }
            }

            // O UsuarioService já foi migrado e cuida do seu próprio DataContext
            usuarioCriado = UsuarioService.CriarUsuario(nomeCompleto.Trim(), nomeUsuario.Trim(), senha, cargo);

            if (cargo == CargoUsuario.Jogador)
            {
                var j = new Jogador
                {
                    // IdJogador = Db.Context.NextJogadorId(), // <-- REMOVIDO
                    IdUsuario = usuarioCriado.IdUsuario, // O usuário já tem ID do banco
                    Apelido = nomeUsuario.Trim(),
                    Saldo = Math.Max(0m, saldoInicialJogador),
                    Email = string.Empty,
                    DataCriacao = DateTime.UtcNow
                };

                // Lógica ANTIGA:
                // Db.Context.Jogadores.Add(j);

                // Lógica NOVA (para salvar o Jogador associado):
                using (var ctx = new DataContext())
                {
                    ctx.Jogadores.Add(j);
                    ctx.SaveChanges();
                }
            }
            mensagem = "Usuário criado com sucesso.";
            return true;
        }

        public bool Atualizar(int idUsuarioAlvo, string nomeCompleto, string nomeUsuario, string novaSenhaOuVazia, CargoUsuario novoCargo, int idUsuarioLogado, out string mensagem)
        {
            // Lógica ANTIGA:
            // var u = Db.Context.Usuarios.FirstOrDefault(x => x.IdUsuario == idUsuarioAlvo);

            // Lógica NOVA (Todo o método deve estar em UM DataContext):
            using (var ctx = new DataContext())
            {
                var u = ctx.Usuarios.Find(idUsuarioAlvo);
                if (u == null)
                {
                    mensagem = "Usuário não encontrado.";
                    return false;
                }
                if (idUsuarioLogado <= idUsuarioAlvo && u.Cargo != novoCargo)
                {
                    mensagem = "Você não pode alterar o cargo deste usuário.";
                    return false;
                }

                // Lógica ANTIGA:
                // var existeLogin = Db.Context.Usuarios.Any(x => x.IdUsuario != idUsuarioAlvo && x.NomeUsuario.Equals(nomeUsuario, StringComparison.OrdinalIgnoreCase));
                // Lógica NOVA:
                var existeLogin = ctx.Usuarios.Any(x => x.IdUsuario != idUsuarioAlvo && x.NomeUsuario.Equals(nomeUsuario, StringComparison.OrdinalIgnoreCase));
                if (existeLogin)
                {
                    mensagem = "Já existe um usuário com este login.";
                    return false;
                }

                u.NomeCompleto = (nomeCompleto ?? string.Empty).Trim();
                u.NomeUsuario = (nomeUsuario ?? string.Empty).Trim();
                if (!string.IsNullOrEmpty(novaSenhaOuVazia))
                {
                    u.SenhaHash = UsuarioService.GerarHashSenha(novaSenhaOuVazia);
                }
                if (u.Cargo != novoCargo)
                {
                    u.Cargo = novoCargo;

                    // Lógica ANTIGA:
                    // var jogador = Db.Context.Jogadores.FirstOrDefault(j => j.IdUsuario == u.IdUsuario);
                    // Lógica NOVA:
                    var jogador = ctx.Jogadores.FirstOrDefault(j => j.IdUsuario == u.IdUsuario);
                    if (novoCargo == CargoUsuario.Jogador)
                    {
                        if (jogador == null)
                        {
                            jogador = new Jogador
                            {
                                // IdJogador = Db.Context.NextJogadorId(), // <-- REMOVIDO
                                IdUsuario = u.IdUsuario,
                                Apelido = u.NomeUsuario,
                                Saldo = 0m,
                                Email = string.Empty,
                                DataCriacao = DateTime.UtcNow
                            };
                            ctx.Jogadores.Add(jogador);
                        }
                    }
                }

                ctx.SaveChanges(); // <-- ADICIONADO
                mensagem = "Usuário atualizado com sucesso.";
                return true;
            }
        }

        // Este método não acessa Db.Context, ele chama 'Remover' e 'CarregarLista'
        // que já foram corrigidos ou não precisavam de correção.
        public void RemoverSelecionado(IWin32Window owner, ListBox listBox, Usuario usuarioLogado)
        {
            if (usuarioLogado == null)
            {
                MessageBox.Show("Usuário logado não identificado.");
                return;
            }
            int id = ObterIdSelecionado(listBox);
            if (id <= 0)
            {
                MessageBox.Show("Selecione um usuário.");
                return;
            }
            if (MessageBox.Show("Confirmar remoção?", "Remover", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            // 'Remover' agora usa o novo DataContext
            if (Remover(id, usuarioLogado.IdUsuario, out string msg))
            {
                MessageBox.Show(msg);
                // 'CarregarLista' agora usa o novo DataContext (indiretamente via Listar)
                CarregarLista(listBox);
            }
            else
            {
                MessageBox.Show(msg);
            }
        }
    }
}