using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace CustomControls.Utility
{
    [System.AttributeUsage(AttributeTargets.Class)]
    public class PresentationClass : System.Attribute
    {

        private FontFamily _fontType;

        public FontFamily FontType
        {
            get { return _fontType; }
            set { _fontType = value; }
        }


        private double _fontSize;

        public double FontSize
        {
            get { return _fontSize; }
            set { _fontSize = value; }
        }

        private int _sizeGroupsHeader;

        public int SizeGroupsHeader
        {
            get { return _sizeGroupsHeader; }
            set { _sizeGroupsHeader = value; }
        }

        public PresentationClass(string pFontType, double pFontSize, int pSizeGroupsHeaders = 0)
        {
            this.FontType = new FontFamily(pFontType);
            this.FontSize = pFontSize;
            this.SizeGroupsHeader = pSizeGroupsHeaders;
        }




    }
}
