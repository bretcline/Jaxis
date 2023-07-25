using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlServerCe;

namespace WS.DataManager
{
    public class LazyTransaction<T> : IList<T>, IDisposable
    {
        private bool _disposed;
        private LazyTransactionEnumerator _enumerator;

        /// <summary>
        /// Initializes a new instance of the <see cref="LazyTransaction{T}"/> class.
        /// </summary>
        /// <param name="dataManager">The data manager.</param>
        /// <param name="transaction">The transaction.</param>
        public LazyTransaction(DataManager dataManager, BaseTransaction<T> transaction)
        {
            _enumerator = new LazyTransactionEnumerator(dataManager, transaction);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LazyTransaction{T}"/> class.
        /// </summary>
        /// <param name="dataManager">The data manager.</param>
        /// <param name="transaction">The transaction.</param>
        /// <param name="blockSize">Size of the block.</param>
        public LazyTransaction(DataManager dataManager, BaseTransaction<T> transaction, int blockSize)
        {
            BlockSize = blockSize;
            _enumerator = new LazyTransactionEnumerator(dataManager, transaction, blockSize);
        }

        /// <summary>
        /// Gets or sets the size of each read block.
        /// </summary>
        /// <value>The size of the block.</value>
        public int BlockSize { get; set; }

        #region IDisposable Members

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);

            // Use SupressFinalize in case a subclass
            // of this type implements a finalizer.
            GC.SuppressFinalize(this);
        }

        #endregion

        #region IList<T> Members

        /// <summary>
        /// Adds an item to the <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </summary>
        /// <param name="item">The object to add to the <see cref="T:System.Collections.Generic.ICollection`1"/>.</param>
        /// <exception cref="T:System.NotSupportedException">
        /// The <see cref="T:System.Collections.Generic.ICollection`1"/> is read-only.
        /// </exception>
        public void Add(T item)
        {
            _enumerator.DataCache.Add(item);
        }

        /// <summary>
        /// Removes all items from the <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </summary>
        /// <exception cref="T:System.NotSupportedException">
        /// The <see cref="T:System.Collections.Generic.ICollection`1"/> is read-only.
        /// </exception>
        public void Clear()
        {
            _enumerator.DataCache.Clear();
            _enumerator.Reset();
        }

        /// <summary>
        /// Closes this instance.
        /// </summary>
        public void Close()
        {
            Dispose();

            // Request GC in order to free connection
            GC.Collect();
        }

        /// <summary>
        /// Determines whether the <see cref="T:System.Collections.Generic.ICollection`1"/> contains a specific value.
        /// </summary>
        /// <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.ICollection`1"/>.</param>
        /// <returns>
        /// true if <paramref name="item"/> is found in the <see cref="T:System.Collections.Generic.ICollection`1"/>; otherwise, false.
        /// </returns>
        public bool Contains(T item)
        {
            return _enumerator.DataCache.Contains(item);
        }

