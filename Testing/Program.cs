using System;
using System.Collections.Generic;
using System.Linq.Expressions;

public class HelloWorld
{
    public static void Main(string[] args)
    {
        User user = new User(1, "Nemanja Rackovic", 27);
        User user2 = new User(2, "Nikola Sacki", 22);
        User user3 = new User(3, "Nevena Lapcevic", 25);

        List<User> users = new List<User>();
        users.Add(user);
        users.Add(user2);
        users.Add((user3));


        int[] ids = { 1, 3 };

        List<User> forUpdate = users.Where(user =>
        {
            if (ids.Contains(user.UserId))
            {
                users.Remove(user);
                user.Update = true;

                return true;
            }

            return false;
        }).ToList();


        Console.WriteLine("For Creation:");
        users.ForEach(user => Console.WriteLine(user));

        Console.WriteLine("--------------------");

        forUpdate.ForEach(user => Console.WriteLine(user));
    }
}

public class User
{
    public User(int id, string name, int age)
    {
        this.UserId = id;
        this.Name = name;
        this.Age = age;
        this.Update = false;
    }
    public int UserId { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public bool Update { get; set; }

    public override string ToString()
    {
        return $"Id : {this.UserId}, Name: {this.Name}, Age : {this.Age}, Update : {this.Update}";

    }
}

