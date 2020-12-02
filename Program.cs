using System;
using System.Data;

namespace CalculatIt
{
    class Program
    {
        static readonly string symbols = "0123456789()/*+-,.";
        static readonly string symbols1 = "0123456789,.";
        static readonly string symbols2 = "/*+-";
        static string userExpression, bracketsInner;
        static void Main(string[] args)
        {
            Help();
            WorkWithStr();
        }
        static void Help()
        {
            Console.WriteLine("Для ввода допустимо использовать только цифры '0-9'");
            Console.WriteLine("операторы арифметических операций '+', '-', '*' и '/'");
            Console.WriteLine("скобки '(' и ')'");
            Console.WriteLine("и в качестве разделителя дробных чисел '.' или ','");
            Console.WriteLine();
            Console.Write("Введите выражение: ");
        }
        static void WorkWithStr()
        {
            userExpression = Console.ReadLine();
            SearchForExcep(userExpression);
            ExpressionCutter();
        }
        static void SearchForExcep(string str)
        {
            userExpression = userExpression.Replace(" ", "");
            for (int i = 0; i < str.Length; i++)
            {
                bool excep = symbols.Contains(str[i]);
                if (excep == false)
                {
                    Console.WriteLine("Вы ввели недопустимый символ");
                    Help();
                    WorkWithStr();
                    Console.WriteLine();
                    break;
                }
            }
            int BracketsCounter = 0;
            for (int i = 0; i < userExpression.Length; i++)
            {
                if (userExpression[i] == '(') ++BracketsCounter;
                else if (userExpression[i] == ')') --BracketsCounter;
                if (BracketsCounter < 0)
                {
                    Console.WriteLine("Скобки расположены не верно, либо количество закрытых и открытых скобок не совпадает");
                    Help();
                    WorkWithStr();
                    break;
                }
            }
            if (BracketsCounter > 0)
            {
                Console.WriteLine("Скобки расположены не верно, либо количество закрытых и открытых скобок не совпадает");
                Help();
                WorkWithStr();
                Console.WriteLine();
            }
        }
        static void ExpressionCutter()
        {
            int symCounter = 0;
            int closeBracketCounter;
            if (userExpression.Contains('('))
            {
                for (closeBracketCounter = userExpression.LastIndexOf('('); userExpression[closeBracketCounter] != ')'; closeBracketCounter++) 
                {

                }

                bracketsInner = userExpression.Substring(userExpression.LastIndexOf('(') + 1, closeBracketCounter - userExpression.LastIndexOf('(') - 1);//userExpression.IndexOf(')') - userExpression.LastIndexOf('(') - 1);
                if (bracketsInner.Contains('*')) LetsMult(bracketsInner);
                else
                    if (bracketsInner.Contains('/')) LetsDiv(bracketsInner);
                else
                    if (bracketsInner.Contains('-') && bracketsInner.IndexOf('-') == 0)
                {
                    for (int i = 1; i < bracketsInner.Length; i++)
                    {
                        if (symbols2.Contains(bracketsInner[i])) symCounter++;
                    }
                    if (symCounter == 0) BracketsDown(bracketsInner);
                    else LetsAddSubs(bracketsInner);
                }
                else
                    if (bracketsInner.Contains('+') || bracketsInner.Contains('-')) LetsAddSubs(bracketsInner);
                else BracketsDown(bracketsInner);
            }
            else
                if (userExpression.Contains('*')) LetsMult(userExpression);
            else
                    if (userExpression.Contains('/')) LetsDiv(userExpression);
            else
            if (userExpression.Contains('-') && userExpression.IndexOf('-') == 0)
            {
                for (int i = 1; i < userExpression.Length; i++)
                {
                    if (symbols2.Contains(userExpression[i])) symCounter++;
                }
                if (symCounter == 0) ResultShow();
                else LetsAddSubs(userExpression);
            }
            else 
            if (userExpression.Contains('+') || userExpression.Contains('-')) LetsAddSubs(userExpression);
            else
                ResultShow();
        }
        static void ResultShow()
        {
            Console.WriteLine("Результат: " + userExpression);
        }

