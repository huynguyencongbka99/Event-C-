public class MyEventArgs : EventArgs
{
	public double operand1;
	public double operand2;
	
	public MyEventArgs(double op1, double op2){this. operand1 = op1; this.operand2 = op2;}
	
}

public class Publisher
{
	private double sothunhat;
	private double sothuhai;
	
	public event EventHandler<MyEventArgs> myEventHandler;
	public Publisher(){this.sothunhat = 5; this.sothuhai = 15;}
	public void PublisherPublish()
	{
		myEventHandler.Invoke(this, new MyEventArgs(sothunhat, sothuhai));	
	}

}

public class Subscriber
{
	public Subscriber(Publisher pub)
	{
		pub.myEventHandler += SubDoSomething;
	}
	public void SubDoSomething(object sender, MyEventArgs e)
	{
		Console.WriteLine($"Print the sub of two operands: {e.operand1 + e.operand2}");
	}
	
}

class Program
{
	public static void Main(string[] args)
	{
		Publisher pub1 = new Publisher();
		Subscriber sub1 = new Subscriber(pub1);
		//pub1.myEventHandler += sub1.SubDoSomething;
		pub1.PublisherPublish();
	}
}
