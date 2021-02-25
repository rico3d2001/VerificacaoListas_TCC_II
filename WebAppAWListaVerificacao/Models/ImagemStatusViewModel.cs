using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppAWListaVerificacao.Models
{
    public class ImagemStatusViewModel
    {
        string _imagePathV;
        string _imagePathX;
        string _imagePathNA;
        string _imagePathND;
        string _imagePathI;

        public string ImagePathV { get { return _imagePathV; } set { _imagePathV = value; } }
        public string ImagePathX { get { return _imagePathX; } set { _imagePathX = value; } }
        public string ImagePathNA { get { return _imagePathNA; } set { _imagePathNA = value; } }
        public string ImagePathND { get { return _imagePathND; } set { _imagePathND = value; } }
        public string ImagePathI { get { return _imagePathI; } set { _imagePathI = value; } }
    }
}