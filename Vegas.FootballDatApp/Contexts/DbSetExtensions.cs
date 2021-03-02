using System.Linq;
using Microsoft.EntityFrameworkCore;
using Vegas.FootballDatApp.Entities;

namespace Vegas.FootballDatApp.Contexts
{
    public static class DbSetExtensions
    {
        public static void AddOrUpdate<TEntity>(this DbSet<TEntity> dbSet, TEntity entity)
            where TEntity : EntityBase
        {
            if (dbSet.Any(x => x.Id == entity.Id))
            {
                dbSet.Update(entity);
            }
            else
            {
                dbSet.Add(entity);
            }
        }
    }
}
