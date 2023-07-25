using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jaxis.Readers.POS
{
    public enum EpsonESC
    {
        DLE = 0x10,
        ESC = 0x1B,
        GS = 0x1D,
        AB = 0x09,								// Horizontal Tab
        ERT_TAB = 0x0B,							// Vertical Tab
        AN = 0x18,								// Cancel print data in page mode.
        TART_ASCII = 0x20,
        ND_ASCII = 0x7E,
        LINE_FEED = 0x0A,						// LF
        CARRIAGE_RETURN = 0x0D,					// CR
        FORM_FEED = 0x0C,						// FF - Look in options to see if we treat this as a cut.

        REAL_TIME_STATUS = 0x1004,				// DLE EOT [n]

        PRINT_DATA_PAGE_MODE = 0x1B0C,			// ESC FF
        SET_CHAR_SPACING_RIGHT = 0x1B20,			// ESC SP [n]
        SELECT_PRINT_MODES = 0x1B21,				// ESC ! [n]
        SET_ABSOLUTE_PRINT_POSITION = 0x1B24,	// ESC $ nL nH
        SET_USER_DEFINED_CHAR_SET = 0x1B25,		// ESC % [n]
        UNDERLINE_MODE = 0x1B2D,					// ESC - [n]
        SET_DEFAULT_LINE_SPACING = 0x1B32,		// ESC 2
        SET_LINE_SPACING = 0x1B33,				// ESC 3 [n]
        RETURN_HOME = 0x1B3C,					// ESC <
        CANCEL_USER_DEFINED_CHARS = 0x1B3F,		// ESC ? [n]
        GO = 0x1B40,		                        // ESC GO
        SET_CUT_SHEET_EJECT_LENGTH = 0x1B43,		// ESC C [n]
        SET_TAB_POSITIONS = 0x1B44,				// ESC D n1...nk 00
        EMPHASIZE_MODE = 0x1B45,					// ESC E [0|1]
        CUT_SHEET_REV_MODE = 0x1B46,				// ESC F [n]
        DOUBLE_STRIKE = 0x1B47,					// ESC G [n]
        PRINT_AND_FEED_PAPER = 0x1B4A,			// ESC J [n]
        PRINT_AND_FEED_REV = 0x1B4B,				// ESC K [n]
        SELECT_CHAR_FONT = 0x1B4D,				// ESC M [n]
        SELECT_INTL_CHAR = 0x1B52,				// ESC R [n]
        SELECT_PRINT_DIRECTION = 0x1B54,			// ESC T [n]
        UNIDIRECTIONAL_MODE = 0x1B55,			// ESC U [n]
        ROTATE_90_MODE = 0x1B56,					// ESC V [n]
        SET_PRINT_AREA_PAGED = 0x1B57,			// ESC W (10 bytes total)
        SET_RELATIVE_POSITION = 0x1B5C,			// ESC \ nL nH
        JUSTIFICATION = 0x1B61,					// ESC a [n]
        SELECT_PAPER_TYPES = 0x1B63,				// ESC c [0|1] [n]
        PRINT_AND_FEED_LINES = 0x1B64,			// ESC d [n]
        PRINT_AND_FEED_REV_LINES = 0x1B65,		// ESC e [n]
        SET_CUT_SHEET_WAIT_TIME = 0x1B66,		// ESC f t1 t2
        PARTIAL_CUT = 0x1B69,					// ESC i
        PARTIAL_CUT_2 = 0x1B6D,					// ESC m
        STAMP = 0x1B6F,							// ESC o
        PAPER_RELEASE = 0x1B71,					// ESC q
        SELECT_PRINT_COLOR = 0x1B72,				// ESC r [n]
        SELECT_CHAR_CODE_TABLE = 0x1B74,			// ESC t [n]
        TRANSMIT_PERPH_STATUS = 0x1B75,			// ESC u [n]
        TRANSMIT_PAPER_STATUS = 0x1B76,			// ESC v,
        UPSIDE_DOWN_MODE = 0x1B7B,				// ESC { [n]

        TRANSMIT_REAL_TIME_STATUS = 0x1D05,		// GS ENQ
        SELECT_CHAR_SIZE = 0x1D21,				// GS ! [n]
        SET_ABS_VERT_PRINT_POSITION = 0x1D24,	// GS $ nL nH
        REVERSE_BLACK_PRINTING = 0x1D42,			// GS B [n]
        SET_LEFT_MARGIN = 0x1D4C,				// GS L nL nH
        SET_PRINT_AREA = 0x1D57,					// GS W nL nH
        SET_REL_VERT_PRINT_POSITION = 0x1D5C,	// GS \ nL nH
        AUTOMATIC_STATUS_BACK = 0x1D61,			// GS a [n]
        SMOOTHING_MODE = 0x1D62,					// GS b [n]
        TRANSMIT_STATUS = 0x1D72,				// GS r [n]

    }

    public static class PosEscTable
    {
        // The following are command characters that need to be processed.
        public const byte DLE = 0x10;
        public const byte ESC = 0x1B;
        public const byte GS = 0x1D;

        // The printable character range.
        public const byte TAB = 0x09;								// Horizontal Tab
        public const byte VERT_TAB = 0x0B;							// Vertical Tab
        public const byte CAN = 0x18;								// Cancel print data in page mode.
        public const byte START_ASCII = 0x20;
        public const byte END_ASCII = 0x7E;

        // Print commands
        public const short LINE_FEED = 0x0A;						// LF
        public const short CARRIAGE_RETURN = 0x0D;					// CR
        public const short FORM_FEED = 0x0C;						// FF - Look in options to see if we treat this as a cut.

        // Commands -- in numerical order.
        public const short REAL_TIME_STATUS = 0x1004;				// DLE EOT [n]

        public const short PRINT_DATA_PAGE_MODE = 0x1B0C;			// ESC FF
        public const short SET_CHAR_SPACING_RIGHT = 0x1B20;			// ESC SP [n]
        public const short SELECT_PRINT_MODES = 0x1B21;				// ESC ! [n]
        public const short SET_ABSOLUTE_PRINT_POSITION = 0x1B24;	// ESC $ nL nH
        public const short SET_USER_DEFINED_CHAR_SET = 0x1B25;		// ESC % [n]
        public const short UNDERLINE_MODE = 0x1B2D;					// ESC - [n]
        public const short SET_DEFAULT_LINE_SPACING = 0x1B32;		// ESC 2
        public const short SET_LINE_SPACING = 0x1B33;				// ESC 3 [n]
        public const short RETURN_HOME = 0x1B3C;					// ESC <
        public const short CANCEL_USER_DEFINED_CHARS = 0x1B3F;		// ESC ? [n]
        public const short GO = 0x1B40;		                        // ESC GO
        public const short SET_CUT_SHEET_EJECT_LENGTH = 0x1B43;		// ESC C [n]
        public const short SET_TAB_POSITIONS = 0x1B44;				// ESC D n1...nk 00
        public const short EMPHASIZE_MODE = 0x1B45;					// ESC E [0|1]
        public const short CUT_SHEET_REV_MODE = 0x1B46;				// ESC F [n]
        public const short DOUBLE_STRIKE = 0x1B47;					// ESC G [n]
        public const short PRINT_AND_FEED_PAPER = 0x1B4A;			// ESC J [n]
        public const short PRINT_AND_FEED_REV = 0x1B4B;				// ESC K [n]
        public const short SELECT_CHAR_FONT = 0x1B4D;				// ESC M [n]
        public const short SELECT_INTL_CHAR = 0x1B52;				// ESC R [n]
        public const short SELECT_PRINT_DIRECTION = 0x1B54;			// ESC T [n]
        public const short UNIDIRECTIONAL_MODE = 0x1B55;			// ESC U [n]
        public const short ROTATE_90_MODE = 0x1B56;					// ESC V [n]
        public const short SET_PRINT_AREA_PAGED = 0x1B57;			// ESC W (10 bytes total)
        public const short SET_RELATIVE_POSITION = 0x1B5C;			// ESC \ nL nH
        public const short JUSTIFICATION = 0x1B61;					// ESC a [n]
        public const short SELECT_PAPER_TYPES = 0x1B63;				// ESC c [0|1] [n]
        public const short PRINT_AND_FEED_LINES = 0x1B64;			// ESC d [n]
        public const short PRINT_AND_FEED_REV_LINES = 0x1B65;		// ESC e [n]
        public const short SET_CUT_SHEET_WAIT_TIME = 0x1B66;		// ESC f t1 t2
        public const short PARTIAL_CUT = 0x1B69;					// ESC i
        public const short PARTIAL_CUT_2 = 0x1B6D;					// ESC m
        public const short STAMP = 0x1B6F;							// ESC o
        public const short PAPER_RELEASE = 0x1B71;					// ESC q
        public const short SELECT_PRINT_COLOR = 0x1B72;				// ESC r [n]
        public const short SELECT_CHAR_CODE_TABLE = 0x1B74;			// ESC t [n]
        public const short TRANSMIT_PERPH_STATUS = 0x1B75;			// ESC u [n]
        public const short TRANSMIT_PAPER_STATUS = 0x1B76;			// ESC v;
        public const short UPSIDE_DOWN_MODE = 0x1B7B;				// ESC { [n]

        public const short TRANSMIT_REAL_TIME_STATUS = 0x1D05;		// GS ENQ
        public const short SELECT_CHAR_SIZE = 0x1D21;				// GS ! [n]
        public const short SET_ABS_VERT_PRINT_POSITION = 0x1D24;	// GS $ nL nH
        public const short REVERSE_BLACK_PRINTING = 0x1D42;			// GS B [n]
        public const short SET_LEFT_MARGIN = 0x1D4C;				// GS L nL nH
        public const short SET_PRINT_AREA = 0x1D57;					// GS W nL nH
        public const short SET_REL_VERT_PRINT_POSITION = 0x1D5C;	// GS \ nL nH
        public const short AUTOMATIC_STATUS_BACK = 0x1D61;			// GS a [n]
        public const short SMOOTHING_MODE = 0x1D62;					// GS b [n]
        public const short TRANSMIT_STATUS = 0x1D72;				// GS r [n]
        /// <summary>
        ///		Stores all of the command lengths in a hash table.
        /// </summary>
        private static Dictionary<short, int> m_commands = new Dictionary<short, int>( );

        /// <summary>
        ///		Gets the length of the ESC or GS command.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public static int GetCommandLength( short command )
        {
            if( m_commands.Count == 0 )
                LoadCommands( );

            return Convert.ToInt32( m_commands[command] );
        }

        /// <summary>
        ///		Loads the commands in to the hash table so we can easily return their length.
        /// </summary>
        private static void LoadCommands( )
        {
            m_commands.Add( GO, 2 );
            m_commands.Add( AUTOMATIC_STATUS_BACK, 3 );
            m_commands.Add( CANCEL_USER_DEFINED_CHARS, 3 );
            m_commands.Add( CUT_SHEET_REV_MODE, 3 );
            m_commands.Add( DOUBLE_STRIKE, 3 );
            m_commands.Add( EMPHASIZE_MODE, 3 );
            m_commands.Add( JUSTIFICATION, 3 );
            m_commands.Add( PAPER_RELEASE, 2 );
            m_commands.Add( PARTIAL_CUT, 2 );
            m_commands.Add( PARTIAL_CUT_2, 2 );
            m_commands.Add( PRINT_AND_FEED_LINES, 3 );
            m_commands.Add( PRINT_AND_FEED_PAPER, 3 );
            m_commands.Add( PRINT_AND_FEED_REV, 3 );
            m_commands.Add( PRINT_AND_FEED_REV_LINES, 3 );
            m_commands.Add( PRINT_DATA_PAGE_MODE, 2 );
            m_commands.Add( REAL_TIME_STATUS, 3 );
            m_commands.Add( RETURN_HOME, 2 );
            m_commands.Add( REVERSE_BLACK_PRINTING, 3 );
            m_commands.Add( ROTATE_90_MODE, 3 );
            m_commands.Add( SELECT_CHAR_CODE_TABLE, 3 );
            m_commands.Add( SELECT_CHAR_FONT, 3 );
            m_commands.Add( SELECT_CHAR_SIZE, 3 );
            m_commands.Add( SELECT_INTL_CHAR, 3 );
            m_commands.Add( SELECT_PAPER_TYPES, 4 );
            m_commands.Add( SELECT_PRINT_DIRECTION, 3 );
            m_commands.Add( SELECT_PRINT_COLOR, 3 );
            m_commands.Add( SELECT_PRINT_MODES, 3 );
            m_commands.Add( SET_ABSOLUTE_PRINT_POSITION, 4 );
            m_commands.Add( SET_ABS_VERT_PRINT_POSITION, 4 );
            m_commands.Add( SET_CUT_SHEET_EJECT_LENGTH, 3 );
            m_commands.Add( SET_CUT_SHEET_WAIT_TIME, 4 );
            m_commands.Add( SET_CHAR_SPACING_RIGHT, 3 );
            m_commands.Add( SET_DEFAULT_LINE_SPACING, 2 );
            m_commands.Add( SET_LEFT_MARGIN, 4 );
            m_commands.Add( SET_LINE_SPACING, 3 );
            m_commands.Add( SET_PRINT_AREA, 4 );
            m_commands.Add( SET_PRINT_AREA_PAGED, 10 );
            m_commands.Add( SET_RELATIVE_POSITION, 4 );
            m_commands.Add( SET_REL_VERT_PRINT_POSITION, 4 );
            m_commands.Add( SET_TAB_POSITIONS, 2 );				// Default to 2...need to read until NULL.
            m_commands.Add( SET_USER_DEFINED_CHAR_SET, 3 );
            m_commands.Add( SMOOTHING_MODE, 3 );
            m_commands.Add( STAMP, 2 );
            m_commands.Add( TRANSMIT_PAPER_STATUS, 2 );
            m_commands.Add( TRANSMIT_PERPH_STATUS, 3 );
            m_commands.Add( TRANSMIT_REAL_TIME_STATUS, 2 );
            m_commands.Add( TRANSMIT_STATUS, 3 );
            m_commands.Add( UNDERLINE_MODE, 3 );
            m_commands.Add( UNIDIRECTIONAL_MODE, 3 );
            m_commands.Add( UPSIDE_DOWN_MODE, 3 );
        }
    }
}
