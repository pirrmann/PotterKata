module PotterKata

let greedyGroup books = 
    // we count how many books there are of each type and sort them
    let types = books |> Seq.countBy id |> Seq.map snd |> Seq.sort |> Seq.toList
    // then we recursively compute how many groups of books we can build
    let rec countGroups types used =
        match types with
        | [] -> []
        | head :: tail -> head - used :: countGroups tail head
    countGroups types 0 |> List.rev

let adjust = function 
    | [a; b; c; d; e] ->
        let shift = min c e
        [a; b; c-shift; d+2*shift; e-shift]
    | l -> l

let priceGroup = function | 1 -> 8.0M | 2 -> 15.2M | 3 -> 21.6M
                          | 4 -> 25.6M | 5 -> 30.0M | _ -> failwith "Really ? A 6th book ?"

let price books =
    books
    |> greedyGroup
    |> adjust
    |> Seq.mapi (fun i nb -> priceGroup (i+1) * decimal nb)
    |> Seq.sum
    
//let test1 = price [1]
//let test2 = price [1;2;5]
//let test3 = price [1;2;3;4;5]
//
//let test_edge = price [1;1;2;2;3;3;4;5]
//let test_large = price [1;2;3;4;5;1;2;3;4;5;1;2;3;4;5;1;2;3;4;5;1;2;3;4;5;1;2;3;4;5;1;2;3;4;5;1;2;3;4;5;1;2;3;4;5;1;2;3;4;5]
//
//let test_large_random = price [1;1;2;2;3;3;4;5;4;5;4;5;4;5;1;1;1;1;2;1;2;1;2;2;2;2;2;2;1;1;2;2;1;1;2;4;5;4;5;1;2;3;4;5;1;2;3;4;5;1;2;3;4;5;1]
//
//let test_generic_1 = price ["Harry Potter and the Philosopher's Stone";
//                            "Harry Potter and the Chamber of Secrets";
//                            "Harry Potter and the Prisoner of Azkaban";
//                            "Harry goes to Hollywood";
//                            "Harry Rabienkirira - ledernier"]
//
//let test_generic_2 = price [typedefof<System.Collections.ArrayList>;
//                            typedefof<System.String>;
//                            typedefof<System.Int32>]
