// /!\ THIS FILE IS GENERATED, DO NOT MODIFY BY HAND /!\

#pragma warning disable CS2436

using System.Linq.Expressions;
using System.Reflection;
using Moq;

namespace Cusco.ReactiveX.Test;

public static class CallSequence
{
  public static CallSequence<T> ForMock<T>(Mock<T> mock) where T : class => new(mock);
}

public class CallSequence<T> where T : class
{
  private int invocationsCounter;
  private readonly Mock<T> mock;

  internal CallSequence(Mock<T> mock)
  {
    this.mock = mock;
  }

  public CallSequence<T> VerifyInvocation(Expression<Func<T, Action>> invocation)
  {
    var methodImpl = GetMethodInfo(invocation);
    var mockInvocation = GetNextInvocation();

    // Ensuring called method matches the expected one
    Assert.That(mockInvocation.Method, Is.EqualTo(methodImpl));

    // Ensuring the number of arguments is equal
    Assert.That(mockInvocation.Arguments.Count, Is.EqualTo(0));

    return this;
  }

  public CallSequence<T> VerifyInvocation<TResult>(Expression<Func<T, Func<TResult>>> invocation, TResult expectedResult)
  {
    var methodImpl = GetMethodInfo(invocation);
    var mockInvocation = GetNextInvocation();

    // Ensuring called method matches the expected one
    Assert.That(mockInvocation.Method, Is.EqualTo(methodImpl));

    // Ensuring the number of arguments is equal
    Assert.That(mockInvocation.Arguments.Count, Is.EqualTo(0));

    // Checking the return value
    Assert.That(mockInvocation.ReturnValue, Is.EqualTo(expectedResult));

    return this;
  }

  public CallSequence<T> VerifyInvocation<T1>(
    Expression<Func<T, Action<T1>>> invocation,
    T1 expectedArg1
  )
  {
    var methodImpl = GetMethodInfo(invocation);
    var mockInvocation = GetNextInvocation();

    // Ensuring called method matches the expected one
    Assert.That(mockInvocation.Method, Is.EqualTo(methodImpl));

    // Ensuring the number of arguments is equal
    Assert.That(mockInvocation.Arguments.Count, Is.EqualTo(1));

    // Checking arguments one by one
    Assert.That(mockInvocation.Arguments[0], Is.EqualTo(expectedArg1));


    return this;
  }

  public CallSequence<T> VerifyInvocation<T1, TResult>(
    Expression<Func<T, Func<T1, TResult>>> invocation,
    T1 expectedArg1,
    TResult expectedResult
  )
  {
    var methodImpl = GetMethodInfo(invocation);
    var mockInvocation = GetNextInvocation();

    // Ensuring called method matches the expected one
    Assert.That(mockInvocation.Method, Is.EqualTo(methodImpl));

    // Ensuring the number of arguments is equal
    Assert.That(mockInvocation.Arguments.Count, Is.EqualTo(1));

    // Checking arguments one by one
    Assert.That(mockInvocation.Arguments[0], Is.EqualTo(expectedArg1));


    // Checking the return value
    Assert.That(mockInvocation.ReturnValue, Is.EqualTo(expectedResult));

    return this;
  }

  public CallSequence<T> VerifyInvocation<T1, T2>(
    Expression<Func<T, Action<T1, T2>>> invocation,
    T1 expectedArg1, T2 expectedArg2
  )
  {
    var methodImpl = GetMethodInfo(invocation);
    var mockInvocation = GetNextInvocation();

    // Ensuring called method matches the expected one
    Assert.That(mockInvocation.Method, Is.EqualTo(methodImpl));

    // Ensuring the number of arguments is equal
    Assert.That(mockInvocation.Arguments.Count, Is.EqualTo(2));

    // Checking arguments one by one
    Assert.That(mockInvocation.Arguments[0], Is.EqualTo(expectedArg1));
    Assert.That(mockInvocation.Arguments[1], Is.EqualTo(expectedArg2));


    return this;
  }

  public CallSequence<T> VerifyInvocation<T1, T2, TResult>(
    Expression<Func<T, Func<T1, T2, TResult>>> invocation,
    T1 expectedArg1, T2 expectedArg2,
    TResult expectedResult
  )
  {
    var methodImpl = GetMethodInfo(invocation);
    var mockInvocation = GetNextInvocation();

    // Ensuring called method matches the expected one
    Assert.That(mockInvocation.Method, Is.EqualTo(methodImpl));

    // Ensuring the number of arguments is equal
    Assert.That(mockInvocation.Arguments.Count, Is.EqualTo(2));

    // Checking arguments one by one
    Assert.That(mockInvocation.Arguments[0], Is.EqualTo(expectedArg1));
    Assert.That(mockInvocation.Arguments[1], Is.EqualTo(expectedArg2));


    // Checking the return value
    Assert.That(mockInvocation.ReturnValue, Is.EqualTo(expectedResult));

    return this;
  }

  public CallSequence<T> VerifyInvocation<T1, T2, T3>(
    Expression<Func<T, Action<T1, T2, T3>>> invocation,
    T1 expectedArg1, T2 expectedArg2, T3 expectedArg3
  )
  {
    var methodImpl = GetMethodInfo(invocation);
    var mockInvocation = GetNextInvocation();

    // Ensuring called method matches the expected one
    Assert.That(mockInvocation.Method, Is.EqualTo(methodImpl));

    // Ensuring the number of arguments is equal
    Assert.That(mockInvocation.Arguments.Count, Is.EqualTo(3));

    // Checking arguments one by one
    Assert.That(mockInvocation.Arguments[0], Is.EqualTo(expectedArg1));
    Assert.That(mockInvocation.Arguments[1], Is.EqualTo(expectedArg2));
    Assert.That(mockInvocation.Arguments[2], Is.EqualTo(expectedArg3));


    return this;
  }

  public CallSequence<T> VerifyInvocation<T1, T2, T3, TResult>(
    Expression<Func<T, Func<T1, T2, T3, TResult>>> invocation,
    T1 expectedArg1, T2 expectedArg2, T3 expectedArg3,
    TResult expectedResult
  )
  {
    var methodImpl = GetMethodInfo(invocation);
    var mockInvocation = GetNextInvocation();

    // Ensuring called method matches the expected one
    Assert.That(mockInvocation.Method, Is.EqualTo(methodImpl));

    // Ensuring the number of arguments is equal
    Assert.That(mockInvocation.Arguments.Count, Is.EqualTo(3));

    // Checking arguments one by one
    Assert.That(mockInvocation.Arguments[0], Is.EqualTo(expectedArg1));
    Assert.That(mockInvocation.Arguments[1], Is.EqualTo(expectedArg2));
    Assert.That(mockInvocation.Arguments[2], Is.EqualTo(expectedArg3));


    // Checking the return value
    Assert.That(mockInvocation.ReturnValue, Is.EqualTo(expectedResult));

    return this;
  }

