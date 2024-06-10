
using TempApp.Core.Entities.Concrete;
using Microsoft.Extensions.DependencyInjection;
using TempApp.Business.Services.Abstract;
using TempApp.Business.Services.Concrete;
using TempApp.Data.Repositories.Abstract;
using TempApp.Data.Repositories.Concrete;
using TempApp.Core.Contexts;

var serviceProvider = new ServiceCollection()
    .AddSingleton<IConversionService, ConversionService>()
    .AddSingleton<IConversionRepository<TempType>, ConversionRepository<TempType>>()
    .AddDbContext<AppDbContext>()
    .BuildServiceProvider();

var _conService = serviceProvider.GetRequiredService<IConversionService>();

bool isAgain = true;

//Bu While döngüsü, kullanıcının tekrar işlem yapabilmesini sağlamak için eklendi.
while (isAgain)
{
    string option = "0";
    Console.WriteLine("====Sıcaklık Dönüştürücü Uygulamasına Hoşgeldiniz.====");
    while (!(option.Trim() == "1" || option.Trim() == "2"))
    {
        Console.WriteLine("Yapmak istediğiniz işlemi seçiniz: ");
        Console.WriteLine("1- Dönüştürme işlemi");
        Console.WriteLine("2- Yeni tür ekleme");
        option = Console.ReadLine().Trim();
        if (!(option.Trim() == "1" || option.Trim() == "2"))
            Console.WriteLine("Lütfen 1 veya 2 seçeneklerinden birini seçin!");
    }

    if (option.Trim() == "2")
        _conService.CreateNewType();
    _conService.WriteTypes();
    _conService.EvaluateChoice();

    Console.WriteLine("Dönüştürülecek sıcaklık: ");
    _conService.ConvertTemp(Console.ReadLine());

    Console.WriteLine("Tekrar dönüşüm işlemi yapmak ister misiniz? (E/H):");
    var again = Console.ReadLine().Trim().ToUpper();
    isAgain = again == "E" ;
}