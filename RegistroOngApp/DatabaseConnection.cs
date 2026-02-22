using Microsoft.Data.SqlClient;

public sealed class DatabaseConnection
{
    private static DatabaseConnection _instance;
    private readonly string _connectionString ="Server=TU_SERVIDOR;Database=OngDB;Integrated Security=True;TrustServerCertificate=True;"; 
    public SqlConnection Connection { get; private set; }

    // Constructor privado: previene instanciación externa 
    private DatabaseConnection()
    {
        Connection = new SqlConnection(_connectionString);
    }

    // Punto de acceso global único 
    public static DatabaseConnection Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new DatabaseConnection();
            }
            return _instance;
        }
    }
}