  public CallSequence<T> VerifyInvocation<T1, T2, T3, T4>(
    Expression<Func<T, Action<T1, T2, T3, T4>>> invocation,
    T1 expectedArg1, T2 expectedArg2, T3 expectedArg3, T4 expectedArg4
  )
  {
    var methodImpl = GetMethodInfo(invocation);
    var mockInvocation = GetNextInvocation();

    // Ensuring called method matches the expected one
    Assert.That(mockInvocation.Method, Is.EqualTo(methodImpl));

    // Ensuring the number of arguments is equal
    Assert.That(mockInvocation.Arguments.Count, Is.EqualTo(4));

    // Checking arguments one by one
    Assert.That(mockInvocation.Arguments[0], Is.EqualTo(expectedArg1));
    Assert.That(mockInvocation.Arguments[1], Is.EqualTo(expectedArg2));
    Assert.That(mockInvocation.Arguments[2], Is.EqualTo(expectedArg3));
    Assert.That(mockInvocation.Arguments[3], Is.EqualTo(expectedArg4));


    return this;
  }

  public CallSequence<T> VerifyInvocation<T1, T2, T3, T4, TResult>(
    Expression<Func<T, Func<T1, T2, T3, T4, TResult>>> invocation,
    T1 expectedArg1, T2 expectedArg2, T3 expectedArg3, T4 expectedArg4,
    TResult expectedResult
  )
  {
    var methodImpl = GetMethodInfo(invocation);
    var mockInvocation = GetNextInvocation();

    // Ensuring called method matches the expected one
    Assert.That(mockInvocation.Method, Is.EqualTo(methodImpl));

    // Ensuring the number of arguments is equal
    Assert.That(mockInvocation.Arguments.Count, Is.EqualTo(4));

    // Checking arguments one by one
    Assert.That(mockInvocation.Arguments[0], Is.EqualTo(expectedArg1));
    Assert.That(mockInvocation.Arguments[1], Is.EqualTo(expectedArg2));
    Assert.That(mockInvocation.Arguments[2], Is.EqualTo(expectedArg3));
    Assert.That(mockInvocation.Arguments[3], Is.EqualTo(expectedArg4));


    // Checking the return value
    Assert.That(mockInvocation.ReturnValue, Is.EqualTo(expectedResult));

    return this;
  }

  public CallSequence<T> VerifyInvocation<T1, T2, T3, T4, T5>(
    Expression<Func<T, Action<T1, T2, T3, T4, T5>>> invocation,
    T1 expectedArg1, T2 expectedArg2, T3 expectedArg3, T4 expectedArg4, T5 expectedArg5
  )
  {
    var methodImpl = GetMethodInfo(invocation);
    var mockInvocation = GetNextInvocation();

    // Ensuring called method matches the expected one
    Assert.That(mockInvocation.Method, Is.EqualTo(methodImpl));

    // Ensuring the number of arguments is equal
    Assert.That(mockInvocation.Arguments.Count, Is.EqualTo(5));

    // Checking arguments one by one
    Assert.That(mockInvocation.Arguments[0], Is.EqualTo(expectedArg1));
    Assert.That(mockInvocation.Arguments[1], Is.EqualTo(expectedArg2));
    Assert.That(mockInvocation.Arguments[2], Is.EqualTo(expectedArg3));
    Assert.That(mockInvocation.Arguments[3], Is.EqualTo(expectedArg4));
    Assert.That(mockInvocation.Arguments[4], Is.EqualTo(expectedArg5));


    return this;
  }

  public CallSequence<T> VerifyInvocation<T1, T2, T3, T4, T5, TResult>(
    Expression<Func<T, Func<T1, T2, T3, T4, T5, TResult>>> invocation,
    T1 expectedArg1, T2 expectedArg2, T3 expectedArg3, T4 expectedArg4, T5 expectedArg5,
    TResult expectedResult
  )
  {
    var methodImpl = GetMethodInfo(invocation);
    var mockInvocation = GetNextInvocation();

    // Ensuring called method matches the expected one
    Assert.That(mockInvocation.Method, Is.EqualTo(methodImpl));

    // Ensuring the number of arguments is equal
    Assert.That(mockInvocation.Arguments.Count, Is.EqualTo(5));

    // Checking arguments one by one
    Assert.That(mockInvocation.Arguments[0], Is.EqualTo(expectedArg1));
    Assert.That(mockInvocation.Arguments[1], Is.EqualTo(expectedArg2));
    Assert.That(mockInvocation.Arguments[2], Is.EqualTo(expectedArg3));
    Assert.That(mockInvocation.Arguments[3], Is.EqualTo(expectedArg4));
    Assert.That(mockInvocation.Arguments[4], Is.EqualTo(expectedArg5));


    // Checking the return value
    Assert.That(mockInvocation.ReturnValue, Is.EqualTo(expectedResult));

    return this;
  }

  public CallSequence<T> VerifyInvocation<T1, T2, T3, T4, T5, T6>(
    Expression<Func<T, Action<T1, T2, T3, T4, T5, T6>>> invocation,
    T1 expectedArg1, T2 expectedArg2, T3 expectedArg3, T4 expectedArg4, T5 expectedArg5, T6 expectedArg6
  )
  {
    var methodImpl = GetMethodInfo(invocation);
    var mockInvocation = GetNextInvocation();

    // Ensuring called method matches the expected one
    Assert.That(mockInvocation.Method, Is.EqualTo(methodImpl));

    // Ensuring the number of arguments is equal
    Assert.That(mockInvocation.Arguments.Count, Is.EqualTo(6));

    // Checking arguments one by one
    Assert.That(mockInvocation.Arguments[0], Is.EqualTo(expectedArg1));
    Assert.That(mockInvocation.Arguments[1], Is.EqualTo(expectedArg2));
    Assert.That(mockInvocation.Arguments[2], Is.EqualTo(expectedArg3));
    Assert.That(mockInvocation.Arguments[3], Is.EqualTo(expectedArg4));
    Assert.That(mockInvocation.Arguments[4], Is.EqualTo(expectedArg5));
    Assert.That(mockInvocation.Arguments[5], Is.EqualTo(expectedArg6));


    return this;
  }

  public CallSequence<T> VerifyInvocation<T1, T2, T3, T4, T5, T6, TResult>(
    Expression<Func<T, Func<T1, T2, T3, T4, T5, T6, TResult>>> invocation,
    T1 expectedArg1, T2 expectedArg2, T3 expectedArg3, T4 expectedArg4, T5 expectedArg5, T6 expectedArg6,
    TResult expectedResult
  )
  {
    var methodImpl = GetMethodInfo(invocation);
    var mockInvocation = GetNextInvocation();

    // Ensuring called method matches the expected one
    Assert.That(mockInvocation.Method, Is.EqualTo(methodImpl));

    // Ensuring the number of arguments is equal
    Assert.That(mockInvocation.Arguments.Count, Is.EqualTo(6));

    // Checking arguments one by one
    Assert.That(mockInvocation.Arguments[0], Is.EqualTo(expectedArg1));
    Assert.That(mockInvocation.Arguments[1], Is.EqualTo(expectedArg2));
    Assert.That(mockInvocation.Arguments[2], Is.EqualTo(expectedArg3));
    Assert.That(mockInvocation.Arguments[3], Is.EqualTo(expectedArg4));
    Assert.That(mockInvocation.Arguments[4], Is.EqualTo(expectedArg5));
    Assert.That(mockInvocation.Arguments[5], Is.EqualTo(expectedArg6));


    // Checking the return value
    Assert.That(mockInvocation.ReturnValue, Is.EqualTo(expectedResult));

    return this;
  }

