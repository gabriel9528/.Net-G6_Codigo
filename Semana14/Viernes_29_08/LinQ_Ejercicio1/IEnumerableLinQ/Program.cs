
public class IEnumerableLinQ
{
    public static void Main(string[] args)
    {
        List<Employee> employees = new List<Employee>()
        {
            new Employee { Id = 1, Name = "Jhon", Salary = 5000 },
            new Employee { Id = 2, Name = "Pepe", Salary = 3500 },
            new Employee { Id = 3, Name = "Tomy", Salary = 7000 },
            new Employee { Id = 4, Name = "Cynthia", Salary = 10000 },
        };

        //*------------------IEnumerable------------------------
        Console.WriteLine("IEnumerable");
        IEnumerable<Employee> query = from item in employees
                                      where item.Salary > 4000
                                      select item;

        foreach(var employee in query)
        {
            Console.WriteLine($"{employee.Name} - {employee.Salary}" );
        }
        Console.WriteLine("\r\n");
        //*------------------IQueryable--------------------------
        Console.WriteLine("IQueryable");
        IQueryable<Employee> queryable = employees.AsQueryable()
                                                  .Where(e => e.Salary > 4000);

        foreach (var employee in query)
        {
            Console.WriteLine($"{employee.Name} - {employee.Salary}");
        }

    }
}

public class Employee
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int? Salary { get; set; }
}