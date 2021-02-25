using LVModel;

namespace WebAppAWListaVerificacao.Models
{
    public class CabecalhoApp
    {

   

        

        public CabecalhoViewModel GetCabecalhoViewModel()
        {




            var cabecalho = new CabecalhoViewModel();


            setImagens(cabecalho);

            cabecalho.Funcao = "FUNÇÃO";
            cabecalho.Titulo = "NOME PLANILHA VERIFICACAO";
            cabecalho.Disciplina = "DISCIPLINA";

            return cabecalho;
        }

        public CabecalhoViewModel GetCabecalhoViewModel(Planilha planilha)
        {

   

            var cabecalho = new CabecalhoViewModel();

            setImagens(cabecalho);

         
            

            cabecalho.Funcao = planilha.NOME;
            cabecalho.Titulo = planilha.DESCRICAO;

    
            cabecalho.Disciplina = planilha.Tipo.Configuracao.Disciplina.NOME;
            cabecalho.NumeroDocumento = "NUMERO DOC. VERIFICADO";


            return cabecalho;
        }




        public CabecalhoViewModel GetCabecalhoViewModel(ListaVerificacao documento)
        {
            var cabecalho = new CabecalhoViewModel();

            setImagens(cabecalho);


            cabecalho.Funcao = documento.Planilha.NOME;
            cabecalho.Titulo = documento.Planilha.DESCRICAO;

          
            cabecalho.Disciplina = documento.Planilha.Tipo.Configuracao.NOME;
            cabecalho.NumeroDocumento = documento.DOC_VERIFICADO; 
       

            return cabecalho;
        }

        private void setImagens(CabecalhoViewModel cabecalho)
        {
            cabecalho.ImagePath = "~/imagens/logo_snc_lavalin.png";
            
        }


       
    }
}