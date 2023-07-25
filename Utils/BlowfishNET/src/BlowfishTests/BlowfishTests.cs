/*
  Copyright 2001-2009 Markus Hahn 
  All rights reserved. See documentation for license details.
*/

using System;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using BlowfishNET;
using BlowfishNET.JavaInterop;
using NUnit.Framework;

namespace BlowfishNET.Tests
{
    /// <summary>Test for the core functionality of Blowfish.NET.</summary>
    [TestFixture] 
    public class TestCore
    {
        Random rnd = new Random(0x1234beeb);

        void RunCBC(byte[] key, byte[] data, int ofs)
        {
            BlowfishCBC bfc = new BlowfishCBC();

            bfc.Initialize(key, ofs, key.Length - ofs);

            byte[] iv = new byte[BlowfishCBC.BLOCK_SIZE + ofs];
            this.rnd.NextBytes(iv);

            bfc.SetIV(iv, ofs);
            bfc.IV = bfc.IV;
            byte[] iv2 = new byte[BlowfishCBC.BLOCK_SIZE + ofs];
            bfc.GetIV(iv2, ofs);

            for (int i = ofs, c = iv2.Length; i < c; i++)
            {
                Assert.IsTrue(iv[i] == iv2[i]);
            }

            byte[] enc = new byte[data.Length];
            byte[] dec = new byte[data.Length];
        
            bfc.Encrypt(data, ofs, enc, ofs, data.Length - ofs);

            bfc.SetIV(iv2, ofs);
            bfc.Decrypt(enc , ofs, dec, ofs, data.Length - ofs);

            for (int i = ofs, c = data.Length - ofs; i < c; i++)
            {
                Assert.IsTrue(dec[i] == data[i]);
            }

            bfc = (BlowfishCBC) bfc.Clone();
            bfc.SetIV(iv2, ofs);

            bfc.Decrypt(enc , ofs, dec, ofs, data.Length - ofs);

            for (int i = ofs, c = data.Length - ofs; i < c; i++)
            {
                Assert.IsTrue(dec[i] == data[i]);
            }

            uint hi, lo, hi2, lo2;
            uint hi3 = (uint)this.rnd.Next();
            uint lo3 = (uint)this.rnd.Next();

            bfc.SetIV(iv, ofs);
            bfc.EncryptBlock(hi3, hi3, out hi, out lo);

            bfc.SetIV(iv2, ofs);
            bfc.DecryptBlock(hi, lo, out hi2, out lo2);

            Assert.IsTrue(hi3 != hi);
            Assert.IsTrue(lo3 != lo);
            
            Assert.IsTrue(hi2 == hi3);
            Assert.IsTrue(lo2 == hi3);

            bfc.Invalidate();

            iv = bfc.IV;
            Assert.IsTrue(BlowfishCBC.BLOCK_SIZE == iv.Length);

            for (int i = 0; i < iv.Length; i++)
            {
                Assert.IsTrue(0 == iv[i]);
            }
        }

        void RunECB(byte[] key, byte[] data, int ofs)
        {
            BlowfishECB bfe = new BlowfishECB();

            bfe.Initialize(key, ofs, key.Length - ofs);

            byte[] enc = new byte[data.Length];
            byte[] dec = new byte[data.Length];
        
            bfe.Encrypt(data, ofs, enc, ofs, data.Length - ofs);
            bfe.Decrypt(enc , ofs, dec, ofs, data.Length - ofs);

            for (int i = ofs, c = data.Length - ofs; i < c; i++)
            {
                Assert.IsTrue(dec[i] == data[i]);
            }

            bfe = (BlowfishECB) bfe.Clone();

            bfe.Decrypt(enc , ofs, dec, ofs, data.Length - ofs);

            for (int i = ofs, c = data.Length - ofs; i < c; i++)
            {
                Assert.IsTrue(dec[i] == data[i]);
            }

            uint hi, lo, hi2, lo2;
            uint hi3 = (uint)this.rnd.Next();
            uint lo3 = (uint)this.rnd.Next();

            bfe.EncryptBlock(hi3, hi3, out hi, out lo);
            bfe.DecryptBlock(hi, lo, out hi2, out lo2);

            Assert.IsTrue(hi3 != hi);
            Assert.IsTrue(lo3 != lo);
            
            Assert.IsTrue(hi2 == hi3);
            Assert.IsTrue(lo2 == hi3);
        }

