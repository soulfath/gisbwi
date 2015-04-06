using System;
using System.Web;
using EGIS.Web.Controls;
using EGIS.ShapeFileLib;
using System.Collections.Generic;
using System.Drawing;
using System.Collections;
using System.Web.Caching;
using GISBWI.CustomClass.CustomRenderClass;
using System.Linq;
using System.Web.Script.Serialization;

namespace GISBWI.Handler.MapHttpHandler
{
 
    public class ExampleMapHandler : TiledMapHandler
    {
        private List<ShapeFile> layers;

        protected override Color MapBackgroundColor
        {
            get
            {
                Color newColor = Color.FromArgb(255, Color.White);
                return newColor;
            }
        }
                
        protected override bool CacheOnServer
        {
            get
            {
                return false; //set false during testing
                //return true;
            }
        }

        private static ICustomRenderSettings CreateRecordRenderSettings(ShapeFile sf)
        {
            //RecordTypeCustomRenderSettings rs = new RecordTypeCustomRenderSettings(sf.RenderSettings,);
            Dictionary<string, Color> colors = new Dictionary<string, Color>();
            colors.Add("Jawa Barat", Color.Green);
            colors.Add("Jawa Timur", Color.Red);
            colors.Add("Jawa Tengah", Color.Blue);

            RecordTypeCustomRenderSettings rs = new RecordTypeCustomRenderSettings(sf.RenderSettings, "PROV", colors);
            //synchronize the creation. Another thread may be attempting to render
            //the map or create a custom render settings and reading from the 
            //internal DBFReader is not thread safe
            lock (EGIS.ShapeFileLib.ShapeFile.Sync)
            {
                return rs;
                //return CustomRenderSettingsUtil.CreateQuantileCustomRenderSettings(
                //    sf.RenderSettings,
                //    populationColors,
                //    "KODE_KAB");
            }
                
        }

        //private RectangleF getExtent(HttpContext context)
        //{
        //    string[] _name = context.Request["mapname"].ToString().Split(',');
        //    List<ShapeFile> layers1 = new List<ShapeFile>();

        //    foreach (string s in _name)
        //    {
        //        if (s != "")
        //        {
        //            string shapeFilePath = context.Server.MapPath("/SHP_files/" + s + ".shp");
        //            ShapeFile sf = new ShapeFile(shapeFilePath);
        //            sf.RenderSettings.UseToolTip = true;
        //            sf.RenderSettings.CustomRenderSettings = CustomRenderSettingsUtil.CreateRandomColorCustomRenderSettings(sf.RenderSettings, 1);
        //            layers1.Add(sf);
        //        }
        //    }
        //    return layers1[0].GetActualExtent();
        //}

