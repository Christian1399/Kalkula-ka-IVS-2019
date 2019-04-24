/* Nazov projektu : IVS Kalkulacka
 * Subor: MainWindow.xaml.cs
 * Datum: April 2019
 * Autori: Kristian Klein, Adam Policek, Adam Batala
 * Projekt: Kalkulacka
 * Popis: Vlastna matematicka kniznica s matematickymi operaciami
 */

using Math_functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using Microsoft.Win32;

namespace Calculator
{
    /// <summary>
    /// Vnutorna logika jednotlivych komponentov a tlacidiel hlavneho okna
    /// </summary>
    public partial class MainWindow : Window
    {
        double number1 = 0;
        double number2 = 0;
        double number3 = 0;
        string operation = "";
        double result = 0;
        double ans;
        bool pow_n = false;
        int j = 0;
        bool dot = false;

        /// <summary>
        /// Inicializacia komponentov
        /// </summary>
        public MainWindow()
        {
            /// Metoda ktora je potrebna pri spusteni okna
            InitializeComponent();
        }

        private void NewCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        /// <summary>
        /// Tlacidlo Help otvori jednoduchu napovedau pre uzivatela
        /// </summary>
        /// <param name="sender">odosielatel (ovladaci prvok ktory vyvolava udalost)</param>
        /// <param name="e">objekt ktory uchovava uzitocne informacie (napr. pozicia kurzora mysi)</param>
        private void _Help_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Toto je program \"kalkulačka\" vytvorená ako školský projekt. Pre bližšie info ako kalkulačku používať použite manuál pribalený k inštalátoru alebo: ");
        }

        /// <summary>
        /// Tlacidlo CE vymaze aktualne cislo nachadzajuce sa v txtDisplay1 (teda v primarnom/hlavnom displeji kalkulacky)
        /// </summary>
        /// <param name="sender">odosielatel (ovladaci prvok ktory vyvolava udalost)</param>
        /// <param name="e">objekt ktory uchovava uzitocne informacie (napr. pozicia kurzora mysi)</param>
        private void CE_Click(object sender, RoutedEventArgs e)
        {
            j = 0;
            dot = false;
            /// testovanie ci je zadana nejaka operacia a nulujeme prve cislo alebo druhe cislo
            if (operation == "")
            {
                number1 = 0;
            }
            else
            {
                number2 = 0;
            }
            /// nulovanie primarneho (hlavneho) displeja kalkulacky
            txtDisplay1.Text = "0";
        }

        /// <summary>
        /// Tlacidlo C vymaze oba displeje kalkulacky (primarny aj vedlajsi)
        /// </summary>
        /// <param name="sender">odosielatel (ovladaci prvok ktory vyvolava udalost)</param>
        /// <param name="e">objekt ktory uchovava uzitocne informacie (napr. pozicia kurzora mysi)</param>
        private void C_Click(object sender, RoutedEventArgs e)
        {
            j = 0;
            dot = false;
            number1 = 0;
            number2 = 0;
            operation = "";
            txtDisplay1.Text = "0";
            txtDisplay2.Text = "";
        }

        /// <summary>
        /// Tlacidlo Delete vymaze aktualnu cislicu na displeji
        /// </summary>
        /// <param name="sender">odosielatel (ovladaci prvok ktory vyvolava udalost)</param>
        /// <param name="e">objekt ktory uchovava uzitocne informacie (napr. pozicia kurzora mysi)</param>
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            j--;
            /// testovanie ci je nacitane len prve cislo alebo oba
            if (number1 != 0 && number2 == 0)
            {
                /// prevod retazca na pole cisel
                string removeChar = number1.ToString();
                if (removeChar.Length > 1)
                {
                    /// vymazanie posledneho cisla z pola
                    removeChar = removeChar.Remove(removeChar.Length - 1);
                    number1 = Double.Parse(removeChar);
                    /// prevod cisla na string a nasledny vypis na displej
                    txtDisplay1.Text = number1.ToString();
                }
                /// ak je dlzka retazca mensia alebo rovna 1
                else
                {
                    number1 = 0;
                    txtDisplay1.Text = number1.ToString();
                }
            }
            /// ak su nacitane obidva cisla
            else if (number1 != 0 && number2 != 0 && operation != "")
            {
                /// mazanie z druheho cisla (number2)
                /// rovnaky algoritmus ako v prvej vetve
                string removeChar = number2.ToString();
                if (removeChar.Length > 1)
                {
                    removeChar = removeChar.Remove(removeChar.Length - 1);
                    number2 = Int32.Parse(removeChar);
                    txtDisplay1.Text = number2.ToString();
                }
                else
                {
                    number2 = 0;
                    txtDisplay1.Text = number2.ToString();
                }
            }
        }

        /// <summary>
        /// Tlacidlo Faktorial vypocita faktorial pomocou funkcie <see cref="Mathematic_functions.Fact(double)"/> z matematickej kniznice 
        /// </summary>
        /// <param name="sender">odosielatel (ovladaci prvok ktory vyvolava udalost)</param>
        /// <param name="e">objekt ktory uchovava uzitocne informacie (napr. pozicia kurzora mysi)</param>
        private void Faktorial_Click(object sender, RoutedEventArgs e)
        {
            j = 0;
            dot = false;
            /// osetrenie pripadu napr. 5! + 2^3
            if (pow_n == true)
            {
                /// vypocet mocniny so vseoecnym exponentom pomocou <see cref="Pow_n()"/>
                Pow_n();
                pow_n = false;
            }
            /// ak nie je nacitane druhe cislo
            if (number2 == 0)
            {
                /// volanie funkcie z Matematickej kniznice, prevod na string a vypis vysledku
                number1 = Mathematic_functions.Fact(number1);
                txtDisplay1.Text = number1.ToString();
            }
            else if (number1 != 0 && number2 != 0)
            {
                /// volanie funkcie z Matematickej kniznice, prevod na string a vypis jeho vysledku
                number2 = Mathematic_functions.Fact(number2);
                txtDisplay1.Text = number2.ToString();
                /// uchovanie prveho cisla na neskorsi vypis do pomocneho displeja
                result = number1;
            }
        }

        /// <summary>
        /// Tlacidlo Ans ulozi po vypocte vysledok do pamate pre neskorsie pouzitie 
        /// </summary>
        /// <param name="sender">odosielatel (ovladaci prvok ktory vyvolava udalost)</param>
        /// <param name="e">objekt ktory uchovava uzitocne informacie (napr. pozicia kurzora mysi)</param>
        private void Ans_Click(object sender, RoutedEventArgs e)
        {
            j = 0;
            dot = false;
            if (pow_n == true)
            {
                /// vypocet mocniny so vseoecnym exponentom pomocou <see cref="Pow_n()"/>
                Pow_n();
                pow_n = false;
            }
            if (number1 == 0)
            {
                number1 = ans;
                txtDisplay1.Text = number1.ToString();
            }
            else if (number1 != 0 && number2 == 0)
            {
                number2 = ans;
                txtDisplay1.Text = number2.ToString();
            }
            else
            {
                number2 = Mathematic_functions.Mult(number2, ans);
                txtDisplay1.Text = number2.ToString();
            }
        }

        /// <summary>
        /// Tlacidlo Number7 vypise tlacidlo 7 na displej
        /// </summary>
        /// <param name="sender">odosielatel (ovladaci prvok ktory vyvolava udalost)</param>
        /// <param name="e">objekt ktory uchovava uzitocne informacie (napr. pozicia kurzora mysi)</param>
        private void Number_7_Click(object sender, RoutedEventArgs e)
        {
            if (operation == "" && dot == false)
            {
                number1 = (number1 * 10) + 7;
                txtDisplay1.Text = number1.ToString();
            }
            /// pri vypocte ukoncenom tlacidlom "=" vynuluje vsetky hodnoty (vycisti pamat) pre opatovne pouzitie
            else if (operation == "=")
            {
                number1 = number2 = 0;
                number1 = (number1 * 10) + 7;
                txtDisplay1.Text = number1.ToString();
            }
            /// exponent je vzdy nacitany do number3
            else if (pow_n == true)
            {
                number3 = (number3 * 10) + 7;
                txtDisplay1.Text = number3.ToString();
            }
            else if (dot == true)
            {
                /// počítadlo desatinných cifier
                j++;
                if (number2 == 0)
                {
                    /// úprava čísla na pôvodný tvar
                    for (int i = 0; i < j; i++)
                    {
                        number1 = (number1 * 10);
                    }
                    /// pridanie čísla tlačidla
                    number1 = number1 + 7;
                    /// pridanie desatinnej čiarky
                    for (int i = 0; i < j; i++)
                    {
                        number1 = (number1 / 10);
                    }
                    txtDisplay1.Text = number1.ToString();
                }
                /// načítanie do druhého čísla ak je prvé už načítané
                else if (number1 != 0 && number2 != 0)
                {
                    for (int i = 0; i < j; i++)
                    {
                        number2 = (number2 * 10);
                    }
                    number2 = number2 + 7;
                    for (int i = 0; i < j; i++)
                    {
                        number2 = (number2 / 10);
                    }
                    txtDisplay1.Text = number2.ToString();
                }
            }
            /// vykonava sa ak mame zadany nejaku operaciu (+, -. *, / ...)
            else
            {
                if (pow_n == false)
                {
                    number2 = (number2 * 10) + 7;
                    txtDisplay1.Text = number2.ToString();
                }
            }
        }

        /// <summary>
        /// Tlacidlo Number8 vypise tlacidlo 8 na displej
        /// </summary>
        /// <param name="sender">odosielatel (ovladaci prvok ktory vyvolava udalost)</param>
        /// <param name="e">objekt ktory uchovava uzitocne informacie (napr. pozicia kurzora mysi)</param>
        private void Number_8_Click(object sender, RoutedEventArgs e)
        {
            if (operation == "" && dot == false)
            {
                number1 = (number1 * 10) + 8;
                txtDisplay1.Text = number1.ToString();
            }
            /// pri vypocte ukoncenom tlacidlom "=" vynuluje vsetky hodnoty (vycisti pamat) pre opatovne pouzitie
            else if (operation == "=")
            {
                number1 = number2 = 0;
                number1 = (number1 * 10) + 8;
                txtDisplay1.Text = number1.ToString();
            }
            /// exponent je vzdy nacitany do number3
            else if (pow_n == true)
            {
                number3 = (number3 * 10) + 8;
                txtDisplay1.Text = number3.ToString();
            }
            else if (dot == true)
            {
                /// počítadlo desatinných cifier
                j++;
                if (number2 == 0)
                {
                    /// úprava čísla na pôvodný tvar
                    for (int i = 0; i < j; i++)
                    {
                        number1 = (number1 * 10);
                    }
                    /// pridanie čísla tlačidla
                    number1 = number1 + 8;
                    /// pridanie desatinnej čiarky
                    for (int i = 0; i < j; i++)
                    {
                        number1 = (number1 / 10);
                    }
                    txtDisplay1.Text = number1.ToString();
                }
                /// načítanie do druhého čísla ak je prvé už načítané
                else if (number1 != 0 && number2 != 0)
                {
                    for (int i = 0; i < j; i++)
                    {
                        number2 = (number2 * 10);
                    }
                    number2 = number2 + 8;
                    for (int i = 0; i < j; i++)
                    {
                        number2 = (number2 / 10);
                    }
                    txtDisplay1.Text = number2.ToString();
                }
            }
            /// vykonava sa ak mame zadany nejaku operaciu (+, -. *, / ...)
            else
            {
                if (pow_n == false)
                {
                    number2 = (number2 * 10) + 8;
                    txtDisplay1.Text = number2.ToString();
                }
            }
        }

        /// <summary>
        /// Tlacidlo Number9 vypise tlacidlo 9 na displej
        /// </summary>
        /// <param name="sender">odosielatel (ovladaci prvok ktory vyvolava udalost)</param>
        /// <param name="e">objekt ktory uchovava uzitocne informacie (napr. pozicia kurzora mysi)</param>
        private void Number_9_Click(object sender, RoutedEventArgs e)
        {
            if (operation == "" && dot == false)
            {
                number1 = (number1 * 10) + 9;
                txtDisplay1.Text = number1.ToString();
            }
            /// pri vypocte ukoncenom tlacidlom "=" vynuluje vsetky hodnoty (vycisti pamat) pre opatovne pouzitie
            else if (operation == "=")
            {
                number1 = number2 = 0;
                number1 = (number1 * 10) + 9;
                txtDisplay1.Text = number1.ToString();
            }
            /// exponent nacita vzdy do number3
            else if (pow_n == true)
            {
                number3 = (number3 * 10) + 9;
                txtDisplay1.Text = number3.ToString();
            }
            else if (dot == true)
            {
                /// počítadlo desatinných cifier
                j++;
                if (number2 == 0)
                {
                    /// úprava čísla na pôvodný tvar
                    for (int i = 0; i < j; i++)
                    {
                        number1 = (number1 * 10);
                    }
                    /// pridanie čísla tlačidla
                    number1 = number1 + 9;
                    /// pridanie desatinnej čiarky
                    for (int i = 0; i < j; i++)
                    {
                        number1 = (number1 / 10);
                    }
                    txtDisplay1.Text = number1.ToString();
                }
                /// načítanie do druhého čísla ak je prvé už načítané
                else if (number1 != 0 && number2 != 0)
                {
                    for (int i = 0; i < j; i++)
                    {
                        number2 = (number2 * 10);
                    }
                    number2 = number2 + 9;
                    for (int i = 0; i < j; i++)
                    {
                        number2 = (number2 / 10);
                    }
                    txtDisplay1.Text = number2.ToString();
                }
            }
            /// vykonava sa ak mame zadany nejaku operaciu (+, -. *, / ...)
            else
            {
                if (pow_n == false)
                {
                    number2 = (number2 * 10) + 9;
                    txtDisplay1.Text = number2.ToString();
                }
            }
        }

        /// <summary>
        /// Tlacidlo Div vypocita podiel pomocou funkcie <see cref="Mathematic_functions.Div(double, double)"/> z matematickej kniznice 
        /// </summary>
        /// <param name="sender">odosielatel (ovladaci prvok ktory vyvolava udalost)</param>
        /// <param name="e">objekt ktory uchovava uzitocne informacie (napr. pozicia kurzora mysi)</param>
        private void Div_Click(object sender, RoutedEventArgs e)
        {
            j = 0;
            dot = false;
            if (pow_n == true)
            {
                /// vypocet mocniny so vseoecnym exponentom pomocou <see cref="Pow_n()"/>
                Pow_n();
                pow_n = false;
            }
            /// ak nie je zadane druhe cislo
            if (number2 != 0)
            {
                /// prepinac porovnava aktualnu operaciu s predoslou za ucelom vyvarovania sa chyb v porovnavani znamienok
                switch (operation)
                {
                    case "+":
                        result = Mathematic_functions.Sum(number1, number2);
                        /// formatovany vypis prisposobeny poctu nacitanych cisel (number1, number2)
                        txtDisplay2.Text = number1 + " + " + number2.ToString();
                        txtDisplay1.Text = result.ToString();
                        /// vynulovanie oboch cisel za ucelom nobeho pouzitia tychto premennych s novymi nacitanymi cislami
                        number1 = number2 = 0;
                        break;
                    case "-":
                        result = Mathematic_functions.Min(number1, number2);
                        txtDisplay2.Text = number1 + " - " + number2.ToString();
                        txtDisplay1.Text = result.ToString();
                        number1 = number2 = 0;
                        break;
                    case "*":
                        result = Mathematic_functions.Mult(number1, number2);
                        txtDisplay2.Text = number1 + " x " + number2.ToString();
                        txtDisplay1.Text = result.ToString();
                        number1 = number2 = 0;
                        break;
                    case "/":
                        result = Mathematic_functions.Div(number1, number2);
                        txtDisplay2.Text = number1 + " ÷ " + number2.ToString();
                        txtDisplay1.Text = result.ToString();
                        number1 = number2 = 0;
                        break;
                    case "!":
                        result = Mathematic_functions.Fact(number1);
                        txtDisplay2.Text = number1 + " ÷ " + number2.ToString();
                        txtDisplay1.Text = result.ToString();
                        number1 = number2 = 0;
                        break;
                }
                /// uchovavanie prveho cisla za ucelom neskorsieho pouzitia pri vypise z cisla na pomocny displej
                number1 = result;
            }
            if (number2 == 0)
            {
                txtDisplay2.Text = number1.ToString() + " ÷";                                 //todo -- vyriesit 5 / - 4 a v tomto pripade vypisanie zaporneho vysledku
            }
            /// ak su nacitane oba cisla
            else
            {
                txtDisplay2.Text = number1.ToString() + " ÷ " + number2.ToString() + " ÷ ";
                result = Mathematic_functions.Div(number1, number2);
                number2 = 0;
                txtDisplay1.Text = result.ToString();
                number1 = result;

            }
            /// do premmenej operation nacita "/", ktora dava informaciu dalsej operacie/ dalsiemu cislu o zmene operacie
            operation = "/";
        }

        /// <summary>
        /// Tlacidlo Number4 vypise tlacidlo 4 na displej
        /// </summary>
        /// <param name="sender">odosielatel (ovladaci prvok ktory vyvolava udalost)</param>
        /// <param name="e">objekt ktory uchovava uzitocne informacie (napr. pozicia kurzora mysi)</param>
        private void Number_4_Click(object sender, RoutedEventArgs e)
        {
            if (operation == "" && dot == false)
            {
                number1 = (number1 * 10) + 4;
                txtDisplay1.Text = number1.ToString();
            }
            /// pri vypocte ukoncenom tlacidlom "=" vynuluje vsetky hodnoty (vycisti pamat) pre opatovne pouzitie
            else if (operation == "=")
            {
                number1 = number2 = 0;
                number1 = (number1 * 10) + 4;
                txtDisplay1.Text = number1.ToString();
            }
            /// exponent nacita vzdy do number3
            else if (pow_n == true)
            {
                number3 = (number3 * 10) + 4;
                txtDisplay1.Text = number3.ToString();
            }
            else if (dot == true)
            {
                /// počítadlo desatinných cifier
                j++;
                if (number2 == 0)
                {
                    /// úprava čísla na pôvodný tvar
                    for (int i = 0; i < j; i++)
                    {
                        number1 = (number1 * 10);
                    }
                    /// pridanie čísla tlačidla
                    number1 = number1 + 4;
                    /// pridanie desatinnej čiarky
                    for (int i = 0; i < j; i++)
                    {
                        number1 = (number1 / 10);
                    }
                    txtDisplay1.Text = number1.ToString();
                }
                /// načítanie do druhého čísla ak je prvé už načítané
                else if (number1 != 0 && number2 != 0)
                {
                    for (int i = 0; i < j; i++)
                    {
                        number2 = (number2 * 10);
                    }
                    number2 = number2 + 4;
                    for (int i = 0; i < j; i++)
                    {
                        number2 = (number2 / 10);
                    }
                    txtDisplay1.Text = number2.ToString();
                }
            }
            /// vykonava sa ak mame zadany nejaku operaciu (+, -. *, / ...)
            else
            {
                if (pow_n == false)
                {
                    number2 = (number2 * 10) + 4;
                    txtDisplay1.Text = number2.ToString();
                }
            }
        }

        /// <summary>
        /// Tlacidlo Number5 vypise tlacidlo 5 na displej
        /// </summary>
        /// <param name="sender">odosielatel (ovladaci prvok ktory vyvolava udalost)</param>
        /// <param name="e">objekt ktory uchovava uzitocne informacie (napr. pozicia kurzora mysi)</param>
        private void Number_5_Click(object sender, RoutedEventArgs e)
        {
            if (operation == "" && dot == false)
            {
                number1 = (number1 * 10) + 5;
                txtDisplay1.Text = number1.ToString();
            }
            /// pri vypocte ukoncenom tlacidlom "=" vynuluje vsetky hodnoty (vycisti pamat) pre opatovne pouzitie
            else if (operation == "=")
            {
                number1 = number2 = 0;
                number1 = (number1 * 10) + 5;
                txtDisplay1.Text = number1.ToString();
            }
            /// exponent vzdy nacita do number3
            else if (pow_n == true)
            {
                number3 = (number3 * 10) + 5;
                txtDisplay1.Text = number3.ToString();
            }
            else if (dot == true)
            {
                /// počítadlo desatinných cifier
                j++;
                if (number2 == 0)
                {
                    /// úprava čísla na pôvodný tvar
                    for (int i = 0; i < j; i++)
                    {
                        number1 = (number1 * 10);
                    }
                    /// pridanie čísla tlačidla
                    number1 = number1 + 5;
                    /// pridanie desatinnej čiarky
                    for (int i = 0; i < j; i++)
                    {
                        number1 = (number1 / 10);
                    }
                    txtDisplay1.Text = number1.ToString();
                }
                /// načítanie do druhého čísla ak je prvé už načítané
                else if (number1 != 0 && number2 != 0)
                {
                    for (int i = 0; i < j; i++)
                    {
                        number2 = (number2 * 10);
                    }
                    number2 = number2 + 5;
                    for (int i = 0; i < j; i++)
                    {
                        number2 = (number2 / 10);
                    }
                    txtDisplay1.Text = number2.ToString();
                }
            }
            /// vykonava sa ak mame zadany nejaku operaciu (+, -. *, / ...)
            else
            {
                if (pow_n == false)
                {
                    number2 = (number2 * 10) + 5;
                    txtDisplay1.Text = number2.ToString();
                }
            }
        }

        /// <summary>
        /// Tlacidlo Number6 vypise tlacidlo 6 na displej
        /// </summary>
        /// <param name="sender">odosielatel (ovladaci prvok ktory vyvolava udalost)</param>
        /// <param name="e">objekt ktory uchovava uzitocne informacie (napr. pozicia kurzora mysi)</param>
        private void Number_6_Click(object sender, RoutedEventArgs e)
        {
            if (operation == "" && dot == false)
            {
                number1 = (number1 * 10) + 6;
                txtDisplay1.Text = number1.ToString();
            }
            /// pri vypocte ukoncenom tlacidlom "=" vynuluje vsetky hodnoty (vycisti pamat) pre opatovne pouzitie
            else if (operation == "=")
            {
                number1 = number2 = 0;
                number1 = (number1 * 10) + 6;
                txtDisplay1.Text = number1.ToString();
            }
            /// exponent vzdy nacita do number3
            else if (pow_n == true)
            {
                number3 = (number3 * 10) + 6;
                txtDisplay1.Text = number3.ToString();
            }
            else if (dot == true)
            {
                /// počítadlo desatinných cifier
                j++;
                if (number2 == 0)
                {
                    /// úprava čísla na pôvodný tvar
                    for (int i = 0; i < j; i++)
                    {
                        number1 = (number1 * 10);
                    }
                    /// pridanie čísla tlačidla
                    number1 = number1 + 6;
                    /// pridanie desatinnej čiarky
                    for (int i = 0; i < j; i++)
                    {
                        number1 = (number1 / 10);
                    }
                    txtDisplay1.Text = number1.ToString();
                }
                /// načítanie do druhého čísla ak je prvé už načítané
                else if (number1 != 0 && number2 != 0)
                {
                    for (int i = 0; i < j; i++)
                    {
                        number2 = (number2 * 10);
                    }
                    number2 = number2 + 6;
                    for (int i = 0; i < j; i++)
                    {
                        number2 = (number2 / 10);
                    }
                    txtDisplay1.Text = number2.ToString();
                }
            }
            /// vykonava sa ak mame zadany nejaku operaciu (+, -. *, / ...)
            else
            {
                if (pow_n == false)
                {
                    number2 = (number2 * 10) + 6;
                    txtDisplay1.Text = number2.ToString();
                }
            }
        }

        /// <summary>
        /// Tlacidlo Multiply vypocita sucin pomocou funkcie <see cref="Mathematic_functions.Mult(double, double)"/> z matematickej kniznice
        /// </summary>
        /// <param name="sender">odosielatel (ovladaci prvok ktory vyvolava udalost)</param>
        /// <param name="e">objekt ktory uchovava uzitocne informacie (napr. pozicia kurzora mysi)</param>
        private void Multiply_Click(object sender, RoutedEventArgs e)
        {
            j = 0;
            dot = false;
            if (pow_n == true)
            {
                /// vypocet mocniny so vseoecnym exponentom pomocou <see cref="Pow_n()"/>
                Pow_n();
                pow_n = false;
            }
            if (number2 != 0)
            {
                /// prepinac porovnava aktualnu operaciu s predoslou za ucelom vyvarovania sa chyb v porovnavani znamienok
                switch (operation)
                {
                    case "+":
                        result = Mathematic_functions.Sum(number1, number2);
                        txtDisplay1.Text = result.ToString();
                        /// vynulovanie oboch cisel za ucelom noveho pouzitia tychto premennych s novymi nacitanymi cislami
                        number1 = number2 = 0;
                        break;
                    case "-":
                        result = Mathematic_functions.Min(number1, number2);
                        txtDisplay1.Text = result.ToString();
                        number1 = number2 = 0;
                        break;
                    case "*":
                        result = Mathematic_functions.Mult(number1, number2);
                        txtDisplay1.Text = result.ToString();
                        number1 = number2 = 0;
                        break;
                    case "/":
                        result = Mathematic_functions.Div(number1, number2);
                        txtDisplay1.Text = result.ToString();
                        number1 = number2 = 0;
                        break;

                }
                number1 = result;
            }
            /// formatovany vypis pre jedno nacitane cislo a operaciu
            if (number2 == 0)
            {
                txtDisplay2.Text = number1.ToString() + " x";                                    //todo -- vyriesit 5 * - 4 a aj 5 *(-4) a v tomto pripade vypisanie zaporneho vysledku
            }
            /// formatovany vypis pre dve cisla s operaciou
            else
            {
                txtDisplay2.Text = number1.ToString() + " x " + number2.ToString() + " x ";
                result = Mathematic_functions.Mult(number1, number2);
                number2 = 0;
                txtDisplay1.Text = result.ToString();
                number1 = result;

            }
            operation = "*";
        }

        /// <summary>
        /// Tlacidlo Number1 vypise tlacidlo 1 na displej
        /// </summary>
        /// <param name="sender">odosielatel (ovladaci prvok ktory vyvolava udalost)</param>
        /// <param name="e">objekt ktory uchovava uzitocne informacie (napr. pozicia kurzora mysi)</param>
        private void Umocnovanie_Click(object sender, RoutedEventArgs e)
        {
            j = 0;
            dot = false;
            if (pow_n == true)
            {
                /// vypocet mocniny so vseobecnym exponentom pomocou <see cref="Pow_n()"/>
                Pow_n();
                pow_n = false;
            }
            /// vypis znaku mocniny za prislusnym cislom
            if (number1 != 0 && number2 == 0)
            {
                txtDisplay1.Text = number1.ToString() + "^";
            }
            else if (number2 != 0 && number1 != 0)
            {
                txtDisplay1.Text = number2.ToString() + "^";
            }
            pow_n = true;
            operation = "^n";
        }

        /// <summary>
        /// Funkcia Pow_n sluzi na umocnenie cisla na lubovolny exponent pomocou funkcie <see cref="Mathematic_functions.Pow_n(double, double)"/> z matematickej kniznice
        /// </summary>
        /// <param name="sender">odosielatel (ovladaci prvok ktory vyvolava udalost)</param>
        /// <param name="e">objekt ktory uchovava uzitocne informacie (napr. pozicia kurzora mysi)</param>
        private void Pow_n()
        {
            j = 0;
            dot = false;
            if (number1 != 0 && number2 == 0)
            {
                number1 = Mathematic_functions.Pow_n(number1, number3);
                txtDisplay1.Text = number1.ToString();
            }
            else if (number1 != 0 && number2 != 0)
            {
                number2 = Mathematic_functions.Pow_n(number2, number3);
                txtDisplay1.Text = number2.ToString();
            }
            /// vynulovanie prislusnych hodnot z dovodu opatovneho pouzitia
            number3 = 0;
            pow_n = false;
        }

        /// <summary>
        /// Tlacidlo Number1 vypise tlacidlo 1 na displej
        /// </summary>
        /// <param name="sender">odosielatel (ovladaci prvok ktory vyvolava udalost)</param>
        /// <param name="e">objekt ktory uchovava uzitocne informacie (napr. pozicia kurzora mysi)</param>
        private void Number_1_Click(object sender, RoutedEventArgs e)
        {
            if (operation == "" && dot == false)
            {
                number1 = (number1 * 10) + 1;
                txtDisplay1.Text = number1.ToString();
            }
            /// pri vypocte ukoncenom tlacidlom "=" vynuluje vsetky hodnoty (vycisti pamat) pre opatovne pouzitie
            else if (operation == "=")
            {
                number1 = number2 = 0;
                number1 = (number1 * 10) + 1;
                txtDisplay1.Text = number1.ToString();
            }
            /// exponent nacita vzdy do number3
            else if (pow_n == true)
            {
                number3 = (number1 * 10) + 1;
                txtDisplay1.Text = number3.ToString();
            }
            /// výpočet desatinného čísla podmienený stlačením tlačidla desatinnej čiarky
            else if (dot == true)
            {
                /// počítadlo desatinných cifier
                j++;
                if (number2 == 0)
                {
                    /// úprava čísla na pôvodný tvar
                    for (int i = 0; i < j; i++)
                    {
                        number1 = (number1 * 10);
                    }
                    /// pridanie čísla tlačidla
                    number1 = number1 + 1;
                    /// pridanie desatinnej čiarky
                    for (int i = 0; i < j; i++)
                    {
                        number1 = (number1 / 10);
                    }
                    txtDisplay1.Text = number1.ToString();
                }
                /// načítanie do druhého čísla ak je prvé už načítané
                else if (number1 != 0 && number2 != 0)
                {
                    for (int i = 0; i < j; i++)
                    {
                        number2 = (number2 * 10);
                    }
                    number2 = number2 + 1;
                    for (int i = 0; i < j; i++)
                    {
                        number2 = (number2 / 10);
                    }
                    txtDisplay1.Text = number2.ToString();
                }
            }
            /// vykonava sa ak mame zadany nejaku operaciu (+, -. *, / ...)
            else
            {
                if (pow_n == false)
                {
                    number2 = (number3 * 10) + 1;
                    txtDisplay1.Text = number2.ToString();
                }
            }
        }

        /// <summary>
        /// Tlacidlo Number2 vypise tlacidlo 2 na displej
        /// </summary>
        /// <param name="sender">odosielatel (ovladaci prvok ktory vyvolava udalost)</param>
        /// <param name="e">objekt ktory uchovava uzitocne informacie (napr. pozicia kurzora mysi)</param>
        private void Number_2_Click(object sender, RoutedEventArgs e)
        {
            if (operation == "" && dot == false)
            {
                number1 = (number1 * 10) + 2;
                txtDisplay1.Text = number1.ToString();
            }
            /// pri vypocte ukoncenom tlacidlom "=" vynuluje vsetky hodnoty (vycisti pamat) pre opatovne pouzitie
            else if (operation == "=")
            {
                number1 = number2 = 0;
                number1 = (number1 * 10) + 2;
                txtDisplay1.Text = number1.ToString();
            }
            /// exponent naicta vzdy do number3
            else if (pow_n == true)
            {
                number3 = (number3 * 10) + 2;
                txtDisplay1.Text = number3.ToString();
            }
            /// výpočet desatinného čísla podmienený stlačením tlačidla desatinnej čiarky
            else if (dot == true)
            {
                /// počítadlo desatinných cifier
                j++;
                if (number2 == 0)
                {
                    /// úprava čísla na pôvodný tvar
                    for (int i = 0; i < j; i++)
                    {
                        number1 = (number1 * 10);
                    }
                    /// pridanie čísla tlačidla
                    number1 = number1 + 2;
                    /// pridanie desatinnej čiarky
                    for (int i = 0; i < j; i++)
                    {
                        number1 = (number1 / 10);
                    }
                    txtDisplay1.Text = number1.ToString();
                }
                /// načítanie do druhého čísla ak je prvé už načítané
                else if (number1 != 0 && number2 != 0)
                {
                    for (int i = 0; i < j; i++)
                    {
                        number2 = (number2 * 10);
                    }
                    number2 = number2 + 2;
                    for (int i = 0; i < j; i++)
                    {
                        number2 = (number2 / 10);
                    }
                    txtDisplay1.Text = number2.ToString();
                }
            }
            /// vykonava sa ak mame zadany nejaku operaciu (+, -. *, / ...)
            else
            {
                if (pow_n == false)
                {
                    number2 = (number2 * 10) + 2;
                    txtDisplay1.Text = number2.ToString();
                }
            }
        }

        /// <summary>
        /// Tlacidlo Number3 vypise tlacidlo 3 na displej
        /// </summary>
        /// <param name="sender">odosielatel (ovladaci prvok ktory vyvolava udalost)</param>
        /// <param name="e">objekt ktory uchovava uzitocne informacie (napr. pozicia kurzora mysi)</param>
        private void Number_3_Click(object sender, RoutedEventArgs e)
        {
            if (operation == "" && dot == false)
            {
                number1 = (number1 * 10) + 3;
                txtDisplay1.Text = number1.ToString();
            }
            /// pri vypocte ukoncenom tlacidlom "=" vynuluje vsetky hodnoty (vycisti pamat) pre opatovne pouzitie
            else if (operation == "=")
            {
                number1 = number2 = 0;
                number1 = (number1 * 10) + 3;
                txtDisplay1.Text = number1.ToString();
            }
            /// exponent nacita do number3
            else if (pow_n == true)
            {
                number3 = (number3 * 10) + 3;
                txtDisplay1.Text = number3.ToString();
            }
            /// výpočet desatinného čísla podmienený stlačením tlačidla desatinnej čiarky
            else if (dot == true)
            {
                /// počítadlo desatinných cifier
                j++;
                if (number2 == 0)
                {
                    /// úprava čísla na pôvodný tvar
                    for (int i = 0; i < j; i++)
                    {
                        number1 = (number1 * 10);
                    }
                    /// pridanie čísla tlačidla
                    number1 = number1 + 3;
                    /// pridanie desatinnej čiarky
                    for (int i = 0; i < j; i++)
                    {
                        number1 = (number1 / 10);
                    }
                    txtDisplay1.Text = number1.ToString();
                }
                /// načítanie do druhého čísla ak je prvé už načítané
                else if (number1 != 0 && number2 != 0)
                {
                    for (int i = 0; i < j; i++)
                    {
                        number2 = (number2 * 10);
                    }
                    number2 = number2 + 3;
                    for (int i = 0; i < j; i++)
                    {
                        number2 = (number2 / 10);
                    }
                    txtDisplay1.Text = number2.ToString();
                }
            }
            /// vykonava sa ak mame zadany nejaku operaciu (+, -. *, / ...)
            else
            {
                if (pow_n == false)
                {
                    number2 = (number2 * 10) + 3;
                    txtDisplay1.Text = number2.ToString();
                }
            }
        }

        /// <summary>
        /// Tlacidlo Plus vypocita sucet pomocou funkcie <see cref="Mathematic_functions.Sum(double, double)"/> z matematickej kniznice
        /// </summary>
        /// <param name="sender">odosielatel (ovladaci prvok ktory vyvolava udalost)</param>
        /// <param name="e">objekt ktory uchovava uzitocne informacie (napr. pozicia kurzora mysi)</param>
        private void Plus_Click(object sender, RoutedEventArgs e)
        {
            dot = false;
            j = 0;
            if (pow_n == true)
            {
                /// vypocet mocniny so vseobecnym exponentom pomocou <see cref="Pow_n()"/>
                Pow_n();
                pow_n = false;
            }
            if (number2 != 0)
            {
                /// prepinac porovnava aktualnu operaciu s predoslou za ucelom vyvarovania sa chyb v porovnavani znamienok
                switch (operation)
                {
                    case "+":
                        result = Mathematic_functions.Sum(number1, number2);
                        /// formatovany vypis prisposobeny poctu nacitanych cisel (number1, number2)
                        txtDisplay2.Text = number1 + " + " + number2.ToString();
                        txtDisplay1.Text = result.ToString();
                        /// vynulovanie oboch cisel za ucelom noveho pouzitia tychto premennych s novymi nacitanymi cislami
                        number1 = number2 = 0;
                        break;
                    case "-":
                        result = Mathematic_functions.Min(number1, number2);
                        txtDisplay2.Text = number1 + " - " + number2.ToString();
                        txtDisplay1.Text = result.ToString();
                        number1 = number2 = 0;
                        break;
                    case "*":
                        result = Mathematic_functions.Mult(number1, number2);
                        txtDisplay2.Text = number1 + " x " + number2.ToString();
                        txtDisplay1.Text = result.ToString();
                        number1 = number2 = 0;
                        break;
                    case "/":
                        result = Mathematic_functions.Div(number1, number2);
                        txtDisplay2.Text = number1 + " ÷ " + number2.ToString();
                        txtDisplay1.Text = result.ToString();
                        number1 = number2 = 0;
                        break;
                    case "^n":
                        Pow_n();

                        break;
                }
                /// ulozenie vysledku pre neskorsi vypis na pomocny displej
                number1 = result;
            }
            if (number2 == 0)
            {
                /// formatovany vypis pre jedno cislo a operaciu
                txtDisplay2.Text = number1.ToString() + " +";                                   //todo -- vyriesit 5 + - 4 (malo by fungovat kedze +- dava - )
            }                                                                                   // mozny navrh :
            else                                                                                // - po kazdom stlaceni operacie ulozenie danej operacie do premennej to jest posledny
            {                                                                                   // znak v boli txtDisplay2.Text
                /// formatovany vypis pre dva cisla
                txtDisplay2.Text = number1.ToString() + " + " + number2.ToString() + " + ";    // - pomocou switch skombinovat dane operacie
                result = Mathematic_functions.Sum(number1, number2);                            // GOOD LUCK
                number2 = 0;
                txtDisplay1.Text = result.ToString();
                number1 = result;
            }
            operation = "+";
        }

        /// <summary>
        /// Tlacidlo Odmocnina vypocita vseobecnu odmocninu pomocou funkcie <see cref="Mathematic_functions.Sqrt(double)"/> z matematickej kniznice
        /// </summary>
        /// <param name="sender">odosielatel (ovladaci prvok ktory vyvolava udalost)</param>
        /// <param name="e">objekt ktory uchovava uzitocne informacie (napr. pozicia kurzora mysi)</param>
        private void Odmocnina_Click(object sender, RoutedEventArgs e)
        {
            j = 0;
            dot = false;
            if (pow_n == true)
            {
                /// vypocet mocniny so vseobecnym exponentom pomocou <see cref="Pow_n()"/>
                Pow_n();
                pow_n = false;
            }
            if (number2 == 0)
            {
                number1 = Mathematic_functions.Sqrt(number1);
                txtDisplay1.Text = number1.ToString();
            }
            else if (number1 != 0 && number2 != 0)
            {

                number2 = Mathematic_functions.Sqrt(number2);
                txtDisplay1.Text = number2.ToString();
                result = number1;
            }
        }

        /// <summary>
        /// Tlacidlo Desatinna_bodka zada desatinnu ciarku za posledne nacitane cislo
        /// </summary>
        /// <param name="sender">odosielatel (ovladaci prvok ktory vyvolava udalost)</param>
        /// <param name="e">objekt ktory uchovava uzitocne informacie (napr. pozicia kurzora mysi)</param>
        private void Desatinna_Bodka_Click(object sender, RoutedEventArgs e)
        {
            dot = true;
        }

        /// <summary>
        /// Tlacidlo Number0 vypise tlacidlo 0 na displej
        /// </summary>
        /// <param name="sender">odosielatel (ovladaci prvok ktory vyvolava udalost)</param>
        /// <param name="e">objekt ktory uchovava uzitocne informacie (napr. pozicia kurzora mysi)</param>
        private void Number_0_Click(object sender, RoutedEventArgs e)
        {
            if (operation == "" && dot == false)
            {
                number1 = (number1 * 10);
                txtDisplay1.Text = number1.ToString();
            }
            /// pri vypocte ukoncenom tlacidlom "=" vynuluje vsetky hodnoty (vycisti pamat) pre opatovne pouzitie
            else if (operation == "=")
            {
                number1 = number2 = 0;
                number1 = (number1 * 10);
                txtDisplay1.Text = number1.ToString();
            }
            /// exponent nacita vzdy do number3
            else if (pow_n == true)
            {
                number3 = (number3 * 10);
                txtDisplay1.Text = number3.ToString();
            }
            /// výpočet desatinného čísla podmienený stlačením tlačidla desatinnej čiarky
            else if (dot == true)
            {
                /// počítadlo desatinných cifier
                j++;
                if (number2 == 0)
                {
                    /// úprava čísla na pôvodný tvar
                    for (int i = 0; i < j; i++)
                    {
                        number1 = (number1 * 10);
                    }
                    /// pridanie desatinnej čiarky
                    for (int i = 0; i < j; i++)
                    {
                        number1 = (number1 / 10);
                    }
                    txtDisplay1.Text = number1.ToString();
                }
                /// načítanie do druhého čísla ak je prvé už načítané
                else if (number1 != 0 && number2 != 0)
                {
                    for (int i = 0; i < j; i++)
                    {
                        number2 = (number2 * 10);
                    }
                    for (int i = 0; i < j; i++)
                    {
                        number2 = (number2 / 10);
                    }
                    txtDisplay1.Text = number2.ToString();
                }
                /// vykonava sa ak mame zadany nejaku operaciu (+, -. *, / ...)
            }
            else
            {
                number2 = (number2 * 10);
                txtDisplay1.Text = number2.ToString();
            }
        }

        /// <summary>
        /// Tlacidlo Equal(rovna sa) vracia vysledok vsetkych druhov matematickych operacii
        /// </summary>
        /// <param name="sender">odosielatel (ovladaci prvok ktory vyvolava udalost)</param>
        /// <param name="e">objekt ktory uchovava uzitocne informacie (napr. pozicia kurzora mysi)</param>
        private void Equal_Click(object sender, RoutedEventArgs e)
        {
            j = 0;
            if (pow_n == true)
            {
                /// vypocet mocniny so vseobecnym exponentom pomocou <see cref="Pow_n()"/>
                Pow_n();
                pow_n = false;
            }
            /// prepinac porovnava aktualnu operaciu s predoslou za ucelom vyvarovania sa chyb v porovnavani znamienok
            switch (operation)
            {
                case "+":
                    result = Mathematic_functions.Sum(number1, number2);
                    /// formatovany vypis prisposobeny poctu nacitanych cisel (number1, number2)
                    txtDisplay2.Text = number1 + " + " + number2.ToString();
                    txtDisplay1.Text = result.ToString();
                    /// vynulovanie oboch cisel za ucelom nobeho pouzitia tychto premennych s novymi nacitanymi cislami
                    number1 = number2 = 0;
                    break;
                case "-":
                    result = Mathematic_functions.Min(number1, number2);
                    txtDisplay2.Text = number1 + " - " + number2.ToString();
                    txtDisplay1.Text = result.ToString();
                    number1 = number2 = 0;
                    break;
                case "*":
                    result = Mathematic_functions.Mult(number1, number2);
                    txtDisplay2.Text = number1 + " x " + number2.ToString();
                    txtDisplay1.Text = result.ToString();
                    number1 = number2 = 0;
                    break;
                case "/":
                    result = Mathematic_functions.Div(number1, number2);
                    txtDisplay2.Text = number1 + " ÷ " + number2.ToString();
                    txtDisplay1.Text = result.ToString();
                    number1 = number2 = 0;
                    break;
                case "^n":
                    result = Mathematic_functions.Pow_n(number1, number2);
                    txtDisplay1.Text = result.ToString();
                    break;
            }
            /// zachovanie vysledku pre neskorsi vypis na pomocny displej
            number1 = result;
            txtDisplay1.Text = number1.ToString();
            /// kazdy jeden vysledok ulozi do ans
            ans = result;
            operation = "=";
        }

        /// <summary>
        /// Tlacidlo minus vypcita rozdiel dvoch cisel a pri zadani druheho cisla vypise znamienko na pomocny displej
        /// </summary>
        /// <param name="sender">odosielatel (ovladaci prvok ktory vyvolava udalost)</param>
        /// <param name="e">objekt ktory uchovava uzitocne informacie (napr. pozicia kurzora mysi)</param>
        private void Minus_Click(object sender, RoutedEventArgs e)
        {
            j = 0;
            dot = false;
            if (pow_n == true)
            {
                /// vypocet mocniny so vseoecnym exponentom pomocou <see cref="Pow_n()"/>
                Pow_n();
                pow_n = false;
            }
            if (number2 != 0)
            {
                /// prepinac porovnava aktualnu operaciu s predoslou za ucelom vyvarovania sa chyb v porovnavani znamienok
                switch (operation)                                                     
                {                                                                       
                    case "+":
                        result = Mathematic_functions.Sum(number1, number2);
                        /// formatovany vypis prisposobeny poctu nacitanych cisel (number1, number2)
                        txtDisplay2.Text = number1 + " + " + number2.ToString();
                        txtDisplay1.Text = result.ToString();
                        /// vynulovanie oboch cisel za ucelom nobeho pouzitia tychto premennych s novymi nacitanymi cislami
                        number1 = number2 = 0;
                        break;
                    case "-":
                        result = Mathematic_functions.Min(number1, number2);
                        txtDisplay2.Text = number1 + " - " + number2.ToString();
                        txtDisplay1.Text = result.ToString();
                        number1 = number2 = 0;
                        break;
                    case "*":
                        result = Mathematic_functions.Mult(number1, number2);
                        txtDisplay2.Text = number1 + " x " + number2.ToString();
                        txtDisplay1.Text = result.ToString();
                        number1 = number2 = 0;
                        break;
                    case "/":
                        result = Mathematic_functions.Div(number1, number2);
                        txtDisplay2.Text = number1 + " ÷ " + number2.ToString();
                        txtDisplay1.Text = result.ToString();
                        number1 = number2 = 0;
                        break;
                    case "!":
                        result = Mathematic_functions.Fact(number1);
                        txtDisplay2.Text = number1 + " !";
                        txtDisplay1.Text = result.ToString();
                        number1 = number2 = 0;
                        break;
                }
                /// zachovanie vysledku pre neskorsi vypis na pomocny displej
                number1 = result;
            }
            if (number2 == 0)
            {
                /// formatovany vypis pre jedno cislo a operaciu
                txtDisplay2.Text = number1.ToString() + " -";                                       
            }
            else
            {
                /// formatovany vypis pre dve cisla
                txtDisplay2.Text = number1.ToString() + " - " + number2.ToString() + " - ";
                result = Mathematic_functions.Min(number1, number2);
                number2 = 0;
                txtDisplay1.Text = result.ToString();
                number1 = result;

            }
            operation = "-";
        }

        /// <summary>
        /// Tlacidlo X^2 vypocita mocninu dvoch z nacitaneho cisla pomocou funkcie <see cref="Mathematic_functions.Pow(double)"/> z matematickej kniznice
        /// </summary>
        /// <param name="sender">odosielatel (ovladaci prvok ktory vyvolava udalost)</param>
        /// <param name="e">objekt ktory uchovava uzitocne informacie (napr. pozicia kurzora mysi)</param>
        private void X_na_druhu_Click(object sender, RoutedEventArgs e)
        {
            j = 0;
            dot = false;
            if (pow_n == true)
            {
                /// vypocet mocniny so vseoecnym exponentom pomocou <see cref="Pow_n()"/>
                Pow_n();
                pow_n = false;
            }
            if (number1 != 0 && number2 == 0)
            {
                number1 = Mathematic_functions.Pow(number1);
                /// vypis a prevod int na string
                txtDisplay1.Text = number1.ToString();
            }
            else if (number2 != 0 && number1 != 0)
            {
                number2 = Mathematic_functions.Pow(number2);
                txtDisplay2.Text = number2.ToString();
            }
        }

        /// <summary>
        /// Tlacidlo Save1 ulozi vybrane cislo do pamate pre jeho neskorsie pouzitie
        /// </summary>
        /// <param name="sender">odosielatel (ovladaci prvok ktory vyvolava udalost)</param>
        /// <param name="e">objekt ktory uchovava uzitocne informacie (napr. pozicia kurzora mysi)</param>
        private void Save1_Click(object sender, RoutedEventArgs e)
        {
            j = 0;
            dot = false;
            /// potrebne pri specifickom formate vstupu napr. <example>(2^3 + 5)</example>
            if (pow_n == true)
            {
                /// vypocet mocniny so vseoecnym exponentom pomocou <see cref="Pow_n()"/>
                Pow_n();
                pow_n = false;
            }
            if (result != 0)
            {
                double savedVal = result;
                if (number1 != 0)
                {
                    number2 = savedVal;
                    txtDisplay1.Text = savedVal.ToString();
                }
                else if (number2 != 0)
                {
                    /// prepinac porovnava aktualnu operaciu s predoslou za ucelom vyvarovania sa chyb v porovnavani znamienok
                    switch (operation)
                    {
                        case "+":
                            number2 = 0;
                            result = Mathematic_functions.Sum(number1, number2);
                            /// formatovany vystup prisposobeny poctu nacitanych cisel (number1, number2)
                            txtDisplay2.Text = number1 + " + " + number2.ToString();
                            txtDisplay1.Text = result.ToString();
                            /// vynulovanie oboch cisel za ucelom nobeho pouzitia tychto premennych s novymi nacitanymi cislami
                            number1 = number2 = 0;
                            break;
                        case "-":
                            result = Mathematic_functions.Min(number1, number2);
                            txtDisplay2.Text = number1 + " - " + number2.ToString();
                            txtDisplay1.Text = result.ToString();
                            number1 = number2 = 0;
                            break;
                        case "*":
                            result = Mathematic_functions.Mult(number1, number2);
                            txtDisplay2.Text = number1 + " x " + number2.ToString();
                            txtDisplay1.Text = result.ToString();
                            number1 = number2 = 0;
                            break;
                        case "/":
                            result = Mathematic_functions.Div(number1, number2);
                            txtDisplay2.Text = number1 + " ÷ " + number2.ToString();
                            txtDisplay1.Text = result.ToString();
                            number1 = number2 = 0;
                            break;
                    }
                    number1 = result;

                    number2 = savedVal;
                }
            }
        }

        /// <summary>
        /// Tlacidlo Logaritmus vypocita logaritmus o zaklade 10 pomocou funkcie <see cref="Mathematic_functions.Log10(double)"/>
        /// </summary>
        /// <param name="sender">odosielatel (ovladaci prvok ktory vyvolava udalost)</param>
        /// <param name="e">objekt ktory uchovava uzitocne informacie (napr. pozicia kurzora mysi)</param>
        private void Logaritmus_Click(object sender, RoutedEventArgs e)
        {
            j = 0;
            dot = false;
            /// osetrenie pripadu napr. : log 5 + 2^3
            if (pow_n == true)
            {
                /// vypocet mocniny so vseoecnym exponentom pomocou <see cref="Pow_n()"/>
                Pow_n();
                pow_n = false;
            }
            if (number2 == 0)
            {
                /// volanie funkcie z Matematickej kniznice, prevod na string a vypis vysledku
                number1 = Mathematic_functions.Log10(number1);
                txtDisplay1.Text = number1.ToString();
            }
            else if (number1 != 0 && number2 != 0)
            {
                /// volanie funkcie z Matematickej kniznice, prevod na string a vypis vysledku
                number2 = Mathematic_functions.Log10(number2);
                txtDisplay1.Text = number2.ToString();
                /// zachovanie prveho nacitaneho cisla za ucelom neskorsieho pouzitia v pomocnom displeji
                result = number1;
            }
        }

        /// <summary>
        /// Tlacidlo Switch otvori okno ScienceCalculator a zatvori aktualne
        /// </summary>
        /// <param name="sender">odosielatel (ovladaci prvok ktory vyvolava udalost)</param>
        /// <param name="e">objekt ktory uchovava uzitocne informacie (napr. pozicia kurzora mysi)</param>
        private void Switch_Click(object sender, RoutedEventArgs e)
        {
            VedeckaKalkulacka vk = new VedeckaKalkulacka();
            vk.Show();
            this.Close();
        }
    }
}
