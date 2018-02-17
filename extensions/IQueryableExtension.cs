using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using angular_udemy_demo.Models;
using angular_udemy_demo.ViewModels;

namespace angular_udemy_demo.extensions
{
    public static class IQueryableExtension
    {
         public static IQueryable<T> ApplySorting<T>(this IQueryable<T> query, IQueryObject filter, Dictionary<string,Expression<Func<T,object>>> columnMap)
         {
            // foreach(var property in filter.GetType().GetProperties())
            // {
            //     if(property.Name=="")
            // }
            
            if(string.IsNullOrWhiteSpace(filter.SortBy) || !columnMap.ContainsKey(filter.SortBy))
                return query;

            if(filter.IsSortAscending){
                return query.OrderBy(columnMap[filter.SortBy]); 
            }
            else
            {
                return query.OrderByDescending(columnMap[filter.SortBy]); 
            }  
        }
        public static IQueryable<T> ApplyPaging<T>(this IQueryable<T> query, IQueryObject filter)
        {
            if(filter.Page<=0) 
                filter.Page=1;
            if(filter.PageSize<=0) 
                filter.PageSize=5;
            return query.Skip((filter.Page-1)*filter.PageSize).Take(filter.PageSize);
        }
    }
}