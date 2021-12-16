using System;
using System.Collections.Generic;
using System.Text;

namespace Measurements
{
    class Calculator
    {
        public Calculator()
        {
        }

        private double get_expected_value(List<double> data)
        {
            double sum = 0;

            foreach (double value in data)
            {
                sum += value;
            }

            return sum / data.Count;
        }

        private double get_correction(List<double> data, double delta)
        {
            double standart_neopr_B;

            standart_neopr_B = delta / Math.Sqrt(3);

            return standart_neopr_B;
        }

        public struct neopredelennost {
            public double neopr_a_value;
            public double neopr_a_percent;
            public double neopr_b_value;
            public double neopr_b_percent;
            public double summarn_neopr;
        };

        public void round_if_required(ref double num, int pace)
        {
            if (pace != 0)
            {
                num = Math.Round(num, pace);
            }
        }

        //public neopredelennost get_neopredelennost(List<double> data, double delta, int max_pace)
        //{
        //    neopredelennost result = new neopredelennost();

        //    double standart_neopr_A = 0;
        //    double standart_neopr_B;

        //    /// TODO: correct this part
        //    /// 
        //    double expected_value = get_expected_value(data);
        //    round_if_required(ref expected_value, max_pace);

        //    foreach (double value in data)
        //    {
        //        double pow = Math.Pow(value - expected_value, 2);
        //        round_if_required(ref pow, max_pace);
        //        standart_neopr_A += pow;
        //        round_if_required(ref standart_neopr_A, max_pace);
        //    }

        //    standart_neopr_A = standart_neopr_A / (data.Count * (data.Count - 1));
        //    round_if_required(ref standart_neopr_A, max_pace);
        //    // standart_neopr_A = Math.Sqrt(standart_neopr_A);
        //    double sqrt_3 = Math.Sqrt(3);
        //    round_if_required(ref sqrt_3, max_pace);
        //    round_if_required(ref delta, max_pace);
        //    standart_neopr_B = delta / sqrt_3;
        //    round_if_required(ref standart_neopr_B, max_pace);

        //    result.neopr_a_value = standart_neopr_A;
        //    result.neopr_b_value = standart_neopr_B;

        //    double pow_a = Math.Pow(standart_neopr_A, 2);
        //    round_if_required(ref pow_a, max_pace);
        //    double pow_b = Math.Pow(standart_neopr_B, 2);
        //    round_if_required(ref pow_b, max_pace);

        //    double sum_neopredelennost = Math.Sqrt(pow_a + pow_b);
        //    round_if_required(ref sum_neopredelennost, max_pace);
        //    result.summarn_neopr = sum_neopredelennost;
        //    double pow_sum = Math.Pow(sum_neopredelennost, 2);

        //    result.neopr_a_percent = pow_a / pow_sum;


        //    result.neopr_b_percent = pow_b / pow_sum;

        //    return result;
        //}

        public neopredelennost get_neopredelennost(List<double> data, double delta)
        {
            neopredelennost result = new neopredelennost();

            double standart_neopr_A = 0;
            double standart_neopr_B;

            /// TODO: correct this part
            /// 
            double expected_value = get_expected_value(data);
            foreach (double value in data)
            {
                standart_neopr_A += Math.Pow(value - expected_value, 2);
            }

            standart_neopr_A = standart_neopr_A / (data.Count * (data.Count - 1));
            // standart_neopr_A = Math.Sqrt(standart_neopr_A);
            standart_neopr_B = delta / Math.Sqrt(3);

            result.neopr_a_value = standart_neopr_A;
            result.neopr_b_value = standart_neopr_B;

            double sum_neopredelennost = Math.Sqrt(Math.Pow(standart_neopr_A, 2) + Math.Pow(standart_neopr_B, 2));
            result.summarn_neopr = sum_neopredelennost;
            result.neopr_a_percent = Math.Pow(standart_neopr_A, 2) / Math.Pow(sum_neopredelennost, 2) * 100;
            result.neopr_b_percent = Math.Pow(standart_neopr_B, 2) / Math.Pow(sum_neopredelennost, 2) * 100;

            return result;
        }

        public double get_extended_neopredelennost(List<double> data, double delta, double koef)
        {
            neopredelennost neopr = get_neopredelennost(data, delta);

            return neopr.summarn_neopr * koef;
        }

        public double get_value(List<double> data, double delta)
        {
            return get_expected_value(data) + get_correction(data, delta);
        }

    }
}
