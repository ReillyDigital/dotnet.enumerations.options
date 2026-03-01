using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;

using ReillyDigital.Enumerations.Options.Benchmarks.Scenarios;

BenchmarkRunner.Run<Scenario1>(
	ManualConfig
		.Create(DefaultConfig.Instance)
		.WithOptions(ConfigOptions.DisableOptimizationsValidator)
);
