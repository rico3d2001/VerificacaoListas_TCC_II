
using AppExcel.AppWeb;
using AutoMapper;
using LVModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAppAWListaVerificacao.Models;

namespace WebAppAWListaVerificacao.Mappers
{
    public class AutoMapperProfile:Profile
    {

        public AutoMapperProfile()
        {
            CreateMap<Projeto, ProjetoViewModel>();
            CreateMap<Confirmacao, ConfirmacaoViewModel>();

        }
        

    }
}