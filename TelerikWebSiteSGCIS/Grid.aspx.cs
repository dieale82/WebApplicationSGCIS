using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class Grid : System.Web.UI.Page
{
    private enum AnswerTypes
    {
        Error = 0,
        Succesful = 1,
    }

    public DataTable Persons
    {
        get
        {
            //This can be replaced with the session variable List <Person> Persons, just is necessary to call a method
            //that call the GetPersons Service, and then map the results to a Datatable object

            DataTable data = Session["Data"] as DataTable;

            if (data == null)
            {
                data = GetPersons();
                Session["Data"] = data;
            }

            return data;
        }
    }

    public DataTable GetPersons()
    {
        DataTable data = new DataTable();
        data.Columns.Add("ID", typeof(int));
        data.Columns.Add("Name");
        data.Columns.Add("Age", typeof(int)).DefaultValue = 0;
        data.Columns.Add("Type");
        data.Columns.Add("PersonType", typeof(int));
        data.Columns.Add("Actions");
        data.PrimaryKey = new DataColumn[] { data.Columns["ID"] };

        SGCISSoapServices.ServiceClient serviceClient = new SGCISSoapServices.ServiceClient();
        var personList = serviceClient.GetPersons().ToList();
        Session["OriginalData"] = personList;

        foreach (SGCISSoapServices.BEPerson servicePerson in personList)
        {
            data.Rows.Add(servicePerson.PersonId, servicePerson.PersonName, servicePerson.PersonAge, servicePerson.PersonTypeDescription, servicePerson.PersonTypeId, "");
        }
        return data;
    }

    protected void RadGrid1_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        RadGrid1.DataSource = this.Persons;
    }

    protected void RadGrid1_UpdateCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
    {
        UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
        var personToUpdate = MapToServiceObject(userControl);

        SGCISSoapServices.ServiceClient serviceClient = new SGCISSoapServices.ServiceClient();
        var personToUpdateResponse = serviceClient.UpdatePerson(personToUpdate);

        if (personToUpdateResponse.answerTypeId.Equals((int)AnswerTypes.Succesful))
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            DataRow[] changedRows = this.Persons.Select("ID = " + editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["ID"]);

            Hashtable newValues = new Hashtable();
            newValues["ID"] = personToUpdateResponse.rowId;
            newValues["Age"] = Convert.ToInt32((userControl.FindControl("txtAge") as TextBox).Text);
            newValues["Type"] = (userControl.FindControl("cmdType") as DropDownList).SelectedItem.Text;
            newValues["PersonType"] = (userControl.FindControl("cmdType") as DropDownList).SelectedItem.Value;
            newValues["Name"] = (userControl.FindControl("txtName") as TextBox).Text;

            changedRows[0].BeginEdit();

            foreach (DictionaryEntry entry in newValues)
            {
                changedRows[0][(string)entry.Key] = entry.Value;
            }
            changedRows[0].EndEdit();
            this.Persons.AcceptChanges();
        }
    }

    protected void RadGrid1_InsertCommand(object sender, GridCommandEventArgs e)
    {
        UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
        var personToAdd = MapToServiceObject(userControl);

        SGCISSoapServices.ServiceClient serviceClient = new SGCISSoapServices.ServiceClient();
        var personToAddResponse = serviceClient.CreatePerson(personToAdd);

        if (personToAddResponse.answerTypeId.Equals((int)AnswerTypes.Succesful))
        {

            Hashtable newValues = new Hashtable();

            DataRow newRow = this.Persons.NewRow();

            newValues["ID"] = personToAddResponse.rowId;
            newValues["Age"] = Convert.ToInt32((userControl.FindControl("txtAge") as TextBox).Text);
            newValues["Type"] = (userControl.FindControl("cmdType") as DropDownList).SelectedItem.Text;
            newValues["PersonType"] = (userControl.FindControl("cmdType") as DropDownList).SelectedItem.Value;
            newValues["Name"] = (userControl.FindControl("txtName") as TextBox).Text;

            foreach (DictionaryEntry entry in newValues)
            {
                newRow[(string)entry.Key] = entry.Value;
            }
            this.Persons.Rows.Add(newRow);
            this.Persons.AcceptChanges();
        }
    }

    protected void RadGrid1_DeleteCommand(object sender, GridCommandEventArgs e)
    {
        int personToDeleteId = Convert.ToInt32((e.Item as GridDataItem).OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ID"].ToString());

        SGCISSoapServices.ServiceClient serviceClient = new SGCISSoapServices.ServiceClient();
        var personToDeleteResponse = serviceClient.DeletePerson(personToDeleteId);

        if (personToDeleteResponse.answerTypeId.Equals((int)AnswerTypes.Succesful))
        {            
            DataTable personTable = this.Persons;
            if (personTable.Rows.Find(personToDeleteId.ToString()) != null)
            {
                personTable.Rows.Find(personToDeleteId.ToString()).Delete();
                personTable.AcceptChanges();
            }
        }       
    }

    private SGCISSoapServices.BEPerson MapToServiceObject(UserControl userControl)
    {
        return new SGCISSoapServices.BEPerson()
        {
            PersonId = Convert.ToInt32((userControl.FindControl("txtId") as TextBox).Text),
            PersonTypeId = Convert.ToInt32((userControl.FindControl("cmdType") as DropDownList).SelectedItem.Value),
            PersonName = (userControl.FindControl("txtName") as TextBox).Text,
            PersonAge = Convert.ToInt32((userControl.FindControl("txtAge") as TextBox).Text)
        };
    }
    

    protected void RadGrid1_PreRender(object sender, EventArgs e)
    {
        foreach (GridDataItem item in RadGrid1.Items)
        {
            if (item["PersonType"].Text != "1")
            {
                item["btnReport"].Text = "";
            }
        }
    }

    protected void RadGrid1_ItemCommand(object sender, GridCommandEventArgs e)
    {
        if (e.CommandName == "Report")
        {
            string personId = (e.Item as GridDataItem).OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ID"].ToString();
            string windowsParams = "scrollbars=no,resizable=no,status=no,location=no,toolbar=no,menubar=no,width=1000,height=650";            

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('Chart.aspx?idPerson="+ personId + "','window','"+ windowsParams + "')", true);            
        }
    }
}

