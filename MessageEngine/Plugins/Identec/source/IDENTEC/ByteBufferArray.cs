namespace IDENTEC
{
    using System;
    using System.Reflection;

    internal class ByteBufferArray
    {
        public const int DefaultSize = 0x80;
        private byte[] m_buffer;
        private int m_size;
        private int m_version;

        public ByteBufferArray()
        {
            this.m_buffer = new byte[0x80];
        }

        public ByteBufferArray(ByteBufferArray other)
        {
            this.m_buffer = new byte[other.Count];
            this.m_size = other.m_size;
            Array.Copy(other.ToArray(), 0, this.m_buffer, 0, this.m_size);
            this.m_version++;
        }

        public ByteBufferArray(int capacity)
        {
            if (capacity < 0)
            {
                throw new ArgumentOutOfRangeException("The buffer cannot have a negative length");
            }
            this.m_buffer = new byte[capacity];
        }

        public virtual void Add(byte b)
        {
            if (this.m_size >= this.m_buffer.Length)
            {
                this.EnsureCapacity(this.m_size + 1);
            }
            this.m_buffer[this.m_size++] = b;
            this.m_version++;
        }

        public virtual void Add(byte[] buffer, int sourceIndex, int sourceCount)
        {
            if (this.m_size < (this.m_buffer.Length + sourceCount))
            {
                this.EnsureCapacity(this.m_size + sourceCount);
            }
            Array.Copy(buffer, sourceIndex, this.m_buffer, this.m_size, sourceCount);
            this.m_size += sourceCount;
            this.m_version++;
        }

        public virtual void Add(ByteBufferArray b, int sourceIndex, int sourceCount)
        {
            byte[] buffer = b.ToArray();
            this.Add(buffer, sourceIndex, sourceCount);
        }

        public virtual void Clear()
        {
            Array.Clear(this.m_buffer, 0, this.m_size);
            this.m_size = 0;
            this.m_version++;
        }

        private void EnsureCapacity(int min)
        {
            if (this.m_buffer.Length < min)
            {
                int num = (this.m_buffer.Length == 0) ? 4 : (this.m_buffer.Length * 2);
                if (num < min)
                {
                    num = min;
                }
                this.Capacity = num;
            }
        }

        public virtual void RemoveRange(int index, int count)
        {
            if ((index < 0) || (count < 0))
            {
                throw new ArgumentOutOfRangeException("Cannot remove negative index or count");
            }
            if ((this.m_size - index) < count)
            {
                throw new ArgumentOutOfRangeException("The number of items to remove is too long");
            }
            if (count > 0)
            {
                this.m_size -= count;
                if (index < this.m_size)
                {
                    Array.Copy(this.m_buffer, index + count, this.m_buffer, index, this.m_size - index);
                }
                Array.Clear(this.m_buffer, this.m_size, count);
                this.m_version++;
            }
        }

        public virtual byte[] ToArray()
        {
            byte[] destinationArray = new byte[this.m_size];
            Array.Copy(this.m_buffer, 0, destinationArray, 0, this.m_size);
            return destinationArray;
        }

        public virtual byte[] ToArray(int length)
        {
            byte[] destinationArray = new byte[length];
            Array.Copy(this.m_buffer, 0, destinationArray, 0, length);
            return destinationArray;
        }

        public int Capacity
        {
            get
            {
                return this.m_buffer.Length;
            }
            set
            {
                if (value != this.m_buffer.Length)
                {
                    if (value < this.m_size)
                    {
                        throw new ArgumentOutOfRangeException("The capacity is too small");
                    }
                    if (value > 0)
                    {
                        byte[] destinationArray = new byte[value];
                        if (this.m_size > 0)
                        {
                            Array.Copy(this.m_buffer, 0, destinationArray, 0, this.m_buffer.Length);
                        }
                        this.m_buffer = destinationArray;
                    }
                    else
                    {
                        this.m_buffer = null;
                    }
                }
            }
        }

        public int Count
        {
            get
            {
                return this.m_size;
            }
        }

        public byte this[int index]
        {
            get
            {
                if (index >= this.m_size)
                {
                    throw new ArgumentOutOfRangeException("The index is out of range");
                }
                return this.m_buffer[index];
            }
            set
            {
                if (index >= this.m_size)
                {
                    throw new ArgumentOutOfRangeException("The index is out of range");
                }
                this.m_buffer[index] = value;
                this.m_version++;
            }
        }
    }
}