  public CallSequence<T> VerifyInvocation<T1, T2, T3, T4, T5, T6, T7>(
    Expression<Func<T, Action<T1, T2, T3, T4, T5, T6, T7>>> invocation,
    T1 expectedArg1, T2 expectedArg2, T3 expectedArg3, T4 expectedArg4, T5 expectedArg5, T6 expectedArg6, T7 expectedArg7
  )
  {
    var methodImpl = GetMethodInfo(invocation);
    var mockInvocation = GetNextInvocation();

    // Ensuring called method matches the expected one
    Assert.That(mockInvocation.Method, Is.EqualTo(methodImpl));

    // Ensuring the number of arguments is equal
    Assert.That(mockInvocation.Arguments.Count, Is.EqualTo(7));

    // Checking arguments one by one
    Assert.That(mockInvocation.Arguments[0], Is.EqualTo(expectedArg1));
    Assert.That(mockInvocation.Arguments[1], Is.EqualTo(expectedArg2));
    Assert.That(mockInvocation.Arguments[2], Is.EqualTo(expectedArg3));
    Assert.That(mockInvocation.Arguments[3], Is.EqualTo(expectedArg4));
    Assert.That(mockInvocation.Arguments[4], Is.EqualTo(expectedArg5));
    Assert.That(mockInvocation.Arguments[5], Is.EqualTo(expectedArg6));
    Assert.That(mockInvocation.Arguments[6], Is.EqualTo(expectedArg7));


    return this;
  }

  public CallSequence<T> VerifyInvocation<T1, T2, T3, T4, T5, T6, T7, TResult>(
    Expression<Func<T, Func<T1, T2, T3, T4, T5, T6, T7, TResult>>> invocation,
    T1 expectedArg1, T2 expectedArg2, T3 expectedArg3, T4 expectedArg4, T5 expectedArg5, T6 expectedArg6, T7 expectedArg7,
    TResult expectedResult
  )
  {
    var methodImpl = GetMethodInfo(invocation);
    var mockInvocation = GetNextInvocation();

    // Ensuring called method matches the expected one
    Assert.That(mockInvocation.Method, Is.EqualTo(methodImpl));

    // Ensuring the number of arguments is equal
    Assert.That(mockInvocation.Arguments.Count, Is.EqualTo(7));

    // Checking arguments one by one
    Assert.That(mockInvocation.Arguments[0], Is.EqualTo(expectedArg1));
    Assert.That(mockInvocation.Arguments[1], Is.EqualTo(expectedArg2));
    Assert.That(mockInvocation.Arguments[2], Is.EqualTo(expectedArg3));
    Assert.That(mockInvocation.Arguments[3], Is.EqualTo(expectedArg4));
    Assert.That(mockInvocation.Arguments[4], Is.EqualTo(expectedArg5));
    Assert.That(mockInvocation.Arguments[5], Is.EqualTo(expectedArg6));
    Assert.That(mockInvocation.Arguments[6], Is.EqualTo(expectedArg7));


    // Checking the return value
    Assert.That(mockInvocation.ReturnValue, Is.EqualTo(expectedResult));

    return this;
  }

  public CallSequence<T> VerifyInvocation<T1, T2, T3, T4, T5, T6, T7, T8>(
    Expression<Func<T, Action<T1, T2, T3, T4, T5, T6, T7, T8>>> invocation,
    T1 expectedArg1, T2 expectedArg2, T3 expectedArg3, T4 expectedArg4, T5 expectedArg5, T6 expectedArg6, T7 expectedArg7, T8 expectedArg8
  )
  {
    var methodImpl = GetMethodInfo(invocation);
    var mockInvocation = GetNextInvocation();

    // Ensuring called method matches the expected one
    Assert.That(mockInvocation.Method, Is.EqualTo(methodImpl));

    // Ensuring the number of arguments is equal
    Assert.That(mockInvocation.Arguments.Count, Is.EqualTo(8));

    // Checking arguments one by one
    Assert.That(mockInvocation.Arguments[0], Is.EqualTo(expectedArg1));
    Assert.That(mockInvocation.Arguments[1], Is.EqualTo(expectedArg2));
    Assert.That(mockInvocation.Arguments[2], Is.EqualTo(expectedArg3));
    Assert.That(mockInvocation.Arguments[3], Is.EqualTo(expectedArg4));
    Assert.That(mockInvocation.Arguments[4], Is.EqualTo(expectedArg5));
    Assert.That(mockInvocation.Arguments[5], Is.EqualTo(expectedArg6));
    Assert.That(mockInvocation.Arguments[6], Is.EqualTo(expectedArg7));
    Assert.That(mockInvocation.Arguments[7], Is.EqualTo(expectedArg8));


    return this;
  }

  public CallSequence<T> VerifyInvocation<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(
    Expression<Func<T, Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult>>> invocation,
    T1 expectedArg1, T2 expectedArg2, T3 expectedArg3, T4 expectedArg4, T5 expectedArg5, T6 expectedArg6, T7 expectedArg7, T8 expectedArg8,
    TResult expectedResult
  )
  {
    var methodImpl = GetMethodInfo(invocation);
    var mockInvocation = GetNextInvocation();

    // Ensuring called method matches the expected one
    Assert.That(mockInvocation.Method, Is.EqualTo(methodImpl));

    // Ensuring the number of arguments is equal
    Assert.That(mockInvocation.Arguments.Count, Is.EqualTo(8));

    // Checking arguments one by one
    Assert.That(mockInvocation.Arguments[0], Is.EqualTo(expectedArg1));
    Assert.That(mockInvocation.Arguments[1], Is.EqualTo(expectedArg2));
    Assert.That(mockInvocation.Arguments[2], Is.EqualTo(expectedArg3));
    Assert.That(mockInvocation.Arguments[3], Is.EqualTo(expectedArg4));
    Assert.That(mockInvocation.Arguments[4], Is.EqualTo(expectedArg5));
    Assert.That(mockInvocation.Arguments[5], Is.EqualTo(expectedArg6));
    Assert.That(mockInvocation.Arguments[6], Is.EqualTo(expectedArg7));
    Assert.That(mockInvocation.Arguments[7], Is.EqualTo(expectedArg8));


    // Checking the return value
    Assert.That(mockInvocation.ReturnValue, Is.EqualTo(expectedResult));

    return this;
  }

