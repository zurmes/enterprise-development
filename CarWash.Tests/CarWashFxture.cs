using CarWash.Domain.Entities;
using CarWash.Domain.Enums;

namespace CarWash.Tests;

/// <summary>
/// Набор тестовых данных для проведения юнит-тестирования автомойки
/// </summary>
public class CarWashFixture
{
    /// <summary>
    /// Зарегистрированные клиенты
    /// </summary>
    public List<Client> Clients { get; set; } = [];

    /// <summary>
    /// Прейскурант услуг
    /// </summary>
    public List<Service> Services { get; set; } = [];

    /// <summary>
    /// Журнал заказов
    /// </summary>
    public List<Order> Orders { get; set; } = [];

    public CarWashFixture()
    {
        // Автомобили
        var car1 = new Car { Id = 1, LicensePlate = "А101АА663", Brand = "Lada Ballada" };
        var car2 = new Car { Id = 2, LicensePlate = "В206ВВ663", Brand = "Toyota Yenota" };
        var car3 = new Car { Id = 3, LicensePlate = "С302СС363", Brand = "CUMAZ 67" };
        var car4 = new Car { Id = 4, LicensePlate = "Е456ЕЕ163", Brand = "BMW X5group" };
        var car5 = new Car { Id = 5, LicensePlate = "К545КК067", Brand = "Kia Rio" };
        var car6 = new Car { Id = 6, LicensePlate = "М611ММ663", Brand = "Hyundai Slesaris" };
        var car7 = new Car { Id = 7, LicensePlate = "Х701ТР167", Brand = "Ssania R500" };
        var car8 = new Car { Id = 8, LicensePlate = "О893ОО557", Brand = "GAZelle Prev" };
        var car9 = new Car { Id = 9, LicensePlate = "Р999РР347", Brand = "Mercedes Splinter" };
        var car10 = new Car { Id = 10, LicensePlate = "Т042ТТ127", Brand = "Geely Monjaro" };

        // Клиенты
        var client1 = new Client { Id = 1, FullName = "Иванов Иван Иванович", Phone = "+78461112233", Cars = [car1] };
        var client2 = new Client { Id = 2, FullName = "Петров Сергей Алексеевич", Phone = "+78462223344", Cars = [car2, car5] };
        var client3 = new Client { Id = 3, FullName = "Сидоров Алексей Михайлович", Phone = "+78463334455", Cars = [car3] };
        var client4 = new Client { Id = 4, FullName = "Смирнова Елена Павловна", Phone = "+78464445566", Cars = [car4] };
        var client5 = new Client { Id = 5, FullName = "Кузнецов Дмитрий Николаевич", Phone = "+78465556677", Cars = [car6] };
        var client6 = new Client { Id = 6, FullName = "Васильев Андрей Игоревич", Phone = "+78466667788", Cars = [car7] };
        var client7 = new Client { Id = 7, FullName = "Попов Михаил Сергеевич", Phone = "+78467778899", Cars = [car8] };
        var client8 = new Client { Id = 8, FullName = "Соколова Анна Владимировна", Phone = "+78468889900", Cars = [car9] };
        var client9 = new Client { Id = 9, FullName = "Михайлов Денис Олегович", Phone = "+78469990011", Cars = [car10] };
        var client10 = new Client { Id = 10, FullName = "Федоров Роман Денисович", Phone = "+78460001122", Cars = [car1] };

        Clients.AddRange([client1, client2, client3, client4, client5, client6, client7, client8, client9, client10]);

        // Услуги
        var s1 = new Service { Id = 1, Name = "Экспресс-мойка", Category = CarCategory.Passenger, Price = 600m, DurationMinutes = 20 };
        var s2 = new Service { Id = 2, Name = "Комплексная мойка", Category = CarCategory.Passenger, Price = 1400m, DurationMinutes = 45 };
        var s3 = new Service { Id = 3, Name = "Стандартная мойка SUV", Category = CarCategory.Suv, Price = 1100m, DurationMinutes = 35 };
        var s4 = new Service { Id = 4, Name = "Химчистка салона", Category = CarCategory.Passenger, Price = 4500m, DurationMinutes = 90 };
        var s5 = new Service { Id = 5, Name = "Мойка грузовика", Category = CarCategory.Truck, Price = 2800m, DurationMinutes = 60 };
        var s6 = new Service { Id = 6, Name = "Полировка кузова", Category = CarCategory.Suv, Price = 3500m, DurationMinutes = 75 };
        var s7 = new Service { Id = 7, Name = "Мойка двигателя", Category = CarCategory.Suv, Price = 1200m, DurationMinutes = 30 };
        var s8 = new Service { Id = 8, Name = "Обработка воском", Category = CarCategory.Passenger, Price = 500m, DurationMinutes = 15 };
        var s9 = new Service { Id = 9, Name = "Мойка днища", Category = CarCategory.Suv, Price = 900m, DurationMinutes = 25 };
        var s10 = new Service { Id = 10, Name = "Мойка минивэна", Category = CarCategory.Minivan, Price = 1800m, DurationMinutes = 50 };

        Services.AddRange([s1, s2, s3, s4, s5, s6, s7, s8, s9, s10]);

        // Базовая точка отсчета по времени для воспроизводимости тестов
        var baseTime = new DateTime(2026, 6, 1, 10, 0, 0);

        // Заказы
        Orders =
        [
            // 1) Иванов
            new Order { Id = 1, Client = client1, Car = car1, Service = s1, StartTime = baseTime, BoxNumber = 1 },
            new Order { Id = 2, Client = client1, Car = car1, Service = s1, StartTime = baseTime.AddHours(2), BoxNumber = 1 },
            new Order { Id = 3, Client = client1, Car = car1, Service = s2, StartTime = baseTime.AddDays(1), BoxNumber = 2 },
            new Order { Id = 4, Client = client1, Car = car1, Service = s8, StartTime = baseTime.AddDays(2), BoxNumber = 1 },
            new Order { Id = 5, Client = client1, Car = car1, Service = s1, StartTime = baseTime.AddDays(3), BoxNumber = 1 },

            // Петров
            new Order { Id = 6, Client = client2, Car = car2, Service = s2, StartTime = baseTime, BoxNumber = 2 },
            new Order { Id = 7, Client = client2, Car = car2, Service = s2, StartTime = baseTime.AddHours(3), BoxNumber = 1 },
            new Order { Id = 8, Client = client2, Car = car5, Service = s3, StartTime = baseTime.AddDays(1), BoxNumber = 3 },
            new Order { Id = 9, Client = client2, Car = car5, Service = s1, StartTime = baseTime.AddDays(2), BoxNumber = 2 },

            // Сидоров
            new Order { Id = 10, Client = client3, Car = car3, Service = s5, StartTime = baseTime, BoxNumber = 3 },
            new Order { Id = 11, Client = client3, Car = car3, Service = s5, StartTime = baseTime.AddDays(2), BoxNumber = 3 },
            new Order { Id = 12, Client = client3, Car = car3, Service = s3, StartTime = baseTime.AddDays(4), BoxNumber = 3 },

            // Смирнова
            new Order { Id = 13, Client = client4, Car = car4, Service = s3, StartTime = baseTime.AddMinutes(10), BoxNumber = 4 },
            new Order { Id = 14, Client = client4, Car = car4, Service = s1, StartTime = baseTime.AddDays(1), BoxNumber = 1 },

            // Кузнецов
            new Order { Id = 15, Client = client5, Car = car6, Service = s4, StartTime = baseTime.AddHours(5), BoxNumber = 2 },
            new Order { Id = 16, Client = client5, Car = car6, Service = s6, StartTime = baseTime.AddDays(3), BoxNumber = 2 },

            // Остальные по 1 заказу
            new Order { Id = 17, Client = client6, Car = car7, Service = s7, StartTime = baseTime.AddHours(4), BoxNumber = 4 },
            new Order { Id = 18, Client = client7, Car = car8, Service = s10, StartTime = baseTime.AddHours(1), BoxNumber = 5 },
            new Order { Id = 19, Client = client8, Car = car9, Service = s9, StartTime = baseTime.AddHours(2), BoxNumber = 5 },
            new Order { Id = 20, Client = client9, Car = car10, Service = s2, StartTime = baseTime.AddHours(6), BoxNumber = 2 },
            new Order { Id = 21, Client = client10, Car = car1, Service = s4, StartTime = baseTime.AddDays(5), BoxNumber = 2 }
        ];
    }
}