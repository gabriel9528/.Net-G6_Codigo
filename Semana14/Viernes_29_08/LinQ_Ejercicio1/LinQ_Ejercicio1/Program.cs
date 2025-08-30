
public class SyntaxLinQ
{
    public static void Main(string[] args)
    {
        List<int> numbers = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8 };

        //*-----------------Syntax Query--------------------------
        Console.WriteLine("Syntax Query");
        
        var querySyntax = from number in numbers
                          where number > 2
                          select number;

        foreach (var number in querySyntax)
        {
            Console.WriteLine(number);
        }

        //*---------------Method Query ----------------------------
        Console.WriteLine("Method Query");

        var methoQuery = numbers.Where(n => n > 2);

        foreach(var number in methoQuery)
        {
            Console.WriteLine(number);
        }

        //*-------------Mixed Query -------------------------------
        Console.WriteLine("Mixed Query");

        var mixedQuery = (from item in numbers
                          where item > 2
                          select item).Where(x => x % 3 == 0);

        foreach (var number in mixedQuery)
        {
            Console.WriteLine(number);
        }
        //Console.WriteLine("La suma de los numeros es: " + mixedQuery);
    }
}