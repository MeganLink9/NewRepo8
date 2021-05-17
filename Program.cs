using System;

namespace ConsoleApp8
{

    /*
     * Раціональний дріб представлена парою цілих чисел, де чисельник, знаменник. 
     * Створити клас Rational для роботи з раціональними дробами. Обов'язково повинні 
     * бути реалізовані наступні операції:
        - додавання 
        - віднімання 
        - множення dcd
        - розподіл 
        - порівняння більше, менше, дорівнює.
     */


    public class Rational
    {
        //Чисельник та знаменик
        public int Numerator { get; set; }
        public int Denominator { get; set; }

        #region private_methods

        //Скорочуємо дріб
        private void Reduce1()
        {
            this.Numerator = this.Numerator > 0 ? this.Numerator : -this.Numerator;
            this.Denominator = this.Denominator > 0 ? this.Denominator : -this.Denominator;

            int maxval = Numerator > Denominator ? Numerator : Denominator;
            for (int i = maxval; i >= 2; maxval--)
            {
                if (Numerator % maxval == 0 && Denominator % maxval == 0)
                {
                    this.Numerator /= maxval;
                    this.Denominator /= maxval;
                    break;
                }
            }
        }

        //найменший спільник знаменик
        private static int NOZ(int[] maxmin)
        {
            Array.Sort(maxmin);
            if (maxmin[1] % maxmin[0] == 0)
                return maxmin[1];

            int temp = 0;
            for (int i = 2; ; i++)
            {
                temp = maxmin[1] * i;
                if (temp % maxmin[0] == 0)
                {
                    break;
                }
            }
            return temp;
        }

        #endregion

        //Яко в знаменику 0
        public bool IsNan
        {
            get { return this.Denominator != 0 ? false : true; }
        }

        //Скрочення дроба якщо можливо
        public Rational(int numerator, int denominator = 1)
        {
            if (numerator == 0)
            {
                this.Numerator = 0;
                this.Denominator = 1;
            }
            else if (numerator == denominator)
            {
                this.Numerator = 1;
                this.Denominator = 1;
            }
            else if (numerator > 0 && denominator > 0 || numerator < 0 && denominator < 0)
            {
                this.Numerator = numerator;
                this.Denominator = denominator;
                Reduce1();

            }
            else if (numerator < 0 || denominator < 0)
            {
                this.Numerator = numerator;
                this.Denominator = denominator;
                Reduce1();
                this.Numerator *= -1;
            }
        }


        //Додавання
        public static Rational operator +(Rational a, Rational b)
        {
            if (a.Denominator == 0 || b.Denominator == 0)
                return new Rational(1, 0);

            Rational result1 = new Rational(1), result2 = new Rational(1);
            int noz = NOZ(new int[2] { a.Denominator, b.Denominator });
            result1.Numerator = noz / a.Denominator * a.Numerator;
            result2.Numerator = noz / b.Denominator * b.Numerator;
            return new Rational(result1.Numerator + result2.Numerator, noz);
        }

        //Віднімання
        public static Rational operator -(Rational a, Rational b)
        {
            if (a.Denominator == 0 || b.Denominator == 0)
                return new Rational(1, 0);

            Rational result1 = new Rational(1), result2 = new Rational(1);
            int noz = NOZ(new int[2] { a.Denominator, b.Denominator });
            result1.Numerator = noz / a.Denominator * a.Numerator;
            result2.Numerator = noz / b.Denominator * b.Numerator;
            return new Rational(result1.Numerator - result2.Numerator, noz);
        }

        //Множенння
        public static Rational operator *(Rational a, Rational b)
        {
            if (a.Denominator == 0 || b.Denominator == 0)
                return new Rational(1, 0);

            return new Rational(a.Numerator * b.Numerator, a.Denominator * b.Denominator);
        }

        //Ділення
        public static Rational operator /(Rational a, Rational b)
        {
            if (a.Denominator == 0 || b.Numerator == 0 || b.Denominator == 0)
                return new Rational(1, 0);

            return new Rational(a.Numerator / b.Numerator, a.Denominator / b.Denominator);
        }

        //Переробка методу ToString() для зручного виводу
        public override string ToString()
        {
            return String.Format("{0}/{1}", this.Numerator, this.Denominator);
        }


    }

    class Program
    {
        private static void print(object outtext)
        {
            Console.WriteLine("_______\nЗадача: " + outtext);
        }

        public static void Main()
        {
            Console.WriteLine("Введіт чисельник 1 числа: ");
            int a = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введіт знаменик 1 числа: ");
            int b = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Введіт чисельник 2 числа: ");
            int a2 = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введіт знаменик 2 числа: ");
            int b2 = Convert.ToInt32(Console.ReadLine());


            Rational r35 = new Rational(a, b);
            Rational r36 = new Rational(a2, b2);

            Console.WriteLine("1 Раціональний дріб " + a + "/" + b + ", якщо можливе скорочення: " + r35);
            Console.WriteLine("2 Раціональний дріб " + a2 + "/" + b2 + ", якщо можливе скорочення: " + r36);

            //Додавання
            Rational sumdr = r35 + r36;
            print("Додвання раціональних дробів " + r35 + " и " + r36 + " \n" + sumdr);

            //Віднімання
            print("Віднімання раціональних дробів: " + r35 + " и " + r36 + " \n" + (r35 - r36));

            //Множення
            print("Множення раціональних дробів: " + r35 + " и " + r36 + " \n" + (r35 * r36));

            //Ділення
            print("Ділення раціональних дробів: " + r35 + " и " + r36 + " \n" + (r35 / r36));



        }
    }



}
