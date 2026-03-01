namespace ReillyDigital.Enumerations.Options.Benchmarks.Scenarios;

using BenchmarkDotNet.Attributes;

using Rascal;
using Rascal.Errors;

public partial class Scenario1
{
	[Benchmark]
	[BenchmarkCategory("S1: Return Void Success")]
	public void S1_ReturnVoidSuccess_Rascal()
	{
		for (var iteration = 0; iteration < Iterations; iteration++)
		{
			_ = S1_ReturnVoidSuccess_Impl_Rascal();
		}
	}

	[Benchmark]
	[BenchmarkCategory("S1: Return Void Error No Message")]
	public void S1_ReturnVoidErrorNoMessage_Rascal()
	{
		for (var iteration = 0; iteration < Iterations; iteration++)
		{
			_ = S1_ReturnVoidErrorNoMessage_Impl_Rascal();
		}
	}

	[Benchmark]
	[BenchmarkCategory("S1: Return Void Error With Message")]
	public void S1_ReturnVoidErrorWithMessage_Rascal()
	{
		for (var iteration = 0; iteration < Iterations; iteration++)
		{
			_ = S1_ReturnVoidErrorWithMessage_Impl_Rascal();
		}
	}

	[Benchmark]
	[BenchmarkCategory("S1: Return Option String Success")]
	public void S1_ReturnOptionStringSuccess_Rascal()
	{
		for (var iteration = 0; iteration < Iterations; iteration++)
		{
			_ = S1_ReturnOptionStringSuccess_Impl_Rascal();
		}
	}

	[Benchmark]
	[BenchmarkCategory("S1: Return Option String Error No Message")]
	public void S1_ReturnOptionStringErrorNoMessage_Rascal()
	{
		for (var iteration = 0; iteration < Iterations; iteration++)
		{
			_ = S1_ReturnOptionStringErrorNoMessage_Impl_Rascal();
		}
	}

	[Benchmark]
	[BenchmarkCategory("S1: Return Option String Error With Message")]
	public void S1_ReturnOptionStringErrorWithMessage_Rascal()
	{
		for (var iteration = 0; iteration < Iterations; iteration++)
		{
			_ = S1_ReturnOptionStringErrorWithMessage_Impl_Rascal();
		}
	}

	[Benchmark]
	[BenchmarkCategory("S1: Return Option String None")]
	public void S1_ReturnOptionStringNone_Rascal()
	{
		for (var iteration = 0; iteration < Iterations; iteration++)
		{
			_ = S1_ReturnOptionStringNone_Impl_Rascal();
		}
	}

	[Benchmark]
	[BenchmarkCategory("S1: Check Void Success")]
	public void S1_CheckVoidSuccess_Rascal()
	{
		for (var iteration = 0; iteration < Iterations; iteration++)
		{
			_ = S1_ReturnVoidSuccess_Impl_Rascal().IsOk;
		}
	}

	[Benchmark]
	[BenchmarkCategory("S1: Check Void Error No Message")]
	public void S1_CheckVoidErrorNoMessage_Rascal()
	{
		for (var iteration = 0; iteration < Iterations; iteration++)
		{
			_ = S1_ReturnVoidErrorNoMessage_Impl_Rascal().IsError;
		}
	}

	[Benchmark]
	[BenchmarkCategory("S1: Check Void Error With Message")]
	public void S1_CheckVoidErrorWithMessage_Rascal()
	{
		for (var iteration = 0; iteration < Iterations; iteration++)
		{
			if (S1_ReturnVoidErrorWithMessage_Impl_Rascal().TryGetError(out var error))
			{
				_ = error;
			}
		}
	}

	[Benchmark]
	[BenchmarkCategory("S1: Check Option String Success")]
	public void S1_CheckOptionStringSuccess_Rascal()
	{
		for (var iteration = 0; iteration < Iterations; iteration++)
		{
			_ = S1_ReturnOptionStringSuccess_Impl_Rascal().IsOk;
		}
	}

	[Benchmark]
	[BenchmarkCategory("S1: Check Option String Error No Message")]
	public void S1_CheckOptionStringErrorNoMessage_Rascal()
	{
		for (var iteration = 0; iteration < Iterations; iteration++)
		{
			_ = S1_ReturnOptionStringErrorNoMessage_Impl_Rascal().IsError;
		}
	}

	[Benchmark]
	[BenchmarkCategory("S1: Check Option String Error With Message")]
	public void S1_CheckOptionStringErrorWithMessage_Rascal()
	{
		for (var iteration = 0; iteration < Iterations; iteration++)
		{
			if (S1_ReturnOptionStringErrorWithMessage_Impl_Rascal().TryGetError(out var error))
			{
				_ = error;
			}
		}
	}

	private Result<bool> S1_ReturnVoidSuccess_Impl_Rascal() => Prelude.Ok(true);

	private Result<bool> S1_ReturnVoidErrorNoMessage_Impl_Rascal() => Prelude.Err<bool>(new StringError(""));

	private Result<bool> S1_ReturnVoidErrorWithMessage_Impl_Rascal() => Prelude.Err<bool>(new StringError(ErrorMessage));

	private Result<string> S1_ReturnOptionStringSuccess_Impl_Rascal() => Prelude.Ok<string>(SuccessMessage);

	private Result<string> S1_ReturnOptionStringErrorNoMessage_Impl_Rascal() => Prelude.Err<string>(new StringError(""));

	private Result<string> S1_ReturnOptionStringErrorWithMessage_Impl_Rascal() => Prelude.Err<string>(new StringError(ErrorMessage));

	private Result<string?> S1_ReturnOptionStringNone_Impl_Rascal() => Prelude.Ok<string?>(null);
}
