using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TempApp.Business.Services.Abstract;
using TempApp.Core.Entities.Concrete;
using TempApp.Data.Repositories.Abstract;
using TempApp.Data.Repositories.Concrete;

namespace TempApp.Business.Services.Concrete
{
    public class ConversionService(IConversionRepository<TempType> conversionRepo) : IConversionService
    {
        private readonly IConversionRepository<TempType> _conversionRepo = conversionRepo;
        public List<TempType> tempTypes;
        public TempType model;
        public double temp;
        public string sourceChoice;
        public string targetChoice;
        public List<string> displayOrderList;
        public Tuple<string, string, string, List<TempType>> tuple;

        //DB bağlantısı kurmaya gerek görmediğim için bu şekilde listeyi doldurup listeledim. Projede DB bağlantısı için altyapı olsa da  kullanmamayı tercih ettim.
        /// <summary>
        /// Eğer tempTypes listesi boşsa doldurup gönderir, eğer doluysa olduğu gibi geri döndürür.
        /// </summary>
        /// <returns></returns>
        public List<TempType> CreateTempList()
        {
            if (tempTypes == null)
                return [
            new() { Id = 1, Name = "Celcius", DisplayOrder = 1, MinHeat= 0, MaxHeat = 100, PickedHeat = 0 },
            new() { Id = 2, Name = "Fahrenheit", DisplayOrder = 2, MinHeat= 32, MaxHeat = 212, PickedHeat = 0 },
            new() { Id = 3, Name = "Kelvin", DisplayOrder = 3, MinHeat= 273.15, MaxHeat = 373.15, PickedHeat = 0 },
            new() { Id = 4, Name = "Newton", DisplayOrder = 4, MinHeat= 0, MaxHeat = 100, PickedHeat = 0 },
            new() { Id = 5, Name = "Rømer", DisplayOrder = 5, MinHeat= 7.5, MaxHeat = 60, PickedHeat = 0 },
            new() { Id = 6, Name = "Réaumur", DisplayOrder = 6, MinHeat= 0, MaxHeat = 80, PickedHeat = 0 },
            new() { Id = 7, Name = "Rankine", DisplayOrder = 7, MinHeat= 491.67, MaxHeat = 671.67, PickedHeat = 0 },
        ];
            return tempTypes;
        }
        /// <summary>
        /// Kullanıcının yeni bir sıcaklık türü tanımlamasını sağlar. Dışarıdan parametre almasa da kullanıcıdan aldığı girişlerin atamasını ilgili modelin ilgili alanlarına yapar.
        /// <param name="model.Name"> Yeni modelin adı </param>
        /// <param name="model.MinHeat"> Suyun donma noktası </param>
        /// <param name="model.MaxHeat"> Suyun kaynama noktası </param>
        /// <returns><list type="TempType"</returns>
        /// </summary>
        public void CreateNewType()
        {
            //Normalde bu metot için de her girdi için harf - rakam kontrolü yapılması gerekirdi ancak ben amaçtan çok uzaklaşmamak için eklemiyorum.
            tempTypes = CreateTempList();
            model = new();
            Console.WriteLine("Yeni türün adı: ");
            model.Name = Console.ReadLine();
            Console.WriteLine("Yeni türde suyun donma sıcaklığı: ");
            model.MinHeat = double.Parse(Console.ReadLine());
            Console.WriteLine("Yeni türde suyun kaynama sıcaklığı: ");
            model.MaxHeat = double.Parse(Console.ReadLine());
            model.DisplayOrder = tempTypes.Select(x => x.DisplayOrder).ToList().Max() + 1;
            model.Id = tempTypes.Select(x => x.Id).ToList().Max() + 1;
            tempTypes.Add(model);
        }
        /// <summary>
        /// DB'den ilgili listeyi çeker ve döndürür.
        /// </summary>
        /// <returns></returns>

        //Bu metot DB bağlantısı olduğunda kullanılacak.
        public List<TempType> GetTempTypes()
        {
            return tempTypes = _conversionRepo.GetAll();
        }
        /// <summary>
        /// İki sıcaklık türü arasındaki dönüşümü gerçekleştirir.
        /// </summary>
        /// <param name="temp"></param>
        /// <returns> <param name="targetHeat"/> </returns>
        public void ConvertTemp(string temp)
        {
            TempType sourceType = tempTypes.FirstOrDefault(x => x.DisplayOrder.ToString() == sourceChoice.Trim());
            TempType targetType = tempTypes.FirstOrDefault(x => x.DisplayOrder.ToString() == targetChoice.Trim());
            double pickedHeat = double.Parse(temp);
            double convertedSourceHeat = (pickedHeat - sourceType.MinHeat) / (sourceType.MaxHeat - sourceType.MinHeat);
            double targetHeat = convertedSourceHeat * (targetType.MaxHeat - targetType.MinHeat) + targetType.MinHeat;

            Console.WriteLine("===== SONUÇ =====");
            Console.WriteLine($"{sourceType.Name} türünde {temp} derece, {targetType.Name} türünde yaklaşık olarak {targetHeat} dereceye eşittir.");
        }
        /// <summary>
        /// Ekrana o anda tempTypes türünde mevcut tüm türlerin DisplayOrder ve Name alanlarını yazdırır.
        /// </summary>
        /// <returns> <list type="TempType"</returns>
        public void WriteTypes()
        {
            tempTypes = CreateTempList();
            foreach (var tempType in tempTypes)
            {
                Console.WriteLine($"{tempType.DisplayOrder} - {tempType.Name}");
            }
        }
        /// <summary>
        /// Kullanıcının yaptığı tür seçimlerinin geçerliliğini kontrol eder. Seçimler doğru bir şekilde yapılana kadar metot recursive çalışır.
        /// </summary>
        public void EvaluateChoice()
        {
            displayOrderList = tempTypes.Select(x => x.DisplayOrder.ToString()).ToList();
            Console.WriteLine("Kaynak sıcaklık türünü seçiniz: ");
            sourceChoice = Console.ReadLine();
            if (sourceChoice == null || sourceChoice.Trim() == "" || !displayOrderList.Contains(sourceChoice))
            {
                Console.WriteLine("xxxxx Hatalı bir giriş yaptınız! xxxxx");
                Console.WriteLine(" Lütfen listede yer alan sayılardan birini seçiniz.");
                EvaluateChoice();
            }
            Console.WriteLine("Hedef sıcaklık türünü seçiniz: ");

            targetChoice = Console.ReadLine();
            if (targetChoice == null || targetChoice.Trim() == "" || !displayOrderList.Contains(targetChoice))
            {
                Console.WriteLine("xxxxx Hatalı bir giriş yaptınız! xxxxx");
                Console.WriteLine(" Lütfen listede yer alan sayılardan birini seçiniz.");
                EvaluateChoice();
            }
        }
    }
}
