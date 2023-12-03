using Logic;
var strOp1 = "0000000100000111";
var strOp2 = "0000000000010011";

var boolOp1 = Converter.StringToBoolArray(strOp1);
var boolOp2 = Converter.StringToBoolArray(strOp2);

bool OV;
var result = BitOperations.GetAdditionalCode(boolOp1, out OV);

Console.WriteLine(Converter.BoolArrayToString(result));
Console.WriteLine(BitOperations.IsGreater(boolOp1, boolOp2));