        void RunCFB(byte[] key, byte[] data, int ofs)
        {
            byte[] enc = new byte[data.Length];
            byte[] dec = new byte[data.Length];

            BlowfishCFB bff = new BlowfishCFB();

            bff.Initialize(key, ofs, key.Length - ofs);

            byte[] iv = new byte[BlowfishCBC.BLOCK_SIZE + ofs];
            this.rnd.NextBytes(iv);
            bff.SetIV(iv, ofs);
            bff.IV = bff.IV;
            bff.Encrypt(data, ofs, enc, ofs, data.Length - ofs);

            bff.SetIV(iv, ofs);
            bff.Decrypt(enc, ofs, dec, ofs, data.Length - ofs);

            for (int i = ofs, c = data.Length - ofs; i < c; i++)
            {
                Assert.IsTrue(dec[i] == data[i]);
            }

            bff = (BlowfishCFB)bff.Clone();
            bff.SetIV(iv, ofs);

            bff.Decrypt(enc, ofs, dec, ofs, data.Length - ofs);

            for (int i = ofs, c = data.Length - ofs; i < c; i++)
            {
                Assert.IsTrue(dec[i] == data[i]);
            }
        }

        static int[] KEY_SIZES = { 0, 1, 2, 7, 8, 9, 15, 16, 32, 33, BlowfishECB.MAX_KEY_LENGTH };

        /// <summary>Performs simple tests for ECB, CBC and CFB.</summary>
        [Test] 
        public void Test() 
        {
            for (int mode = 0; mode < 3; mode++)
            {
                bool blkcph = mode < 2;
                int keyIdx = 0;
                while (keyIdx < KEY_SIZES.Length)
                {
                    int dataLen = 0;
                    int inc = 1;
                    while (dataLen < (blkcph ? 20000 : 1000))
                    {
                        for (int ofs = 0; ofs < 2; ofs++)
                        {
                            byte[] key = new byte[KEY_SIZES[keyIdx] + ofs];
                            for (int i = 0; i < key.Length; i++)
                            {
                                key[i] = (byte)i;
                            }

                            byte[] data = new byte[dataLen + ofs];
                            for (int i = 0; i < data.Length; i++)
                            {
                                data[i] = (byte)i;
                            }
                        
                            switch (mode)
                            {
                                case 0: RunECB(key, data, ofs); break;
                                case 1: RunCBC(key, data, ofs); break;
                                case 2: RunCFB(key, data, ofs); break;
                            }

                        }

                        if (blkcph)   // only ECB and CBC need block alignment
                        {
                            dataLen += dataLen + BlowfishECB.BLOCK_SIZE;
                            dataLen -= dataLen % BlowfishECB.BLOCK_SIZE;
                        }
                        else
                        {
                            dataLen += inc++;   // more suitable for stream cipher like
                        }
                    }

                    keyIdx++;
                }
            }   
        }

