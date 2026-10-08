using System.Net.Mail;
using CadeMeu.Data;
using CadeMeu.Exceptions;
using CadeMeu.Models;

namespace CadeMeu.Services
{
    /// <summary>
    /// Regras de negócio do sistema. É a única classe que os formulários conhecem.
    /// Valida tudo ANTES de chamar os repositórios, para nunca mandar dado inválido ao banco.
    /// Quando uma regra é violada, lança RegraNegocioException com uma mensagem para o usuário.
    /// </summary>
    public class EmprestimoService
    {
        // Mesmos tamanhos das colunas do banco (Database/CriarBanco.sql).
        private const int TamanhoMaximoItem = 100;
        private const int TamanhoMaximoNome = 100;
        private const int TamanhoMaximoEmail = 150;

        private readonly EmprestimoRepository _emprestimoRepository = new();
        private readonly AmigoRepository _amigoRepository = new();

        public List<Emprestimo> ListarEmprestimos(bool incluirDevolvidos)
        {
            return _emprestimoRepository.Listar(incluirDevolvidos);
        }

        public List<Amigo> ListarAmigos()
        {
            return _amigoRepository.ListarTodos();
        }

        public void RegistrarParaAmigoExistente(Emprestimo emprestimo)
        {
            ValidarEmprestimo(emprestimo);

            if (emprestimo.AmigoId <= 0)
            {
                throw new RegraNegocioException("Selecione o amigo que pegou o item emprestado.");
            }

            _emprestimoRepository.Inserir(emprestimo);
        }

        public void RegistrarParaNovoAmigo(Emprestimo emprestimo, Amigo novoAmigo)
        {
            ValidarEmprestimo(emprestimo);
            ValidarAmigo(novoAmigo);

            // Amigo + empréstimo são gravados juntos, dentro de uma transação.
            _emprestimoRepository.InserirComNovoAmigo(novoAmigo, emprestimo);
        }

        public void MarcarComoDevolvido(int emprestimoId)
        {
            Emprestimo emprestimo = _emprestimoRepository.BuscarPorId(emprestimoId)
                ?? throw new RegraNegocioException("Empréstimo não encontrado. Atualize a lista (F5).");

            if (emprestimo.FoiDevolvido)
            {
                throw new RegraNegocioException(
                    $"\"{emprestimo.Item}\" já foi devolvido em {emprestimo.DataDevolucao:dd/MM/yyyy}.");
            }

            bool atualizou = _emprestimoRepository.RegistrarDevolucao(emprestimoId, DateTime.Today);

            // Pode acontecer se outra janela marcou a devolução entre a busca e o UPDATE.
            if (!atualizou)
            {
                throw new RegraNegocioException($"\"{emprestimo.Item}\" já estava marcado como devolvido.");
            }
        }

        private static void ValidarEmprestimo(Emprestimo emprestimo)
        {
            emprestimo.Item = emprestimo.Item.Trim();

            if (emprestimo.Item.Length == 0)
            {
                throw new RegraNegocioException("Informe qual item foi emprestado.");
            }

            if (emprestimo.Item.Length > TamanhoMaximoItem)
            {
                throw new RegraNegocioException($"O nome do item pode ter no máximo {TamanhoMaximoItem} caracteres.");
            }

            if (emprestimo.DataEmprestimo.Date > DateTime.Today)
            {
                throw new RegraNegocioException("A data do empréstimo não pode estar no futuro.");
            }

            if (emprestimo.DataDevolucaoPrevista.HasValue
                && emprestimo.DataDevolucaoPrevista.Value.Date < emprestimo.DataEmprestimo.Date)
            {
                throw new RegraNegocioException(
                    "A data combinada para devolução não pode ser anterior à data do empréstimo.");
            }
        }

        private static void ValidarAmigo(Amigo amigo)
        {
            amigo.Nome = amigo.Nome.Trim();
            amigo.Email = string.IsNullOrWhiteSpace(amigo.Email) ? null : amigo.Email.Trim();

            if (amigo.Nome.Length == 0)
            {
                throw new RegraNegocioException("Informe o nome do amigo.");
            }

            if (amigo.Nome.Length > TamanhoMaximoNome)
            {
                throw new RegraNegocioException($"O nome do amigo pode ter no máximo {TamanhoMaximoNome} caracteres.");
            }

            // Telefone com DDD: 10 dígitos (fixo) ou 11 dígitos (celular).
            bool telefoneValido = amigo.Telefone.Length is 10 or 11 && amigo.Telefone.All(char.IsDigit);
            if (!telefoneValido)
            {
                throw new RegraNegocioException("Informe o telefone do amigo com DDD.");
            }

            if (amigo.Email != null)
            {
                if (amigo.Email.Length > TamanhoMaximoEmail || !MailAddress.TryCreate(amigo.Email, out _))
                {
                    throw new RegraNegocioException("O e-mail informado não é válido.");
                }
            }
        }
    }
}
