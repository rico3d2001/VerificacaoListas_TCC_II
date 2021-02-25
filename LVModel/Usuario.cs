namespace LVModel
{
    public class Usuario
    {

        private string sigla;
        private string senha;
        private string guid;
        private int _isVerificador;
        private int _isGestor;
        private int _isConfigurador;
        private string nome;


        public virtual bool GetBoolIsVerificador() { return _isVerificador == 1 ? true : false; }
        public virtual void SetBoolIsVerificador(bool se) { _isVerificador = (se == true ? 1 : 0); }

        public virtual bool GetBoolIsGestor() { return _isGestor == 0 ? false : true; }
        public virtual void SetBoolIsGestor(bool se) { _isGestor = (se == true ? 1 : 0); }

        public virtual bool GetBoolIsConfigurador() { return _isConfigurador == 0 ? false : true; }
        public virtual void SetBoolIsConfigurador(bool se) { _isConfigurador = (se == true ? 1 : 0); }



       

        public virtual string GUID { get => guid; set => guid = value; }
        public virtual string NOME { get => nome; set => nome = value; }
        public virtual int ISVERIFICADOR { get => _isVerificador; set => _isVerificador = value; }
        public virtual int ISGESTOR { get => _isGestor; set => _isGestor = value; }
        public virtual int ISCONFIGURADOR { get => _isConfigurador; set => _isConfigurador = value; }
        public virtual string SIGLA { get => sigla; set => sigla = value; }
        public virtual string SENHA { get => senha; set => senha = value; }



        public Usuario()
        {

        }


        private int BooleanoToInt(bool boleano)
        {
            switch (boleano)
            {
                case false:
                    return 0;
                case true:
                    return 1;
                default:
                    return 0;
            }
        }

        private bool IntToBooleano(int inteiro)
        {
            switch (inteiro)
            {
                case 0:
                    return false;
                case 1:
                    return true;
                default:
                    return false;
            }
        }


        public virtual bool VerficaSenha(string senhaDigitada)
        {


            if (this.senha.Equals(senhaDigitada))
            {
                return true;
            }


            return false;

        }



        private string getSenhaPadrao()
        {
            return "cliente123";
        }


        //public virtual bool PodeVerificar()
        //{
        //    return string.IsNullOrEmpty(guid) || _isVerificador == 0 ? false : true;
        //}


    }
}
