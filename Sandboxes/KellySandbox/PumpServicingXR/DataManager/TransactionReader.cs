using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace LFI.Sync.DataManager
{
	public class TransactionReader: IDisposable
	{
		public static readonly Guid ErrorGuid;
		private readonly IDataReader _reader;
		private readonly List<string> _errors;
		private readonly int? _timeZoneOffset = null;

		//----------------------------------------------------------------------
		static TransactionReader()
        {
            ErrorGuid = new Guid("99999999-9999-9999-9999-999999999999");
        }

		//----------------------------------------------------------------------
		public TransactionReader(IDataReader reader)
		{
			_reader = reader;
			_errors = new List<string>();
		}

		//----------------------------------------------------------------------
		/// <summary>
		/// Wraps an IDataReader and adds functionality
		/// </summary>
		/// <param name="reader"></param>
		/// <param name="timeZoneOffset">Timezone offset of data-source from application local time. This is used to convert dates to application time as they are read from the datasource into the application.</param>
		public TransactionReader(IDataReader reader, int timeZoneOffset)
		{
			if (timeZoneOffset != 0)
				_timeZoneOffset = timeZoneOffset;
			_reader = reader;
			_errors = new List<string>();
		}

		//----------------------------------------------------------------------
		public void Dispose()
		{
			_reader.Dispose();
		}

		//----------------------------------------------------------------------
		public bool ReadNextRow()
		{
			return _reader.Read();
		}

		//----------------------------------------------------------------------
		public int GetColumnCount()
		{
			return _reader.FieldCount;
		}

		//----------------------------------------------------------------------
		public string GetColumnName(int columnIndex)
		{
			return _reader.GetName(columnIndex);
		}

		//----------------------------------------------------------------------
		public object GetColumnValue(int columnIndex)
		{
			return _reader[columnIndex];
		}

		//----------------------------------------------------------------------
		public Dictionary<string, object> GetCurrentRow()
		{
			Dictionary<string, object> outRow = new Dictionary<string, object>();
			for (int i = 0; i < _reader.FieldCount; ++i)
			{
				outRow.Add(_reader.GetName(i), _reader[i]);
			}

			return outRow;
		}

		//----------------------------------------------------------------------
		public string ReadStringWithNull(string name)
		{
			if (_reader[name] == DBNull.Value)
				return null;

            return ToXmlString(((string)_reader[name]).TrimEnd()).Replace("\n", "\r\n");
		}

		//----------------------------------------------------------------------
		public string TryReadStringWithNull(string name)
		{
			try
			{
				return ReadStringWithNull(name);
			}
			catch (Exception ex)
			{
				_errors.Add(String.Format("Failed to read string for '{0}'. Returning default: '{1}' with {2}.", name, String.Empty, DataManager.BuildExceptionString(ex)));
				return null;
			}
		}

		//----------------------------------------------------------------------
		public string TryReadGuidAsStringWithNull(string name)
		{
			try
			{
				Guid? outGuid = ReadGuidWithNull(name);
				return outGuid == null ? null : outGuid.Value.ToString();
			}
			catch (Exception ex)
			{
				_errors.Add(String.Format("Failed to read string for '{0}'. Returning default: '{1}' with {2}.", name, String.Empty, DataManager.BuildExceptionString(ex)));
				return null;
			}
		}

		static readonly UTF8Encoding _utf8Encoder = new UTF8Encoding();
		static readonly ASCIIEncoding _ASCIIEncoder = new ASCIIEncoding();

		//----------------------------------------------------------------------
		/// <summary>
		/// Verifies that a string is a valid XML content string and
		/// formats it if not.
		/// </summary>
		/// <param name="s">The string.</param>
		/// <returns></returns>
		public static string ToXmlString(string s)
		{
#if !PocketPC
			// Convert old ASCII data into unicode format (some of the old DB data appears to be extended ASCII)
			// one character in particular causes a serialization problem when a web request is processed '\f'
			return _utf8Encoder.GetString(_ASCIIEncoder.GetBytes(s)).Replace('\f', ' ');
#else
            return s;
#endif
		}

		//----------------------------------------------------------------------
		public object ReadObject(string name)
		{
			if (_reader[name] == DBNull.Value)
				return null;

			return _reader[name];
		}

		//----------------------------------------------------------------------
		public object TryReadObject(string name)
		{
			try
			{
				return ReadObject(name);
			}
			catch (Exception ex)
			{
				_errors.Add(String.Format("Failed to read object for '{0}'. Returning default: 'null' with {1}.", name, DataManager.BuildExceptionString(ex)));
				return null;
			}
		}

		//----------------------------------------------------------------------
		public string ReadString(string name)
		{
			if (_reader[name] == DBNull.Value)
				return String.Empty;

			return ToXmlString(((string)_reader[name]).TrimEnd()).Replace("\n", "\r\n");
		}

        //----------------------------------------------------------------------
        public string TryReadstring( string name )
        {
            return TryReadString( name );
        }

		//----------------------------------------------------------------------
		public string TryReadString(string name)
		{
			try
			{
				return ReadString(name);
			}
			catch (Exception ex)
			{
				_errors.Add(String.Format("Failed to read string for '{0}'. Returning default: '{1}' with {2}.", name, String.Empty, DataManager.BuildExceptionString(ex)));
				return String.Empty;
			}
		}

		//----------------------------------------------------------------------
		public string TryReadGuidAsString(string name)
		{
			try
			{
				return ReadGuid(name).ToString();
			}
			catch (Exception ex)
			{
				_errors.Add(String.Format("Failed to read string for '{0}'. Returning default: '{1}' with {2}.", name, String.Empty, DataManager.BuildExceptionString(ex)));
				return String.Empty;
			}
		}

		//----------------------------------------------------------------------
		public string TryReadString(int index)
		{
			try
			{
                return ToXmlString(_reader[0].ToString()).Replace("\n", "\r\n");
			}
			catch (Exception ex)
			{
				_errors.Add(String.Format("Failed to read string by reader index: '{0}'. Returning default: '{1}' with {2}", index, String.Empty, DataManager.BuildExceptionString(ex)));
				return String.Empty;
			}
		}

		//----------------------------------------------------------------------
		public Guid ReadGuid(string name)
		{
			if (_reader[name] == DBNull.Value)
			{
				_errors.Add(String.Format("NULL value read for '{0}'. Returning default: '{1}'.", name, ErrorGuid));
				return ErrorGuid;
			}

			Guid? guid = _reader[name] as Guid?;

			return guid == null ? new Guid(_reader[name].ToString()) : (Guid)_reader[name];
		}

		//----------------------------------------------------------------------
		public Guid TryReadGuid(string name)
		{
			try
			{
				return ReadGuid(name);
			}
			catch (Exception ex)
			{
				_errors.Add(String.Format("Failed to read Guid for '{0}'. Returning default: '{1}' with {2}.", name, ErrorGuid, DataManager.BuildExceptionString(ex)));
				return ErrorGuid;
			}
		}

		//----------------------------------------------------------------------
		public Guid? ReadGuidWithNull(string name)
		{
			if (_reader[name] == DBNull.Value)
				return null;

			return (Guid?)_reader[name];
		}

		//----------------------------------------------------------------------
		public Guid? TryReadGuidWithNull(string name)
		{
			try
			{
				return ReadGuidWithNull(name);
			}
			catch (Exception ex)
			{
				_errors.Add(String.Format("Failed to read Guid for '{0}'. Returning default: '{1}' with {2}.", name, ErrorGuid, DataManager.BuildExceptionString(ex)));
				return ErrorGuid;
			}
		}

		//----------------------------------------------------------------------
		public DateTime ReadDate(string name)
		{
			if (_reader[name] == DBNull.Value)
			{
				_errors.Add(String.Format("NULL value read for '{0}'. Returning default: '{1}'.", name, DataManager.KMinDateTime));
				return DataManager.KMinDateTime;
			}

			DateTime date = (DateTime)_reader[name];
			if (_timeZoneOffset != null)
				date -= TimeSpan.FromHours(_timeZoneOffset.Value);

			return date;
		}

        //----------------------------------------------------------------------
        public DateTime TryReadDateTime( string name )
        {
            return TryReadDate( name );
        }

		//----------------------------------------------------------------------
		public DateTime TryReadDate(string name)
		{
			try
			{
				return ReadDate(name);
			}
			catch (Exception ex)
			{
				_errors.Add(String.Format("Failed to read date for '{0}'. Returning default: '{1}' with {2}.", name, DataManager.KMinDateTime, DataManager.BuildExceptionString(ex)));
				return DataManager.KMinDateTime;
			}
		}

		//----------------------------------------------------------------------
		public DateTime? ReadDateWithNull(string name)
		{
			if (_reader[name] == DBNull.Value)
				return null;

			DateTime? date = (DateTime?)_reader[name];
			if (_timeZoneOffset != null)
				date -= TimeSpan.FromHours(_timeZoneOffset.Value);

			return date;
		}

		//----------------------------------------------------------------------
		public DateTime? TryReadDateWithNull(string name)
		{
			try
			{
				return ReadDateWithNull(name);
			}
			catch (Exception ex)
			{
				_errors.Add(String.Format("Failed to read date for '{0}'. Returning null with {1}.", name, DataManager.BuildExceptionString(ex)));
				return null;
			}
		}

		//----------------------------------------------------------------------
		public TimeSpan ReadTimeSpan(string name)
		{
			if (_reader[name] == DBNull.Value)
			{
				_errors.Add(String.Format("NULL value read for '{0}'. Returning default: '{1}'.", name, TimeSpan.MinValue));
				return TimeSpan.MinValue;
			}

			double? hours = _reader[name] as double?;
			hours = hours == null ? double.Parse(_reader[name].ToString()) : (double)_reader[name];

			return TimeSpan.FromHours(hours.Value);
		}

		//----------------------------------------------------------------------
		public TimeSpan TryReadTimeSpan(string name)
		{
			try
			{
				return ReadTimeSpan(name);
			}
			catch (Exception ex)
			{
				_errors.Add(String.Format("Failed to read TimeSpace for '{0}'. Returning default: '{1}' with {2}", name, TimeSpan.MinValue, DataManager.BuildExceptionString(ex)));
				return TimeSpan.MinValue;
			}
		}

		//----------------------------------------------------------------------
		public float ReadFloat(string name)
		{
			if (_reader[name] == DBNull.Value)
			{
				_errors.Add(String.Format("NULL value read for '{0}'. Returning default: '{1}'.", name, "0.0f"));
				return 0.0f;
			}

			float? value = _reader[name] as float?;
			return value == null ? Convert.ToSingle(_reader[name].ToString()) : (float)_reader[name];
		}

		//----------------------------------------------------------------------
		public float TryReadFloat(string name)
		{
			try
			{
				return ReadFloat(name);
			}
			catch (Exception ex)
			{
				_errors.Add(String.Format("Failed to read float for '{0}'. Return default: '{1}' with {2}", name, "0.0f", DataManager.BuildExceptionString(ex)));
				return 0.0f;
			}
		}

		//----------------------------------------------------------------------
		public float? ReadFloatWithNull(string name)
		{
			if (_reader[name] == DBNull.Value)
			{
				_errors.Add(String.Format("NULL value read for '{0}'. Returning default: '{1}'.", name, "0.0f"));
				return null;
			}

			return Convert.ToSingle(_reader[name]);
		}

		//----------------------------------------------------------------------
		public float? TryReadFloatWithNull(string name)
		{
			try
			{
				return ReadFloatWithNull(name);
			}
			catch (Exception ex)
			{
				_errors.Add(String.Format("Failed to read float for '{0}'. Return default: '{1}' with {2}.", name, "0.0f", DataManager.BuildExceptionString(ex)));
				return null;
			}
		}

		//----------------------------------------------------------------------
		public double ReadDouble(string name)
		{
			if (_reader[name] == DBNull.Value)
			{
				_errors.Add(String.Format("NULL value read for '{0}'. Default value '{1}' returned.", name, "0.0d"));
				return 0.0d;
			}

			double? value = _reader[name] as double?;
			return value == null ? Convert.ToDouble(_reader[name].ToString()) : (double)_reader[name];
		}

		//----------------------------------------------------------------------
		public double TryReadDouble(string name)
		{
			try
			{
				return ReadDouble(name);
			}
			catch (Exception ex)
			{
				_errors.Add(String.Format("Failed to read double for '{0}'. Returning default: '{1}' wiith {2}.", name, "0.0d", DataManager.BuildExceptionString(ex)));
				return 0.0d;
			}
		}

		//----------------------------------------------------------------------
		public double? ReadDoubleWithNull(string name)
		{
			if (_reader[name] == DBNull.Value)
				return null;

			return Convert.ToDouble(_reader[name]);
		}

		//----------------------------------------------------------------------
		public double? TryReadDoubleWithNull(string name)
		{
			try
			{
				return ReadDoubleWithNull(name);
			}
			catch (Exception ex)
			{
				_errors.Add(string.Format("Failed to read double for '{0}'. Returning default: null with {1}.", name, DataManager.BuildExceptionString(ex)));
				return null;
			}
		}

        //----------------------------------------------------------------------
        public decimal ReadDecimal( string name )
        {
            if( _reader[ name ] == DBNull.Value )
            {
                _errors.Add( String.Format( "NULL value read for '{0}'. Default value '{1}' returned.", name, "0.0M" ) );
                return 0.0M;
            }

            decimal? value = _reader[ name ] as decimal?;
            return value == null ? Convert.ToDecimal( _reader[ name ].ToString( ) ) : ( decimal ) _reader[ name ];
        }

        //----------------------------------------------------------------------
        public decimal TryReaddecimal( string name )
        {
            return TryReadDecimal( name );
        }

        //----------------------------------------------------------------------
        public decimal TryReadDecimal( string name )
        {
            try
            {
                return ReadDecimal( name );
            }
            catch( Exception ex )
            {
                _errors.Add( String.Format( "Failed to read decimal for '{0}'. Returning default: '{1}' with {2}.", name, "0.0M", DataManager.BuildExceptionString( ex ) ) );
                return 0.0M;
            }
        }

        //----------------------------------------------------------------------
        public decimal? ReadDecimalWithNull( string name )
        {
            if( _reader[ name ] == DBNull.Value )
                return null;

            return Convert.ToDecimal( _reader[ name ] );
        }

        //----------------------------------------------------------------------
        public decimal? TryReadDecimalWithNull( string name )
        {
            try
            {
                return ReadDecimalWithNull( name );
            }
            catch( Exception ex )
            {
                _errors.Add( string.Format( "Failed to read decimal for '{0}'. Returning default: null with {1}.", name, DataManager.BuildExceptionString( ex ) ) );
                return null;
            }
        }

        //----------------------------------------------------------------------
		public bool ReadBool(string name)
		{
			if (_reader[name] == DBNull.Value)
				return false;

			bool? value = _reader[name] as bool?;
			return value == null ? Convert.ToBoolean(_reader[name].ToString()) : (bool)_reader[name];
		}

        //----------------------------------------------------------------------
        public bool TryReadbool( string name )
        {
            return TryReadBool( name );
        }

		//----------------------------------------------------------------------
		public bool TryReadBool(string name)
		{
			try
			{
				return ReadBool(name);
			}
			catch (Exception ex)
			{
				_errors.Add(String.Format("Failed to read bool for '{0}'. Returning default: '{1}' with {2}.", name, "false", DataManager.BuildExceptionString(ex)));
				return false;
			}
		}

		//----------------------------------------------------------------------
		public char ReadChar(string name)
		{
			if (_reader[name] == DBNull.Value)
			{
				_errors.Add(String.Format("NULL value read for '{0}'. Default value '{1}' returned.", name, "0"));
				return '0';
			}

			return (char)_reader[name];
		}

		//----------------------------------------------------------------------
		public char TryReadChar(string name)
		{
			try
			{
				return ReadChar(name);
			}
			catch (Exception ex)
			{
				_errors.Add(String.Format("Failed to read char for '{0}'. Return default: '{1}' with {2}.", name, "0", DataManager.BuildExceptionString(ex)));
				return '0';
			}
		}

		//----------------------------------------------------------------------
		public int ReadInt(string name)
		{
			if (_reader[name] == DBNull.Value)
			{
				_errors.Add(String.Format("NULL value read for '{0}'. Default value '{1}' returned.", name, "0"));
				return 0;
			}

			int? value = _reader[name] as int?;
			return value == null ? Convert.ToInt32(_reader[name].ToString()) : (int)_reader[name];
		}

        //----------------------------------------------------------------------
        public int TryReadint( string name )
        {
            return TryReadInt( name );
        }

		//----------------------------------------------------------------------
		public int TryReadInt(string name)
		{
			try
			{
				return ReadInt(name);
			}
			catch (Exception ex)
			{
				_errors.Add(String.Format("Failed to read int for '{0}'. Returning default: '{1}' with {2}.", name, "0", DataManager.BuildExceptionString(ex)));
				return 0;
			}
		}

        //----------------------------------------------------------------------
        public long ReadLong( string name )
        {
            if( _reader[ name ] == DBNull.Value )
            {
                _errors.Add( String.Format( "NULL value read for '{0}'. Default value '{1}' returned.", name, "0" ) );
                return 0;
            }

            long? value = _reader[ name ] as long?;
            return value == null ? Convert.ToInt64( _reader[ name ].ToString( ) ) : ( long ) _reader[ name ];
        }

        //----------------------------------------------------------------------
        public long TryReadlong( string name )
        {
            return TryReadLong( name );
        }

        //----------------------------------------------------------------------
        public long TryReadLong( string name )
        {
            try
            {
                return ReadLong( name );
            }
            catch( Exception ex )
            {
                _errors.Add( String.Format( "Failed to read long for '{0}'. Returning default: '{1}' with {2}.", name, "0", DataManager.BuildExceptionString( ex ) ) );
                return 0;
            }
        }

        //----------------------------------------------------------------------
        public byte[] ReadByteArray( string name )
        {
            if( _reader[ name ] == DBNull.Value )
            {
                _errors.Add( String.Format( "NULL value read for '{0}'. Default value '{1}' returned.", name, "null" ) );
                return null;
            }

            byte[] value = _reader[ name ] as byte[];
            return value == null ? ( byte[] )null : ( byte[] ) _reader[ name ];
        }

        //----------------------------------------------------------------------
        public byte[] TryReadbyteArray( string name )
        {
            return TryReadByteArray( name );
        }

        //----------------------------------------------------------------------
        public byte[] TryReadByteArray( string name )
        {
            try
            {
                return ReadByteArray( name );
            }
            catch( Exception ex )
            {
                _errors.Add( String.Format( "Failed to read long for '{0}'. Returning default: '{1}' with {2}.", name, "null", DataManager.BuildExceptionString( ex ) ) );
                return null;
            }
        }

        //----------------------------------------------------------------------
		/// <summary>
		/// Gets and clears the transaction errors after exectution.
		/// </summary>
		/// <returns>string representing all the transaction errors.</returns>
		public string GetAndClearErrors()
		{
			if (_errors.Count == 0)
				return "No TransactionReader errors.";

			string outMessage = "TransactionReader errors: \r\n";

			foreach (string error in _errors)
			{
				outMessage += error + "\r\n";
			}

			_errors.Clear();
			return outMessage;
		}
	}
}
