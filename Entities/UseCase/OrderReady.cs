using Domain.Entities;

namespace Domain.UseCase
{
    public class OrderProcessor
    {
        public event EventHandler<OrderEventArgs> OrderReady;
        public void MarkOrderAsReady(Order order)
        {
            if (order == null)
                throw new ArgumentNullException(nameof(order));

            order.Status =OrderStatus.Completed;
            OnOrderReady(order);
        }
        protected virtual void OnOrderReady(Order order)
        {
            OrderReady?.Invoke(this, new OrderEventArgs(order));
        }
    }
    public class OrderEventArgs : EventArgs
    {
        public Order Order { get; }

        public OrderEventArgs(Order order)
        {
            Order = order;
        }
    }
}
