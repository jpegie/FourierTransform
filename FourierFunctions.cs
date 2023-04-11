using System.Numerics;

namespace Fourier
{
    public static class FourierFuncs
    {
        public static Complex[] DFT(Complex[] signal)
        {
            var length = signal.Length;
            var dft_res = new Complex[length];
            for (int k = 0; k < length; k++)
            {
                Complex sum = 0;
                for (int n = 0; n < length; n++)
                {
                    sum += signal[n] * Complex.Exp(-2 * Math.PI * Complex.ImaginaryOne * k * n / length);
                }
                dft_res[k] = sum;
            }
            return BinaryInversePermutate(dft_res);
        }

        public static Complex[]? FFT(Complex[] signal)
        {
            if (signal.Length < 2)
            {
                return null;
            };

            var length = signal.Length / 2;
            var x_even = new Complex[length];
            var x_odd = new Complex[length];

            for (int i = 0; i < length; i++)
            {
                var even = signal[i];
                var odd = signal[length + i];

                x_even[i] = even + odd;
                x_odd[i] = (even - odd) * GetW(length, i);
            }

            FFT(x_even);
            FFT(x_odd);

            for (int i = 0, j = 0; i < length; i++)
            {
                signal[j++] = x_even[i];
                signal[j++] = x_odd[i];
            }
            return signal;
        }

        public static Complex[] BinaryInversePermutate(Complex[] permutated)
        {
            double factor = permutated.Length / 2;
            for (var i = 0; i < permutated.Length; i++)
            {
                permutated[i] /= factor;
            }
            return permutated;
        }

        static Complex GetW(int length, int i)
        {
            var term = - Math.PI * i / length;
            var res = new Complex(Math.Cos(term), Math.Sin(term));
            return res;
        }
    }
}