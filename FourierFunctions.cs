using System.Numerics;

namespace Fourier
{
    public static class FourierFuncs
    {
        public static Complex[] DFT(Complex[] signal)
        {
            var length = signal.Length;
            var res = new Complex[length];
            for (int k = 0; k < length; k++)
            {
                Complex sum = 0;
                for (int n = 0; n < length; n++)
                {
                    sum += signal[n] * Complex.Exp(-2 * Math.PI * Complex.ImaginaryOne * k * n / length);
                }
                res[k] = sum;
            }
            return Normalize(res);
        }

        public static Complex[]? FFT(Complex[] signal)
        {
            if (signal.Length < 2)
            {
                return null;
            };

            var length = signal.Length / 2;
            var leftPart = new Complex[length];
            var rightPart = new Complex[length];

            for (int i = 0; i < length; i++)
            {
                var leftElem = signal[i];
                var rightElem = signal[length + i];

                leftPart[i] = leftElem + rightElem;
                rightPart[i] = (leftElem - rightElem) * GetW(length, i);
            }

            FFT(leftPart);
            FFT(rightPart);

            for (int i = 0, j = 0; i < length; i++)
            {
                signal[j++] = leftPart[i];
                signal[j++] = rightPart[i];
            }
            return signal;
        }

        public static Complex[] Normalize(Complex[] data)
        {
            double normalizationFactor = data.Length / 2;
            for (var i = 0; i < data.Length; i++)
            {
                data[i] /= normalizationFactor;
            }
            return data;
        }

        static Complex GetW(int length, int i)
        {
            var term = -Math.PI * i / length;
            var res = new Complex(Math.Cos(term), Math.Sin(term));
            return res;
        }
    }
}