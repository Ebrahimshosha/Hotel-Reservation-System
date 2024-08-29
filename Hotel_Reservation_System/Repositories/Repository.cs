using Hotel_Reservation_System.Data;
using Hotel_Reservation_System.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Hotel_Reservation_System.Repositories;

public class Repository<T> : IRepository<T> where T : BaseModel
{
	private readonly DbContext _context;

	public Repository(StoreContext context)
    {
        _context = context;
    }

    public IQueryable<T> GetAll()
    {
        return _context.Set<T>().Where(x => !x.IsDeleted).AsNoTracking();
    }

    public T GetByID(int id)
    {
        return _context.Set<T>().FirstOrDefault(x => x.Id == id && !x.IsDeleted)!;
    }


    public T GetWithTrackinByID(int id)
    {
        return _context.Set<T>()
            .Where(x => !x.IsDeleted && x.Id == id)
            .AsTracking()
            .FirstOrDefault()!;
    }

    public IQueryable<T> Get(Expression<Func<T, bool>> predicate)
    {
        return GetAll().Where(predicate);
    }

    public T First(Expression<Func<T, bool>> predicate)
    {
        return Get(predicate).FirstOrDefault()!;
    }

    public T Add(T entity)
    {
        _context.Set<T>().Add(entity);
        return entity;
    }

    public T Update(T entity)
    {
        _context.Set<T>().Update(entity);
        return entity;
    }

    public void Delete(T entity)
    {
        entity.IsDeleted = true;
        Update(entity); // This line because change tracker maybe disable 
    }

    public void Delete(int id)
    {
        T entity = _context.Find<T>(id)!;
        Delete(entity);
    }

    public void HardDelete(int id)
    {
        _context.Set<T>().Where(x => x.Id == id).ExecuteDelete();
    }

    public void SaveChanges()
    {
        _context.SaveChanges();
    }
}
