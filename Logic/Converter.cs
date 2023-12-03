using System.Text;

namespace Logic;

public static class Converter
{    
    public static bool[] StringToBoolArray(this string source) 
    {
        if (source.Length < 1)
            throw new ArgumentException($"Длина строки должна быть больше 0.");
        var result = new bool[source.Length];
        for(int i = 0; i < source.Length; ++i) 
            if (source[i] == '1')
                result[i] = true;
            else if (source[i] == '0')
                result[i] = false;
            else
                throw new ArgumentException("Строка должна состоять только из символов 0 или 1.");
        return result;
    }
    public static string BoolArrayToString(this bool[] source) 
    {
        var stringBuilder = new StringBuilder();
        for(int i = 0; i < source.Length; ++i) 
            stringBuilder.Append(source[i] ? "1" : "0");
        return stringBuilder.ToString();
    }
    public static double BoolArrayToDouble(this bool[] source) 
    {
        if (source.Length < 2)
            throw new ArgumentException($"Длина массива должна быть как минимум 2.");
        double result = 0;
        for(int i = 1; i < source.Length; ++i) 
            if (source[i])
                result += Math.Pow(2d, -1d * (double)i);
        if (source[0])
            result *= -1d;
        return result;
    }
}
