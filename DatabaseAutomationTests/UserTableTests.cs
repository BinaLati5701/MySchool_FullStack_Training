using Microsoft.Data.SqlClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DatabaseAutomationTests;

[TestClass]
public class UserTableTests
{
    private SqlConnection? connection;

    [TestInitialize]
    public void Setup()
    {
        string connectionString =
            "Server=localhost\\SQLEXPRESS;Database=TestDB;Trusted_Connection=True;TrustServerCertificate=True;";

        connection = new SqlConnection(connectionString);

        connection.Open();
    }

    [TestMethod]
    public void GetAllUsers_ReturnsUsers()      
    {
        string query = "SELECT COUNT(*) FROM users";

        SqlCommand command = new SqlCommand(query, connection!);

        int usersCount = (int)command.ExecuteScalar();

        Assert.IsGreaterThan(0, usersCount, "No users were returned from database.");
    }

    [TestMethod]
    public void GetUserById2_ReturnsJohn()
    {
        string query = "SELECT first_name FROM users WHERE id = 2";

        SqlCommand command = new SqlCommand(query, connection!);

        object result = command.ExecuteScalar();

        Assert.IsNotNull(result);

        string actualFirstName = result.ToString()!;

        Assert.AreEqual("John", actualFirstName);
    }

    [TestMethod]
    public void GetUserById3_ReturnsMike()
    {
        string query = "SELECT first_name FROM users WHERE id = 3";

        SqlCommand command = new SqlCommand(query, connection!);

        object result = command.ExecuteScalar();

        Assert.IsNotNull(result);

        string actualFirstName = result.ToString()!;

        Assert.AreEqual("Mike", actualFirstName);
    }

    [TestMethod]
    public void FirstName_MaxLength_Allows50Characters()
    {
        string query = "SELECT first_name FROM users WHERE id = 4";

        SqlCommand command = new SqlCommand(query, connection!);

        object? result = command.ExecuteScalar();

        Assert.IsNotNull(result);

        string actualFirstName = result.ToString()!;

        Assert.AreEqual(50, actualFirstName.Length);
    }

    [TestMethod]
    public void FirstName_OverMaxLength_ReturnsError()
    {
        string query =
            "INSERT INTO users VALUES (5, 'AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA')";

        SqlCommand command = new SqlCommand(query, connection);

        try
        {
            command.ExecuteNonQuery();
            Assert.Fail("Expected SqlException was not thrown");
        }
        catch (SqlException)
        {
            // Expected exception
        }
    }

    [TestCleanup]
    public void Teardown()
    {
        connection?.Close();
    }
}