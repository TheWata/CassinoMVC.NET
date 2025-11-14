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
        // ADICIONADO: Instância do serviço
        private readonly UsuarioService _usuarioService = new UsuarioService();

        // ... (Classe UsuarioListItem permanece igual) ...
        public class UsuarioListItem
        {
            public int Id { get; set; }
            public string Texto { get; set; }
            public override string ToString() => Texto;
        }


        public Usuarios Listar(string filtro = null)
        {
            using (var ctx = new DataContext())
            {
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

        // ... (Métodos ListarFormatado, ListarItems, CarregarLista, ObterIdSelecionado permanecem iguais) ...

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
            using (var ctx = new DataContext())
            {
                return ctx.Usuarios.Find(idUsuario); // .Find(PK)
            }
        }

        public decimal ObterSaldoFichas(int idUsuario)
        {
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

                ctx.SaveChanges();
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

            using (var ctx = new DataContext())
            {
                if (ctx.Usuarios.Any(u => u.NomeUsuario.Equals(nomeUsuario, StringComparison.OrdinalIgnoreCase)))
                {
                    mensagem = "Já existe um usuário com este login.";
                    return false;
                }
            }

            // MODIFICADO: Chamando o método da *instância*
            usuarioCriado = _usuarioService.CriarUsuario(nomeCompleto.Trim(), nomeUsuario.Trim(), senha, cargo);

            if (cargo == CargoUsuario.Jogador)
            {
                var j = new Jogador
                {
                    IdUsuario = usuarioCriado.IdUsuario,
                    Apelido = nomeUsuario.Trim(),
                    Saldo = Math.Max(0m, saldoInicialJogador),
                    Email = string.Empty,
                    DataCriacao = DateTime.UtcNow
                };

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
                    // MODIFICADO: Chamando o método da *instância*
                    u.SenhaHash = _usuarioService.GerarHashSenha(novaSenhaOuVazia);
                }
                if (u.Cargo != novoCargo)
                {
                    u.Cargo = novoCargo;
                    var jogador = ctx.Jogadores.FirstOrDefault(j => j.IdUsuario == u.IdUsuario);
                    if (novoCargo == CargoUsuario.Jogador)
                    {
                        if (jogador == null)
                        {
                            jogador = new Jogador
                            {
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

                ctx.SaveChanges();
                mensagem = "Usuário atualizado com sucesso.";
                return true;
            }
        }

        // ... (Método RemoverSelecionado permanece igual) ...
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

            if (Remover(id, usuarioLogado.IdUsuario, out string msg))
            {
                MessageBox.Show(msg);
                CarregarLista(listBox);
            }
            else
            {
                MessageBox.Show(msg);
            }
        }
    }
}