        // (taken from the Counterpane website at
        //  http://www.counterpane.com/vectors.txt
        //  triplets of key, plain, cipher)
        static readonly ulong[] TEST_VECTORS =
        {
            0x0000000000000000, 0x0000000000000000, 0x4ef997456198dd78,
            0xffffffffffffffff, 0xffffffffffffffff, 0x51866fd5b85ecb8a,
            0x3000000000000000, 0x1000000000000001, 0x7d856f9a613063f2,
            0x1111111111111111, 0x1111111111111111, 0x2466dd878b963c9d,
            0x0123456789abcdef, 0x1111111111111111, 0x61f9c3802281b096,
            0x1111111111111111, 0x0123456789abcdef, 0x7d0cc630afda1ec7,
            0x0000000000000000, 0x0000000000000000, 0x4ef997456198dd78,
            0xfedcba9876543210, 0x0123456789abcdef, 0x0aceab0fc6a0a28d,
            0x7ca110454a1a6e57, 0x01a1d6d039776742, 0x59c68245eb05282b,
            0x0131d9619dc1376e, 0x5cd54ca83def57da, 0xb1b8cc0b250f09a0,
            0x07a1133e4a0b2686, 0x0248d43806f67172, 0x1730e5778bea1da4,
            0x3849674c2602319e, 0x51454b582ddf440a, 0xa25e7856cf2651eb,
            0x04b915ba43feb5b6, 0x42fd443059577fa2, 0x353882b109ce8f1a,
            0x0113b970fd34f2ce, 0x059b5e0851cf143a, 0x48f4d0884c379918,
            0x0170f175468fb5e6, 0x0756d8e0774761d2, 0x432193b78951fc98,
            0x43297fad38e373fe, 0x762514b829bf486a, 0x13f04154d69d1ae5,
            0x07a7137045da2a16, 0x3bdd119049372802, 0x2eedda93ffd39c79,
            0x04689104c2fd3b2f, 0x26955f6835af609a, 0xd887e0393c2da6e3,
            0x37d06bb516cb7546, 0x164d5e404f275232, 0x5f99d04f5b163969,
            0x1f08260d1ac2465e, 0x6b056e18759f5cca, 0x4a057a3b24d3977b,
            0x584023641aba6176, 0x004bd6ef09176062, 0x452031c1e4fada8e,
            0x025816164629b007, 0x480d39006ee762f2, 0x7555ae39f59b87bd,
            0x49793ebc79b3258f, 0x437540c8698f3cfa, 0x53c55f9cb49fc019,
            0x4fb05e1515ab73a7, 0x072d43a077075292, 0x7a8e7bfa937e89a3,
            0x49e95d6d4ca229bf, 0x02fe55778117f12a, 0xcf9c5d7a4986adb5,
            0x018310dc409b26d6, 0x1d9d5c5018f728c2, 0xd1abb290658bc778,
            0x1c587f1c13924fef, 0x305532286d6f295a, 0x55cb3774d13ef201,
            0x0101010101010101, 0x0123456789abcdef, 0xfa34ec4847b268b2,
            0x1f1f1f1f0e0e0e0e, 0x0123456789abcdef, 0xa790795108ea3cae,
            0xe0fee0fef1fef1fe, 0x0123456789abcdef, 0xc39e072d9fac631d,
            0x0000000000000000, 0xffffffffffffffff, 0x014933e0cdaff6e4,
            0xffffffffffffffff, 0x0000000000000000, 0xf21e9a77b71c49bc,
            0x0123456789abcdef, 0x0000000000000000, 0x245946885754369a,
            0xfedcba9876543210, 0xffffffffffffffff, 0x6b5c5a9c5d9e0a5a
        };

        /// <summary>Checks the Blowfish.NET implementation against the official test vectors.</summary>
        [Test] 
        public void TestVectors() 
        {
            for (int i = 0; i < TEST_VECTORS.Length;)
            {
                byte[] key = new byte[8]; ulong lkey = TEST_VECTORS[i++];
                byte[] plain = new byte[8]; ulong lplain = TEST_VECTORS[i++];
                byte[] cipher = new byte[8]; ulong lcipher = TEST_VECTORS[i++];

                for (int j = 7; j >= 0; j--)
                {
                    key   [j] = (byte)(lkey    & 0x0ff);    lkey    >>= 8;
                    plain [j] = (byte)(lplain  & 0x0ff);    lplain  >>= 8;
                    cipher[j] = (byte)(lcipher & 0x0ff);    lcipher >>= 8;
                }

                byte[] testBuf = new byte[8];

                BlowfishECB bfe = new BlowfishECB(key, 0, key.Length);

                bfe.Encrypt(plain, 0, testBuf, 0, 8);

                for (int j = 0; j < testBuf.Length; j++)
                {
                    Assert.IsTrue(testBuf[j] == cipher[j]);
                }
            }
        }

        static readonly byte[] SOME_WEAK_KEY = 
        {
            0xe4, 0x19, 0xbc, 0xec, 0x18, 0x7b, 0x27, 0x81, 0x64, 0x51, 
            0x54, 0xe6, 0x0a, 0x42, 0x79, 0x6b, 0x16, 0xc8, 0x54, 0x85, 
            0x3b, 0x64, 0xfa, 0x1e, 0x61, 0x29, 0x7e, 0x36, 0xe9, 0xd3, 
            0xcf, 0xe2, 0x2b, 0x69, 0x68, 0x33, 0x11, 0xa1, 0x57, 0x83
        };


        /// <summary>Tests weak key detection.</summary>
        [Test] 
        public void TestWeakKey() 
        {
            byte[] key = (byte[])SOME_WEAK_KEY.Clone();

            BlowfishECB bfe = new BlowfishECB(key, 0, key.Length);
            Assert.IsTrue(bfe.IsWeakKey);

            Array.Clear(key, 0, key.Length);

            bfe = new BlowfishECB(key, 0, key.Length);
            Assert.IsTrue(!bfe.IsWeakKey);
        }

