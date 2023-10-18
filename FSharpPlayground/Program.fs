//random code bellow for learning purposes
open System;

//wait for keyboard input
let waitKeyboard() : uint = 
    match Console.IsOutputRedirected with 
        | false -> (uint)(Console.ReadKey().Key)
        | _ -> 0u

//print message
let message = "hello world"
printf "%s" message

//inner let
let i, j, k = 
    let iInner, jInner, kInner = (1, 2, 3)
    (iInner * 2, jInner * 2, kInner * 2)
printf "%i_%i_%i\n" i j k

//fibonacci with tail recursion
let fibonacci n =
    let rec fib n a b =
        match n with
        | 0 -> a
        | _ -> fib (n - 1) b (a + b)
    fib n 0 1

let number = fibonacci 10
printf "%i\n" number 

// functions pipe, doing sum of filtered list
let numbers = [ 1; 2; 3; 4; 5 ]
let oddNumbersSum = 
    numbers |>
    List.filter(fun x -> x % 2 <> 0) |>
    List.fold(fun acc  n -> acc + n) 0    
printf "%i\n" oddNumbersSum

// functions composition, doing sum of filtered list
let sumOfOddNumbers = 
    let filterOddNumbers = List.filter(fun x -> x % 2 <> 0)
    let sumOddNumbers = List.fold(fun acc n -> acc + n) 0 
    filterOddNumbers >> sumOddNumbers
let oddNumbersSumComposition = sumOfOddNumbers numbers
printf "%i\n" oddNumbersSumComposition

// functions pipe, doing sum of filtered sequence
let numbersSequence = seq { for i in 1 .. 100 -> i }
let filteredNumbers threshold = 
    numbersSequence |>
    Seq.filter(fun i -> if i > threshold then false else true) |>
    Seq.fold(fun acc n -> acc + n) 0
let sumFiltered = filteredNumbers 5
printf "%i\n" sumFiltered

// pattern, recursively decompose list
let list1 = [ 1; 2; 3; 4 ]
let rec printList l =
    match l with
        | head :: tail -> printf "%d " head; printList tail
        | [] -> printfn ""
printList list1

// pattern, recursively decompose list and sum it up
let rec sumList l = 
    match l with
        | head :: tail -> head + sumList tail
        | [] -> 0
let sumSample = sumList list1
printf "%d" sumSample

// pattern, decompose list and count number of element that matching certain value
let countValues list value =
    let rec checkList list acc =
       match list with
       | head :: tail when head = value -> checkList tail (acc + 1)
       | _ :: tail -> checkList tail acc
       | [] -> acc
    checkList list 0
let sampleList = [ for x in -10..10 -> x*x - 4 ]
let result = countValues sampleList 0
printfn "\n%d" result

// end
do waitKeyboard() |> ignore