using System.Linq.Expressions;
using AutomicObjectDesignerBack.Data;
using Microsoft.EntityFrameworkCore;

namespace AutomicObjectDesignerBack.Repository;

public class RepositoryBase<T> : IRepositoryBase<T> where T : class
{
    public AppDatabaseContext _context { get; set; }

    public RepositoryBase(AppDatabaseContext databaseContext)
    {
        _context = databaseContext;
    }
    public IQueryable<T> GetAll()
    {
        return _context.Set<T>().AsNoTracking();
    }

    public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
    {
        return _context.Set<T>().Where(expression).AsNoTracking();
    }


    public void Create(T entity)
    {
        _context.Set<T>().Add(entity);
    }

    public void Delete(T entity)
    {
        _context.Set<T>().Remove(entity);
    }

    public void Update(T entity)
    {
        _context.Set<T>().Update(entity);
    }

    public async Task Save()
    {
        await _context.SaveChangesAsync();
    }
}