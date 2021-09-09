using Wrapper;

namespace HomeWorkDataBase.Control
{
    delegate string Request(string nameTable, string column);
    static class Logon
    {
        static public DBWrapper DB;
        static public string nameServer;
        static public string nameDataBase;
        static public string nameUser;
        static public string paswordUser;
    }
}
