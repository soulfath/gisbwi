using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EGIS.ShapeFileLib;
using EGIS.Web.Controls;

namespace GISBWI.CustomClass.CustomRenderClass
{
    public class RecordTypeCustomRenderSettings : ICustomRenderSettings
    {
        private List<System.Drawing.Color> colorList;
        RenderSettings defaultSettings;
        public RecordTypeCustomRenderSettings(RenderSettings defaultSettings, string typeField, Dictionary<string,System.Drawing.Color> recordtypeColors)
        {
            this.defaultSettings = defaultSettings;
            BuildColorList(defaultSettings, typeField, recordtypeColors);           
        }

        private void BuildColorList(RenderSettings defaultSettings, string typeField,  Dictionary<string, System.Drawing.Color> roadtypeColors)
        {
            int fieldIndex = defaultSettings.DbfReader.IndexOfFieldName(typeField);
            if(fieldIndex >=0)
            {
                colorList = new List<System.Drawing.Color>();
                int numRecords = defaultSettings.DbfReader.DbfRecordHeader.RecordCount;
                for (int n = 0; n < numRecords; ++n)
                {
                    string nextField = defaultSettings.DbfReader.GetField(n, fieldIndex).Trim();
                    if (roadtypeColors.ContainsKey(nextField))
                    {
                        colorList.Add(roadtypeColors[nextField]);
                    }
                    else
                    {
                        colorList.Add(defaultSettings.FillColor);
                    }
                }                
            }
        }

        #region ICustomRenderSettings Members

        public System.Drawing.Color GetRecordFillColor(int recordNumber)
        {
            if (colorList != null)
            {
                return colorList[recordNumber];
            }
            return defaultSettings.FillColor;
        }

        public System.Drawing.Color GetRecordFontColor(int recordNumber)
        {
            return defaultSettings.FontColor;
        }

        public System.Drawing.Image GetRecordImageSymbol(int recordNumber)
        {
            return defaultSettings.GetImageSymbol();
        }

        public System.Drawing.Color GetRecordOutlineColor(int recordNumber)
        {
            return defaultSettings.OutlineColor;
        }

        public string GetRecordToolTip(int recordNumber)
        {
            return "";
        }

        public bool RenderShape(int recordNumber)
        {
            return true;
        }

        public bool UseCustomImageSymbols
        {
            get { return false; }
        }

        public bool UseCustomTooltips
        {
            get { return false; }
        }

        #endregion
    }
}