        protected override List<ShapeFile> CreateMapLayers(HttpContext context)
        {
            //HttpContext.Current.Response.AppendHeader("Cache-Control", "no-cache"); 
            string[] _name = context.Request["mapname"].ToString().Split(',');
            //string clear = context.Request["clear"].ToString();

            //string _name = context.Request["mapname"].ToString();

            layers = new List<ShapeFile>();

            foreach (string s in _name)
            {
                if (s != "")
                {
                    string shapeFilePath = context.Server.MapPath("/SHP_files/" + s + "/" + s +".shp");
                    ShapeFile sf = new ShapeFile(shapeFilePath);
                    sf.RenderSettings.UseToolTip = true;
                    sf.RenderSettings.LineDashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
                    sf.RenderSettings.CustomRenderSettings = CustomRenderSettingsUtil.CreateRandomColorCustomRenderSettings(sf.RenderSettings, 1);
                    layers.Add(sf);
                }
            }
            //load the shapefiles         
            //if (_name=="land")
            //{
            //    string shapeFilePath = context.Server.MapPath("/demos/JavaDistrict/Java_Districts.shp");
            //    ShapeFile sf = new ShapeFile(shapeFilePath);
            //    sf.RenderSettings.FieldName = "NAMA_KAB";
            //    sf.RenderSettings.UseToolTip = true;
            //    sf.RenderSettings.CustomRenderSettings = CreateRecordRenderSettings(sf); //CustomRenderSettingsUtil.CreateRandomColorCustomRenderSettings(sf.RenderSettings, 1);

            //    layers.Add(sf);
            //}
            //if (_name=="river")
            //{
            //    string path1 = context.Server.MapPath("/demos/Sungai_Jawa/Sungai_Jawa.shp");

            //    ShapeFile sf1 = new ShapeFile(path1);

            //    sf1.RenderSettings.FillColor = Color.Red;
            //    sf1.RenderSettings.MinPixelPenWidth = 4;
            //    sf1.RenderSettings.MaxPixelPenWidth = 5;

            //    layers.Add(sf1);
            //}
            
            
            //set the field name used to label the shapes
            
            //

            //set some CustomRenderSettings depending on the render type selected by the user
            //int renderSettingsType = 0;
            //int.TryParse(context.Request["rendertype"], out renderSettingsType);
            //if (renderSettingsType == 0)
            //{
            //    layers[0].RenderSettings.CustomRenderSettings = CreatePopulationRenderSettings(layers[0]);
            //}
            //else if (renderSettingsType == 1)
            //{
            //    layers[0].RenderSettings.CustomRenderSettings = null;
            //}
            //else
            //{
            //    layers[0].RenderSettings.CustomRenderSettings = CustomRenderSettingsUtil.CreateRandomColorCustomRenderSettings(layers[0].RenderSettings, 1);
            //}
            
            return layers;           
        }

        protected override void GetCustomTooltipText(HttpContext context, ShapeFile layer, int recordIndex, ref string tooltipText)
        {
            //override the default ToolTip text - we will just return all attributes for the record
            if (recordIndex >= 0)
            {
                string[] fieldNames = layer.RenderSettings.DbfReader.GetFieldNames();
                string[] values = layer.RenderSettings.DbfReader.GetFields(recordIndex);

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("<table>");
                for (int n = 0; n < fieldNames.Length; ++n)
                {
                    sb.Append("<tr>");
                    sb.Append("<td>").Append(fieldNames[n]).Append("</td>");
                    sb.Append("<td>").Append(values[n]).Append("</td>");
                    sb.Append("</tr>");
                }
                sb.Append("</table>");
                tooltipText = sb.ToString();
                layer.RenderSettings.DbfReader.Close();
            }

        }


        /// <summary>
        /// override the CreateCachePath member
        /// </summary>
        /// <param name="context"></param>
        /// <param name="tileX"></param>
        /// <param name="tileY"></param>
        /// <param name="zoom"></param>
        /// <returns></returns>
        /// <remarks>This member is overriden because we want to create a unique cache path based on the 
        /// renderSettingsType parameter. the rendertype parameter is set in the client javascript when
        /// the combobox selection is changed</remarks>
        protected override string  CreateCachePath(HttpContext context, int tileX, int tileY, int zoom)
        {
            int renderSettingsType = 0;
            int.TryParse(context.Request["rendertype"], out renderSettingsType);
            return CreateCachePath(context.Server.MapPath(CacheDirectory), tileX, tileY, zoom, renderSettingsType); 	     
        }

        private static string CreateCachePath(string cacheDirectory, int tileX, int tileY, int zoom, int renderType)
        {
            string file = string.Format("{0}_{1}_{2}_{3}.png", new object[] { tileX, tileY, zoom, renderType });
            return System.IO.Path.Combine(cacheDirectory, file);
        }

        protected override void OnBeginRequest(HttpContext context)
        {
            base.OnBeginRequest(context);

            //if (context.Request.Params["getextent"] != null)
            //{
            //    var extent = getExtent(context);
            //    JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            //    string serEmployee = javaScriptSerializer.Serialize(extent);
            //    context.Response.ContentType = "text/html";
            //    context.Response.Write(serEmployee);
            //    return;
            //}
        }

        protected override void OnEndRequest(HttpContext context)
        {
            //base.OnEndRequest(context);
            foreach (ShapeFile sf in layers)
            {
                sf.Close();
            }
            //layers[0].Close();
        }
    }

}
