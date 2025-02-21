using System;
using System.Collections.Generic;

namespace Arsal
{
    class Program
    {
        static void Main(string[] args)
        {
            Calculator calculator = new Calculator();
            bool isContinue = true;

            while (isContinue)
            {
                try
                {
                    float num1 = GetNumber("Enter the first number: ");
                    float num2 = GetNumber("Enter the second number: ");
                    string op = GetOperator("Enter the operator (+, -, *, /, ^): ");
                    float result = calculator.PerformOperation(num1, num2, op);
                    Console.WriteLine($"Result: {result}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }

                isContinue = GetContinueResponse("Do you want to continue? (Y/N): ");
            }
        }

        static float GetNumber(string prompt)
        {
            float number;
            while (true)
            {
                Console.Write(prompt);
                if (float.TryParse(Console.ReadLine(), out number))
                    return number;
                Console.WriteLine("Invalid input. Please enter a valid number.");
            }
        }

        static string GetOperator(string prompt)
        {
            Console.Write(prompt);
            return Console.ReadLine();
        }

        static bool GetContinueResponse(string prompt)
        {
            Console.Write(prompt);
            string response = Console.ReadLine();
            return response.ToUpper() == "Y";
        }
    }

    public interface IOperation
    {
        float Execute(float num1, float num2);
    }

    public class AddOperation : IOperation
    {
        public float Execute(float num1, float num2) => num1 + num2;
    }

    public class SubtractOperation : IOperation
    {
        public float Execute(float num1, float num2) => num1 - num2;
    }

    public class MultiplyOperation : IOperation
    {
        public float Execute(float num1, float num2) => num1 * num2;
    }

    public class DivideOperation : IOperation
    {
        public float Execute(float num1, float num2)
        {
            if (num2 == 0)
                throw new DivideByZeroException("Cannot divide by zero");
            return num1 / num2;
        }
    }

    public class PowerOperation : IOperation
    {
        public float Execute(float num1, float num2) => MathF.Pow(num1, num2);
    }

    public class Calculator
    {
        private readonly Dictionary<string, IOperation> operations;

        public Calculator()
        {
            operations = new Dictionary<string, IOperation>
            {
                { "+", new AddOperation() },
                { "-", new SubtractOperation() },
                { "*", new MultiplyOperation() },
                { "/", new DivideOperation() },
                { "^", new PowerOperation() }
            };
        }

        public float PerformOperation(float num1, float num2, string op)
        {
            if (operations.ContainsKey(op))
            {
                return operations[op].Execute(num1, num2);
            }
            else
            {
                throw new InvalidOperationException("Invalid operator");
            }
        }
    }
}
