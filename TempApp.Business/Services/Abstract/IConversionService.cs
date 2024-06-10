using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TempApp.Core.Entities.Concrete;

namespace TempApp.Business.Services.Abstract
{
    public interface IConversionService
    {
        void CreateNewType();
        List<TempType> CreateTempList();
        List<TempType> GetTempTypes();
        void ConvertTemp(string temp);
        void WriteTypes();
        void EvaluateChoice();
    }
}