        static void LetsMult(string str)
        {
            string leftpart = "";
            string rightpart = "";
            string res;
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == '*')
                {
                    for (int i0 = 0; i0 < str.IndexOf('*'); i0++)
                    {
                        if (symbols1.Contains(str[i0]) || (str[i0] == '-' && leftpart == ""))
                        {
                            leftpart += str[i0];
                        }
                        else
                            if (str[i0] == '-') leftpart = "" + str[i0];
                        else leftpart = "";
                    }
                    for (int i1 = str.IndexOf('*') + 1; i1 < str.Length; i1++)
                    {
                        if (symbols1.Contains(str[i1]) == true)
                            rightpart += str[i1];
                        else
                            break;
                    }
                    res = Convert.ToString(Convert.ToDouble(leftpart) * Convert.ToDouble(rightpart));
                    userExpression = userExpression.Replace(leftpart + '*' + rightpart, res);
                    str = str.Replace(leftpart + '*' + rightpart, res);
                }
            }
            ExpressionCutter();
        }
        static void LetsDiv(string str)
        {
            string leftpart = "";
            string rightpart = "";
            string res;
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == '/')
                {
                    for (int i0 = 0; i0 < str.IndexOf('/'); i0++)
                    {
                        if (symbols1.Contains(str[i0]) || (str[i0] == '-' && leftpart == ""))
                        {
                            leftpart += str[i0];
                        }
                        else 
                            if (str[i0] == '-')  leftpart = "" + str[i0];
                            else leftpart = "";
                    }
                    for (int i1 = str.IndexOf('/') + 1; i1 < str.Length; i1++)
                    {
                        if (symbols1.Contains(str[i1]) == true)
                            rightpart += str[i1];
                        else
                            break;
                    }
                    res = Convert.ToString(Convert.ToDouble(leftpart) / Convert.ToDouble(rightpart));
                    userExpression = userExpression.Replace(leftpart + '/' + rightpart, res);
                    str = str.Replace(leftpart + '/' + rightpart, res);
                }
            }
            ExpressionCutter();
        }
        static void LetsAddSubs(string str)
        {
            string leftpart = "";
            string rightpart = "";
            string res;
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == '+')
                {
                    for (int i0 = 0; i0 < str.IndexOf('+'); i0++)
                    {
                        if (symbols1.Contains(str[i0]) || (str[i0] == '-' && leftpart == ""))
                        {
                            leftpart += str[i0];
                        }
                        else
                            if (str[i0] == '-') leftpart = "" + str[i0];
                        else leftpart = "";
                    }
                    for (int i1 = str.IndexOf('+') + 1; i1 < str.Length; i1++)
                    {
                        if (symbols1.Contains(str[i1]) == true)
                            rightpart += str[i1];
                        else
                            break;
                    }
                    res = Convert.ToString(Convert.ToDouble(leftpart) + Convert.ToDouble(rightpart));
                    userExpression = userExpression.Replace(leftpart + '+' + rightpart, res);
                    str = str.Replace(leftpart + '+' + rightpart, res);
                    leftpart = "";
                    rightpart = "";
                    res = "";
                    break;
                }
                if (str[i] == '-' && i != 0)
                {
                    for (int i0 = 0; i0 < str.IndexOf('-'); i0++)
                    {
                        if (symbols1.Contains(str[i0]) || (str[i0] == '-' && leftpart == ""))
                        {
                            leftpart += str[i0];
                        }
                        else leftpart = "";
                    }
                    for (int i1 = str.IndexOf('-') + 1; i1 < str.Length; i1++)
                    {
                        if (symbols1.Contains(str[i1]) == true)
                            rightpart += str[i1];
                        else
                            break;
                    }
                    res = Convert.ToString(Convert.ToDouble(leftpart) - Convert.ToDouble(rightpart));
                    userExpression = userExpression.Replace(leftpart + '-' + rightpart, res);
                    str = str.Replace(leftpart + '-' + rightpart, res);
                    leftpart = "";
                    rightpart = "";
                    res = "";
                    break;
                }
            }
            ExpressionCutter();
        }
        static void BracketsDown(string str)
        {
            double result;
            if (Double.TryParse(str, out result) == true)     
            {
                if (result >= 0)
                {

                    userExpression = userExpression.Replace('(' + str + ')', str);
                }
                else
                    if (result < 0)
                {
                    if (userExpression.IndexOf('(' + str + ')') == 0)
                    {
                        userExpression = userExpression.Replace('(' + str + ')', str);
                    }
                    else
                        if (userExpression[userExpression.IndexOf('(' + str + ')')-1] == '+')
                    {
                        userExpression = userExpression.Replace("+(" + str + ')', str);
                        ExpressionCutter();
                    }
                    else
                        if(userExpression[userExpression.IndexOf('(' + str + ')') - 1] == '-')
                    {
                        string str2 = Convert.ToString(Convert.ToDouble(str) * (-1));
                        userExpression = userExpression.Replace("-(" + str + ')', '+' + str2);
                        ExpressionCutter();
                    }
                    else 
                        if (userExpression[userExpression.IndexOf('(' + str + ')') - 1] == '*')
                    {
                        string leftpart = "", res;
                        for (int i = 0; i < userExpression.IndexOf('*'); i++)
                        {

                            if (symbols1.Contains(userExpression[i]) || (userExpression[i] == '-' && leftpart == ""))
                            {
                                leftpart += userExpression[i];
                            }
                            else
                                if (userExpression[i] == '-') leftpart = "" + userExpression[i];
                            else leftpart = "";
                        }
                        res = Convert.ToString(Convert.ToDouble(leftpart) * Convert.ToDouble(str));
                        userExpression = userExpression.Replace(leftpart + "*(" + str + ')', res);
                        ExpressionCutter();
                    }
                    else
                        if (userExpression[userExpression.IndexOf('(' + str + ')') - 1] == '/')
                    {
                        string leftpart = "", res;
                        for (int i = 0; i < userExpression.IndexOf('/'); i++)
                        {
                            
                            if (symbols1.Contains(userExpression[i]) || (userExpression[i] == '-' && leftpart == ""))
                            {
                                leftpart += userExpression[i];
                            }
                            else
                                if (userExpression[i] == '-') leftpart = "" + userExpression[i];
                            else leftpart = "";
                        }
                        res = Convert.ToString(Convert.ToDouble(leftpart) / Convert.ToDouble(str));
                        userExpression = userExpression.Replace(leftpart + "/(" + str + ')', res);
                        ExpressionCutter();
                    }
                }
            }
            ExpressionCutter();
        }
    }
}