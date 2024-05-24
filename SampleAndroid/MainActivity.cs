using DSoft.UI.Grid;
using SampleAndroid.Data.Grid;

namespace SampleAndroid
{
    [Activity(Label = "@string/app_name", MainLauncher = false)]
    public class MainActivity : DSGridViewActivity
    {
        protected override void OnCreate(Bundle? savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            //set that data source and the table name
            DataSource = new ExampleDataSet(this);
            TableName = "DT1";
        }
    }
}