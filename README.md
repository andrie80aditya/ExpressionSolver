You are to write a program that solves expressions. The expressions will look like the following.

( ( a gt b ) or ( b gt d )
( ( a gt b ) or ( b lte d ) )

All expressions evaluate to a boolean answer. The expressions can be of arbitrary complexity.

Write a console program that produces the following output.

Reserved Words:
gt -> greater than (>)
lt -> less than (<)
gte -> greater than or equal (>=)
lte -> less than or equal (<=)
eq -> equals (==)
neq -> not equals (!=)
or -> or (||)
and -> and (&&)

Notes:
> -> User Input

The program should detect if the expression is not balanced 
Example:
>( a gt b 
Should return: “Expression cannot be parsed”

The program should detect if the expression contains undefined variables. If list of variables is { "a" : 1, "b" : 3, "c" : 4, "d" : -1 } then
>( a lt z )
Should return: “Undefined variable z”

You can assume that the expression:
All elements in the input ( ‘(‘, ‘)’, reserved words like gt, lt etc.) are separated by exactly one space
The list of variables may include reserved words

DONOT use Javascript eval or equivalent.

___________________________

Program Execution:

Welcome to expression Solver!

Enter your set of values in JSON format:
> { "a" : 1, "b" : 3, "c" : 4, "d" : -1 }

Enter your expression:
> ( ( a gt b ) or ( b gt d ) )

Result: true

Do you want to evaluate another expression (y/n):
> y

Enter your expression:
> ( ( a gt b ) or ( b lte d ) )

Result: false

Do you want to evaluate another expression (y/n):
> n

Do you want to restart the program (y/n):
> n

Bye!
