/* Subor: Math_functions.cs
 * Datum: April 2019
 * Autori: Kristian Klein, Adam Policek, Adam Batala
 * Projekt: Kalkulacka
 * Popis: Vlastna matematicka kniznica s matematickymi operaciami
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
using Microsoft.Win32;
using System.Windows.Input;


namespace Math_functions
{

    /// <summary>
    /// Trieda s hlavnymi matematickymi funkciami
    /// </summary>
    public class Mathematic_functions
    {

        /// <summary>
        /// Funkcia Sum vypocita sucet dvoch nacitanych cisel
        /// </summary>
        /// <param name="number1">Prve nacitane cislo</param>
        /// <param name="number2">Druhe nacitane cislo</param>
        /// <returns>Vracia vysledok suctu dvoch cisel</returns>
        public static double Sum(double number1, double number2)
        {
            double result = number1 + number2;
            return result;
        }

        /// <summary>
        /// Funkcia Min vracua rozdiel dvoch nacitanych cisel
        /// </summary>
        /// <param name="number1">Prve nacitane cislo</param>
        /// <param name="number2">Druhe nacitane cislo</param>
        /// <returns>Vracia vysledok rozdielu dvoch cisel</returns>
        public static double Min(double number1, double number2)
        {
            double result = number1 - number2;
            return result;
        }

        /// <summary>
        /// Funkcia Mult vypocita sucin dvoch nacitanych cisel
        /// </summary>
        /// <param name="number1">Prve nacitane cislo</param>
        /// <param name="number2">Druhe nacitane cislo</param>
        /// <returns>Vracia vysledok sucinu dvoch cisel</returns>
        public static double Mult(double number1, double number2)
        {
            double result = number1 * number2;
            return result;
        }

        /// <summary>
        /// Funkcia Div vypocita podiel dvoch nacitanych cisel
        /// </summary>
        /// <param name="number1">Prve nacitane cislo</param>
        /// <param name="number2">Druhe nacitane cislo</param>
        /// <returns>Vracia vysledok podielu dvoch cisel</returns>
        public static double Div(double number1, double number2)
        {
            double result = number1 / number2;
            return result;
        }

        /// <summary>
        /// Funkcia Fact vypocita faktorial nacitaneho cisla
        /// </summary>
        /// <param name="number1">Prve nacitane cislo</param>
        /// <returns>Vracia vypocitany faktorial daneho cisla</returns>
        public static int Fact(double number1)
        {
            /// explicitne prevedenie double na int
            int number = (int)number1;
            for (int i = number - 1; i > 0; --i)
            {
                number *= i;
            }
            return number;

        }

        /// <summary>
        /// Funkcia Pow vypocita druhu mocninu nacitanych cisel
        /// </summary>
        /// <param name="number1">Prve nacitane cislo</param>
        /// <returns>Vracia druhu mocninu dvoch cisel</returns>
        public static double Pow(double number1)
        {
            number1 = number1 * number1;
            return number1;
        }

        /// <summary>
        /// Funkcia Pow_n vypocita mocninu s lubovolnym prirodzenym exponentom
        /// </summary>
        /// <param name="number1">Prve nacitane cislo</param>
        /// <param name="number2">Lubovolny prirodzeny exponent</param>
        /// <returns>Vracia druhu mocninu s lubovolnym prirodzenym exponentom</returns>
        public static double Pow_n(double number1, double number2)
        {
            double number = number1;
            for(int i = 1; i < number2; i++)
            {
                number1 *= number;
            }
            return number1;
        }

        /// <summary>
        /// Funkcia Log10 vypocita logaritmus o zaklade 10 podla vstavanej matematicke kniznice jazyka C# z nacitaneho cisla
        /// </summary>
        /// <param name="number1">Prve nacitane cislo</param>
        /// <returns>Vracia logaritmus o zaklade 10</returns>
        public static double Log10(double number1)
        {
            double number = Math.Log10(number1);
            return number;
        }

        /// <summary>
        /// Funkcia Sqrt vypocita vseobecnu odmocninu podla vstavanej matematicke kniznice jazyka C# z nacitaneho cisla
        /// </summary>
        /// <param name="number1">Prve nacitane cislo</param>
        /// <returns>Vracia vseobecnu odmocninu z nacitaneho cisla</returns>
        public static double Sqrt(double number1)
        {
            /// Pouzitie vstavanej Matematickej kniznice jazyka C#
            double number = Math.Sqrt(number1);
            return number;
        }
    }  
 }
