namespace LVModel
{
    public class OS
    {

        public OS()
        {

        }

        public virtual string GUID { get; set; }
        public virtual string NUMERO { get; set; }
        //public virtual string GUID_PROJETO { get; set; }

        public virtual Projeto Projeto { get; set; }
    }
}
