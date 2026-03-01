namespace ReillyDigital.Enumerations.Options.Benchmarks.Scenarios;

using BenchmarkDotNet.Attributes;

using SimpleResults;

public partial class Scenario1
{
	[Benchmark]
	[BenchmarkCategory("S1: Return Void Success")]
	public void S1_ReturnVoidSuccess_SimpleResults()
	{
		for (var iteration = 0; iteration < Iterations; iteration++)
		{
			_ = S1_ReturnVoidSuccess_Impl_SimpleResults();
		}
	}

	[Benchmark]
	[BenchmarkCategory("S1: Return Void Error No Message")]
	public void S1_ReturnVoidErrorNoMessage_SimpleResults()
	{
		for (var iteration = 0; iteration < Iterations; iteration++)
		{
			_ = S1_ReturnVoidErrorNoMessage_Impl_SimpleResults();
		}
	}

	[Benchmark]
	[BenchmarkCategory("S1: Return Void Error With Message")]
	public void S1_ReturnVoidErrorWithMessage_SimpleResults()
	{
		for (var iteration = 0; iteration < Iterations; iteration++)
		{
			_ = S1_ReturnVoidErrorWithMessage_Impl_SimpleResults();
		}
	}

	[Benchmark]
	[BenchmarkCategory("S1: Return Option String Success")]
	public void S1_ReturnOptionStringSuccess_SimpleResults()
	{
		for (var iteration = 0; iteration < Iterations; iteration++)
		{
			_ = S1_ReturnOptionStringSuccess_Impl_SimpleResults();
		}
	}

	[Benchmark]
	[BenchmarkCategory("S1: Return Option String Error No Message")]
	public void S1_ReturnOptionStringErrorNoMessage_SimpleResults()
	{
		for (var iteration = 0; iteration < Iterations; iteration++)
		{
			_ = S1_ReturnOptionStringErrorNoMessage_Impl_SimpleResults();
		}
	}

	[Benchmark]
	[BenchmarkCategory("S1: Return Option String Error With Message")]
	public void S1_ReturnOptionStringErrorWithMessage_SimpleResults()
	{
		for (var iteration = 0; iteration < Iterations; iteration++)
		{
			_ = S1_ReturnOptionStringErrorWithMessage_Impl_SimpleResults();
		}
	}

	[Benchmark]
	[BenchmarkCategory("S1: Return Option String None")]
	public void S1_ReturnOptionStringNone_SimpleResults()
	{
		for (var iteration = 0; iteration < Iterations; iteration++)
		{
			_ = S1_ReturnOptionStringNone_Impl_SimpleResults();
		}
	}

	[Benchmark]
	[BenchmarkCategory("S1: Check Void Success")]
	public void S1_CheckVoidSuccess_SimpleResults()
	{
		for (var iteration = 0; iteration < Iterations; iteration++)
		{
			_ = S1_ReturnVoidSuccess_Impl_SimpleResults().IsSuccess;
		}
	}

	[Benchmark]
	[BenchmarkCategory("S1: Check Void Error No Message")]
	public void S1_CheckVoidErrorNoMessage_SimpleResults()
	{
		for (var iteration = 0; iteration < Iterations; iteration++)
		{
			_ = S1_ReturnVoidErrorNoMessage_Impl_SimpleResults().IsFailed;
		}
	}

	[Benchmark]
	[BenchmarkCategory("S1: Check Void Error With Message")]
	public void S1_CheckVoidErrorWithMessage_SimpleResults()
	{
		for (var iteration = 0; iteration < Iterations; iteration++)
		{
			var result = S1_ReturnVoidErrorWithMessage_Impl_SimpleResults();
			if (result.IsFailed)
			{
				_ = result.Errors.FirstOrDefault();
			}
		}
	}

	[Benchmark]
	[BenchmarkCategory("S1: Check Option String Success")]
	public void S1_CheckOptionStringSuccess_SimpleResults()
	{
		for (var iteration = 0; iteration < Iterations; iteration++)
		{
			_ = S1_ReturnOptionStringSuccess_Impl_SimpleResults().IsSuccess;
		}
	}

	[Benchmark]
	[BenchmarkCategory("S1: Check Option String Error No Message")]
	public void S1_CheckOptionStringErrorNoMessage_SimpleResults()
	{
		for (var iteration = 0; iteration < Iterations; iteration++)
		{
			_ = S1_ReturnOptionStringErrorNoMessage_Impl_SimpleResults().IsFailed;
		}
	}

	[Benchmark]
	[BenchmarkCategory("S1: Check Option String Error With Message")]
	public void S1_CheckOptionStringErrorWithMessage_SimpleResults()
	{
		for (var iteration = 0; iteration < Iterations; iteration++)
		{
			var result = S1_ReturnOptionStringErrorWithMessage_Impl_SimpleResults();
			if (result.IsFailed)
			{
				_ = result.Errors.FirstOrDefault();
			}
		}
	}

	private Result S1_ReturnVoidSuccess_Impl_SimpleResults() => Result.Success();

	private Result S1_ReturnVoidErrorNoMessage_Impl_SimpleResults() => Result.Failure();

	private Result S1_ReturnVoidErrorWithMessage_Impl_SimpleResults() => Result.Failure(ErrorMessage);

	private Result<string> S1_ReturnOptionStringSuccess_Impl_SimpleResults() => Result.Success(SuccessMessage);

	private Result<string> S1_ReturnOptionStringErrorNoMessage_Impl_SimpleResults() => (Result<string>)Result.Failure();

	private Result<string> S1_ReturnOptionStringErrorWithMessage_Impl_SimpleResults() => (Result<string>)Result.Failure(ErrorMessage);

	private Result<string?> S1_ReturnOptionStringNone_Impl_SimpleResults() => Result.Success<string?>(null);
}