        // reference data from OpenSSL for CFB testing (crypto/bf/bftest.c)
        static readonly byte[] OPENSSL_BFCFB_REFKEY =
        {
                0x01,0x23,0x45,0x67,0x89,0xab,0xcd,0xef,
                0xf0,0xe1,0xd2,0xc3,0xb4,0xa5,0x96,0x87
        };
        static readonly byte[] OPENSSL_BFCFB_REFDATA = Encoding.ASCII.GetBytes("7654321 Now is the time for \0");
        static readonly byte[] OPENSSL_BFCFB_REFIV = { 0xfe,0xdc,0xba,0x98,0x76,0x54,0x32,0x10 };
        static readonly byte[] OPENSSL_BFCFB_REFCTXT =
        {
                0xe7,0x32,0x14,0xa2,0x82,0x21,0x39,0xca,
                0xf2,0x6e,0xcf,0x6d,0x2e,0xb9,0xe7,0x6e,
                0x3d,0xa3,0xde,0x04,0xd1,0x51,0x72,0x00,
                0x51,0x9d,0x57,0xa6,0xc3
        };

        /// <summary>Tests CFB compatibility with Eric Young's Blowfish/CFB found in OpenSSL.</summary>
        [Test]
        public void TestCFBOpenSSL()
        {
            BlowfishCFB bfc = new BlowfishCFB();
            
            bfc.Initialize(OPENSSL_BFCFB_REFKEY, 0, OPENSSL_BFCFB_REFKEY.Length);
            bfc.IV = OPENSSL_BFCFB_REFIV;
            
            int len = OPENSSL_BFCFB_REFDATA.Length;
            
            byte[] buf = new byte[len];
            
            bfc.Encrypt(OPENSSL_BFCFB_REFDATA, 0, buf, 0, 13);    // (just for fun, the same way like in the C code)
            bfc.Encrypt(OPENSSL_BFCFB_REFDATA, 13, buf, 13, len - 13);
            
            Assert.IsTrue(len == OPENSSL_BFCFB_REFCTXT.Length);
            
            for (int i = 0; i < len; i++)
            {
                Assert.IsTrue(OPENSSL_BFCFB_REFCTXT[i] == buf[i]);
            }
        }
    }

    /// <summary>Test covering the BlowfishSimple class.</summary>
    [TestFixture]
    public class TestBlowfishSimple
    {
        /// <summary>Runs some standard tests for BlowfishSimple.</summary>
        [Test] 
        public void Test() 
        {
            StringBuilder sb = new StringBuilder();

            for (int keylen = 0; 
                 keylen < 10000; 
                 keylen += keylen + ((keylen & 1) ^ 1))
            {
                sb.Length = 0;
                for (int i = 0; i < keylen; i++)
                {
                    sb.Append((char)i);
                }

                String key = sb.ToString();

                for (int plainLen = 0; 
                     plainLen < 10000; 
                     plainLen += plainLen + (plainLen & 1) ^ 1)
                {
                    sb.Length = 0;
                    for (int i = 0; i < plainLen; i++)
                    {
                        sb.Append((char)i);
                    }

                    String plain = sb.ToString();

                    BlowfishSimple bfs0 = new BlowfishSimple(key);
                    BlowfishSimple bfs1 = new BlowfishSimple();
                    bfs1.Initialize(key);

                    String enc = bfs0.Encrypt(plain);
                    String dec = bfs1.Decrypt(enc);

                    Assert.IsTrue(BlowfishSimple.VerifyKey(key, bfs0.KeyChecksum));
                    Assert.IsTrue(BlowfishSimple.VerifyKey(key, bfs1.KeyChecksum));

                    Assert.IsTrue(0 == dec.CompareTo(plain));

                    bfs0 = new BlowfishSimple(key + ' ');
            
                    Assert.IsTrue(BlowfishSimple.VerifyKey(key, bfs1.KeyChecksum));
                }
            }
        }
    }

    /// <summary>Tests the Java Interoperability classes of Blowfish.NET.</summary>
    [TestFixture] 
    public class TestJavaInterop
    {
        /// <summary>Tests string encryption with BlowfishEasy.</summary>
        [Test] 
        public void TestBlowfishEasy() 
        {
            StringBuilder sb = new StringBuilder();

            for (int keyLen = 0; 
                 keyLen < 10000;
                 keyLen += keyLen + ((keyLen & 1) ^ 1))
            {
                sb.Length = 0;
                for (int i = 0; i < keyLen; i++)
                {
                    sb.Append((char)i);
                }

                String key = sb.ToString();

                for (int plainLen = 1; 
                     plainLen < 10000;
                     plainLen += plainLen + (plainLen & 1) ^ 1)
                {
                    sb.Length = 0;
                    for (int i = 0; i < plainLen; i++)
                    {
                        sb.Append((char)((i % 500) + 32));
                    }

                    String plain = sb.ToString();

                    BlowfishEasy bfes0 = new BlowfishEasy(key);
                    BlowfishEasy bfes1 = new BlowfishEasy(key);

                    String enc = bfes0.EncryptString(plain);
                    String dec = bfes1.DecryptString(enc);

                    Assert.IsTrue(0 == dec.CompareTo(plain));

                    bfes1 = new BlowfishEasy(key + "wrong");
                    dec = bfes1.DecryptString(enc);

                    Assert.IsTrue(
                        (null == dec) ||
                        (0 != dec.CompareTo(plain)));
                }
            }
        }

