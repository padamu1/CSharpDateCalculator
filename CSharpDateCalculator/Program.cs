using CSharpDateCalculator.Calculator;

public class Programe
{
    public static void Main(string[] args)
    {
        Calculator calculator = new Calculator();
        calculator.SetTargetHour(13);
        calculator.SetTargetDayOfWeek(DayOfWeek.Monday);
        calculator.Calculate();
        Console.WriteLine(calculator.GetHour());
    }
}