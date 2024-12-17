using Domain.UseCase;

namespace Infrastructure.Notifications
{
    public class NotificationService
    {
        public void NotifyOrderReady(object sender, OrderEventArgs e)
        {
            Console.WriteLine($"[Уведомление]: Заказ с ID {e.Order.Id} готов.");
        }
    }

    public class EmailService
    {
        public void SendOrderReadyEmail(object sender, OrderEventArgs e)
        {
            Console.WriteLine($"[Email]: Заказ с ID {e.Order.Id} готов. Отправка email на адрес {e.Order.ClientEmail}...");
        }
    }
}
