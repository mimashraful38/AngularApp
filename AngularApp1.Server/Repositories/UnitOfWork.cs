﻿using AngularApp1.Server.DB;
using AngularApp1.Server.Repositories.ProductRepo;
using AngularApp1.Server.Repositories;
using AngularApp1.Server.Repositories.CustomerRepo;
using AngularApp1.Server.Repositories.OrderRepo;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    private IProductRepository _productRepository;
    private ICustomerRepository _customerRepository;
    private IOrderRepository _orderRepository;
    private IOrderDtlsRepository _orderDtlsRepository;


    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }

    public IProductRepository Product => _productRepository ??= new ProductRepository(_context);
    public ICustomerRepository Customer => _customerRepository ??= new CustomerRepository(_context);

    public IOrderRepository Order => _orderRepository ??= new OrderRepository(_context);
    public IOrderDtlsRepository OrderDtls => _orderDtlsRepository ??= new OrderDtlsRepository(_context);

    public int SaveChanges()
    {
        return _context.SaveChanges();
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }

    public void BeginTransaction()
    {
        _context.Database.BeginTransaction();
    }

    public void CommitTransaction()
    {
        _context.Database.CommitTransaction();
    }

    public void RollbackTransaction()
    {
        _context.Database.RollbackTransaction();
    }
}
