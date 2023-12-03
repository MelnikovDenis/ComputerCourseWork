﻿namespace Logic;

public class BitOperations
{
    public const int BitSize = 16;
    public static bool[] Zero { get; } = new bool[BitSize];
    public static bool[] One { get; } = new bool[BitSize];
    static BitOperations() 
    {
        One[BitSize - 1] = true;
    }
    public bool[] Op1 { get; private set; } = new bool[BitSize];
    public bool[] Op2 { get; private set; } = new bool[BitSize];
    public bool[] Result { get; private set; } = new bool[BitSize];
    public bool OV { get; private set; } = false;
    public bool Z { get; private set; } = false;
    
    public static bool[] GetAdditionalCode(bool[] source, out bool OV) 
    {
        OV = false;
        var result = new bool[source.Length];
        source.CopyTo(result, 0);
        if (!source[0])
            return result;
        else
        {
            for (int i = 1; i < result.Length; ++i)
                result[i] ^= true;
            result = Add(result, One, out OV);
        }
        return result;
    }
    public static bool IsGreater(bool[] op1, bool[] op2) 
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
    public static bool IsEqual(bool[] op1, bool[] op2) 
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
    public static bool[] Add(bool[] op1, bool[] op2, out bool OV)
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
        if (next == 1)
            OV = true;
        else
            OV = false;
        return result;
    }
}
