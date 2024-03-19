using System;

class Publisher
{
	// Khai bao EventHandler
	public event MyEventHandler<DataEventArgs> myEventHandler;
	
	// publish some data when you want
	public void PublishData(object sender, DataEventArgs e)
	{
		myEventHandler?.Invoke(this, new DataEventArgs())
	}
}

class Subscriber
{
	public Subscriber(Publisher _register)
	{
		_register.myEventHandler += DoSomethingWhenPublisher_publish;
	}
	
	private void DoSomethingWhenPublisher_publish()
	{
		Console.WriteLine("I will do something here when I receive data from publisher!");
	}
}

class DataEventArgs : EventArgs
{
	
}

class Program
{
	public static void Main(string[]args)
	{
		Publisher pub = new Publisher();
		Subscriber sub = new Subscriber(pub);
		pub.PublishData();
	}
}

