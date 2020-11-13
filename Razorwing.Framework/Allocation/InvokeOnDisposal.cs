using System;

namespace Razorwing.Framework.Allocation
{
    /// <summary>
    /// Instances of this class capture an action for later cleanup. When a method returns an instance of this class, the appropriate usage is:
    /// <code>using (SomeMethod())
    /// {
    ///     // ...
    /// }</code>
    /// The using block will automatically dispose the returned instance, doing the necessary cleanup work.
    /// </summary>
    public class InvokeOnDisposal : IDisposable
    {
        private readonly Action action;

        /// <summary>
        /// Constructs a new instance, capturing the given action to be run during disposal.
        /// </summary>
        /// <param name="action">The action to invoke during disposal.</param>
        public InvokeOnDisposal(Action action) => this.action = action ?? throw new ArgumentNullException(nameof(action));

        #region IDisposable Support

        /// <summary>
        /// Disposes this instance, calling the initially captured action.
        /// </summary>
        public void Dispose()
        {
            action();
        }

        #endregion
    }
}
