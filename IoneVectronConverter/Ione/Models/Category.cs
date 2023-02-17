﻿namespace IoneVectronConverter.Ione.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        public int VectronNo { get; set; }
        public int IoneRefId { get; set; }
        
        public bool IsSent { get; set; }


    }
}
