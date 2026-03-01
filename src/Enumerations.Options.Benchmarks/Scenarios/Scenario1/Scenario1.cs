namespace ReillyDigital.Enumerations.Options.Benchmarks.Scenarios;

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;

using JetBrains.Annotations;

[CategoriesColumn]
[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
[HideColumns(Column.Error, Column.Gen0, Column.Gen1, Column.Gen2, Column.Iterations, Column.Job, Column.Median, Column.RatioSD, Column.StdDev)]
[IterationTime(100)]
[MemoryDiagnoser]
[SimpleJob(RuntimeMoniker.Net10_0)]
public partial class Scenario1
{
	private const string ErrorMessage = "An unknown error occurred.";

	private const string SuccessMessage = "This was a success.";

	[Params(2)]
	public int Iterations { get; [UsedImplicitly] set; }
}
