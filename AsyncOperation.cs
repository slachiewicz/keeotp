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

        public AsyncOperation(Action asyncAction, Action continuation, Action<Exception> catchAction, Action finallyAction)
        {
            context = SynchronizationContext.Current;

            this.asyncAction = asyncAction;
            this.continuation = continuation;
            this.catchAction = catchAction;
            this.finallyAction = finallyAction;
        }

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

            // grab a thread pool thread and run
            runAction.BeginInvoke(null, null);
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
                        context.Post(state => finallyAction(), null);
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
                        context.Post(state => catchAction(e), null);
                    }
                }
            }
        }
    }
}
