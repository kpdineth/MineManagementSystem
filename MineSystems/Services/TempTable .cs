using System;
using System.Data;
using System.Linq;

// Define a namespace for your project
namespace MineSystems.Services
{
    // Create a TempTable class
    public class TempTable
    {
        // Declare a private DataTable instance variable to store the in-memory table
        private readonly DataTable _table;

        // Constructor for the TempTable class
        public TempTable()
        {
            // Initialize the DataTable with a table name
            _table = new DataTable("MyInMemoryTable");

            // Add columns (ID, Name, and Age) to the DataTable
            _table.Columns.Add("ID", typeof(int));
            _table.Columns.Add("Name", typeof(string));
            _table.Columns.Add("Age", typeof(int));
        }

        // Insert method to add a new row to the in-memory table
        public void Insert(int id, string name, int age)
        {
            // Create a new DataRow
            DataRow newRow = _table.NewRow();

            // Set the values for each column in the DataRow
            newRow["ID"] = id;
            newRow["Name"] = name;
            newRow["Age"] = age;

            // Add the new DataRow to the DataTable
            _table.Rows.Add(newRow);
        }

        // Get method to find a row in the in-memory table by ID
        public DataRow Get(int id)
        {
            // Use LINQ to find the first row with the specified ID, or return null if not found
            //return _table.AsEnumerable().FirstOrDefault(row => row.Field<int>("ID") == id);

            DataRow[] foundRows = _table.Select($"ID = {id}");

            if (foundRows.Length > 0)
            {
                return foundRows[0];
            }

            return null;


        }

        // Update method to modify an existing row in the in-memory table
        public void Update(int id, string name, int age)
        {
            // Find the row to update using the Get method
            DataRow rowToUpdate = Get(id);

            // If the row exists, update the Name and Age columns
            if (rowToUpdate != null)
            {
                rowToUpdate["Name"] = name;
                rowToUpdate["Age"] = age;
            }
        }

        // Delete method to remove a row from the in-memory table by ID
        public void Delete(int id)
        {
            // Find the row to delete using the Get method
            DataRow rowToDelete = Get(id);

            // If the row exists, remove it from the DataTable
            if (rowToDelete != null)
            {
                _table.Rows.Remove(rowToDelete);
            }
        }

        public void PrintTable()
        {
            Console.WriteLine("TempTable contents:");

            foreach (DataRow row in _table.Rows)
            {
                Console.WriteLine($"ID: {row["ID"]}, Name: {row["Name"]}, Age: {row["Age"]}");
            }
        }

    }
}
