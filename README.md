## Get It
```
	Install-Package Hallmanac.Funqy.CSharp
```

## Description
A small library to allow writing C# in a more fluent functional style. This library took some inspiration from how Promises work in JavaScript by wrapping code in a transport object and returning that object wrapper from each method in order to facilitate fluent API method chaining.

There are 5 primary concepts for this library:

- `FunqResult` and `FunqResult<T>`
- `FunqFactory` static factory class
- `Then(...)` extension method and its overloads
- `Catch(...)` extension method and its overloads
- `Finally(...)` extension method and its overloads

## `FunqResult` and `FunqResult<T>`
The `FunqResult` class is an immutable class which acts as a results wrapper that gets returned from the extension methods. It contains the status, a message, and (if applicable) a value. This wrapper allows for consistency among the returned values of the extension methods. All values are set in the constructors (not shown for brevity).

```
public class FunqResult
{
    public bool IsSuccessful { get; }
    public string Message { get; }
    public bool IsFailure => !IsSuccessful;
}

public class FunqResult<T> : FunqResult
{
    public bool HasValue => Value != null;
    public T Value { get; }
}

```

## The `FunqFactory` For Groovin'
The `FunqFactory` class is where you will do most of the interaction with this library. This static class holds the extension methods for beginning a fluent method chain, continuing that method chain, optionally handling errors within that method chain, and optionally finalizing that method chain with any cleanup code or logic. Those operations are discussed in the sections following this one.

The `FunqFactory` class is also responsible for being a shortcut to create new instances of the `FunqResult` class. The two methods are as follows:

```
    FunqFactory.Ok<T>(someValue, "Some Success Message");
    FunqFactory.Fail<T>("Some Error Message", optionalValue);
```

There are also overloads of those two factory methods for dealing with scenarios where there is no `FunqResult.Value` being passed around.

## Do Something `Then` Do Something Else
This method allows for chaining successful operations together by passing in callback `Func`s. 

```
var someResult = "First Line".GetFunqy()
                             .Then(val =>
                             {
                                 var sb = new StringBuilder();
                                 sb.AppendLine(val);
                                 sb.AppendLine("Second Line");
                                 var strValue = sb.ToString();
                                 return FunqFactory.Ok<string>(strValue, "The second line was added successfully");
                             })
                             .Then(val =>
                             {
                                 var sb = new StringBuilder();
                                 sb.Append(val);
                                 sb.AppendLine("Third Line");
                                 var strValue = sb.ToString();
                                 return FunqFactory.Ok<string>(strValue, "The third line was added successfully");
                             });
```

When you run that code and execute `someResult.Value`, it should result in:

```
First Line
Second Line
Third Line
```

That code is from one of the few tests for this library. Feel free to check it out in there.

## `Catch` an error
The `Catch` extension method is very similar to the `Then` method except that it handles any failures that get through. This can be for handling the error and continuing with the method chain or it can be the last method in the chain and handle some logging before returning the error result.

```
var someResult = "First Line".GetFunqy()
                             .Then(val =>
                             {
                                 var sb = new StringBuilder();
                                 sb.AppendLine(val);
                                 sb.AppendLine("Second Line");
                                 var strValue = sb.ToString();
                                 return FunqFactory.Ok<string>(strValue, "The second line was added successfully");
                             })
                             .Then(val =>
                             {
                                 var sb = new StringBuilder();
                                 return FunqFactory.Fail<string>("I forgot what number comes after two!!", val);
                             })
                             .Catch(resultSoFar =>
                             {
                                 if (resultSoFar.IsSuccessful)
                                 {
                                     return FunqFactory.Ok<string>(resultSoFar.Value, "Everything Was a success");
                                 }
                                 // Write out the current error message to a log
                                 someLogger = $"I just logged a message. Here is the error:\n{resultSoFar.Message}";
                                 return FunqFactory.Fail<string>("You suck!", resultSoFar.Value);
                             });
```

The `someResult` variable will have a value that only goes up to "Second Line" with an error message that says "I forgot what number comes after two!!". 

## Feedback
This is the first iteration of the "docs" (a.k.a. ReadMe). If the above explanation is not intuitive feel free to file an issue. Of course, feel free to file an issue with any feedback you have for the library itself.

