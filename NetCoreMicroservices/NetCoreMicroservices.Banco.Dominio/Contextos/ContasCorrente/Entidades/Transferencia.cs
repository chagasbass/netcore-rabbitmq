namespace NetCoreMicroservices.Banco.Dominio.Contextos.ContasCorrente.Entidades
{
    public class Transferencia
    {
        public int ContaDe { get; set; }
        public int ContaPara { get; set; }
        public decimal ValorTranferencia { get; set; }

        public Transferencia()
        {

        }

    }
}
