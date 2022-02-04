using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Common.Semaphores;

// https://github.com/joelfillmore/JFLibrary/blob/master/JFLibrary/TimeSpanSemaphore.cs

/// <summary>
///     Allows a limited number of acquisitions during a timespan
/// </summary>
public class TimeSpanSemaphore : IDisposable {
	private readonly SemaphoreSlim _pool;

	// protect release time queue
	private readonly object _queueLock = new();

	// keep track of the release times
	private readonly Queue<DateTime> _releaseTimes;

	// the time span for the max number of callers
	private readonly TimeSpan _resetSpan;

	public TimeSpanSemaphore(int maxCount, TimeSpan resetSpan, TimeSpan? wiggleRoom = null) {
		_pool = new SemaphoreSlim(maxCount, maxCount);
		if (wiggleRoom is not null) {
			resetSpan += wiggleRoom.Value;
		}
		
		_resetSpan = resetSpan;

		// initialize queue with old timestamps
		_releaseTimes = new Queue<DateTime>(maxCount);
		for (var i = 0; i < maxCount; i++) {
			_releaseTimes.Enqueue(DateTime.MinValue);
		}
	}

	/// <summary>
	///     Releases all resources used by the current instance
	/// </summary>
	public void Dispose() {
		_pool.Dispose();
	}

	/// <summary>
	///     Blocks the current thread until it can enter the semaphore, while observing a CancellationToken
	/// </summary>
	private void Wait(CancellationToken cancelToken) {
		// will throw if token is cancelled
		_pool.Wait(cancelToken);

		// get the oldest release from the queue
		DateTime oldestRelease;
		lock (_queueLock) {
			oldestRelease = _releaseTimes.Dequeue();
		}

		// sleep until the time since the previous release equals the reset period
		var now = DateTime.UtcNow;
		var windowReset = oldestRelease.Add(_resetSpan);
		if (windowReset > now) {
			var sleepMilliseconds = Math.Max(
				(int)(windowReset.Subtract(now).Ticks / TimeSpan.TicksPerMillisecond),
				1); // sleep at least 1ms to be sure next window has started
			Debug.WriteLine("Waiting {0} ms for TimeSpanSemaphore limit to reset.", sleepMilliseconds);

			var cancelled = cancelToken.WaitHandle.WaitOne(sleepMilliseconds);
			if (cancelled) {
				Release();
				cancelToken.ThrowIfCancellationRequested();
			}
		}
	}

	/// <summary>
	///     Exits the semaphore
	/// </summary>
	private void Release() {
		lock (_queueLock) {
			_releaseTimes.Enqueue(DateTime.UtcNow);
		}

		_pool.Release();
	}

	/// <summary>
	///     Runs an action after entering the semaphore (if the CancellationToken is not canceled)
	/// </summary>
	public void Run(Action action, CancellationToken cancelToken = default) {
		// will throw if token is cancelled, but will auto-release lock
		Wait(cancelToken);

		try {
			action();
		}
		finally {
			Release();
		}
	}

	// https://github.com/saguiitay/OneDriveRestAPI/blob/master/src/OneDriveRestAPI/Util/TimeSpanSemaphore.cs
	/// <summary>
	///     Runs an action after entering the semaphore (if the CancellationToken is not canceled)
	/// </summary>
	public async Task RunAsync(Func<Task> action, CancellationToken cancelToken = default) {
		// will throw if token is cancelled, but will auto-release lock
		Wait(cancelToken);

		try {
			await action().ConfigureAwait(false);
		}
		finally {
			Release();
		}
	}
	
	// https://github.com/saguiitay/OneDriveRestAPI/blob/master/src/OneDriveRestAPI/Util/TimeSpanSemaphore.cs
	/// <summary>
	///     Runs an action after entering the semaphore (if the CancellationToken is not canceled)
	/// </summary>
	public async Task<T> RunAsync<T>(Func<Task<T>> action, CancellationToken cancelToken = default) {
		// will throw if token is cancelled, but will auto-release lock
		Wait(cancelToken);

		try {
			return await action().ConfigureAwait(false);
		}
		finally {
			Release();
		}
	}

	/// <summary>
	///     Runs an action after entering the semaphore (if the CancellationToken is not canceled)
	/// </summary>
	public async Task RunAsync<T>(Func<T, Task> action, T arg, CancellationToken cancelToken = default) {
		// will throw if token is cancelled, but will auto-release lock
		Wait(cancelToken);

		try {
			await action(arg).ConfigureAwait(false);
		}
		finally {
			Release();
		}
	}

	/// <summary>
	///     Runs an action after entering the semaphore (if the CancellationToken is not canceled)
	/// </summary>
	public async Task<TR> RunAsync<T, TR>(Func<T, CancellationToken, Task<TR>> action, T arg, CancellationToken cancelToken = default) {
		// will throw if token is cancelled, but will auto-release lock
		Wait(cancelToken);

		try {
			return await action(arg, cancelToken).ConfigureAwait(false);
		}
		finally {
			Release();
		}
	}
}
