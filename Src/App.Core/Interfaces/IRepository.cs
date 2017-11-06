using App.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.Core.Interfaces
{
  public interface IRepository<T> where T : EntityBase
  {
    T GetById(int id);
    T Create(T entity);
    IEnumerable<T> Read();
    IEnumerable<T> Read(ISpecification<T> spec);
    void Update(T entity);
    void Delete(T entity);
  }
}