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
        private StreamWriter _sw;
        private IO.ByteOrder Endian = IO.ByteOrder.LittleEndian;
        private List<string> StrTable = new List<string>();

        public FPK(StreamWriter sw)
        {
            _sw = sw;
        }

        public bool ParseFPK(string path)
        {
            if (!File.Exists(path))
            {
                return false;
            }
            return _internalLoad(path);
        }


        private bool _internalLoad(string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            using (BinaryReader br = new BinaryReader(fs))
            {
                fpk_head fpk_h = new fpk_head(br, Endian);
                if (!fpk_h.IsValid)
                    return false;

                IO.AlignStream(fs, 16);

                uint32 foxTOC_aCount = IO.ReadUInt32(br, Endian);
                uint32 foxTOC_bCount = IO.ReadUInt32(br, Endian);


                fpk_data fpk_d = new fpk_data(br, Endian);
                if(!fpk_d.isValid)
                    return false;

                //Calc
                int64 data_end_marker = (int64)fpk_d.unkPtrC;

                //Lets go to that string table and read
                fs.Seek(fpk_d.strTableOffset, SeekOrigin.Begin);
                while (fs.Position < data_end_marker)
                {
                    string cur_str = IO.ReadStringASCIIZ(br);
                    if(cur_str != string.Empty)
                        StrTable.Add(cur_str);
                }
            }

            for (int x = 0; x < StrTable.Count; x++)
                _sw.WriteLine(StrTable[x]);

            _sw.Flush();
            return true;
        }




    }
}
