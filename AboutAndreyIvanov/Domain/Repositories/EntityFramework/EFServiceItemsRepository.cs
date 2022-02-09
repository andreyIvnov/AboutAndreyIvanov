using AboutAndreyIvanov.Domain.Entittes;
using AboutAndreyIvanov.Domain.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace AboutAndreyIvanov.Domain.Repositories.EntityFramework
{
    public class EFServiceItemsRepository : IServiceItemsRepository
    {
        private readonly AppDbContext _context;
        public EFServiceItemsRepository(AppDbContext context)
        {
            _context = context;
        }


        public IQueryable<ServiceItem> GetServiceItems()
        {
            return _context.SeviceItems;
        }
        public ServiceItem GetServiceItemdById(Guid id)
        {
            return _context.SeviceItems.FirstOrDefault(x => x.Id == id);
        }
        public void SaveServiceItem(ServiceItem entity)
        {
            if (entity.Id == default)
                _context.Entry(entity).State = EntityState.Added;
            else
                _context.Entry(entity).State = EntityState.Modified;

            _context.SaveChanges();
        }
        public void DeleteServiceItem(Guid id)
        {
            _context.Remove(new ServiceItem() { Id = id });
            _context.SaveChanges();
        }
    }
}
