using FitComrade.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FitComrade.Core.Services
{
    public interface IDataService
    {
        List<Product> GetProducts();
    }
}
