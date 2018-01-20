/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cut.Dialogs
{
    // A reader/writer lock that is compatible with async. Note that this lock is NOT recursive!
    public sealed class AsyncReaderWriterLock
    {
        // Creates a new async-compatible reader/writer lock.
        public AsyncReaderWriterLock();

        // Gets a semi-unique identifier for this asynchronous lock.
        public int Id { get; }

        // Asynchronously acquires the lock as a writer.
        // Returns a disposable that releases the lock when disposed.
        public Task<IDisposable> WriterLockAsync(CancellationToken cancellationToken = new CancellationToken());

        // Asynchronously acquires the lock as a reader.
        // Returns a disposable that releases the lock when disposed.
        public Task<IDisposable> ReaderLockAsync(CancellationToken cancellationToken = new CancellationToken());

        // Asynchronously acquires the lock as a reader with the option to upgrade.
        // Returns a key that can be used to upgrade and downgrade the lock, and releases the lock when disposed.
        public Task<UpgradeableReaderKey> UpgradeableReaderLockAsync(CancellationToken cancellationToken = new CancellationToken());

        // The disposable which manages the upgradeable reader lock.
        public sealed class UpgradeableReaderKey : IDisposable
        {
            // Gets a value indicating whether this lock has been upgraded to a write lock.
            public bool Upgraded { get; }

            // Upgrades the reader lock to a writer lock.
            // Returns a disposable that downgrades the writer lock to a reader lock when disposed.
            public Task<IDisposable> UpgradeAsync(CancellationToken cancellationToken = new CancellationToken());

            // Release the lock.
            public void Dispose();
        }
    }
}*/