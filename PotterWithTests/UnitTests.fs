namespace UnitTests

open Microsoft.VisualStudio.TestTools.UnitTesting

[<TestClass>]
type UnitTests() = 

    //Simple pricer
    //let price = PotterKata.price
    //Generic pricer
    let price = PotterKataTooMuch.price (function | 1 -> 8.0M | 2 -> 15.2M | 3 -> 21.6M | 4 -> 25.6M | 5 -> 30.0M | _ -> failwith "Really ? A 6th book ?")

    [<TestMethod>]
    member x.``1 book (each kind)`` () = 
        Assert.AreEqual(8.0M, price [1])
        Assert.AreEqual(8.0M, price [2])
        Assert.AreEqual(8.0M, price [3])
        Assert.AreEqual(8.0M, price [4])
        Assert.AreEqual(8.0M, price [5])

    [<TestMethod>]
    member x.``2 same books`` () = 
        Assert.AreEqual(16.0M, price [1;1])
        Assert.AreEqual(24.0M, price [2;2;2])

    [<TestMethod>]
    member x.``2 distinct books`` () = 
        Assert.AreEqual(15.2M, price [1;2])

    [<TestMethod>]
    member x.``3 distinct books`` () = 
        Assert.AreEqual(21.6M, price [1;2;3])

    [<TestMethod>]
    member x.``4 distinct books`` () = 
        Assert.AreEqual(25.6M, price [1;2;3;4])

    [<TestMethod>]
    member x.``5 distinct books`` () = 
        Assert.AreEqual(30.0M, price [1;2;3;4;5])

    [<TestMethod>]
    member x.``Simple discount`` () = 
        Assert.AreEqual(29.6M, price [1;2;1;3])

    [<TestMethod>]
    member x.``Two discounts`` () = 
        Assert.AreEqual(43.2M, price [1;2;1;2;3;3])

    [<TestMethod>]
    member x.``Edge case 1`` () = 
        Assert.AreEqual(51.2M, price [1;1;2;2;3;3;4;5])

    [<TestMethod>]
    member x.``Edge case 2`` () = 
        Assert.AreEqual(81.2M, price [1;1;2;2;3;3;4;5;1;2;3;4;5])

    [<TestMethod>]
    member x.``Edge case 3`` () = 
        Assert.AreEqual(102.4M, price [1;1;1;1;2;2;2;2;3;3;3;3;4;4;5;5])

    [<TestMethod>]
    member x.``Large case`` () = 
        Assert.AreEqual(300.0M, price [1;2;3;4;5;1;2;3;4;5;1;2;3;4;5;1;2;3;4;5;1;2;3;4;5;1;2;3;4;5;1;2;3;4;5;1;2;3;4;5;1;2;3;4;5;1;2;3;4;5])

    [<TestMethod>]
    member x.``Large case 2`` () = 
        Assert.AreEqual(358.8M, price [1;1;2;2;3;3;4;5;4;5;4;5;4;5;1;1;1;1;2;1;2;1;2;2;2;2;2;2;1;1;2;2;1;1;2;4;5;4;5;1;2;3;4;5;1;2;3;4;5;1;2;3;4;5;1])
        