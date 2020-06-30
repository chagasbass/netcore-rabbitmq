namespace NetCoreMicroservices.Banco.Dominio.Contextos.ContasCorrente.Comandos
{
    public class CriarTransferenciaComando: TransferenciaComando
    {
        public CriarTransferenciaComando(int contaDe,int contaPara,decimal valorTransferencia)
        {
            ContaDe = contaDe;
            ContaPara = contaPara;
            ValorTransferencia = valorTransferencia;
        }
    }
}
