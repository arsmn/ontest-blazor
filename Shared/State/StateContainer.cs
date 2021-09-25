using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace OnTest.Blazor.Shared.State
{
    public abstract class StateContainer
    {
        public event Action OnChange;
        private void NotifyStateChanged() => OnChange?.Invoke();

        protected void SetValue<T>(ref T backingFiled, T value)
        {
            if (EqualityComparer<T>.Default.Equals(backingFiled, value)) return;
            backingFiled = value;
            NotifyStateChanged();
        }
    }
}