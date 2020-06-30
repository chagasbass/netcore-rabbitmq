namespace NetCoreMicroservices.Banco.Dominio.Contextos.ContasCorrente.Entidades
{
    public class ContaCorrente
    {
        public int Id { get;  set; }
        public string TipoDeConta { get;  set; }
        public decimal Saldo { get;  set; }

        public ContaCorrente(string tipoDeConta, decimal saldo)
        {
            TipoDeConta = tipoDeConta;
            Saldo = saldo;
        }
    }
}
