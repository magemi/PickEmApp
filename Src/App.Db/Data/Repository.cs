using App.Core.Entities;
using App.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace App.Db.Data
{
  public class Repository<T> : IRepository<T> where T : EntityBase
  {
    protected readonly AppDbContext _context;

    public Repository(AppDbContext context)
    {
      _context = context;
    }

    public virtual T GetById(int id)
    {
      return _context.Set<T>().Find(id);
    }

    public T Create(T entity)
    {
      _context.Set<T>().Add(entity);
      _context.SaveChanges();

      return entity;
    }

    public IEnumerable<T> Read()
    {
      return _context.Set<T>().AsEnumerable();
    }

    public IEnumerable<T> Read(ISpecification<T> spec)
    {
      var queryableResultWithIncludes = spec.Includes.Aggregate(_context.Set<T>().AsQueryable(), (current, include) => current.Include(include));
      var secondaryResult = spec.IncludeStrings.Aggregate(queryableResultWithIncludes, (current, include) => current.Include(include));
      return secondaryResult.Where(spec.Criteria).AsEnumerable();
    }

    public void Update(T entity)
    {
      _context.Entry(entity).State = EntityState.Modified;
      _context.SaveChanges();
    }

    public void Delete(T entity)
    {
      _context.Set<T>().Remove(entity);
      _context.SaveChanges();
    }
  }
}