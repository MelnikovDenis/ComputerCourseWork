using Logic;
var strOp1 = "0000000000000001";
var strOp2 = "0111111111111111";

var boolOp1 = Converter.StringToBoolArray(strOp1);
var boolOp2 = Converter.StringToBoolArray(strOp2);
bool OV, Z;
/*
var promres = BitOperations.Add(BitOperations.Zero, BitOperations.GetAdditionalCode(boolOp1, ref OV), ref OV);
boolOp1[0] = false;
Console.WriteLine($"{Converter.BoolArrayToString(BitOperations.Add(promres, boolOp1, ref OV))}");
*/
var result = BitOperations.Divide(boolOp1, boolOp2, out OV, out Z);

Console.WriteLine(Converter.BoolArrayToString(result));