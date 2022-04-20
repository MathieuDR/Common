using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using MathieuDR.Common.Semaphores;
using Xunit;
using Xunit.Abstractions;

namespace MathieuDR.Common.tests.Semaphores;

public class TimespanSemaphoreTests {
	private readonly ITestOutputHelper _logger;

	public TimespanSemaphoreTests(ITestOutputHelper logger) {
		_logger = logger;
	}
	
	// See blog post for detailed explanation of tests:
	// http://joelfillmore.com/throttling-web-api-calls/

	// helper method to simulate work on the thread
	private static void DoWork(int taskId, ITestOutputHelper logger) {
		var started = DateTime.Now;
		Thread.Sleep(300); // simulate work
		logger.WriteLine(
			"Task {0} started {1}, completed {2}",
			taskId,
			started.ToString("ss.fff"),
			DateTime.Now.ToString("ss.fff"));
	}

	[Fact(Skip = "This test checks the behavior of the semaphore, but it is not a unit test as it relies on the log")]
	public async Task RunAsync_ShouldWaitInbetweenRequests_WhenThereIsNoOutput() {
		using var throttle = new TimeSpanSemaphore(5, TimeSpan.FromSeconds(1));

		var tasks = new Task[10];
		for (int i = 0; i < 10; i++) {
			var taskIndex = i;
			tasks[i] = throttle.RunAsync(async () => {
				var started = DateTime.Now;
				await Task.Delay(300);
				_logger.WriteLine($"Task {taskIndex} started at {started.ToString("ss.fff")}, completed at {DateTime.Now.ToString("ss.fff")}");
			});
		}

		await Task.WhenAll(tasks);
		
		// NOTE: view the console output in Test Explorer
	}
	
	[Fact]
	public async Task RunAsync_ShouldWaitInbetweenRequests_WhenThereIsAnOutput() {
		var callChunks = 5;
		var throttleAmount = 5;
		var calls = callChunks * throttleAmount;
		
		using var throttle = new TimeSpanSemaphore(throttleAmount, TimeSpan.FromSeconds(1), TimeSpan.FromMilliseconds(1));

		var tasks = new Task<DateTime>[calls];
		for (int i = 0; i < calls; i++) {
			tasks[i] = throttle.RunAsync(() => Task.FromResult(DateTime.UtcNow));
		}

		var dates = await Task.WhenAll(tasks);
		// group datetime by timespan
		var firstCompletion = dates.OrderBy(x => x).First();
		var grouped = dates.GroupBy(d => {
			var diff = d - firstCompletion;
			return diff.Seconds;
		}).ToList();

		
		var listOfCounts =grouped.Select(x => x.ToList().Count).ToList();
		_logger.WriteLine(string.Join(",", listOfCounts));
		
		grouped.Should().HaveCount(calls / throttleAmount, "Should fit all in equal chunks chunks");
		listOfCounts.All(x => x == callChunks).Should().BeTrue();
	}

	[Fact(Skip = "This test checks the behavior of the semaphore, but it is not a unit test as it relies on the log")]
	public void Run_ShouldWaitInbetweenRequests_WhenThereIsNoOutput() {
		using var throttle = new TimeSpanSemaphore(5, TimeSpan.FromSeconds(1), TimeSpan.FromMilliseconds(10));

		for (var i = 1; i <= 6; i++) {
			var t = new Thread(taskId => {
				throttle.Run(
					() => DoWork((int)taskId, _logger),
					CancellationToken.None);
			});
			t.Start(i);
		}

		Thread.Sleep(2000); // give all the threads a chance to finish

		// NOTE: view the console output in Test Explorer
	}
}
