List<int> list = new List<int>() { 20, 1, 4, 8, 9, 44 };
// Process each argument with code statements
var evenNumbers = list.FindAll((i) =>
{
    Console.WriteLine("Value of i is: {0}", i);
    return (i % 2) == 0;
});
