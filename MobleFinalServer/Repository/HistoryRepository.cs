namespace MobleFinalServer.Repository
{
    public class HistoryRepository
    {
        private string connectionUrl;
        public HistoryRepository(string connectionUrl) { 
            this.connectionUrl = connectionUrl;
        }
    }
}
