﻿using Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.DTOs.Articles
{
    public class ArticleListDto
    {
        public IList<Article> Articles { get; set; } 
        public Guid? CategoryId { get; set; }   
        public virtual int CurrentPage { get; set; }    
        public virtual int PageSize { get; set;}
        public virtual int TotalCount { get; set;}
        public virtual int TotalPages => (int)Math.Ceiling(decimal.Divide(TotalCount, PageSize));
        public virtual bool ShowPrevious => CurrentPage > 1;
        public virtual bool ShowNext => CurrentPage < TotalPages;
        public virtual bool IsAscending { get; set; } = false;
    }
}