        /// <summary>Tests stream I/O with BlowfishEasy.</summary>
        [Test] 
        public void TestBlowfishStream() 
        {
            int[] KEYLENS = { 0, 1, 2, 8, 16, 32, 33, 49, 55, BlowfishECB.MAX_KEY_LENGTH };
            int[] DATALENS = { 0, 1, 2, 3, 7, 8, 9, 15, 16, 17, 1024, 103117 };

            foreach (int keyLen in KEYLENS)
            {
                byte[] key = new byte[keyLen];
        
                for (int i = 0; i < keyLen; i++)
                {
                    key[i] = (byte)i;
                }

                foreach (int dataLen in DATALENS)
                {
                    byte[] plain = new byte[dataLen];

                    for (int i = 0; i < dataLen; i++)
                    {
                        plain[i] = (byte)i;
                    }

                    MemoryStream ms = new MemoryStream();
                    BlowfishStream bfs = BlowfishStream.Create(
                        ms, 
                        BlowfishStreamMode.Write, 
                        key, 
                        0, 
                        key.Length);

                    int ofs = 0;
                    while (ofs < dataLen)
                    {
                        bfs.Write(plain, ofs, 1);
                        ofs++;
                    }
                    bfs.Close();

                    byte[] enc = ms.ToArray();
                    ms = new MemoryStream(enc);
                    bfs = BlowfishStream.Create(
                        ms, 
                        BlowfishStreamMode.Read, 
                        key, 
                        0, 
                        key.Length);

                    byte[] dec = new byte[dataLen];
                    Array.Clear(dec, 0, dec.Length);
                    
                    ofs = 0;
                    while (ofs < dataLen)
                    {
                        int tmp = tmp = bfs.ReadByte();
                        Assert.IsTrue(-1 != tmp);
                        dec[ofs++] = (byte)tmp;
                    }
                    Assert.IsTrue(-1 == bfs.ReadByte());
                    bfs.Close();

                    for (int i = 0; i < dataLen; i++)
                    {
                        Assert.IsTrue(plain[i] == dec[i]);
                    }
                }
            }
        }

        /// <summary>Data produced with the Java class BlowfishJ.BlowfishOutputStream.</summary>
        static readonly byte[] XCHG_DATA =
        {
            0x32, 0xe3, 0x5d, 0x37, 0xc6, 0xbe, 0xe1, 0xf8, 0x5a, 0x3f, 0x4c,
            0xf6, 0x2b, 0xe3, 0x69, 0x7e, 0xb8, 0x35, 0x65, 0xf8, 0xcf, 0x6d,
            0x11, 0xd0, 0xa0, 0xfc, 0x51, 0x0a, 0x2b, 0x95, 0x1c, 0x1f, 0x44,
            0xb5, 0xc5, 0xd4, 0x7a, 0x76, 0xb5, 0x91, 0x03, 0x29, 0x24, 0x60,
            0xe1, 0x67, 0xbe, 0x1a, 0xfe, 0xfb, 0x8c, 0xc5, 0x82, 0x60, 0xf6,
            0xe9, 0x17, 0xc3, 0xc6, 0x34, 0x3c, 0xd9, 0x3b, 0x45, 0x47, 0x54,
            0xf6, 0x4d, 0xbc, 0x09, 0x56, 0x8a, 0x69, 0x22, 0xe6, 0x69, 0x44,
            0x18, 0x94, 0x28, 0xae, 0xa5, 0x72, 0x30, 0x24, 0xaf, 0x14, 0x14,
            0xa6, 0x3b, 0x21, 0x85, 0xca, 0xab, 0x58, 0x26, 0xa3, 0x89, 0x9d,
            0x3f, 0x1e, 0x2a, 0x40, 0xe6, 0xe6, 0xad, 0x4d, 0x4e, 0x8d, 0xe3,
            0x6d, 0x0b, 0xfa, 0x81, 0x51, 0x31, 0xa8, 0x1a, 0xe2, 0x2a 
        };

