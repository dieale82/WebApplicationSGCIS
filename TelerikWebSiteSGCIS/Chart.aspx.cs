using SGCISSoapServices;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Chart : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Or you can use the datatable standar from radGrid instead
        DataTable data = Session["Data"] as DataTable;
        DataRow[] changedRows = data.Select("ID = " + Request.QueryString["idPerson"]);

        List<BEPerson> originalPersonData = Session["OriginalData"] as List<BEPerson>;
        BEPerson personToShow = originalPersonData.Where(x => x.PersonId.Equals(Convert.ToInt32(Request.QueryString["idPerson"]))).FirstOrDefault();

        lblPersonName.InnerText = personToShow.PersonName;
        lblAge.InnerText = personToShow.PersonAge.ToString();
        lblType.InnerText = personToShow.PersonTypeDescription;

    }

    [System.Web.Services.WebMethod]
    public static string GetCharData()
    {
        List<DataForChart> chartData = new List<DataForChart>();
        for (int a = 0; a < 10; a++)
        {
            chartData.Add(new DataForChart()
            {
                label = RandomDay().ToString("dd'/'MM'/'yyyy"),
                y = gen.Next(0, 40)
            });
        }

        var json = new JavaScriptSerializer().Serialize(chartData);

        return json;
    }

    private static Random gen = new Random();
    static DateTime RandomDay()
    {
        DateTime start = new DateTime(1995, 1, 1);
        int range = (DateTime.Today - start).Days;
        return start.AddDays(gen.Next(range));
    }

    public class DataForChart
    { 
        public string label { get; set; }
        public int y { get; set; }
    }
}