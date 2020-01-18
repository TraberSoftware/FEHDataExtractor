using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FEH_Data_Downloader {
    public class Threadify<T> {
        private ConcurrentDictionary<int, Task> __Stack;
        private BlockingCollection<T> __StackItems;
        private object __StackItemsLocker = new { };
        private Action<int> __ThreadInitializationCallback;

        public Threadify(List<T> items) {
            this.__InitializeStack(items);
        }

        public Threadify() {
            this.__InitializeStack();
        }

        #region Public functions
        /// <summary>
        /// Yielded yarn of threads, this is to create an empty queue to be filled and handled later
        /// </summary>
        /// <param name="callback"></param>
        /// <param name="numThreads"></param>
        public void Yarn(Action<T> callback, int numThreads = 2) {
            this.__Yarn(callback, numThreads, true);
        }

        public void Yarn(List<T> items, Action<T> callback, int numThreads = 2, bool Yielded = false) {
            this.__InitializeStack(items);

            this.__Yarn(callback, numThreads, Yielded);
        }

        public void Yarn(T[] items, Action<T> callback, int numThreads = 2, bool Yielded = false) {
            this.__InitializeStack(items);

            this.__Yarn(callback, numThreads, Yielded);
        }

        public void CompleteYieldAdd() {
            this.__StackItems.CompleteAdding();
        }

        public void Enqueue(T item) {
            this.__StackItems.Add(item);
        }

        public int StackSize() {
            int items = 0;
            lock (__StackItemsLocker) {
                items = this.__StackItems.Count;
            }

            return items;
        }

        public void OnThreadInitialize(Action<int> callback) {
            this.__ThreadInitializationCallback = callback;
        }
        #endregion

        #region Private functions
        private void __InitializeStack() {
            this.__InitializeStack(new List<T>());
        }

        private void __InitializeStack(List<T> items) {
            this.__Stack = new ConcurrentDictionary<int, Task>();
            this.__StackItems = new BlockingCollection<T>(new ConcurrentQueue<T>(items));
        }

        private void __InitializeStack(T[] items) {
            this.__InitializeStack(new List<T>(items));
        }

        private bool __AllThreadsDead(ConcurrentDictionary<int, Task> ThreadStack) {
            bool dead = true;
            foreach (KeyValuePair<int, Task> taskData in ThreadStack) {
                bool taskDead =
                    (taskData.Value.Status == TaskStatus.RanToCompletion)
                    ||
                    (taskData.Value.Status == TaskStatus.Faulted)
                    ||
                    (taskData.Value.Status == TaskStatus.Canceled);
                dead = dead && taskDead;
            }

            return dead;
        }
        #endregion

        #region Yarn
        private void __Yarn(Action<T> callback, int numThreads = 2, bool Yielded = false) {
            bool ThreadsReady = false;

            if (!Yielded) {
                this.__StackItems.CompleteAdding();

                if (numThreads > this.__StackItems.Count) {
                    numThreads = this.__StackItems.Count;
                }
            }

            // No data to process or no threads set
            if(numThreads == 0) {
                return;
            }

            CountdownEvent ThreadWaiter = new CountdownEvent(numThreads);
            for (int i = 0; i < numThreads; i++) {
                this.__Stack.TryAdd(
                    i,
                    new Task(() => {
                        while (!ThreadsReady) {
                            // Wait for all threads to spawn so we don't have a thread consuming high CPU
                            // and denying the others to spawn
                            Thread.Sleep(50);
                        }

                        Thread thread = Thread.CurrentThread;
                        int threadId = 0;
                        if (thread.IsThreadPoolThread) {
                            threadId = thread.ManagedThreadId;
                        }

                        if (this.__ThreadInitializationCallback != null) {
                            this.__ThreadInitializationCallback.Invoke(threadId);
                        }

                        bool completed = this.__StackItems.IsCompleted;
                        while (!completed) {
                            T item;

                            try {
                                lock (__StackItemsLocker) {
                                    item = this.__StackItems.Take();
                                }
                                callback.Invoke(item);

                                completed = this.__StackItems.IsCompleted;
                            }
                            catch (Exception e) {
                                // Exception happened while retrieving items from the Queue
                                if (
                                    e is ObjectDisposedException 
                                    || 
                                    e is InvalidOperationException
                                ) {
                                    completed = true;
                                }
                                // An inner exception occured within the callback, damnit!
                                else {
                                    Logger.LogToFile(
                                        e.ToString(),
                                        Logger.LOG_LEVEL.ERROR
                                    );
                                }
                            }
                        }

                        ThreadWaiter.Signal();
                    })
                );
            }
            foreach (KeyValuePair<int, Task> workerStackItem in this.__Stack) {
                workerStackItem.Value.Start();
            }
            ThreadsReady = true;

            ThreadWaiter.Wait();

            // Even if ThreadWaiter has finished, the threads may be alive somehow, just wait a bit 
            while (!this.__AllThreadsDead(this.__Stack)) {
                Thread.Sleep(50);
            }

            // Finally, dispose the threads (memory leaks yay!)
            foreach (KeyValuePair<int, Task> workerStackItem in this.__Stack) {
                workerStackItem.Value.Dispose();
            }
        }
        #endregion
    }
}
