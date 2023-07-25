using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace LFI.Sync.DataManager
{
    //public class BulkTransactionReader : IDataReader
    //{
    //    private Dictionary<string, int> _ordinals;
    //    private Dictionary<int, string> _nameByOrdinal;
    //    private Dictionary<int, Type> _dataTypeByOrdinal;
        
    //    private int _currentIndex = -1;

    //    private IDictionary<string, object> objects;

    //    private readonly List<ITransaction> _transactions;

    //    private bool _disposed;

    //    /// <summary>
    //    /// Initializes a new instance of the <see cref="BulkTransactionReader"/> class.
    //    /// </summary>
    //    /// <param name="transactions">The transactions.</param>
    //    public BulkTransactionReader(List<ITransaction> transactions)
    //    {
    //        if (transactions.Count == 0) throw new ArgumentException("There must be at least one transaction passed to the BulkTransactionReader", "transactions");
    //        _transactions = transactions;

    //        // Automatically read to the first item
    //        Read();

    //        _currentIndex = -1;

    //        // Reset the reading state and
    //        // indicate that the reader is Open()
    //        IsClosed = false;
    //    }

    //    #region IDataReader Members

    //    /// <summary>
    //    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    //    /// </summary>
    //    public void Dispose()
    //    {
    //        Dispose(true);

    //        // Use SupressFinalize in case a subclass
    //        // of this type implements a finalizer.
    //        GC.SuppressFinalize(this);
    //    }

    //    /// <summary>
    //    /// Releases unmanaged and - optionally - managed resources
    //    /// </summary>
    //    /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
    //    protected virtual void Dispose(bool disposing)
    //    {
    //        // If you need thread safety, use a lock around these 
    //        // operations, as well as in your methods that use the resource.
    //        if (!_disposed)
    //        {
    //            if (disposing)
    //            {
    //                foreach (ITransaction transaction in _transactions)
    //                {
    //                    ((IDisposable)transaction).Dispose();
    //                }

    //                if (objects != null) objects.Clear();
    //                if (_transactions != null) _transactions.Clear();
    //                if (_nameByOrdinal != null) _nameByOrdinal.Clear();
    //                if (_ordinals != null) _ordinals.Clear();
    //            }

    //            // Indicate that the instance has been disposed.
    //            _disposed = true;
    //        }
    //    }

    //    /// <summary>
    //    /// Gets the name for the field to find.
    //    /// </summary>
    //    /// <param name="i">The index of the field to find.</param>
    //    /// <returns>
    //    /// The name of the field or the empty string (""), if there is no value to return.
    //    /// </returns>
    //    /// <exception cref="T:System.IndexOutOfRangeException">
    //    /// The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount"/>.
    //    /// </exception>
    //    public string GetName(int i)
    //    {
    //        if (_dataTypeByOrdinal.Count <= i) throw new IndexOutOfRangeException();
    //        return _nameByOrdinal[i];
    //    }

    //    /// <summary>
    //    /// Gets the data type information for the specified field.
    //    /// </summary>
    //    /// <param name="i">The index of the field to find.</param>
    //    /// <returns>
    //    /// The data type information for the specified field.
    //    /// </returns>
    //    /// <exception cref="T:System.IndexOutOfRangeException">
    //    /// The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount"/>.
    //    /// </exception>
    //    public string GetDataTypeName(int i)
    //    {
    //        if (_dataTypeByOrdinal.Count <= i) throw new IndexOutOfRangeException();
    //        return _dataTypeByOrdinal[i].Name;
    //    }

    //    /// <summary>
    //    /// Gets the <see cref="T:System.Type"/> information corresponding to the type of <see cref="T:System.Object"/> that would be returned from <see cref="M:System.Data.IDataRecord.GetValue(System.Int32)"/>.
    //    /// </summary>
    //    /// <param name="i">The index of the field to find.</param>
    //    /// <returns>
    //    /// The <see cref="T:System.Type"/> information corresponding to the type of <see cref="T:System.Object"/> that would be returned from <see cref="M:System.Data.IDataRecord.GetValue(System.Int32)"/>.
    //    /// </returns>
    //    /// <exception cref="T:System.IndexOutOfRangeException">
    //    /// The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount"/>.
    //    /// </exception>
    //    public Type GetFieldType(int i)
    //    {
    //        if (_dataTypeByOrdinal.Count <= i) throw new IndexOutOfRangeException();
    //        return _dataTypeByOrdinal[i];
    //    }

    //    /// <summary>
    //    /// Return the value of the specified field.
    //    /// </summary>
    //    /// <param name="i">The index of the field to find.</param>
    //    /// <returns>
    //    /// The <see cref="T:System.Object"/> which will contain the field value upon return.
    //    /// </returns>
    //    /// <exception cref="T:System.IndexOutOfRangeException">
    //    /// The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount"/>.
    //    /// </exception>
    //    public object GetValue(int i)
    //    {
    //        if (_dataTypeByOrdinal.Count <= i) throw new IndexOutOfRangeException();
    //        return objects[GetName(i)];
    //    }

    //    /// <summary>
    //    /// Gets all the attribute fields in the collection for the current record.
    //    /// </summary>
    //    /// <param name="values">An array of <see cref="T:System.Object"/> to copy the attribute fields into.</param>
    //    /// <returns>
    //    /// The number of instances of <see cref="T:System.Object"/> in the array.
    //    /// </returns>
    //    public int GetValues(object[] values)
    //    {
    //        for (int i = 0; i < objects.Count; i++)
    //        {
    //            values[i] = GetValue(i);
    //        }

    //        return objects.Count;
    //    }

    //    /// <summary>
    //    /// Return the index of the named field.
    //    /// </summary>
    //    /// <param name="name">The name of the field to find.</param>
    //    /// <returns>The index of the named field.</returns>
    //    public int GetOrdinal(string name)
    //    {
    //        if (_ordinals.ContainsKey(name))
    //        {
    //            return _ordinals[name];
    //        }

    //        throw new ArgumentOutOfRangeException("name");
    //    }

    //    /// <summary>
    //    /// Gets the value of the specified column as a Boolean.
    //    /// </summary>
    //    /// <param name="i">The zero-based column ordinal.</param>
    //    /// <returns>The value of the column.</returns>
    //    /// <exception cref="T:System.IndexOutOfRangeException">
    //    /// The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount"/>.
    //    /// </exception>
    //    public bool GetBoolean(int i)
    //    {
    //        return Convert.ToBoolean(GetValue(i));
    //    }

    //    /// <summary>
    //    /// Gets the 8-bit unsigned integer value of the specified column.
    //    /// </summary>
    //    /// <param name="i">The zero-based column ordinal.</param>
    //    /// <returns>
    //    /// The 8-bit unsigned integer value of the specified column.
    //    /// </returns>
    //    /// <exception cref="T:System.IndexOutOfRangeException">
    //    /// The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount"/>.
    //    /// </exception>
    //    public byte GetByte(int i)
    //    {
    //        return Convert.ToByte(GetValue(i));
    //    }

    //    /// <summary>
    //    /// This method is currently not supported by the BulkTransactionReader
    //    /// </summary>
    //    /// <param name="i">The zero-based column ordinal.</param>
    //    /// <param name="fieldOffset">The index within the field from which to start the read operation.</param>
    //    /// <param name="buffer">The buffer into which to read the stream of bytes.</param>
    //    /// <param name="bufferoffset">The index for <paramref name="buffer"/> to start the read operation.</param>
    //    /// <param name="length">The number of bytes to read.</param>
    //    /// <returns>The actual number of bytes read.</returns>
    //    /// <exception cref="T:System.IndexOutOfRangeException">
    //    /// The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount"/>.
    //    /// </exception>
    //    public long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length)
    //    {
    //        throw new NotSupportedException();
    //    }

    //    /// <summary>
    //    /// Gets the character value of the specified column.
    //    /// </summary>
    //    /// <param name="i">The zero-based column ordinal.</param>
    //    /// <returns>
    //    /// The character value of the specified column.
    //    /// </returns>
    //    /// <exception cref="T:System.IndexOutOfRangeException">
    //    /// The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount"/>.
    //    /// </exception>
    //    public char GetChar(int i)
    //    {
    //        return Convert.ToChar(GetValue(i));
    //    }

    //    /// <summary>
    //    /// This method is currently not supported by the BulkTransactionReader
    //    /// </summary>
    //    /// <param name="i">The zero-based column ordinal.</param>
    //    /// <param name="fieldoffset">The index within the row from which to start the read operation.</param>
    //    /// <param name="buffer">The buffer into which to read the stream of bytes.</param>
    //    /// <param name="bufferoffset">The index for <paramref name="buffer"/> to start the read operation.</param>
    //    /// <param name="length">The number of bytes to read.</param>
    //    /// <returns>The actual number of characters read.</returns>
    //    /// <exception cref="T:System.IndexOutOfRangeException">
    //    /// The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount"/>.
    //    /// </exception>
    //    public long GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length)
    //    {
    //        throw new NotSupportedException();
    //    }

    //    /// <summary>
    //    /// Returns the GUID value of the specified field.
    //    /// </summary>
    //    /// <param name="i">The index of the field to find.</param>
    //    /// <returns>The GUID value of the specified field.</returns>
    //    /// <exception cref="T:System.IndexOutOfRangeException">
    //    /// The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount"/>.
    //    /// </exception>
    //    public Guid GetGuid(int i)
    //    {
    //        return new Guid(GetValue(i).ToString());
    //    }

    //    /// <summary>
    //    /// Gets the 16-bit signed integer value of the specified field.
    //    /// </summary>
    //    /// <param name="i">The index of the field to find.</param>
    //    /// <returns>
    //    /// The 16-bit signed integer value of the specified field.
    //    /// </returns>
    //    /// <exception cref="T:System.IndexOutOfRangeException">
    //    /// The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount"/>.
    //    /// </exception>
    //    public short GetInt16(int i)
    //    {
    //        return Convert.ToInt16(GetValue(i));
    //    }

    //    /// <summary>
    //    /// Gets the 32-bit signed integer value of the specified field.
    //    /// </summary>
    //    /// <param name="i">The index of the field to find.</param>
    //    /// <returns>
    //    /// The 32-bit signed integer value of the specified field.
    //    /// </returns>
    //    /// <exception cref="T:System.IndexOutOfRangeException">
    //    /// The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount"/>.
    //    /// </exception>
    //    public int GetInt32(int i)
    //    {
    //        return Convert.ToInt32(GetValue(i));
    //    }

    //    /// <summary>
    //    /// Gets the 64-bit signed integer value of the specified field.
    //    /// </summary>
    //    /// <param name="i">The index of the field to find.</param>
    //    /// <returns>
    //    /// The 64-bit signed integer value of the specified field.
    //    /// </returns>
    //    /// <exception cref="T:System.IndexOutOfRangeException">
    //    /// The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount"/>.
    //    /// </exception>
    //    public long GetInt64(int i)
    //    {
    //        return Convert.ToInt64(GetValue(i));
    //    }

    //    /// <summary>
    //    /// Gets the single-precision floating point number of the specified field.
    //    /// </summary>
    //    /// <param name="i">The index of the field to find.</param>
    //    /// <returns>
    //    /// The single-precision floating point number of the specified field.
    //    /// </returns>
    //    /// <exception cref="T:System.IndexOutOfRangeException">
    //    /// The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount"/>.
    //    /// </exception>
    //    public float GetFloat(int i)
    //    {
    //        return Convert.ToSingle(GetValue(i));
    //    }

    //    /// <summary>
    //    /// Gets the double-precision floating point number of the specified field.
    //    /// </summary>
    //    /// <param name="i">The index of the field to find.</param>
    //    /// <returns>
    //    /// The double-precision floating point number of the specified field.
    //    /// </returns>
    //    /// <exception cref="T:System.IndexOutOfRangeException">
    //    /// The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount"/>.
    //    /// </exception>
    //    public double GetDouble(int i)
    //    {
    //        return Convert.ToDouble(GetValue(i));
    //    }

    //    /// <summary>
    //    /// Gets the string value of the specified field.
    //    /// </summary>
    //    /// <param name="i">The index of the field to find.</param>
    //    /// <returns>The string value of the specified field.</returns>
    //    /// <exception cref="T:System.IndexOutOfRangeException">
    //    /// The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount"/>.
    //    /// </exception>
    //    public string GetString(int i)
    //    {
    //        return GetValue(i).ToString();
    //    }

    //    /// <summary>
    //    /// Gets the fixed-position numeric value of the specified field.
    //    /// </summary>
    //    /// <param name="i">The index of the field to find.</param>
    //    /// <returns>
    //    /// The fixed-position numeric value of the specified field.
    //    /// </returns>
    //    /// <exception cref="T:System.IndexOutOfRangeException">
    //    /// The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount"/>.
    //    /// </exception>
    //    public decimal GetDecimal(int i)
    //    {
    //        return Convert.ToDecimal(GetValue(i));
    //    }

    //    /// <summary>
    //    /// Gets the date and time data value of the specified field.
    //    /// </summary>
    //    /// <param name="i">The index of the field to find.</param>
    //    /// <returns>
    //    /// The date and time data value of the specified field.
    //    /// </returns>
    //    /// <exception cref="T:System.IndexOutOfRangeException">
    //    /// The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount"/>.
    //    /// </exception>
    //    public DateTime GetDateTime(int i)
    //    {
    //        return Convert.ToDateTime(GetValue(i));
    //    }

    //    /// <summary>
    //    /// Returns an <see cref="T:System.Data.IDataReader"/> for the specified column ordinal.
    //    /// </summary>
    //    /// <param name="i">The index of the field to find.</param>
    //    /// <returns>
    //    /// An <see cref="T:System.Data.IDataReader"/>.
    //    /// </returns>
    //    /// <exception cref="T:System.IndexOutOfRangeException">
    //    /// The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount"/>.
    //    /// </exception>
    //    public IDataReader GetData(int i)
    //    {
    //        return this;
    //    }

    //    /// <summary>
    //    /// Return whether the specified field is set to null.
    //    /// </summary>
    //    /// <param name="i">The index of the field to find.</param>
    //    /// <returns>
    //    /// true if the specified field is set to null; otherwise, false.
    //    /// </returns>
    //    /// <exception cref="T:System.IndexOutOfRangeException">
    //    /// The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount"/>.
    //    /// </exception>
    //    public bool IsDBNull(int i)
    //    {
    //        return GetValue(i) == null;
    //    }

    //    /// <summary>
    //    /// Gets the number of columns in the current row.
    //    /// </summary>
    //    /// <value></value>
    //    /// <returns>
    //    /// When not positioned in a valid recordset, 0; otherwise, the number of columns in the current record. The default is -1.
    //    /// </returns>
    //    public int FieldCount
    //    {
    //        get { return objects.Count; }
    //    }

    //    /// <summary>
    //    /// Gets the <see cref="System.Object"/> with the specified i.
    //    /// </summary>
    //    /// <value></value>
    //    object IDataRecord.this[int i]
    //    {
    //        get
    //        {
    //            return GetValue(i);
    //        }
    //    }

    //    /// <summary>
    //    /// Gets the <see cref="System.Object"/> with the specified name.
    //    /// </summary>
    //    /// <value></value>
    //    object IDataRecord.this[string name]
    //    {
    //        get
    //        {
    //            if (objects.Keys.ToList().BinarySearch(name) != -1)
    //            {
    //                return objects[name];
    //            }

    //            throw new ArgumentOutOfRangeException("name");
    //        }
    //    }

    //    /// <summary>
    //    /// Closes the <see cref="T:System.Data.IDataReader"/> Object.
    //    /// </summary>
    //    public void Close()
    //    {
    //        IsClosed = true;
    //        Dispose();
    //    }

    //    /// <summary>
    //    /// Returns a <see cref="T:System.Data.DataTable"/> that describes the column metadata of the <see cref="T:System.Data.IDataReader"/>.
    //    /// </summary>
    //    /// <returns>
    //    /// A <see cref="T:System.Data.DataTable"/> that describes the column metadata.
    //    /// </returns>
    //    /// <exception cref="T:System.InvalidOperationException">
    //    /// The <see cref="T:System.Data.IDataReader"/> is closed.
    //    /// </exception>
    //    public DataTable GetSchemaTable()
    //    {
    //        return _transactions[0].BuildDataTable();
    //    }

    //    /// <summary>
    //    /// Advances the data reader to the next result, when reading the results of batch SQL statements.
    //    /// </summary>
    //    /// <returns>
    //    /// true if there are more rows; otherwise, false.
    //    /// </returns>
    //    public bool NextResult()
    //    {
    //        return Read();
    //    }

    //    /// <summary>
    //    /// Advances the <see cref="T:System.Data.IDataReader"/> to the next record.
    //    /// </summary>
    //    /// <returns>
    //    /// true if there are more rows; otherwise, false.
    //    /// </returns>
    //    public bool Read()
    //    {
    //        if (IsClosed) throw new Exception("The BulkTransactionReader is closed.");

    //        _currentIndex++;

    //        if (_currentIndex != _transactions.Count)
    //        {
    //            if (_currentIndex - 1 > 0)
    //            {
    //                ((IDisposable)_transactions[_currentIndex - 1]).Dispose();
    //            }

    //            _transactions[_currentIndex].Compile();
    //            objects = _transactions[_currentIndex].GetData();

    //            if (_ordinals == null)
    //            {
    //                _dataTypeByOrdinal = new Dictionary<int, Type>(25);
    //                _ordinals = new Dictionary<string, int>(25);
    //                _nameByOrdinal = new Dictionary<int, string>(25);
    //                int index = 0;
    //                foreach (string columnName in objects.Keys)
    //                {
    //                    _dataTypeByOrdinal.Add(index, objects[columnName].GetType());
    //                    _nameByOrdinal.Add(index, columnName);
    //                    _ordinals.Add(columnName, index++);
    //                }
    //            }

    //            return true;
    //        }

    //        return false;
    //    }

    //    /// <summary>
    //    /// Gets a value indicating the depth of nesting for the current row.
    //    /// </summary>
    //    /// <value></value>
    //    /// <returns>
    //    /// The level of nesting.
    //    /// </returns>
    //    public int Depth
    //    {
    //        get { return 0; }
    //    }

    //    /// <summary>
    //    /// Gets a value indicating whether the data reader is closed.
    //    /// </summary>
    //    /// <value></value>
    //    /// <returns>true if the data reader is closed; otherwise, false.
    //    /// </returns>
    //    public bool IsClosed { get; private set; }

    //    /// <summary>
    //    /// Gets the number of rows changed, inserted, or deleted by execution of the SQL statement.
    //    /// </summary>
    //    /// <value></value>
    //    /// <returns>
    //    /// The number of rows changed, inserted, or deleted; 0 if no rows were affected or the statement failed; and -1 for SELECT statements.
    //    /// </returns>
    //    public int RecordsAffected
    //    {
    //        get { return 0; }
    //    }

    //    #endregion
    //}
}