using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LVRepositorioDb4o
{
    public class LV_VIEW_ITENS_REV
    {
        string _guid;

        [GUIDAtributo]
        public virtual string GUID_ITEM { get => _guid; set => _guid = value; }

        public virtual string GUID_PLANILHA { get; set; }
        public virtual int ORDENADOR_ITEM { get; set; }
        public virtual int ORDENADOR_GRUPO { get; set; }
        public virtual string NOME_GRUPO { get; set; }
        public virtual string GUID_GRUPO { get; set; }
        public virtual string DESCRICAO { get; set; }

        public LV_VIEW_ITENS_REV() { }

        //public List<IEntityView> GetByProperty(string propertyName, object value)
        //{

        //    List<IEntityView> lista = new List<IEntityView>();

        //    List<LV_PLANILHA> lv_PLANILHA = 
        //        new Repository<LV_PLANILHA>().GetByProperty(propertyName, value).ToList();

        //    foreach (var lv in lv_PLANILHA)
        //    {
        //        LV_VIEW_ITENS_REV lV_VIEW = new LV_VIEW_ITENS_REV();
        //        lV_VIEW.GUID_ITEM = _guid;
        //        LV_ITEM_REVISAO lv_ITEM_REVISAO = new RepositoryGUID<LV_ITEM_REVISAO>().ReturnByGUID(lV_VIEW.GUID_ITEM);
        //        lV_VIEW.GUID_GRUPO = lv_ITEM_REVISAO.GUID_GRUPO;
        //        LV_GRUPO lv_GRUPO = new RepositoryGUID<LV_GRUPO>().ReturnByGUID(lV_VIEW.GUID_GRUPO);
        //        lV_VIEW.GUID_PLANILHA = lv_GRUPO.GUID_PLANILHA;
        //        lV_VIEW.NOME_GRUPO = lv_GRUPO.NOME;
        //        lV_VIEW.ORDENADOR_GRUPO = lv_GRUPO.ORDENADOR;

        //        lista.Add(lV_VIEW);

        //    }



        //    return lista;
        //}
    }
}
