namespace Cusco.Core.Test;

public class ExceptionExtensionsTests
{
  [SetUp]
  public void SetUp()
  {
    CuscoRT.featureFlags.enhanceExceptions = true;
  }

  [Test]
  public void EnhanceExceptionSetStackTraceIfNull()
  {
    var exc = new Exception();

    Assert.That(exc.StackTrace, Is.Null);

    exc.Enhance();
    Assert.That(exc.StackTrace, Is.Not.Null);
    Assert.That(exc.StackTrace, Contains.Substring("EnhanceException"));
  }

  [Test]
  public void DoesntEnhanceExceptionWhenFeatureIsDisabled()
  {
    CuscoRT.featureFlags.enhanceExceptions = false;
    var exc = new Exception();

    Assert.That(exc.StackTrace, Is.Null);

    exc.Enhance();
    Assert.That(exc.StackTrace, Is.Null);
  }

  [Test]
  public void EnhanceExceptionKeepExistingStackTrace()
  {
    void AVerySpecificMethodName()
    {
      throw new Exception();
    }

    try
    {
      AVerySpecificMethodName();
    }
    catch (Exception exc)
    {
      Assert.That(exc.StackTrace, Is.Not.Null);
      var stackTrace = exc.StackTrace;

      exc.Enhance();

      Assert.That(exc.StackTrace, Is.Not.Null);
      Assert.That(exc.StackTrace, Is.EqualTo(stackTrace));
      Assert.That(exc.StackTrace, Contains.Substring("AVerySpecificMethodName"));
    }
  }

  [Test]
  public void RethrowKeepsStackTrace()
  {
    void AVerySpecificMethodName()
    {
      throw new Exception();
    }

    try
    {
      AVerySpecificMethodName();
    }
    catch (Exception exc)
    {
      Assert.That(exc.StackTrace, Is.Not.Null);
      Assert.That(exc.StackTrace, Contains.Substring("AVerySpecificMethodName"));

      try
      {
        exc.Rethrow();
      }
      catch (Exception rethrownExc)
      {
        Assert.That(rethrownExc.StackTrace, Is.Not.Null);
        Assert.That(rethrownExc.StackTrace, Contains.Substring("AVerySpecificMethodName"));
        Assert.That(rethrownExc.StackTrace, Is.EqualTo(exc.StackTrace));
      }
    }
  }
}
