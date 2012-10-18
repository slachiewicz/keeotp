using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace KeeOtp
{
    /// <summary>
    /// A helper class to help yout with async operations since we aren't using tasks to avoid any reliance on the TPL
    /// </summary>
    /// <remarks>
    /// This is a crude replacement for tasks in the .net 4.0 TPL.  In order to make this plugin work on machines
    /// that don't have .net 4.0 we are avoiding TPL usage.
    /// </remarks>
    class AsyncOperation
    {
        readonly SynchronizationContext context;
        readonly Action asyncAction;
        readonly Action continuation;
        readonly Action<Exception> catchAction;
        readonly Action finallyAction;

        readonly object postSync = new object();

        bool catchPosted = false;
        bool finallyPosted = false;

        /// <summary>
        /// Create an instance of Async Operation.
        /// </summary>
        /// <remarks>
        /// This must be done on the UI thread if continuations and error handling are to also occur on the UI thread.
        /// </remarks>
        /// <param name="asyncAction">The action to run in the background</param>
        /// <param name="continuation">The continuation to run agains the current synchronization context</param>
        /// <param name="catchAction">Error handling to run agains the current synchronization context</param>
        /// <param name="finallyAction">Finally to run agains the current synchronization context</param>
        public AsyncOperation(Action asyncAction, Action continuation, Action<Exception> catchAction, Action finallyAction)
        {
            context = SynchronizationContext.Current;

            this.asyncAction = asyncAction;
            this.continuation = continuation;
            this.catchAction = catchAction;
            this.finallyAction = finallyAction;
        }

        /// <summary>
        /// Run the async action delegate provided upon construction and post any continuations to the synchronization context that was avilable at construction
        /// </summary>
        public void Run()
        {
            Action runAction = () =>
            {
                try
                {
                    if (asyncAction != null)
                        asyncAction();
                    if (continuation != null)
                    {
                        context.Post(state =>
                        {
                            try
                            {
                                continuation();
                            }
                            catch (Exception e)
                            {
                                PostCatchActionIfNeeded(e);
                            }
                            finally
                            {
                                PostFinallyActionIfNeeded();
                            }
                        }, null);
                    }
                }
                catch (Exception e)
                {
                    PostCatchActionIfNeeded(e);
                }
                finally
                {
                    PostFinallyActionIfNeeded();
                }
            };

            ThreadPool.QueueUserWorkItem(state => runAction());
        }

        private void PostFinallyActionIfNeeded()
        {
            if (finallyAction != null)
            {
                lock (postSync)
                {
                    if (!finallyPosted)
                    {
                        finallyPosted = true;
                        if (context != null)
                            context.Post(state => finallyAction(), null);
                        else
                            ThreadPool.QueueUserWorkItem(state => finallyAction());
                    }
                }
            }
        }

        private void PostCatchActionIfNeeded(Exception e)
        {
            if (catchAction != null)
            {
                lock (postSync)
                {
                    if (!catchPosted)
                    {
                        catchPosted = true;
                        if (context != null)
                            context.Post(state => catchAction(e), null);
                        else
                            ThreadPool.QueueUserWorkItem(state => catchAction(e));
                    }
                }
            }
        }
    }
}