  public CallSequence<T> VerifyInvocation<T1, T2, T3, T4, T5, T6, T7, T8, T9>(
    Expression<Func<T, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9>>> invocation,
    T1 expectedArg1, T2 expectedArg2, T3 expectedArg3, T4 expectedArg4, T5 expectedArg5, T6 expectedArg6, T7 expectedArg7, T8 expectedArg8, T9 expectedArg9
  )
  {
    var methodImpl = GetMethodInfo(invocation);
    var mockInvocation = GetNextInvocation();

    // Ensuring called method matches the expected one
    Assert.That(mockInvocation.Method, Is.EqualTo(methodImpl));

    // Ensuring the number of arguments is equal
    Assert.That(mockInvocation.Arguments.Count, Is.EqualTo(9));

    // Checking arguments one by one
    Assert.That(mockInvocation.Arguments[0], Is.EqualTo(expectedArg1));
    Assert.That(mockInvocation.Arguments[1], Is.EqualTo(expectedArg2));
    Assert.That(mockInvocation.Arguments[2], Is.EqualTo(expectedArg3));
    Assert.That(mockInvocation.Arguments[3], Is.EqualTo(expectedArg4));
    Assert.That(mockInvocation.Arguments[4], Is.EqualTo(expectedArg5));
    Assert.That(mockInvocation.Arguments[5], Is.EqualTo(expectedArg6));
    Assert.That(mockInvocation.Arguments[6], Is.EqualTo(expectedArg7));
    Assert.That(mockInvocation.Arguments[7], Is.EqualTo(expectedArg8));
    Assert.That(mockInvocation.Arguments[8], Is.EqualTo(expectedArg9));


    return this;
  }

  public CallSequence<T> VerifyInvocation<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>(
    Expression<Func<T, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>>> invocation,
    T1 expectedArg1, T2 expectedArg2, T3 expectedArg3, T4 expectedArg4, T5 expectedArg5, T6 expectedArg6, T7 expectedArg7, T8 expectedArg8, T9 expectedArg9,
    TResult expectedResult
  )
  {
    var methodImpl = GetMethodInfo(invocation);
    var mockInvocation = GetNextInvocation();

    // Ensuring called method matches the expected one
    Assert.That(mockInvocation.Method, Is.EqualTo(methodImpl));

    // Ensuring the number of arguments is equal
    Assert.That(mockInvocation.Arguments.Count, Is.EqualTo(9));

    // Checking arguments one by one
    Assert.That(mockInvocation.Arguments[0], Is.EqualTo(expectedArg1));
    Assert.That(mockInvocation.Arguments[1], Is.EqualTo(expectedArg2));
    Assert.That(mockInvocation.Arguments[2], Is.EqualTo(expectedArg3));
    Assert.That(mockInvocation.Arguments[3], Is.EqualTo(expectedArg4));
    Assert.That(mockInvocation.Arguments[4], Is.EqualTo(expectedArg5));
    Assert.That(mockInvocation.Arguments[5], Is.EqualTo(expectedArg6));
    Assert.That(mockInvocation.Arguments[6], Is.EqualTo(expectedArg7));
    Assert.That(mockInvocation.Arguments[7], Is.EqualTo(expectedArg8));
    Assert.That(mockInvocation.Arguments[8], Is.EqualTo(expectedArg9));


    // Checking the return value
    Assert.That(mockInvocation.ReturnValue, Is.EqualTo(expectedResult));

    return this;
  }

  public CallSequence<T> VerifyInvocation<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(
    Expression<Func<T, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>>> invocation,
    T1 expectedArg1, T2 expectedArg2, T3 expectedArg3, T4 expectedArg4, T5 expectedArg5, T6 expectedArg6, T7 expectedArg7, T8 expectedArg8, T9 expectedArg9, T10 expectedArg10
  )
  {
    var methodImpl = GetMethodInfo(invocation);
    var mockInvocation = GetNextInvocation();

    // Ensuring called method matches the expected one
    Assert.That(mockInvocation.Method, Is.EqualTo(methodImpl));

    // Ensuring the number of arguments is equal
    Assert.That(mockInvocation.Arguments.Count, Is.EqualTo(10));

    // Checking arguments one by one
    Assert.That(mockInvocation.Arguments[0], Is.EqualTo(expectedArg1));
    Assert.That(mockInvocation.Arguments[1], Is.EqualTo(expectedArg2));
    Assert.That(mockInvocation.Arguments[2], Is.EqualTo(expectedArg3));
    Assert.That(mockInvocation.Arguments[3], Is.EqualTo(expectedArg4));
    Assert.That(mockInvocation.Arguments[4], Is.EqualTo(expectedArg5));
    Assert.That(mockInvocation.Arguments[5], Is.EqualTo(expectedArg6));
    Assert.That(mockInvocation.Arguments[6], Is.EqualTo(expectedArg7));
    Assert.That(mockInvocation.Arguments[7], Is.EqualTo(expectedArg8));
    Assert.That(mockInvocation.Arguments[8], Is.EqualTo(expectedArg9));
    Assert.That(mockInvocation.Arguments[9], Is.EqualTo(expectedArg10));


    return this;
  }

  public CallSequence<T> VerifyInvocation<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>(
    Expression<Func<T, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>>> invocation,
    T1 expectedArg1, T2 expectedArg2, T3 expectedArg3, T4 expectedArg4, T5 expectedArg5, T6 expectedArg6, T7 expectedArg7, T8 expectedArg8, T9 expectedArg9, T10 expectedArg10,
    TResult expectedResult
  )
  {
    var methodImpl = GetMethodInfo(invocation);
    var mockInvocation = GetNextInvocation();

    // Ensuring called method matches the expected one
    Assert.That(mockInvocation.Method, Is.EqualTo(methodImpl));

    // Ensuring the number of arguments is equal
    Assert.That(mockInvocation.Arguments.Count, Is.EqualTo(10));

    // Checking arguments one by one
    Assert.That(mockInvocation.Arguments[0], Is.EqualTo(expectedArg1));
    Assert.That(mockInvocation.Arguments[1], Is.EqualTo(expectedArg2));
    Assert.That(mockInvocation.Arguments[2], Is.EqualTo(expectedArg3));
    Assert.That(mockInvocation.Arguments[3], Is.EqualTo(expectedArg4));
    Assert.That(mockInvocation.Arguments[4], Is.EqualTo(expectedArg5));
    Assert.That(mockInvocation.Arguments[5], Is.EqualTo(expectedArg6));
    Assert.That(mockInvocation.Arguments[6], Is.EqualTo(expectedArg7));
    Assert.That(mockInvocation.Arguments[7], Is.EqualTo(expectedArg8));
    Assert.That(mockInvocation.Arguments[8], Is.EqualTo(expectedArg9));
    Assert.That(mockInvocation.Arguments[9], Is.EqualTo(expectedArg10));


    // Checking the return value
    Assert.That(mockInvocation.ReturnValue, Is.EqualTo(expectedResult));

    return this;
  }

