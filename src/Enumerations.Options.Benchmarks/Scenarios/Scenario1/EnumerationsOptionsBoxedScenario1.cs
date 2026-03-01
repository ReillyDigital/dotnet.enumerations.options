namespace ReillyDigital.Enumerations.Options.Benchmarks.Scenarios;

using BenchmarkDotNet.Attributes;

using ReillyDigital.Enumerations.Options;
using ReillyDigital.Enumerations.Options.Boxed;
using static ReillyDigital.Enumerations.OptionsFunctions;

public partial class Scenario1
{
	[Benchmark]
	[BenchmarkCategory("S1: Return Void Success")]
	public void S1_ReturnVoidSuccess_EnumerationsOptionsBoxed()
	{
		for (var iteration = 0; iteration < Iterations; iteration++)
		{
			_ = S1_ReturnVoidSuccess_Impl_EnumerationsOptionsBoxed();
		}
	}

	[Benchmark]
	[BenchmarkCategory("S1: Return Void Error No Message")]
	public void S1_ReturnVoidErrorNoMessage_EnumerationsOptionsBoxed()
	{
		for (var iteration = 0; iteration < Iterations; iteration++)
		{
			_ = S1_ReturnVoidErrorNoMessage_Impl_EnumerationsOptionsBoxed();
		}
	}

	[Benchmark]
	[BenchmarkCategory("S1: Return Void Error With Message")]
	public void S1_ReturnVoidErrorWithMessage_EnumerationsOptionsBoxed()
	{
		for (var iteration = 0; iteration < Iterations; iteration++)
		{
			_ = S1_ReturnVoidErrorWithMessage_Impl_EnumerationsOptionsBoxed();
		}
	}

	[Benchmark]
	[BenchmarkCategory("S1: Return Option String Success")]
	public void S1_ReturnOptionStringSuccess_EnumerationsOptionsBoxed()
	{
		for (var iteration = 0; iteration < Iterations; iteration++)
		{
			_ = S1_ReturnOptionStringSuccess_Impl_EnumerationsOptionsBoxed();
		}
	}

	[Benchmark]
	[BenchmarkCategory("S1: Return Option String Error No Message")]
	public void S1_ReturnOptionStringErrorNoMessage_EnumerationsOptionsBoxed()
	{
		for (var iteration = 0; iteration < Iterations; iteration++)
		{
			_ = S1_ReturnOptionStringErrorNoMessage_Impl_EnumerationsOptionsBoxed();
		}
	}

	[Benchmark]
	[BenchmarkCategory("S1: Return Option String Error With Message")]
	public void S1_ReturnOptionStringErrorWithMessage_EnumerationsOptionsBoxed()
	{
		for (var iteration = 0; iteration < Iterations; iteration++)
		{
			_ = S1_ReturnOptionStringErrorWithMessage_Impl_EnumerationsOptionsBoxed();
		}
	}

	[Benchmark]
	[BenchmarkCategory("S1: Return Option String None")]
	public void S1_ReturnOptionStringNone_EnumerationsOptionsBoxed()
	{
		for (var iteration = 0; iteration < Iterations; iteration++)
		{
			_ = S1_ReturnOptionStringNone_Impl_EnumerationsOptionsBoxed();
		}
	}

	[Benchmark]
	[BenchmarkCategory("S1: Check Void Success")]
	public void S1_CheckVoidSuccess_EnumerationsOptionsBoxed()
	{
		for (var iteration = 0; iteration < Iterations; iteration++)
		{
			_ = S1_ReturnVoidSuccess_Impl_EnumerationsOptionsBoxed() is not IError;
		}
	}

	[Benchmark]
	[BenchmarkCategory("S1: Check Void Error No Message")]
	public void S1_CheckVoidErrorNoMessage_EnumerationsOptionsBoxed()
	{
		for (var iteration = 0; iteration < Iterations; iteration++)
		{
			_ = S1_ReturnVoidErrorNoMessage_Impl_EnumerationsOptionsBoxed() is IError;
		}
	}

	[Benchmark]
	[BenchmarkCategory("S1: Check Void Error With Message")]
	public void S1_CheckVoidErrorWithMessage_EnumerationsOptionsBoxed()
	{
		for (var iteration = 0; iteration < Iterations; iteration++)
		{
			if (S1_ReturnVoidErrorWithMessage_Impl_EnumerationsOptionsBoxed() is IError error)
			{
				_ = error.Value;
			}
		}
	}

	[Benchmark]
	[BenchmarkCategory("S1: Check Option String Success")]
	public void S1_CheckOptionStringSuccess_EnumerationsOptionsBoxed()
	{
		for (var iteration = 0; iteration < Iterations; iteration++)
		{
			_ = S1_ReturnOptionStringSuccess_Impl_EnumerationsOptionsBoxed() is ISome<string>;
		}
	}

	[Benchmark]
	[BenchmarkCategory("S1: Check Option String Error No Message")]
	public void S1_CheckOptionStringErrorNoMessage_EnumerationsOptionsBoxed()
	{
		for (var iteration = 0; iteration < Iterations; iteration++)
		{
			_ = S1_ReturnOptionStringErrorNoMessage_Impl_EnumerationsOptionsBoxed() is IError;
		}
	}

	[Benchmark]
	[BenchmarkCategory("S1: Check Option String Error With Message")]
	public void S1_CheckOptionStringErrorWithMessage_EnumerationsOptionsBoxed()
	{
		for (var iteration = 0; iteration < Iterations; iteration++)
		{
			if (S1_ReturnOptionStringErrorWithMessage_Impl_EnumerationsOptionsBoxed() is IError error)
			{
				_ = error.Value;
			}
		}
	}

	private IVoid S1_ReturnVoidSuccess_Impl_EnumerationsOptionsBoxed() => BoxedVoid();

	private IVoid S1_ReturnVoidErrorNoMessage_Impl_EnumerationsOptionsBoxed() => BoxedError();

	private IVoid S1_ReturnVoidErrorWithMessage_Impl_EnumerationsOptionsBoxed() => BoxedError(ErrorMessage);

	private IOption<string> S1_ReturnOptionStringSuccess_Impl_EnumerationsOptionsBoxed() => BoxedSome(SuccessMessage);

	private IOption<string> S1_ReturnOptionStringErrorNoMessage_Impl_EnumerationsOptionsBoxed() => BoxedError<string>();

	private IOption<string> S1_ReturnOptionStringErrorWithMessage_Impl_EnumerationsOptionsBoxed() => BoxedError<string>(ErrorMessage);

	private IOption<string> S1_ReturnOptionStringNone_Impl_EnumerationsOptionsBoxed() => BoxedNone<string>();
}
