using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using UdpServer.Core.Data.Source.Local.Dal.DataContext;

namespace UdpServer.Services.Services.Repository;
public class Repository<T>(DataContext data) where T : class
{
    private readonly DataContext data = data;
    public void Add([DisallowNull] T item)
    {
        this.data.Set<T>().Add(item);
        this.data.SaveChanges();
    }
    public void Remove([DisallowNull] T item)
    {
        this.data.Set<T>().Remove(item);
        this.data.SaveChanges();
    }
    public List<T> GetAll() => this.data.Set<T>().ToList();
    public T? Find(Expression<Func<T, bool>> predicate) => this.data.Set<T>().Find(predicate);
    public List<T> FindAll(Expression<Func<T, bool>> predicate) => this.data.Set<T>().Where(predicate).ToList();
}
