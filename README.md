# Name Sorter

A program that takes an input file of names and sorts them according to surname and given names.

## Usage

### Prerequisite
- .NET Core v8 runtime
- A text file with one name per line, no delimiters, format: <Surname> <Given Name> [<Given Name>...]
e.g
```
Richards Norman
Smith Pat
Asquith Jane Rebecca
```
### Execution
 name-sorter <File path>

### Output
If file exists and is processed successfully
- Console output of the sorted list of names
- An output file in the same folder as the program named "sorted-names-list.txt"
