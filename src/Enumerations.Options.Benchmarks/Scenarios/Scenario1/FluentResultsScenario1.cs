namespace ReillyDigital.Enumerations.Options.Benchmarks.Scenarios;

using BenchmarkDotNet.Attributes;

public partial class Scenario1
{
	[Benchmark]
	[BenchmarkCategory("S1: Return Void Success")]
	public void S1_ReturnVoidSuccess_FluentResults()
	{
		for (var iteration = 0; iteration < Iterations; iteration++)
		{
			_ = S1_ReturnVoidSuccess_Impl_FluentResults();
		}
	}

	[Benchmark]
	[BenchmarkCategory("S1: Return Void Error No Message")]
	public void S1_ReturnVoidErrorNoMessage_FluentResults()
	{
		for (var iteration = 0; iteration < Iterations; iteration++)
		{
			_ = S1_ReturnVoidErrorNoMessage_Impl_FluentResults();
		}
	}

	[Benchmark]
	[BenchmarkCategory("S1: Return Void Error With Message")]
	public void S1_ReturnVoidErrorWithMessage_FluentResults()
	{
		for (var iteration = 0; iteration < Iterations; iteration++)
		{
			_ = S1_ReturnVoidErrorWithMessage_Impl_FluentResults();
		}
	}

	[Benchmark]
	[BenchmarkCategory("S1: Return Option String Success")]
	public void S1_ReturnOptionStringSuccess_FluentResults()
	{
		for (var iteration = 0; iteration < Iterations; iteration++)
		{
			_ = S1_ReturnOptionStringSuccess_Impl_FluentResults();
		}
	}

	[Benchmark]
	[BenchmarkCategory("S1: Return Option String Error No Message")]
	public void S1_ReturnOptionStringErrorNoMessage_FluentResults()
	{
		for (var iteration = 0; iteration < Iterations; iteration++)
		{
			_ = S1_ReturnOptionStringErrorNoMessage_Impl_FluentResults();
		}
	}

	[Benchmark]
	[BenchmarkCategory("S1: Return Option String Error With Message")]
	public void S1_ReturnOptionStringErrorWithMessage_FluentResults()
	{
		for (var iteration = 0; iteration < Iterations; iteration++)
		{
			_ = S1_ReturnOptionStringErrorWithMessage_Impl_FluentResults();
		}
	}

	[Benchmark]
	[BenchmarkCategory("S1: Return Option String None")]
	public void S1_ReturnOptionStringNone_FluentResults()
	{
		for (var iteration = 0; iteration < Iterations; iteration++)
		{
			_ = S1_ReturnOptionStringNone_Impl_FluentResults();
		}
	}

	[Benchmark]
	[BenchmarkCategory("S1: Check Void Success")]
	public void S1_CheckVoidSuccess_FluentResults()
	{
		for (var iteration = 0; iteration < Iterations; iteration++)
		{
			_ = S1_ReturnVoidSuccess_Impl_FluentResults().IsSuccess;
		}
	}

	[Benchmark]
	[BenchmarkCategory("S1: Check Void Error No Message")]
	public void S1_CheckVoidErrorNoMessage_FluentResults()
	{
		for (var iteration = 0; iteration < Iterations; iteration++)
		{
			_ = S1_ReturnVoidErrorNoMessage_Impl_FluentResults().IsFailed;
		}
	}

	[Benchmark]
	[BenchmarkCategory("S1: Check Void Error With Message")]
	public void S1_CheckVoidErrorWithMessage_FluentResults()
	{
		for (var iteration = 0; iteration < Iterations; iteration++)
		{
			var result = S1_ReturnVoidErrorWithMessage_Impl_FluentResults();
			if (result.IsFailed)
			{
				_ = result.Errors[0];
			}
		}
	}

	[Benchmark]
	[BenchmarkCategory("S1: Check Option String Success")]
	public void S1_CheckOptionStringSuccess_FluentResults()
	{
		for (var iteration = 0; iteration < Iterations; iteration++)
		{
			_ = S1_ReturnOptionStringSuccess_Impl_FluentResults().IsSuccess;
		}
	}

	[Benchmark]
	[BenchmarkCategory("S1: Check Option String Error No Message")]
	public void S1_CheckOptionStringErrorNoMessage_FluentResults()
	{
		for (var iteration = 0; iteration < Iterations; iteration++)
		{
			_ = S1_ReturnOptionStringErrorNoMessage_Impl_FluentResults().IsFailed;
		}
	}

	[Benchmark]
	[BenchmarkCategory("S1: Check Option String Error With Message")]
	public void S1_CheckOptionStringErrorWithMessage_FluentResults()
	{
		for (var iteration = 0; iteration < Iterations; iteration++)
		{
			var result = S1_ReturnOptionStringErrorWithMessage_Impl_FluentResults();
			if (result.IsFailed)
			{
				_ = result.Errors[0];
			}
		}
	}

	private FluentResults.Result S1_ReturnVoidSuccess_Impl_FluentResults() => FluentResults.Result.Ok();

	private FluentResults.Result S1_ReturnVoidErrorNoMessage_Impl_FluentResults() => FluentResults.Result.Fail("");

	private FluentResults.Result S1_ReturnVoidErrorWithMessage_Impl_FluentResults() => FluentResults.Result.Fail(new FluentResults.Error(ErrorMessage));

	private FluentResults.Result<string> S1_ReturnOptionStringSuccess_Impl_FluentResults() => FluentResults.Result.Ok(SuccessMessage);

	private FluentResults.Result<string> S1_ReturnOptionStringErrorNoMessage_Impl_FluentResults() => FluentResults.Result.Fail<string>("");

	private FluentResults.Result<string> S1_ReturnOptionStringErrorWithMessage_Impl_FluentResults() => FluentResults.Result.Fail<string>(new FluentResults.Error(ErrorMessage));

	private FluentResults.Result<string?> S1_ReturnOptionStringNone_Impl_FluentResults() => FluentResults.Result.Ok<string?>(null);
}
