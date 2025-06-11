using Application.Entities;
using Application.UseCase;

public class NotificationService
{
    public void RegisterSubscriptions(CompleteMakingUseCase useCase)
    {
        useCase.OrderReady += NotifyConsole;
        useCase.OrderReady += NotifyEmail;
    }

    private void NotifyConsole(Order order)
    {
        Console.WriteLine($"Уведомление: Заказ с ID {order.Id} готов для клиента {order.ClientEmail}.");
    }

    private void NotifyEmail(Order order)
    {
        Console.WriteLine($"Отправка email на адрес {order.ClientEmail}: Ваш заказ с ID {order.Id} готов.");
    }
}
