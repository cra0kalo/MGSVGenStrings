//
// Reversed by @cra0kalo
//
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

//typedef in c#
using int8 = System.SByte;
using int16 = System.Int16;
using int32 = System.Int32;
using int64 = System.Int64;

using uint8 = System.Byte;
using uint16 = System.UInt16;
using uint32 = System.UInt32;
using uint64 = System.UInt64;

using Cra0Framework.Core.Min;

namespace genstr.FOXEngine
{
    public partial class FPK
    {
        const int FPK_HEADERSIZE = 18;

        public class fpk_head
        {
            private bool _isValidFPK;
            private const string magic_str = "foxfpk";
            public byte[]   magic = new byte[10];
            public uint64   fpk_length;


            public bool IsValid
            {
                get
                {
                    return _isValidFPK;
                }
            }

            public fpk_head(BinaryReader br, IO.ByteOrder endian)
            {
                magic = IO.ReadBytes(br, 6, endian);
                if (Utilz.GetString_ASCII(magic) == magic_str)
                {
                    _isValidFPK = true;
                    byte[] str_winIdentif = IO.ReadBytes(br, 4, endian);
                    fpk_length = IO.ReadUInt64(br, endian);
                }
                else
                {
                    _isValidFPK = false;
                }
            }
        }

        public class fpk_entry
        {
            public byte[] hash16 = new byte[16];
            public uint64 entryOffset;
            public uint64 entrySize;
            public uint64 entryNameOffset;
            public uint64 B;
        }

        public class fpk_data
        {
            public bool isValid;
            public uint32 valuePairB_a;
            public uint32 valuePairB_b;
            public uint64 unkPtrC;
            public uint64 unkPtrC_DataSize;
            public uint32 strTableOffset;
            public uint32 valuePairD_b;
            public uint32 valuePairE_a;
            public uint32 valuePairE_b;

            public fpk_data(BinaryReader br, IO.ByteOrder endian)
            {
                valuePairB_a = IO.ReadUInt32(br, endian);
                valuePairB_b = IO.ReadUInt32(br, endian);

                if (br.BaseStream.Position < br.BaseStream.Length)
                {
                    unkPtrC = IO.ReadUInt64(br, endian);
                    unkPtrC_DataSize = IO.ReadUInt64(br, endian);
                    strTableOffset = IO.ReadUInt32(br, endian);
                    valuePairD_b = IO.ReadUInt32(br, endian);
                    valuePairE_a = IO.ReadUInt32(br, endian);
                    valuePairE_b = IO.ReadUInt32(br, endian);

                    isValid = true;
                }
                else
                {
                    isValid = false;
                }
            }
        }

    }
}
