using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conversie
{
    class Program
    {
        static public int b1,b2;
        static public string nr;
        static public bool periodicB10 = false;
        //static public char[] bazele = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F'};
        static void Main(string[] args)
        {
            Console.Write("Introduceti prima baza:");
            b1 = int.Parse(Console.ReadLine());
            if (b1 < 2 || b1 > 16) Console.WriteLine("Baza incorecta.");
            else 
            {
            Console.Write("Introduceti a doua baza:");
            b2 = int.Parse(Console.ReadLine());
            if (b2 < 2 || b2 > 16) Console.WriteLine("Baza incorecta.");
            else
            {
                Console.Write($"Introduceti numarul in baza {b1} (partea zecimala este impartita de caracterul \",\"):");
                nr = Console.ReadLine();
                if (NrBazaCorecta())
                {
                    if (b1 != 10) nr = ConvertToB10(nr);
                    if (b2 != 10) nr = ConvertToB2(nr);
                    Console.WriteLine(nr);
                    Console.ReadKey();
                }
                else
                { Console.WriteLine($"Numarul nu este in baza {b1}!"); Console.ReadKey(); }
            }
            }
        }

        private static bool NrBazaCorecta()
        {
            int x;
            for(int i = 0 ; i < nr.Length ; i++)
            {
                x = Convert.ToInt32(nr[i]);
                if (nr[i] != ',' && nr[i]!='(' && nr[i] != ')')
                {
                    if (!(x < 58 && x > 47) && !(x > 64 && x < 72)) return false;
                    else
                    {
                        if (x < 58 && x > 47)
                        { if ((x - 48) >= b1) return false; }
                        else
                        { if ((x - 55) >= b1) return false; }
                    }
                }
               
            }
            return true;
        }

        private static string ConvertToB2(string nr)
        {
            List<int> LI = new List<int>();
            List<int> LD = new List<int>();
            List<double> rest = new List<double>();
            int intreg;
            double zecimal;
            bool periodic = false;
            StringBuilder final = new StringBuilder();

            if (periodicB10)
                nr = NoPeriod(nr);
            intreg = GetIntegerPart(nr);
            zecimal = GetDecimalPart(nr);

            if (intreg == 0) LI.Add(0);
            while (intreg!=0)
            {
                LI.Add(intreg%b2);
                intreg /= b2;
            }

            for (int i = LI.Count - 1; i >= 0; i--)
            { 
                switch(LI[i])
                {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                    case 6:
                    case 7:
                    case 8:
                    case 9:
                    final.Append(LI[i]); break;
                    case 10: final.Append('A'); break;
                    case 11: final.Append('B'); break;
                    case 12: final.Append('C'); break;
                    case 13: final.Append('D'); break;
                    case 14: final.Append('E'); break;
                    case 15: final.Append('F'); break;
                }

            }

            int zec2;

            while (zecimal != 0)
            {
                if (!rest.Contains(zecimal))
                {
                    rest.Add(zecimal);
                    zecimal *= b2;
                    zec2 = GetIntegerPart(Convert.ToString(zecimal));
                    LD.Add(zec2);
                    zecimal = GetDecimalPart(Convert.ToString(zecimal));
                }
                else
                { periodic = true; break; }
            }

            if (LD.Count > 0)
            {
                final.Append(',');
                if (!periodic)
                {
                    for (int i = 0; i < LD.Count; i++)
                    {
                        switch (LD[i])
                        {
                            case 0:
                            case 1:
                            case 2:
                            case 3:
                            case 4:
                            case 5:
                            case 6:
                            case 7:
                            case 8:
                            case 9:
                                final.Append(LD[i]); break;
                            case 10: final.Append('A'); break;
                            case 11: final.Append('B'); break;
                            case 12: final.Append('C'); break;
                            case 13: final.Append('D'); break;
                            case 14: final.Append('E'); break;
                            case 15: final.Append('F'); break;
                        }

                    }
                }
                else
                {
                    
                    for (int i = 0; i < LD.Count; i++)
                    {
                        if(rest[i] == zecimal)
                            final.Append('(');
                        switch (LD[i])
                        {
                            case 0:
                            case 1:
                            case 2:
                            case 3:
                            case 4:
                            case 5:
                            case 6:
                            case 7:
                            case 8:
                            case 9:
                                final.Append(LD[i]); break;
                            case 10: final.Append('A'); break;
                            case 11: final.Append('B'); break;
                            case 12: final.Append('C'); break;
                            case 13: final.Append('D'); break;
                            case 14: final.Append('E'); break;
                            case 15: final.Append('F'); break;
                        }

                    }
                    final.Append(')');
                }

            }

            return final.ToString();
        }

        private static string NoPeriod(string nr)
        {
            StringBuilder final = new StringBuilder();
            int i = 0;

            while (nr[i] != '(' && i < nr.Length)
            {
                final.Append(nr[i]);
                i++;
            }
            i++;
            final.Append(nr[i]);
            return Convert.ToString(final);
        }

        private static double GetDecimalPart(string nr)
        {
            int i, p=0;
            double zecimal = 0;
            for (i = 0; i < nr.Length; i++)
            {
                if (!(nr[i] != ','))
                    break;
            }
            for (i++; i < nr.Length; i++)
            { zecimal = zecimal * 10 + Convert.ToInt32(nr[i]) - '0'; p++; }
            zecimal = zecimal / Math.Pow(10, p);
            return zecimal;
        }

        private static int GetIntegerPart(string nr)
        {
            int intreg=0;
            for (int i = 0; i < nr.Length; i++)
            {
                if (nr[i] != ',')
                {
                    intreg = intreg * 10 + Convert.ToInt32(nr[i]) - '0';
                }
                else
                    break;
            }
            return intreg;
        }

        private static string ConvertToB10(string nr)
        {
            int i,intreg = 0, zecimal =0,j;
            //long numar_int = 0;
            double inb10 = 0;
            string finalb10;

            /*int x;
            for (i = 0; i < nr.Length; i++)
            {
                x = Convert.ToInt32(nr[i]);
                if (nr[i] != ',')
                {
                    if(x > 64 && x < 72)
                    numar_int = numar_int * 10 + x - 55;
                    else
                    numar_int = numar_int*10 + x - 48;  
                }
            }*/
            for (i = 0; i < nr.Length; i++)
            {
                if (nr[i] != ',')
                {
                    intreg++;
                }
                else
                    break;
            }
            for (i ++; i < nr.Length; i++)
            {
                zecimal++; ;
            }
            j = nr.Length-1;
            for(i=-zecimal; i < intreg;i++)
            {
                switch(nr[j])
                {
                    case '0':
                    case '1':
                    case '2':
                    case '3':
                    case '4':
                    case '5':
                    case '6':
                    case '7':
                    case '8':
                    case '9':
                        inb10 += (Convert.ToInt32(nr[j])-'0') * Math.Pow(b1, i); break;
                    case 'A': 
                    case 'B': 
                    case 'C': 
                    case 'D': 
                    case 'E': 
                    case 'F':
                        inb10 += (Convert.ToInt32(nr[j]) - 55) * Math.Pow(b1, i); break;

                }
                /*x = Convert.ToInt32(nr[j]);
                if(x > 64 && x < 72)
                    inb10 += (x - 55) * Math.Pow(b1, i);
                else
                    inb10 += (x - 48) * Math.Pow(b1, i);
                */
                j--;
                if (j > 0 && nr[j] == ',') j--;
                
            }
            finalb10 = Convert.ToString(inb10);
            StringBuilder final = new StringBuilder();
            List<char> rest = new List<char>();
            char ultrest='0';
            
            if (zecimal != 0)
            {
                i = 0;
                while (finalb10[i] != ',') i++;
                for (i = i+1; i < finalb10.Length; i++)
                {
                    if (!(rest.Contains(finalb10[i])))
                        rest.Add(finalb10[i]);
                    else
                    { periodicB10 = true; ultrest = finalb10[i]; break; }
                }
                i = 0;
                if (periodicB10)
                {
                    while (finalb10[i] != ',')
                    {
                        final.Append(finalb10[i]);
                        i++;
                    }
                    final.Append(',');
                    for (i = 0; i < rest.Count; i++)
                    {
                        if (rest[i] == ultrest)
                            final.Append('(');
                        final.Append(rest[i]);
                    }
                    final.Append(')');
                }
                else
                    return finalb10;
            }
            else
                return finalb10;

            /*if (zecimal != 0)
            {
                while (finalb10[i] != ',' && i < finalb10.Length)
                {
                    final.Append(finalb10[i]);
                    i++;
                }
                if (i < finalb10.Length)
                {
                    final.Append(',');

                    for (i = i + 1; i < finalb10.Length - 1; i++)
                    {
                        if (finalb10[i] != finalb10[i + 1])
                        {
                            final.Append(finalb10[i]);
                        }
                        else
                        {
                            final.Append('(');
                            final.Append(finalb10[i]);
                            final.Append(')');
                            periodicB10 = true;
                            break;

                        }
                    }
                    if (!periodicB10) final.Append(finalb10[i]);
                }

            }
            else
                return finalb10;*/
            return Convert.ToString(final);
        }
    }
}