        /// <summary>
        /// Copies the elements of the <see cref="T:System.Collections.Generic.ICollection`1"/> to an <see cref="T:System.Array"/>, starting at a particular <see cref="T:System.Array"/> index.
        /// </summary>
        /// <param name="array">The one-dimensional <see cref="T:System.Array"/> that is the destination of the elements copied from <see cref="T:System.Collections.Generic.ICollection`1"/>. The <see cref="T:System.Array"/> must have zero-based indexing.</param>
        /// <param name="arrayIndex">The zero-based index in <paramref name="array"/> at which copying begins.</param>
        /// <exception cref="T:System.ArgumentNullException">
        /// 	<paramref name="array"/> is null.
        /// </exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        /// 	<paramref name="arrayIndex"/> is less than 0.
        /// </exception>
        /// <exception cref="T:System.ArgumentException">
        /// 	<paramref name="array"/> is multidimensional.
        /// -or-
        /// <paramref name="arrayIndex"/> is equal to or greater than the length of <paramref name="array"/>.
        /// -or-
        /// The number of elements in the source <see cref="T:System.Collections.Generic.ICollection`1"/> is greater than the available space from <paramref name="arrayIndex"/> to the end of the destination <paramref name="array"/>.
        /// -or-
        /// Type <paramref name="T"/> cannot be cast automatically to the type of the destination <paramref name="array"/>.
        /// </exception>
        public void CopyTo(T[] array, int arrayIndex)
        {
            _enumerator.DataCache.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Removes the first occurrence of a specific object from the <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </summary>
        /// <param name="item">The object to remove from the <see cref="T:System.Collections.Generic.ICollection`1"/>.</param>
        /// <returns>
        /// true if <paramref name="item"/> was successfully removed from the <see cref="T:System.Collections.Generic.ICollection`1"/>; otherwise, false. This method also returns false if <paramref name="item"/> is not found in the original <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </returns>
        /// <exception cref="T:System.NotSupportedException">
        /// The <see cref="T:System.Collections.Generic.ICollection`1"/> is read-only.
        /// </exception>
        public bool Remove(T item)
        {
            return _enumerator.DataCache.Remove(item);
        }

        /// <summary>
        /// Gets the number of elements contained in the <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </summary>
        /// <value></value>
        /// <returns>
        /// The number of elements contained in the <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </returns>
        public int Count
        {
            get { return (_enumerator != null) ? _enumerator._count : 0; }
        }

        /// <summary>
        /// Gets a value indicating whether the <see cref="T:System.Collections.Generic.ICollection`1"/> is read-only.
        /// </summary>
        /// <value></value>
        /// <returns>true if the <see cref="T:System.Collections.Generic.ICollection`1"/> is read-only; otherwise, false.
        /// </returns>
        public bool IsReadOnly
        {
            get { return true; }
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<T> GetEnumerator()
        {
            return _enumerator;
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Determines the index of a specific item in the <see cref="T:System.Collections.Generic.IList`1"/>.
        /// </summary>
        /// <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.IList`1"/>.</param>
        /// <returns>
        /// The index of <paramref name="item"/> if found in the list; otherwise, -1.
        /// </returns>
        public int IndexOf(T item)
        {
            return _enumerator.DataCache.IndexOf(item);
        }

        /// <summary>
        /// Inserts an item to the <see cref="T:System.Collections.Generic.IList`1"/> at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index at which <paramref name="item"/> should be inserted.</param>
        /// <param name="item">The object to insert into the <see cref="T:System.Collections.Generic.IList`1"/>.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        /// 	<paramref name="index"/> is not a valid index in the <see cref="T:System.Collections.Generic.IList`1"/>.
        /// </exception>
        /// <exception cref="T:System.NotSupportedException">
        /// The <see cref="T:System.Collections.Generic.IList`1"/> is read-only.
        /// </exception>
        public void Insert(int index, T item)
        {
            _enumerator.DataCache.Insert(index, item);
        }

        /// <summary>
        /// Removes the <see cref="T:System.Collections.Generic.IList`1"/> item at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the item to remove.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        /// 	<paramref name="index"/> is not a valid index in the <see cref="T:System.Collections.Generic.IList`1"/>.
        /// </exception>
        /// <exception cref="T:System.NotSupportedException">
        /// The <see cref="T:System.Collections.Generic.IList`1"/> is read-only.
        /// </exception>
        public void RemoveAt(int index)
        {
            _enumerator.DataCache.RemoveAt(index);
        }

        /// <summary>
        /// Gets or sets the <see cref="T"/> at the specified index.
        /// </summary>
        /// <value></value>
        public T this[int index]
        {
            get
            {
                if (_enumerator.DataCache.Count - 1 < index)
                {
                    // if the item has not been added to cache yet (if it even exists)
                    // then attempt to enumerate up to that item in order to 
                    // ensure it is cached.
                    int i = index - (_enumerator.DataCache.Count - 1);

                    while (i > 0)
                    {
                        if (!_enumerator.MoveNext()) 
							break;
                        if (--i == 0) 
							break;
                    }
                }

                return _enumerator.DataCache[index];
            }
            set { throw new NotImplementedException(); }
        }

        #endregion

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            // If you need thread safety, use a lock around these 
            // operations, as well as in your methods that use the resource.
            if (!_disposed)
            {
                if (disposing)
                {
                    if (_enumerator != null)
                    {
                        _enumerator.Dispose();
                        _enumerator = null;
                    }
                }

                // Indicate that the instance has been disposed.
                _disposed = true;
            }
        }

        #region Nested type: LazyTransactionEnumerator

        internal class LazyTransactionEnumerator : IEnumerator<T>
        {
            private List<T> _dataCache = new List<T>();
            private readonly DataManager _dataManager;
            private readonly BaseTransaction<T> _transaction;
            public int _count;
            private int _currentBlock;
            private int _currentIndex;
            private bool _disposed;
            private bool _eof;
            private IDataReader _transactionDataReader;

            /// <summary>
            /// Initializes a new instance of the <see cref="LazyTransactionEnumerator"/> class.
            /// </summary>
            /// <param name="dataManager">The data manager.</param>
            /// <param name="transaction">The transaction.</param>
            public LazyTransactionEnumerator(DataManager dataManager, BaseTransaction<T> transaction)
                : this(dataManager, transaction, 10)
            {
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="LazyTransactionEnumerator"/> class.
            /// </summary>
            /// <param name="dataManager">The data manager.</param>
            /// <param name="transaction">The transaction.</param>
            /// <param name="blockSize">Size of the block.</param>
            public LazyTransactionEnumerator(DataManager dataManager, BaseTransaction<T> transaction, int blockSize)
            {
                BlockSize = blockSize;
                _dataManager = dataManager;
                _transaction = transaction;

                _count = _dataManager.GetResultCount(_transaction);
                _transaction.Reset();
            }

            /// <summary>
            /// Gets or sets the size of the block.
            /// </summary>
            /// <value>The size of the block.</value>
            public int BlockSize { get; set; }

            /// <summary>
            /// Gets the data cache.
            /// </summary>
            /// <value>The data cache.</value>
            public List<T> DataCache
            {
                get { return _dataCache; }
            }

            /// <summary>
            /// Gets the number of cached records.
            /// </summary>
            /// <value>The number of cached records.</value>
            public int CachedRecords
            {
                get { return _dataCache.Count; }
            }

            #region IEnumerator<T> Members

            /// <summary>
            /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
            /// </summary>
            public void Dispose()
            {
                Dispose(true);

                // Use SupressFinalize in case a subclass
                // of this type implements a finalizer.
                GC.SuppressFinalize(this);
            }

            /// <summary>
            /// Advances the enumerator to the next element of the collection.
            /// </summary>
            /// <returns>
            /// true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.
            /// </returns>
            /// <exception cref="T:System.InvalidOperationException">
            /// The collection was modified after the enumerator was created.
            /// </exception>
            public bool MoveNext()
            {
                try
                {
                    if (_transactionDataReader == null && _dataManager != null)
                        _transactionDataReader = _dataManager.GetDataReader(_transaction, CommandBehavior.CloseConnection);

                    // If we have not reached the end of the reader data and
                    // we haven't yet returned the last result as Current
                    if (!_eof && ((_dataCache.Count - 1) > _currentBlock))
                        return true;

                    int index = 0;

                    // Check for the end of the reader data (false read())
                    _eof = !(_transactionDataReader != null ? ((SqlCeDataReader) _transactionDataReader).Read() : false);

                    // Read up to the BlockSize number of items and add them to cache
                    while (!_eof && index < BlockSize - 1)
                    {
                        if (_eof) return false;

                        _dataCache.Add(_transaction.BuildFromReader(new TransactionReader(_transactionDataReader)));

                        index++;

                        // Check for the end of the reader data (false read())
                        _eof = !(_transactionDataReader != null ? ((SqlCeDataReader) _transactionDataReader).Read() : false);
                    }

                    if(!_eof) _dataCache.Add(_transaction.BuildFromReader(new TransactionReader(_transactionDataReader)));

                    // if we have not yet reached the end of the data cache
                    return _dataCache.Count > _currentBlock;
                }
                finally
                {
                    _currentBlock += BlockSize;
                    _currentIndex++;
                }
            }

            /// <summary>
            /// Sets the enumerator to its initial position, which is before the first element in the collection.
            /// </summary>
            /// <exception cref="T:System.InvalidOperationException">
            /// The collection was modified after the enumerator was created.
            /// </exception>
            public void Reset()
            {
                if (_transactionDataReader != null) _transactionDataReader.Close();

                _transactionDataReader = null;
            }

            /// <summary>
            /// Gets the element in the collection at the current position of the enumerator.
            /// </summary>
            /// <value></value>
            /// <returns>
            /// The element in the collection at the current position of the enumerator.
            /// </returns>
            public T Current
            {
                get { return _dataCache[_currentIndex]; }
            }

            /// <summary>
            /// Gets the element in the collection at the current position of the enumerator.
            /// </summary>
            /// <value></value>
            /// <returns>
            /// The element in the collection at the current position of the enumerator.
            /// </returns>
            object IEnumerator.Current
            {
                get { return Current; }
            }

            #endregion

            /// <summary>
            /// Releases unmanaged and - optionally - managed resources
            /// </summary>
            /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
            protected virtual void Dispose(bool disposing)
            {
                // If you need thread safety, use a lock around these 
                // operations, as well as in your methods that use the resource.
                if (!_disposed)
                {
                    if (disposing)
                    {
                        if (_transactionDataReader == null) return;

                        _transactionDataReader.Close();
                        _transactionDataReader.Dispose();
                        _transactionDataReader = null;

                        if (_dataCache != null)
                        {
                            _dataCache.Clear();
                            _dataCache = null;
                        }
                    }

                    // Indicate that the instance has been disposed.
                    _disposed = true;
                }
            }
        }

        #endregion
    }
}