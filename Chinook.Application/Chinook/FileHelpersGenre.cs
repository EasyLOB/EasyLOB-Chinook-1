using FileHelpers;
using System;
using System.Collections.Generic;

namespace Chinook.Application
{
    [FixedLengthRecord(FixedMode.AllowLessChars)]
    public class FileHelpersGenre
    {
        [FieldFixedLength(6)]
        [FieldConverter(typeof(IntegerConverter00))]
        public int GenreId;

        [FieldFixedLength(120)]
        [FieldTrim(TrimMode.Left)]
        public string Name;

        internal class DecimalConverter000 : ConverterBase
        {
            public override object StringToField(string from)
            {
                decimal d = Convert.ToDecimal(from);
                return d / 100;
            }
        }

        internal class IntegerConverter00 : ConverterBase
        {
            public override object StringToField(string from)
            {
                return Convert.ToInt32(from);
            }
        }
    }

    public class FileHelpersGenreSet : List<FileHelpersGenre>
    {
    }
}