module PotterKataTooMuch

open System.Collections.Generic

//let rec priceTypes priceBucket (types: int list) =
//    let nbTypes = List.length types
//    if nbTypes = 0 then 0.0M
//    else
//        let allPrices = seq {
//            for bucketSize = 1 to nbTypes do
//                let bucketPrice = priceBucket bucketSize
//                let remainingTypes =
//                    types
//                    |> Seq.mapi (fun i n -> if i < bucketSize then n - 1 else n)
//                    |> Seq.filter (fun n -> n > 0)
//                    |> Seq.sort |> Seq.toList |> List.rev
//
//                yield bucketPrice + priceTypes priceBucket remainingTypes }
//        allPrices |> Seq.min

let memoize f =
    let dict = Dictionary<_, _>()
    fun x ->
        match dict.TryGetValue(x) with
        | true, res -> res
        | false, _ -> 
            let res = f x
            dict.Add(x, res)
            res

#nowarn "40"
let rec priceTypes priceBucketFunc =
    let rec memoizedPriceTypes =
        let priceTypesRaw (types: int list) =
            let nbTypes = List.length types
            if nbTypes = 0 then 0.0M
            else
                let allPrices = seq {
                    for bucketSize = 1 to nbTypes do
                        let bucketPrice = priceBucketFunc bucketSize
                        let remainingTypes =
                            types
                            |> Seq.mapi (fun i n -> if i < bucketSize then n - 1 else n)
                            |> Seq.filter (fun n -> n > 0)
                            |> Seq.sort |> Seq.toList |> List.rev

                        yield bucketPrice + memoizedPriceTypes remainingTypes }
                allPrices |> Seq.min
        memoize priceTypesRaw
    memoizedPriceTypes

let price priceBucketFunc books =
    let types = books |> Seq.countBy id |> Seq.map snd |> Seq.sort |> Seq.toList |> List.rev
    priceTypes priceBucketFunc types