  public CallSequence<T> VerifyInvocation<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(
    Expression<Func<T, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>>> invocation,
    T1 expectedArg1, T2 expectedArg2, T3 expectedArg3, T4 expectedArg4, T5 expectedArg5, T6 expectedArg6, T7 expectedArg7, T8 expectedArg8, T9 expectedArg9, T10 expectedArg10,
    T11 expectedArg11
  )
  {
    var methodImpl = GetMethodInfo(invocation);
    var mockInvocation = GetNextInvocation();

    // Ensuring called method matches the expected one
    Assert.That(mockInvocation.Method, Is.EqualTo(methodImpl));

    // Ensuring the number of arguments is equal
    Assert.That(mockInvocation.Arguments.Count, Is.EqualTo(11));

    // Checking arguments one by one
    Assert.That(mockInvocation.Arguments[0], Is.EqualTo(expectedArg1));
    Assert.That(mockInvocation.Arguments[1], Is.EqualTo(expectedArg2));
    Assert.That(mockInvocation.Arguments[2], Is.EqualTo(expectedArg3));
    Assert.That(mockInvocation.Arguments[3], Is.EqualTo(expectedArg4));
    Assert.That(mockInvocation.Arguments[4], Is.EqualTo(expectedArg5));
    Assert.That(mockInvocation.Arguments[5], Is.EqualTo(expectedArg6));
    Assert.That(mockInvocation.Arguments[6], Is.EqualTo(expectedArg7));
    Assert.That(mockInvocation.Arguments[7], Is.EqualTo(expectedArg8));
    Assert.That(mockInvocation.Arguments[8], Is.EqualTo(expectedArg9));
    Assert.That(mockInvocation.Arguments[9], Is.EqualTo(expectedArg10));
    Assert.That(mockInvocation.Arguments[10], Is.EqualTo(expectedArg11));


    return this;
  }

  public CallSequence<T> VerifyInvocation<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>(
    Expression<Func<T, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>>> invocation,
    T1 expectedArg1, T2 expectedArg2, T3 expectedArg3, T4 expectedArg4, T5 expectedArg5, T6 expectedArg6, T7 expectedArg7, T8 expectedArg8, T9 expectedArg9, T10 expectedArg10,
    T11 expectedArg11,
    TResult expectedResult
  )
  {
    var methodImpl = GetMethodInfo(invocation);
    var mockInvocation = GetNextInvocation();

    // Ensuring called method matches the expected one
    Assert.That(mockInvocation.Method, Is.EqualTo(methodImpl));

    // Ensuring the number of arguments is equal
    Assert.That(mockInvocation.Arguments.Count, Is.EqualTo(11));

    // Checking arguments one by one
    Assert.That(mockInvocation.Arguments[0], Is.EqualTo(expectedArg1));
    Assert.That(mockInvocation.Arguments[1], Is.EqualTo(expectedArg2));
    Assert.That(mockInvocation.Arguments[2], Is.EqualTo(expectedArg3));
    Assert.That(mockInvocation.Arguments[3], Is.EqualTo(expectedArg4));
    Assert.That(mockInvocation.Arguments[4], Is.EqualTo(expectedArg5));
    Assert.That(mockInvocation.Arguments[5], Is.EqualTo(expectedArg6));
    Assert.That(mockInvocation.Arguments[6], Is.EqualTo(expectedArg7));
    Assert.That(mockInvocation.Arguments[7], Is.EqualTo(expectedArg8));
    Assert.That(mockInvocation.Arguments[8], Is.EqualTo(expectedArg9));
    Assert.That(mockInvocation.Arguments[9], Is.EqualTo(expectedArg10));
    Assert.That(mockInvocation.Arguments[10], Is.EqualTo(expectedArg11));


    // Checking the return value
    Assert.That(mockInvocation.ReturnValue, Is.EqualTo(expectedResult));

    return this;
  }

  public CallSequence<T> VerifyInvocation<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(
    Expression<Func<T, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>>> invocation,
    T1 expectedArg1, T2 expectedArg2, T3 expectedArg3, T4 expectedArg4, T5 expectedArg5, T6 expectedArg6, T7 expectedArg7, T8 expectedArg8, T9 expectedArg9, T10 expectedArg10,
    T11 expectedArg11, T12 expectedArg12
  )
  {
    var methodImpl = GetMethodInfo(invocation);
    var mockInvocation = GetNextInvocation();

    // Ensuring called method matches the expected one
    Assert.That(mockInvocation.Method, Is.EqualTo(methodImpl));

    // Ensuring the number of arguments is equal
    Assert.That(mockInvocation.Arguments.Count, Is.EqualTo(12));

    // Checking arguments one by one
    Assert.That(mockInvocation.Arguments[0], Is.EqualTo(expectedArg1));
    Assert.That(mockInvocation.Arguments[1], Is.EqualTo(expectedArg2));
    Assert.That(mockInvocation.Arguments[2], Is.EqualTo(expectedArg3));
    Assert.That(mockInvocation.Arguments[3], Is.EqualTo(expectedArg4));
    Assert.That(mockInvocation.Arguments[4], Is.EqualTo(expectedArg5));
    Assert.That(mockInvocation.Arguments[5], Is.EqualTo(expectedArg6));
    Assert.That(mockInvocation.Arguments[6], Is.EqualTo(expectedArg7));
    Assert.That(mockInvocation.Arguments[7], Is.EqualTo(expectedArg8));
    Assert.That(mockInvocation.Arguments[8], Is.EqualTo(expectedArg9));
    Assert.That(mockInvocation.Arguments[9], Is.EqualTo(expectedArg10));
    Assert.That(mockInvocation.Arguments[10], Is.EqualTo(expectedArg11));
    Assert.That(mockInvocation.Arguments[11], Is.EqualTo(expectedArg12));


    return this;
  }

