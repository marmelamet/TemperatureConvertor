using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TempApp.Core.Entities.Abstract;

namespace TempApp.Core.Entities.Concrete
{
    public class BaseEntity : IBaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //This property stands for the freezing temperature of water
        public double MinHeat { get; set; }
        //This property stands for the boiling temperature of water
        public double MaxHeat { get; set; }
        public double PickedHeat { get; set; }
        public int DisplayOrder { get; set; }

    }
}
