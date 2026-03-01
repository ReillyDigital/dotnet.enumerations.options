namespace ReillyDigital.Enumerations.Options.Benchmarks.Scenarios;

using BenchmarkDotNet.Attributes;

public partial class Scenario1
{
	[Benchmark]
	[BenchmarkCategory("S1: Return Void Success")]
	public void S1_ReturnVoidSuccess_ArdalisResult()
	{
		for (var iteration = 0; iteration < Iterations; iteration++)
		{
			_ = S1_ReturnVoidSuccess_Impl_ArdalisResult();
		}
	}

	[Benchmark]
	[BenchmarkCategory("S1: Return Void Error No Message")]
	public void S1_ReturnVoidErrorNoMessage_ArdalisResult()
	{
		for (var iteration = 0; iteration < Iterations; iteration++)
		{
			_ = S1_ReturnVoidErrorNoMessage_Impl_ArdalisResult();
		}
	}

	[Benchmark]
	[BenchmarkCategory("S1: Return Void Error With Message")]
	public void S1_ReturnVoidErrorWithMessage_ArdalisResult()
	{
		for (var iteration = 0; iteration < Iterations; iteration++)
		{
			_ = S1_ReturnVoidErrorWithMessage_Impl_ArdalisResult();
		}
	}

	[Benchmark]
	[BenchmarkCategory("S1: Return Option String Success")]
	public void S1_ReturnOptionStringSuccess_ArdalisResult()
	{
		for (var iteration = 0; iteration < Iterations; iteration++)
		{
			_ = S1_ReturnOptionStringSuccess_Impl_ArdalisResult();
		}
	}

	[Benchmark]
	[BenchmarkCategory("S1: Return Option String Error No Message")]
	public void S1_ReturnOptionStringErrorNoMessage_ArdalisResult()
	{
		for (var iteration = 0; iteration < Iterations; iteration++)
		{
			_ = S1_ReturnOptionStringErrorNoMessage_Impl_ArdalisResult();
		}
	}

	[Benchmark]
	[BenchmarkCategory("S1: Return Option String Error With Message")]
	public void S1_ReturnOptionStringErrorWithMessage_ArdalisResult()
	{
		for (var iteration = 0; iteration < Iterations; iteration++)
		{
			_ = S1_ReturnOptionStringErrorWithMessage_Impl_ArdalisResult();
		}
	}

	[Benchmark]
	[BenchmarkCategory("S1: Return Option String None")]
	public void S1_ReturnOptionStringNone_ArdalisResult()
	{
		for (var iteration = 0; iteration < Iterations; iteration++)
		{
			_ = S1_ReturnOptionStringNone_Impl_ArdalisResult();
		}
	}

	[Benchmark]
	[BenchmarkCategory("S1: Check Void Success")]
	public void S1_CheckVoidSuccess_ArdalisResult()
	{
		for (var iteration = 0; iteration < Iterations; iteration++)
		{
			_ = S1_ReturnVoidSuccess_Impl_ArdalisResult().IsSuccess;
		}
	}

	[Benchmark]
	[BenchmarkCategory("S1: Check Void Error No Message")]
	public void S1_CheckVoidErrorNoMessage_ArdalisResult()
	{
		for (var iteration = 0; iteration < Iterations; iteration++)
		{
			_ = !S1_ReturnVoidErrorNoMessage_Impl_ArdalisResult().IsSuccess;
		}
	}

	[Benchmark]
	[BenchmarkCategory("S1: Check Void Error With Message")]
	public void S1_CheckVoidErrorWithMessage_ArdalisResult()
	{
		for (var iteration = 0; iteration < Iterations; iteration++)
		{
			var result = S1_ReturnVoidErrorWithMessage_Impl_ArdalisResult();
			if (!result.IsSuccess)
			{
				_ = result.Errors.FirstOrDefault();
			}
		}
	}

	[Benchmark]
	[BenchmarkCategory("S1: Check Option String Success")]
	public void S1_CheckOptionStringSuccess_ArdalisResult()
	{
		for (var iteration = 0; iteration < Iterations; iteration++)
		{
			_ = S1_ReturnOptionStringSuccess_Impl_ArdalisResult().IsSuccess;
		}
	}

	[Benchmark]
	[BenchmarkCategory("S1: Check Option String Error No Message")]
	public void S1_CheckOptionStringErrorNoMessage_ArdalisResult()
	{
		for (var iteration = 0; iteration < Iterations; iteration++)
		{
			_ = !S1_ReturnOptionStringErrorNoMessage_Impl_ArdalisResult().IsSuccess;
		}
	}

	[Benchmark]
	[BenchmarkCategory("S1: Check Option String Error With Message")]
	public void S1_CheckOptionStringErrorWithMessage_ArdalisResult()
	{
		for (var iteration = 0; iteration < Iterations; iteration++)
		{
			var result = S1_ReturnOptionStringErrorWithMessage_Impl_ArdalisResult();
			if (!result.IsSuccess)
			{
				_ = result.Errors.FirstOrDefault();
			}
		}
	}

	private Ardalis.Result.Result S1_ReturnVoidSuccess_Impl_ArdalisResult() => Ardalis.Result.Result.Success();

	private Ardalis.Result.Result S1_ReturnVoidErrorNoMessage_Impl_ArdalisResult() => Ardalis.Result.Result.Error();

	private Ardalis.Result.Result S1_ReturnVoidErrorWithMessage_Impl_ArdalisResult() => Ardalis.Result.Result.Error(ErrorMessage);

	private Ardalis.Result.Result<string> S1_ReturnOptionStringSuccess_Impl_ArdalisResult() => Ardalis.Result.Result<string>.Success(SuccessMessage);

	private Ardalis.Result.Result<string> S1_ReturnOptionStringErrorNoMessage_Impl_ArdalisResult() => Ardalis.Result.Result<string>.Error();

	private Ardalis.Result.Result<string> S1_ReturnOptionStringErrorWithMessage_Impl_ArdalisResult() => Ardalis.Result.Result<string>.Error(ErrorMessage);

	private Ardalis.Result.Result<string?> S1_ReturnOptionStringNone_Impl_ArdalisResult() => Ardalis.Result.Result<string?>.Success(null);
}
