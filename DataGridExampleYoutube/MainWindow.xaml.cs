using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Controls.Primitives;
using System.Configuration;
using System.Diagnostics;
using System.Windows.Navigation;
using System.Data;
using System.Data.SQLite;
using System.Data.SqlClient;
using System.Collections;
using System.ComponentModel;

namespace DataGridExampleYoutube
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string mDbPath;
        SQLiteConnection mConn;
        SQLiteDataAdapter mAdapter;
        DataTable mTable;
        SQLiteCommand mCmd;
        int tabindex = -1;
        int currenttabindex = -1;
        string editcategoryid, editcategoryname, editcategorydescription;
        string editbrandid, editbrandname, editbrandcategoryid;
        string edititemid, edititemname, edititemquantity, edititemproductid;
        string editproductid, editproductname, editproductcategoryid, editproductbrandid, editproductmeasureunit, editproductprice;
        //SQLiteDataReader reader;
        object item;
        public MainWindow()
        {
            InitializeComponent();
            FillDataGrid();

        }

        private void FillDataGrid()
        {
            mDbPath = "Data Source=" + AppDomain.CurrentDomain.BaseDirectory + ConfigurationManager.ConnectionStrings["mDbPath"].ConnectionString;
            try
            {
                using (mConn = new SQLiteConnection(mDbPath))
                {
                    mConn.Open();


                    if (tabindex == 0 && currenttabindex != 0)//Category Table Tab in Database Tab
                    {
                        mCmd = new SQLiteCommand("SELECT id, category_name, category_description FROM tbl_category order by id", mConn);

                        mCmd.ExecuteNonQuery();
                        mAdapter = new SQLiteDataAdapter(mCmd);
                        mTable = new DataTable("tbl_category");
                        mAdapter.Fill(mTable);
                        mConn.Close();
                        categoryDataGridatDatabase.ItemsSource = mTable.DefaultView;
                        mAdapter.Update(mTable);

                        this.categoryDataGridatDatabase.Columns[0].Header = "Id";
                        this.categoryDataGridatDatabase.Columns[1].Header = "Category Name";
                        this.categoryDataGridatDatabase.Columns[2].Header = "Category Description";
                    }
                    else if (tabindex == 1 && currenttabindex != 1)//Items Table Tab in Database Tab
                    {
                        mCmd = new SQLiteCommand("SELECT id, item_name, quantity, product_id FROM tbl_item order by id", mConn);

                        mCmd.ExecuteNonQuery();
                        mAdapter = new SQLiteDataAdapter(mCmd);
                        mTable = new DataTable("tbl_item");
                        mAdapter.Fill(mTable);
                        mConn.Close();
                        itemsDataGridatDatabase.ItemsSource = mTable.DefaultView;
                        mAdapter.Update(mTable);

                        this.itemsDataGridatDatabase.Columns[0].Header = "Id";
                        this.itemsDataGridatDatabase.Columns[1].Header = "Item Name";
                        this.itemsDataGridatDatabase.Columns[2].Header = "Quantity";
                        this.itemsDataGridatDatabase.Columns[3].Header = "Product Id";
                    }



                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Message : " + ex);
            }

        }
        #region CategoryTable Functions
        private void Button_Add_CategoryElement_Click(object sender, RoutedEventArgs e)
        {
            AddCategoryElementInputBox.Visibility = System.Windows.Visibility.Visible;
        }

        private void Button_Delete_CategoryElement_Click(object sender, RoutedEventArgs e)
        {
            //Save Changes
            mDbPath = "Data Source=" + AppDomain.CurrentDomain.BaseDirectory + ConfigurationManager.ConnectionStrings["mDbPath"].ConnectionString;
            try
            {
                using (mConn = new SQLiteConnection(mDbPath))
                {
                    mConn.Open();
                    //Insert Command
                    mCmd = new SQLiteCommand("delete from tbl_category where id = " + Int32.Parse(editcategoryid), mConn);
                    mCmd.ExecuteNonQuery();
                    mCmd = null;
                    //Select Command
                    mCmd = new SQLiteCommand("SELECT id, category_name, category_description FROM tbl_category order by id", mConn);
                    mCmd.ExecuteNonQuery();
                    mAdapter = new SQLiteDataAdapter(mCmd);
                    mTable = new DataTable("tbl_category");

                    mAdapter.Fill(mTable);
                    mConn.Close();
                    categoryDataGridatDatabase.ItemsSource = mTable.DefaultView;
                    mAdapter.Update(mTable);
                    this.categoryDataGridatDatabase.Columns[0].Header = "Id";
                    this.categoryDataGridatDatabase.Columns[1].Header = "Category Name";
                    this.categoryDataGridatDatabase.Columns[2].Header = "Category Description";


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Message : " + ex);
            }
        }

        private void Button_Edit_CategoryElement_Click(object sender, RoutedEventArgs e)
        {
            InputEditCategoryIdBox.Text = editcategoryid;
            InputEditCategoryNameBox.Text = editcategoryname;
            InputEditCategoryDescriptionBox.Text = editcategorydescription;
            EditCategoryElementInputBox.Visibility = System.Windows.Visibility.Visible;

        }

        private void Button_Submit_Category(object sender, RoutedEventArgs e)
        {
            AddCategoryElementInputBox.Visibility = System.Windows.Visibility.Collapsed;
            //Do Something Here
            mDbPath = "Data Source=" + AppDomain.CurrentDomain.BaseDirectory + ConfigurationManager.ConnectionStrings["mDbPath"].ConnectionString;
            try
            {
                using (mConn = new SQLiteConnection(mDbPath))
                {
                    mConn.Open();
                    //Insert Command
                    mCmd = new SQLiteCommand("insert into tbl_category (id, category_name, category_description) values('" + Int32.Parse(this.InputCategoryIdBox.Text) + "','" + this.InputCategoryNameBox.Text.ToString() + "','" + this.InputCategoryDescriptionBox.Text.ToString() + "')", mConn);
                    mCmd.ExecuteNonQuery();
                    mCmd = null;
                    //Select Command
                    mCmd = new SQLiteCommand("SELECT id, category_name, category_description FROM tbl_category order by id", mConn);
                    mCmd.ExecuteNonQuery();
                    mAdapter = new SQLiteDataAdapter(mCmd);
                    mTable = new DataTable("tbl_category");

                    mAdapter.Fill(mTable);
                    mConn.Close();
                    categoryDataGridatDatabase.ItemsSource = mTable.DefaultView;
                    mAdapter.Update(mTable);
                    this.categoryDataGridatDatabase.Columns[0].Header = "Id";
                    this.categoryDataGridatDatabase.Columns[1].Header = "Category Name";
                    this.categoryDataGridatDatabase.Columns[2].Header = "Category Description";


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Message : " + ex);
            }




            //ENd Here
            InputCategoryIdBox.Text = String.Empty;
            InputCategoryNameBox.Text = String.Empty;
            InputCategoryDescriptionBox.Text = String.Empty;

        }

        private void Button_Cancel_Category(object sender, RoutedEventArgs e)
        {
            AddCategoryElementInputBox.Visibility = System.Windows.Visibility.Collapsed;
            InputCategoryIdBox.Text = String.Empty;
            InputCategoryNameBox.Text = String.Empty;
            InputCategoryDescriptionBox.Text = String.Empty;
        }

        private void ButtonEdit_Update_Category(object sender, RoutedEventArgs e)
        {

            //Do Something 
            if (!InputEditCategoryIdBox.Text.Equals(editcategoryid) || !InputEditCategoryNameBox.Text.Equals(editcategoryname) || !InputEditCategoryDescriptionBox.Text.Equals(editcategorydescription))
            {
                EditCategoryElementInputBox.Visibility = System.Windows.Visibility.Collapsed;
                try
                {
                    using (mConn = new SQLiteConnection(mDbPath))
                    {
                        mConn.Open();
                        //Insert Command
                        mCmd = new SQLiteCommand("update tbl_category SET category_name ='" + this.InputEditCategoryNameBox.Text.ToString() + "', category_description ='" + this.InputEditCategoryDescriptionBox.Text.ToString() + "' where id =" + Int32.Parse(this.InputEditCategoryIdBox.Text.ToString()), mConn);
                        mCmd.ExecuteNonQuery();
                        mCmd = null;
                        //Select Command
                        mCmd = new SQLiteCommand("SELECT id, category_name, category_description FROM tbl_category order by id", mConn);
                        mCmd.ExecuteNonQuery();
                        mAdapter = new SQLiteDataAdapter(mCmd);
                        mTable = new DataTable("tbl_category");

                        mAdapter.Fill(mTable);
                        mConn.Close();
                        categoryDataGridatDatabase.ItemsSource = mTable.DefaultView;
                        //mAdapter.Update(mTable);
                        this.categoryDataGridatDatabase.Columns[0].Header = "Id";
                        this.categoryDataGridatDatabase.Columns[1].Header = "Category Name";
                        this.categoryDataGridatDatabase.Columns[2].Header = "Category Description";

                        //mConn.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error Message : " + ex);
                }
                InputEditCategoryIdBox.Text = String.Empty;
                InputEditCategoryNameBox.Text = String.Empty;
                InputEditCategoryDescriptionBox.Text = String.Empty;
                editcategoryid = String.Empty;
                editcategoryname = String.Empty;
                editcategorydescription = String.Empty;
            }
            else
            {
                MessageBox.Show("No Entry Changed.");
            }


        }

        private void ButtonEdit_Cancel_Category(object sender, RoutedEventArgs e)
        {
            EditCategoryElementInputBox.Visibility = System.Windows.Visibility.Collapsed;
            InputEditCategoryIdBox.Text = String.Empty;
            InputEditCategoryNameBox.Text = String.Empty;
            InputEditCategoryDescriptionBox.Text = String.Empty;
        }
        #endregion



        #region ItemsTable Functions
        /*This function is altered. Debugging may be required.*/
        private void Button_Add_ItemElement_Click(object sender, RoutedEventArgs e)
        {
            AddItemElementInputBox.Visibility = System.Windows.Visibility.Visible;
        }

        /*This function is altered. Debugging may be required.*/
        private void Button_Delete_ItemElement_Click(object sender, RoutedEventArgs e)
        {
            //Save Changes
            mDbPath = "Data Source=" + AppDomain.CurrentDomain.BaseDirectory + ConfigurationManager.ConnectionStrings["mDbPath"].ConnectionString;
            try
            {
                using (mConn = new SQLiteConnection(mDbPath))
                {
                    mConn.Open();
                    //Insert Command
                    mCmd = new SQLiteCommand("delete from tbl_item where id = " + Int32.Parse(edititemid), mConn);
                    mCmd.ExecuteNonQuery();
                    mCmd = null;
                    //Select Command
                    mCmd = new SQLiteCommand("SELECT id, item_name, quantity, product_id FROM tbl_item order by id", mConn);
                    mCmd.ExecuteNonQuery();
                    mAdapter = new SQLiteDataAdapter(mCmd);
                    mTable = new DataTable("tbl_item");

                    mAdapter.Fill(mTable);
                    mConn.Close();
                    itemsDataGridatDatabase.ItemsSource = mTable.DefaultView;
                    mAdapter.Update(mTable);
                    this.itemsDataGridatDatabase.Columns[0].Header = "Id";
                    this.itemsDataGridatDatabase.Columns[1].Header = "Item Name";
                    this.itemsDataGridatDatabase.Columns[2].Header = "Quantity";
                    this.itemsDataGridatDatabase.Columns[3].Header = "Product Id";


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Message : " + ex);
            }
        }

        /*This function is altered. Debugging may be required.*/
        private void Button_Edit_ItemElement_Click(object sender, RoutedEventArgs e)
        {
            InputEditItemIdBox.Text = edititemid;
            InputEditItemNameBox.Text = edititemname;
            InputEditItemQuantityBox.Text = edititemquantity;
            InputEditItemProductIdBox.Text = edititemproductid;
            EditItemElementInputBox.Visibility = System.Windows.Visibility.Visible;

        }

        /*This function is altered. Debugging may be required.*/
        private void Button_Submit_Item(object sender, RoutedEventArgs e)
        {
            AddItemElementInputBox.Visibility = System.Windows.Visibility.Collapsed;
            //Do Something Here
            mDbPath = "Data Source=" + AppDomain.CurrentDomain.BaseDirectory + ConfigurationManager.ConnectionStrings["mDbPath"].ConnectionString;
            try
            {
                using (mConn = new SQLiteConnection(mDbPath))
                {
                    mConn.Open();
                    //Insert Command
                    mCmd = new SQLiteCommand("insert into tbl_item (id, item_name, quantity, product_id) values('" + Int32.Parse(this.InputItemIdBox.Text) + "','" + this.InputItemNameBox.Text.ToString() + "','" + Int32.Parse(this.InputItemQuantityBox.Text.ToString()) + "','" + Int32.Parse(this.InputItemProductIdBox.Text.ToString()) + "')", mConn);
                    mCmd.ExecuteNonQuery();
                    mCmd = null;
                    //Select Command
                    mCmd = new SQLiteCommand("SELECT id, item_name, quantity, product_id FROM tbl_item order by id", mConn);
                    mCmd.ExecuteNonQuery();
                    mAdapter = new SQLiteDataAdapter(mCmd);
                    mTable = new DataTable("tbl_item");

                    mAdapter.Fill(mTable);
                    mConn.Close();
                    itemsDataGridatDatabase.ItemsSource = mTable.DefaultView;
                    mAdapter.Update(mTable);
                    this.itemsDataGridatDatabase.Columns[0].Header = "Id";
                    this.itemsDataGridatDatabase.Columns[1].Header = "Item Name";
                    this.itemsDataGridatDatabase.Columns[2].Header = "Quantity";
                    this.itemsDataGridatDatabase.Columns[3].Header = "Product Id";


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Message : " + ex);
            }




            //ENd Here
            InputItemIdBox.Text = String.Empty;
            InputItemNameBox.Text = String.Empty;
            InputItemQuantityBox.Text = String.Empty;
            InputItemProductIdBox.Text = String.Empty;

        }

        /*This function is altered. Debugging may be required.*/
        private void Button_Cancel_Item(object sender, RoutedEventArgs e)
        {
            AddItemElementInputBox.Visibility = System.Windows.Visibility.Collapsed;
            InputItemIdBox.Text = String.Empty;
            InputItemNameBox.Text = String.Empty;
            InputItemQuantityBox.Text = String.Empty;
            InputItemProductIdBox.Text = String.Empty;
        }

        /*This function is altered. Debugging may be required.*/
        private void ButtonEdit_Update_Item(object sender, RoutedEventArgs e)
        {

            //Do Something 
            if (!InputEditItemIdBox.Text.Equals(edititemid) || !InputEditItemNameBox.Text.Equals(edititemname) || !InputEditItemQuantityBox.Text.Equals(edititemquantity) || !InputEditItemProductIdBox.Text.Equals(edititemproductid))
            {
                EditItemElementInputBox.Visibility = System.Windows.Visibility.Collapsed;
                try
                {
                    using (mConn = new SQLiteConnection(mDbPath))
                    {
                        mConn.Open();
                        //Insert Command
                        mCmd = new SQLiteCommand("update tbl_item SET item_name ='" + this.InputEditItemNameBox.Text.ToString() + "', quantity ='" + Int32.Parse(this.InputEditItemQuantityBox.Text.ToString()) + "', product_id ='" + Int32.Parse(this.InputEditItemProductIdBox.Text.ToString()) + "' where id =" + Int32.Parse(this.InputEditItemIdBox.Text.ToString()), mConn);
                        mCmd.ExecuteNonQuery();
                        mCmd = null;
                        //Select Command
                        mCmd = new SQLiteCommand("SELECT id, item_name, quantity, product_id FROM tbl_item order by id", mConn);
                        mCmd.ExecuteNonQuery();
                        mAdapter = new SQLiteDataAdapter(mCmd);
                        mTable = new DataTable("tbl_item");

                        mAdapter.Fill(mTable);
                        mConn.Close();
                        itemsDataGridatDatabase.ItemsSource = mTable.DefaultView;
                        //mAdapter.Update(mTable);
                        this.itemsDataGridatDatabase.Columns[0].Header = "Id";
                        this.itemsDataGridatDatabase.Columns[1].Header = "Item Name";
                        this.itemsDataGridatDatabase.Columns[2].Header = "Quantity";
                        this.itemsDataGridatDatabase.Columns[3].Header = "Product Id";

                        //mConn.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error Message : " + ex);
                }
                InputEditItemIdBox.Text = String.Empty;
                InputEditItemNameBox.Text = String.Empty;
                InputEditItemQuantityBox.Text = String.Empty;
                InputEditItemProductIdBox.Text = String.Empty;
                edititemid = String.Empty;
                edititemname = String.Empty;
                edititemquantity = String.Empty;
                edititemproductid = String.Empty;
            }
            else
            {
                MessageBox.Show("No Entry Changed.");
            }


        }

        /*This function is altered. Debugging may be required.*/
        private void ButtonEdit_Cancel_Item(object sender, RoutedEventArgs e)
        {
            EditItemElementInputBox.Visibility = System.Windows.Visibility.Collapsed;
            InputEditItemIdBox.Text = String.Empty;
            InputEditItemNameBox.Text = String.Empty;
            InputEditItemQuantityBox.Text = String.Empty;
            InputEditItemProductIdBox.Text = String.Empty;
        }
        #endregion



        private void OnTabItemChanged(object sender, SelectionChangedEventArgs e)
        {

            TabControl tabControl = sender as TabControl; // e.Source could have been used instead of sender as well
            TabItem item = tabControl.SelectedValue as TabItem;
            if (item.Name == "categorytableTab")
            {
                tabindex = 0;
                FillDataGrid();
                currenttabindex = 0;
            }
            else if (item.Name == "itemtableTab")
            {
                tabindex = 1;
                FillDataGrid();
                currenttabindex = 1;
            }


        }

        public IEnumerable<DataGridRow> GetDataGridRows(DataGrid grid)
        {
            var itemsSource = grid.ItemsSource as IEnumerable;
            if (null == itemsSource) yield return null;
            foreach (var item in itemsSource)
            {
                var row = grid.ItemContainerGenerator.ContainerFromItem(item) as DataGridRow;
                if (null != row) yield return row;
            }
        }

        private void categoryDataGridatDatabase_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (this.categoryDataGridatDatabase.SelectedItem != null)
                {
                    item = this.categoryDataGridatDatabase.SelectedItem;
                    editcategoryid = (this.categoryDataGridatDatabase.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text;
                    this.InputEditCategoryIdBox.Text = editcategoryid;
                    editcategoryname = (this.categoryDataGridatDatabase.SelectedCells[1].Column.GetCellContent(item) as TextBlock).Text;
                    this.InputEditCategoryNameBox.Text = editcategoryname;
                    editcategorydescription = (this.categoryDataGridatDatabase.SelectedCells[2].Column.GetCellContent(item) as TextBlock).Text;
                    this.InputEditCategoryDescriptionBox.Text = editcategorydescription;
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show("Message:" + exp);
            }
        }


        /*This method is altered. Debugging may be required.*/
        private void itemsDataGridatDatabase_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (this.itemsDataGridatDatabase.SelectedItem != null)
                {
                    item = this.itemsDataGridatDatabase.SelectedItem;
                    edititemid = (this.itemsDataGridatDatabase.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text;
                    this.InputEditItemIdBox.Text = edititemid;
                    edititemname = (this.itemsDataGridatDatabase.SelectedCells[1].Column.GetCellContent(item) as TextBlock).Text;
                    this.InputEditItemNameBox.Text = edititemname;
                    edititemquantity = (this.itemsDataGridatDatabase.SelectedCells[2].Column.GetCellContent(item) as TextBlock).Text;
                    this.InputEditItemQuantityBox.Text = edititemquantity;
                    edititemproductid = (this.itemsDataGridatDatabase.SelectedCells[3].Column.GetCellContent(item) as TextBlock).Text;
                    this.InputEditItemProductIdBox.Text = edititemproductid;
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show("Message:" + exp);
            }
        }


        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }
    }
}

