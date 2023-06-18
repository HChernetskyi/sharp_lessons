using static SharpLesson.Program;

namespace SharpLesson;

public delegate void NameOfDelegate(Order order);

public class Program
{
    public static event NameOfDelegate? NameOfEvent;
    public static void Main()
    {
        var waiter = new Waiter();
        var cook = new Cook();
        var chef = new Chef();

        Console.WriteLine("Shift starts. We have two cooks");
        NameOfEvent += chef.TakeOrder;
        NameOfEvent += cook.TakeOrder;
        waiter.GetOrder(new Order() { IsMainDish = true, Name = "Steak, Medium roar" });
        waiter.GetOrder(new Order() { IsMainDish = false, Name = "Chicken soup" });
        waiter.GetOrder(new Order() { IsMainDish = false, Name = "Cake" });

        Console.WriteLine("End of chef's shift. We have only one cook");
        NameOfEvent -= chef.TakeOrder;
        waiter.GetOrder(new Order() { IsMainDish = true, Name = "Steak, well done" });
        waiter.GetOrder(new Order() { IsMainDish = false, Name = "Cookies" });
    }

    public class Waiter
    {
        public void GetOrder(Order order)
        {
            Console.WriteLine($"We have a new order! It is {order.IsMainDish} that this dish is main. Who will take it?");
            NameOfEvent?.Invoke(order);
        }

    }

    public class Cook
    {
        public void TakeOrder(Order order)
        {
            if (!order.IsMainDish)
                Console.WriteLine($"Ordinary cook: I will take this order: \"{order.Name}\".");
        }
    }

    public class Chef
    {
        public void TakeOrder(Order order)
        {
            if (order.IsMainDish)
                Console.WriteLine($"Chef: I will take this important order: \"{order.Name}\".");
        }
    }

    public class Order
    {
        public bool IsMainDish { get; set; }
        public string Name { get; set; }
    }
}