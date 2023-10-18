//random code bellow for learning purposes
open System;

//wait for keyboard input
let waitKeyboard() : uint =
    match Console.IsOutputRedirected with
        | false -> (uint)(Console.ReadKey().Key)
        | _ -> 0u

//print message
let message = "hello world"
printfn "%s" message

//inner let
let i, j, k =
    let iInner, jInner, kInner = (1, 2, 3)
    (iInner * 2, jInner * 2, kInner * 2)
printfn "%i_%i_%i" i j k

//fibonacci with tail recursion
let fibonacci n =
    let rec fib n a b =
        match n with
        | 0 -> a
        | _ -> fib (n - 1) b (a + b)
    fib n 0 1

let number = fibonacci 10
printfn "%i" number

// functions pipe, doing sum of filtered list
let numbers = [ 1; 2; 3; 4; 5 ]
let oddNumbersSum =
    numbers |>
    List.filter(fun x -> x % 2 <> 0) |>
    List.fold(fun acc  n -> acc + n) 0

// functions composition, doing sum of filtered list
let sumOfOddNumbers =
    let filterOddNumbers = List.filter(fun x -> x % 2 <> 0)
    let sumOddNumbers = List.fold(fun acc n -> acc + n) 0
    filterOddNumbers >> sumOddNumbers
let oddNumbersSumComposition = sumOfOddNumbers numbers
printfn "%i %i" oddNumbersSum oddNumbersSumComposition

// functions pipe, doing sum of filtered sequence
let numbersSequence = seq { for i in 1 .. 100 -> i }
let filteredNumbers threshold =
    numbersSequence |>
    Seq.filter(fun i -> if i > threshold then false else true) |>
    Seq.fold(fun acc n -> acc + n) 0
let sumFiltered = filteredNumbers 5
printfn "%i" sumFiltered

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
printfn "%d" sumSample

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
printfn "%d" result

// using pattern matching match Name field within record and within Union type
type UserRecord = { Name: string; LastName: string }
type UserRecordExtended = { Name: string; LastName: string; Address: string }

type User =
    | Standard of UserRecord
    | Extended of UserRecordExtended

let IsMatchByName user (name: string) =
    match user with
    | { UserRecord.Name = userName; } when userName = name -> true
    | _ -> false

let IsMatchUserByName user (name: string) =
    match user with
    | Standard { Name = userName; } when userName = name -> true
    | Extended { Name = userName; } when userName = name -> true
    | _ -> false

let userStandard = { Name = "Parker"; LastName = "Smith" }
let userExtended = { Name = "Parker"; LastName = "Smith"; Address = "" }
let user1 = Extended userExtended
let user2 = Standard userStandard

let isMatched1 = IsMatchByName userStandard "Parker"
let isMatched2 = IsMatchByName userStandard "Smith"
let isMatched3 = IsMatchUserByName user1 "Parker"
let isMatched4 = IsMatchUserByName user2 "Parker"
printfn "%b %b %b %b" isMatched1 isMatched2 isMatched3 isMatched4

// pattern against union type, when will be executed only if type matches
type SampleStringUnion =
    | CaseA of string
    | CaseB of string

let evaluate value =
    match value with
    | CaseA a when a.Length > 10 -> "A's string length is greater than 10"
    | CaseA _ -> "A's string length is 10 or less"
    | CaseB b when b.Length > 5 -> "B's string length is greater than 5"
    | CaseB _ -> "B's string length is 5 or less"

let evaluateOut1= evaluate (CaseA "abcdf")
let evaluateOut2= evaluate (CaseB "abcdf")

type SampleIntUnion =
    | A of int
    | B of int

//this is combined pattern so when will be executed for both of supplied types
let foo(value: SampleIntUnion) =
    let test = value
    match test with
    | A a
    | B a when a > 41 -> a // the guard applies to both patterns
    | _ -> 1
let evaluate4 = foo (A 9)
let evaluate5 = foo (B 3)

//active patterns

let (|Even|Odd|) number = if number % 2 = 0 then Even else Odd
let describeNumber n =
    match n with
    | Even -> "even"
    | Odd -> "odd"
let evenNumber = describeNumber 4
let oddNumber = describeNumber 3

let (|Default|) namePattern value =
    match value with
    | None -> namePattern
    | Some valueInternal -> valueInternal

let greet (Default "random citizen" name) =
    printfn "Hello, %s!" name

greet None
greet (Some "George")

// end
do waitKeyboard() |> ignore