        const int XCHG_DATA_SIZE = 111;
        static readonly byte[] XCHG_KEY = { 0xaa, 0xbb, 0xcc, 0x00, 0x42, 0x33 };

        /// <summary>Tests compatibility between BlowfishJ and Blowfish.NET streams.</summary>
        [Test]
        public void TestBlowfishJXchg()
        {
            BlowfishStream bfs = BlowfishStream.Create(
                new MemoryStream(XCHG_DATA),
                BlowfishStreamMode.Read,
                XCHG_KEY,
                0,
                XCHG_KEY.Length);

            for (int i = 0; i < XCHG_DATA_SIZE; i++)
            {
                Assert.IsTrue((i & 0x0ff) == bfs.ReadByte());
            }

            Assert.IsTrue(-1 == bfs.ReadByte());

            bfs.Close();    
        }
    }

    /// <summary>Tests for the BlowfishAlgorithm implementation.</summary>
    [TestFixture]
    public class TestBlowfishAlgorithm
    {
        static int[] SIZES = { 0, 1, 2, 3, 7, 8, 9, 15, 16, 17, 63, 129, 1023, 5551 };
        static int[] BUFSZS = { 1, 2, 3, 7, 8, 9, 15, 16, 17, 255, 256, 257 };

        /// <summary>Test using cryptostreams.</summary>
        [Test]
        public void TestCryptoStreams()
        {
            foreach (int size in SIZES)
            foreach (int bufsz in BUFSZS)
            {
                byte[] buf = new byte[bufsz];

                for (int pad = 0; pad < 4; pad++)
                {
                    PaddingMode padMode;
                    bool padPrecise = false;
                    switch (pad)
                    {
                        case 0 : padMode = PaddingMode.ANSIX923; padPrecise = true;  break;
                        case 1 : padMode = PaddingMode.ISO10126; break;
                        case 2 : padMode = PaddingMode.PKCS7; padPrecise = true; break;
                        default: padMode = PaddingMode.Zeros; break;
                    }

                    for (int mode = 0; mode < 2; mode++)
                    {
                        BlowfishAlgorithm bfa = new BlowfishAlgorithm();
                        bfa.Mode = 0 == mode ? CipherMode.ECB : CipherMode.CBC;
                        bfa.Padding = padMode;

                        bfa.GenerateKey();
                        byte[] key = bfa.Key;
                        Assert.IsTrue(56 == key.Length);
                        bfa.Key = key;

                        ICryptoTransform cte = bfa.CreateEncryptor();
                        ICryptoTransform ctd = bfa.CreateDecryptor();

                        for (int k = 0; k < 2; k++)
                        {
                            MemoryStream ms = new MemoryStream();
                            CryptoStream cs = new CryptoStream(ms, cte, CryptoStreamMode.Write);

                            int i = 0;
                            while (i < size)
                            {
                                int towrite = size - i > bufsz ? bufsz : size - i;
                                for (int j = 0; j < bufsz; j++)
                                {
                                    buf[j] = (byte)i++;
                                }
                                cs.Write(buf, 0, towrite);
                            }
                            cs.Close();

                            byte[] enc = ms.ToArray();
                            Assert.IsTrue(size <= enc.Length);

                            ms = new MemoryStream(enc);
                            cs = new CryptoStream(ms, ctd, CryptoStreamMode.Read);
                            for (i = 0; i < size; i++)
                            {
                                int dval = cs.ReadByte();
                                Assert.IsFalse(-1 == dval);
                                Assert.IsTrue((i & 0xff) == dval);
                            }
                            if (padPrecise)
                            {
                                Assert.IsTrue(-1 == cs.ReadByte());
                            }
                            cs.Close();

                            ms = new MemoryStream(enc);
                            cs = new CryptoStream(ms, ctd, CryptoStreamMode.Read);

                            i = 0;
                            while (i < size)
                            {
                                int toread = size - i > bufsz ? bufsz : size - i;
                                Assert.IsTrue(toread == cs.Read(buf, 0, toread));
                                for (int j = 0; j < toread; j++)
                                {
                                    Assert.IsTrue(buf[j] == (byte)i++);
                                }
                            }
                            if (padPrecise)
                            {
                                Assert.IsTrue(-1 == cs.ReadByte());
                            }
                            cs.Close();
                        }
                    }
                }
            }
        }
    }
}
