using GameStore.Domain.Interfaces;
using GameStore.Infrastructure.Data.Context;

namespace GameStore.Infrastructure.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly GameStoreContext _context;

        public UnitOfWork(GameStoreContext gameStoreContext)
        {
            _context = gameStoreContext;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }
    }
}
