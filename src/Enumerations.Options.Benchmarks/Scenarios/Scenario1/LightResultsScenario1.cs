namespace ReillyDigital.Enumerations.Options.Benchmarks.Scenarios;

using BenchmarkDotNet.Attributes;

using LightResults;

public partial class Scenario1
{
	[Benchmark]
	[BenchmarkCategory("S1: Return Void Success")]
	public void S1_ReturnVoidSuccess_LightResults()
	{
		for (var iteration = 0; iteration < Iterations; iteration++)
		{
			_ = S1_ReturnVoidSuccess_Impl_LightResults();
		}
	}

	[Benchmark]
	[BenchmarkCategory("S1: Return Void Error No Message")]
	public void S1_ReturnVoidErrorNoMessage_LightResults()
	{
		for (var iteration = 0; iteration < Iterations; iteration++)
		{
			_ = S1_ReturnVoidErrorNoMessage_Impl_LightResults();
		}
	}

	[Benchmark]
	[BenchmarkCategory("S1: Return Void Error With Message")]
	public void S1_ReturnVoidErrorWithMessage_LightResults()
	{
		for (var iteration = 0; iteration < Iterations; iteration++)
		{
			_ = S1_ReturnVoidErrorWithMessage_Impl_LightResults();
		}
	}

	[Benchmark]
	[BenchmarkCategory("S1: Return Option String Success")]
	public void S1_ReturnOptionStringSuccess_LightResults()
	{
		for (var iteration = 0; iteration < Iterations; iteration++)
		{
			_ = S1_ReturnOptionStringSuccess_Impl_LightResults();
		}
	}

	[Benchmark]
	[BenchmarkCategory("S1: Return Option String Error No Message")]
	public void S1_ReturnOptionStringErrorNoMessage_LightResults()
	{
		for (var iteration = 0; iteration < Iterations; iteration++)
		{
			_ = S1_ReturnOptionStringErrorNoMessage_Impl_LightResults();
		}
	}

	[Benchmark]
	[BenchmarkCategory("S1: Return Option String Error With Message")]
	public void S1_ReturnOptionStringErrorWithMessage_LightResults()
	{
		for (var iteration = 0; iteration < Iterations; iteration++)
		{
			_ = S1_ReturnOptionStringErrorWithMessage_Impl_LightResults();
		}
	}

	[Benchmark]
	[BenchmarkCategory("S1: Return Option String None")]
	public void S1_ReturnOptionStringNone_LightResults()
	{
		for (var iteration = 0; iteration < Iterations; iteration++)
		{
			_ = S1_ReturnOptionStringNone_Impl_LightResults();
		}
	}

	[Benchmark]
	[BenchmarkCategory("S1: Check Void Success")]
	public void S1_CheckVoidSuccess_LightResults()
	{
		for (var iteration = 0; iteration < Iterations; iteration++)
		{
			_ = S1_ReturnVoidSuccess_Impl_LightResults().IsSuccess();
		}
	}

	[Benchmark]
	[BenchmarkCategory("S1: Check Void Error No Message")]
	public void S1_CheckVoidErrorNoMessage_LightResults()
	{
		for (var iteration = 0; iteration < Iterations; iteration++)
		{
			_ = S1_ReturnVoidErrorNoMessage_Impl_LightResults().IsFailure();
		}
	}

	[Benchmark]
	[BenchmarkCategory("S1: Check Void Error With Message")]
	public void S1_CheckVoidErrorWithMessage_LightResults()
	{
		for (var iteration = 0; iteration < Iterations; iteration++)
		{
			if (S1_ReturnVoidErrorWithMessage_Impl_LightResults().IsFailure(out var error))
			{
				_ = error;
			}
		}
	}

	[Benchmark]
	[BenchmarkCategory("S1: Check Option String Success")]
	public void S1_CheckOptionStringSuccess_LightResults()
	{
		for (var iteration = 0; iteration < Iterations; iteration++)
		{
			_ = S1_ReturnOptionStringSuccess_Impl_LightResults().IsSuccess();
		}
	}

	[Benchmark]
	[BenchmarkCategory("S1: Check Option String Error No Message")]
	public void S1_CheckOptionStringErrorNoMessage_LightResults()
	{
		for (var iteration = 0; iteration < Iterations; iteration++)
		{
			_ = S1_ReturnOptionStringErrorNoMessage_Impl_LightResults().IsFailure();
		}
	}

	[Benchmark]
	[BenchmarkCategory("S1: Check Option String Error With Message")]
	public void S1_CheckOptionStringErrorWithMessage_LightResults()
	{
		for (var iteration = 0; iteration < Iterations; iteration++)
		{
			if (S1_ReturnOptionStringErrorWithMessage_Impl_LightResults().IsFailure(out var error))
			{
				_ = error;
			}
		}
	}

	private Result S1_ReturnVoidSuccess_Impl_LightResults() => Result.Success();

	private Result S1_ReturnVoidErrorNoMessage_Impl_LightResults() => Result.Failure();

	private Result S1_ReturnVoidErrorWithMessage_Impl_LightResults() => Result.Failure(new Error(ErrorMessage));

	private Result<string> S1_ReturnOptionStringSuccess_Impl_LightResults() => Result.Success<string>(SuccessMessage);

	private Result<string> S1_ReturnOptionStringErrorNoMessage_Impl_LightResults() => Result.Failure<string>();

	private Result<string> S1_ReturnOptionStringErrorWithMessage_Impl_LightResults() => Result.Failure<string>(new Error(ErrorMessage));

	private Result<string?> S1_ReturnOptionStringNone_Impl_LightResults() => Result.Success<string?>(null);
}
