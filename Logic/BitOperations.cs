namespace Logic;

public static class BitOperations
{
    public const int BitSize = 16;
    public static bool[] Zero { get; } = new bool[BitSize];
    public static bool[] One { get; } = new bool[BitSize];
    static BitOperations() 
    {
        One[BitSize - 1] = true;
    }        
    public static bool[] Equivalence(bool[] a, bool[] b, out bool OV, out bool Z)
    {
        if (a.Length != b.Length)
            throw new ArgumentException($"Длины массивов должны быть одинаковыми.");
        var result = new bool[a.Length];
		var i = 16;
		do
		{
			result[0] = a[0] == b[0];
			result = CycleShift(result, -1);
			a = CycleShift(a, -1);
			b = CycleShift(b, -1);
			--i;
		}while(i > 0);
        if(IsEqual(result, Zero))
            Z = true;
        else
            Z = false;
        OV = false;

        return result;
    }
    public static bool[] Divide(bool[] a, bool[] b, out bool OV, out bool Z)
    {        
		//y1 a = value
		//y2 b = value
		//y3 ov = false
		//y4 z = false
        if (a.Length != b.Length || a.Length != BitSize)
            throw new ArgumentException($"Длины массивов должны быть одинаковыми и равны {BitSize}.");
            
        var c = new bool[a.Length]; //y5c = 0
        c[c.Length - 1] = a[0] ^ b[0]; //y6 запись знака в последний бит C
        a[0] = false; //y7модуль a
        b[0] = false; //y8модуль b
		if(IsGreater(a, b) || IsEqual(a, b) || IsEqual(b, Zero))
            OV = true;
        else
            OV = false;    
        a = CycleShift(a, 1); //y9 сдвиг влево с 0 
        a[a.Length - 1] = false;
		var i = 15;
		do
		{
			b[0] = true; //y10 смена знака на отрицательный
            a = Add(a, GetAdditionalCode(b)); 
            if(!a[0])
            {
                c = CycleShift(c, 1); 
                c[c.Length - 1] = true;                           
            }
            else
            {
                b[0] = false;
                a = Add(a, b);
                c = CycleShift(c, 1); 
                c[c.Length - 1] = false;
            }
            a = CycleShift(a, 1);
            a[a.Length - 1] = false;
			--i; 
		}while(i > 0);
        if(IsEqual(c, Zero))
            Z = true;
        else
            Z = false;        
        return c;
    }
    private static bool[] GetAdditionalCode(bool[] source) 
    {
        var result = new bool[source.Length];
        source.CopyTo(result, 0);
        if (!source[0])
            return result;
        else
        {
            for (int i = 1; i < result.Length; ++i)
                result[i] ^= true;
            result = Add(result, One);
        }
        return result;
    }
    private static bool IsGreater(bool[] op1, bool[] op2) 
    {
        if (op1.Length != op2.Length)
            throw new ArgumentException("Длины массивов должны быть одинаковыми.");
        if (op1[0] && op2[0])
        {
            bool? result = null;
            for (int i = 1; i < op1.Length && result == null; ++i)
            {
                if (op1[i] && !op2[i])
                    result = false;
                else if (!op1[i] && op2[i])
                    result = true;
            }
            result ??= false;
            return (bool)result;
        }
        else if (op1[0])
        {
            return false;
        }
        else if (op2[0]) 
        {
            return true;
        }
        else 
        {
            bool? result = null;
            for(int i = 1; i < op1.Length && result == null; ++i) 
            {
                if (op1[i] && !op2[i])
                    result = true;
                else if (!op1[i] && op2[i])
                    result = false;
            }
            result ??= false;
            return (bool)result;
        }
    }
    private static bool IsEqual(bool[] op1, bool[] op2) 
    {
        if (op1.Length != op2.Length)
            return false;
        else
        {
            bool result = true;
            for (int i = 0; i < op1.Length && result; ++i)
            {
                if (op1[i] != op2[i])
                    result = false;
            }
            return result;
        }            
    }    
    private static bool[] Add(bool[] op1, bool[] op2)
    {
        if (op1.Length != op2.Length)
            throw new ArgumentException("Длины массивов должны быть одинаковыми.");
        var result = new bool[op1.Length];
        int next = 0, cur1, cur2, sum;
        for (int i = op1.Length - 1; i > -1; --i)
        {
            if (op1[i])
                cur1 = 1;
            else
                cur1 = 0;

            if (op2[i])
                cur2 = 1;
            else
                cur2 = 0;

            sum = cur1 + cur2 + next;

            if (sum > 1)
                next = 1;
            else
                next = 0;

            if (sum % 2 == 1)
                result[i] = true;
        }
        return result;
    }
    /// <summary>
    /// Циклический сдвиг битов влево
    /// </summary>
    /// <param name="source">Исходный массив бит</param>
    /// <param name="shift">Длина битового сдвига (отрицатеьное значение - сдвиг вправо, 0 - отстутствие сдвига)/param>
    private static bool[] CycleShift(bool[] source, int shift)
    {
        var res = new bool[source.Length];
        if(Math.Abs(shift) > source.Length)
            shift %= source.Length;
        if(shift == 0)
            source.CopyTo(res, 0);                 
        else if(shift > 0)
        {
            for(int i = 0; i < shift; ++i) 
                res[source.Length - shift + i] = source[i];
            for(int i = 0; i < source.Length - shift; ++i)
                res[i] = source[shift + i];
        }
        else
        {
            shift = Math.Abs(shift);
            for(int i = 0; i < shift; ++i)
                res[i] = source[source.Length - shift + i];
            for(int i = 0; i < source.Length - shift; ++i)
                res[shift + i] = source[i];
        }
        return res;                              
    }
}
