using MathNet.Numerics;
using System.Numerics;
using Fourier;

class Program
{
    static string GetWordFromResult(Complex[] result)
    {
        var modulos = result.Select(compl => Complex.Abs(compl)).Take(7);
        var roundedModulos = modulos.Select(modulo => Math.Round(modulo));
        var symbolsCodes = roundedModulos.Select(modulo => (char)modulo);
        var str = string.Join("", symbolsCodes);
        return str;
    }
    static Complex[] GetSignalFromFile()
    {
        var signal = new StreamReader(@"C:\Users\Vladimir\Desktop\10.txt")
            .ReadToEnd()
            .Trim()
            .Split("\n")
            .Select(sig => float.Parse(sig));
        var signalComplex = signal.Select(sig => new Complex(sig, 0))
                                  .ToArray();
        return signalComplex;
    } 
    public static void Main()
    {
        var signalComplex = GetSignalFromFile();

        var dftRes = FourierFuncs.DFT(signalComplex);
        var fftRes = FourierFuncs.FFT(signalComplex);
        var bipRes = FourierFuncs.BinaryInversePermutate(fftRes!);

        var dft_word = GetWordFromResult(dftRes);
        var fft_word = GetWordFromResult(bipRes);

        Console.WriteLine("Слово, полученное DFT: " + dft_word );
        Console.WriteLine("Слово, полученное FFT: " + fft_word);
    }
}
