# Problem description

P(T)Vzzle – 2022-01-01
You have a lot of Christmas cakes – oh wtf I’ve just discovered now the English version of the word “Panettone” … a cake? Mah – and you are allowed to perform only the following operations:
-	Add 1 xMas cake
-	Remove 1 xMas cake
-	Divide the entire group by 2 (ONLY IF you have an even number of xMAS cakes)
Find a function that given the total beginning amount of xMAS cakes as a string containing up to 309 digits, will provide the minimum number of operations you need to reduce your starting group to 1 (example: you are given 8 xMas cakes, so halving 3 times you get 1)

# Install .NET 6

## Windows
Run dotnet-install.ps1

## Linux/Mac
Run dotnet-install.sh

# Execute
cd PtVzzlexMasCake

dotnet run -- \<value\>

the value must be a positive integer (arbitrarily large)

## Output
A text file named "output.txt" with the following format:

The first line is the input value

From the second line there is the sequence of operations needed to reach the result "one",
one operation per line.

the operation are:

SubtractOne: current value - 1

AddOne: current value + 1

Halve: current value / 2

the operations must be performed in sequence.

Solution examples (files with the "#.expected.txt" pattern) are provided in the PtVzzlexMasCake.Tests\Resources\ directory.

## Tests
cd PtVzzlexMasCake.Tests
dotnet test