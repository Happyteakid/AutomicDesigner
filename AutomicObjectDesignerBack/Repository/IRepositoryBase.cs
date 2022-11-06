using System.Linq.Expressions;
using AutomicObjectDesigner.Models.Objects;

namespace AutomicObjectDesignerBack.Repository;

public interface IRepositoryBase<T>
{
    IQueryable<T> GetAll();
    IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);
    void Create(T entity);
    void Delete(T entity);

    void Update(T entity);
    Task Save();
}