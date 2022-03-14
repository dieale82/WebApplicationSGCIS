using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class EditForm : System.Web.UI.UserControl
{
    private object _dataItem = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtName.Text)) {
            DataTable data = new DataTable();
            data.Columns.Add("PersonTypeId", typeof(int));
            data.Columns.Add("PersonTypeDescription");
            SGCISSoapServices.ServiceClient serviceClient = new SGCISSoapServices.ServiceClient();
            var personTypesList = serviceClient.GetPersonTypes().ToList();

            foreach (SGCISSoapServices.BEPersonType servicePerson in personTypesList)
            {
                data.Rows.Add(servicePerson.PersonTypeId, servicePerson.PersonTypeDescription);
            }

            cmdType.DataSource = data;
            cmdType.DataTextField = "PersonTypeDescription";
            cmdType.DataValueField = "PersonTypeId";
            cmdType.DataBind();
        }        
    }

    public object DataItem
    {
        get
        {
            return this._dataItem;
        }
        set
        {
            this._dataItem = value;
        }
    }
}