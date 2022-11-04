using Microsoft.EntityFrameworkCore;
using Vives.Services.Model;
using VivesRental.Model;
using VivesRental.Repository.Core;
using VivesRental.Services.Abstractions;
using VivesRental.Services.Extensions;
using VivesRental.Services.Mappers;
using VivesRental.Services.Model.Filters;
using VivesRental.Services.Model.Results;

namespace VivesRental.Services;

public class OrderService : IOrderService
{
    private readonly VivesRentalDbContext _context;

    public OrderService(VivesRentalDbContext context)
    {
        _context = context;
    }

    public Task<OrderResult?> GetAsync(Guid id)
    {
        return _context.Orders
            .Where(o => o.Id == id)
            .MapToResults()
            .FirstOrDefaultAsync();
    }

    public Task<List<OrderResult>> FindAsync(OrderFilter? filter = null)
    {
        return _context.Orders
            .ApplyFilter(filter)
            .MapToResults()
            .ToListAsync();
    }
        
    public async Task<ServiceResult<OrderResult?>> CreateAsync(Guid customerId)
    {
        var customer = await _context.Customers
            .Where(c => c.Id == customerId)
            .FirstOrDefaultAsync();

        if (customer == null)
        {
            return null;
        }

        var order = new Order
        {
            CustomerId = customer.Id,
            CustomerFirstName = customer.FirstName,
            CustomerLastName = customer.LastName,
            CustomerEmail = customer.Email,
            CustomerPhoneNumber = customer.PhoneNumber,
            CreatedAt = DateTime.Now
        };

        _context.Orders.Add(order);
        var numberOfObjectsUpdated = await _context.SaveChangesAsync();
        if (numberOfObjectsUpdated > 0)
        {
            var result = await GetAsync(order.Id);
            return new ServiceResult<OrderResult?>(result);
        }
        
        var serviceResult = new ServiceResult<OrderResult>();
        serviceResult.Messages.Add(new ServiceMessage
        {
            Code = "No changes",
            Message = "nothing changed in database",
            Type = ServiceMessageType.Error
        });
        return serviceResult;
    }

    public async Task<ServiceResult> ReturnAsync(Guid orderId, DateTime returnedAt)
    {
        var orderLines = await _context.OrderLines
            .Where(ol => ol.OrderId == orderId && !ol.ReturnedAt.HasValue)
            .ToListAsync();
        foreach (var orderLine in orderLines)
        {
            orderLine.ReturnedAt = returnedAt;
        }

        var numberOfObjectsUpdated = await _context.SaveChangesAsync();
        if (numberOfObjectsUpdated is 0)
        {
            var serviceResult = new ServiceResult();
            serviceResult.Messages.Add(new ServiceMessage
            {
                Code = "NothingChanged",
                Message = "no changes is database",
                Type = ServiceMessageType.Error
            });
            return serviceResult;
        }

        return new ServiceResult();
    }
}