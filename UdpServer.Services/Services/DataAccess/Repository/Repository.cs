using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using UdpServer.Core.Data.Source.Dal.DataContext;

namespace UdpServer.Services.Services.DataAccess.Repository;
public abstract class Repository<T>(DataContext data) where T : class
{
    private readonly DataContext data = data;
    public void Add([DisallowNull] T item)
    {
        data.Set<T>().Add(item);
        data.SaveChanges();
    }
    public void Remove([DisallowNull] T item)
    {
        data.Set<T>().Remove(item);
        data.SaveChanges();
    }
    public void SaveChanges() => data.SaveChanges();
    public List<T> GetAll() => data.Set<T>().ToList();
    public T? Find(Expression<Func<T, bool>> predicate) => data.Set<T>().FirstOrDefault(predicate);
    public List<T> FindAll(Expression<Func<T, bool>> predicate) => data.Set<T>().Where(predicate).ToList();
}
