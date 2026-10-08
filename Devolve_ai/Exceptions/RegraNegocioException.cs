namespace CadeMeu.Exceptions
{
    /// <summary>
    /// Lançada pelo Service quando uma regra do sistema é violada
    /// (ex.: data combinada antes do empréstimo).
    /// Tem um tipo próprio para a tela diferenciar "erro do usuário" de "erro técnico":
    /// a mensagem desta exceção é escrita para ser mostrada ao usuário.
    /// </summary>
    public class RegraNegocioException : Exception
    {
        public RegraNegocioException(string mensagem) : base(mensagem)
        {
        }
    }
}
