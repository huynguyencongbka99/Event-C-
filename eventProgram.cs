/*

public delegate void EventHandler(object sender, EventArgs e);

'
// Khi chúng ta nhìn vào Myclass chúng ta chỉ thấy 2 element chính
// Thứ nhất là một khai báo MyEvent
// Thứ hai là một phương thức để làm điều gì đó.
// Do vậy sau này để chúng ta có thể sử dụng được tính chất của event
// Chúng ta cứ khai báo ra một class để chứa event. Từ 
public class MyClass
{
	public event EventHandler MyEvent;
	public void DoSomething()
	{
		MyEvent?.Invoke(this, EventArgs.Empty);
	}
}

public class AnotherClass
{
	public void SubscribeToEvent(MyClass myClassInstance)
	{
		myClassInstance.MyEvent += HandleEvent;
	}
	
	private void HandleEvent(object sender, EventArgs e)
	{
		Console.WriteLine("Event handled!");
	}
	
	public void UnsubscribeFromEvent(MyClass myClassInstance)
	{
		myClassInstance.MyEvent -= HandleEvent;
	}
}

class Program
{
	static void Main(string[] args)
	{
		MyClass myClass = new MyClass();
		
		AnotherClass anotherClass = new AnotherClass();
		
		anotherClass.SubscribeToEvent(myClass);
		
		myClass.DoSomething();
		
		anotherClass.UnsubscribeFromEvent(myClass);
		
		myClass.DoSomething();
		
		Console.ReadLine();
	}
}

*/


/*
In this code:

StockTicker class represents a stock ticker that periodically updates its price.
PriceChanged event notifies subscribers when the price changes.
StockDisplay class is a subscriber that displays the new price whenever the PriceChanged event occurs.
In the Main method, we create instances of StockTicker and StockDisplay. The StockDisplay subscribes to the PriceChanged event of StockTicker.
We start updating prices in a separate thread to simulate real-time updates.
Press any key to exit the program.
This example demonstrates how to use events to implement a simple observer pattern, where subscribers are notified of changes in the subject (the StockTicker in this case).
*/

using System;
using System.Threading;

public class StockTicker
{
    // Declare an event handler delegate
    public delegate void PriceChangedEventHandler(object sender, PriceChangedEventArgs e);

    // Declare the event
    public event PriceChangedEventHandler PriceChanged;

    // Simulate stock price
    private decimal _price;

    public decimal Price
    {
        get { return _price; }
        set
        {
            if (_price != value)
            {
                _price = value;
                // Raise the event when price changes
                OnPriceChanged(new PriceChangedEventArgs(_price));
            }
        }
    }

    // Method to raise the event
    protected virtual void OnPriceChanged(PriceChangedEventArgs e)
    {
        PriceChanged?.Invoke(this, e);
    }

    // Method to simulate price updates
    public void StartUpdatingPrices()
    {
        Random random = new Random();
        while (true)
        {
            // Generate a random price change
            decimal newPrice = _price + Convert.ToDecimal(random.NextDouble() * 10 - 5);
            Price = Math.Round(newPrice, 2);

            // Wait for some time before the next update
            Thread.Sleep(2000);
        }
    }
}

// Custom event arguments class
public class PriceChangedEventArgs : EventArgs
{
    public decimal NewPrice { get; }

    public PriceChangedEventArgs(decimal newPrice)
    {
        NewPrice = newPrice;
    }
}

// Subscriber class
public class StockDisplay
{
    public StockDisplay(StockTicker ticker)
    {
        // Subscribe to the PriceChanged event
        ticker.PriceChanged += Ticker_PriceChanged;
    }

    // Event handler method
    private void Ticker_PriceChanged(object sender, PriceChangedEventArgs e)
    {
        Console.WriteLine($"New price: {e.NewPrice}");
    }
}

class Program
{
    static void Main(string[] args)
    {
        StockTicker ticker = new StockTicker();
        StockDisplay display = new StockDisplay(ticker);

        // Start updating prices in a separate thread
        Thread priceUpdateThread = new Thread(ticker.StartUpdatingPrices);
        priceUpdateThread.Start();

        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }
}