  public CallSequence<T> VerifyInvocation<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>(
    Expression<Func<T, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>>> invocation,
    T1 expectedArg1, T2 expectedArg2, T3 expectedArg3, T4 expectedArg4, T5 expectedArg5, T6 expectedArg6, T7 expectedArg7, T8 expectedArg8, T9 expectedArg9, T10 expectedArg10,
    T11 expectedArg11, T12 expectedArg12,
    TResult expectedResult
  )
  {
    var methodImpl = GetMethodInfo(invocation);
    var mockInvocation = GetNextInvocation();

    // Ensuring called method matches the expected one
    Assert.That(mockInvocation.Method, Is.EqualTo(methodImpl));

    // Ensuring the number of arguments is equal
    Assert.That(mockInvocation.Arguments.Count, Is.EqualTo(12));

    // Checking arguments one by one
    Assert.That(mockInvocation.Arguments[0], Is.EqualTo(expectedArg1));
    Assert.That(mockInvocation.Arguments[1], Is.EqualTo(expectedArg2));
    Assert.That(mockInvocation.Arguments[2], Is.EqualTo(expectedArg3));
    Assert.That(mockInvocation.Arguments[3], Is.EqualTo(expectedArg4));
    Assert.That(mockInvocation.Arguments[4], Is.EqualTo(expectedArg5));
    Assert.That(mockInvocation.Arguments[5], Is.EqualTo(expectedArg6));
    Assert.That(mockInvocation.Arguments[6], Is.EqualTo(expectedArg7));
    Assert.That(mockInvocation.Arguments[7], Is.EqualTo(expectedArg8));
    Assert.That(mockInvocation.Arguments[8], Is.EqualTo(expectedArg9));
    Assert.That(mockInvocation.Arguments[9], Is.EqualTo(expectedArg10));
    Assert.That(mockInvocation.Arguments[10], Is.EqualTo(expectedArg11));
    Assert.That(mockInvocation.Arguments[11], Is.EqualTo(expectedArg12));


    // Checking the return value
    Assert.That(mockInvocation.ReturnValue, Is.EqualTo(expectedResult));

    return this;
  }

  public CallSequence<T> VerifyInvocation<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(
    Expression<Func<T, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>>> invocation,
    T1 expectedArg1, T2 expectedArg2, T3 expectedArg3, T4 expectedArg4, T5 expectedArg5, T6 expectedArg6, T7 expectedArg7, T8 expectedArg8, T9 expectedArg9, T10 expectedArg10,
    T11 expectedArg11, T12 expectedArg12, T13 expectedArg13
  )
  {
    var methodImpl = GetMethodInfo(invocation);
    var mockInvocation = GetNextInvocation();

    // Ensuring called method matches the expected one
    Assert.That(mockInvocation.Method, Is.EqualTo(methodImpl));

    // Ensuring the number of arguments is equal
    Assert.That(mockInvocation.Arguments.Count, Is.EqualTo(13));

    // Checking arguments one by one
    Assert.That(mockInvocation.Arguments[0], Is.EqualTo(expectedArg1));
    Assert.That(mockInvocation.Arguments[1], Is.EqualTo(expectedArg2));
    Assert.That(mockInvocation.Arguments[2], Is.EqualTo(expectedArg3));
    Assert.That(mockInvocation.Arguments[3], Is.EqualTo(expectedArg4));
    Assert.That(mockInvocation.Arguments[4], Is.EqualTo(expectedArg5));
    Assert.That(mockInvocation.Arguments[5], Is.EqualTo(expectedArg6));
    Assert.That(mockInvocation.Arguments[6], Is.EqualTo(expectedArg7));
    Assert.That(mockInvocation.Arguments[7], Is.EqualTo(expectedArg8));
    Assert.That(mockInvocation.Arguments[8], Is.EqualTo(expectedArg9));
    Assert.That(mockInvocation.Arguments[9], Is.EqualTo(expectedArg10));
    Assert.That(mockInvocation.Arguments[10], Is.EqualTo(expectedArg11));
    Assert.That(mockInvocation.Arguments[11], Is.EqualTo(expectedArg12));
    Assert.That(mockInvocation.Arguments[12], Is.EqualTo(expectedArg13));


    return this;
  }

  public CallSequence<T> VerifyInvocation<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult>(
    Expression<Func<T, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult>>> invocation,
    T1 expectedArg1, T2 expectedArg2, T3 expectedArg3, T4 expectedArg4, T5 expectedArg5, T6 expectedArg6, T7 expectedArg7, T8 expectedArg8, T9 expectedArg9, T10 expectedArg10,
    T11 expectedArg11, T12 expectedArg12, T13 expectedArg13,
    TResult expectedResult
  )
  {
    var methodImpl = GetMethodInfo(invocation);
    var mockInvocation = GetNextInvocation();

    // Ensuring called method matches the expected one
    Assert.That(mockInvocation.Method, Is.EqualTo(methodImpl));

    // Ensuring the number of arguments is equal
    Assert.That(mockInvocation.Arguments.Count, Is.EqualTo(13));

    // Checking arguments one by one
    Assert.That(mockInvocation.Arguments[0], Is.EqualTo(expectedArg1));
    Assert.That(mockInvocation.Arguments[1], Is.EqualTo(expectedArg2));
    Assert.That(mockInvocation.Arguments[2], Is.EqualTo(expectedArg3));
    Assert.That(mockInvocation.Arguments[3], Is.EqualTo(expectedArg4));
    Assert.That(mockInvocation.Arguments[4], Is.EqualTo(expectedArg5));
    Assert.That(mockInvocation.Arguments[5], Is.EqualTo(expectedArg6));
    Assert.That(mockInvocation.Arguments[6], Is.EqualTo(expectedArg7));
    Assert.That(mockInvocation.Arguments[7], Is.EqualTo(expectedArg8));
    Assert.That(mockInvocation.Arguments[8], Is.EqualTo(expectedArg9));
    Assert.That(mockInvocation.Arguments[9], Is.EqualTo(expectedArg10));
    Assert.That(mockInvocation.Arguments[10], Is.EqualTo(expectedArg11));
    Assert.That(mockInvocation.Arguments[11], Is.EqualTo(expectedArg12));
    Assert.That(mockInvocation.Arguments[12], Is.EqualTo(expectedArg13));


    // Checking the return value
    Assert.That(mockInvocation.ReturnValue, Is.EqualTo(expectedResult));

    return this;
  }

  public CallSequence<T> VerifyInvocation<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(
    Expression<Func<T, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>>> invocation,
    T1 expectedArg1, T2 expectedArg2, T3 expectedArg3, T4 expectedArg4, T5 expectedArg5, T6 expectedArg6, T7 expectedArg7, T8 expectedArg8, T9 expectedArg9, T10 expectedArg10,
    T11 expectedArg11, T12 expectedArg12, T13 expectedArg13, T14 expectedArg14
  )
  {
    var methodImpl = GetMethodInfo(invocation);
    var mockInvocation = GetNextInvocation();

    // Ensuring called method matches the expected one
    Assert.That(mockInvocation.Method, Is.EqualTo(methodImpl));

    // Ensuring the number of arguments is equal
    Assert.That(mockInvocation.Arguments.Count, Is.EqualTo(14));

    // Checking arguments one by one
    Assert.That(mockInvocation.Arguments[0], Is.EqualTo(expectedArg1));
    Assert.That(mockInvocation.Arguments[1], Is.EqualTo(expectedArg2));
    Assert.That(mockInvocation.Arguments[2], Is.EqualTo(expectedArg3));
    Assert.That(mockInvocation.Arguments[3], Is.EqualTo(expectedArg4));
    Assert.That(mockInvocation.Arguments[4], Is.EqualTo(expectedArg5));
    Assert.That(mockInvocation.Arguments[5], Is.EqualTo(expectedArg6));
    Assert.That(mockInvocation.Arguments[6], Is.EqualTo(expectedArg7));
    Assert.That(mockInvocation.Arguments[7], Is.EqualTo(expectedArg8));
    Assert.That(mockInvocation.Arguments[8], Is.EqualTo(expectedArg9));
    Assert.That(mockInvocation.Arguments[9], Is.EqualTo(expectedArg10));
    Assert.That(mockInvocation.Arguments[10], Is.EqualTo(expectedArg11));
    Assert.That(mockInvocation.Arguments[11], Is.EqualTo(expectedArg12));
    Assert.That(mockInvocation.Arguments[12], Is.EqualTo(expectedArg13));
    Assert.That(mockInvocation.Arguments[13], Is.EqualTo(expectedArg14));


    return this;
  }

