using System.IO;
using GameStore.Domain.Models;

namespace GameStore.Domain.Interfaces.Services
{
    public interface IInvoiceGenerateService
    {
        MemoryStream GenerateInPdf(Order order);
    }
}
