using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomControls.Utility
{
    [System.AttributeUsage(AttributeTargets.Property)]
    public class PresentationAttribute : System.Attribute
    {
        private bool _isVisible;

        public bool IsVisible
        {
            get { return _isVisible; }
            set { _isVisible = value; }
        }


        private string _header;

        public string Header
        {
            get { return _header; }
            set { _header = value; }
        }

        private int _displayIndex;

        public int DisplayIndex
        {
            get { return _displayIndex; }
            set { _displayIndex = value; }
        }


        public PresentationAttribute(bool pIsVisible, string pHeader, int pDisplayIndex)
        {
            this.IsVisible = pIsVisible;
            this.Header = pHeader;
        }

        public PresentationAttribute(bool pIsVisible)
        {
            this.IsVisible = pIsVisible;
        }


    }
}
