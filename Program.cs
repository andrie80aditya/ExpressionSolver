using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.RegularExpressions;
using NCalc;

class Program
{
    static Dictionary<string, int> variables = new Dictionary<string, int>();

    static void Main(string[] args)
    {
        Console.WriteLine("Welcome te expression Solver!");
        Console.WriteLine("Enter your set of values in JSON format:");
        string variablesJson = Console.ReadLine();

        try
        {
            variables = JsonSerializer.Deserialize<Dictionary<string, int>>(variablesJson);
        }   
        catch (JsonException)
        {
            Console.WriteLine("Invalid JSON format");
            return;
        }

        while (true)
        {
            Console.WriteLine("\nEnter an expression (or 'quit' to exit):");
            string expr = Console.ReadLine();
            if (expr.ToLower() == "quit")
            {
                break;
            }

            string result = SolveExpression(expr);
            Console.WriteLine($"Result: {result}");
            Console.WriteLine("Do you want to evaluate another expression (y/n)");
            if (Console.ReadLine().ToLower() != "y")
            {
                break;
            }
        }

        Console.WriteLine("Do you want to restart the program (y/n):");
        if (Console.ReadLine().ToLower() != "y")
        {
            Console.WriteLine("Bye");
        }
    }

    static string SolveExpression(string expr)
    {
        if (!isBalanced(expr))
        {
            return "Expression cannot be parsed";
        }

        string[] tokens = expr.Trim().Split(' ');
        foreach (string token in tokens)
        {
            if (!IsOperator(token) && token != "(" && token != ")" && !variables.ContainsKey(token))
            {
                return $"Undefined vairable {token}";
            }
        }

        try
        {
            bool result = EvaluateExpression(expr);
            return result.ToString().ToLower();
        }
        catch (Exception ex)
        {
            string err = ex.Message;
            return "Invalid expression";
        }
    }

    static bool isBalanced(string expr)
    {
        int count = 0;
        foreach (char c in expr)
        { 
            if (c == '(') count++;
            if (c == ')') count--;
            if (count < 0) return false;
        }
        return count == 0;
    }

    static bool IsOperator(string token)
    {
        return new[] { "gt", "lt", "gte", "lte", "eq", "neq", "or", "and" }.Contains(token);
    }

    static bool EvaluateExpression(string expr)
    {
        expr = Regex.Replace(expr, @"\bgt\b", ">");
        expr = Regex.Replace(expr, @"\blt\b", "<");
        expr = Regex.Replace(expr, @"\bgte\b", ">=");
        expr = Regex.Replace(expr, @"\blte\b", "<=");
        expr = Regex.Replace(expr, @"\beq\b", "==");
        expr = Regex.Replace(expr, @"\bneq\b", "!=");
        expr = Regex.Replace(expr, @"\bor\b", "||");
        expr = Regex.Replace(expr, @"\band\b", "&&");

        foreach (var variable in variables)
        {
            expr = expr.Replace(variable.Key, variable.Value.ToString());
        }

        NCalc.Expression e = new NCalc.Expression(expr);
        bool result = (bool)e.Evaluate();

        return result;
    }
}