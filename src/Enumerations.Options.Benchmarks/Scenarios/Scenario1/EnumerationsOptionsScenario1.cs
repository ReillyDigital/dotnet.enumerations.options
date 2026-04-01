namespace ReillyDigital.Enumerations.Options.Benchmarks.Scenarios;

using BenchmarkDotNet.Attributes;

using ReillyDigital.Enumerations.Options;
using static ReillyDigital.Enumerations.OptionsFunctions;

public partial class Scenario1
{
	[Benchmark(Baseline = true)]
	[BenchmarkCategory("S1: Return Void Success")]
	public void S1_ReturnVoidSuccess_EnumerationsOptions()
	{
		for (var iteration = 0; iteration < Iterations; iteration++)
		{
			_ = S1_ReturnVoidSuccess_Impl_EnumerationsOptions();
		}
	}

	[Benchmark(Baseline = true)]
	[BenchmarkCategory("S1: Return Void Error No Message")]
	public void S1_ReturnVoidErrorNoMessage_EnumerationsOptions()
	{
		for (var iteration = 0; iteration < Iterations; iteration++)
		{
			_ = S1_ReturnVoidErrorNoMessage_Impl_EnumerationsOptions();
		}
	}

	[Benchmark(Baseline = true)]
	[BenchmarkCategory("S1: Return Void Error With Message")]
	public void S1_ReturnVoidErrorWithMessage_EnumerationsOptions()
	{
		for (var iteration = 0; iteration < Iterations; iteration++)
		{
			_ = S1_ReturnVoidErrorWithMessage_Impl_EnumerationsOptions();
		}
	}

	[Benchmark(Baseline = true)]
	[BenchmarkCategory("S1: Return Option String Success")]
	public void S1_ReturnOptionStringSuccess_EnumerationsOptions()
	{
		for (var iteration = 0; iteration < Iterations; iteration++)
		{
			_ = S1_ReturnOptionStringSuccess_Impl_EnumerationsOptions();
		}
	}

	[Benchmark(Baseline = true)]
	[BenchmarkCategory("S1: Return Option String Error No Message")]
	public void S1_ReturnOptionStringErrorNoMessage_EnumerationsOptions()
	{
		for (var iteration = 0; iteration < Iterations; iteration++)
		{
			_ = S1_ReturnOptionStringErrorNoMessage_Impl_EnumerationsOptions();
		}
	}

	[Benchmark(Baseline = true)]
	[BenchmarkCategory("S1: Return Option String Error With Message")]
	public void S1_ReturnOptionStringErrorWithMessage_EnumerationsOptions()
	{
		for (var iteration = 0; iteration < Iterations; iteration++)
		{
			_ = S1_ReturnOptionStringErrorWithMessage_Impl_EnumerationsOptions();
		}
	}

	[Benchmark(Baseline = true)]
	[BenchmarkCategory("S1: Return Option String None")]
	public void S1_ReturnOptionStringNone_EnumerationsOptions()
	{
		for (var iteration = 0; iteration < Iterations; iteration++)
		{
			_ = S1_ReturnOptionStringNone_Impl_EnumerationsOptions();
		}
	}

	[Benchmark(Baseline = true)]
	[BenchmarkCategory("S1: Check Void Success")]
	public void S1_CheckVoidSuccess_EnumerationsOptions()
	{
		for (var iteration = 0; iteration < Iterations; iteration++)
		{
			_ = S1_ReturnVoidSuccess_Impl_EnumerationsOptions().IsVoid;
		}
	}

	[Benchmark(Baseline = true)]
	[BenchmarkCategory("S1: Check Void Error No Message")]
	public void S1_CheckVoidErrorNoMessage_EnumerationsOptions()
	{
		for (var iteration = 0; iteration < Iterations; iteration++)
		{
			_ = S1_ReturnVoidErrorNoMessage_Impl_EnumerationsOptions().IsError;
		}
	}

	[Benchmark(Baseline = true)]
	[BenchmarkCategory("S1: Check Void Error With Message")]
	public void S1_CheckVoidErrorWithMessage_EnumerationsOptions()
	{
		for (var iteration = 0; iteration < Iterations; iteration++)
		{
			if (S1_ReturnVoidErrorWithMessage_Impl_EnumerationsOptions() is { Type: VoidType.Error, ErrorValue: var error })
			{
				_ = error;
			}
		}
	}

	[Benchmark(Baseline = true)]
	[BenchmarkCategory("S1: Check Option String Success")]
	public void S1_CheckOptionStringSuccess_EnumerationsOptions()
	{
		for (var iteration = 0; iteration < Iterations; iteration++)
		{
			_ = S1_ReturnOptionStringSuccess_Impl_EnumerationsOptions().IsSome;
		}
	}

	[Benchmark(Baseline = true)]
	[BenchmarkCategory("S1: Check Option String Error No Message")]
	public void S1_CheckOptionStringErrorNoMessage_EnumerationsOptions()
	{
		for (var iteration = 0; iteration < Iterations; iteration++)
		{
			_ = S1_ReturnOptionStringErrorNoMessage_Impl_EnumerationsOptions().IsError;
		}
	}

	[Benchmark(Baseline = true)]
	[BenchmarkCategory("S1: Check Option String Error With Message")]
	public void S1_CheckOptionStringErrorWithMessage_EnumerationsOptions()
	{
		for (var iteration = 0; iteration < Iterations; iteration++)
		{
			if (S1_ReturnOptionStringErrorWithMessage_Impl_EnumerationsOptions() is { Type: OptionType.Some, Value: var error })
			{
				_ = error;
			}
		}
	}

	private Void S1_ReturnVoidSuccess_Impl_EnumerationsOptions() => Void();

	private Void S1_ReturnVoidErrorNoMessage_Impl_EnumerationsOptions() => VoidError();

	private Void S1_ReturnVoidErrorWithMessage_Impl_EnumerationsOptions() => VoidError(ErrorMessage);

	private Option<string> S1_ReturnOptionStringSuccess_Impl_EnumerationsOptions() => SuccessMessage;

	private Option<string> S1_ReturnOptionStringErrorNoMessage_Impl_EnumerationsOptions() => OptionError<string>();

	private Option<string> S1_ReturnOptionStringErrorWithMessage_Impl_EnumerationsOptions() => OptionError<string>(ErrorMessage);

	private Option<string> S1_ReturnOptionStringNone_Impl_EnumerationsOptions() => null;
}
