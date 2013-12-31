using System;
using System.Collections.Generic;

namespace NMahjong.Aux
{
    public class DisposableContainer : IDisposable
    {
        private List<IDisposable> mDisposables;

        public DisposableContainer()
        {
            mDisposables = new List<IDisposable>();
        }

        ~DisposableContainer()
        {
            Dispose(false);
        }

        public T Add<T>(T item) where T : IDisposable
        {
            if (mDisposables == null) {
                throw new ObjectDisposedException(null);
            }
            if (item == null) {
                throw new ArgumentNullException("item");
            }
            mDisposables.Add(item);
            return item;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (mDisposables == null) {
                return;
            }
            if (disposing) mDisposables.ForEach(item => item.Dispose());
            mDisposables = null;
        }
    }
}
