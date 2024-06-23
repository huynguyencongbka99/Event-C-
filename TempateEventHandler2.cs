using System;

// Define a class derived from EventArgs to pass data with the event
public class UserEventArgs : EventArgs
{
    public string UserName { get; }
    public int UserId { get; }

    public UserEventArgs(string userName, int userId)
    {
        UserName = userName;
        UserId = userId;
    }
}

// Define a class that will raise the event
public class User
{
    // Event using EventHandler<TEventArgs>
    public event EventHandler<UserEventArgs> UserCreated;

    // Method to raise the event
    protected virtual void OnUserCreated(UserEventArgs e)
    {
        UserCreated?.Invoke(this, e);
    }

    // Method to create a user and raise the event
    public void CreateUser(string userName, int userId)
    {
        // Raise the event
        OnUserCreated(new UserEventArgs(userName, userId));
    }
}

// Define the event subscriber class
public class Program
{
    public static void Main()
    {
        var program = new Program();
        var user = new User();
        user.UserCreated += program.OnUserCreated;

        user.CreateUser("JohnDoe", 1);
    }

    // Event handler method
    private void OnUserCreated(object sender, UserEventArgs e)
    {
        Console.WriteLine($"User created: {e.UserName}, ID: {e.UserId}");
    }
}
