using CarWash.Domain.Entities;

namespace CarWash.Tests;

/// <summary>
/// Юнит-тесты бизнес-логики автомойки
/// </summary>
public class CarWashTests(CarWashFixture fixture) : IClassFixture<CarWashFixture>
{
    private readonly CarWashFixture _fixture = fixture;

    /// <summary>
    /// Вывести топ 5 клиентов по количеству посещений
    /// </summary>
    [Fact]
    public void GetTop5Clients_ReturnsCorrectOrder()
    {
        List<string> expectedNames =
        [
            "Иванов Иван Иванович",
            "Петров Сергей Алексеевич",
            "Сидоров Алексей Михайлович",
            "Смирнова Елена Павловна",
            "Кузнецов Дмитрий Николаевич"
        ];

        var actualNames = _fixture.Orders
            .GroupBy(o => o.Client)
            .OrderByDescending(g => g.Count())
            .ThenBy(g => g.Key.Id)
            .Take(5)
            .Select(g => g.Key.FullName)
            .ToList();

        Assert.Equal(expectedNames, actualNames);
    }

    /// <summary>
    /// Вывести информацию об автомобилях, находящихся на мойке в данный момент
    /// </summary>
    [Fact]
    public void GetCurrentCarsAtWash_ReturnsActiveCars()
    {
        var currentTime = new DateTime(2026, 6, 1, 10, 15, 0);

        List<string> expectedPlates =
        [
            "А101АА663",
            "В206ВВ663",
            "Е456ЕЕ163",
            "С302СС363"
        ];

        var actualPlates = _fixture.Orders
            .Where(o => o.StartTime <= currentTime && currentTime < o.StartTime.AddMinutes(o.Service.DurationMinutes))
            .Select(o => o.Car.LicensePlate)
            .OrderBy(plate => plate)
            .ToList();

        Assert.Equal(expectedPlates, actualPlates);
    }

    /// <summary>
    /// Вывести 5 самых популярных услуг
    /// </summary>
    [Fact]
    public void GetTop5PopularServices_ReturnsCorrectServices()
    {
        List<string> expectedServices =
        [
            "Экспресс-мойка",
            "Комплексная мойка",
            "Стандартная мойка SUV",
            "Химчистка салона",
            "Мойка грузовика"
        ];

        var actualServices = _fixture.Orders
            .GroupBy(o => o.Service)
            .OrderByDescending(g => g.Count())
            .ThenBy(g => g.Key.Id)
            .Take(5)
            .Select(g => g.Key.Name)
            .ToList();

        Assert.Equal(expectedServices, actualServices);
    }

    /// <summary>
    /// Узнать для выбранного бокса мойки, начиная с какого времени он освобождается
    /// </summary>
    [Fact]
    public void GetBoxFreeTime_ReturnsNextAvailableTime()
    {
        var targetBox = 1;
        var currentTime = new DateTime(2026, 6, 1, 10, 10, 0);
        var expectedFreeTime = new DateTime(2026, 6, 1, 10, 20, 0);

        var busyOrders = _fixture.Orders
            .Where(o => o.BoxNumber == targetBox && o.StartTime.AddMinutes(o.Service.DurationMinutes) > currentTime)
            .OrderBy(o => o.StartTime)
            .ToList();

        DateTime freeTime = currentTime;

        foreach (Order order in busyOrders)
        {
            DateTime orderEndTime = order.StartTime.AddMinutes(order.Service.DurationMinutes);

            if (order.StartTime <= freeTime)
            {
                if (orderEndTime > freeTime)
                {
                    freeTime = orderEndTime;
                }
            }
            else
            {
                break;
            }
        }

        Assert.Equal(expectedFreeTime, freeTime);
    }

    /// <summary>
    /// Для каждой услуги вывести суммарную выручку
    /// </summary>
    [Fact]
    public void GetRevenueForEachService_CalculatesTotalRevenue()
    {
        var revenueByService = _fixture.Orders
            .GroupBy(o => o.Service.Name)
            .ToDictionary(g => g.Key, g => g.Sum(o => o.Service.Price));

        Assert.Equal(3000m, revenueByService["Экспресс-мойка"]);
        Assert.Equal(5600m, revenueByService["Комплексная мойка"]);
        Assert.Equal(3300m, revenueByService["Стандартная мойка SUV"]);
        Assert.Equal(9000m, revenueByService["Химчистка салона"]);
        Assert.Equal(5600m, revenueByService["Мойка грузовика"]);
        Assert.Equal(3500m, revenueByService["Полировка кузова"]);
        Assert.Equal(1200m, revenueByService["Мойка двигателя"]);
        Assert.Equal(500m, revenueByService["Обработка воском"]);
        Assert.Equal(900m, revenueByService["Мойка днища"]);
        Assert.Equal(1800m, revenueByService["Мойка минивэна"]);
    }
}