  public CallSequence<T> VerifyInvocation<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult>(
    Expression<Func<T, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult>>> invocation,
    T1 expectedArg1, T2 expectedArg2, T3 expectedArg3, T4 expectedArg4, T5 expectedArg5, T6 expectedArg6, T7 expectedArg7, T8 expectedArg8, T9 expectedArg9, T10 expectedArg10,
    T11 expectedArg11, T12 expectedArg12, T13 expectedArg13, T14 expectedArg14,
    TResult expectedResult
  )
  {
    var methodImpl = GetMethodInfo(invocation);
    var mockInvocation = GetNextInvocation();

    // Ensuring called method matches the expected one
    Assert.That(mockInvocation.Method, Is.EqualTo(methodImpl));

    // Ensuring the number of arguments is equal
    Assert.That(mockInvocation.Arguments.Count, Is.EqualTo(14));

    // Checking arguments one by one
    Assert.That(mockInvocation.Arguments[0], Is.EqualTo(expectedArg1));
    Assert.That(mockInvocation.Arguments[1], Is.EqualTo(expectedArg2));
    Assert.That(mockInvocation.Arguments[2], Is.EqualTo(expectedArg3));
    Assert.That(mockInvocation.Arguments[3], Is.EqualTo(expectedArg4));
    Assert.That(mockInvocation.Arguments[4], Is.EqualTo(expectedArg5));
    Assert.That(mockInvocation.Arguments[5], Is.EqualTo(expectedArg6));
    Assert.That(mockInvocation.Arguments[6], Is.EqualTo(expectedArg7));
    Assert.That(mockInvocation.Arguments[7], Is.EqualTo(expectedArg8));
    Assert.That(mockInvocation.Arguments[8], Is.EqualTo(expectedArg9));
    Assert.That(mockInvocation.Arguments[9], Is.EqualTo(expectedArg10));
    Assert.That(mockInvocation.Arguments[10], Is.EqualTo(expectedArg11));
    Assert.That(mockInvocation.Arguments[11], Is.EqualTo(expectedArg12));
    Assert.That(mockInvocation.Arguments[12], Is.EqualTo(expectedArg13));
    Assert.That(mockInvocation.Arguments[13], Is.EqualTo(expectedArg14));


    // Checking the return value
    Assert.That(mockInvocation.ReturnValue, Is.EqualTo(expectedResult));

    return this;
  }

  public CallSequence<T> VerifyInvocation<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(
    Expression<Func<T, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>>> invocation,
    T1 expectedArg1, T2 expectedArg2, T3 expectedArg3, T4 expectedArg4, T5 expectedArg5, T6 expectedArg6, T7 expectedArg7, T8 expectedArg8, T9 expectedArg9, T10 expectedArg10,
    T11 expectedArg11, T12 expectedArg12, T13 expectedArg13, T14 expectedArg14, T15 expectedArg15
  )
  {
    var methodImpl = GetMethodInfo(invocation);
    var mockInvocation = GetNextInvocation();

    // Ensuring called method matches the expected one
    Assert.That(mockInvocation.Method, Is.EqualTo(methodImpl));

    // Ensuring the number of arguments is equal
    Assert.That(mockInvocation.Arguments.Count, Is.EqualTo(15));

    // Checking arguments one by one
    Assert.That(mockInvocation.Arguments[0], Is.EqualTo(expectedArg1));
    Assert.That(mockInvocation.Arguments[1], Is.EqualTo(expectedArg2));
    Assert.That(mockInvocation.Arguments[2], Is.EqualTo(expectedArg3));
    Assert.That(mockInvocation.Arguments[3], Is.EqualTo(expectedArg4));
    Assert.That(mockInvocation.Arguments[4], Is.EqualTo(expectedArg5));
    Assert.That(mockInvocation.Arguments[5], Is.EqualTo(expectedArg6));
    Assert.That(mockInvocation.Arguments[6], Is.EqualTo(expectedArg7));
    Assert.That(mockInvocation.Arguments[7], Is.EqualTo(expectedArg8));
    Assert.That(mockInvocation.Arguments[8], Is.EqualTo(expectedArg9));
    Assert.That(mockInvocation.Arguments[9], Is.EqualTo(expectedArg10));
    Assert.That(mockInvocation.Arguments[10], Is.EqualTo(expectedArg11));
    Assert.That(mockInvocation.Arguments[11], Is.EqualTo(expectedArg12));
    Assert.That(mockInvocation.Arguments[12], Is.EqualTo(expectedArg13));
    Assert.That(mockInvocation.Arguments[13], Is.EqualTo(expectedArg14));
    Assert.That(mockInvocation.Arguments[14], Is.EqualTo(expectedArg15));


    return this;
  }

  public CallSequence<T> VerifyInvocation<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult>(
    Expression<Func<T, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult>>> invocation,
    T1 expectedArg1, T2 expectedArg2, T3 expectedArg3, T4 expectedArg4, T5 expectedArg5, T6 expectedArg6, T7 expectedArg7, T8 expectedArg8, T9 expectedArg9, T10 expectedArg10,
    T11 expectedArg11, T12 expectedArg12, T13 expectedArg13, T14 expectedArg14, T15 expectedArg15,
    TResult expectedResult
  )
  {
    var methodImpl = GetMethodInfo(invocation);
    var mockInvocation = GetNextInvocation();

    // Ensuring called method matches the expected one
    Assert.That(mockInvocation.Method, Is.EqualTo(methodImpl));

    // Ensuring the number of arguments is equal
    Assert.That(mockInvocation.Arguments.Count, Is.EqualTo(15));

    // Checking arguments one by one
    Assert.That(mockInvocation.Arguments[0], Is.EqualTo(expectedArg1));
    Assert.That(mockInvocation.Arguments[1], Is.EqualTo(expectedArg2));
    Assert.That(mockInvocation.Arguments[2], Is.EqualTo(expectedArg3));
    Assert.That(mockInvocation.Arguments[3], Is.EqualTo(expectedArg4));
    Assert.That(mockInvocation.Arguments[4], Is.EqualTo(expectedArg5));
    Assert.That(mockInvocation.Arguments[5], Is.EqualTo(expectedArg6));
    Assert.That(mockInvocation.Arguments[6], Is.EqualTo(expectedArg7));
    Assert.That(mockInvocation.Arguments[7], Is.EqualTo(expectedArg8));
    Assert.That(mockInvocation.Arguments[8], Is.EqualTo(expectedArg9));
    Assert.That(mockInvocation.Arguments[9], Is.EqualTo(expectedArg10));
    Assert.That(mockInvocation.Arguments[10], Is.EqualTo(expectedArg11));
    Assert.That(mockInvocation.Arguments[11], Is.EqualTo(expectedArg12));
    Assert.That(mockInvocation.Arguments[12], Is.EqualTo(expectedArg13));
    Assert.That(mockInvocation.Arguments[13], Is.EqualTo(expectedArg14));
    Assert.That(mockInvocation.Arguments[14], Is.EqualTo(expectedArg15));


    // Checking the return value
    Assert.That(mockInvocation.ReturnValue, Is.EqualTo(expectedResult));

    return this;
  }

