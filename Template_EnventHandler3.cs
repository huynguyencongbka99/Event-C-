using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace EventHandler1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            PublisherData publisherData = new PublisherData(32,19);
            Subscriber1_Sum sub_sum = new Subscriber1_Sum();
            Subscriber2_Subtract sub_subtract = new Subscriber2_Subtract();

            sub_sum.Subscriber1_Sum_to_Publisher(publisherData);
            sub_subtract.Subscriber2_Sub_to_Publisher(publisherData);

            publisherData.PublisherSendData();

            Console.ReadLine();

        }
    }


    class MyEventArgs : EventArgs
    {
        public double x;
        public double y;
        public MyEventArgs(double x, double y)
        {
            this.x = x;
            this.y = y;
        }
    }

    class PublisherData
    {
        private double operand1;
        private double operand2;
        public event EventHandler<MyEventArgs> handler;
        public PublisherData(double operand1, double operand2)
        {
            this.operand1 = operand1;
            this.operand2 = operand2;
        }
        public void PublisherSendData()
        {
            handler?.Invoke(this, new MyEventArgs(operand1, operand2));
        }

    }

    class Subscriber1_Sum
    {
        public void Subscriber1_Sum_to_Publisher(PublisherData pub)
        {
            pub.handler += CalculateTheSumOf2Operands;
        }
        public void CalculateTheSumOf2Operands(object sender, MyEventArgs e)
        {
            Console.WriteLine($"The sum of two opeands are: {e.x + e.y}");
        }
    }

    class Subscriber2_Subtract
    {
        public void Subscriber2_Sub_to_Publisher(PublisherData pub)
        {
            pub.handler += CalculateTheSubOf2Operands;
        }
        public void CalculateTheSubOf2Operands(object sender, MyEventArgs e)
        {
            Console.WriteLine($"The sub of two opeands are: {e.x - e.y}");
        }
    }
}
