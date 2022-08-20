using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebsiteBanHang.Models
{
    public class Pagers
    {
        public int TotalItems { get;private set; }
        public int CurrentPage { get;private set; }
        public int PageSize { get;private set; }
        public int TotalPages { get;private set; }
        public int StartPages { get;private set; }
        public int EndPages { get;private set; }

        public int StartRecord { get;private set; }
        public int EndRecord { get;private set; }
          

        public Pagers()
        {

        }
        public Pagers(int totalItems,int page,int pageSize = 10)
        {
            int totalPages = (int)Math.Ceiling((decimal)totalItems / (decimal)pageSize);
            int currentPage = page;

            int startPage = currentPage - 5;
            int endPage = currentPage + 4;

            if(endPage > totalPages)
            {
                currentPage = totalPages;
                if(endPage>10)
                {
                    startPage = endPage - 9;
                }
            }
            TotalItems= totalItems;
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalPages = totalPages;
            StartPages = startPage;
            EndPages = endPage;
        }
    }
}