  public CallSequence<T> VerifyInvocation<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(
    Expression<Func<T, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>>> invocation,
    T1 expectedArg1, T2 expectedArg2, T3 expectedArg3, T4 expectedArg4, T5 expectedArg5, T6 expectedArg6, T7 expectedArg7, T8 expectedArg8, T9 expectedArg9, T10 expectedArg10,
    T11 expectedArg11, T12 expectedArg12, T13 expectedArg13, T14 expectedArg14, T15 expectedArg15, T16 expectedArg16
  )
  {
    var methodImpl = GetMethodInfo(invocation);
    var mockInvocation = GetNextInvocation();

    // Ensuring called method matches the expected one
    Assert.That(mockInvocation.Method, Is.EqualTo(methodImpl));

    // Ensuring the number of arguments is equal
    Assert.That(mockInvocation.Arguments.Count, Is.EqualTo(16));

    // Checking arguments one by one
    Assert.That(mockInvocation.Arguments[0], Is.EqualTo(expectedArg1));
    Assert.That(mockInvocation.Arguments[1], Is.EqualTo(expectedArg2));
    Assert.That(mockInvocation.Arguments[2], Is.EqualTo(expectedArg3));
    Assert.That(mockInvocation.Arguments[3], Is.EqualTo(expectedArg4));
    Assert.That(mockInvocation.Arguments[4], Is.EqualTo(expectedArg5));
    Assert.That(mockInvocation.Arguments[5], Is.EqualTo(expectedArg6));
    Assert.That(mockInvocation.Arguments[6], Is.EqualTo(expectedArg7));
    Assert.That(mockInvocation.Arguments[7], Is.EqualTo(expectedArg8));
    Assert.That(mockInvocation.Arguments[8], Is.EqualTo(expectedArg9));
    Assert.That(mockInvocation.Arguments[9], Is.EqualTo(expectedArg10));
    Assert.That(mockInvocation.Arguments[10], Is.EqualTo(expectedArg11));
    Assert.That(mockInvocation.Arguments[11], Is.EqualTo(expectedArg12));
    Assert.That(mockInvocation.Arguments[12], Is.EqualTo(expectedArg13));
    Assert.That(mockInvocation.Arguments[13], Is.EqualTo(expectedArg14));
    Assert.That(mockInvocation.Arguments[14], Is.EqualTo(expectedArg15));
    Assert.That(mockInvocation.Arguments[15], Is.EqualTo(expectedArg16));


    return this;
  }

  public CallSequence<T> VerifyInvocation<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult>(
    Expression<Func<T, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult>>> invocation,
    T1 expectedArg1, T2 expectedArg2, T3 expectedArg3, T4 expectedArg4, T5 expectedArg5, T6 expectedArg6, T7 expectedArg7, T8 expectedArg8, T9 expectedArg9, T10 expectedArg10,
    T11 expectedArg11, T12 expectedArg12, T13 expectedArg13, T14 expectedArg14, T15 expectedArg15, T16 expectedArg16,
    TResult expectedResult
  )
  {
    var methodImpl = GetMethodInfo(invocation);
    var mockInvocation = GetNextInvocation();

    // Ensuring called method matches the expected one
    Assert.That(mockInvocation.Method, Is.EqualTo(methodImpl));

    // Ensuring the number of arguments is equal
    Assert.That(mockInvocation.Arguments.Count, Is.EqualTo(16));

    // Checking arguments one by one
    Assert.That(mockInvocation.Arguments[0], Is.EqualTo(expectedArg1));
    Assert.That(mockInvocation.Arguments[1], Is.EqualTo(expectedArg2));
    Assert.That(mockInvocation.Arguments[2], Is.EqualTo(expectedArg3));
    Assert.That(mockInvocation.Arguments[3], Is.EqualTo(expectedArg4));
    Assert.That(mockInvocation.Arguments[4], Is.EqualTo(expectedArg5));
    Assert.That(mockInvocation.Arguments[5], Is.EqualTo(expectedArg6));
    Assert.That(mockInvocation.Arguments[6], Is.EqualTo(expectedArg7));
    Assert.That(mockInvocation.Arguments[7], Is.EqualTo(expectedArg8));
    Assert.That(mockInvocation.Arguments[8], Is.EqualTo(expectedArg9));
    Assert.That(mockInvocation.Arguments[9], Is.EqualTo(expectedArg10));
    Assert.That(mockInvocation.Arguments[10], Is.EqualTo(expectedArg11));
    Assert.That(mockInvocation.Arguments[11], Is.EqualTo(expectedArg12));
    Assert.That(mockInvocation.Arguments[12], Is.EqualTo(expectedArg13));
    Assert.That(mockInvocation.Arguments[13], Is.EqualTo(expectedArg14));
    Assert.That(mockInvocation.Arguments[14], Is.EqualTo(expectedArg15));
    Assert.That(mockInvocation.Arguments[15], Is.EqualTo(expectedArg16));


    // Checking the return value
    Assert.That(mockInvocation.ReturnValue, Is.EqualTo(expectedResult));

    return this;
  }

  public CallSequence<T> VerifyNoOtherInvocation()
  {
    Assert.AreEqual(invocationsCounter, mock.Invocations.Count, "Expecting mock to have exactly {0} invocation(s), but it has only {1}.", invocationsCounter,
      mock.Invocations.Count);
    return this;
  }

  private MethodInfo GetMethodInfo(Expression expression)
  {
    return ((((expression as LambdaExpression)?.Body as UnaryExpression)?.Operand as MethodCallExpression)?.Object as ConstantExpression)?.Value as MethodInfo ??
           throw new ArgumentException("Unsupported expression for VerifyInvocation");
  }

  private IInvocation GetNextInvocation()
  {
    int invocationIndex = invocationsCounter++;
    Assert.Less(invocationIndex, mock.Invocations.Count, "Expecting mock to have at least {0} invocation(s), but it has only {1}.", invocationIndex + 1, mock.Invocations.Count);
    return mock.Invocations[invocationIndex];
  }
}
