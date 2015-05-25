using CustomControls.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using Telerik.Windows.Controls;
using Telerik.Windows.Documents.FormatProviders;
using Telerik.Windows.Documents.FormatProviders.Pdf;
using Telerik.Windows.Documents.Model;

namespace CustomControls.Windows.Export
{
    public class ExportMethod<T> : ViewModelBase
    {
        #region properties & attributes

        private RadDocument document;

        public RadDocument Document
        {
            get { return document; }
            set { document = value; }
        }

        private bool _showHeaderColumn;

        public bool ShowHeaderColumn
        {
            get { return _showHeaderColumn; }
            set { _showHeaderColumn = value; }
        }

        private int _sizeGroupsHeaders;

        public int SizeGroupsHeaders
        {
            get { return _sizeGroupsHeaders; }
            set
            {
                _sizeGroupsHeaders = value;
                OnPropertyChanged(() => this.SizeGroupsHeaders);
            }
        }

        private FontFamily _fontType;

        public FontFamily FontType
        {
            get { return _fontType; }
            set
            {
                _fontType = value;
                OnPropertyChanged(() => this.FontType);
            }
        }

        private IEnumerable<T> _data;

        public IEnumerable<T> Data
        {
            get { return _data; }
            set { _data = value; }
        }

        private double _fontSize;
        private IEnumerable<string> _propertiesNames;

        private bool _showGridLinesTable;

        public bool ShowGridLinesTable
        {
            get { return _showGridLinesTable; }
            set { _showGridLinesTable = value; }
        }

        public IEnumerable<string> PropertiesNamesData
        {
            get { return _propertiesNames; }
            set { _propertiesNames = value; }
        }

        public double FontSize
        {
            get { return _fontSize; }
            set
            {
                _fontSize = value;
                OnPropertyChanged(() => this.FontSize);
            }
        }

        #endregion properties & attributes

        public ExportMethod(IEnumerable<T> pData, bool pShowHeaderColumn = true, bool pGridLines = true)
        {
            this.Data = pData;
            this.ShowHeaderColumn = pShowHeaderColumn;
            this.ShowGridLinesTable = pGridLines;
            PropertiesNamesData = this.getPropertiesNames(pData.FirstOrDefault());
        }

        private void ajustDocument()
        {
            document.LayoutMode = DocumentLayoutMode.Paged;
            document.Measure(RadDocument.MAX_DOCUMENT_SIZE);
            document.Arrange(new RectangleF(PointF.Empty, document.DesiredSize));
        }

        private IEnumerable<string> getPropertiesNames(T item)
        {
            var properties = item.GetType().GetProperties();
            var names = (from a in properties where item.GetAttributeFrom<PresentationAttribute>(a.Name).IsVisible == true select a.Name);

            if (names != null)
            {
                return names;
            }

            return null;
        }

        public void GenerateDocument()
        {
            getDocumentSettings(Data);
            createDocument(Data);
            ajustDocument();
        }

        private void createDocument(IEnumerable<T> pData)
        {
            var item = pData.FirstOrDefault();
            var headers = this.getHeadersColumnsNames(item);

            var table = new Table();

            if (this.ShowHeaderColumn)
            {
                var rowHeader = this.getTableRow(headers);

                table.AddRow(rowHeader);
            }

            foreach (var dataRow in pData)
            {
                var tableRow = new TableRow();
                foreach (var prop in this.PropertiesNamesData)
                {
                    tableRow.Cells.Add(this.getCell(Convert.ToString(dataRow.getValueProperty(prop))));
                }
                table.AddRow(tableRow);
            }

            Section sec = new Section();
            sec.Blocks.Add(table);

            this.Document = new RadDocument();
            this.Document.Sections.Add(sec);
        }

        private TableRow getTableRow(IEnumerable<string> headers)
        {
            var headerRow = new TableRow();

            foreach (var item in headers)
            {
                TableCell cell = this.getCell(item);
                headerRow.Cells.Add(cell);
            }

            return headerRow;
        }

        private TableCell getCell(string item)
        {
            if (!string.IsNullOrEmpty(item))
            {
                var paragraph = new Paragraph();

                Span text = new Span(item);
                paragraph.Inlines.Add(text);

                TableCell cell = new TableCell();
                cell.TextAlignment = Telerik.Windows.Documents.Layout.RadTextAlignment.Center;
                cell.Blocks.Add(paragraph);

                return cell;
            }

            return new TableCell();
        }

        private IEnumerable<string> getHeadersColumnsNames(T item)
        {
            var properties = item.GetType().GetProperties();
            var headers = (from a in properties where item.GetAttributeFrom<PresentationAttribute>(a.Name).IsVisible == true orderby item.GetAttributeFrom<PresentationAttribute>(a.Name).DisplayIndex select item.GetAttributeFrom<PresentationAttribute>(a.Name).Header);

            if (headers != null)
            {
                return headers;
            }

            return null;
        }

        private void getDocumentSettings(IEnumerable pData)
        {
            PresentationClass press = typeof(T).GetCustomAttributes(typeof(PresentationClass), false).Cast<PresentationClass>().First();
            this.FontSize = press.FontSize;
            this.FontType = press.FontType;
            this.SizeGroupsHeaders = press.SizeGroupsHeader;
        }

        public void ExportToPdf(System.IO.Stream pFileOutput)
        {
            if (Document != null)
            {
                IDocumentFormatProvider provider = new PdfFormatProvider();
                provider.Export(this.Document, pFileOutput);
            }
        }
    }
}