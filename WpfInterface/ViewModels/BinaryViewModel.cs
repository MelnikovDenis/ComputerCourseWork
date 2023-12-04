using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Logic;

namespace WpfInterface.ViewModels;

public class BinaryViewModel : IDataErrorInfo, INotifyPropertyChanged
{
    public const int BitSize = 16;
    private Dictionary<string, string> Errors { get; } = new Dictionary<string, string>();
    public string this[string propertyName] => Errors.ContainsKey(propertyName) ? Errors[propertyName] : string.Empty;
    public bool IsValid => !Errors.Values.Any(x => x != string.Empty);
    public string Error
    {
        get => string.Empty;
    }
    private string a = "0000000000000000";
    public string A 
    {
        get 
        {
            return a;
        }
        set
        {
            a = value;           
            if(IsCorrect(value))
            {
                Errors[nameof(A)] = string.Empty;
                BoolA = Converter.StringToBoolArray(value);
            }
            else
            {
                Errors[nameof(A)] = $"{nameof(A)} должен быть шестнадцатиразрядным двоичным числом.";
                BoolA = BitOperations.Zero;
            }
            OnPropertyChanged(nameof(A));
            OnPropertyChanged(nameof(IsValid));
        }
    }
    private string b = "0000000000000000";
    public string B 
    {
        get 
        {
            return b;
        }
        set
        {
            b = value;            
            if(IsCorrect(value))
            {
                Errors[nameof(B)] = string.Empty;   
                BoolB = Converter.StringToBoolArray(value);
            }
            else
            {
                Errors[nameof(B)] = $"{nameof(B)} должен быть шестнадцатиразрядным двоичным числом.";
                BoolB = BitOperations.Zero;
            }
            OnPropertyChanged(nameof(B));
            OnPropertyChanged(nameof(IsValid));
        }
    }
    private bool[] boolA = BitOperations.Zero;
    public bool[] BoolA 
    { 
        get => boolA; 
        private set 
        {
            boolA = value;
            OnPropertyChanged(nameof(BoolA));
        } 
    }
    private bool[] boolB = BitOperations.Zero;
    public bool[] BoolB
    { 
        get => boolB; 
        private set 
        {
            boolB = value;
            OnPropertyChanged(nameof(BoolB));
        } 
    }
    private bool[] boolC = BitOperations.Zero;
    public bool[] BoolC 
    { 
        get => boolC;
        private set
        {
            boolC = value;
            OnPropertyChanged(nameof(BoolC));
        } 
    }     
    private bool ov = false;
    public bool OV
    { 
        get => ov;
        private set
        {
            ov = value;
            OnPropertyChanged(nameof(OV));
        } 
    } 
    private bool z = false;
    public bool Z
    { 
        get => z;
        private set
        {
            z = value;
            OnPropertyChanged(nameof(Z));
        } 
    } 
    public void Divide()
    {        
        if(IsValid)
        {
            bool _ov, _z;
            BoolC = BitOperations.Divide(BoolA, BoolB, out _ov, out _z);
            OV = _ov;
            Z = _z;
        }           
    }
    public void Equivalence()
    {        
        if(IsValid)
        {
            bool _ov, _z;
            BoolC = BitOperations.Equivalence(BoolA, BoolB, out _ov, out _z);
            OV = _ov;
            Z = _z;
        }            
    }
    public event PropertyChangedEventHandler? PropertyChanged;
    public void OnPropertyChanged([CallerMemberName] string prop = "") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    private bool IsCorrect(string source)
    {
        if(source.Length != BitSize)
            return false;
        else
            foreach(var symb in source)
                if(symb != '0' && symb != '1')
                    return false;
        return true;
    }
}
