using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASP.NET.ProjectTime.DataContext;
using ASP.NET.ProjectTime.Models;

namespace ASP.NET.ProjectTime._1._Repositories
{
    public static class Repository
    {

        public static T GetById<T>(this IEnumerable<T> itemList, Func<T, string> getIdFunc, string id)
        {
            return itemList.FirstOrDefault(item => getIdFunc(item) == id);
        }
        
        // public static T GetById<T>(this IEnumerable<T> itemList, Guid id) where T : class
        // {
        //     return itemList.FirstOrDefault(item => item.GetType().GetProperty("Id")?.GetValue(item) as Guid? == id);
        // }
        
        // public static T GetById<T>(this IEnumerable<T> itemList, string id) where T : class
        // {
        //     return itemList.FirstOrDefault(item => item.GetType().GetProperty("Id")?.GetValue(item)?.ToString() == id);
        // }

        
        // public static T GetById<T>(this IEnumerable<T> itemList, string id) where T : class
        // {
        //     foreach (var item in itemList)
        //     {
        //         var itemIdProperty = item.GetType().GetProperty("Id");
        //         if (itemIdProperty != null)
        //         {
        //             var itemIdValue = itemIdProperty.GetValue(item)?.ToString();
        //             if (itemIdValue == id)
        //             {
        //                 return item;
        //             }
        //         }
        //     }
        //     return null;
        // }

        
        // public DataContext.DataContext context;
        //
        // private Save Entities => context.Set<T>();
        //
        // public T GetById(string id)
        // {
        //     return Entities.FirstOrDefault(e => e.Id == id);
        // }
        //
        //
        // public void Add(T entity)
        // {
        //     Entities.Add(entity);
        // }
        //
        // public void Modify(T entity)
        // {
        //     for (var i = 0; i < Entities.Count; i++)
        //     {
        //         if (Entities[i].Id == entity.Id)
        //         {
        //             Entities[i] = entity;
        //         }
        //     }
        // }
        //
        // public void Delete(T entity)
        // {
        //     // Entities.Remove(entity);
        // }
        //
        // public async Task Save()
        // {
        //     await context.Save();
        // }
    }
}