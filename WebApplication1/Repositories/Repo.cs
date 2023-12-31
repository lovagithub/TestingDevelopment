﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System.Diagnostics;
using System.Linq.Expressions;

namespace WebApplication1.Repositories;

public interface IRepo<TEntity, TDbContext> where TEntity : class where TDbContext : DbContext 
{   
    Task<bool> ExistAsync(Expression<Func<TEntity, bool>> expression);
    Task<TEntity> CreteAsync(TEntity entity);
    Task<TEntity> ReadAsync(Expression<Func<TEntity, bool>> expression);
    Task<IEnumerable<TEntity>> ReadAllAsync();
    Task<bool> DeleteAsync(TEntity entity);
    
}

public abstract class Repo<TEntity, TDbContext> : IRepo<TEntity, TDbContext> where TEntity : class where TDbContext : DbContext
{
    private readonly TDbContext _context;

    protected Repo(TDbContext context)
    {
        _context = context;
    }

    public async Task<bool> ExistAsync(Expression<Func<TEntity, bool>> expression)
    {
        try
        {
            return await _context.Set<TEntity>().AllAsync(expression);
        }
        catch (Exception ex) 
        { 
           Debug.WriteLine(ex.Message);
            return false;
        }
    }
  
   
    public async Task<TEntity> CreteAsync(TEntity entity)
    {
        try
        {
            await _context.Set<TEntity>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return null!;
        }
    }
    public async Task<TEntity> ReadAsync(Expression<Func<TEntity, bool>> expression)
    {

        try
        {
            return await _context.Set<TEntity>().FirstOrDefaultAsync(expression) ?? null!;

        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return null!;
        }
    }
    public async Task<IEnumerable<TEntity>> ReadAllAsync()
    {
        try
        {
            return await _context.Set<TEntity>().ToListAsync();

        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return null!;
        }
    }
public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        throw new NotImplementedException();
    }
    public async Task<bool> DeleteAsync(TEntity entity)
    {
        throw new NotImplementedException();
    }
}

