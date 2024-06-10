using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TempApp.Core.Entities.Concrete;
using TempApp.Data.Repositories.Abstract;

namespace TempApp.Console.Controllers
{
    public class ConversionController
    {
        private readonly IConversionRepository<TempType> _conversionRepo;
        public ConversionController()
        {            
        }
        public ConversionController(IConversionRepository<TempType> conversionRepo)
        {
            _conversionRepo = conversionRepo;
        }
    }
}
