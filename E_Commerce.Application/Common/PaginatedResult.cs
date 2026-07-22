using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Common
{
    public class  PaginatedResult <TEntity>
    {
        public PaginatedResult(int count, int pageSize, int pageIndex, IReadOnlyList<TEntity> date)
        {
            this.count=count;
            PageSize=pageSize;
            PageIndex=pageIndex;
            Date=date;
        }

        public int count {  get;}
        public  int PageSize { get;}
        public int PageIndex { get;}
        public IReadOnlyList<TEntity> Date { get;}
    }
}
