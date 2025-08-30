class Program
{
    static void Main(string[] args)
    {
        int[] numbers = { 1, 5, 7, 11, 15, 2, 13, 21, 9 };

        IEnumerable<int> valuesInt = from x in numbers
                                     where x > 6 && x < 16
                                     select x;

        foreach (int value in valuesInt )
        {
            Console.WriteLine(value);
        }



        string[] desserts = { "pay de manzana", "pastel de chocolate", "manzana caramelizada", "fresas con crema" };

        IEnumerable<string> valuesString = from x in desserts
                                           where x.Contains("manzana")
                                           orderby x
                                           select x;

        foreach (string value in valuesString)
        {
            Console.WriteLine(value);
        }
    }
}