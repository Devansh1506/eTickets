using eTickets.Data.Base;
using eTickets.Models;

namespace eTickets.Data.Services
{
    public class ProducersServices : EntityBaseRepository<Producer>, IProducersServices
    {
        public ProducersServices(AppDbContext context) : base(context) { }

    }
}
